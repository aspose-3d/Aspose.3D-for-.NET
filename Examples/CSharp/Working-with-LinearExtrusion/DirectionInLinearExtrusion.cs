using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Profiles;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion
{
    public class DirectionInLinearExtrusion
    {
        public static void Run()
        {
            // ExStart:DirectionInLinearExtrusion
            // Initialize the base profile to be extruded
            var profile = new RectangleShape()
            {
                RoundingRadius = 0.3
            };
            // Create a scene 
            Scene scene = new Scene();
            // Create left node
            var left = scene.RootNode.CreateChildNode();
            // Create right node
            var right = scene.RootNode.CreateChildNode();
            left.Transform.Translation = new Vector3(8, 0, 0);

            // Direction property defines the direction of the extrusion.
            // Perform linear extrusion on left node using twist and slices property
            left.CreateChildNode(new LinearExtrusion(profile, 10) { Twist = 360, Slices = 100 });
            // Perform linear extrusion on right node using twist, slices, and direction property
            right.CreateChildNode(new LinearExtrusion(profile, 10) { Twist = 360, Slices = 100, Direction = new Vector3(0.3, 0.2, 1) });

            // Save 3D scene
            scene.Save(RunExamples.GetOutputFilePath("DirectionInLinearExtrusion.obj"), FileFormat.WavefrontOBJ);
            // ExEnd:DirectionInLinearExtrusion            
        }
    }
}
