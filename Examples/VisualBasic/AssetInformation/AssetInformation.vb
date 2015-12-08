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

Namespace AssetInformation
    Public Class InformationToScene
        Public Shared Sub Run()

            ' ExStart:AddAssetInformationToScene  
            ' Initialize a 3D scene
            Dim scene As New Scene()

            ' Set application/tool name
            scene.AssetInfo.ApplicationName = "Egypt"

            ' Set application/tool vendor name
            scene.AssetInfo.ApplicationVendor = "Manualdesk"

            ' We use ancient egyption measurement unit Pole
            scene.AssetInfo.UnitName = "pole"

            ' One Pole equals to 60cm
            scene.AssetInfo.UnitScaleFactor = 0.6

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            MyDir = MyDir & RunExamples.GetOutputFilePath("InformationToScene.fbx")

            ' Save scene to 3D supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)
            ' ExEnd:AddAssetInformationToScene  

            Console.WriteLine(Convert.ToString(vbLf & "Asset information added successfully to Scene." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace

