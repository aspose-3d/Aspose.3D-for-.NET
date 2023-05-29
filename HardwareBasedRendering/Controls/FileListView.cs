using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Aspose.ThreeD;

namespace AssetBrowser.Controls
{
    public partial class FileListView : ListView
    {
        private string directory;
        private Dictionary<string, int> indices = new Dictionary<string, int>();
        private HashSet<string> supportedExtensions;
        public FileListView()
        {
            if (!DesignMode)
                InitUI();

        }

        public void InitUI()
        {
            LargeImageList = new ImageList();
            LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            this.View = View.LargeIcon;

            supportedExtensions = new HashSet<string>(
                from field in typeof(FileFormat).GetFields(BindingFlags.Public | BindingFlags.Static)
                from ext in ((FileFormat) field.GetValue(null)).Extensions
                select ext
                );
        }
        private bool CanDisplayFile(string path)
        {
            var fileName = Path.GetFileName(path);
            if (fileName == "SourceFile" || fileName == "ReviewFile")
                return true;

            string ext = Path.GetExtension(path).ToLower();
            return supportedExtensions.Contains(ext);

        }

        public void OpenDirectory(string path)
        {
            this.directory = path;
            BeginUpdate();
            Items.Clear();
            try

            {
                foreach (string fileName in Directory.GetFiles(path))
                {
                    if (CanDisplayFile(fileName))
                    {
                        FileNode node = new FileNode(fileName);
                        node.ImageIndex = GetIconName(fileName);
                        Items.Add(node);
                    }

                }
            }
            catch (Exception)
            {

            }
            finally
            {
                EndUpdate();
            }
        }

        public string SelectedFileName
        {
            get
            {
                if (SelectedItems.Count == 0)
                    return null;
                FileNode fn = SelectedItems[0] as FileNode;
                if (fn == null)
                    return null;
                return fn.FileName;
            }
        }

        private int GetIconName(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            int ret;
            if (indices.TryGetValue(ext, out ret))
                return ret;
            Icon icon = Shell.LargeIconFromPath(fileName);
            ret = LargeImageList.Images.Count;
            LargeImageList.Images.Add(icon);
            indices.Add(ext, ret);
            return ret;
        }

        class FileNode : ListViewItem
        {
            public string FileName { get; set; }
            public FileNode(string fileName)
            {
                this.FileName = fileName;
                this.Text = Path.GetFileName(fileName);
            }
        }
    }
}
