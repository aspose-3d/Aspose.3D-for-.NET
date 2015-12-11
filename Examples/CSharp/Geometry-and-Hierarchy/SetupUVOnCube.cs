﻿//////////////////////////////////////////////////////////////////////////
// Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.3D. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utils;

namespace CSharp.Geometry_Hierarchy
{
    class SetupUVOnCube
    {
        public static void Run()

        {
            //ExStart:SetupUVOnCube
            // UVs
            Vector2[] uvs = new Vector2[]
            {
                new Vector2( 0.0, 1.0),
                new Vector2( 1.0, 0.0),
                new Vector2( 0.0, 0.0),
                new Vector2( 1.0, 1.0)
            };

            // Indices of the uvs per each polygon
            int[] uvsId = new int[]
            {
                0,1,3,2,2,3,5,4,4,5,7,6,6,7,9,8,1,10,11,3,12,0,2,13
            };

            // Call Common class create mesh using polygon builder method to set mesh instance 
            Mesh mesh = Common.CreateMeshUsingPolygonBuilder();

            // Create UVset
            VertexElementUV elementUV = mesh.CreateElementUV(TextureMapping.Diffuse);
            elementUV.MappingMode = MappingMode.PolygonVertex;
            elementUV.ReferenceMode = ReferenceMode.IndexToDirect;
            elementUV.Data.AddRange(uvs);
            elementUV.Indices.AddRange(uvsId);
            //ExEnd:SetupUVOnCube

            Console.WriteLine("\nUVs has been setup successfully on cube.");
        }
    }
}
