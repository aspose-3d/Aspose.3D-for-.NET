using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Profiles;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion
{
    public class SlicesInLinearExtrusion
    {
        public static void Run()
        {
            // ExStart:SlicesInLinearExtrusion
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
            left.Transform.Translation = new Vector3(15, 0, 0);
            
            // Slices parameter defines the number of intermediate points along the path of the extrusion
            // Perform linear extrusion on left node using slices property
            left.CreateChildNode(new LinearExtrusion(profile, 2) { Slices = 2 });
            // Perform linear extrusion on right node using slices property
            right.CreateChildNode(new LinearExtrusion(profile, 2) { Slices = 10 });

            // Save 3D scene
            scene.Save(RunExamples.GetOutputFilePath("SlicesInLinearExtrusion.obj"), FileFormat.WavefrontOBJ);
            // ExEnd:SlicesInLinearExtrusion            
        }
    }
}
