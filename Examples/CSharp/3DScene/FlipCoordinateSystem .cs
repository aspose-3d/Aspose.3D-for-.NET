//////////////////////////////////////////////////////////////////////////
// Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.3D. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animations;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.IO;

namespace CSharp._3DScene
{
    class FlipCoordinateSystem
    {
        public static void Run()
        {
            //ExStart:FlipCoordinateSystem
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();            
            // Initialize scene object
            Scene scene = new Scene();
            scene.Open(MyDir + "camera.3ds", new Discreet3DSConfig() { FlipCoordinateSystem = true });
            MyDir = MyDir + "FlipCoordinateSystem.obj";
            scene.Save(MyDir, new ObjConfig() { EnableMaterials = false });
            //ExEnd:FlipCoordinateSystem
            Console.WriteLine("\nCoordinate system has been flipped successfully.\nFile saved at " + MyDir);
        }
    }
}
