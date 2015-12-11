'///////////////////////////////////////////////////////////////////////
' Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
'
' This file is part of Aspose.3D. The source code in this file
' is only intended as a supplement to the documentation, and is provided
' "as is", without warranty of any kind, either expressed or implied.
'///////////////////////////////////////////////////////////////////////
Imports System.IO
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utils

Namespace Geometry_Hierarchy
    Public Class SetupNormalsOnCube

        Public Shared Sub Run()
            ' ExStart:SetupNormalsOnCube
            ' Raw normal data
            Dim normals As Vector4() = New Vector4() {New Vector4(-0.577350258, -0.577350258, 0.577350258, 1.0), New Vector4(0.577350258, -0.577350258, 0.577350258, 1.0), New Vector4(0.577350258, 0.577350258, 0.577350258, 1.0), New Vector4(-0.577350258, 0.577350258, 0.577350258, 1.0), New Vector4(-0.577350258, -0.577350258, -0.577350258, 1.0), New Vector4(0.577350258, -0.577350258, -0.577350258, 1.0), _
                New Vector4(0.577350258, 0.577350258, -0.577350258, 1.0), New Vector4(-0.577350258, 0.577350258, -0.577350258, 1.0)}

            ' Call Common class create mesh using polygon builder method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMeshUsingPolygonBuilder()

            Dim elementNormal As VertexElementNormal = TryCast(mesh.CreateElement(VertexElementType.Normal), VertexElementNormal)
            ' Specify normal per control point.
            elementNormal.MappingMode = MappingMode.ControlPoint
            ' The data is directly referenced.
            elementNormal.ReferenceMode = ReferenceMode.Direct
            ' Copy the data to the vertex element
            elementNormal.Data.AddRange(normals)
            ' ExEnd:SetupNormalsOnCube

            Console.WriteLine(vbLf & "Normals has been setup successfully on cube.")
        End Sub
    End Class
End Namespace
