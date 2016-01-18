Imports System.IO
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utils

Namespace Geometry_Hierarchy
    Public Class TransformationToNodeByTransformationMatrix
        Public Shared Sub Run()

            ' ExStart:AddTransformationToNodeByTransformationMatrix
            ' Initialize scene object
            Dim scene As New Scene()

            ' Initialize Node class object
            Dim cubeNode As New Node("cube")

            ' Call Common class create mesh using polygon builder method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMeshUsingPolygonBuilder()

            ' Point node to the Mesh geometry
            cubeNode.Entity = mesh
            ' Set custom translation matrix
            cubeNode.Transform.TransformMatrix = New Matrix4(1, -0.3, 0, 0, 0.4, 1,
                0.3, 0, 0, 0, 1, 0,
                0, 20, 0, 1)
            ' Add cube to the scene
            scene.RootNode.ChildNodes.Add(cubeNode)

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            MyDir = MyDir & RunExamples.GetOutputFilePath("TransformationToNode.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)
            ' ExEnd:AddTransformationToNodeByTransformationMatrix

            Console.WriteLine(Convert.ToString(vbLf & "Transformation added successfully to node." & vbLf & "File saved at ") & MyDir)

        End Sub
    End Class
End Namespace
