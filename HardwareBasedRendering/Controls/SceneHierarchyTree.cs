using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Shading;

namespace AssetBrowser.Controls
{
    partial class SceneHierarchyTree : TreeView
    {
        private const int Img_Node = 0;
        private const int Img_Camera = 1;
        private const int Img_Light = 2;
        private const int Img_Geometry = 3;
        private const int Img_Material = 4;
        private ContextMenuStrip contextMenu;
        private ToolStripItem editControlPoints;
        private ToolStripItem saveAs;
        private ToolStripItem gotoNode;
        private ToolStripItem visibleNode;
        internal RenderView RenderView;

        public SceneHierarchyTree()
        {
            if (!DesignMode)
                InitUI();
        }

        private void InitUI()
        {

            contextMenu = new ContextMenuStrip();
            editControlPoints = contextMenu.Items.Add("Edit Control Points");
            gotoNode = contextMenu.Items.Add("Go to");
            saveAs = contextMenu.Items.Add("Save as");
            visibleNode = contextMenu.Items.Add("Hide");
            visibleNode.Click += OnToggleVisibility;
            editControlPoints.Click += OnEditControlPoints;
            saveAs.Click += OnSaveAs;
            gotoNode.Click += OnGotoNode;
        }

        private void OnSaveAs(object sender, EventArgs e)
        {
            var mesh = (Mesh) SelectedObject;
            var s = new Scene(mesh);
            using(var dialog = new SaveFileDialog())
            {
                dialog.Filter = "FBX File(*.fbx)|*.fbx|OBJ File(*.obj)|*.obj|STL File(*.stl)|*.stl";
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                var ext = Path.GetExtension(dialog.FileName);
                var ft = FileFormat.FBX7700ASCII;
                if (ext == ".obj")
                    ft = FileFormat.WavefrontOBJ;
                else if(ext == ".stl")
                    ft = FileFormat.STLASCII;

                s.Save(dialog.FileName, ft);
            }


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
                saveAs.Enabled = SelectedObject is Mesh;
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
            Nodes.Clear();
            //set up scene nodes
            var node = new SceneNodeWrapper(scene.RootNode);
            node.ImageIndex = Img_Node;
            CreateNodes(node, scene.RootNode);
            Nodes.Add(node);
            node.ExpandAll();
        }

        private void CreateNodes(SceneNodeWrapper node, Node sceneNode)
        {
            foreach (Node childNode in sceneNode.ChildNodes)
            {

                if (ABUtils.IsHidden(childNode))
                    continue;
                var childTree = new SceneNodeWrapper(childNode);
                childTree.ImageIndex = Img_Node;
                CreateNodes(childTree, childNode);
                node.Nodes.Add(childTree);
            }

            //create entity nodes
            foreach (Entity entity in sceneNode.Entities)
            {
                if (ABUtils.IsHidden(entity))
                    continue;
                node.Nodes.Add(new SceneNodeWrapper(entity));
            }
            //create material nodes
            foreach (Material mat in sceneNode.Materials)
            {
                if (mat == null)
                    continue;
                var matNode = new SceneNodeWrapper(mat);
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
            var texNode = new SceneNodeWrapper(tex);
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
            public SceneNodeWrapper(A3DObject obj)
            {
                this.obj = obj;
                this.Text = obj.Name + " : " + obj.GetType().Name;
            }


            public Bitmap GetIcon()
            {
                if(obj is Node)
                    return Images.Node;
                if (obj is Camera)
                    return Images.Camera;
                if (obj is Light)
                    return Images.Light;
                if (obj is Entity)
                    return Images.Geometry;
                if (obj is Material)
                    return Images.Material;
                return null;
            }
        }
    }
}
