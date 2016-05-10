using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;

namespace CSharp._Working_with_Objects
{
    class SplitAllMeshesofScenebyMaterial
    {
        public static void Run()
        {
            //ExStart:SplitAllMeshesofScenebyMaterial
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            MyDir = MyDir + "test.fbx";

            // load a 3D file
            Scene scene = new Scene(MyDir);
            // split all meshes
            PolygonModifier.SplitMesh(scene, SplitMeshPolicy.CloneData);

            // save file
            MyDir = RunExamples.GetDataDir() + RunExamples.GetOutputFilePath("test-splitted.fbx");
            scene.Save(MyDir, FileFormat.FBX7500ASCII);

            //ExEnd:SplitAllMeshesofScenebyMaterial
            Console.WriteLine("\nSpliting all meshes of a scene per material successfully.\nFile saved at " + MyDir);
        }
    }
}
