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
            List<float> xCoords = new List<float>();
            List<float> yCoords = new List<float>();
            List<float> zCoords = new List<float>();

            //loop through each character in the line
            for (int i = 0; i < line.Length; i++)
            {
                //int xFlag = 0;
                
                //check for an X coordinate in the line
                if (line[i] == 'X')
                {
                    StringBuilder sb = new StringBuilder();
                    //get value until a letter shows up, then store in database
                    for (int ii = i + 1; ii < line.Length; ii++)
                    {                       
                        if (Char.IsLetter(line[ii]))
                        {
                            xCoords.Add(float.Parse(sb.ToString()));
                            //xFlag = 1;
                            break;
                        }
                        else
                        {                            
                            sb.Append(line[ii]);                            
                        }
                    }                    
                }

                //if (i == line.Length - 1 && xFlag == 0 && xCoords.Count > 0)
                //{
                //    xCoords.Add(xCoords[xCoords.Count - 1]);
                //}
            }

            foreach (float xCoord in xCoords)
            {
                Debug.WriteLine(xCoord);
            }
        }
    }
}
