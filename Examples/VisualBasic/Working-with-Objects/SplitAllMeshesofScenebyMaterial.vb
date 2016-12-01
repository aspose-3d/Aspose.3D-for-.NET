Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats

Namespace Working_with_Objects
    Class SplitAllMeshesofScenebyMaterial
        Public Shared Sub Run()
            ' ExStart:SplitAllMeshesofScenebyMaterial
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            MyDir = MyDir & Convert.ToString("test.fbx")

            ' load a 3D file
            Dim scene As New Scene(MyDir)
            ' Split all meshes
            PolygonModifier.SplitMesh(scene, SplitMeshPolicy.CloneData)

            ' Save file
            MyDir = RunExamples.GetDataDir() + RunExamples.GetOutputFilePath("test-splitted.fbx")
            scene.Save(MyDir, FileFormat.FBX7500ASCII)

            ' ExEnd:SplitAllMeshesofScenebyMaterial
            Console.WriteLine(Convert.ToString(vbLf & "Spliting all meshes of a scene per material successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace