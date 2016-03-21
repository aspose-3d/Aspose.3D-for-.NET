using System;
using System.Collections.Generic;
using System.IO;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;

namespace CSharp.Geometry_Hierarchy
{
    class NodeHierarchy
    {
        public static void Run()
        {
            //ExStart:AddNodeHierarchy
            // Initialize scene object
            Scene scene = new Scene();

            // Get a child node object
            Node top = scene.RootNode.CreateChildNode();
            // Each cube node has their own translation
            Node cube1 = top.CreateChildNode("cube1");
            // Call Common class create mesh using polygon builder method to set mesh instance 
            Mesh mesh = Common.CreateMeshUsingPolygonBuilder();            
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
            string MyDir = RunExamples.GetDataDir();
            MyDir = MyDir + RunExamples.GetOutputFilePath("NodeHierarchy.fbx");
            
            // Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7500ASCII);
            //ExEnd:AddNodeHierarchy
           
            Console.WriteLine("\nNode hierarchy added successfully to document.\nFile saved at " + MyDir);

        }
    }
}
