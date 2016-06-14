using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp._Working_with_Objects
{
    class ConvertTorusPrimitivetoMesh
    {
        public static void Run()
        {
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            MyDir = MyDir + "test.fbx";

            // load a 3D file
            Scene scene = new Scene(MyDir);
            // Initialize Node class object
            Node cubeNode = new Node("torus");

            //ExStart:ConvertTorusPrimitivetoMesh
            // initialize object by Torus class
            IMeshConvertible convertible = new Torus();
            
            // convert a Torus to Mesh
            Mesh mesh = convertible.ToMesh();
            //ExEnd:ConvertTorusPrimitivetoMesh

            // Point node to the Mesh geometry
            cubeNode.Entity = mesh;

            // Add Node to a scene
            scene.RootNode.ChildNodes.Add(cubeNode);

            // The path to the documents directory.
            MyDir = RunExamples.GetDataDir() + RunExamples.GetOutputFilePath("TorusToMeshScene.fbx");

            // Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII);

            Console.WriteLine("\n Converted the primitive Torus to a mesh successfully.\nFile saved at " + MyDir);
        }
    }
}
