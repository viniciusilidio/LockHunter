using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void ToogleUI ()
        {
            textBox1.Enabled = !textBox1.Enabled;
            Execute.Enabled = !Execute.Enabled;
        }
        
        public void UpdateProgressBar (int current, int total)
        {
            progressBar1.Value = (int)((current / (float)total) * 100.0f);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog () == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;             
            }
        }

        private void Execute_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Execute(textBox1.Text);
        }
    }
}
