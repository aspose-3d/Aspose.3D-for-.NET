Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Formats

Namespace Loading_Saving
    Class ExtractAll3DScenes
        Public Shared Sub Run()
            'ExStart:ExtractAll3DScenes
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            Dim password As Byte() = Nothing
            Dim scenes As List(Of Scene) = FileFormat.PDF.ExtractScene(MyDir & Convert.ToString("House_Design.pdf"), password)
            Dim i As Integer = 1
            ' Iterate through the scenes and save in 3D files
            For Each scene As Scene In scenes
                Dim fileName As String = "3d-" + (System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)) + ".fbx"
                scene.Save(fileName, FileFormat.FBX7400ASCII)
            Next
            'ExEnd:ExtractAll3DScenes            
        End Sub
    End Class
End Namespace
