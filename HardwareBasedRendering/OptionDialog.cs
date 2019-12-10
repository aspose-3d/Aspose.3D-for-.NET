using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetBrowser
{
    public partial class OptionDialog : Form
    {
        public OptionDialog()
        {
            InitializeComponent();
        }


        public static DialogResult ShowDialog(Form parent, string title, object options)
        {
            using (OptionDialog d = new OptionDialog())
            {
                d.Text = title;
                d.propertyGrid1.SelectedObject = options;
                return d.ShowDialog(parent);
            }
        }
    }
}
