Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD

Namespace Loading_Saving
    Public Class ReadExistingScene
        Public Shared Sub Run()

            ' ExStart:ReadExistingScene
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            ' Initialize a Scene class object
            Dim scene As New Scene()
            ' load an existing 3D document
            scene.Open(MyDir & Convert.ToString("document.fbx"))
            ' ExEnd:ReadExistingScene

            Console.WriteLine(vbLf & "3D Scene is ready for the modification, addition or processing purposes.")

        End Sub
    End Class
End Namespace

