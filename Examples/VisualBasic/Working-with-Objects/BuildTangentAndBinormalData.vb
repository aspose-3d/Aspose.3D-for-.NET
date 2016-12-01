Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats

Namespace Working_with_Objects
    Class BuildTangentAndBinormalData
        Public Shared Sub Run()
            ' ExStart:BuildTangentAndBinormalData
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            ' Load an existing 3D file
            Dim scene As New Scene(MyDir & Convert.ToString("document.fbx"))
            ' Triangulate a scene
            PolygonModifier.BuildTangentBinormal(scene)
            ' Save 3D scene
            scene.Save("BuildTangentAndBinormalData_out.fbx", FileFormat.FBX7400ASCII)
            ' ExEnd:BuildTangentAndBinormalData              
        End Sub
    End Class
End Namespace

