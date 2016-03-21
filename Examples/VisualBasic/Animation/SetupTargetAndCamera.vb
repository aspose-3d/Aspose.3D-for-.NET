Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utilities

Namespace Animation
    Class SetupTargetAndCamera
        Public Shared Sub Run()
            ' ExStart:SetupTargetAndCamera
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize scene object
            Dim scene As New Scene()
            ' Get a child node object
            Dim cameraNode As Node = scene.RootNode.CreateChildNode("camera", New Camera())
            ' Set camera node translation
            cameraNode.Transform.Translation = New Vector3(100, 20, 0)
            cameraNode.GetEntity(Of Camera)().Target = scene.RootNode.CreateChildNode("target")
            MyDir = MyDir & Convert.ToString("camera-test.3ds")
            scene.Save(MyDir, FileFormat.Distreet3DS)
            ' ExEnd:SetupTargetAndCamera
            Console.WriteLine(Convert.ToString(vbLf & "The target and camera has been setup successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace