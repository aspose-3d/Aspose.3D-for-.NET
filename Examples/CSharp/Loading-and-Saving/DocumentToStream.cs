using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;

namespace CSharp.Loading_Saving
{
    class DocumentToStream
    {
        public static void Run()
        {
            //ExStart:SaveDocumentToStream
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            MyDir = MyDir + "document.fbx";
                        
            // Load a 3D document into Aspose.3D
            Scene scene = new Scene(MyDir);
            
            MemoryStream dstStream = new MemoryStream();            
            scene.Save(dstStream, FileFormat.FBX7400ASCII);
            
            // Rewind the stream position back to zero so it is ready for next reader.
            dstStream.Position = 0;
            //ExEnd:SaveDocumentToStream

            Console.WriteLine("\nConverted 3D document to stream successfully.");
        }
    }
}
