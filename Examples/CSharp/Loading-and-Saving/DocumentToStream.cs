//////////////////////////////////////////////////////////////////////////
// Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.3D. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////
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
            //ExStart:DocumentToStream
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            MyDir = MyDir + "document.fbx";
            
            // ExStart:LoadAndSave
            // Load a 3D document into Aspose.3D
            Scene scene = new Scene(MyDir);
            
            MemoryStream dstStream = new MemoryStream();            
            scene.Save(dstStream, FileFormat.FBX7400ASCII);
            // ExEnd:LoadAndSave

            // Rewind the stream position back to zero so it is ready for next reader.
            dstStream.Position = 0;
            //ExEnd:DocumentToStream

            Console.WriteLine("\nConverted 3D document to stream successfully.");
        }
    }
}
