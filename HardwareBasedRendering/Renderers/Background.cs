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
        private PushConstant pushConstant = new PushConstant();
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
            
        }
        public override void Dispose()
        {
            shader.Dispose();
            pipeline.Dispose();
        }
        public override void PrepareRenderQueue(Renderer renderer, IRenderQueue queue, Node node, Entity entity)
        {
            //Render the background in Background queue
            queue.Add(RenderQueueGroupId.Background, pipeline, null, 0);
        }
        public override void RenderEntity(Renderer renderer, ICommandList cl, Node node, object renderableResource, int subEntity)
        {
            //Bind the render pipeline
            cl.BindPipeline(pipeline);
            //Push the height/upper color/lower color to the fragment shader
            pushConstant
                .Write(1000.0f)
                .Write(0.22f, 0.2f, 0.13f, 1.0f)
                .Write(0.2f, 0.3f, 0.3f, 1.0f)
                .Commit(ShaderStage.FragmentShader, cl);


            //draw a triangle(without vertex buffer)
            cl.Draw(0, 3);
        }

    }
}
