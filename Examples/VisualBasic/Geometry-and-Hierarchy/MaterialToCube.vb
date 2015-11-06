
'///////////////////////////////////////////////////////////////////////
' Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
'
' This file is part of Aspose.3D. The source code in this file
' is only intended as a supplement to the documentation, and is provided
' "as is", without warranty of any kind, either expressed or implied.
'///////////////////////////////////////////////////////////////////////
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utils
Imports Aspose.ThreeD.Shading
Imports System.Drawing

Namespace Geometry_Hierarchy
    Public Class MaterialToCube

        Public Shared Sub Run()

            ' Initialize scene object
            Dim scene As New Scene()

            ' Initialize cube node object
            Dim cubeNode As New Node("cube")

            ' Call Common class create mesh method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMesh()

            ' Point node to the mesh
            cubeNode.Entity = mesh

            ' Add cube to the scene
            scene.RootNode.ChildNodes.Add(cubeNode)

            ' Initiallize PhongMaterial object
            Dim mat As New PhongMaterial()

            ' Initiallize Texture object
            Dim diffuse As New Texture()

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir_GeometryAndHierarchy()

            ' Set local file path
            diffuse.FileName = MyDir & Convert.ToString("surface.dds")

            ' Set Texture of the material
            mat.SetTexture("DiffuseColor", diffuse)

            ' Set color
            mat.SpecularColor = New Vector3(Color.Red)

            ' Set brightness
            mat.Shininess = 100

            ' Set material property of the cube object
            cubeNode.Material = mat

            MyDir = MyDir & Convert.ToString("MaterialToCube.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)

            Console.WriteLine(Convert.ToString(vbLf & "Material added successfully to cube." & vbLf & "File saved at ") & MyDir)

        End Sub
    End Class
End Namespace

