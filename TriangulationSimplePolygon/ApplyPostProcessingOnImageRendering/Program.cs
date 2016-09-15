using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Render;
using Aspose.ThreeD.Utilities;

namespace ApplyPostProcessingOnImageRendering
{
    class Program
    {
        static void Main(string[] args)
        {
            Scene scene = new Scene("scene.obj");
            // Create a camera
            var camera = new Camera();
            scene.RootNode.CreateChildNode("camera", camera).Transform.Translation = new Vector3(2, 44, 66);
            camera.LookAt = new Vector3(50, 12, 0);
            // Create a light
            scene.RootNode.CreateChildNode("light", new Light() {Color = new Vector3(Color.White), LightType =  LightType.Point}).Transform.Translation = new Vector3(26, 57, 43);

            // The CreateRenderer will create a hardware OpenGL-backend renderer, more renderer can be added in the future on user's demand
            // And some internal initializations will be done.
            // When the renderer left the using scope, the unmanaged hardware resources will also be disposed
            using (var renderer = Renderer.CreateRenderer())
            {
                renderer.EnableShadows = false;

                //create a new render target that renders the scene to texture(s)
                //use default render parameters
                //and one output targets
                //size is 1024 x 1024
                //this render target can have multiple render output textures, but here we only need one output.
                //The other textures and depth textures are mainly used by deferred shading in the future.
                //But you can also access the depth texture through IRenderTexture.DepthTeture
                using (
                    IRenderTexture rt = renderer.RenderFactory.CreateRenderTexture(new RenderParameters(), 1, 1024, 1024)
                    )
                {
                    //This render target has one viewport to render, the viewport occupies the 100% width and 100% height
                    Viewport vp = rt.CreateViewport(camera, new RelativeRectangle() {ScaleWidth = 1, ScaleHeight = 1});
                    //render the target and save the target texture to external file
                    renderer.Render(rt);
                    rt.Targets[0].Save("file-1viewports.png", ImageFormat.Png);


                    //now lets change the previous viewport only uses the half left side(50% width and 100% height) 
                    vp.Area = new RelativeRectangle() {ScaleWidth = 0.5f, ScaleHeight = 1};
                    //and create a new viewport that occupies the 50% width and 100% height and starts from 50%
                    //both of them are using the same camera, so the rendered content should be the same
                    rt.CreateViewport(camera, new RelativeRectangle() {ScaleX = 0.5f, ScaleWidth = 0.5f, ScaleHeight = 1});
                    //but this time let's increase the field of view of the camera to 90degree so it can see more part of the scene
                    camera.FieldOfView = 90;
                    renderer.Render(rt);
                    rt.Targets[0].Save("file-2viewports.png", ImageFormat.Png);

                    //add a post-processing effect(or filter)

                    PostProcessing pixelation = renderer.GetPostProcessing("pixelation");
                    renderer.PostProcessings.Add(pixelation);
                    renderer.Render(rt);
                    rt.Targets[0].Save("file-pixelation.png", ImageFormat.Png);

                    //clear previous post-processing effects and try another one
                    PostProcessing grayscale = renderer.GetPostProcessing("grayscale");
                    renderer.PostProcessings.Clear();
                    renderer.PostProcessings.Add(grayscale);
                    renderer.Render(rt);
                    rt.Targets[0].Save("file-grayscale.png", ImageFormat.Png);

                    //we can also combine post-processing effects
                    renderer.PostProcessings.Clear();
                    renderer.PostProcessings.Add(grayscale);
                    renderer.PostProcessings.Add(pixelation);
                    renderer.Render(rt);
                    rt.Targets[0].Save("file-grayscale+pixelation.png", ImageFormat.Png);
                }
            }
        }
    }
}
