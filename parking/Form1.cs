using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;

using System.Data.OleDb;

using System.Diagnostics;

using System.IO;
using System.IO.Ports;

using System.Collections;
using System.Media;
using System.Runtime.InteropServices;

using System.Net.NetworkInformation;
using System.Windows.Threading;
using DeviceDriver;

namespace parking
{
    public partial class Form1 : Form
    {       

        public Form1()
        {
            InitializeComponent();
        }       

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyPreview = true;

             InvokeSafeAction(ListDevices);    
        }        

        private void InvokeSafeAction(Action action)
        {
            var actionWrapper = new Action(() =>
            {
                action();

            });

            Dispatcher.CurrentDispatcher.BeginInvoke(actionWrapper);

        }

        private void ListDevices()
        {
            using (var relayController = new RelayController())
            {
                //var devices = relayController.ListDevices();
                var devices = relayController.ListDevices();

                if (devices.Count == 1)
                {

                    // o bước này khi cắm mạch vào thì biến devices.Count = 1
                    // khi rút mạch vào thì biến devices.Count = 0
                    MessageBox.Show("co ket noi");
                    comboBox1.Items.Add(devices);
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                     MessageBox.Show("k ket noi");
                }             


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                InvokeSafeAction(LoadDevice);
            }
        }

        private void LoadDevice()
        {
            IRelayDeviceInfo device = GetCurrentDevice();           
        }

        private IRelayDeviceInfo GetCurrentDevice()
        {            
            return comboBox1.SelectedItem as IRelayDeviceInfo;            
        }









        private void button3_Click(object sender, EventArgs e)
        {
            InvokeSafeAction(ListDevices);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            InvokeSafeAction(CloseAllChannels);
        }

        private void CloseAllChannels()
        {
            IRelayDeviceInfo device = GetCurrentDevice();
            if (device == null) return;
            using (var relayController = new RelayController())
            {
                relayController.ConnectDevice(device);
                relayController.CloseAllChannels(device);
            }
        }  

        private void OpenAllChannels()
        {          

            IRelayDeviceInfo device = GetCurrentDevice();
            if (device == null) return;
            using (var relayController = new RelayController())
            {
                relayController.ConnectDevice(device);
                relayController.OpenAllChannels(device);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            InvokeSafeAction(OpenAllChannels);
        }

      
    }
}
