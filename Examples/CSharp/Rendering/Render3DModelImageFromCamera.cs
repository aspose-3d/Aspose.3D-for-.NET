using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;
using Aspose.ThreeD.Utilities;
using System.Drawing;
using System.Drawing.Imaging;

namespace Aspose._3D.Examples.CSharp.Rendering
{
    class Render3DModelImageFromCamera
    {
        public static void Run()
        {
            try
            {
                // ExStart:Render3DModelImageFromCamera

                // Load scene from file
                Scene scene = new Scene(RunExamples.GetDataFilePath("camera.3ds"));
                // Create a camera at (10,10,10) and look at the origin point for rendering,
                // It must be attached to the scene before render
                Camera camera = new Camera();
                scene.RootNode.CreateChildNode("camera", camera);
                camera.ParentNode.Transform.Translation = new Vector3(10, 10, 10);
                camera.LookAt = Vector3.Origin;

                // Specify the image render option
                ImageRenderOptions opt = new ImageRenderOptions();
                // Set the background color
                opt.BackgroundColor = Color.AliceBlue;
                // Tells renderer where the it can find textures
                opt.AssetDirectories.Add(RunExamples.GetDataDir() + "textures");
                // Turn on shadow
                opt.EnableShadows = true;
                // Render the scene in given camera's perspective into specified png file with size 1024x1024
                scene.Render(camera, RunExamples.GetOutputFilePath("Render3DModelImageFromCamera.png"), new Size(1024, 1024), ImageFormat.Png, opt);
                // ExEnd:Render3DModelImageFromCamera  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            
        }
    }
}
