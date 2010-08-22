using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeometricalObjects;

namespace TriangleGUI
{
    public partial class MainForm : Form
    {
        private Triangle triangle;

        public MainForm()
        {
            InitializeComponent();
            isScaleneLbl.Text = string.Empty;
            isIsolescenesLbl.Text = string.Empty;
            isEquilateralLbl.Text = string.Empty;
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
         
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            var vertex = new Point[3];
            var coordinates = textBox1.Text;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {   

                var points = coordinates.Split(new char[] { ';' });
                int i = 0;
                foreach (var point in points)
                {
                    var coords = point.Split(new char[] { ',' });
                    vertex[i++] = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
                }
                triangle = new Triangle(vertex[0], vertex[1], vertex[2]);
                isScaleneLbl.Text = triangle.IsScalene() ? "yes" : "no";
                isIsolescenesLbl.Text = triangle.IsIsosceles() ? "yes" : "no";
                isEquilateralLbl.Text = triangle.IsEquilateral() ? "yes" : "no";
                Refresh();

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            if (triangle != null)
            {
                triangle.Render(splitContainer1.Panel2.CreateGraphics());
            }
        }
    }
}
