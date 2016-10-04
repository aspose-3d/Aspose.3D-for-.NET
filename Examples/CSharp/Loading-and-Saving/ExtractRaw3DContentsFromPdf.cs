using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class ExtractRaw3DContentsFromPdf
    {
        public static void Run()
        {
            //ExStart:ExtractRaw3DContentsFromPdf
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            byte[] password = null;
            // Extract 3D contents
            List<byte[]> contents = FileFormat.PDF.Extract(MyDir + "House_Design.pdf", password);
            int i = 1;
            // Iterate through the contents and in separate 3D files
            foreach (byte[] content in contents)
            {
                string fileName = "3d-" + (i++);
                File.WriteAllBytes(fileName, content);
            }
            //ExEnd:ExtractRaw3DContentsFromPdf            
        }
    }
}
