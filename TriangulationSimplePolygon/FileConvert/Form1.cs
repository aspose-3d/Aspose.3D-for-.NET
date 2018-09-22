using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;

namespace FileConvert
{
    public partial class Form1 : Form
    {
        private LoadOptions loadOptions;
        private SaveOptions saveOptions;
        private Scene scene;
        public Form1()
        {
            InitializeComponent();
        }

        private void OnConvert(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbInput.Text))
            {
                MessageBox.Show(this, "Input file is not specified.", "Convert", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbOutput.Text))
            {
                MessageBox.Show(this, "Input file is not specified.", "Convert", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Scene scene = new Scene();
            scene.Open(tbInput.Text, loadOptions);
            scene.Save(tbOutput.Text, saveOptions);
                MessageBox.Show(this, "File converted.", "Convert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnOpenInput(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = "All files (*.*)|*.*";
            if (dialog.ShowDialog(this) == DialogResult.Cancel)
                return;
            FileFormat inputFormat = FileFormat.Detect(dialog.FileName);
            if (inputFormat == null)
            {
                MessageBox.Show(this, "Unsupported 3D file format", "Open file", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            tbInput.Text = dialog.FileName;
            //show load options for this format
            pgInput.SelectedObject = loadOptions = inputFormat.CreateLoadOptions();
        }

        private void OnSaveOutput(object sender, EventArgs e)
        {
            FileFormat[] formats =
            {

                FileFormat.Collada,
                FileFormat.Discreet3DS,
                FileFormat.FBX7200ASCII,
                FileFormat.FBX7200Binary,
                FileFormat.FBX7300ASCII,
                FileFormat.FBX7300Binary,
                FileFormat.FBX7400ASCII,
                FileFormat.FBX7400Binary,
                FileFormat.FBX7500ASCII,
                FileFormat.FBX7500Binary,
                FileFormat.STLASCII,
                FileFormat.STLBinary,
                FileFormat.Universal3D,
                FileFormat.WavefrontOBJ,
            };
            StringBuilder filter = new StringBuilder();
            foreach (FileFormat fmt in formats)
            {
                if (filter.Length > 0)
                    filter.Append('|');
                filter.AppendFormat("{0} {1} {2} (*{3})|*{3}", fmt.FileFormatType, fmt.Version, fmt.ContentType,
                    fmt.Extension);
            }
            SaveFileDialog d = new SaveFileDialog();
            d.AddExtension = true;
            d.Filter = filter.ToString();
            if (d.ShowDialog() == DialogResult.Cancel)
                return;
            FileFormat outputFormat = formats[d.FilterIndex - 1];
            tbOutput.Text = d.FileName;
            pgOutput.SelectedObject = saveOptions = outputFormat.CreateSaveOptions();
        }
    }
}
