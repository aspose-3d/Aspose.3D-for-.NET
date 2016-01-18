using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;

namespace CSharp.Loading_Saving
{
    class ReadExistingScene
    {
        public static void Run()
        {
            //ExStart:ReadExistingScene
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            MyDir = MyDir + "document.fbx";

            // Call the scene constructor to load an existing one
            Scene scene = new Scene(MyDir);

            // Initialize a scene object to build a scene from scratch
            Scene parentScene = new Scene();

            // Initialize a scene object and also define its parent scene. New scene can also be used as parent scene
            Scene childscene = new Scene(parentScene, MyDir);

            //ExEnd:ReadExistingScene
            Console.WriteLine("\n3D Scene is ready for the modification, addition or processing purposes.");
        }
    }
}
