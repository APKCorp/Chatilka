using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        //rec = new Thread(new ThreadStart(Receive));
        
        public void ShowComp()
        {
            textBoxSend.Visible = true;
            textBoxReceive.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void fMain_Load(object sender, EventArgs e)
        {
        
            // Запускаем отдельный поток для асинхронной работы приложения
            // во время приема сообщений
            stopReceive = false;
            rec = new Thread(new ThreadStart(Receive));
            rec.Start();
        }

        Thread rec = null;
        UdpClient udp = new UdpClient(15000);
        bool stopReceive = false;

        // Функция извлекающая пришедшие сообщения
        // работающая в отдельном потоке.
        void Receive()
        {
            try
            {
                while (true)
                {

                    IPEndPoint ipendpoint = null;
                    byte[] message = udp.Receive(ref ipendpoint);
                    //ShowMessage(Encoding.Default.GetString(message));

                    // Если дана команда остановить поток, останавливаем бесконечный цикл.
                    if (stopReceive == true) break;
                }  
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
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

        private void button7_Click(object sender, EventArgs e)
        {
               UdpClient udp = new UdpClient();

   // Указываем адрес отправки сообщения
   IPAddress ipaddress = IPAddress.Parse(textBoxAddress.Text);
   IPEndPoint ipendpoint = new IPEndPoint(ipaddress, 15000);

   // Формирование оправляемого сообщения и его отправка.
   // Сеть "понимает" только поток байтов и ей безразличны
   // объекты классов, строки и т.п. Поэтому преобразуем текстовое
   /// сообщение в поток байтов.
   byte[] message = Encoding.Default.GetBytes(textBoxSend.Text);
   int sended = udp.Send(message, message.Length, ipendpoint);

   // Если количество переданных байтов и предназначенных для 
   // отправки совпадают, то 99,9% вероятности, что они доберутся 
   // до адресата.
   if (sended == message.Length)
   {
      // все в порядке
      textBoxSend.Text = "";
   }
      

   // После окончания попытки отправки закрываем UDP соединение,
   // и освобождаем занятые объектом UdpClient ресурсы.
   udp.Close();

        }
        void StopReceive()
        {
            stopReceive = true;
            if (udp != null) udp.Close();
            if (rec != null) rec.Join();
        }


        // Блок кода предоставляющий безопасный доступ к членам класса из разных потоков
        delegate void ShowMessageCallback(string message);
        void ShowMessage(string message)
        {
            if (textBoxReceive.InvokeRequired)
            {
                ShowMessageCallback dt = new ShowMessageCallback(ShowMessage);
                Invoke(dt, new object[] { message });
            }
            else
            {
                textBoxReceive.Text = message;
            }
        }
    } 
}
