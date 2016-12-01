Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats
Imports Aspose.ThreeD.Utilities
Imports System.Runtime.InteropServices

Namespace Working_with_Objects
    Class ConvertBoxMeshtoTriangleMeshCustomMemoryLayout
        Public Shared Sub Run()
            ' Initialize scene object
            Dim scene As New Scene()

            ' Initialize Node class object
            Dim cubeNode As New Node("box")

            ' ExStart:ConvertBoxMeshtoTriangleMeshCustomMemoryLayout
            ' Get mesh of the Box
            Dim box As Mesh = (New Box()).ToMesh()
            ' Create a customized vertex layout
            Dim vd As New VertexDeclaration()
            Dim position As VertexField = vd.AddField(VertexFieldDataType.FVector4, VertexFieldSemantic.Position)
            vd.AddField(VertexFieldDataType.FVector3, VertexFieldSemantic.Normal)
            ' Get a triangle mesh
            Dim triMesh__1 As TriMesh = TriMesh.FromMesh(box)
            ' ExEnd:ConvertBoxMeshtoTriangleMeshCustomMemoryLayout

            ' Point node to the Mesh geometry
            cubeNode.Entity = box

            ' Add Node to a scene
            scene.RootNode.ChildNodes.Add(cubeNode)

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir() + RunExamples.GetOutputFilePath("BoxToTriangleMeshCustomMemoryLayoutScene.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)

            Console.WriteLine(Convert.ToString(vbLf & " Converted a Box mesh to triangle mesh with custom memory layout of the vertex successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace