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

namespace GcodeSimulator
{
    public partial class Form1 : Form
    {
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
                    richTextBox1.AppendText(text);
                    sr.Close();

                    //loop through all the lines in the textbox
                    foreach (string line in richTextBox1.Lines)
                    {
                        GetXCoordinate(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        static void GetXCoordinate(string line)
        {
            //xyz coordinate database
            CoordinateData cd = new CoordinateData();
            int xFlag = 0;

            //loop through each character in the line
            for (int i = 0; i < line.Length; i++)
            {
                //check for an X coordinate in the line
                if (line[i] == 'X' || line[i] == 'x')
                {
                    StringBuilder sb = new StringBuilder();
                    xFlag = 1;
                    //get value until a letter shows up, then store in database
                    for (int ii = i + 1; ii < line.Length; ii++)
                    {
                        if (Char.IsLetter(line[ii]))
                        {
                            cd.XCoord.Add(double.Parse(sb.ToString()));

                            break;
                        }
                        else
                        {
                            sb.Append(line[ii]);
                        }
                    }
                }
                //if (i == line.Length && xFlag == 0 && cd.XCoord.Count > 0)

                
            }
            if (xFlag == 0)
            {
                // cd.XCoord.Add(cd.XCoord[cd.XCoord.Count - 1]);
                Debug.WriteLine(cd.XCoord.Count);

            }
            foreach (float xCoord in cd.XCoord)
            {
                Debug.WriteLine(xCoord);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
