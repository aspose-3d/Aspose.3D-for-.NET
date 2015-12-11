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
Namespace Loading_Saving
    Public Class CreateEmpty3DDocument
        Public Shared Sub Run()
            ' ExStart:CreateEmpty3DDocument
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            MyDir = MyDir & Convert.ToString("document.fbx")

            ' Create an object of the Scene class
            Dim scene As New Scene()
            ' Save 3D scene document
            scene.Save(MyDir, FileFormat.FBX7400ASCII)
            ' ExEnd:CreateEmpty3DDocument

            Console.WriteLine(Convert.ToString(vbLf & "An empty 3D document created successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace