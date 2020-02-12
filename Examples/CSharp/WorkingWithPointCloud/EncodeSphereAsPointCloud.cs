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
    public class EncodeSphereAsPointCloud
    {
        public static void Run()
        {
            // ExStart:1
            FileFormat.Draco.Encode(new Sphere(), RunExamples.GetDataDir() + "sphere.drc", new DracoSaveOptions() { PointCloud = true });
            // ExEnd:1              
        }
    }
}
