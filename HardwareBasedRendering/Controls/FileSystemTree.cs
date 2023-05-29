﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetBrowser.Controls
{
    public partial class FileSystemTree : TreeView
    {
        private ImageList imageList;
        public FileListView FileListView { get; set; }
        public event EventHandler DirectoryChanged;
        private PathNode root;
        public FileSystemTree()
        {
            this.ShowRootLines = false;
            this.ItemHeight = 22;
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (!DesignMode)
            {
                InitializeUI();
            }
        }

        private void InitializeUI()
        {
            imageList = new ImageList();
            imageList.ImageSize = new Size(16, 16);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            this.ImageList = imageList;
            this.StateImageList = imageList;
            BeginUpdate();
            try
            {
                root = new PathNode("", "My Computer");
                
                foreach (DriveInfo di in DriveInfo.GetDrives())
                {
                    if (!di.IsReady)
                        continue;
                    string name = di.Name;
                    if (!string.IsNullOrEmpty(di.VolumeLabel))
                        name += string.Format("({0})", di.VolumeLabel);

                    Icon icon = Shell.SmallIconFromPath(di.Name, false);
                    imageList.Images.Add(di.Name, icon);
                    PathNode n = new PathNode(di.Name, name);
                    root.children[di.Name.ToUpper()] = n;
                    n.ImageKey = di.Name;
                    root.Nodes.Add(n);
                }
                Nodes.Add(root);
                root.Expand();
            }
            finally
            {
                EndUpdate();
            }
            
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
            PathNode n = e.Node as PathNode;
            if (n != null && FileListView != null)
            {
                FileListView.OpenDirectory(n.path);
            }
            if (n != null && DirectoryChanged != null)
            {
                DirectoryChanged(this, new EventArgs());
            }
        }


        public string CurrentPath
        {
            get
            {
                PathNode n = SelectedNode as PathNode;
                if (n != null)
                    return n.path;
                return null;
            }
            set
            {
                if (value == null)
                {
                    SelectedNode = null;
                    return;
                }
                string[] path = value.Split('/', '\\');
                path[0] = path[0] + "\\";
                PathNode n = root;
                foreach (string p in path)
                {
                    n.ExpandDirectories();
                    n = n.GetChild(p);
                    if (n == null)
                        break;
                }
                SelectedNode = n;

                if (n != null && FileListView != null)
                {
                    FileListView.OpenDirectory(n.path);
                }
            }

        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            PathNode n = e.Node as PathNode;
            if(n != null)
            {
                //clear the dummy node and expand child directories
                n.ExpandDirectories();
            }
            base.OnBeforeExpand(e);
        }

        public string GetImageKey(string subdir, bool open)
        {
            var key = open ? "open" : "closed";
            if (!imageList.Images.ContainsKey(key))
            {
                Icon icon = Shell.SmallIconFromPath(subdir, open);
                imageList.Images.Add(key, icon);
            }
            return key;
        }
    }

    class PathNode : TreeNode
    {
        internal string path;
        internal bool expanded;
        internal Dictionary<string, PathNode> children = new Dictionary<string, PathNode>();
        public PathNode(string path, string title)
        {
            this.Text = title;
            this.path = path;
            if (HasChildDirectories())
            {
                expanded = false;
                //create a dummy child
                Nodes.Add("tmp");
            }
            else
                expanded = true;
        }

        public PathNode GetChild(string name)
        {
            PathNode ret;
            children.TryGetValue(name.ToUpper(), out ret);
            return ret;
        }

        public void ExpandDirectories()
        {
            if (expanded || string.IsNullOrEmpty(path))
                return;
            expanded = true;
            FileSystemTree fst = this.TreeView as FileSystemTree;
            fst.BeginUpdate();
            Nodes.Clear();
            foreach (string subdir in Directory.EnumerateDirectories(path))
            {
                string name = Path.GetFileName(subdir);
                PathNode child = new PathNode(subdir, name);
                children[name.ToUpper()] = child;
                child.ImageKey = fst.GetImageKey(subdir, false);
                child.SelectedImageKey = fst.GetImageKey(subdir, true);
                Nodes.Add(child);
            }
            fst.EndUpdate();
        }

        private bool HasChildDirectories()
        {
            try
            {

                foreach (var v in Directory.EnumerateDirectories(path))
                    return true;
            }
            catch (Exception)
            {

            }
            return false;
        }
        
    }
}
