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
        }
        public static void Discreet3DSLoadOption()
        {
            // ExStart:Discreet3DSOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            Discreet3DSLoadOptions loadOpts = new Discreet3DSLoadOptions();
            // Sets wheather to use the transformation defined in the first frame of animation track.
            loadOpts.ApplyAnimationTransform = true;
            // Flip the coordinate system
            loadOpts.FlipCoordinateSystem = true;
            // Prefer to use gamma-corrected color if a 3ds file provides both original color and gamma-corrected color.
            loadOpts.GammaCorrectedColor = true;
            // Configure the look up paths to allow importer to find external dependencies.
            loadOpts.LookupPaths = new List<string>(new string[] { MyDir });
            // ExEnd:Discreet3DSOption
        }
        public static void ObjLoadOption()
        {
            // ExStart:ObjLoadOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize an object
            ObjLoadOptions loadObjOpts = new ObjLoadOptions();
            // Import materials from external material library file
            loadObjOpts.EnableMaterials = true;
            // Flip the coordinate system.
            loadObjOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            loadObjOpts.LookupPaths = new List<string>(new string[] { MyDir});
            // ExEnd:ObjLoadOption
        }
        public static void STLLoadOption()
        {
            // ExStart:STLLoadOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize an object
            STLLoadOptions loadSTLOpts = new STLLoadOptions();
            // Flip the coordinate system.
            loadSTLOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            loadSTLOpts.LookupPaths = new List<string>(new string[] { MyDir });
            // ExEnd:STLLoadOption
        }
        public static void U3DLoadOption()
        {
            // ExStart:U3DLoadOption
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize an object
            U3DLoadOptions loadU3DOpts = new U3DLoadOptions();
            // Flip the coordinate system.
            loadU3DOpts.FlipCoordinateSystem = true;
            // Configure the look up paths to allow importer to find external dependencies.
            loadU3DOpts.LookupPaths = new List<string>(new string[] { MyDir });
            // ExEnd:U3DLoadOption
        }
        public static void glTFLoadOptions()
        {
            // ExStart:glTFLoadOptions
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // Initialize Scene class object
            Scene scene = new Scene();
            // Set load options
            GLTFLoadOptions loadOpt = new GLTFLoadOptions();
            // The default value is true, usually we don't need to change it. Aspose.3D will automatically flip the V/T texture coordinate during load and save.       
            loadOpt.FlipTexCoordV = true;
            scene.Open( MyDir + "Duck.gltf", loadOpt);
            // ExEnd:glTFLoadOptions
        }
        public static void PlyLoadOptions()
        {
            // ExStart:PlyLoadOptions
            // the path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // initialize Scene class object
            Scene scene = new Scene();
            // initialize an object
            PlyLoadOptions loadPLYOpts = new PlyLoadOptions();
            // Flip the coordinate system.
            loadPLYOpts.FlipCoordinateSystem = true;
            // load 3D Ply model
            scene.Open(MyDir + "vase-v2.ply", loadPLYOpts);
            // ExEnd:PlyLoadOptions
        }
        public static void XLoadOptions()
        {
            // ExStart:XLoadOptions
            // the path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // initialize Scene class object
            Scene scene = new Scene();
            // initialize an object
            XLoadOptions loadXOpts = new XLoadOptions(FileContentType.ASCII);
            // flip the coordinate system.
            loadXOpts.FlipCoordinateSystem = true;
            // load 3D X file
            scene.Open(MyDir + "warrior.x", loadXOpts);
            // ExEnd:XLoadOptions
        } 
    }
}
