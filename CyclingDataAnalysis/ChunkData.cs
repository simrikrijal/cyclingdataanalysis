using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment1_Polar
{
    public partial class ChunkData : Form
    {
        int chunkNo;
        public ChunkData()
        {
            InitializeComponent();
        }

        private void ChunkData_Load(object sender, EventArgs e)
        {
            OK.DialogResult = DialogResult.OK;
            chunk.Text = "Select the Number of Portions :";
            label1.Text = "CHUNK THE DATA ";
        }

        private void chunk_Click(object sender, EventArgs e)
        {
           
        }
        public int chunkGet { get; set; }
      
        private void button1_Click(object sender, EventArgs e)
        {
           
        

        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chunkValue_SelectedIndexChanged(object sender, EventArgs e)
        {
           // chunkNo =Convert.ToInt32(chunkValue.SelectedValue.ToString()); ;
        }

        private void OK_Click(object sender, EventArgs e)
        {

             this.chunkGet = Convert.ToInt32(inputValue.Text);
           // this.chunkGet = Convert.ToInt32(i);
            this.Close();


        }
        string i;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           i = comboBox1.SelectedValue.ToString();
        }
    }
}
