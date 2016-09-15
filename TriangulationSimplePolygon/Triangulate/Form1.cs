using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triangulate
{
    public partial class Form1 : Form
    {
        private PolygonCanvas polygonCanvas;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            polygonCanvas = new PolygonCanvas();
            polygonCanvas.Dock = DockStyle.Fill;
            polygonCanvas.TriangleUpdated += PolygonCanvas_TriangleUpdated;
            splitContainer1.Panel1.Controls.Add(polygonCanvas);

        }

        private void PolygonCanvas_TriangleUpdated(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Vertex vt in polygonCanvas.points)
                listBox1.Items.Add(vt);
        }

        private void OnTriangleChanged(object s, PropertyValueChangedEventArgs e)
        {
            polygonCanvas.UpdateTriangles();
        }

        private void OnSelectedTriangleChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = listBox1.SelectedItem;

        }
    }
}
