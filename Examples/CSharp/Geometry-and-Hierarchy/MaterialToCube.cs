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
    class MaterialToCube
    {
        public static void Run()
        {
            // Initialize scene object
            Scene scene = new Scene();
            
            // Initialize cube node object
            Node cubeNode = new Node("cube");
            
            // Call Common class create mesh method to set mesh instance 
            Mesh mesh = Common.CreateMesh();
            
            // Point node to the mesh
            cubeNode.Entity = mesh;
            
            // Add cube to the scene
            scene.RootNode.ChildNodes.Add(cubeNode);

            // Initiallize PhongMaterial object
            PhongMaterial mat = new PhongMaterial();
            
            // Initiallize Texture object
            Texture diffuse = new Texture();
            
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir_GeometryAndHierarchy();
            
            // Set local file path
            diffuse.FileName = MyDir + "surface.dds";

            // Set Texture of the material
            mat.SetTexture("DiffuseColor", diffuse);
            
            // Set color
            mat.SpecularColor = new Vector3(Color.Red);

            // Set brightness
            mat.Shininess = 100;

            // Set material property of the cube object
            cubeNode.Material = mat;
            
            MyDir = MyDir + "MaterialToCube.fbx";
            
            // Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII);

            Console.WriteLine("\nMaterial added successfully to cube.\nFile saved at " + MyDir);

        }
    }
}
