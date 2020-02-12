using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp._3DScene
{
    public class ChangePlaneOrientation
    {
        public static void Run()
        {
            // ExStart:ChangePlaneOrientation
            // The path to the data directory
            string dataDir = RunExamples.GetDataDir();
            // Initialize scene object
            Scene scene = new Scene();
            // Set Vector
            scene.RootNode.CreateChildNode(new Plane() { Up = new Vector3(1, 1, 3) });
            //This will generate a plane that has customized orientation
            scene.Save(dataDir + "ChangePlaneOrientation.obj", FileFormat.WavefrontOBJ);
            // ExEnd:ChangePlaneOrientation
        }
    }
}
