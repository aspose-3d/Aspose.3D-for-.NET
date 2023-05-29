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
    /// This draws the wireframe of selected geometry.
    /// </summary>
    class WireframeRenderer : EntityRenderer
    {
        private bool dirty;
        private Entity geometry;
        /// <summary>
        /// The vertex buffer that stores the position of the normal lines
        /// </summary>
        private IVertexBuffer verticeBuffer;
        /// <summary>
        /// The vertex buffer that stores the position of the normal lines
        /// </summary>
        private IIndexBuffer indiceBuffer;
        /// <summary>
        /// The shader to render the normal
        /// </summary>
        private ShaderProgram shader;
        /// <summary>
        /// The pipeline to render the normal vectors
        /// </summary>
        private IPipeline pipeline;
        private PushConstant constants = new PushConstant();

        public Entity Geometry
        {
            get => geometry;
            set
            {
                dirty |= geometry != value;
                geometry = value;
            }
        }

        public WireframeRenderer()
            :base(null, EntityRendererFeatures.FrameEnd)
        {

        }
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
            rs.DepthFunction = CompareFunction.LEqual;
            rs.DepthMask = false;
            //Define the memory layout of the vertex
            var vd = new VertexDeclaration();
            vd.AddField(VertexFieldDataType.FVector3, VertexFieldSemantic.Position);
            //create the pipeline and vertex buffer
            pipeline = renderer.RenderFactory.CreatePipeline(shader, rs, vd, DrawOperation.Lines);
            verticeBuffer = renderer.RenderFactory.CreateVertexBuffer(vd);
            indiceBuffer = renderer.RenderFactory.CreateIndexBuffer();
        }
        public override void Dispose()
        {
            verticeBuffer.Dispose();
            indiceBuffer.Dispose();
            shader.Dispose();
            pipeline.Dispose();
            base.Dispose();
        }
        public override void FrameEnd(Renderer renderer, IRenderQueue renderQueue)
        {
            if (geometry == null)
                return;
            if(dirty)
            {
                dirty = false;
                BuildWireframe(renderer);
            }
            if (indiceBuffer.Count > 0)
            {
                //get the command list for the geometries
                renderQueue.Add(RenderQueueGroupId.Geometries, pipeline, this, 0);
            }
        }
        public override void RenderEntity(Renderer renderer, ICommandList cl, Node node, object renderableResource, int subEntity)
        {
            //bind the pipeline so GPU knows how to render the vector
            cl.BindPipeline(pipeline);
            //bind the vertex buffer that used by the vertex shader
            cl.BindVertexBuffer(verticeBuffer);
            cl.BindIndexBuffer(indiceBuffer);
            //prepare the view-projection matrix through the push constant
            var matWorld = new FMatrix4(geometry.ParentNode.EvaluateGlobalTransform(true));
            constants.Write(renderer.Variables.MatrixViewProjection * matWorld);
            constants.Commit(ShaderStage.VertexShader, cl);
            constants.Write(0.0f, 1.0f, 0.0f, 1.0f);
            constants.Commit(ShaderStage.FragmentShader, cl);
            //draw the lines
            cl.DrawIndex();
        }

        private void BuildWireframe(Renderer renderer)
        {
            var mc = geometry as IMeshConvertible;
            if(mc != null)
            {
                var mesh = mc.ToMesh();
                if (mesh != null)
                {
                    List<FVector3> vectors = new List<FVector3>();
                    for(int i = 0; i < mesh.ControlPoints.Count; i++)
                    {
                        var v = mesh.ControlPoints[i];
                        vectors.Add(new FVector3(v));
                    }
                    List<int> indices = new List<int>();
                    verticeBuffer.LoadData(vectors.ToArray());
                    foreach (var polygon in mesh.Polygons)
                    {
                        for (int i = 1; i < polygon.Length; i++)
                        {
                            indices.Add(polygon[i - 1]);
                            indices.Add(polygon[i]);
                        }
                        indices.Add(polygon[polygon.Length - 1]);
                        indices.Add(polygon[0]);
                    }
                    indiceBuffer.LoadData(indices.ToArray());
                }
            }
        }
    }
}
