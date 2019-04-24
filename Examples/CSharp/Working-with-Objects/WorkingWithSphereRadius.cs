using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.Working_with_Objects
{
    public class WorkingWithSphereRadius
    {
        public static void Run()
        {
            // ExStart:WorkingWithSphereRadius
            // Create a Scene
            Scene scene = new Scene();
            // Set Sphere Radius (Using Radius property you can get or set radius of Sphere)
            scene.RootNode.CreateChildNode(new Sphere() { Radius = 10 });
            // Save scene
            scene.Save(RunExamples.GetDataDir() +"sphere.obj", FileFormat.WavefrontOBJ);
            // ExEnd:WorkingWithSphereRadius              
        }
    }
}
