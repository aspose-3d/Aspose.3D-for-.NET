using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion
{
    public class SlicesInLinearExtrusion
    {
        public static void Run()
        {
            // ExStart:SlicesInLinearExtrusion
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
            
            // Slices parameter defines the number of intermediate points along the path of the extrusion
            // Perform linear extrusion on left node using slices property
            left.CreateChildNode(new LinearExtrusion(shape, 2) { Slices = 2 });
            // Perform linear extrusion on right node using slices property
            right.CreateChildNode(new LinearExtrusion(shape, 2) { Slices = 10 });

            // Save 3D scene
            scene.Save(MyDir + "SlicesInLinearExtrusion.obj", FileFormat.WavefrontOBJ);
            // ExEnd:SlicesInLinearExtrusion            
        }
    }
}
