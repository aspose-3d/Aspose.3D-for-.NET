using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.WorkingWithPointCloud
{
    public class ExportToPlyAsPointCloud
    {
        public static void Run()
        {
            // ExStart:1
            FileFormat.PLY.Encode(new Sphere(), RunExamples.GetDataDir() + "sphere.ply", new PlySaveOptions() { PointCloud = true });

            // ExEnd:1              
        }
    }
}
