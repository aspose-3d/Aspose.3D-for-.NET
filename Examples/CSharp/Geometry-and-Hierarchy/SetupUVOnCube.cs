using System;
using System.Collections.Generic;
using System.IO;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;

namespace Aspose._3D.Examples.CSharp.Geometry_Hierarchy
{
    class SetupUVOnCube
    {
        public static void Run()

        {
            //ExStart:SetupUVOnCube
            // UVs
            Vector4[] uvs = new Vector4[]
            {
                new Vector4( 0.0, 1.0,0.0, 1.0),
                new Vector4( 1.0, 0.0,0.0, 1.0),
                new Vector4( 0.0, 0.0,0.0, 1.0),
                new Vector4( 1.0, 1.0,0.0, 1.0)
            };

            // Indices of the uvs per each polygon
            int[] uvsId = new int[]
            {
                0,1,3,2,2,3,5,4,4,5,7,6,6,7,9,8,1,10,11,3,12,0,2,13
            };

            // Call Common class create mesh using polygon builder method to set mesh instance 
            Mesh mesh = Common.CreateMeshUsingPolygonBuilder();

            // Create UVset
            VertexElementUV elementUV = mesh.CreateElementUV(TextureMapping.Diffuse, MappingMode.PolygonVertex, ReferenceMode.IndexToDirect);
            // Copy the data to the UV vertex element 
            elementUV.Data.AddRange(uvs);
            elementUV.Indices.AddRange(uvsId);
            //ExEnd:SetupUVOnCube

            Console.WriteLine("\nUVs has been setup successfully on cube.");
        }
    }
}
