using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.Polygons
{
    public class GenerateUV
    {
        public static void Run()
        {

/*
            /// <summary>
            /// Generate UV data from the given input mesh and specified normal data.
            /// </summary>
            /// <param name="mesh">The input mesh</param>
            /// <param name="normals">The normal data</param>
            /// <returns>Generated UV data</returns>
            public static VertexElementUV GenerateUV(Mesh mesh, VertexElementNormal normals);
            /// <summary>
            /// Generate UV data from the given input mesh
            /// </summary>
            /// <param name="mesh">The input mesh</param>
            /// <returns>Generated UV data</returns>
            public static VertexElementUV GenerateUV(Mesh mesh);
*/
            // ExStart:GenerateUV
            Scene scene = new Scene();
            //since all primitive entities in Aspose.3D will have builtin UV generation
            //here we manually remove it to assume we have a mesh without UV data
            var mesh = (new Box()).ToMesh();
            mesh.VertexElements.Remove(mesh.GetElement(VertexElementType.UV));
            //then we can manually generate UV for it
            var uv = PolygonModifier.GenerateUV(mesh);
            //generated UV data is not associated with the mesh, we should manually do this
            mesh.AddElement(uv);
            //put it to the scene
            var node = scene.RootNode.CreateChildNode(mesh);
            //then save it
            scene.Save(RunExamples.GetOutputFilePath("Aspose.obj"), FileFormat.WavefrontOBJ);
            // ExEnd:GenerateUV
        }
    }
}
