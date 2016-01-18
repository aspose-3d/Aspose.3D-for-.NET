Imports System.IO
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utils

Namespace Geometry_Hierarchy
    Public Class NodeHierarchy
        Public Shared Sub Run()

            ' ExStart:AddNodeHierarchy
            ' Initialize scene object
            Dim scene As New Scene()

            ' Get a child node object
            Dim top As Node = scene.RootNode.CreateChildNode()

            ' Each cube node has their own translation
            Dim cube1 As Node = top.CreateChildNode("cube1")

            ' Call Common class create mesh using polygon builder method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMeshUsingPolygonBuilder()

            ' Point node to the mesh
            cube1.Entity = mesh
            ' Set first cube translation
            cube1.Transform.Translation = New Vector3(-10, 0, 0)

            Dim cube2 As Node = top.CreateChildNode("cube2")
            ' Point node to the mesh
            cube2.Entity = mesh

            ' Set second cube translation
            cube2.Transform.Translation = New Vector3(10, 0, 0)

            ' The rotated top node will affect all child nodes
            top.Transform.Rotation = Quaternion.FromEulerAngle(Math.PI, 4, 0)

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            MyDir = MyDir & RunExamples.GetOutputFilePath("NodeHierarchy.fbx")
            
            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)
            ' ExEnd:AddNodeHierarchy

            Console.WriteLine(Convert.ToString(vbLf & "Node hierarchy added successfully to document." & vbLf & "File saved at ") & MyDir)

        End Sub
    End Class
End Namespace