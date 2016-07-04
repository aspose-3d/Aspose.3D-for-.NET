using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Shading;
using Aspose.ThreeD.Utilities;
using System.Drawing;
using System.Drawing.Imaging;

namespace Aspose._3D.Examples.CSharp.Rendering
{
    class CastAndReceiveShadow
    {
        public static void Run()
        {
            try
            {
                //ExStart:CastAndReceiveShadow
                // The path to the documents directory.
                string MyDir = RunExamples.GetDataDir();

                Scene scene = new Scene();
                Camera camera = new Camera();
                camera.NearPlane = 0.1;
                scene.RootNode.CreateChildNode("camera", camera);
                Light light;
                scene.RootNode.CreateChildNode("light", light = new Light()
                {
                    NearPlane = 0.1,
                    CastShadows = true,
                    Color = new Vector3(Color.White)
                }).Transform.Translation = new Vector3(9.4785, 5, 3.18);
                light.LookAt = Vector3.Origin;
                light.Falloff = 90;

                // Create a plane
                Node plane = scene.RootNode.CreateChildNode("plane", new Plane(20, 20));
                plane.Material = new PhongMaterial() { DiffuseColor = new Vector3(Color.DarkOrange) };
                plane.Transform.Translation = new Vector3(0, 0, 0);

                // Create a torus for casting shadows
                Mesh m = (new Torus("", 1, 0.4, 20, 20, Math.PI * 2)).ToMesh();
                Node torus = scene.RootNode.CreateChildNode("torus", m);
                torus.Material = new PhongMaterial() { DiffuseColor = new Vector3(Color.Cornsilk) };
                torus.Transform.Translation = new Vector3(2, 1, 1);

                { 
                    // Create a blue box don't cast shadows
                    m = (new Box()).ToMesh();
                    m.CastShadows = false;
                    Node box = scene.RootNode.CreateChildNode("box", m);
                    box.Material = new PhongMaterial() { DiffuseColor = new Vector3(Color.Blue) };
                    box.Transform.Translation = new Vector3(2, 1, -1);
                }
                {
                    // Create a red box that don't receive shadow but cast shadows
                    m = (new Box()).ToMesh();
                    m.ReceiveShadows = false;
                    Node box = scene.RootNode.CreateChildNode("box", m);
                    box.Material = new PhongMaterial() { DiffuseColor = new Vector3(Color.Red) };
                    box.Transform.Translation = new Vector3(-2, 1, 1);
                }
                camera.ParentNode.Transform.Translation = new Vector3(10, 10, 10);
                camera.LookAt = Vector3.Origin;
                ImageRenderOptions opt = new ImageRenderOptions() { EnableShadows = true };
                scene.Render(camera, MyDir + "CastAndReceiveShadow_out_.png", new Size(1024, 1024), ImageFormat.Png, opt);
                //ExEnd:CastAndReceiveShadow  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
