using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.Working_with_Objects
{
    public class CreatePolygonInMesh
    {
        /// <summary>
        /// Create a polygon with 3 vertices(triangle) or 4 vertices(quad)
        /// </summary>
        public static void Run()
        {
            // ExStart:1
            Mesh mesh = new Mesh();
            mesh.CreatePolygon(new int[] { 0, 1, 2 }); //The old CreatePolygon needs to create a temporary array for holding the face indices
            mesh.CreatePolygon(0, 1, 2); //The new overloads doesn't need extra allocation, and it's optimized internally.

            //Or You can create a polygon using 4 vertices(quad)
            //mesh.CreatePolygon(0, 1, 2, 3);
            // ExEnd:1              
        }
    }
}
