Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats

Namespace Working_with_Objects
    Class SplitMeshbyMaterial
        Public Shared Sub Run()
            ' ExStart:SplitMeshbyMaterial

            ' Create a mesh of box(A box is composed by 6 planes)
            Dim box As Mesh = (New Box()).ToMesh()
            ' Create a material element on this mesh
            Dim mat As VertexElementMaterial = DirectCast(box.CreateElement(VertexElementType.Material, MappingMode.Polygon, ReferenceMode.Index), VertexElementMaterial)
            ' And specify different material index for each plane
            mat.Indices.AddRange(New Integer() {0, 1, 2, 3, 4, 5})
            ' Now split it into 6 sub meshes, we specified 6 different materials on each plane, each plane will become a sub mesh.
            ' We used the CloneData policy, each plane will has the same control point information or control point-based vertex element information.
            Dim planes As Mesh() = PolygonModifier.SplitMesh(box, SplitMeshPolicy.CloneData)

            mat.Indices.Clear()
            mat.Indices.AddRange(New Integer() {0, 0, 0, 1, 1, 1})
            ' Now split it into 2 sub meshes, first mesh will contains 0/1/2 planes, and second mesh will contains the 3/4/5th planes.
            ' We used the CompactData policy, each plane will has its own control point information or control point-based vertex element information.
            planes = PolygonModifier.SplitMesh(box, SplitMeshPolicy.CompactData)

            ' ExEnd:SplitMeshbyMaterial
            Console.WriteLine(vbLf & "Spliting a mesh by specifying the material successfully.")
        End Sub
    End Class
End Namespace