using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;
using Aspose.ThreeD.Shading;
using Aspose.ThreeD.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aspose._3D.Examples.CSharp.Loading_and_Saving
{
    class Non_PBRtoPBRMaterial
    {
        public static void Run()
        {
            // ExStart:Non_PBRtoPBRMaterial
            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir();
            // initialize a new 3D scene
            var s = new Scene();
            var box = new Box();
            s.RootNode.CreateChildNode("box1", box).Material = new PhongMaterial() { DiffuseColor = new Vector3(1, 0, 1) };
            GLTFSaveOptions opt = new GLTFSaveOptions(FileFormat.GLTF2);
            //Custom material converter to convert PhongMaterial to PbrMaterial
            opt.MaterialConverter = delegate (Material material)
            {
                PhongMaterial m = (PhongMaterial)material;
                return new PbrMaterial() { Albedo = new Vector3(m.DiffuseColor.x, m.DiffuseColor.y, m.DiffuseColor.z) };
            };
            // save in GLTF 2.0 format
            s.Save(MyDir + "Non_PBRtoPBRMaterial_Out.gltf", opt);
            // ExEnd:Non_PBRtoPBRMaterial
        }
    }
}
