using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.WorkingWithCylinder
{
    public class CustomizedOffsetTopCylinder
    {
        public static void Run()
        {
            // ExStart:1
            // Create a scene
            Scene scene = new Scene();
            // Initialize cylinder
            var cylinder1 = new Cylinder(2, 2, 10, 20, 1, false);
            // Set OffsetTop
            cylinder1.OffsetTop = new Vector3(5, 3, 0);
            // Create ChildNode
            scene.RootNode.CreateChildNode(cylinder1).Transform.Translation = new Vector3(10, 0, 0);
            // Intialze second cylinder without customized OffsetTop
            var cylinder2 = new Cylinder(2, 2, 10, 20, 1, false);
            // Create ChildNode
            scene.RootNode.CreateChildNode(cylinder2);
            // Save
            scene.Save(RunExamples.GetDataDir()+ "CustomizedOffsetTopCylinder.obj", FileFormat.WavefrontOBJ);
            // ExEnd:1              
        }
    }
}
