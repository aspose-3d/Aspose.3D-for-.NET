using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion
{
    public class TwistInLinearExtrusion
    {
        public static void Run()
        {
            // ExStart:TwistInLinearExtrusion
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
            // Create rifht node
            var right = scene.RootNode.CreateChildNode();
            left.Transform.Translation = new Vector3(5, 0, 0);

            // Twist property defines the degree of the rotation while extruding the shape
            // Perform linear extrusion on left node using twist and slices property
            left.CreateChildNode(new LinearExtrusion(shape, 10) { Twist = 0, Slices = 100 });
            // Perform linear extrusion on right node using twist and slices property
            right.CreateChildNode(new LinearExtrusion(shape, 10) { Twist = 90, Slices = 100 });

            // Save 3D scene
            scene.Save(MyDir + "TwistInLinearExtrusion.obj", FileFormat.WavefrontOBJ);
            // ExEnd:TwistInLinearExtrusion            
        }
    }
}
