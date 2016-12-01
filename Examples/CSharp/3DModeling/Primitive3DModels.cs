using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp._3DModeling
{
    class Primitive3DModels
    {
        public static void Run()
        {
            // ExStart:Primitive3DModels
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();

            // Initialize a Scene object
            Scene scene = new Scene();
            // Create a Box model
            scene.RootNode.CreateChildNode("box", new Box());
            // Create a Cylinder model
            scene.RootNode.CreateChildNode("cylinder", new Cylinder());
            // Save drawing in the FBX format
            MyDir = MyDir + RunExamples.GetOutputFilePath("test.fbx");
            scene.Save(MyDir, FileFormat.FBX7500ASCII);

            // ExEnd:Primitive3DModels
            Console.WriteLine("\nBuilding a scene from primitive 3D models successfully.\nFile saved at " + MyDir);
        }
    }
}
