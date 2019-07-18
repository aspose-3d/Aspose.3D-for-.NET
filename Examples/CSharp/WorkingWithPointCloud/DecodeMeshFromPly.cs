using Aspose.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.WorkingWithPointCloud
{
    public class DecodeMeshToPly
    {
        public static void Run()
        {
            // ExStart:1
            var geom = FileFormat.PLY.Decode(RunExamples.GetDataDir() + "sphere.ply");
            // ExEnd:1              
        }
    }
}
