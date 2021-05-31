using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.IO;
using System.Text;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using MinePinger;

namespace MineDownloader
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Pinging()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("you have to put your subnet first to scan your network", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    for (int i = 1; i < 255; i++)
                    {
                        string IPs = textBox1.Text + "." + i;
                        Ping PingDevices = new Ping();
                        PingReply TheReply = PingDevices.Send(IPs);
                        if (TheReply.Status.ToString() == "Success")
                        {
                            listView1.Items.Add(TheReply.Address.ToString() + " are alive.");
                        }
                    }
                }
                catch
                {

                }
            }
        }
                

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            Thread PingingThread = new Thread(new ThreadStart(Pinging));
            PingingThread.Name = "PingingThread";
            PingingThread.Priority = ThreadPriority.AboveNormal;
            PingingThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Environment.UserName.Contains("shit"))
            {
                MessageBox.Show("Hello NiceTry, this program contains pinging loop, Network Discovery, retrives system information and disk, including Adapters.", "What is That?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else if(Environment.UserName.Contains("fuck"))
            {
                MessageBox.Show("Hello NiceTry, this program contains pinging loop, Network Discovery, retrives system information and disk, including Adapters.", "What is That?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Hello " + Environment.UserName + ", this program contains pinging loop, Network Discovery, retrives system information and disk, including Adapters.", "What is That?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new SpecificAddress().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new NetworksForm().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new PCInformationForm().ShowDialog();
        }
    }
}
