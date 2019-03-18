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
            // ExStart:ConvertPolygonsToTriangles
            // Load an existing 3D file
            Scene scene = new Scene(RunExamples.GetDataFilePath("document.fbx"));
            // Triangulate a scene
            PolygonModifier.Triangulate(scene);
            // Save 3D scene
            scene.Save(RunExamples.GetOutputFilePath("triangulated_out.fbx"), FileFormat.FBX7400ASCII);
            // ExEnd:ConvertPolygonsToTriangles            
        }
    }
}
