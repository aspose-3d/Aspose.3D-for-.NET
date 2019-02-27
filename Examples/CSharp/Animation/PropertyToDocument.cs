using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;
using Aspose._3D.Examples.CSharp.Geometry_Hierarchy;

namespace Aspose._3D.Examples.CSharp.Animation
{
    class PropertyToDocument
    {
        public static void Run()
        {
            // ExStart:AddAnimationPropertyToDocument
            // Initialize scene object
            Scene scene = new Scene();

            // Call Common class create mesh using polygon builder method to set mesh instance 
            Mesh mesh = Common.CreateMeshUsingPolygonBuilder();             

            // Each cube node has their own translation
            Node cube1 = scene.RootNode.CreateChildNode("cube1", mesh);

            // Find translation property on node's transform object
            Property translation = cube1.Transform.FindProperty("Translation");
            
            // Create a curve mapping based on translation property
            CurveMapping mapping = new CurveMapping(scene, translation);

            // Create the animation curve on X component of the scale 
            mapping.BindCurve("X", new Curve()
            {
                // Move node's translation to (10, 0, 10) at 0 sec using bezier interpolation
                {0, 10.0f, Interpolation.Bezier},
                // Move node's translation to (20, 0, -10) at 3 sec
                {3, 20.0f, Interpolation.Bezier},
                // Move node's translation to (30, 0, 0) at 5 sec
                {5, 30.0f, Interpolation.Linear},
            });

            // Create the animation curve on Z component of the scale 
            mapping.BindCurve("Z", new Curve()
            {
                // Move node's translation to (10, 0, 10) at 0 sec using bezier interpolation
                {0, 10.0f, Interpolation.Bezier},
                // Move node's translation to (20, 0, -10) at 3 sec
                {3, -10.0f, Interpolation.Bezier},
                // Move node's translation to (30, 0, 0) at 5 sec
                {5, 0.0f, Interpolation.Linear},
            });

            // The path to the documents directory.
            string output = RunExamples.GetOutputFilePath("PropertyToDocument.fbx");            

            // Save 3D scene in the supported file formats
            scene.Save(output, FileFormat.FBX7500ASCII);
            // ExEnd:AddAnimationPropertyToDocument

            Console.WriteLine("\nAnimation property added successfully to document.\nFile saved at " + output);
            
        }
    }
}
