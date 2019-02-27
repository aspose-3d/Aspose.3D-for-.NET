using System;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;
using Aspose.ThreeD.Shading;
using System.Drawing;

namespace Aspose._3D.Examples.CSharp.Geometry_Hierarchy
{
    class TriangulateMesh
    {
        public static void Run()
        {
            // ExStart:TriangulateMesh 
            // The path to the documents directory.
           
            // Initialize scene object
            Scene scene = new Scene();
            scene.Open(RunExamples.GetDataFilePath("document.fbx"));
            
            scene.RootNode.Accept(delegate(Node node)
            {
                Mesh mesh = node.GetEntity<Mesh>();
                if (mesh != null)
                {
                    // Triangulate the mesh
                    Mesh newMesh = PolygonModifier.Triangulate(mesh);
                    // Replace the old mesh
                    node.Entity = mesh;
                }
                return true;
            });
            var output  = RunExamples.GetOutputFilePath("document.fbx");
            scene.Save(output, FileFormat.FBX7400ASCII);
            // ExEnd:TriangulateMesh   
            Console.WriteLine("\nMesh has been Triangulated.\nFile saved at " + output);
        }
    }
}
