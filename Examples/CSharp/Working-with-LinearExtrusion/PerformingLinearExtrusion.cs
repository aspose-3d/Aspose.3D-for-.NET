using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion
{
    public class PerformingLinearExtrusion
    {
        public static void Run()
        {
            // ExStart:PerformingLinearExtrusion
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize the base shape to be extruded
            var shape = Shape.FromControlPoints(
            new Vector3(1, 1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(-1, -1, 0),
            new Vector3(1, -1, 0)
            );
            // Perform Linear extrusion by passing a 2D shape as input and extend the shape in the 3rd dimension
            var extrusion = new LinearExtrusion(shape, 10) { Slices = 100, Center = true, Twist = 360, TwistOffset = new Vector3(10, 0, 0) };
            // Create a scene 
            Scene scene = new Scene();
            // Create a child node by passing extrusion 
            scene.RootNode.CreateChildNode(extrusion);
            // Save 3D scene
            scene.Save(MyDir +  "LinearExtrusion.obj", FileFormat.WavefrontOBJ);
            // ExEnd:PerformingLinearExtrusion            
        }
    }
}
