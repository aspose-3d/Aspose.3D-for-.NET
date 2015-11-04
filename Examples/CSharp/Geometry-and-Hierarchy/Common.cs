//////////////////////////////////////////////////////////////////////////
// Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.3D. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////
using System;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utils;

namespace CSharp.Geometry_Hierarchy
{
    class Common
    {
        public static Mesh CreateMesh()
        {
            // Initialize control points
            Vector4[] controlPoints = new Vector4[]{
    new Vector4( -5.0, 0.0, 5.0, 1.0),
    new Vector4( 5.0, 0.0, 5.0, 1.0),
    new Vector4( 5.0, 10.0, 5.0, 1.0),
    new Vector4( -5.0, 10.0, 5.0, 1.0),
    new Vector4( -5.0, 0.0, -5.0, 1.0),
    new Vector4( 5.0, 0.0, -5.0, 1.0),
    new Vector4( 5.0, 10.0, -5.0, 1.0),
    new Vector4( -5.0, 10.0, -5.0, 1.0)
};
            // Initialize mesh object
            Mesh mesh = new Mesh();

            // Add control points to the mesh
            mesh.ControlPoints.AddRange(controlPoints);

            // Indices of the vertices per each polygon
            int[] indices = new int[]{
    0,1,2,3, // front face (Z+)
    1,5,6,2, // right side (X+)
    5,4,7,6, // back face (Z-)
    4,0,3,7, // left side (X-)
    0,4,5,1, // bottom face (Y-)
    3,2,6,7 // top face (Y+)
};

            int vertexId = 0;
            PolygonBuilder builder = new PolygonBuilder(mesh);
            for (int face = 0; face < 6; face++)
            {
                // Start defining a new polygon
                builder.Begin();
                for (int v = 0; v < 4; v++)
                    // The indice of vertice per each polygon
                    builder.AddVertex(indices[vertexId++]);
                // Finished one polygon
                builder.End();
            }

            return mesh;
        }
    }
}
