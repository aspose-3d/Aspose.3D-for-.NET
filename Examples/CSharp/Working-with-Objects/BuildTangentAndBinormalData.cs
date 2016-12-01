using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp._Working_with_Objects
{
    class BuildTangentAndBinormalData
    {
        public static void Run()
        {
            // ExStart:BuildTangentAndBinormalData
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();

            // Load an existing 3D file
            Scene scene = new Scene( MyDir + "document.fbx");
            // Triangulate a scene
            PolygonModifier.BuildTangentBinormal(scene);
            // Save 3D scene
            scene.Save("BuildTangentAndBinormalData_out.fbx", FileFormat.FBX7400ASCII);
            // ExEnd:BuildTangentAndBinormalData              
        }
    }
}
