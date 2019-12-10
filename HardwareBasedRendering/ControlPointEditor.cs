using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.ThreeD.Utilities;

namespace AssetBrowser
{
    public partial class ControlPointEditor : Form
    {
        private BindingSource dataSource;

        class Vector4Wrapper
        {
            private IList<Vector4> list;
            private int index;

            public Vector4Wrapper(IList<Vector4> list, int index)
            {
                this.index = index;
                this.list = list;
            }

            public double X
            {
                get { return list[index].x; }
                set
                {
                    var v = list[index];
                    v.x = value;
                    list[index] = v;
                }
            }
            public double Y
            {
                get { return list[index].y; }
                set
                {
                    var v = list[index];
                    v.y = value;
                    list[index] = v;
                }
            }
            public double Z
            {
                get { return list[index].z; }
                set
                {
                    var v = list[index];
                    v.z = value;
                    list[index] = v;
                }
            }

        }
        public ControlPointEditor()
        {
            InitializeComponent();
            dataSource = new BindingSource();
            dataGridView1.DataSource = dataSource;
            dataGridView1.AutoGenerateColumns = true;
        }

        public IList<Vector4> Data
        {
            set
            {
                List<Vector4Wrapper> wrapper = new List<Vector4Wrapper>(value.Count);
                for (int i = 0; i < value.Count; i++)
                {
                    wrapper.Add(new Vector4Wrapper(value, i));
                }

                dataSource.DataSource = wrapper;
            }
        }
    }

}
