Imports System.IO
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utilities

Namespace Geometry_Hierarchy
    Public Class SetupNormalsOnCube

        Public Shared Sub Run()
            ' ExStart:SetupNormalsOnCube
            ' Raw normal data
            Dim normals As Vector4() = New Vector4() {
                New Vector4(-0.577350258, -0.577350258, 0.577350258, 1.0),
                New Vector4(0.577350258, -0.577350258, 0.577350258, 1.0),
                New Vector4(0.577350258, 0.577350258, 0.577350258, 1.0),
                New Vector4(-0.577350258, 0.577350258, 0.577350258, 1.0),
                New Vector4(-0.577350258, -0.577350258, -0.577350258, 1.0),
                New Vector4(0.577350258, -0.577350258, -0.577350258, 1.0),
                New Vector4(0.577350258, 0.577350258, -0.577350258, 1.0),
                New Vector4(-0.577350258, 0.577350258, -0.577350258, 1.0)}

            ' Call Common class create mesh using polygon builder method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMeshUsingPolygonBuilder()

            Dim elementNormal As VertexElementNormal = TryCast(mesh.CreateElement(VertexElementType.Normal, MappingMode.ControlPoint, ReferenceMode.Direct), VertexElementNormal)
            ' Copy the data to the vertex element
            elementNormal.Data.AddRange(normals)
            ' ExEnd:SetupNormalsOnCube

            Console.WriteLine(vbLf & "Normals has been setup successfully on cube.")
        End Sub
    End Class
End Namespace
