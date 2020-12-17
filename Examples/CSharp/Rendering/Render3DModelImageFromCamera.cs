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
                Scene scene = new Scene();
                var path = RunExamples.GetDataFilePath("Aspose3D.obj");
                scene.Open(path);
                // Create a camera at (10,10,10) and look at the origin point for rendering,
                // It must be attached to the scene before render
                Camera cam = new Camera(ProjectionType.Perspective);
                cam.NearPlane = 1;
                cam.FarPlane = 500;
                scene.RootNode.CreateChildNode(cam).Transform.Translation = new Vector3(170, 16, 130);
                cam.LookAt = new Vector3(28, 0, -30);


                //create three lights to illuminate the scene
                scene.RootNode.CreateChildNode(new Light() 
                {
                    LightType = LightType.Point ,
                    ConstantAttenuation = 0.3,
                    Color = new Vector3(Color.White)
                }).Transform.Translation = new Vector3(30, 10, 10);
                scene.RootNode.CreateChildNode(new Light()
                {
                    LightType = LightType.Directional,
                    ConstantAttenuation = 0.3,
                    Direction = new Vector3(-0.3, -0.4, 0.3),
                    Color = new Vector3(Color.White)
                });
                scene.RootNode.CreateChildNode(new Light() 
                {
                    LightType = LightType.Spot ,
                    CastShadows = true,
                    LookAt = new Vector3(28, 10, -30),
                    Color = new Vector3(Color.White)
                }).Transform.Translation = new Vector3(40, 10, 50);

                // Specify the image render option
                ImageRenderOptions opt = new ImageRenderOptions();
                // Set the background color
                opt.BackgroundColor = Color.AliceBlue;
                // Tells renderer where the it can find textures
                opt.AssetDirectories.Add(RunExamples.GetDataDir() + "textures");
                // Turn on shadow
                opt.EnableShadows = true;
                // Render the scene in given camera's perspective into specified png file with size 1024x1024
                scene.Render(cam, RunExamples.GetOutputFilePath("Render3DModelImageFromCamera.png"), new Size(1024, 1024), ImageFormat.Png, opt);
                // ExEnd:Render3DModelImageFromCamera  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            
        }
    }
}
