Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats
Imports Aspose.ThreeD.Utilities
Imports System.Runtime.InteropServices

Namespace Working_with_Objects
    Class ConvertSphereMeshtoTriangleMeshCustomMemoryLayout
        ' ExStart:ConvertSphereMeshtoTriangleMeshCustomMemoryLayout
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure MyVertex
            <Semantic(VertexFieldSemantic.Position)> _
            Private position As FVector3
            <Semantic(VertexFieldSemantic.Normal)> _
            Private normal As FVector3
        End Structure

        Public Shared Sub Run()
            ' Initialize scene object
            Dim scene As New Scene()

            ' Initialize Node class object
            Dim cubeNode As New Node("sphere")

            Dim sphere As Mesh = (New Sphere()).ToMesh()
            'convert any mesh into typed TriMesh
            Dim myMesh = TriMesh(Of MyVertex).FromMesh(sphere)
            'Get the vertex data in customized vertex structure.
            Dim vertex As MyVertex() = myMesh.VerticesToTypedArray()
            'get the 32bit and 16bit indices
            Dim indices32bit As Integer()
            Dim indices16bit As UShort()
            indices32bit = Nothing
            indices16bit = Nothing
            myMesh.IndicesToArray(indices32bit)
            myMesh.IndicesToArray(indices16bit)
            Using ms As New MemoryStream()
                'or we can write the vertex directly into stream like:
                myMesh.WriteVerticesTo(ms)
                'the indice data can be directly write to stream, we support 32-bit and 16-bit indice.
                myMesh.Write16bIndicesTo(ms)
                myMesh.Write32bIndicesTo(ms)
            End Using
            ' Point node to the Mesh geometry
            cubeNode.Entity = sphere

            ' Add Node to a scene
            scene.RootNode.ChildNodes.Add(cubeNode)

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir() + RunExamples.GetOutputFilePath("SphereToTriangleMeshCustomMemoryLayoutScene.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)

            Console.WriteLine("Indices = {0}, Actual vertices = {1}, vertices before merging = {2}", myMesh.IndicesCount, myMesh.VerticesCount, myMesh.UnmergedVerticesCount)
            Console.WriteLine("Total bytes of vertices in memory {0}bytes", myMesh.VerticesSizeInBytes)
            Console.WriteLine(Convert.ToString(vbLf & " Converted a Sphere mesh to triangle mesh with custom memory layout of the vertex successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
        ' ExEnd:ConvertSphereMeshtoTriangleMeshCustomMemoryLayout

    End Class
End Namespace
