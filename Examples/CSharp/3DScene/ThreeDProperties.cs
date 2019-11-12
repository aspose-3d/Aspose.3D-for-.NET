using Aspose.ThreeD;
using Aspose.ThreeD.Shading;
using Aspose.ThreeD.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp._3DScene
{
    public class ThreeDProperties
    {
        public static void Run()
        {
            //ExStart: ThreeDProperties
            string dataDir = RunExamples.GetDataDir();
            Scene scene = new Scene(dataDir+ "EmbeddedTexture.fbx");
            Material material = scene.RootNode.ChildNodes[0].Material;
            PropertyCollection props = material.Properties;
            //List all properties using foreach
            foreach (var prop in props)
            {
                Console.WriteLine("{0} = {1}", prop.Name, prop.Value);
            }
            //or using ordinal for loop
            for (int i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                Console.WriteLine("{0} = {1}", prop.Name, prop.Value);
            }
            //Get property value by name
            var diffuse = props["Diffuse"];
            Console.WriteLine(diffuse);
            //modify property value by name
            props["Diffuse"] = new Vector3(1, 0, 1);
            //Get property instance by name
            Property pdiffuse = props.FindProperty("Diffuse");
            Console.WriteLine(pdiffuse);
            //Since Property is also inherited from A3DObject
            //It's possible to get the property of the property
            Console.WriteLine("Property flags = {0}", pdiffuse.GetProperty("flags"));
            //and some properties that only defined in FBX file:
            Console.WriteLine("Label = {0}", pdiffuse.GetProperty("label"));
            Console.WriteLine("Type Name = {0}", pdiffuse.GetProperty("typeName"));
            //so traversal on property's property is possible
            foreach (var pp in pdiffuse.Properties)
            {
                Console.WriteLine("Diffuse.{0} = {1}", pp.Name, pp.Value);
            }
            //ExEnd: ThreeDProperties
        }
    }
}
