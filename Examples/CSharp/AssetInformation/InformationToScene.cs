using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;

namespace Aspose._3D.Examples.CSharp.AssetInformation
{
    class InformationToScene
    {
        public static void Run()
        {
            //ExStart:AddAssetInformationToScene           
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
            string MyDir = RunExamples.GetDataDir();
            MyDir = MyDir + RunExamples.GetOutputFilePath("InformationToScene.fbx");
                        
            // Save scene to 3D supported file formats
            scene.Save(MyDir, FileFormat.FBX7500ASCII);
            //ExEnd:AddAssetInformationToScene

            Console.WriteLine("\nAsset information added successfully to Scene.\nFile saved at " + MyDir);
        }
    }
}
