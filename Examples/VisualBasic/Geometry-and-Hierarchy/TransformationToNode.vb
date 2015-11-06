'///////////////////////////////////////////////////////////////////////
' Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
'
' This file is part of Aspose.3D. The source code in this file
' is only intended as a supplement to the documentation, and is provided
' "as is", without warranty of any kind, either expressed or implied.
'///////////////////////////////////////////////////////////////////////
Imports System.IO
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utils

Namespace Geometry_Hierarchy
    Public Class TransformationToNode
        Public Shared Sub Run()

            ' Initialize scene object
            Dim scene As New Scene()

            ' Initialize Node class object
            Dim cubeNode As New Node("cube")

            ' Call Common class create mesh method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMesh()

            ' Point node to the Mesh geometry
            cubeNode.Entity = mesh

            ' Set rotation
            cubeNode.Transform.Rotation = Quaternion.FromRotation(New Vector3(0, 1, 0), New Vector3(0.3, 0.5, 0.1))

            ' Set translation
            cubeNode.Transform.Translation = New Vector3(0, 0, 20)

            ' Add cube to the scene
            scene.RootNode.ChildNodes.Add(cubeNode)

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir_GeometryAndHierarchy()
            MyDir = MyDir & Convert.ToString("TransformationToNode.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)

            Console.WriteLine(Convert.ToString(vbLf & "Transformation added successfully to node." & vbLf & "File saved at ") & MyDir)

        End Sub
    End Class
End Namespace
