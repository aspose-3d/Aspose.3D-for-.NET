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
    class Background : Entity
    {
        private static EntityRendererKey Key = new EntityRendererKey("background");

        /// <summary>
        /// Return this key so the renderer can ues the correct renderer to render the background object
        /// </summary>
        /// <returns></returns>
        public override EntityRendererKey GetEntityRendererKey()
        {
            return Key;
        }

    }
    class BackgroundRenderer : EntityRenderer
    {

        private ShaderProgram shader;
        private IPipeline pipeline;
        private byte[] constants;
        public BackgroundRenderer()
            :base("background")
        {

        }
        public override void Initialize(Renderer renderer)
        {
            //compile the shader
            var src = new SPIRVSource()
            {
                VertexShader = Shaders.BackgroundVertex,
                FragmentShader = Shaders.BackgroundFragment,
            };
            shader = renderer.RenderFactory.CreateShaderProgram(src);

            //prepare the render state
            var rs = new RenderState();
            rs.CullFace = false;
            rs.DepthMask = false;

            //Bake the render pipeline
            //This entity rendering does not need a vertex buffer
            //so the vertex declaration is an empty definition here
            var vd = new VertexDeclaration();
            pipeline = renderer.RenderFactory.CreatePipeline(shader, rs, vd, DrawOperation.Triangles);
            
            //The data used by the shader's push constant
            float[] data =
            {
                1000, 0, 0, 0,//height
                0.22f, 0.2f, 0.13f, 1.0f,//upper color
                0.2f, 0.3f, 0.3f, 1.0f//lower color
            };
            constants = new byte[data.Length * 4];
            Buffer.BlockCopy(data, 0, constants, 0, constants.Length);
        }
        public override void Dispose()
        {
            shader.Dispose();
            pipeline.Dispose();
        }
        public override void PrepareRenderQueue(Renderer renderer, Node node, Entity entity)
        {
            //Render the background in Background queue
            var commandList = renderer.GetCommandList(RenderQueueGroupId.Background);
            //Bind the render pipeline
            commandList.BindPipeline(pipeline);
            //Push the height/upper color/lower color to the fragment shader
            commandList.PushConstants(ShaderStage.FragmentShader, constants);
            //draw a triangle(without vertex buffer)
            commandList.Draw(0, 3);
        }

    }
}
