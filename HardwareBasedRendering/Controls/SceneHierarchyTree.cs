using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Shading;

namespace AssetBrowser.Controls
{
    public partial class SceneHierarchyTree : TreeView
    {
        private const int Img_Node = 0;
        private const int Img_Camera = 1;
        private const int Img_Light = 2;
        private const int Img_Geometry = 3;
        private const int Img_Material = 4;

        public SceneHierarchyTree()
        {
            if (!DesignMode)
                InitUI();
        }

        private void InitUI()
        {
            ImageList = new ImageList()
            {
                ColorDepth = ColorDepth.Depth32Bit,
                TransparentColor =  Color.Black,
                ImageSize = new Size(16, 16)
            };

            ImageList.Images.AddRange(new[]
            {
                Images.Node,
                Images.Camera,
                Images.Light,
                Images.Geometry,
                Images.Material
            });
        }
        public void UpdateHierarchy(Scene scene)
        {
            BeginUpdate();
            try
            {
                UpdateHierarchyImpl(scene);

            }
            finally
            {

                EndUpdate();
            }
        }

        private void UpdateHierarchyImpl(Scene scene)
        {
            Nodes.Clear();
            //set up scene nodes
            TreeNode node = new SceneNodeWrapper(scene.RootNode, Img_Node);
            CreateNodes(node, scene.RootNode);
            Nodes.Add(node);
        }

        private void CreateNodes(TreeNode node, Node sceneNode)
        {
            foreach (Node childNode in sceneNode.ChildNodes)
            {

                if (ABUtils.IsHidden(childNode))
                    continue;
                TreeNode childTree = new SceneNodeWrapper(childNode, Img_Node);
                CreateNodes(childTree, childNode);
                node.Nodes.Add(childTree);
            }

            //create entity nodes
            foreach (Entity entity in sceneNode.Entities)
            {
                if (ABUtils.IsHidden(entity))
                    continue;
                int img = Img_Geometry;
                if (entity is Camera)
                    img = Img_Camera;
                else if (entity is Light)
                    img = Img_Light;
                node.Nodes.Add(new SceneNodeWrapper(entity, img));
            }
            //create material nodes
            foreach (Material mat in sceneNode.Materials)
            {
                node.Nodes.Add(new SceneNodeWrapper(mat, Img_Material));
            }
        }

        public A3DObject SelectedObject
        {
            get
            {
                SceneNodeWrapper w = SelectedNode as SceneNodeWrapper;
                if (w != null)
                    return w.obj;
                return null;
            }
        }

        class SceneNodeWrapper : TreeNode
        {
            internal A3DObject obj;
            public SceneNodeWrapper(A3DObject obj, int imageIndex)
            {
                this.Text = string.Format("{0} : {1}", obj.Name, obj.GetType().Name);
                this.obj = obj;
                this.ImageIndex = this.SelectedImageIndex = imageIndex;
            }
        }
    }
}
