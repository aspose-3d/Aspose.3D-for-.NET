using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.ThreeD;

namespace AssetBrowser
{
    /// <summary>
    /// Utilities for Asset Browser
    /// </summary>
    class ABUtils
    {

        public static void MakeHidden(A3DObject obj)
        {
            obj.CreateDynamicProperty<bool>("hidden").Value = true;
        }

        public static bool IsHidden(A3DObject obj)
        {
            return obj.GetDynamicProperty("hidden") != null;
        }
        /// <summary>
        /// Create internal node used by Asset Browser, the internal node will not be displayed in hierarchy tree and will not be exported
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Node CreateInternalNode(Node parent, string name, Entity entity)
        {
            Node ret = parent.CreateChildNode(name, entity);
            ret.Excluded = true;
            MakeHidden(ret);
            return ret;
        }
    }
}
