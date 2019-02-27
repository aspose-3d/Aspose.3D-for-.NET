using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Shading;

namespace Aspose._3D.Examples.CSharp.Materials
{
    class CreateSceneWithEmbeddedTexture
    {
        public static void Run()
        {

            //Create a FBX file with embedded textures
            Scene scene = new Scene();

            //Create an embedded texture
            Texture tex = new Texture()
            {
                Content = CreateTextureContent(),
                //file name is required if the embedded texture is used.
                FileName = "test.png"
            };
            tex.SetProperty("TexProp", "value");
            //create a material with custom property
            LambertMaterial mat = new LambertMaterial("my-mat");
            mat.SetTexture(Material.MapDiffuse, tex);
            mat.SetProperty("MyProp", 1.0);

            //create a torus with this material applied
            scene.RootNode.CreateChildNode(new Torus()).Material = mat;
            //save this to file
            scene.Save(RunExamples.GetOutputFilePath(@"test.fbx"), FileFormat.FBX7500ASCII);
        }

        private static byte[] CreateTextureContent()
        {
            using (var bitmap = new Bitmap(256, 256))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);
                    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 128, 128), Color.Moccasin,
                        Color.ForestGreen, 45);
                    using (var font = new Font(FontFamily.GenericSerif, 40))
                    {
                        g.DrawString("Aspose.3D", font, brush, Point.Empty);
                    }

                }
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }

        }
    }
}
