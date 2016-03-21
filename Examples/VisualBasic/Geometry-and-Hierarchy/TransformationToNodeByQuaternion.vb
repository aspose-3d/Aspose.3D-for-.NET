Imports System.IO
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utilities

Namespace Geometry_Hierarchy
    Public Class TransformationToNodeByQuaternion
        Public Shared Sub Run()

            ' ExStart:AddTransformationToNodeByQuaternion
            ' Initialize scene object
            Dim scene As New Scene()

            ' Initialize Node class object
            Dim cubeNode As New Node("cube")

            ' Call Common class create mesh using polygon builder method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMeshUsingPolygonBuilder()

            ' Point node to the Mesh geometry
            cubeNode.Entity = mesh
            ' Set rotation
            cubeNode.Transform.Rotation = Quaternion.FromRotation(New Vector3(0, 1, 0), New Vector3(0.3, 0.5, 0.1))
            ' Set translation
            cubeNode.Transform.Translation = New Vector3(0, 0, 20)
            ' Add cube to the scene
            scene.RootNode.ChildNodes.Add(cubeNode)

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            MyDir = MyDir & RunExamples.GetOutputFilePath("TransformationToNode.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7500ASCII)
            ' ExEnd:AddTransformationToNodeByQuaternion

            Console.WriteLine(Convert.ToString(vbLf & "Transformation added successfully to node." & vbLf & "File saved at ") & MyDir)

        End Sub
    End Class
End Namespace
