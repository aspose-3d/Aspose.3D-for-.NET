namespace AssetBrowser
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("tmp");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("tmp");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("C:\\", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("tmp");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("D:\\(Data)", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("My Computer", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode6});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnViewport1 = new System.Windows.Forms.ToolStripButton();
            this.btnViewport2 = new System.Windows.Forms.ToolStripButton();
            this.btnViewport4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGrayscale = new System.Windows.Forms.ToolStripButton();
            this.btnEdgeDetection = new System.Windows.Forms.ToolStripButton();
            this.btnBlur = new System.Windows.Forms.ToolStripButton();
            this.btnPixelation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOrbital = new System.Windows.Forms.ToolStripButton();
            this.btnFPS = new System.Windows.Forms.ToolStripButton();
            this.leftContainer = new System.Windows.Forms.SplitContainer();
            this.fileSystemTree1 = new AssetBrowser.Controls.FileSystemTree();
            this.fileListView1 = new AssetBrowser.Controls.FileListView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.mainContainer = new System.Windows.Forms.SplitContainer();
            this.renderView1 = new AssetBrowser.Controls.RenderView();
            this.rightContainer = new System.Windows.Forms.SplitContainer();
            this.sceneHierarchy = new AssetBrowser.Controls.SceneHierarchyTree();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btnStandardMovement = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftContainer)).BeginInit();
            this.leftContainer.Panel1.SuspendLayout();
            this.leftContainer.Panel2.SuspendLayout();
            this.leftContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
            this.mainContainer.Panel1.SuspendLayout();
            this.mainContainer.Panel2.SuspendLayout();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightContainer)).BeginInit();
            this.rightContainer.Panel1.SuspendLayout();
            this.rightContainer.Panel2.SuspendLayout();
            this.rightContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(716, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.saveAsToolStripMenuItem.Text = "&Export...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.OnExport);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 328);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(716, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnViewport1,
            this.btnViewport2,
            this.btnViewport4,
            this.toolStripSeparator1,
            this.btnGrayscale,
            this.btnEdgeDetection,
            this.btnBlur,
            this.btnPixelation,
            this.toolStripSeparator2,
            this.btnStandardMovement,
            this.btnOrbital,
            this.btnFPS});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(716, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnViewport1
            // 
            this.btnViewport1.Checked = true;
            this.btnViewport1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnViewport1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewport1.Image = global::AssetBrowser.Images.Viewport_1;
            this.btnViewport1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewport1.Name = "btnViewport1";
            this.btnViewport1.Size = new System.Drawing.Size(23, 22);
            this.btnViewport1.Text = "toolStripButton1";
            this.btnViewport1.ToolTipText = "1 viewport";
            this.btnViewport1.Click += new System.EventHandler(this.OnChangeViewport);
            // 
            // btnViewport2
            // 
            this.btnViewport2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewport2.Image = global::AssetBrowser.Images.Viewport_2;
            this.btnViewport2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewport2.Name = "btnViewport2";
            this.btnViewport2.Size = new System.Drawing.Size(23, 22);
            this.btnViewport2.Text = "toolStripButton2";
            this.btnViewport2.ToolTipText = "2 viewports";
            this.btnViewport2.Click += new System.EventHandler(this.OnChangeViewport);
            // 
            // btnViewport4
            // 
            this.btnViewport4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewport4.Image = global::AssetBrowser.Images.Viewport_4;
            this.btnViewport4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewport4.Name = "btnViewport4";
            this.btnViewport4.Size = new System.Drawing.Size(23, 22);
            this.btnViewport4.Text = "toolStripButton3";
            this.btnViewport4.ToolTipText = "4 viewports";
            this.btnViewport4.Click += new System.EventHandler(this.OnChangeViewport);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGrayscale
            // 
            this.btnGrayscale.CheckOnClick = true;
            this.btnGrayscale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGrayscale.Image = global::AssetBrowser.Images.Grayscale;
            this.btnGrayscale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGrayscale.Name = "btnGrayscale";
            this.btnGrayscale.Size = new System.Drawing.Size(23, 22);
            this.btnGrayscale.Text = "Grayscale";
            this.btnGrayscale.CheckedChanged += new System.EventHandler(this.OnTogglePostProcessing);
            // 
            // btnEdgeDetection
            // 
            this.btnEdgeDetection.CheckOnClick = true;
            this.btnEdgeDetection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdgeDetection.Image = global::AssetBrowser.Images.EdgeDetection;
            this.btnEdgeDetection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdgeDetection.Name = "btnEdgeDetection";
            this.btnEdgeDetection.Size = new System.Drawing.Size(23, 22);
            this.btnEdgeDetection.Text = "toolStripButton2";
            this.btnEdgeDetection.ToolTipText = "Edge detection";
            this.btnEdgeDetection.CheckedChanged += new System.EventHandler(this.OnTogglePostProcessing);
            // 
            // btnBlur
            // 
            this.btnBlur.CheckOnClick = true;
            this.btnBlur.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBlur.Image = global::AssetBrowser.Images.Blur;
            this.btnBlur.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBlur.Name = "btnBlur";
            this.btnBlur.Size = new System.Drawing.Size(23, 22);
            this.btnBlur.Text = "toolStripButton3";
            this.btnBlur.ToolTipText = "Blur";
            this.btnBlur.CheckedChanged += new System.EventHandler(this.OnTogglePostProcessing);
            // 
            // btnPixelation
            // 
            this.btnPixelation.CheckOnClick = true;
            this.btnPixelation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPixelation.Image = global::AssetBrowser.Images.Pixelization;
            this.btnPixelation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPixelation.Name = "btnPixelation";
            this.btnPixelation.Size = new System.Drawing.Size(23, 22);
            this.btnPixelation.ToolTipText = "Pixelation";
            this.btnPixelation.CheckedChanged += new System.EventHandler(this.OnTogglePostProcessing);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOrbital
            // 
            this.btnOrbital.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOrbital.Image = global::AssetBrowser.Images.Orbital;
            this.btnOrbital.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOrbital.Name = "btnOrbital";
            this.btnOrbital.Size = new System.Drawing.Size(23, 22);
            this.btnOrbital.Text = "btnOrbital";
            this.btnOrbital.ToolTipText = "Orbital camera movement";
            this.btnOrbital.Click += new System.EventHandler(this.OnMovementChanged);
            // 
            // btnFPS
            // 
            this.btnFPS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFPS.Image = ((System.Drawing.Image)(resources.GetObject("btnFPS.Image")));
            this.btnFPS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFPS.Name = "btnFPS";
            this.btnFPS.Size = new System.Drawing.Size(23, 22);
            this.btnFPS.Text = "btnFPS";
            this.btnFPS.ToolTipText = "FPS-style camera movement";
            this.btnFPS.Click += new System.EventHandler(this.OnMovementChanged);
            // 
            // leftContainer
            // 
            this.leftContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.leftContainer.Location = new System.Drawing.Point(0, 49);
            this.leftContainer.Margin = new System.Windows.Forms.Padding(2);
            this.leftContainer.Name = "leftContainer";
            // 
            // leftContainer.Panel1
            // 
            this.leftContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.leftContainer.Panel1.Controls.Add(this.fileSystemTree1);
            // 
            // leftContainer.Panel2
            // 
            this.leftContainer.Panel2.Controls.Add(this.splitContainer2);
            this.leftContainer.Size = new System.Drawing.Size(716, 279);
            this.leftContainer.SplitterDistance = 224;
            this.leftContainer.SplitterWidth = 3;
            this.leftContainer.TabIndex = 3;
            // 
            // fileSystemTree1
            // 
            this.fileSystemTree1.CurrentPath = null;
            this.fileSystemTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileSystemTree1.FileListView = this.fileListView1;
            this.fileSystemTree1.ImageIndex = 0;
            this.fileSystemTree1.Location = new System.Drawing.Point(0, 0);
            this.fileSystemTree1.Margin = new System.Windows.Forms.Padding(2);
            this.fileSystemTree1.Name = "fileSystemTree1";
            treeNode1.Name = "";
            treeNode1.Text = "tmp";
            treeNode3.Name = "";
            treeNode3.Text = "tmp";
            treeNode4.ImageKey = "C:\\";
            treeNode4.Name = "";
            treeNode4.Text = "C:\\";
            treeNode5.Name = "";
            treeNode5.Text = "tmp";
            treeNode6.ImageKey = "D:\\";
            treeNode6.Name = "";
            treeNode6.Text = "D:\\(Data)";
            treeNode7.Name = "";
            treeNode7.Text = "My Computer";
            this.fileSystemTree1.SelectedImageIndex = 0;
            this.fileSystemTree1.ShowRootLines = false;
            this.fileSystemTree1.Size = new System.Drawing.Size(224, 279);
            this.fileSystemTree1.TabIndex = 0;
            // 
            // fileListView1
            // 
            this.fileListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileListView1.Location = new System.Drawing.Point(0, 0);
            this.fileListView1.Margin = new System.Windows.Forms.Padding(2);
            this.fileListView1.Name = "fileListView1";
            this.fileListView1.Size = new System.Drawing.Size(228, 130);
            this.fileListView1.TabIndex = 0;
            this.fileListView1.UseCompatibleStateImageBehavior = false;
            this.fileListView1.DoubleClick += new System.EventHandler(this.OnOpenSelectedFile);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.mainContainer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rightContainer);
            this.splitContainer2.Size = new System.Drawing.Size(489, 279);
            this.splitContainer2.SplitterDistance = 228;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(2);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainContainer.Panel1
            // 
            this.mainContainer.Panel1.Controls.Add(this.renderView1);
            // 
            // mainContainer.Panel2
            // 
            this.mainContainer.Panel2.Controls.Add(this.fileListView1);
            this.mainContainer.Size = new System.Drawing.Size(228, 279);
            this.mainContainer.SplitterDistance = 146;
            this.mainContainer.SplitterWidth = 3;
            this.mainContainer.TabIndex = 0;
            // 
            // renderView1
            // 
            this.renderView1.AltPressed = false;
            this.renderView1.Buttons = System.Windows.Forms.MouseButtons.None;
            this.renderView1.ControlPressed = false;
            this.renderView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderView1.Location = new System.Drawing.Point(0, 0);
            this.renderView1.Margin = new System.Windows.Forms.Padding(2);
            this.renderView1.Name = "renderView1";
            this.renderView1.ShiftPressed = false;
            this.renderView1.Size = new System.Drawing.Size(228, 146);
            this.renderView1.TabIndex = 0;
            this.renderView1.Text = "renderView1";
            // 
            // rightContainer
            // 
            this.rightContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightContainer.Location = new System.Drawing.Point(0, 0);
            this.rightContainer.Margin = new System.Windows.Forms.Padding(2);
            this.rightContainer.Name = "rightContainer";
            this.rightContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightContainer.Panel1
            // 
            this.rightContainer.Panel1.Controls.Add(this.sceneHierarchy);
            // 
            // rightContainer.Panel2
            // 
            this.rightContainer.Panel2.Controls.Add(this.propertyGrid1);
            this.rightContainer.Size = new System.Drawing.Size(258, 279);
            this.rightContainer.SplitterDistance = 117;
            this.rightContainer.SplitterWidth = 3;
            this.rightContainer.TabIndex = 0;
            // 
            // sceneHierarchy
            // 
            this.sceneHierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneHierarchy.ImageIndex = 0;
            this.sceneHierarchy.Location = new System.Drawing.Point(0, 0);
            this.sceneHierarchy.Margin = new System.Windows.Forms.Padding(2);
            this.sceneHierarchy.Name = "sceneHierarchy";
            this.sceneHierarchy.SelectedImageIndex = 0;
            this.sceneHierarchy.Size = new System.Drawing.Size(258, 117);
            this.sceneHierarchy.TabIndex = 0;
            this.sceneHierarchy.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnSceneObjectSelected);
            this.sceneHierarchy.DoubleClick += new System.EventHandler(this.OnDoubleClickSceneObject);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(258, 159);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.OnPropertyChanged);
            // 
            // btnStandardMovement
            // 
            this.btnStandardMovement.Checked = true;
            this.btnStandardMovement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnStandardMovement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStandardMovement.Image = ((System.Drawing.Image)(resources.GetObject("btnStandardMovement.Image")));
            this.btnStandardMovement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStandardMovement.Name = "btnStandardMovement";
            this.btnStandardMovement.Size = new System.Drawing.Size(23, 22);
            this.btnStandardMovement.ToolTipText = "Standard camera movement";
            this.btnStandardMovement.Click += new System.EventHandler(this.OnMovementChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 350);
            this.Controls.Add(this.leftContainer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Aspose.3D Assets Browser";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.leftContainer.Panel1.ResumeLayout(false);
            this.leftContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftContainer)).EndInit();
            this.leftContainer.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.mainContainer.Panel1.ResumeLayout(false);
            this.mainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
            this.mainContainer.ResumeLayout(false);
            this.rightContainer.Panel1.ResumeLayout(false);
            this.rightContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightContainer)).EndInit();
            this.rightContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer leftContainer;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer rightContainer;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SplitContainer mainContainer;
        private Controls.FileSystemTree fileSystemTree1;
        private Controls.FileListView fileListView1;
        private Controls.RenderView renderView1;
        private Controls.SceneHierarchyTree sceneHierarchy;
        private System.Windows.Forms.ToolStripButton btnViewport1;
        private System.Windows.Forms.ToolStripButton btnViewport2;
        private System.Windows.Forms.ToolStripButton btnViewport4;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnGrayscale;
        private System.Windows.Forms.ToolStripButton btnEdgeDetection;
        private System.Windows.Forms.ToolStripButton btnBlur;
        private System.Windows.Forms.ToolStripButton btnPixelation;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnOrbital;
        private System.Windows.Forms.ToolStripButton btnFPS;
        private System.Windows.Forms.ToolStripButton btnStandardMovement;
    }
}