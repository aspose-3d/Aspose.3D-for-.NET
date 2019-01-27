using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;
using Aspose.ThreeD.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp._3DScene
{
    public class ExportSceneToCompressedAMF
    {
        public static void Run()
        {

/*
    /// <summary>
    /// Save options for AMF
    /// </summary>
        public class AMFSaveOptions : SaveOptions
        {
            /// <summary>
            /// Whether to use compression to reduce the final file size, default value is true
            /// </summary>
            public bool EnableCompression { get; set; }
        }
*/

        // ExStart:CompressedAMF
        // The path to the documents directory.
        string MyDir = RunExamples.GetDataDir();
            // Load a scene
            Scene scene = new Scene();
            var box = new Box();
            var tr = scene.RootNode.CreateChildNode(box).Transform;
            tr.Scale = new Vector3(12, 12, 12);
            tr.Translation = new Vector3(10, 0, 0);
            tr = scene.RootNode.CreateChildNode(box).Transform;
            // Scale transform
            tr.Scale = new Vector3(5, 5, 5);
            // Set Euler Angles
            tr.EulerAngles = new Vector3(50, 10, 0);
            scene.RootNode.CreateChildNode();
            scene.RootNode.CreateChildNode().CreateChildNode(box);
            scene.RootNode.CreateChildNode().CreateChildNode(box);
            // Save compressed AMF file
            scene.Save(MyDir + "Aspose.amf", new AMFSaveOptions() { EnableCompression = false });
            // ExEnd:CompressedAMF
        }
    }
}
