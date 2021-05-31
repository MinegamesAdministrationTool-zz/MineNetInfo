using System;
using System.Collections.Generic;
using System.ComponentModel;
using MineDownloader;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinePinger
{
    public partial class NetworksForm : Form
    {
        public NetworksForm()
        {
            InitializeComponent();
        }

        private void NetworksForm_Load(object sender, EventArgs e)
        {
            var GetInterface = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface GetInterfaces in GetInterface)
            {
                PhysicalAddress GetMAC = GetInterfaces.GetPhysicalAddress();
                ListViewItem ToColumns = new ListViewItem(GetInterfaces.Name + " " + "{" + GetInterfaces.Description + "}");
                ToColumns.SubItems.Add(GetInterfaces.GetPhysicalAddress().ToString());
                if (GetInterfaces.OperationalStatus == OperationalStatus.Up)
                {
                    ToColumns.SubItems.Add("Working");
                }
                else
                {
                    ToColumns.SubItems.Add("Disabled");
                }

                if (GetInterfaces.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    ToColumns.SubItems.Add("Wireless Adapter");
                }
                else
                {
                    ToColumns.SubItems.Add(GetInterfaces.NetworkInterfaceType.ToString());
                }

                if (GetInterfaces.GetIPProperties().GetIPv4Properties().IsDhcpEnabled == true)
                {
                    ToColumns.SubItems.Add("Enabled");
                }
                else
                {
                    ToColumns.SubItems.Add("Disabled");
                }

                ToColumns.SubItems.Add(GetInterfaces.Id.ToString());

                listView1.Items.Add(ToColumns);
                timer1.Enabled = true;
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Maximized)
            {
                listView1.Dock = DockStyle.Fill;
            }

            if(WindowState == FormWindowState.Normal)
            {
                listView1.Dock = DockStyle.None;
            }
        }
    }
}
