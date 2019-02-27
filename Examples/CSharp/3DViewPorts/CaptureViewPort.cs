using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Render;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp._3DViewPorts
{
    class CaptureViewPort
    {
        public static void Run()
        {
            // ExStart:CaptureViewPort

            // Load an existing 3D scene
            Scene scene = new Scene(RunExamples.GetDataFilePath("scene.obj"));
            // Create an instance of the camera
            Camera camera = new Camera();
            scene.RootNode.CreateChildNode("camera", camera).Transform.Translation = new Vector3(2, 44, 66);
            // Set the target
            camera.LookAt = new Vector3(50, 12, 0);
            // Create a light
            scene.RootNode.CreateChildNode("light", new Light() { Color = new Vector3(Color.White), LightType = LightType.Point }).Transform.Translation = new Vector3(26, 57, 43);

            // The CreateRenderer will create a hardware OpenGL-backend renderer
            // And some internal initializations will be done.
            // When the renderer left using the scope, the unmanaged hardware resources will also be disposed
            using (var renderer = Renderer.CreateRenderer())
            {
                renderer.EnableShadows = false;

                // Create a new render target that renders the scene to texture(s)
                // Use default render parameters
                // And one output targets
                // Size is 1024 x 1024
                // This render target can have multiple render output textures, but here we only need one output.
                // The other textures and depth textures are mainly used by deferred shading in the future.
                // But you can also access the depth texture through IRenderTexture.DepthTeture
                // Use CreateRenderWindow method to render in window, like:
                // Window = renderer.RenderFactory.CreateRenderWindow(new RenderParameters(), Handle);
                using (IRenderTexture rt = renderer.RenderFactory.CreateRenderTexture(new RenderParameters(), 1, 1024, 1024))
                {
                    // This render target has one viewport to render, the viewport occupies the 100% width and 100% height
                    Viewport vp = rt.CreateViewport(camera, new RelativeRectangle() { ScaleWidth = 1, ScaleHeight = 1 });
                    // Render the target and save the target texture to external file
                    renderer.Render(rt);
                    ((ITexture2D)rt.Targets[0]).Save(RunExamples.GetOutputFilePath("file-1viewports_out.png"), ImageFormat.Png);

                    // Now let's change the previous viewport only uses the half left side(50% width and 100% height)
                    vp.Area = new RelativeRectangle() { ScaleWidth = 0.5f, ScaleHeight = 1 };
                    // And create a new viewport that occupies the 50% width and 100% height and starts from 50%
                    // Both of them are using the same camera, so the rendered content should be the same
                    rt.CreateViewport(camera, new RelativeRectangle() { ScaleX = 0.5f, ScaleWidth = 0.5f, ScaleHeight = 1 });
                    // But this time let's increase the field of view of the camera to 90 degree so it can see more part of the scene
                    camera.FieldOfView = 90;
                    renderer.Render(rt);
                    ((ITexture2D)rt.Targets[0]).Save(RunExamples.GetOutputFilePath("file-2viewports_out.png"), ImageFormat.Png);
                }
            }
            // ExEnd:CaptureViewPort           
        }
    }
}
