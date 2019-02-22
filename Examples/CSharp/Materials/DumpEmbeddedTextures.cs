using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.ThreeD;
using Aspose.ThreeD.Shading;

namespace Aspose._3D.Examples.CSharp.Materials
{
    class DumpEmbeddedTextures
    {
        public static void Run()
        {

            Scene scene = new Scene(RunExamples.GetDataDir() + "EmbeddedTexture.fbx");
            var mat = (LambertMaterial)scene.RootNode.ChildNodes[0].Material;
            Console.WriteLine("Material {0}'s information:", mat.Name);
            Console.WriteLine("\tDiffuse color = {0}", mat.DiffuseColor);
            Console.WriteLine("\tAmbient color = {0}", mat.AmbientColor);
            Console.WriteLine("\tEmissive color = {0}", mat.EmissiveColor);
            Console.WriteLine("\tTransparency = {0}", mat.Transparency);
            Console.WriteLine("\tTransparent color = {0}", mat.TransparentColor);
            Console.WriteLine("\tCustom prop `MyProp` = {0}", mat.GetProperty("MyProp"));
            Console.WriteLine();
            //dump textures
            var tex = (Texture)mat.GetTexture(Material.MapDiffuse);
            Console.WriteLine("Texture {0}'s information:", tex.Name);
            Console.WriteLine("File name = {0}", tex.FileName);
            Console.WriteLine("Custom prop `TexProp` = {0}", tex.GetProperty("TexProp"));
            if(tex.Content != null)
                File.WriteAllBytes("texture.png", tex.Content);
        }
    }
}
