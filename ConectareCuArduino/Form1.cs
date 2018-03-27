using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConectareCuArduino
{
    public partial class Form1 : Form
    {
        private bool isPortOpened = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            serialPort1.NewLine="\r\n";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isPortOpened)
            {
                serialPort1.Close();
                button1.Text = "Connect";
                isPortOpened = false;
            }else 
            {
                serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                serialPort1.Open();
                button1.Text = "Disconnect";
                isPortOpened = true;
            }

        }
        private void showData(string temp,string state)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, string>(showData), new object[] { temp, state });
                return;
            }
            textBox1.Text = temp;
            textBox2.Text = state;

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            String text=serialPort1.ReadLine();
            string[] data=text.Split(';');
            showData(data[0], data[1]);
        }
    }
}
