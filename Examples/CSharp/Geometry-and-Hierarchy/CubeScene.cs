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

namespace CSharp.Geometry_Hierarchy
{
    class CubeScene
    {
        public static void Run()
        {
            // Initialize scene object
            Scene scene = new Scene();
            
            // Initialize Node class object
            Node cubeNode = new Node("cube");
            
            // Call Common class create mesh method to set mesh instance 
            Mesh mesh = Common.CreateMesh();
            
            // Point node to the Mesh geometry
            cubeNode.Entity = mesh;
            
            // Add Node to a scene
            scene.RootNode.ChildNodes.Add(cubeNode);
            
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir_GeometryAndHierarchy();
            MyDir = MyDir + "CubeScene.fbx";
            
            // Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII);

            Console.WriteLine("\nCube Scene created successfully.\nFile saved at " + MyDir);

        }
    }
}
