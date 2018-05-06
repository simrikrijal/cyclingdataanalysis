using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows.Forms;
using MaterialSkin.Controls;

namespace CyclingDataAnalysis
{
    public partial class InputFiles : MaterialForm
    {
        string fn1, fn2;
        List<string> filenames = new List<string>();
        public InputFiles()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "hrm|*.hrm|All|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                fn1 = open.FileName; // name of the browsed file 

            }
            filenames.Add(fn1);
            fname1.Text = Path.GetFileName(fn1);
            fname1.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "hrm|*.hrm|All|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                fn2 = open.FileName; // name of the browsed file 

            }
            if (fn2.Equals(fn1))
            {
                MessageBox.Show("Cannot insert two files of same name try again.");
            }
            else
            {
                filenames.Add(fn2);
                fname2.Text = Path.GetFileName(fn2);
                fname2.Visible = true;
                button3.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                MultipleFile mfile = new MultipleFile(filenames);
                mfile.Show();
            
        }

        private void InputFiles_Load(object sender, EventArgs e)
        {
           
          
        }
    }
}
