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
        private Graphics graphics;

        public MainForm()
        {
            InitializeComponent();
            isScaleneLbl.Text = string.Empty;
            isIsolescenesLbl.Text = string.Empty;
            isEquilateralLbl.Text = string.Empty;
            graphics = splitContainer1.Panel2.CreateGraphics();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var sides = textBox2.Text;
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    var points = sides.Split(new [] { ' ' });

                    var side1 = int.Parse(points[0]);
                    var side2 = int.Parse(points[1]);
                    var side3 = int.Parse(points[2]);
                    
                    triangle = new Triangle(side1, side2, side3);
                    isScaleneLbl.Text = triangle.IsScalene() ? "yes" : "no";
                    isIsolescenesLbl.Text = triangle.IsIsosceles() ? "yes" : "no";
                    isEquilateralLbl.Text = triangle.IsEquilateral() ? "yes" : "no";
                    Refresh();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Couldn't parse input!");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            if (triangle != null)
            {
                triangle.Render(graphics);
            }
        }
    }
}
