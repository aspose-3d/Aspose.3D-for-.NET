using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.WorkingWithPointCloud
{
    class EncodeMeshToPly
    {
        public static void Run()
        {
            // ExStart:1
            FileFormat.PLY.Encode(new Sphere(), "sphere.ply");
            // ExEnd:1              
        }
    }
}
