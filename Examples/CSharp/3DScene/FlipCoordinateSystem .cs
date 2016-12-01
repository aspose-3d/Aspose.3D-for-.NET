using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp._3DScene
{
    class FlipCoordinateSystem
    {
        public static void Run()
        {
            // ExStart:FlipCoordinateSystem
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();            
            // Initialize scene object
            Scene scene = new Scene();
            scene.Open(MyDir + "camera.3ds", FileFormat.Discreet3DS);
            MyDir = MyDir + "FlipCoordinateSystem.obj";
            scene.Save(MyDir, FileFormat.WavefrontOBJ);
            // ExEnd:FlipCoordinateSystem
            Console.WriteLine("\nCoordinate system has been flipped successfully.\nFile saved at " + MyDir);
        }
    }
}
