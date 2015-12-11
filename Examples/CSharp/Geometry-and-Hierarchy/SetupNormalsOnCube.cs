//////////////////////////////////////////////////////////////////////////
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
    class SetupNormalsOnCube
    {
        public static void Run()
        {
            //ExStart:SetupNormalsOnCube
            // Raw normal data
            Vector4[] normals = new Vector4[]
            {
                new Vector4(-0.577350258,-0.577350258, 0.577350258, 1.0),
                new Vector4( 0.577350258,-0.577350258, 0.577350258, 1.0),
                new Vector4( 0.577350258, 0.577350258, 0.577350258, 1.0),
                new Vector4(-0.577350258, 0.577350258, 0.577350258, 1.0),
                new Vector4(-0.577350258,-0.577350258,-0.577350258, 1.0),
                new Vector4( 0.577350258,-0.577350258,-0.577350258, 1.0),
                new Vector4( 0.577350258, 0.577350258,-0.577350258, 1.0),
                new Vector4(-0.577350258, 0.577350258,-0.577350258, 1.0)
            };

            // Call Common class create mesh using polygon builder method to set mesh instance 
            Mesh mesh = Common.CreateMeshUsingPolygonBuilder(); 

            VertexElementNormal elementNormal = mesh.CreateElement(VertexElementType.Normal) as VertexElementNormal;
            // Specify normal per control point.
            elementNormal.MappingMode = MappingMode.ControlPoint;
            // The data is directly referenced.
            elementNormal.ReferenceMode = ReferenceMode.Direct;
            // Copy the data to the vertex element
            elementNormal.Data.AddRange(normals);
            //ExEnd:SetupNormalsOnCube

            Console.WriteLine("\nNormals has been setup successfully on cube.");
        }
    }
}
