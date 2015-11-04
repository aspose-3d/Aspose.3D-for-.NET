//////////////////////////////////////////////////////////////////////////
// Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.3D. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utils;

namespace CSharp.Geometry_Hierarchy
{
    class NodeHierarchy
    {
        public static void Run()
        {
            // Initialize scene object
            Scene scene = new Scene();

            // Get a child node object
            Node top = scene.RootNode.CreateChildNode();

            // Each cube node has their own translation
            Node cube1 = top.CreateChildNode("cube1");

            // Call Common class create mesh method to set mesh instance 
            Mesh mesh = Common.CreateMesh();

            // Point node to the mesh
            cube1.Entity = mesh;
            // Set first cube translation
            cube1.Transform.Translation = new Vector3(-10, 0, 0);

            Node cube2 = top.CreateChildNode("cube2");
            // Point node to the mesh
            cube2.Entity = mesh;
            // Set second cube translation
            cube2.Transform.Translation = new Vector3(10, 0, 0);

            // The rotated top node will affect all child nodes
            top.Transform.Rotation = Quaternion.FromEulerAngle(Math.PI, 4, 0);

            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir_GeometryAndHierarchy();
            MyDir = MyDir + "NodeHierarchy.fbx";
            
            // Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII);

            Console.WriteLine("\nNode hierarchy added successfully to document.\nFile saved at " + MyDir);

        }
    }
}
