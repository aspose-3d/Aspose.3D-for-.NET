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
    public class CustomizedShearBottomCylinder
    {
        public static void Run()
        {
            // ExStart:1
            // Create a scene
            Scene scene = new Scene();
            // Create cylinder 1
            var cylinder1 = new Cylinder(2, 2, 10, 20, 1, false);
            // Customized shear bottom for cylinder 1
            cylinder1.ShearBottom = new Vector2(0, 0.83);// shear 47.5deg in xy plane(z-axis)
            // Add cylinder 1 to the scene
            scene.RootNode.CreateChildNode(cylinder1).Transform.Translation = new Vector3(10, 0, 0);
            // Create cylinder 2
            var cylinder2 = new Cylinder(2, 2, 10, 20, 1, false);
            // Add cylinder to without a ShearBottom to the scene
            scene.RootNode.CreateChildNode(cylinder2);
            // Save scene
            scene.Save(RunExamples.GetDataDir() + "CustomizedShearBottomCylinder.obj", FileFormat.WavefrontOBJ);

            // ExEnd:1              
        }
    }
}
