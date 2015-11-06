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
Imports Aspose.ThreeD.Shading

Namespace Geometry_Hierarchy
    Public Class MeshGeometryData
        Public Shared Sub Run()

            ' Initialize scene object
            Dim scene As New Scene()

            ' Define color vectors
            Dim colors As Vector3() = New Vector3() {New Vector3(1, 0, 0), New Vector3(0, 1, 0), New Vector3(0, 0, 1)}
            ' Call Common class create meshh method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMesh()

            Dim idx As Integer = 0
            For Each color As Vector3 In colors
                ' Initialize cube node object
                Dim cube As New Node("cube")
                cube.Entity = mesh
                Dim mat As New LambertMaterial()
                ' Set color
                mat.DiffuseColor = color
                ' Set material
                cube.Material = mat
                ' Set translation
                cube.Transform.Translation = New Vector3(System.Math.Max(System.Threading.Interlocked.Increment(idx), idx - 1) * 20, 0, 0)
                ' Add cube node
                scene.RootNode.ChildNodes.Add(cube)
            Next

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir_GeometryAndHierarchy()
            MyDir = MyDir & Convert.ToString("MeshGeometryData.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)

            Console.WriteLine(Convert.ToString(vbLf & "Mesh’s geometry data shared successfully between multiple nodes." & vbLf & "File saved at ") & MyDir)

        End Sub
    End Class
End Namespace
