using Aspose.ThreeD;
using Aspose.ThreeD.Render;
using Aspose.ThreeD.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetBrowser.Renderers
{
    class BoundingBoxRenderer : EntityRenderer
    {
        private bool dirty;
        private Node node;
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
        private BoundingBox boundingBox = BoundingBox.Null;

        public Node Node
        {
            get => node;
            set
            {
                dirty |= node != value;
                boundingBox = node == null ? BoundingBox.Null : node.GetBoundingBox();
                if(boundingBox.Extent == BoundingBoxExtent.Null && node != null)
                {
                    var pos = node.Transform.Translation;
                    boundingBox = new BoundingBox(pos, pos);
                }
                node = value;
            }
        }

        public BoundingBoxRenderer()
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
            if (node == null)
                return;
            if (boundingBox.Extent != BoundingBoxExtent.Finite)
                return;
            if(dirty)
            {
                dirty = false;
                BuildWireframe(renderer);
            }
            if (indiceBuffer.Count > 0)
            {
                renderQueue.Add(RenderQueueGroupId.Geometries, pipeline, this, 0);
            }
        }
        public override void PrepareRenderQueue(Renderer renderer, IRenderQueue queue, Node node, Entity entity)
        {
            base.PrepareRenderQueue(renderer, queue, node, entity);
        }
        public override void RenderEntity(Renderer renderer, ICommandList cl, Node node, object renderableResource, int subEntity)
        {
            //get the command list for the geometries
            //bind the pipeline so GPU knows how to render the vector
            cl.BindPipeline(pipeline);
            //bind the vertex buffer that used by the vertex shader
            cl.BindVertexBuffer(verticeBuffer);
            cl.BindIndexBuffer(indiceBuffer);
            //prepare the view-projection matrix through the push constant
            constants.Write(renderer.Variables.MatrixViewProjection);
            constants.Commit(ShaderStage.VertexShader, cl);
            constants.Write(1.0f, 1.0f, 1.0f, 1.0f);
            constants.Commit(ShaderStage.FragmentShader, cl);
            //draw the lines
            cl.DrawIndex();
        }
        private void PrepareBoundary(ref float min, ref float max)
        {
            if(min == max)
            {
                min -= 1;
                max += 1;
            }
        }

        private void BuildWireframe(Renderer renderer)
        {
            var bbox = boundingBox;
            var min = new FVector3(bbox.Minimum);
            var max = new FVector3(bbox.Maximum);
            PrepareBoundary(ref min.x, ref max.x);
            PrepareBoundary(ref min.y, ref max.y);
            PrepareBoundary(ref min.z, ref max.z);
            float[] vectors = new float[] {
                min.x, min.y, min.z,
                max.x, min.y, min.z,
                max.x, min.y, max.z,
                min.x, min.y, max.z,

                min.x, max.y, min.z,
                max.x, max.y, min.z,
                max.x, max.y, max.z,
                min.x, max.y, max.z
            };

            int[] indices =
            {
                0, 1,
                1, 2,
                2, 3,
                3, 0,

                4, 5,
                5, 6,
                6, 7,
                7, 4,

                0, 4,
                1, 5,
                2, 6,
                3, 7
            };
            verticeBuffer.LoadData(vectors);
            indiceBuffer.LoadData(indices);
        }
    }
}
