using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetBrowser.Controls
{
    class AsposeTreeEventArgs : EventArgs
    {
        public AsposeNode Node { get; }
        public AsposeTreeEventArgs(AsposeNode node)
        {
            this.Node = node;
        }
    }
    abstract class AsposeNode
    {
        private List<AsposeNode> nodes = new List<AsposeNode>();
        internal int depth;
        internal bool opened;
        internal bool hasChildren;
        public List<AsposeNode> Nodes => nodes;
        public AsposeTreeView TreeView { get; set; }
        public AsposeNode Parent { get; internal set; }
        public AsposeNode()
        {
        }
        public abstract string GetText();
        public virtual void BeforeExpand()
        {

        }
        public abstract void DrawNode(NodeDrawer drawer);

        public virtual Bitmap GetIcon()
        {
            return null;
        }
        public void ExpandAll()
        {
            DoExpand();
            for(int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].ExpandAll();
            }
        }
        private void DoExpand()
        {
            if (opened)
                return;
            opened = true;
            BeforeExpand();
            for(int i = 0; i < Nodes.Count; i++)
            {
                var child = Nodes[i];
                child.depth = depth + 1;
                child.TreeView = TreeView;
                child.Parent = this;
            }
        }
    }
    class AsposeTreeView : Control
    {
        private List<AsposeNode> rootNodes = new List<AsposeNode>();
        private List<AsposeNode> flatNodes = new List<AsposeNode>();
        
        NodeDrawer drawer;
        public Color LineColor { get; set; }
        public List<AsposeNode> Nodes => rootNodes;
        public event EventHandler<AsposeTreeEventArgs> AfterSelect;
        private VScrollBar vscrollBar = new VScrollBar();
        private HScrollBar hscrollBar = new HScrollBar();
        private int selectedNodeIndex;
        private Brush BrushSelectedBackground = Brushes.LightBlue;
        public AsposeTreeView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Selectable, true);
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.LineColor = Color.White;
            this.Font = new Font(FontFamily.GenericMonospace, 14);
            //this.FullRowSelect = true;
            //this.HotTracking = false;
            this.drawer = new NodeDrawer(this.Font);

            Controls.AddRange(new Control[] { vscrollBar, hscrollBar });
            vscrollBar.ValueChanged += ScrollBar_ValueChanged;
            hscrollBar.ValueChanged += ScrollBar_ValueChanged;
        }

        public AsposeNode SelectedNode
        {
            get { return selectedNodeIndex == -1 ? null : flatNodes[selectedNodeIndex]; }
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void BeginUpdate()
        {

        }
        public void EndUpdate()
        {
            flatNodes.Clear();
            AddFlatNodes(rootNodes, 0);
            UpdateScrollBar();
            Invalidate();
        }

        private void AddFlatNodes(List<AsposeNode> rootNodes, int depth)
        {
            for(int i = 0; i < rootNodes.Count; i++)
            {
                var node = rootNodes[i];
                node.TreeView = this;
                node.depth = depth;
                flatNodes.Add(node);
                if (node.opened)
                    AddFlatNodes(node.Nodes, depth + 1);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            var bounds = Bounds;
            hscrollBar.SetBounds(0, bounds.Height - hscrollBar.Height, bounds.Width, hscrollBar.Height);
            vscrollBar.SetBounds(bounds.Width - vscrollBar.Width, 0, vscrollBar.Width, bounds.Height);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //calculate the selected node
            var idx = HitTest(e.Location);
            SelectNode(idx);
            Focus();
            Invalidate();
        }
        private void SelectNode(int idx)
        {
            if (idx >= 0 && idx < flatNodes.Count)
            {
                selectedNodeIndex = idx;
                var node = flatNodes[idx];
                AfterSelect?.Invoke(this, new AsposeTreeEventArgs(node));
                //if the node is beyond the visible set, scroll it

                var firstNode = vscrollBar.Value;
                var maxNodes = Height / FontHeight;
                var lastNode = maxNodes + firstNode;
                if(selectedNodeIndex >= lastNode)
                {
                    vscrollBar.Value = selectedNodeIndex - maxNodes + 1;


                }
                else if(selectedNodeIndex <firstNode)
                {
                    vscrollBar.Value = selectedNodeIndex;
                }
            }
            else
                selectedNodeIndex = -1;
        }
        private int HitTest(Point loc)
        {
            //calculate the selected node
            var rowHeight = FontHeight;
            int idx = loc.Y / rowHeight + vscrollBar.Value;
            if (idx >= 0 && idx < flatNodes.Count)
                return idx;
            return -1;

        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            var nodeId = HitTest(e.Location);
            ToggleNodeState(nodeId);
        }
        private void ToggleNodeState(int nodeId)
        {
            if (nodeId == -1)
                return;
            var node = flatNodes[nodeId];
            SelectNode(nodeId);
            if(node.opened)
            {
                //collapse it
                node.opened = false;
                //remove the siblings
                if (node.Nodes.Count > 0)
                {
                    flatNodes.RemoveRange(nodeId + 1, node.Nodes.Count);
                    UpdateScrollBar();
                }
            }
            else
            {
                ExpandNode(nodeId);
            }
            Invalidate();

        }
        private void ExpandNode(int nodeId)
        {
            var node = flatNodes[nodeId];
            if (node.opened)
                return;

            node.BeforeExpand();
            node.opened = true;
            //expand the siblings
            if (node.Nodes.Count > 0)
            {
                flatNodes.InsertRange(nodeId + 1, node.Nodes);
                foreach (var n in node.Nodes)
                {
                    n.depth = node.depth + 1;
                    n.TreeView = this;
                }
                UpdateScrollBar();
            }
        }
        private void UpdateScrollBar()
        {
            int maxChars = 10;
            for(int i = 0; i < flatNodes.Count; i++)
            {
                var text = flatNodes[i].GetText();
                if (text != null)
                    maxChars = Math.Max(maxChars, text.Length);
            }
            this.hscrollBar.Maximum = maxChars + 5;
            vscrollBar.Maximum = flatNodes.Count;
        }
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            switch(e.KeyCode)
            {
                case Keys.Up:
                    SelectNode(selectedNodeIndex - 1);
                    break;
                case Keys.Down:
                    SelectNode(selectedNodeIndex + 1);
                    break;
                case Keys.Right:
                    if (selectedNodeIndex != -1)
                        ExpandNode(selectedNodeIndex);
                    break;
                case Keys.Space:
                    ToggleNodeState(selectedNodeIndex);
                    break;
            }
            Invalidate();
        }
        /*
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            var node = e.Node as AsposeObjectSetNode;
            if (node != null)
                node.BeforeExpand();
            base.OnBeforeExpand(e);
        }
        */
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);
            var bounds = this.Bounds;

            int rowHeight = Font.Height;
            int maxElements = bounds.Height / rowHeight;
            int p = vscrollBar.Value;
            int rest = maxElements;

            int y = 0;

            int xOffset = 5 - hscrollBar.Value * 16;
            for (; p < flatNodes.Count && rest > 0;p++, rest--)
            {
                var node = flatNodes[p];
                if(p == selectedNodeIndex)
                {
                    //draw selected background
                    g.FillRectangle(BrushSelectedBackground, 0, y, bounds.Width, rowHeight);
                }
                int x = node.depth * 16 + xOffset;
                //draw icon
                Bitmap icon = node.GetIcon();
                if (icon != null)
                {
                    g.DrawImageUnscaled(icon, x, y + (rowHeight - icon.Height) / 2);
                }
                x += 16;
                var nodeBound = new Rectangle(x, y, bounds.Width, rowHeight);
                drawer.Draw(g, node, nodeBound);
                y += rowHeight;
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int topOffset = vscrollBar.Value;
            if (e.Delta > 0)
                topOffset --;
            else if(e.Delta < 0)
                topOffset ++;
            if (topOffset < 0)
                topOffset = 0;
            if (topOffset > vscrollBar.Maximum)
                topOffset = vscrollBar.Maximum;
            vscrollBar.Value = topOffset;
            Invalidate();
        }

    }
    class NodeDrawer
    {
        private float x;
        private float y;
        private Font font;
        Rectangle bounds;
        Graphics g;
        AsposeNode node;
        public NodeDrawer(Font font)
        {
            this.font = font;
        }
        public void DrawBorder()
        {
            g.DrawRectangle(Pens.Red, bounds.X, bounds.Y, x - bounds.X, bounds.Height - 1);

        }
        public void Draw(Graphics g, AsposeNode node, Rectangle bounds)
        {
            this.g = g;
            this.x = bounds.X;
            this.y = bounds.Y + 3;
            this.node = node;
            this.bounds = bounds;

            node.DrawNode(this);
        }
        public void Draw(string str, Brush brush)
        {
            SizeF size = g.MeasureString(str, font);
            g.DrawString(str, font, brush, x, y);
            x += size.Width - 6;
        }
        public void Indent(int offset)
        {
            x += offset;
        }
    }

}
