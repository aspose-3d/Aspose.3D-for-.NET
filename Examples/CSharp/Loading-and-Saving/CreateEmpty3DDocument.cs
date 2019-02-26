using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class CreateEmpty3DDocument
    {
        public static void Run()
        {
            // ExStart:CreateEmpty3DDocument
            // The path to the documents directory.
            var output = RunExamples.GetOutputFilePath("document.fbx");

            // Create an object of the Scene class
            Scene scene = new Scene();
            // Save 3D scene document
            scene.Save(output, FileFormat.FBX7500ASCII);
            // ExEnd:CreateEmpty3DDocument

            Console.WriteLine("\nAn empty 3D document created successfully.\nFile saved at " + output);
        }
    }
}
