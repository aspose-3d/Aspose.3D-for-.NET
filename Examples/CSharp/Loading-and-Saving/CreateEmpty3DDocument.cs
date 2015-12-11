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
    class CreateEmpty3DDocument
    {
        public static void Run()
        {
            //ExStart:CreateEmpty3DDocument
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            MyDir = MyDir + "document.fbx";

            // Create an object of the Scene class
            Scene scene = new Scene();
            // Save 3D scene document
            scene.Save(MyDir, FileFormat.FBX7400ASCII);
            //ExEnd:CreateEmpty3DDocument

            Console.WriteLine("\nAn empty 3D document created successfully.\nFile saved at " + MyDir);
        }
    }
}
