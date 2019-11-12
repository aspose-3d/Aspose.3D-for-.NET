using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;

namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class ReadExistingScene
    {
        public static void Run()
        {
            ReadExistingSceneFromDisc();

            //ReadRVMWithAttributes();
        }

        public static void ReadExistingSceneFromDisc()
        {
            // ExStart:ReadExistingScene
            // The path to the documents directory.

            // Initialize a Scene class object
            Scene scene = new Scene();
            // Load an existing 3D document
            scene.Open(RunExamples.GetDataFilePath("document.fbx"));

            // ExEnd:ReadExistingScene
            Console.WriteLine("\n3D Scene is ready for the modification, addition or processing purposes.");
        }
        public static void ReadRVMWithAttributes()
        {
            //ExStart:ReadRVMWithAttributes
            string dataDir = RunExamples.GetDataDir();

            Scene scene = new Scene(RunExamples.GetDataFilePath("att-test.rvm"));

            FileFormat.RvmBinary.LoadAttributes(scene, RunExamples.GetDataFilePath("att-test.att"));
            //ExEnd: ReadRVMWithAttributes
        }
    }
}
