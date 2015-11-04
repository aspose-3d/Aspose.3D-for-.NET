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

namespace CSharp.AssetInformation
{
    class InformationToScene
    {
        public static void Run()
        {
            // Initialize a 3D scene
            Scene scene = new Scene();
            
            // Set application/tool name
            scene.AssetInfo.ApplicationName = "Egypt";
            
            // Set application/tool vendor name
            scene.AssetInfo.ApplicationVendor = "Manualdesk";
            
            // We use ancient egyption measurement unit Pole
            scene.AssetInfo.UnitName = "pole";
            
            // One Pole equals to 60cm
            scene.AssetInfo.UnitScaleFactor = 0.6;            
            
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir_AssetInformation();
            MyDir = MyDir + "InformationToScene.fbx";

            // Save scene to 3D supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII);

            Console.WriteLine("\nAsset information added successfully to Scene.\nFile saved at " + MyDir);
        }
    }
}
