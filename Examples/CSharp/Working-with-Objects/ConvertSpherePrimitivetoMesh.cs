using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp._Working_with_Objects
{
    class ConvertSpherePrimitivetoMesh
    {
        public static void Run()
        {
            // Initialize scene object
            Scene scene = new Scene();

            // Initialize Node class object
            Node cubeNode = new Node("sphere");

            // ExStart:ConvertSpherePrimitivetoMesh
            // Initialize object by Sphere class
            IMeshConvertible convertible = new Sphere();
            
            // Convert a Sphere to Mesh
            Mesh mesh = convertible.ToMesh();
            // ExEnd:ConvertSpherePrimitivetoMesh

            // Point node to the Mesh geometry
            cubeNode.Entity = mesh;

            // Add Node to a scene
            scene.RootNode.ChildNodes.Add(cubeNode);

            // The path to the documents directory.
            string output = RunExamples.GetOutputFilePath("SphereToMeshScene.fbx");

            // Save 3D scene in the supported file formats
            scene.Save(output, FileFormat.FBX7400ASCII);

            Console.WriteLine("\n Converted the primitive Sphere to a mesh successfully.\nFile saved at " + output);
        }
    }
}
