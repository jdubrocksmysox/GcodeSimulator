using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coordinates;
using System.Drawing.Drawing2D;

namespace GcodeSimulator
{
    public partial class Form1 : Form
    {
        CoordinateData cd = new CoordinateData();
        int x = 0;
        double zoom = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();


            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);

                    String text = sr.ReadToEnd();
                    textBox1.Clear();
                    textBox1.AppendText(text);
                    sr.Close();

                    //loop through all the lines in the textbox

                    cd.X.Add(0);
                    cd.Y.Add(0);
                    cd.Z.Add(0);

                    foreach (string line in textBox1.Lines)
                    {
                        GetXCoordinate(line, cd);
                    }

                    //for (int i = 0; i < cd.XCoord.Count; i++)
                    //{
                    //    Debug.WriteLine("X{0} Y{1} Z{2}", cd.XCoord[i], cd.YCoord[i], cd.ZCoord[i]);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        static void GetXCoordinate(string line, CoordinateData cd)
        {
            //xyz coordinate database

            int xFlag = 0;
            int yFlag = 0;
            int zFlag = 0;

            //loop through each character in the line
            for (int i = 0; i < line.Length; i++)
            {
                //check for an X coordinate in the line
                if (line[i] == 'X' || line[i] == 'x')
                {
                    StringBuilder sb = new StringBuilder();

                    //get value until a letter shows up, then store in database
                    for (int ii = i + 1; ii < line.Length; ii++)
                    {
                        if (Char.IsLetter(line[ii]) && xFlag == 0 || ii == line.Length - 1 && xFlag == 0)
                        {
                            cd.X.Add(double.Parse(sb.ToString()));
                            xFlag = 1;
                        }
                        else
                        {
                            sb.Append(line[ii]);
                        }
                    }
                }
                if (i == line.Length - 1 && xFlag == 0)
                {
                    cd.X.Add(cd.X[cd.X.Count - 1]);
                }
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 'Y' || line[i] == 'y')
                {
                    StringBuilder sb = new StringBuilder();

                    //get value until a letter shows up, then store in database
                    for (int ii = i + 1; ii < line.Length; ii++)
                    {
                        if (Char.IsLetter(line[ii]) && yFlag == 0 || ii == line.Length - 1 && yFlag == 0)
                        {
                            cd.Y.Add(double.Parse(sb.ToString()));
                            yFlag = 1;
                        }
                        else
                        {
                            sb.Append(line[ii]);
                        }
                    }
                }
                if (i == line.Length - 1 && yFlag == 0)
                {
                    cd.Y.Add(cd.Y[cd.Y.Count - 1]);
                }
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 'Z' || line[i] == 'z')
                {
                    StringBuilder sb = new StringBuilder();

                    //get value until a letter shows up, then store in database
                    for (int ii = i + 1; ii < line.Length; ii++)
                    {
                        if (Char.IsLetter(line[ii]) && zFlag == 0 || ii == line.Length - 1 && zFlag == 0)
                        {
                            cd.Z.Add(double.Parse(sb.ToString()));
                            zFlag = 1;
                        }
                        else
                        {
                            sb.Append(line[ii]);
                        }
                    }
                }
                if (i == line.Length - 1 && zFlag == 0)
                {
                    cd.Z.Add(cd.Z[cd.Z.Count - 1]);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int centerX = panel1.Width / 2 - x;
            int centerY = panel1.Height / 2;

            for (int i = 0; i < cd.X.Count - 1; i++)
            {
                int x1 = Convert.ToInt32((cd.X[i] * 100) * zoom);
                int y1 = Convert.ToInt32((cd.Y[i] * 100) * zoom);
                int x2 = Convert.ToInt32((cd.X[i + 1] * 100) * zoom);
                int y2 = Convert.ToInt32((cd.Y[i + 1] * 100) * zoom);

                g.DrawLine(
            new Pen(Color.DeepSkyBlue, 2f),
            new Point(x1 + centerX, (y1 + centerY)),
            new Point(x2 + centerX, (y2 + centerY)));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            x = x + 10;
            panel1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zoom = zoom + 0.1;
            panel1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zoom = zoom - 0.1;
            panel1.Refresh();
        }
    }
}
