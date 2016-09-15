using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class SaveOptions
    {
        public static void Run()
        {
            ColladaSaveOption();
            Discreet3DSSaveOption();
            FBXSaveOption();
            ObjSaveOption();
        }
        public static void ColladaSaveOption()
        {
            //ExStart:ColladaSaveOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            ColladaSaveOptions saveColladaopts = new ColladaSaveOptions();
            // Generates indented XML document
            saveColladaopts.Indented = true;
            // The style of node transformation
            saveColladaopts.TransformStyle = ColladaTransformStyle.Matrix;
            // Configure the lookup paths to allow importer to find external dependencies.
            saveColladaopts.LookupPaths = new List<string>(new string[] { MyDir });
            //ExEnd:ColladaSaveOption
        }
        public static void Discreet3DSSaveOption()
        {
            //ExStart:Discreet3DSSaveOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize an object
            Discreet3DSSaveOptions saveOpts = new Discreet3DSSaveOptions();
            // The start base for generating new name for duplicated names.
            saveOpts.DuplicatedNameCounterBase = 2;
            // The format of the duplicated counter.
            saveOpts.DuplicatedNameCounterFormat = "NameFormat";
            // The separator between object's name and the duplicated counter.
            saveOpts.DuplicatedNameSeparator = "Separator";
            // Allows to export cameras
            saveOpts.ExportCamera = true;
            // Allows to export light
            saveOpts.ExportLight = true;
            // Flip the coordinate system
            saveOpts.FlipCoordinateSystem = true;
            // Prefer to use gamma-corrected color if a 3ds file provides both original color and gamma-corrected color.
            saveOpts.GammaCorrectedColor = true;
            // Use high-precise color which each color channel will use 32bit float.
            saveOpts.HighPreciseColor = true;
            // Configure the look up paths to allow importer to find external dependencies.
            saveOpts.LookupPaths = new List<string>(new string[] {MyDir});
            // Set the master scale
            saveOpts.MasterScale = 1;
            //ExEnd:Discreet3DSSaveOption
        }
        public static void FBXSaveOption()
        {
            //ExStart:FBXSaveOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize an object
            FBXSaveOptions saveOpts = new FBXSaveOptions(FileFormat.FBX7500ASCII);
            // Generates the legacy material properties.
            saveOpts.ExportLegacyMaterialProperties = true;
            // Fold repeated curve data using FBX's animation reference count
            saveOpts.FoldRepeatedCurveData = true;
            // Always generates material mapping information for geometries if the attached node contains materials.
            saveOpts.GenerateVertexElementMaterial = true;
            // Configure the look up paths to allow importer to find external dependencies.
            saveOpts.LookupPaths = new List<string>(new string[] { MyDir });
            // Generates a video object for texture.
            saveOpts.VideoForTexture = true;
            //ExEnd:FBXSaveOption
        }
        public static void ObjSaveOption()
        {
            //ExStart:ObjSaveOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize an object
            ObjSaveOptions saveObjOpts = new ObjSaveOptions();
            // Import materials from external material library file
            saveObjOpts.EnableMaterials = true;
            // Flip the coordinate system.
            saveObjOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            saveObjOpts.LookupPaths = new List<string>(new string[] { MyDir });
            // Serialize W component in model's vertex position
            saveObjOpts.SerializeW = true;
            // Generate comments for each section
            saveObjOpts.Verbose = true;
            //ExEnd:ObjSaveOption
        }
        public static void STLSaveOption()
        {
            //ExStart:STLSaveOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize an object
            STLSaveOptions saveSTLOpts = new STLSaveOptions();
            // Flip the coordinate system.
            saveSTLOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            saveSTLOpts.LookupPaths = new List<string>(new string[] {MyDir });
            //ExEnd:STLSaveOption
        }
        public static void U3DSaveOption()
        {
            //ExStart:U3DSaveOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize an object
            U3DSaveOptions saveU3DOptions = new U3DSaveOptions();
            // Export normal data.
            saveU3DOptions.ExportNormals = true;
            // Export the texture coordinates.
            saveU3DOptions.ExportTextureCoordinates = true;
            // Export the vertex diffuse color.
            saveU3DOptions.ExportVertexDiffuse = true;
            // Export vertex specular color
            saveU3DOptions.ExportVertexSpecular = true;
            // Flip the coordinate system.
            saveU3DOptions.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            saveU3DOptions.LookupPaths = new List<string>(new string[] { MyDir });
            // Compress the mesh data
            saveU3DOptions.MeshCompression = true;
            //ExEnd:U3DSaveOption
        } 
    }
}
