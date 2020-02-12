using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Profiles;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion
{
    public class TwistOffsetInLinearExtrusion
    {
        public static void Run()
        {
            // ExStart:TwistOffsetInLinearExtrusion
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
            left.Transform.Translation = new Vector3(18, 0, 0);

            // TwistOffset property is the translate offset while rotating the extrusion.
            // Perform linear extrusion on left node using twist and slices property
            left.CreateChildNode(new LinearExtrusion(profile, 10) { Twist = 360, Slices = 100 });
            // Perform linear extrusion on right node using twist, twist offset and slices property
            right.CreateChildNode(new LinearExtrusion(profile, 10) { Twist = 360, Slices = 100, TwistOffset = new Vector3(3, 0, 0) });

            // Save 3D scene
            scene.Save(RunExamples.GetOutputFilePath("TwistOffsetInLinearExtrusion.obj"), FileFormat.WavefrontOBJ);
            // ExEnd:TwistOffsetInLinearExtrusion            
        }
    }
}
