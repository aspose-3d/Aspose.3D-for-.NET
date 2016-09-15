using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;

namespace AssetBrowser
{
    public partial class MainForm : Form
    {
        private string originalTitle;
        private string currentFileName;
        private bool modified;
        private Scene scene = new Scene();
        public MainForm()
        {
            InitializeComponent();
            originalTitle = Text;
            fileSystemTree1.CurrentPath = Directory.GetCurrentDirectory();
            renderView1.Scene = scene;
        }


        private void UpdateTitle()
        {
            if(modified)
                Text = string.Format("{0} - {1} *Modified", originalTitle, Path.GetFileName(currentFileName));
            else
                Text = string.Format("{0} - {1}", originalTitle, Path.GetFileName(currentFileName));
        }

        public async void OpenFile(string fileName)
        {
            FileFormat format = FileFormat.Detect(fileName);
            if (format == null)
            {
                MessageBox.Show(this, "Unsupported file format", "Open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //create load option and allow user to modify the load options
            LoadOptions opt = format.CreateLoadOptions();
            if (OptionDialog.ShowDialog(this, "Import Settings", opt) == DialogResult.Cancel)
                return;



            currentFileName = fileName;
            UpdateTitle();
            long elapsed = 0;
            try
            {
                lblStatus.Text = string.Format("Loading {0}", fileName);
                fileListView1.Enabled = false;
                propertyGrid1.SelectedObject = null;
                Stopwatch st = Stopwatch.StartNew();
                //open the scene using user modified load option in background thread.
                await Task.Run(() => scene.Open(fileName, opt));
                elapsed = st.ElapsedMilliseconds;

                renderView1.SceneUpdated(fileName);
                sceneHierarchy.UpdateHierarchy(scene);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "Cannot open file " + fileName + "\n" + e.Message, "Open file",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(elapsed > 0)
                    lblStatus.Text = string.Format("Loaded in {0}ms, Ready.", elapsed);
                else
                    lblStatus.Text = "Ready.";
                fileListView1.Enabled = true;
            }

        }

        private void OnOpenSelectedFile(object sender, EventArgs e)
        {
            string fileName = fileListView1.SelectedFileName;
            if (fileName == null)
                return;
            OpenFile(fileName);
        }

        private void OnSceneObjectSelected(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = sceneHierarchy.SelectedObject;
        }

        private void OnPropertyChanged(object s, PropertyValueChangedEventArgs e)
        {
            renderView1.Invalidate();
        }

        private void OnChangeViewport(object sender, EventArgs e)
        {
            int viewports = 1;
            if (sender == btnViewport2)
                viewports = 2;
            else if (sender == btnViewport4)
                viewports = 4;
            btnViewport1.Checked = viewports == 1;
            btnViewport2.Checked = viewports == 2;
            btnViewport4.Checked = viewports == 4;
            renderView1.SetViewports(viewports);
        }

        private void OnTogglePostProcessing(object sender, EventArgs e)
        {
            List<string> postProcessingEffects = new List<string>();
            if(btnGrayscale.Checked)
                postProcessingEffects.Add("grayscale");
            if(btnEdgeDetection.Checked)
                postProcessingEffects.Add("edge-detection");
            if(btnPixelation.Checked)
                postProcessingEffects.Add("pixelation");
            if(btnBlur.Checked)
                postProcessingEffects.Add("blur");
            renderView1.SetPostProcessings(postProcessingEffects);
        }

        private void OnDoubleClickSceneObject(object sender, EventArgs e)
        {
            Camera camera = sceneHierarchy.SelectedObject as Camera;
            if (camera != null)
            {
                //switch to the selected camera
                renderView1.SetCamera(camera);
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnExport(object sender, EventArgs e)
        {
            FileFormat format;
            string fileName = GetSaveFileName(out format);
            if (format == null)
                return;
            //show options for exporting
            SaveOptions opt = format.CreateSaveOptions();
            if (OptionDialog.ShowDialog(this, "Export scene", opt) == DialogResult.Cancel)
                return;
            scene.Save(fileName, opt);


        }

        private string GetSaveFileName(out FileFormat format)
        {
            format = null;
            using (SaveFileDialog d = new SaveFileDialog())
            {
                d.Title = "Export scene";
                List<FileFormat> formats = new List<FileFormat>();
                StringBuilder filter = new StringBuilder();
                foreach (FieldInfo fi in typeof(FileFormat).GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    if (fi.FieldType != typeof(FileFormat))
                        continue;
                    FileFormat f = fi.GetValue(null) as FileFormat;
                    formats.Add(f);
                    if (filter.Length > 0)
                        filter.Append("|");
                    filter.Append(string.Format("{0} {2} {3} (*{1})|*{1}", f.FileFormatType, f.FileFormatType.Extension, f.ContentType, f.Version));
                }
                d.Filter = filter.ToString();
                if (d.ShowDialog(this) == DialogResult.Cancel)
                    return null;
                format = formats[d.FilterIndex - 1];
                return d.FileName;
            }
        }
    }
}
