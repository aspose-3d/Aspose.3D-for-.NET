using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.WorkingWithPointCloud
{
    public class DecodeMesh
    {
        public static void Run()
        {
            // ExStart:1
            var pointCloud = (PointCloud)FileFormat.Draco.Decode(RunExamples.GetDataDir() + "point_cloud_no_qp.drc");
            // ExEnd:1              
        }
    }
}
