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

namespace Manual_CNC
{
    public partial class Wybor_COM : Form
    {
        public Wybor_COM()
        {
            InitializeComponent();
            MessageBox.Show("Autor programu nie odpowiada za wadliwe działania programu oraz nie wspiera już tego Programu !", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
        }
        public bool VIRTUAL;
        public string com;
        public string baudrate;
        private void Button1_Click(object sender, EventArgs e)
        { 
            if(comboBox1.SelectedItem.ToString() == "VIRTUAL")
            {
                VIRTUAL = true;
                baudrate = "0";
            }
            else
            {
                VIRTUAL = false;
            }
            com = comboBox1.SelectedItem.ToString();
            if (comboBox2.Text != "")
            {
                baudrate = comboBox2.SelectedItem.ToString();
            }
            Sterowanie fr = new Sterowanie(com, baudrate,VIRTUAL,this);
            fr.Show();
            this.Hide();

        }
        public void Display()
        {
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
