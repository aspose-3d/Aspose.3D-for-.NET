using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;
using Aspose.ThreeD.Shading;

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
            GlTFSaveOptions();
            DRCSaveOptions();
        }
        public static void ColladaSaveOption()
        {
            // ExStart:ColladaSaveOption
            string dataDir = RunExamples.GetDataDir();
            ColladaSaveOptions saveColladaopts = new ColladaSaveOptions();
            // Generates indented XML document
            saveColladaopts.Indented = true;
            // The style of node transformation
            saveColladaopts.TransformStyle = ColladaTransformStyle.Matrix;
            // Configure the lookup paths to allow importer to find external dependencies.
            saveColladaopts.LookupPaths = new List<string>(new string[] { dataDir });
            // ExEnd:ColladaSaveOption
        }
        public static void Discreet3DSSaveOption()
        {
            // ExStart:Discreet3DSSaveOption
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
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
            saveOpts.LookupPaths = new List<string>(new string[] {dataDir});
            // Set the master scale
            saveOpts.MasterScale = 1;
            // ExEnd:Discreet3DSSaveOption
        }
        public static void FBXSaveOption()
        {
            // ExStart:FBXSaveOption
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize an object
            FBXSaveOptions saveOpts = new FBXSaveOptions(FileFormat.FBX7500ASCII);
            // Generates the legacy material properties.
            saveOpts.ExportLegacyMaterialProperties = true;
            // Fold repeated curve data using FBX's animation reference count
            saveOpts.FoldRepeatedCurveData = true;
            // Always generates material mapping information for geometries if the attached node contains materials.
            saveOpts.GenerateVertexElementMaterial = true;
            // Configure the look up paths to allow importer to find external dependencies.
            saveOpts.LookupPaths = new List<string>(new string[] { dataDir });
            // Generates a video object for texture.
            saveOpts.VideoForTexture = true;
            // ExEnd:FBXSaveOption
        }
        public static void ObjSaveOption()
        {
            // ExStart:ObjSaveOption
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize an object
            ObjSaveOptions saveObjOpts = new ObjSaveOptions();
            // Import materials from external material library file
            saveObjOpts.EnableMaterials = true;
            // Flip the coordinate system.
            saveObjOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            saveObjOpts.LookupPaths = new List<string>(new string[] { dataDir });
            // Serialize W component in model's vertex position
            saveObjOpts.SerializeW = true;
            // Generate comments for each section
            saveObjOpts.Verbose = true;
            // ExEnd:ObjSaveOption
        }
        public static void STLSaveOption()
        {
            // ExStart:STLSaveOption
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize an object
            STLSaveOptions saveSTLOpts = new STLSaveOptions();
            // Flip the coordinate system.
            saveSTLOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            saveSTLOpts.LookupPaths = new List<string>(new string[] {dataDir });
            // ExEnd:STLSaveOption
        }
        public static void U3DSaveOption()
        {
            // ExStart:U3DSaveOption
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
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
            saveU3DOptions.LookupPaths = new List<string>(new string[] { dataDir });
            // Compress the mesh data
            saveU3DOptions.MeshCompression = true;
            // ExEnd:U3DSaveOption
        }
        public static void GlTFSaveOptions()
        {
            // ExStart:glTFSaveOptions
            // Initialize Scene object
            Scene scene = new Scene();
            // Create a child node
            scene.RootNode.CreateChildNode("sphere", new Sphere());
            // Set glTF saving options. The code example embeds all assets into the target file usually a glTF file comes with some dependencies, a bin file for model's vertex/indices, two .glsl files for vertex/fragment shaders
            // Use opt.EmbedAssets to tells the Aspose.3D API to export scene and embed the dependencies inside the target file.
            GLTFSaveOptions opt = new GLTFSaveOptions(FileContentType.ASCII);
            opt.EmbedAssets = true;
            // Use KHR_materials_common extension to define the material, thus no GLSL files are generated.
            opt.UseCommonMaterials = true;
            // Customize the name of the buffer file which defines model
            opt.BufferFile = "mybuf.bin";
            //The JSON content of GLTF file is indented for human reading
            opt.PrettyPrint = true;
            // Save GlTF file
            scene.Save(RunExamples.GetOutputFilePath("glTFSaveOptions_out.gltf"), opt);

            // Save a binary glTF file using KHR_binary_glTF extension
            scene.Save(RunExamples.GetOutputFilePath("glTFSaveOptions_out.glb"), FileFormat.GLTF_Binary);

            // Developers may use saving options to create a binary glTF file using KHR_binary_glTF extension
            GLTFSaveOptions opts = new GLTFSaveOptions(FileContentType.Binary);
            scene.Save(RunExamples.GetOutputFilePath("Test_out.glb"), opts);
            // ExEnd:glTFSaveOptions
        }
        public static void DRCSaveOptions()
        {
            // ExStart:DRCSaveOptions
            // Initialize Scene object
            Scene scene = new Scene();
            // Create a child node
            scene.RootNode.CreateChildNode("sphere", new Sphere());
            // Initialize .DRC saving options. 
            DracoSaveOptions opts = new DracoSaveOptions();
            // Quantization bits for position
            opts.PositionBits = 14;
            // Quantization bits for texture coordinate
            opts.TextureCoordinateBits = 8;
            // Quantization bits for vertex color
            opts.ColorBits = 10;
            // Quantization bits for normal vectors
            opts.NormalBits = 7;
            // Set compression level
            opts.CompressionLevel = DracoCompressionLevel.Optimal;

            // Save Google Draco (.drc) file
            scene.Save(RunExamples.GetOutputFilePath("DRCSaveOptions_out.drc"), opts);
            // ExEnd:DRCSaveOptions
        }
        public static void DiscardSavingMaterial()
        {
            // ExStart:DiscardSavingMaterial
            // The code example uses the DummyFileSystem, so the material files are not created.
            // Initialize Scene object
            Scene scene = new Scene();
            // Create a child node
            scene.RootNode.CreateChildNode("sphere", new Sphere()).Material = new PhongMaterial();
            // Set saving options
            ObjSaveOptions opt = new ObjSaveOptions();
            opt.FileSystem = new DummyFileSystem();
            // Save 3D scene
            scene.Save(RunExamples.GetOutputFilePath("DiscardSavingMaterial_out.obj"), opt);
            // ExEnd:DiscardSavingMaterial
        }
        public static void SavingDependenciesInLocalDirectory()
        {
            // ExStart:SavingDependenciesInLocalDirectory
            // The code example uses the LocalFileSystem class to save dependencies to the local directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize Scene object
            Scene scene = new Scene();            
            // Create a child node
            scene.RootNode.CreateChildNode("sphere", new Sphere()).Material = new PhongMaterial();
            // Set saving options
            ObjSaveOptions opt = new ObjSaveOptions();
            opt.FileSystem = new LocalFileSystem(dataDir);
            // Save 3D scene
            scene.Save(RunExamples.GetOutputFilePath("SavingDependenciesInLocalDirectory_out.obj"), opt);
            // ExEnd:SavingDependenciesInLocalDirectory
        }
        public static void SavingDependenciesInMemoryFileSystem()
        {
            // ExStart:SavingDependenciesInMemoryFileSystem
            // The code example uses the MemoryFileSystem to intercepts the dependencies writing.
            // Initialize Scene object
            Scene scene = new Scene();
            // Create a child node
            scene.RootNode.CreateChildNode("sphere", new Sphere()).Material = new PhongMaterial();
            // Set saving options
            ObjSaveOptions opt = new ObjSaveOptions();
            MemoryFileSystem mfs = new MemoryFileSystem();
            opt.FileSystem = mfs;
            // Save 3D scene
            scene.Save(RunExamples.GetOutputFilePath( "SavingDependenciesInMemoryFileSystem_out.obj"), opt);
            // Get the test.mtl file content
            byte[] mtl = mfs.GetFileContent("SavingDependenciesInMemoryFileSystem_out.mtl");
            File.WriteAllBytes( RunExamples.GetOutputFilePath("Material.mtl"), mtl);
            // ExEnd:SavingDependenciesInMemoryFileSystem
        }
    }
}
