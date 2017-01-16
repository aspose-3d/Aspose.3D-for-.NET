Imports Aspose.ThreeD
Imports System.IO
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utilities

Public Class Save3DMeshesInCustomBinaryFormat
    ' ExStart:Save3DMeshesInCustomBinaryFormat
    Public Shared Sub Run()
        ' The path to the documents directory.
        Dim MyDir As String = RunExamples.GetDataDir()
        ' load a 3D file
        Dim scene As New Scene(MyDir + "test.fbx")
        ' visit each descent nodes
        scene.RootNode.Accept(AddressOf VisitMeshNodes)
    End Sub
    Private Shared Function VisitMeshNodes(node As Node)
        ' The path to the documents directory.
        Dim MyDir As String = RunExamples.GetDataDir()
        ' open file for writing in binary mode
        Using writer = New BinaryWriter(New FileStream(MyDir + "Save3DMeshesInCustomBinaryFormat_out", FileMode.Create, FileAccess.Write))
            For Each entity As Entity In node.Entities
                ' only convert meshes, lights/camera and other stuff will be ignored
                If Not (TypeOf entity Is IMeshConvertible) Then
                    Continue For
                End If
                Dim m As Mesh = DirectCast(entity, IMeshConvertible).ToMesh()
                Dim controlPoints = m.ControlPoints
                ' triangulate the mesh, so triFaces will only store triangle indices
                Dim triFaces As Integer()() = PolygonModifier.Triangulate(controlPoints, m.Polygons)
                ' gets the global transform matrix
                Dim transform As Matrix4 = node.GlobalTransform.TransformMatrix
                ' write number of control points and triangle indices
                writer.Write(controlPoints.Count)
                writer.Write(triFaces.Length)
                ' write control points
                For i As Integer = 0 To controlPoints.Count - 1
                    ' calculate the control points in world space and save them to file
                    Dim cp = transform * controlPoints(i)
                    writer.Write(CSng(cp.x))
                    writer.Write(CSng(cp.y))
                    writer.Write(CSng(cp.z))
                Next
                ' write triangle indices
                For i As Integer = 0 To triFaces.Length - 1
                    writer.Write(triFaces(i)(0))
                    writer.Write(triFaces(i)(1))
                    writer.Write(triFaces(i)(2))
                Next
            Next
            Return True
        End Using
    End Function
    ' ExEnd:Save3DMeshesInCustomBinaryFormat
End Class
