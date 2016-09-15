using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;

namespace Triangulate
{

    public class Vertex
    {
        [Browsable(false)]
        public int Index { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Vertex(int index, double x, double y)
        {
            Index = index;
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return string.Format("#{0}: {1} {2}", Index, X, Y);
        }
    }
    
    class PolygonCanvas : Control
    {
        public List<Vertex> points = new List<Vertex>();

        public event EventHandler TriangleUpdated;

        private Point mousePos;
        private Pen virtualLine = new Pen(Color.DarkSeaGreen);
        private Vector4[][] triangles;
        private int[][] triangleIndices;
        private Pen polygonPen = Pens.Black;

        private Brush[] brushes = new Brush[] {Brushes.Blue, Brushes.BlueViolet, Brushes.DarkCyan, Brushes.ForestGreen, Brushes.LimeGreen};
        private Brush textBrush = Brushes.Black;
        public PolygonCanvas()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint   , true);


            Cursor = Cursors.Cross;

            virtualLine.DashStyle = DashStyle.Dash;
            virtualLine.DashPattern = new float[] {10, 10};
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(Color.AliceBlue);

            if (points.Count == 0)
                return;
            Size size = this.Size;
            if (points.Count == 1)
            {
                //draw virtual line
                PointF pt = ToPoint(points[0], size);
                g.DrawLine(virtualLine, mousePos, pt);

            }
            else if (points.Count == 2)
            {
                //draw virtual triangle
                PointF pt1 = ToPoint(points[0], size);
                PointF pt2 = ToPoint(points[1], size);
                g.DrawLine(polygonPen, pt2, pt1);
                g.DrawLine(virtualLine, mousePos, pt1);
                g.DrawLine(virtualLine, mousePos, pt2);
            }
            else
            {
                //draw polygon and virtual line

                //draw triangles
                if (triangles != null)
                {
                    for (int i = 0; i < triangles.Length; i++)
                    {
                        PointF[] tri = ToPoints(triangles[i]);
                        //Shrink the triangle so we can see each triangle
                        float inv = 1.0f/3.0f;
                        float cx= (tri[0].X + tri[1].X + tri[2].X) * inv;
                        float cy= (tri[0].Y + tri[1].Y + tri[2].Y) * inv;
                        Shrink(tri, 0, cx, cy);
                        Shrink(tri, 1, cx, cy);
                        Shrink(tri, 2, cx, cy);
                        Brush brush = brushes[i%brushes.Length];
                        g.FillPolygon(brush, tri);
                        //draw triangle index
                        string text = string.Format("{0}/{1}/{2}", triangleIndices[i][0], triangleIndices[i][1], triangleIndices[i][2]);
                        g.DrawString(text, Font, textBrush, cx, cy);
                    }
                }
                //draw index of each vertex
                PointF[] polygon = ToPoints(this.points);
                g.DrawPolygon(polygonPen, polygon);
                for (int i = 0; i < polygon.Length; i++)
                {
                    string text = string.Format("{0}", i);
                    g.DrawString(text, Font, textBrush, polygon[i]);
                }

                g.DrawLine(virtualLine, mousePos, polygon[0]);
                g.DrawLine(virtualLine, mousePos, polygon[polygon.Length - 1]);

            }

        }

        private void Shrink(PointF[] tri, int i, float cx, float cy)
        {
            float dx = tri[i].X - cx;
            float dy = tri[i].Y - cy;
            float inv = 5.0f/(float) Math.Sqrt(dx*dx + dy*dy);
            dx *= inv;
            dy *= inv;
            tri[i] = new PointF(tri[i].X - dx, tri[i].Y - dy);
        }

        private PointF[] ToPoints(IList<Vector4> vec)
        {
            Size size = Size;
            PointF[] ret = new PointF[vec.Count];
            for(int i = 0; i < ret.Length; i++)
                ret[i] = new PointF((float)vec[i].x * size.Width, (float)vec[i].y * Height);
            return ret;

        }
        private PointF[] ToPoints(IList<Vertex> vec)
        {
            Size size = Size;
            PointF[] ret = new PointF[vec.Count];
            for(int i = 0; i < ret.Length; i++)
                ret[i] = ToPoint(vec[i], size);
            return ret;

        }
        private PointF ToPoint(Vertex vec, Size size)
        {
            return new PointF((float)vec.X * size.Width, (float)vec.Y * Height);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mousePos = e.Location;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                Vertex pt = new Vertex(points.Count, e.X*1.0/Width, e.Y*1.0/Height);
                points.Add(pt);
                UpdateTriangles();
            }
            else
            {
                //erase last point
                if (points.Count > 0)
                {
                    points.RemoveAt(points.Count - 1);
                    UpdateTriangles();
                }
            }
        }

        public void UpdateTriangles()
        {
            DoTriangulate();
            Invalidate();
            if(TriangleUpdated != null)
                TriangleUpdated(this, new EventArgs());
        }

        /// <summary>
        /// Triangulate the polygon into a lot of triangles
        /// </summary>
        private void DoTriangulate()
        {
            triangles = null;
            if (points.Count <= 3)
                return;
            //convert to Vector4[]
            Vector4[] controlPoints = new Vector4[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                controlPoints[i] = new Vector4(points[i].X, points[i].Y, 0);
            }
            //triangulate the polygon
            triangleIndices = PolygonModifier.Triangulate(controlPoints);
            //save triangle vertex for later drawing.
            triangles = new Vector4[triangleIndices.Length][];
            for (int i = 0; i < triangleIndices.Length; i++)
            {
                int[] triangleFace = triangleIndices[i];


                Vector4[] triangle = triangles[i] = new Vector4[3];


                triangle[0] = controlPoints[triangleFace[0]];
                triangle[1] = controlPoints[triangleFace[1]];
                triangle[2] = controlPoints[triangleFace[2]];
            }
        }

    }
}
