using System;
using System.IO;
using System.Text;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class OpenSceneFromProtectedPdf
    {
        public static void Run()
        {
            // ExStart:OpenSceneFromProtectedPdf
            // Create a new scene
            Scene scene = new Scene();
            // Use loading options and apply password
            PdfLoadOptions opt = new PdfLoadOptions() { Password = Encoding.UTF8.GetBytes("password") };
            // Open scene
            scene.Open(RunExamples.GetDataFilePath("House_Design.pdf"), opt);
            // ExEnd:OpenSceneFromProtectedPdf            
        }
    }
}
