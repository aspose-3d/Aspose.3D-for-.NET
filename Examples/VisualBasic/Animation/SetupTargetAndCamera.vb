'///////////////////////////////////////////////////////////////////////
' Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
'
' This file is part of Aspose.3D. The source code in this file
' is only intended as a supplement to the documentation, and is provided
' "as is", without warranty of any kind, either expressed or implied.
'///////////////////////////////////////////////////////////////////////
Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animations
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utils

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
            ' EndEnd:SetupTargetAndCamera
            Console.WriteLine(Convert.ToString(vbLf & "The target and camera has been setup successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace