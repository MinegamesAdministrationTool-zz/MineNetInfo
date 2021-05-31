using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Management;
using System.Drawing;
using System.Linq;
using Microsoft.Win32;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MinePinger
{
    public partial class PCInformationForm : Form
    {
        public PCInformationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey GetSystemInfo = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                var GetOperatingSystem = GetSystemInfo.GetValue("ProductName");
                var GetSystemBuild = GetSystemInfo.GetValue("CurrentBuild");
                var RegisteredOwner = GetSystemInfo.GetValue("RegisteredOwner");

                OperatingSystem GetWindows = Environment.OSVersion;
                listBox1.Items.Clear();
                listBox1.Items.Add("Username: " + Environment.UserName);
                listBox1.Items.Add("Domain Name: " + Environment.UserDomainName);
                if (Environment.Is64BitOperatingSystem == true)
                {
                    listBox1.Items.Add("Operating System Type: 64-bit");
                }
                else
                {
                    listBox1.Items.Add("Operating System Type: 32-bit");
                }
                listBox1.Items.Add("System Directory: " + Environment.SystemDirectory);
                listBox1.Items.Add("Windows Build: " + GetSystemBuild);
                listBox1.Items.Add("Operating System: " + GetOperatingSystem);
                listBox1.Items.Add("Registered Owner: " + RegisteredOwner);
                ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(objectQuery);
                ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
                foreach (ManagementObject managementObject in managementObjectCollection)
                {
                    listBox1.Items.Add("Total RAM in KB: " + managementObject["TotalVisibleMemorySize"]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + ", Please Report that error to my github.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                DriveInfo[] GetDrives = DriveInfo.GetDrives();
                foreach (DriveInfo GetDrivesInformation in GetDrives)
                {
                    listBox1.Items.Add("Disk Basic Info of " + GetDrivesInformation + ", Type: " + GetDrivesInformation.DriveType + ", Drive Format: " + GetDrivesInformation.DriveFormat);
                }

                foreach (DriveInfo GetDrivesInformation in GetDrives)
                {
                    listBox1.Items.Add("Disk Storage Info of " + GetDrivesInformation.Name + ": Free Space in bytes: " + GetDrivesInformation.AvailableFreeSpace + ", Total Space in bytes: " + GetDrivesInformation.TotalSize);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + ", Please Report that error to my github.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}
