using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;

namespace CSharp._Working_with_Objects
{
    class SplitMeshbyMaterial
    {
        public static void Run()
        {
            //ExStart:SplitMeshbyMaterial

            //Create a mesh of box(A box is composed by 6 planes)
            Mesh box = (new Box()).ToMesh();
            //create a material element on this mesh
            VertexElementMaterial mat = (VertexElementMaterial)box.CreateElement(VertexElementType.Material, MappingMode.Polygon, ReferenceMode.Index);
            //And specify different material index for each plane
            mat.Indices.AddRange(new int[] { 0, 1, 2, 3, 4, 5 });
            //now split it into 6 sub meshes, we specified 6 different materials on each plane, each plane will become a sub mesh.
            //We used the CloneData policy, each plane will has the same control point information or control point-based vertex element information.
            Mesh[] planes = PolygonModifier.SplitMesh(box, SplitMeshPolicy.CloneData);

            mat.Indices.Clear();
            mat.Indices.AddRange(new int[] { 0, 0, 0, 1, 1, 1 });
            //now split it into 2 sub meshes, first mesh will contains 0/1/2 planes, and second mesh will contains the 3/4/5th planes.
            //We used the CompactData policy, each plane will has its own control point information or control point-based vertex element information.
            planes = PolygonModifier.SplitMesh(box, SplitMeshPolicy.CompactData);

            //ExEnd:SplitMeshbyMaterial
            Console.WriteLine("\nSpliting a mesh by specifying the material successfully.");
        }
    }
}
