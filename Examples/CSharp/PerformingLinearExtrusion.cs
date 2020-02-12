using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Profiles;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion
{
    public class PerformingLinearExtrusion
    {
        public static void Run()
        {
            // ExStart:PerformingLinearExtrusion
            // Initialize the base profile to be extruded
            var profile = new RectangleShape()
            {
                RoundingRadius = 0.3
            };
            // Perform Linear extrusion by passing a 2D profile as input and extend the shape in the 3rd dimension
            var extrusion = new LinearExtrusion(profile, 10) { Slices = 100, Center = true, Twist = 360, TwistOffset = new Vector3(10, 0, 0) };
            // Create a scene 
            Scene scene = new Scene();
            // Create a child node by passing extrusion 
            scene.RootNode.CreateChildNode(extrusion);
            // Save 3D scene
            scene.Save(RunExamples.GetOutputFilePath( "LinearExtrusion.obj"), FileFormat.WavefrontOBJ);
            // ExEnd:PerformingLinearExtrusion            
        }
    }
}
