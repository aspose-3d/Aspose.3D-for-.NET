using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;

namespace Aspose._3D.Examples.CSharp.Loading_Saving
{
    class LoadOptions
    {
        public static void Run()
        {
            Discreet3DSLoadOption();
            ObjLoadOption();
            STLLoadOption();
            U3DLoadOption();
            glTFLoadOptions();
            PlyLoadOptions();
            FBXLoadOptions();
        }
        private static void Discreet3DSLoadOption()
        {
            // ExStart:Discreet3DSOption
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            Discreet3dsLoadOptions loadOpts = new Discreet3dsLoadOptions();
            // Sets wheather to use the transformation defined in the first frame of animation track.
            loadOpts.ApplyAnimationTransform = true;
            // Flip the coordinate system
            loadOpts.FlipCoordinateSystem = true;
            // Prefer to use gamma-corrected color if a 3ds file provides both original color and gamma-corrected color.
            loadOpts.GammaCorrectedColor = true;
            // Configure the look up paths to allow importer to find external dependencies.
            loadOpts.LookupPaths = new List<string>(new string[] { dataDir });
            // ExEnd:Discreet3DSOption
        }
        private static void ObjLoadOption()
        {
            // ExStart:ObjLoadOption
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize an object
            ObjLoadOptions loadObjOpts = new ObjLoadOptions();
            // Import materials from external material library file
            loadObjOpts.EnableMaterials = true;
            // Flip the coordinate system.
            loadObjOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            loadObjOpts.LookupPaths = new List<string>(new string[] { dataDir});
            // ExEnd:ObjLoadOption
        }
        private static void STLLoadOption()
        {
            // ExStart:STLLoadOption
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize an object
            StlLoadOptions loadSTLOpts = new StlLoadOptions();
            // Flip the coordinate system.
            loadSTLOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            loadSTLOpts.LookupPaths = new List<string>(new string[] { dataDir });
            // ExEnd:STLLoadOption
        }
        private static void U3DLoadOption()
        {
            // ExStart:U3DLoadOption
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize an object
            U3dLoadOptions loadU3DOpts = new U3dLoadOptions();
            // Flip the coordinate system.
            loadU3DOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            loadU3DOpts.LookupPaths = new List<string>(new string[] { dataDir });
            // ExEnd:U3DLoadOption
        }
        private static void glTFLoadOptions()
        {
            // ExStart:glTFLoadOptions
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // Initialize Scene class object
            Scene scene = new Scene();
            // Set load options
            GltfLoadOptions loadOpt = new GltfLoadOptions();
            // The default value is true, usually we don't need to change it. Aspose.3D will automatically flip the V/T texture coordinate during load and save.       
            loadOpt.FlipTexCoordV = true;
            scene.Open( dataDir + "Duck.gltf", loadOpt);
            // ExEnd:glTFLoadOptions
        }
        private static void PlyLoadOptions()
        {
            // ExStart:PlyLoadOptions
            // the path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // initialize Scene class object
            Scene scene = new Scene();
            // initialize an object
            PlyLoadOptions loadPLYOpts = new PlyLoadOptions();
            // Flip the coordinate system.
            loadPLYOpts.FlipCoordinateSystem = true;
            // load 3D Ply model
            scene.Open(RunExamples.GetDataFilePath("vase-v2.ply"), loadPLYOpts);
            // ExEnd:PlyLoadOptions
        }
        private static void XLoadOptions()
        {
            // ExStart:XLoadOptions
            // the path to the documents directory.
            string dataDir = RunExamples.GetDataDir();
            // initialize Scene class object
            Scene scene = new Scene();
            // initialize an object
            XLoadOptions loadXOpts = new XLoadOptions(FileContentType.ASCII);
            // flip the coordinate system.
            loadXOpts.FlipCoordinateSystem = true;
            // load 3D X file
            scene.Open(RunExamples.GetDataFilePath("warrior.x"), loadXOpts);
            // ExEnd:XLoadOptions
        }
        private static void FBXLoadOptions()
        {
            //ExStart: FBXLoadOptions
            string dataDir = RunExamples.GetDataDir();
            //This will output all properties defined in GlobalSettings in FBX file.
            Scene scene = new Scene();
            var opt = new FbxLoadOptions() { KeepBuiltinGlobalSettings = true };
            scene.Open(dataDir + "test.FBX", opt);
            foreach (Property property in scene.RootNode.AssetInfo.Properties)
            {
                Console.WriteLine(property);
            }
            //ExEnd: FBXLoadOptions
        }
    }
}
