using System;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.Geometry_Hierarchy
{
    class Common
    {
        private static Vector4[] DefineControlPoints()
        {
            // ExStart:DefineControlPoints
            // Initialize control points
            Vector4[] controlPoints = new Vector4[]
            {
                new Vector4( -5.0, 0.0, 5.0, 1.0),
                new Vector4( 5.0, 0.0, 5.0, 1.0),
                new Vector4( 5.0, 10.0, 5.0, 1.0),
                new Vector4( -5.0, 10.0, 5.0, 1.0),
                new Vector4( -5.0, 0.0, -5.0, 1.0),
                new Vector4( 5.0, 0.0, -5.0, 1.0),
                new Vector4( 5.0, 10.0, -5.0, 1.0),
                new Vector4( -5.0, 10.0, -5.0, 1.0)
            };
            // ExEnd:DefineControlPoints
            
            return controlPoints;
        }
        public static Mesh CreateMeshUsingPolygonBuilder()
        {
            // ExStart:CreateMeshUsingPolygonBuilder           
            Vector4[] controlPoints = DefineControlPoints();
            
            // Initialize mesh object
            Mesh mesh = new Mesh();

            // Add control points to the mesh
            mesh.ControlPoints.AddRange(controlPoints);
            
            // Indices of the vertices per each polygon
            int[] indices = new int[]
            {
                0,1,2,3, // Front face (Z+)
                1,5,6,2, // Right side (X+)
                5,4,7,6, // Back face (Z-)
                4,0,3,7, // Left side (X-)
                0,4,5,1, // Bottom face (Y-)
                3,2,6,7 // Top face (Y+)
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

            // ExEnd:CreateMeshUsingPolygonBuilder
            return mesh;
        }
        public static Mesh CreateMeshUsingCreatePolygons()
        {
            // ExStart:CreateMeshUsingCreatePolygons           
            Vector4[] controlPoints = DefineControlPoints();
            
            // Initialize mesh object
            Mesh mesh = new Mesh();
            // Add control points to the mesh
            mesh.ControlPoints.AddRange(controlPoints);
            // Create polygons to mesh
            // Front face (Z+)
            mesh.CreatePolygon(new int[] { 0, 1, 2, 3 });
            // Right side (X+)
            mesh.CreatePolygon(new int[] { 1, 5, 6, 2 });
            // Back face (Z-)
            mesh.CreatePolygon(new int[] { 5, 4, 7, 6 });
            // Left side (X-)
            mesh.CreatePolygon(new int[] { 4, 0, 3, 7 });
            // Bottom face (Y-)
            mesh.CreatePolygon(new int[] { 0, 4, 5, 1 });
            // Top face (Y+)
            mesh.CreatePolygon(new int[] { 3, 2, 6, 7 });
            // ExEnd:CreateMeshUsingCreatePolygons
            return mesh;
        }
    }
}
