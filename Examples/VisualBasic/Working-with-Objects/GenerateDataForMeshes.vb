Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats

Namespace Working_with_Objects
    Class GenerateDataForMeshes
        Public Shared Sub Run()
            ' ExStart:GenerateDataForMeshes
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            ' Load a 3ds file, 3ds file doesn' T have normal data, but it has smoothing group
            Dim s As New Scene(MyDir & Convert.ToString("camera.3ds"))
            ' Visit all nodes and create normal data for all meshes
            s.RootNode.Accept(Function(n As Node)
                                  Dim mesh As Mesh = n.GetEntity(Of Mesh)()
                                  If mesh IsNot Nothing Then
                                      Dim normals As VertexElementNormal = PolygonModifier.GenerateNormal(mesh)
                                      mesh.VertexElements.Add(normals)
                                  End If
                                  Return True

                              End Function)
            ' ExEnd:GenerateDataForMeshes  
            Console.WriteLine(vbLf & "Normal data generated successfully for all meshes.")
        End Sub
    End Class
End Namespace