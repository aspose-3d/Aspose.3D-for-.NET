using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose._3D.Examples.CSharp.Working_with_Objects
{
    public class XPathLikeObjectQueries
    {
        public static void Run()
        {
            // ExStart:XPathLikeObjectQueries
            //Create a scene for testing 
            Scene s = new Scene();
            var a = s.RootNode.CreateChildNode("a");
            a.CreateChildNode("a1");
            a.CreateChildNode("a2");
            s.RootNode.CreateChildNode("b");
            var c = s.RootNode.CreateChildNode("c");
            c.CreateChildNode("c1").AddEntity(new Camera("cam"));
            c.CreateChildNode("c2").AddEntity(new Light("light"));
            /*The hierarchy of the scene looks like:
             - Root
                - a
                    - a1
                    - a2
                - b
                - c
                    - c1
                        - cam
                    - c2
                        - light
                         */
            //select objects that has type Camera or name is 'light' whatever it's located.
            var objects = s.RootNode.SelectObjects("//*[(@Type = 'Camera') or (@Name = 'light')]");
            //Select single camera object under the child nodes of node named 'c' under the root node
            var c1 = s.RootNode.SelectSingleObject("/c/*/<Camera>");
            // Select node named 'a1' under the root node, even if the 'a1' is not a directly child node of the 
            var obj = s.RootNode.SelectSingleObject("a1");
            //Select the node itself, since the '/' is selected directly on the root node, so the root node is selected.
            obj = s.RootNode.SelectSingleObject("/");
            // ExEnd:XPathLikeObjectQueries              
        }
    }
}
