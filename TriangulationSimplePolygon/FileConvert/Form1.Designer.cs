namespace FileConvert
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btnOpenInput = new System.Windows.Forms.Button();
            this.btnOpenOutput = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.pgInput = new System.Windows.Forms.PropertyGrid();
            this.pgOutput = new System.Windows.Forms.PropertyGrid();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pgInput);
            this.groupBox1.Controls.Add(this.btnOpenInput);
            this.groupBox1.Controls.Add(this.tbInput);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 304);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pgOutput);
            this.groupBox2.Controls.Add(this.btnOpenOutput);
            this.groupBox2.Controls.Add(this.tbOutput);
            this.groupBox2.Location = new System.Drawing.Point(326, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 304);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output File";
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(7, 21);
            this.tbInput.Name = "tbInput";
            this.tbInput.ReadOnly = true;
            this.tbInput.Size = new System.Drawing.Size(259, 21);
            this.tbInput.TabIndex = 0;
            // 
            // btnOpenInput
            // 
            this.btnOpenInput.Location = new System.Drawing.Point(272, 19);
            this.btnOpenInput.Name = "btnOpenInput";
            this.btnOpenInput.Size = new System.Drawing.Size(30, 23);
            this.btnOpenInput.TabIndex = 1;
            this.btnOpenInput.Text = "...";
            this.btnOpenInput.UseVisualStyleBackColor = true;
            this.btnOpenInput.Click += new System.EventHandler(this.OnOpenInput);
            // 
            // btnOpenOutput
            // 
            this.btnOpenOutput.Location = new System.Drawing.Point(285, 19);
            this.btnOpenOutput.Name = "btnOpenOutput";
            this.btnOpenOutput.Size = new System.Drawing.Size(30, 23);
            this.btnOpenOutput.TabIndex = 3;
            this.btnOpenOutput.Text = "...";
            this.btnOpenOutput.UseVisualStyleBackColor = true;
            this.btnOpenOutput.Click += new System.EventHandler(this.OnSaveOutput);
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(6, 21);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.Size = new System.Drawing.Size(273, 21);
            this.tbOutput.TabIndex = 2;
            // 
            // pgInput
            // 
            this.pgInput.Location = new System.Drawing.Point(7, 48);
            this.pgInput.Name = "pgInput";
            this.pgInput.Size = new System.Drawing.Size(295, 250);
            this.pgInput.TabIndex = 2;
            // 
            // pgOutput
            // 
            this.pgOutput.Location = new System.Drawing.Point(6, 48);
            this.pgOutput.Name = "pgOutput";
            this.pgOutput.Size = new System.Drawing.Size(309, 250);
            this.pgOutput.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(566, 322);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Convert";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnConvert);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 354);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Aspose.3D File Convert Demo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PropertyGrid pgInput;
        private System.Windows.Forms.Button btnOpenInput;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PropertyGrid pgOutput;
        private System.Windows.Forms.Button btnOpenOutput;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button button3;
    }
}

