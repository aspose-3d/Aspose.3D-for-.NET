Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities

Namespace Polygons
    Class ConvertPolygonsToTriangles
        Public Shared Sub Run()
            'ExStart:ConvertPolygonsToTriangles
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Load an existing 3D file
            Dim scene As New Scene(MyDir & Convert.ToString("document.fbx"))
            ' Triangulate a scene
            PolygonModifier.Triangulate(scene)
            ' Save 3D scene
            scene.Save(MyDir & Convert.ToString("triangulated_out_.fbx"), FileFormat.FBX7400ASCII)
            'ExEnd:ConvertPolygonsToTriangles            
        End Sub
    End Class
End Namespace

