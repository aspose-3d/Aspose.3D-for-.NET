using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion
{
    public class CenterInLinearExtrusion
    {
        public static void Run()
        {
            // ExStart:CenterInLinearExtrusion
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize the base shape to be extruded
            var shape = Shape.FromControlPoints(
            new Vector3(1, 1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(-1, -1, 0),
            new Vector3(1, -1, 0)
            );
            // Create a scene 
            Scene scene = new Scene();
            // Create left node
            var left = scene.RootNode.CreateChildNode();
            // Create right node
            var right = scene.RootNode.CreateChildNode();
            left.Transform.Translation = new Vector3(5, 0, 0);

            // If Center property is true, the extrusion range is from -Height/2 to Height/2, otherwise the extrusion is from 0 to Height
            // Perform linear extrusion on left node using center and slices property
            left.CreateChildNode(new LinearExtrusion(shape, 2) { Center = false, Slices = 3 });
            // Set ground plane for reference
            left.CreateChildNode(new Box(0.01, 3, 3));
            // Perform linear extrusion on left node using center and slices property
            right.CreateChildNode(new LinearExtrusion(shape, 2) { Center = true, Slices = 3 });
            // Set ground plane for reference
            right.CreateChildNode(new Box(0.01, 3, 3));

            // Save 3D scene
            scene.Save(MyDir + "CenterInLinearExtrusion.obj", FileFormat.WavefrontOBJ);
            // ExEnd:CenterInLinearExtrusion            
        }
    }
}
