using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;

namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class DetectFormat
    {
        public static void Run()
        {
            // ExStart:DetectFormat
            // Detect the format of a 3D file
            FileFormat inputFormat = FileFormat.Detect(RunExamples.GetDataFilePath("document.fbx"));
            // Display the file format
            Console.WriteLine("File Format: " + inputFormat.ToString());
            // ExEnd:DetectFormat            
        }
    }
}
