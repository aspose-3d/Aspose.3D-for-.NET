using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;

namespace Aspose._3D.Examples.CSharp.Polygons
{
    class ConvertPolygonsToTriangles
    {
        public static void Run()
        {
            //ExStart:ConvertPolygonsToTriangles
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();           
            // Load an existing 3D file
            Scene scene = new Scene(MyDir + "document.fbx");
            // Triangulate a scene
            PolygonModifier.Triangulate(scene);
            // Save 3D scene
            scene.Save(MyDir + "triangulated_out_.fbx", FileFormat.FBX7400ASCII);
            //ExEnd:ConvertPolygonsToTriangles            
        }
    }
}
