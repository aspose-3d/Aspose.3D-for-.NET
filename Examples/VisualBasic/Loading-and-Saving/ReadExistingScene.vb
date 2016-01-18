Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD

Namespace Loading_Saving
    Public Class ReadExistingScene
        Public Shared Sub Run()

            ' ExStart:ReadExistingScene
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            MyDir = MyDir & Convert.ToString("document.fbx")

            ' Call the scene constructor to load an existing one
            Dim scene As New Scene(MyDir)

            ' Initialize a scene object to build a scene from scratch
            Dim parentScene As New Scene()

            ' Initialize a scene object and also define its parent scene. New scene can also be used as parent scene
            Dim childscene As New Scene(parentScene, MyDir)
            'ExEnd:ReadExistingScene

            Console.WriteLine(vbLf & "3D Scene is ready for the modification, addition or processing purposes.")

        End Sub
    End Class
End Namespace

