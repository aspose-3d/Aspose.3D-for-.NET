using Aspose.ThreeD;
using Aspose.ThreeD.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.Geometry_and_Hierarchy
{
    public class ExposeGeometricTransformation
    {
        public static void Run()
        {
            //ExStart: 1
            // Initialize node 
            var n = new Node();
            // Get Geometric Translation
            n.Transform.GeometricTranslation = new Vector3(10, 0, 0);
            // The first Console.WriteLine will output the transform matrix that includes the geometric transformation 
            // while the second one will not.
            Console.WriteLine(n.EvaluateGlobalTransform(true));
            Console.WriteLine(n.EvaluateGlobalTransform(false));
            //ExEnd: 1
        }
    }
}
