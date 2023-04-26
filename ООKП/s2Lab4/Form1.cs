using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace ookplab4winf
{
    public partial class Form1 : Form
    {
        string currentTimeZone;
        string currentFormat;
        public Form1()
        {
            InitializeComponent();
            timer1.Start();

            List<string> timezonesList = new List<string>();
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                timezonesList.Add(z.DisplayName.Substring(0, 12) + z.Id);
            }
            listBox1.DataSource = timezonesList;

            List<string> formatsList = new List<string>();
            formatsList.Add("MM/dd/yyyy h:mm tt");
            formatsList.Add("MM/dd/yyyy HH:mm:ss");
            formatsList.Add("dddd, dd MMMM yyyy HH:mm:ss");
            formatsList.Add("dddd, dd MMMM yyyy h:mm tt");
            formatsList.Add("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss");
            listBox2.DataSource = formatsList;

            currentTimeZone = "FLE Standard Time";
            currentFormat = "MM/dd/yyyy HH:mm:ss";
        }

        public string SendMessageFromSocket(int port, string timezone, string format = "")
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            
            byte[] bytes = new byte[1024];
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            sender.Connect(ipEndPoint);
            string message = timezone + "|" + format;
            byte[] msg = Encoding.UTF8.GetBytes(message);
            int bytesSent = sender.Send(msg);
            int bytesRec = sender.Receive(bytes);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

            return Encoding.UTF8.GetString(bytes, 0, bytesRec);
        }

        private void listBox1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                currentTimeZone = listBox1.SelectedItem.ToString().Substring(12);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.label1.Text = SendMessageFromSocket(11000, currentTimeZone, currentFormat);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                currentFormat = listBox2.SelectedItem.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessageFromSocket(11000, "End");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
            this.Close();
        }
    }
}
