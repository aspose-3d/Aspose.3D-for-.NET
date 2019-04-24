using Aspose.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.WorkingWithVRML
{
    public class OpenVRML
    {
        public static void Run()
        {
            // ExStart:OpenVRML
            // Create a Scene
            Scene scene = new Scene();
            // Open Virtual Reality Modeling Language (VRML) file format
            scene.Open(RunExamples.GetDataDir() + "test.wrl");
            // Work with VRML file format...

            // ExEnd:OpenVRML              
        }
    }
}
