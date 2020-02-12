using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Profiles;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion
{
    public class TwistInLinearExtrusion
    {
        public static void Run()
        {
            // ExStart:TwistInLinearExtrusion
            // Initialize the base profile to be extruded
            var profile = new RectangleShape()
            {
                RoundingRadius = 0.3
            };
            // Create a scene 
            Scene scene = new Scene();
            // Create left node
            var left = scene.RootNode.CreateChildNode();
            // Create rifht node
            var right = scene.RootNode.CreateChildNode();
            left.Transform.Translation = new Vector3(15, 0, 0);

            // Twist property defines the degree of the rotation while extruding the profile
            // Perform linear extrusion on left node using twist and slices property
            left.CreateChildNode(new LinearExtrusion(profile, 10) { Twist = 0, Slices = 100 });
            // Perform linear extrusion on right node using twist and slices property
            right.CreateChildNode(new LinearExtrusion(profile, 10) { Twist = 90, Slices = 100 });

            // Save 3D scene
            scene.Save(RunExamples.GetOutputFilePath("TwistInLinearExtrusion.obj"), FileFormat.WavefrontOBJ);
            // ExEnd:TwistInLinearExtrusion            
        }
    }
}
