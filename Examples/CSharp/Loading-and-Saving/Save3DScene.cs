using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;

namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class Save3DScene
    {
        public static void Run()
        {
            //ExStart:Save3DScene
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
                        
            // Load a 3D document into Aspose.3D
            Scene scene = new Scene();

            // open an existing 3D scene
            scene.Open(MyDir + "document.fbx");

            // save Scene to a stream
            MemoryStream dstStream = new MemoryStream();
            scene.Save(dstStream, FileFormat.FBX7500ASCII);
            
            // Rewind the stream position back to zero so it is ready for next reader.
            dstStream.Position = 0;

            // save Scene to a local path
            scene.Save(MyDir + "output.fbx", FileFormat.FBX7500ASCII);
            //ExEnd:Save3DScene

            Console.WriteLine("\nConverted 3D document to stream successfully.");
        }
    }
}
