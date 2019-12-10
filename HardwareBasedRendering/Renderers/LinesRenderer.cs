using Aspose.ThreeD;
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
    /// The base class of <see cref="Grid"/> and <see cref="Axises"/>
    /// </summary>
    class Lines : Entity
    {
        public static EntityRendererKey Key = new EntityRendererKey("lines");
        public List<FVector3> lines = new List<FVector3>();
        public bool dirty = true;

        /// <summary>
        /// Return the correct key so the <see cref="Renderer"/> can find the <see cref="LinesRenderer"/> to render the entity
        /// </summary>
        /// <returns></returns>
        public override EntityRendererKey GetEntityRendererKey()
        {
            return Key;
        }

        /// <summary>
        /// Plot a line start/end point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="color"></param>
        protected void Plot(float x, float y, float z, FVector3 color)
        {
            lines.Add(new FVector3(x, y, z));
            lines.Add(color);
        }

        /// <summary>
        /// Move the managed data to GPU's memory
        /// </summary>
        /// <param name="buf"></param>
        public void Synchronize(IVertexBuffer buf)
        {
            if(dirty)
            {
                buf.LoadData(lines.ToArray());
                dirty = false;
            }
        }
    }
    /// <summary>
    /// This entity renders the <see cref="Lines"/>'s derived class
    /// </summary>
    class LinesRenderer : EntityRenderer
    {
        IPipeline pipeline;
        ShaderProgram shader;
        private MemoryStream ms;
        private BinaryWriter w;
        IVertexBuffer gridBuffer;
        IVertexBuffer axisBuffer;
        VertexDeclaration vd = new VertexDeclaration();

        public LinesRenderer()
            :base("lines")
        {

        }
        public override void ResetSceneCache()
        {
            base.ResetSceneCache();
        }

        public override void Initialize(Renderer renderer)
        {
            base.Initialize(renderer);
            //Prepare the render state used by the grid/axis
            var rs = new RenderState();
            rs.DepthTest = true;
            rs.DepthMask = true;

            //Define the memory layout of the vertex declaration
            vd.AddField(VertexFieldDataType.FVector3, VertexFieldSemantic.Position);
            vd.AddField(VertexFieldDataType.FVector3, VertexFieldSemantic.VertexColor);
            //compile shader from SPIR-V source code and specify
            var src = new SPIRVSource()
            {
                VertexShader = Shaders.GridVertex,
                FragmentShader = Shaders.GridFragment
            };
            shader = renderer.RenderFactory.CreateShaderProgram(src);
            //bake the graphic render pipeline
            pipeline = renderer.RenderFactory.CreatePipeline(shader, rs, vd, DrawOperation.Lines);

            ms = new MemoryStream(16 * 4);
            w = new BinaryWriter(ms);
        }
        public override void Dispose()
        {
            //remove the unmanaged resources
            shader.Dispose();
            pipeline.Dispose();
            gridBuffer?.Dispose();
            axisBuffer?.Dispose();
            base.Dispose();
        }
        /// <summary>
        /// Get the vertex buffer for grid/axis instance
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        private IVertexBuffer GetVertexBuffer(Renderer renderer, Lines lines)
        {
            if(lines is Grid)
            {
                if (gridBuffer == null)
                    gridBuffer = renderer.RenderFactory.CreateVertexBuffer(vd);
                return gridBuffer;
            }
            if(lines is Axises)
            {
                if (axisBuffer == null)
                    axisBuffer = renderer.RenderFactory.CreateVertexBuffer(vd);
                return axisBuffer;
            }
            return null;
        }

        /// <summary>
        /// Override this to prepare the render instruction to render queue
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="node"></param>
        /// <param name="entity"></param>
        public override void PrepareRenderQueue(Renderer renderer, Node node, Entity entity)
        {
            //Bind the graphics pipeline
            var lines = (Lines)entity;
            var cl = renderer.GetCommandList(RenderQueueGroupId.Geometries);
            cl.BindPipeline(pipeline);

            //Bind buffer
            IVertexBuffer vb = GetVertexBuffer(renderer, lines);
            lines.Synchronize(vb);
            cl.BindVertexBuffer(vb);


            //Prepare the world view projection matrix
            var wvp = renderer.Variables.MatrixWorldViewProjection;
            ms.SetLength(0);
            w.Write(wvp);
            cl.PushConstants(ShaderStage.VertexShader, ms.GetBuffer(), (int)ms.Length);

            //Draw the object
            cl.Draw();
        }
    }
}
