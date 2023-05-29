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
using Aspose.ThreeD.Render;
using Aspose.ThreeD.Utilities;
using AssetBrowser.Controls;
using Microsoft.Win32;

namespace AssetBrowser
{
    public partial class MainForm : Form
    {
        private string originalTitle;
        private string currentFileName;
        private bool modified;
        private Scene scene = new Scene();
        private ContextMenuStrip contextMenu = new ContextMenuStrip();

        private RegistryKey ConfigKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Aspose\AssetBrowser");

        public MainForm()
        {
            InitializeComponent();
            originalTitle = Text;
            string currentPath = ConfigKey.GetValue("Directory", null) as string;
            if (string.IsNullOrEmpty(currentPath))
                currentPath = Directory.GetCurrentDirectory();
            fileSystemTree1.CurrentPath = currentPath;
            fileSystemTree1.DirectoryChanged += OnDirectoryChanged;
            renderView1.Scene = scene;
            sceneHierarchy.RenderView = renderView1;

            contextMenu.Items.Add("Perspective").Click += delegate(object sender, EventArgs e)
            {
                Camera c = (Camera) renderView1.SelectedViewport.Frustum;
                c.ProjectionType = ProjectionType.Perspective;
                OnMovementChanged(btnStandardMovement, null);
            };
            Func<Vector3, Vector3, bool> ortho = delegate(Vector3 pos, Vector3 up)
            {
                Camera c = (Camera) renderView1.SelectedViewport.Frustum;
                OnMovementChanged(btnStandardMovement, null);
                c.ProjectionType = ProjectionType.Orthographic;
                c.Direction = -pos;
                c.Up = up;
                c.ParentNode.Transform.Translation = pos * c.ParentNode.Transform.Translation.Length;
                return true;
            };
            contextMenu.Items.Add("Orthographic - Front").Click += delegate(object sender, EventArgs e)
            {
                ortho(new Vector3(1, 0, 0), Vector3.YAxis);
            };
            contextMenu.Items.Add("Orthographic - Back").Click += delegate(object sender, EventArgs e)
            {
                ortho(new Vector3(-1, 0, 0), Vector3.YAxis);
            };
            contextMenu.Items.Add("Orthographic - Left").Click += delegate(object sender, EventArgs e)
            {
                ortho(new Vector3(0, 0, 1), Vector3.YAxis);
            };
            contextMenu.Items.Add("Orthographic - Right").Click += delegate(object sender, EventArgs e)
            {
                ortho(new Vector3(0, 0, -1), Vector3.YAxis);
            };
            contextMenu.Items.Add("Orthographic - Top").Click += delegate(object sender, EventArgs e)
            {
                ortho(new Vector3(0, 1, 0), Vector3.XAxis);
            };
            contextMenu.Items.Add("Orthographic - Bottom").Click += delegate(object sender, EventArgs e)
            {
                ortho(new Vector3(0, -1, 0), Vector3.XAxis);
            };
            renderView1.ContextMenuStrip = contextMenu;

            if (File.Exists("Aspose.3D.lic"))
            {
                Aspose.ThreeD.License lic = new Aspose.ThreeD.License();
                lic.SetLicense("Aspose.3D.lic");
            }
            TextureCodec.RegisterCodec(new GdiPlusCodec());
            WindowState = FormWindowState.Maximized;
            renderView1.SceneUpdated("");
            sceneHierarchy.UpdateHierarchy(scene);
        }

        internal RenderView RenderView
        {
            get { return renderView1; }
        }

        internal SceneHierarchyTree SceneHierarchy
        {
            get { return sceneHierarchy; }
        }

        internal void RendererInitialized()
        {
            renderView1.Scene = scene;
            renderView1.SceneUpdated("");
            sceneHierarchy.UpdateHierarchy(scene);
        }
        internal void SceneUpdated()
        {
            sceneHierarchy.UpdateHierarchy(scene);
        }

        private void OnDirectoryChanged(object sender, EventArgs e)
        {
            ConfigKey.SetValue("Directory", fileSystemTree1.CurrentPath);
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
            if (opt != null && opt.GetType() != typeof(LoadOptions))
            {
                if (OptionDialog.ShowDialog(this, "Import Settings", opt) == DialogResult.Cancel)
                    return;
            }



            currentFileName = fileName;
            modified = false;
            UpdateTitle();
            long elapsed = 0;
            try
            {
                lblStatus.Text = string.Format("Loading {0}", fileName);
                fileListView1.Enabled = false;
                propertyGrid1.SelectedObject = null;
                Stopwatch st = Stopwatch.StartNew();
                OnMovementChanged(btnStandardMovement, null);
                Console.WriteLine("Loading file {0}", fileName);
                //open the scene using user modified load option in background thread.
                await Task.Run(() => scene.Open(fileName, opt));
                elapsed = st.ElapsedMilliseconds;
                Console.WriteLine("Loaded in {0}ms", elapsed);

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
            var selectedObject = sceneHierarchy.SelectedObject;
            propertyGrid1.SelectedObject = selectedObject;
            renderView1.SelectObject(selectedObject);
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
                postProcessingEffects.Add("edge-highlight");
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
                    if (!typeof(FileFormat).IsAssignableFrom(fi.FieldType))
                        continue;
                    FileFormat f = fi.GetValue(null) as FileFormat;
                    formats.Add(f);
                    if (filter.Length > 0)
                        filter.Append("|");
                    filter.Append(string.Format("{0} {2} {3} (*{1})|*{1}", f.FileFormatType, f.Extension, f.ContentType, f.Version));
                }
                d.Filter = filter.ToString();
                if (d.ShowDialog(this) == DialogResult.Cancel)
                    return null;
                format = formats[d.FilterIndex - 1];
                return d.FileName;
            }
        }

        private void OnMovementChanged(object sender, EventArgs e)
        {

            if (sender == btnFPS)
                renderView1.ChangeMovement<FPSMovement>();
            else if(sender == btnOrbital)
                renderView1.ChangeMovement<OrbitalMovement>();
            else if(sender == btnStandardMovement)
                renderView1.ChangeMovement<StandardMovement>();

            btnFPS.Checked = sender == btnFPS;
            btnOrbital.Checked = sender == btnOrbital;
            btnStandardMovement.Checked = sender == btnStandardMovement;

        }

        private void OnSetExclusivePostProcessing(object sender, EventArgs e)
        {
            if (sender == btnFisheye)
            {
                PostProcessing fisheye = renderView1.Renderer.GetPostProcessing("fisheye");
                btnFisheye.Checked = !btnFisheye.Checked;
                renderView1.CubeBasedPostProcessing = btnFisheye.Checked ? fisheye : null;
            }

        }

        private void OnToggleFileSystem(object sender, EventArgs e)
        {
            var enabled = miFileSystem.Checked;
            leftContainer.Panel1Collapsed = enabled;
            mainContainer.Panel2Collapsed = enabled;

        }

        private void OnUpVectorChanged(object sender, EventArgs e)
        {
            mnUpY.Checked = sender == mnUpY;
            mnUpZ.Checked = sender == mnUpZ;
            if (mnUpY.Checked)
                renderView1.SetUpVector(Axis.YAxis);
            else if (mnUpZ.Checked)
                renderView1.SetUpVector(Axis.ZAxis);

            renderView1.Invalidate();
        }

        private void OnShowNormals(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = renderView1.NormalRenderer;
        }
    }
}
