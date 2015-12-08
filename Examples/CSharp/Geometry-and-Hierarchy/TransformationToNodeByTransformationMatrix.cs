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
    class TransformationToNodeByTransformationMatrix
    {
        public static void Run()
        {
            //ExStart:AddTransformationToNodeByTransformationMatrix            
            // Initialize scene object
            Scene scene = new Scene();

            // Initialize Node class object
            Node cubeNode = new Node("cube");

            // Call Common class create mesh using polygon builder method to set mesh instance 
            Mesh mesh = Common.CreateMeshUsingPolygonBuilder(); 
           
            // Point node to the Mesh geometry
            cubeNode.Entity = mesh;
            // set custom translation matrix
            cubeNode.Transform.TransformMatrix = new Matrix4(
            1, -0.3, 0, 0,
            0.4, 1, 0.3, 0,
            0, 0, 1, 0,
            0, 20, 0, 1
            );        
            // Add cube to the scene
            scene.RootNode.ChildNodes.Add(cubeNode);            

            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            MyDir = MyDir + RunExamples.GetOutputFilePath("TransformationToNode.fbx");
   
            // Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII);
            //ExEnd:AddTransformationToNodeByTransformationMatrix
            Console.WriteLine("\nTransformation added successfully to node.\nFile saved at " + MyDir);

        }
    }
}
