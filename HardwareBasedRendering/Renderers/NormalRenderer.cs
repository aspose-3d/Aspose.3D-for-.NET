using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Render;
using Aspose.ThreeD.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetBrowser.Renderers
{
    /// <summary>
    /// This renderer will display normal vectors for the scene
    /// </summary>
    class NormalRenderer : EntityRenderer
    {
        /// <summary>
        /// The vertex buffer that stores the position of the normal lines
        /// </summary>
        private IVertexBuffer buffer;
        /// <summary>
        /// The shader to render the normal
        /// </summary>
        private ShaderProgram shader;
        /// <summary>
        /// The pipeline to render the normal vectors
        /// </summary>
        private IPipeline pipeline;
        /// <summary>
        /// Scene has reloaded, need to rebuild the normal vector lines
        /// </summary>
        private bool dirty = true;
        /// <summary>
        /// Count of normal vectors
        /// </summary>
        private int normals = 0;
        /// <summary>
        /// Storage for storing push constants used by the shader
        /// </summary>
        private MemoryStream ms = new MemoryStream(256);
        private BinaryWriter w;
        /// <summary>
        /// Gets or sets whether to display the normal vectors
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// This renderer will need the frame-end callback to render the normal vectors before submitting the render queue to GPU
        /// </summary>
        public NormalRenderer()
            :base("normal", EntityRendererFeatures.FrameEnd)
        {
            w = new BinaryWriter(ms);
        }
        /// <summary>
        /// Initialize the renderer
        /// </summary>
        /// <param name="renderer"></param>
        public override void Initialize(Renderer renderer)
        {
            base.Initialize(renderer);
            //create the shader program from the precompiled SPIR-V byte code
            var src = new SPIRVSource()
            {
                VertexShader = Shaders.NormalsVertex,
                FragmentShader = Shaders.NormalsFragment
            };
            shader = renderer.RenderFactory.CreateShaderProgram(src);
            //default render state
            var rs = new RenderState();
            //Define the memory layout of the vertex
            var vd = new VertexDeclaration();
            vd.AddField(VertexFieldDataType.FVector3, VertexFieldSemantic.Position);
            //create the pipeline and vertex buffer
            pipeline = renderer.RenderFactory.CreatePipeline(shader, rs, vd, DrawOperation.Lines);
            buffer = renderer.RenderFactory.CreateVertexBuffer(vd);
        }
        public override void Dispose()
        {
            base.Dispose();
            //Dispose the unmanaged resources
            buffer.Dispose();
            shader.Dispose();
            pipeline.Dispose();
        }
        public override void ResetSceneCache()
        {
            base.ResetSceneCache();
            //Mark the scene dirty
            dirty = true;
        }
        public override void FrameEnd(Renderer renderer)
        {
            //Render the normal only when Visible=true
            if (!Visible)
                return;
            //need to rebuild the normal lines
            if(dirty)
            {
                dirty = false;
                BuildNormals(renderer.Frustum.Scene);
            }
            if (normals > 0)
            {
                //get the command list for the geometries
                var cl = renderer.GetCommandList(RenderQueueGroupId.Geometries);
                //bind the pipeline so GPU knows how to render the vector
                cl.BindPipeline(pipeline);
                //bind the vertex buffer that used by the vertex shader
                cl.BindVertexBuffer(buffer);
                //prepare the view-projection matrix through the push constant
                w.BaseStream.Seek(0, SeekOrigin.Begin);
                w.Write(renderer.Variables.MatrixViewProjection);
                //send the push constant to GPU
                cl.PushConstants(ShaderStage.VertexShader, ms.GetBuffer());
                //draw the lines
                cl.Draw();
            }
        }
        /// <summary>
        /// Build the normal data from the scene,
        /// the vertex buffer stores the coordinate of the normal
        /// lines in world coordinate system.
        /// </summary>
        /// <param name="scene"></param>
        private void BuildNormals(Scene scene)
        {
            vecs.Clear();
            scene.RootNode.Accept(delegate (Node n)
            {
                var mc = n.Entity as IMeshConvertible;
                if (mc == null)
                    return true;
                SetNode(n);
                var mesh = mc.ToMesh();
                var ven = (VertexElementNormal)mesh.GetElement(VertexElementType.Normal);
                if (ven == null)
                    ven = PolygonModifier.GenerateNormal(mesh);
                var indirect = ven.ReferenceMode == ReferenceMode.IndexToDirect;
                //The normal data is mapped by polygon
                if(ven.MappingMode == MappingMode.Polygon)
                {
                    for(int i = 0; i < ven.Data.Count; i++)
                    {
                        var polygon = mesh.Polygons[i];
                        var center = CalculateCenter(polygon, mesh.ControlPoints);
                        var idx = indirect ? ven.Indices[i] : i;
                        var norm = ven.Data[idx];
                        DrawNormal(center, norm);
                    }
                }
                //The normal data is mapped by polygon vertex
                else if(ven.MappingMode == MappingMode.PolygonVertex)
                {
                    int vtx = 0;
                    for(int p = 0; p < mesh.PolygonCount; p++)
                    {
                        var polygon = mesh.Polygons[p];
                        for(int v = 0; v < polygon.Length; v++, vtx++)
                        {
                            var idx = indirect ? ven.Indices[vtx] : vtx;
                            var norm = ven.Data[idx];
                            var center = mesh.ControlPoints[polygon[v]];
                            DrawNormal(center, norm);
                        }
                    }
                }
                //The normal data is mapped by control point
                else if(ven.MappingMode == MappingMode.ControlPoint)
                {
                    int vtx = 0;
                    for(int p = 0; p < mesh.ControlPoints.Count; p++)
                    {
                        var center = mesh.ControlPoints[p];
                        var idx = indirect ? ven.Indices[p] : p;
                        var norm = ven.Data[idx];
                        DrawNormal(center, norm);
                    }

                }
                else
                {
                    Console.WriteLine("Unsupported mapping mode");
                }
                return true;
            });
            normals = vecs.Count / 2;
            //load the data to buffer
            buffer.LoadData(vecs.ToArray());

        }

        private Matrix4 tr;
        private Matrix4 tr2;
        private List<FVector3> vecs = new List<FVector3>();
        /// <summary>
        /// Precalculate the world transformation matrix and the world transformation matrix without translation(for transforming the direction of normal)
        /// </summary>
        /// <param name="n"></param>
        private void SetNode(Node n)
        {
            tr = n.EvaluateGlobalTransform(true);
            tr2 = tr;
            tr2.m30 = tr2.m31 = tr2.m32 = 0;
        }
        /// <summary>
        /// Draw the normal line
        /// </summary>
        /// <param name="origin">origin of the normal line</param>
        /// <param name="dir">direction of the normal</param>
        private void DrawNormal(Vector4 origin, Vector4 dir)
        {
            var p0 = new FVector3(tr * origin);
            vecs.Add(p0);
            vecs.Add(p0 + new FVector3(tr2 * dir));
        }

        /// <summary>
        /// calculate the mass center of the polygon,
        /// which will be used at the origin point of the normal vector
        /// when the mapping mode is by polygon
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="controlPoints"></param>
        /// <returns></returns>
        private Vector4 CalculateCenter(int[] polygon, IArrayList<Vector4> controlPoints)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            for(int i = 0; i < polygon.Length; i++)
            {
                var v = controlPoints[polygon[i]];
                x += v.x;
                y += v.y;
                z += v.z;
            }
            var inv = 1.0 / polygon.Length;
            return new Vector4(x * inv, y * inv, z * inv, 1);
        }
    }
}
