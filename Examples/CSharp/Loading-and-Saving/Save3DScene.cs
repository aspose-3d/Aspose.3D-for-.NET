using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class Save3DScene
    {
        public static void Run()
        {
            // ExStart:Save3DScene
                        
            // Load a 3D document into Aspose.3D
            Scene scene = new Scene();

            // Open an existing 3D scene
            scene.Open(RunExamples.GetDataFilePath("document.fbx"));

            // Save Scene to a stream
            MemoryStream dstStream = new MemoryStream();
            scene.Save(dstStream, FileFormat.FBX7500ASCII);
            
            // Rewind the stream position back to zero so it is ready for next reader.
            dstStream.Position = 0;

            // Save Scene to a local path
            scene.Save(RunExamples.GetOutputFilePath("output_out.fbx"), FileFormat.FBX7500ASCII);
            // ExEnd:Save3DScene

            Console.WriteLine("\nConverted 3D document to stream successfully.");
        }

        public static void Compression()
        {
            // ExStart:Compression

            // Load a 3D document into Aspose.3D
            Scene scene = new Scene(RunExamples.GetDataFilePath("document.fbx"));

            scene.Save(RunExamples.GetOutputFilePath("UncompressedDocument.fbx"), new FbxSaveOptions(FileFormat.FBX7500ASCII) { EnableCompression = false });
            // ExEnd:Compression
        }
    }
}
