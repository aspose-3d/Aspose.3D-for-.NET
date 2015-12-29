//////////////////////////////////////////////////////////////////////////
// Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.3D. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////
using System;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utils;
using Aspose.ThreeD.Shading;
using System.Drawing;

namespace CSharp.Geometry_Hierarchy
{
    class TriangulateMesh
    {
        public static void Run()
        {
            //ExStart:TriangulateMesh 
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
           
            // Initialize scene object
            Scene scene = new Scene();
            scene.Open(MyDir + "document.fbx");
            
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
            MyDir = MyDir + RunExamples.GetOutputFilePath("document.fbx");
            scene.Save(MyDir, FileFormat.FBX7400ASCII);
            //ExEnd:TriangulateMesh   
            Console.WriteLine("\nMesh has been Triangulated.\nFile saved at " + MyDir);
        }
    }
}
