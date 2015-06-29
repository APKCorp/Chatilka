using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadanie
{
    public partial class InsertForm : Form
    {
        public InsertForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 Insert1 = new Form3();
            Insert1.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void InsertForm_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd";
            //  This line runs a command called "net view"
            //  which is a built in windows command that returns all the shares
            //  on a network
            proc.StartInfo.Arguments = "/C Chcp 866";
            proc.StartInfo.Arguments = "/C net view";
            // This property redirects the output of the command ran
            // to the StandardOutput object we use later
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;

            // This tells the program to show the command window or not
            // set to true to hide the window
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();

            // Sets all the output into a string
            string data = proc.StandardOutput.ReadToEnd();
            var fromEncodind = Encoding.UTF8;//из какой кодировки
            var bytes = fromEncodind.GetBytes(data);
            var toEncoding = Encoding.GetEncoding(1251);//в какую кодировку
            data = toEncoding.GetString(bytes);   
            int start = 0;
            int stop = 0;

            // This parses through the output string
            // and grabs each share and outputs it.
            // you can save the strings into an array and add 
            // them to a list box or something if you wanted to.
            string rez;
            while (true)
            {
                start = data.IndexOf('\\', start);
                if (start == -1)
                    break;
                stop = data.IndexOf('\n', start);
                rez=data.Substring(start, stop - start);
                comboBox1.Items.Add(rez);
                start = stop;
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {   
                comboBox1.Visible = true;
                label2.Visible = true;
                button2.Visible = true;
                IPtextBox.Enabled = false;
                button1.Enabled = false;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            label2.Visible = false;
            button2.Visible = false;
            IPtextBox.Enabled = true;
            button1.Enabled = true;

        }
    }
}
