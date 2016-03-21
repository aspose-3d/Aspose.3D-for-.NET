Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utilities
Imports Aspose.ThreeD.Shading
Imports System.Drawing
Imports System.IO

Namespace Geometry_Hierarchy
    Public Class MaterialToCube

        Public Shared Sub Run()

            ' ExStart:AddMaterialToCube
            ' Initialize scene object
            Dim scene As New Scene()

            ' Initialize cube node object
            Dim cubeNode As New Node("cube")

            ' Call Common class create mesh using polygon builder method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMeshUsingPolygonBuilder()

            ' Point node to the mesh
            cubeNode.Entity = mesh

            ' Add cube to the scene
            scene.RootNode.ChildNodes.Add(cubeNode)

            ' Initiallize PhongMaterial object
            Dim mat As New PhongMaterial()

            ' Initiallize Texture object
            Dim diffuse As New Texture()

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            ' Set local file path
            diffuse.FileName = MyDir & Convert.ToString("surface.dds")

            ' Set Texture of the material
            mat.SetTexture("DiffuseColor", diffuse)

            ' embed raw content data to FBX (only for FBX And optional)
            ' set file name
            diffuse.FileName = "embedded-texture.png"
            ' set binary content
            diffuse.Content = File.ReadAllBytes("c:\\test.png")

            ' Set color
            mat.SpecularColor = New Vector3(Color.Red)

            ' Set brightness
            mat.Shininess = 100

            ' Set material property of the cube object
            cubeNode.Material = mat

            MyDir = MyDir & RunExamples.GetOutputFilePath("MaterialToCube.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)
            ' ExEnd:AddMaterialToCube

            Console.WriteLine(Convert.ToString(vbLf & "Material added successfully to cube." & vbLf & "File saved at ") & MyDir)

        End Sub
    End Class
End Namespace

