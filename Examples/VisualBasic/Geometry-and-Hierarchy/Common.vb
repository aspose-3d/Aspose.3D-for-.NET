
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
        Private Shared Function DefineControlPoints() As Vector4()
            ' ExStart:DefineControlPoints
            ' Initialize control points
            Dim controlPoints As Vector4() = New Vector4() {New Vector4(-5.0, 0.0, 5.0, 1.0), New Vector4(5.0, 0.0, 5.0, 1.0), New Vector4(5.0, 10.0, 5.0, 1.0), New Vector4(-5.0, 10.0, 5.0, 1.0), New Vector4(-5.0, 0.0, -5.0, 1.0), New Vector4(5.0, 0.0, -5.0, 1.0), _
                New Vector4(5.0, 10.0, -5.0, 1.0), New Vector4(-5.0, 10.0, -5.0, 1.0)}
            ' ExEnd:DefineControlPoints
            Return controlPoints
        End Function

        Public Shared Function CreateMeshUsingPolygonBuilder() As Mesh

            ' ExStart:CreateMeshUsingPolygonBuilder            
            Dim controlPoints As Vector4() = DefineControlPoints()
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

            ' ExEnd:CreateMeshUsingPolygonBuilder

            Return mesh
        End Function
        Public Shared Function CreateMeshUsingCreatePolygons() As Mesh
            ' ExStart:CreateMeshUsingCreatePolygons           
            Dim controlPoints As Vector4() = DefineControlPoints()

            ' Initialize mesh object
            Dim mesh As New Mesh()
            ' Add control points to the mesh
            mesh.ControlPoints.AddRange(controlPoints)
            ' create polygons to mesh
            ' front face (Z+)
            mesh.CreatePolygon(New Integer() {0, 1, 2, 3})
            ' right side (X+)
            mesh.CreatePolygon(New Integer() {1, 5, 6, 2})
            ' back face (Z-)
            mesh.CreatePolygon(New Integer() {5, 4, 7, 6})
            ' left side (X-)
            mesh.CreatePolygon(New Integer() {4, 0, 3, 7})
            ' bottom face (Y-)
            mesh.CreatePolygon(New Integer() {0, 4, 5, 1})
            ' top face (Y+)
            mesh.CreatePolygon(New Integer() {3, 2, 6, 7})
            ' ExEnd:CreateMeshUsingCreatePolygons
            Return mesh
        End Function
    End Class
End Namespace

