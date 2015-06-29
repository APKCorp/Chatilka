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
        public int btnCount = 0; //количество кнопок Чат
        public int lblCount = 0; //Количество имеющихся контактов
        public Label[] lblKontMass;
        public Button[] btnKontMass;
        public InsertForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 Insert1 = new Form3();
            Insert1.Show();
        }

        public void CreateComp(Form formMain, String name)
        {
            lblKontMass = new Label[30];
            btnKontMass = new Button[30];

            foreach (Control value in formMain.Controls)
            {
                if (value is Label)
                {
                    //перемення проверяющая является ли эта кнопка или lable входящими в инфу о контакте
                    int Kont;
                    Kont = value.Name.IndexOf("Kont");
                    if (Kont > 0)
                    {
                        lblKontMass[lblCount] = new Label() { Name = "lblKont" + lblCount.ToString(), Location = new Point(value.Location.X, value.Location.Y) };
                        lblCount++;
                    }
                }

                if (value is Button)
                {
                    //перемення проверяющая является ли эта кнопка или lable входящими в инфу о контакте
                    int Kont;
                    Kont = value.Name.IndexOf("Kont");

                    if (Kont > 0)
                    {
                        btnKontMass[btnCount] = new Button() { Name = "btnKont" + btnCount.ToString(), Location = new Point(value.Location.X, value.Location.Y), Text = value.Text };
                        btnCount++;
                    }
                }
            }
                //Создание lable
                Label lb = new Label();
                lb.Name = "lblKont" + lblCount;
                lb.Visible = true;
                if (lblCount > 0)
                {
                    lb.Location = new Point(lblKontMass[lblCount - 1].Location.X, lblKontMass[lblCount - 1].Location.Y + 15);
                }
                else
                {
                    lb.Location = new Point(16, 34);
                }
                lb.Text = name;
                formMain.Controls.Add(lb);

                //Создание кнопки
                Button bt = new Button();
                bt.Name = "btnKont" + btnCount;
                bt.Visible = true;
                if (btnCount > 0)
                {
                    bt.Location = new Point(btnKontMass[btnCount - 1].Location.X, btnKontMass[btnCount - 1].Location.Y + 15);
                }
                else
                {
                    bt.Location = new Point(139, 31);
                }
                bt.Text = "Чат";
                formMain.Controls.Add(bt);
            
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

 

        private void button2_Click(object sender, EventArgs e)
        {        
            fMain otherForm = new fMain();
            int count=otherForm.Controls.Count;
            foreach (Control value in otherForm.Controls)
            {
                if (value is Label)
                {
                    Object selectedItem = comboBox1.SelectedItem;
                    if(value.Text == selectedItem.ToString())
                        {
                            MessageBox.Show("Данный пользователь находится у вас в контактах","Внимание",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        }
                   // value.Tag = count;
                 }
            }

            CreateComp(otherForm, comboBox1.Items[comboBox1.SelectedIndex].ToString());
            otherForm.Show();
           // otherForm.Close();
            Close();

        }
    }
}
