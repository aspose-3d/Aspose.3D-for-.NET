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
        private ContextMenu contextMenu;
        private MenuItem editControlPoints;
        private MenuItem gotoNode;
        private MenuItem visibleNode;
        internal RenderView RenderView;

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
            contextMenu = new ContextMenu();
            editControlPoints = contextMenu.MenuItems.Add("Edit Control Points");
            gotoNode = contextMenu.MenuItems.Add("Go to");
            visibleNode = contextMenu.MenuItems.Add("Hide");
            visibleNode.Click += OnToggleVisibility;
            editControlPoints.Click += OnEditControlPoints;
            gotoNode.Click += OnGotoNode;
        }

        private void OnToggleVisibility(object sender, EventArgs e)
        {
            var node = (Node) SelectedObject;
            node.Visible = !node.Visible;
            RenderView.Invalidate();
        }
        private void OnGotoNode(object sender, EventArgs e)
        {
            var node = (Node) SelectedObject;
            var bb = node.GetBoundingBox();
            var center = (bb.Minimum + bb.Maximum) * 0.5;
            var size = bb.Maximum - bb.Minimum;

            Camera cam = (Camera) RenderView.SelectedViewport.Frustum;
            cam.ParentNode.Transform.Translation = size * 1.1 + center;
            cam.LookAt = center;
            RenderView.Invalidate();

        }
        private void OnEditControlPoints(object sender, EventArgs e)
        {
            using (ControlPointEditor form = new ControlPointEditor())
            {
                form.Data = ((Mesh) SelectedObject).ControlPoints;
                form.ShowDialog(this);
            }
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

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                editControlPoints.Enabled = SelectedObject is Mesh;
                var node = SelectedObject as Node;
                gotoNode.Enabled = node != null;
                visibleNode.Visible = node != null;
                if(node != null)
                {
                    visibleNode.Text = node.Visible ? "Hide" : "Show";
                }

                contextMenu.Show(this, e.Location);
            }
        }

        private void UpdateHierarchyImpl(Scene scene)
        {
            BeginUpdate();
            Nodes.Clear();
            //set up scene nodes
            TreeNode node = new SceneNodeWrapper(scene.RootNode, Img_Node);
            CreateNodes(node, scene.RootNode);
            Nodes.Add(node);
            node.ExpandAll();
            EndUpdate();
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
                if (mat == null)
                    continue;
                var matNode = new SceneNodeWrapper(mat, Img_Material);
                CreateTextureNode(matNode, mat, Material.MapDiffuse);
                CreateTextureNode(matNode, mat, Material.MapAmbient);
                CreateTextureNode(matNode, mat, Material.MapEmissive);
                CreateTextureNode(matNode, mat, Material.MapNormal);
                CreateTextureNode(matNode, mat, "Occlusion");
                CreateTextureNode(matNode, mat, "MetallicRoughness");
                node.Nodes.Add(matNode);
            }
        }

        private void CreateTextureNode(SceneNodeWrapper parent, Material mat, String texName)
        {
            var tex = mat.GetTexture(texName);
            if (tex == null)
                return;
            var texNode = new SceneNodeWrapper(tex, Img_Material);
            parent.Nodes.Add(texNode);
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
