using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.WorkingWithPointCloud
{
    public class EncodeMesh
    {
        public static void Run()
        {
            // ExStart:1
            FileFormat.Draco.Encode(new Sphere(), RunExamples.GetDataDir() + "sphere.drc");
            // ExEnd:1              
        }
    }
}
