Imports System.IO
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utilities

Namespace Geometry_Hierarchy
    Public Class SetupUVOnCube
        Public Shared Sub Run()
            ' ExStart:SetupUVOnCube
            ' UVs
            Dim uvs As Vector4() = New Vector4() {
                New Vector4(0.0, 1.0, 0.0, 1.0),
                New Vector4(1.0, 0.0, 0.0, 1.0),
                New Vector4(0.0, 0.0, 0.0, 1.0),
                New Vector4(1.0, 1.0, 0.0, 1.0)
            }

            ' Indices of the uvs per each polygon
            Dim uvsId As Integer() = New Integer() {
            0, 1, 3, 2, 2, 3, 5, 4, 4, 5, 7, 6, 6, 7, 9, 8, 1, 10, 11, 3, 12, 0, 2, 13
            }

            ' Call Common class create mesh using polygon builder method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMeshUsingPolygonBuilder()
            ' Create UVset
            Dim elementUV As VertexElementUV = mesh.CreateElementUV(TextureMapping.Diffuse, MappingMode.PolygonVertex, ReferenceMode.IndexToDirect)
            ' Copy the data to the UV vertex element
            'elementUV.Data.AddRange(uvs)
            elementUV.Indices.AddRange(uvsId)
            ' ExEnd:SetupUVOnCube

            Console.WriteLine(vbLf & "UVs has been setup successfully on cube.")
        End Sub
    End Class
End Namespace

