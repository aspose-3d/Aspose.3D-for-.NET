Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD

Namespace Loading_Saving
    Public Class Save3DScene
        Public Shared Sub Run()
            ' ExStart:Save3DScene
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            ' Load a 3D document into Aspose.3D
            Dim scene As New Scene()
            ' open an existing 3D scene
            scene.Open(MyDir & Convert.ToString("document.fbx"))

            ' save 3D Scene to a stream
            Dim dstStream As New MemoryStream()
            scene.Save(dstStream, FileFormat.FBX7500ASCII)

            ' Rewind the stream position back to zero so it is ready for next reader.
            dstStream.Position = 0

            ' save 3D Scene to a local path
            scene.Save(MyDir + "output.fbx", FileFormat.FBX7500ASCII)
            ' ExEnd:Save3DScene

            Console.WriteLine(vbLf & "Converted 3D document to stream successfully.")
        End Sub
    End Class
End Namespace
