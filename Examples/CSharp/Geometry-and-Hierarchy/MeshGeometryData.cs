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
using Aspose.ThreeD.Shading;

namespace CSharp.Geometry_Hierarchy
{
    class MeshGeometryData
    {
        public static void Run()
        {
            // Initialize scene object
            Scene scene = new Scene();

            // Define color vectors
            Vector3[] colors = new Vector3[] {
    new Vector3(1, 0, 0),
    new Vector3(0, 1, 0),
    new Vector3(0, 0, 1)
};
            // Call Common class create meshh method to set mesh instance 
            Mesh mesh = Common.CreateMesh();

            int idx = 0;
            foreach (Vector3 color in colors)
            {
                // Initialize cube node object
                Node cube = new Node("cube");
                cube.Entity = mesh;
                LambertMaterial mat = new LambertMaterial();
                // Set color
                mat.DiffuseColor = color;
                // Set material
                cube.Material = mat;
                // Set translation
                cube.Transform.Translation = new Vector3(idx++ * 20, 0, 0);
                // Add cube node
                scene.RootNode.ChildNodes.Add(cube);
            }

            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir_GeometryAndHierarchy();
            MyDir = MyDir + "MeshGeometryData.fbx";
            
            // Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII);

            Console.WriteLine("\nMesh’s geometry data shared successfully between multiple nodes.\nFile saved at " + MyDir);

        }
    }
}
