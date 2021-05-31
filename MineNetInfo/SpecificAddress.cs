using MinePinger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineDownloader
{
    public partial class SpecificAddress : Form
    {
        private bool TerminateTheThread;
        public SpecificAddress()
        {
            InitializeComponent();
        }

        private void PingLoop()
        {
            while (true)
            {
                Task.Delay(1300).Wait();
                Ping SeeDevices = new Ping();
                PingReply TheReply = SeeDevices.Send(textBox1.Text);
                if (TheReply.Status.ToString() == "Success")
                {
                    listBox1.Items.Add(TheReply.Address + " are alive.");
                }
                else
                {
                    listBox1.Items.Add(TheReply.Address + " are dead or not available.");
                }

                if (TerminateTheThread == true)
                {
                    listBox1.Items.Clear();
                    break;
                }
            }
        }

        private void Pinging()
        {
            try
            {
                TerminateTheThread = false;
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Don't leave the IP Address textbox empty!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        listBox1.Items.Clear();
                        Thread PingLoopButton = new Thread(new ThreadStart(PingLoop));
                        PingLoopButton.Priority = ThreadPriority.AboveNormal;
                        PingLoopButton.Name = "LoopPing";
                        PingLoopButton.Start();
                        button1.Enabled = false;
                        checkBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else
                    {
                        Ping SeeDevices = new Ping();
                        PingReply TheReply = SeeDevices.Send(textBox1.Text);
                        if (TheReply.Status.ToString() == "Success")
                        {
                            listBox1.Items.Add(TheReply.Address + " are alive.");
                        }
                        else
                        {
                            listBox1.Items.Add(TheReply.Address + " are dead or not available.");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pinging();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Don't leave the IP Address textbox empty!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    TcpClient hi = new TcpClient();
                    hi.Connect(textBox1.Text, 80);
                    if(hi.Connected == true)
                    {
                        Console.WriteLine("Connected.");
                    }
                    listBox1.Items.Add(hi.GetStream());
                }
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && button1.Enabled == false && textBox1.Enabled == false)
            {
                TerminateTheThread = true;
                checkBox1.Checked = false;
                checkBox1.Enabled = true;
                button1.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                MessageBox.Show("nothing to stop", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}
