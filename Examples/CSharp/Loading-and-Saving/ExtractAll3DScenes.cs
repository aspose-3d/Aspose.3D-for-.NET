using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class ExtractAll3DScenes
    {
        public static void Run()
        {
            // ExStart:ExtractAll3DScenes

            byte[] password = null;
            List<Scene> scenes = FileFormat.PDF.ExtractScene(RunExamples.GetDataFilePath("House_Design.pdf"), password);
            int i = 1;
            // Iterate through the scenes and save in 3D files
            foreach (Scene scene in scenes)
            {
                string fileName = "3d-" + (i++) + ".fbx";
                scene.Save(RunExamples.GetOutputFilePath(fileName), FileFormat.FBX7400ASCII);
            }
            // ExEnd:ExtractAll3DScenes            
        }
    }
}
