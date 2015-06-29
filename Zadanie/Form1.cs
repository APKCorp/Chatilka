using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadanie
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }
        public void ShowComp()
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void fMain_Load(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void добавитьКонтактToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertForm Insert = new InsertForm();
            Insert.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
