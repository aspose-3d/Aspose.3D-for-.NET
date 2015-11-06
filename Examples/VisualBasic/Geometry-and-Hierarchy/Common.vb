
'///////////////////////////////////////////////////////////////////////
' Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
'
' This file is part of Aspose.3D. The source code in this file
' is only intended as a supplement to the documentation, and is provided
' "as is", without warranty of any kind, either expressed or implied.
'/////////////////////////////////////////////////////////////////////
Imports System
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utils

Namespace Geometry_Hierarchy
    Public Class Common
        Public Shared Function CreateMesh() As Mesh
            ' Initialize control points
            Dim controlPoints As Vector4() = New Vector4() {
                New Vector4(-5.0, 0.0, 5.0, 1.0),
                New Vector4(5.0, 0.0, 5.0, 1.0),
                New Vector4(5.0, 10.0, 5.0, 1.0),
                New Vector4(-5.0, 10.0, 5.0, 1.0),
                New Vector4(-5.0, 0.0, -5.0, 1.0),
                New Vector4(5.0, 0.0, -5.0, 1.0),
                New Vector4(5.0, 10.0, -5.0, 1.0),
                New Vector4(-5.0, 10.0, -5.0, 1.0)
            }
            ' Initialize mesh object
            Dim mesh As New Mesh()
            ' Add control points to the mesh
            mesh.ControlPoints.AddRange(controlPoints)

            ' Indices of the vertices per each polygon
            ' 0, 1, 2, 3 -> front face (Z+)
            ' 1, 5, 6, 2 -> right side (X+)
            ' 5, 4, 7, 6 -> back face (Z-)
            ' 4, 0, 3, 7 -> left side (X-)
            ' 0, 4, 5, 1 -> bottom face (Y-)
            ' 3, 2, 6, 7 -> top face (Y+)
            Dim indices = New Integer() {0, 1, 2, 3, 1, 5, 6, 2, 5, 4, 7, 6, 4, 0, 3, 7, 0, 4, 5, 1, 3, 2, 6, 7}


            Dim vertexId As Integer = 0
            Dim builder As New PolygonBuilder(mesh)
            For face As Integer = 0 To 5
                ' Start defining a new polygon
                builder.Begin()
                For v As Integer = 0 To 3
                    ' The indice of vertice per each polygon
                    builder.AddVertex(indices(vertexId))
                    vertexId = vertexId + 1
                Next
                ' Finished one polygon
                builder.End()
            Next

            Return mesh
        End Function
    End Class
End Namespace

