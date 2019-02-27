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
            // Load a 3D file
            Scene scene = new Scene(RunExamples.GetDataFilePath("test.fbx"));
            // Initialize Node class object
            Node cubeNode = new Node("torus");

            // ExStart:ConvertTorusPrimitivetoMesh
            // Initialize object by Torus class
            IMeshConvertible convertible = new Torus();
            
            // Convert a Torus to Mesh
            Mesh mesh = convertible.ToMesh();
            // ExEnd:ConvertTorusPrimitivetoMesh

            // Point node to the Mesh geometry
            cubeNode.Entity = mesh;

            // Add Node to a scene
            scene.RootNode.ChildNodes.Add(cubeNode);

            // The path to the documents directory.
            var output = RunExamples.GetOutputFilePath("TorusToMeshScene.fbx");

            // Save 3D scene in the supported file formats
            scene.Save(output, FileFormat.FBX7400ASCII);

            Console.WriteLine("\n Converted the primitive Torus to a mesh successfully.\nFile saved at " + output);
        }
    }
}
