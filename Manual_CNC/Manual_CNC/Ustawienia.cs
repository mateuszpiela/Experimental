using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Manual_CNC
{
    public partial class Ustawienia : Form
    {
        bool uzytkownik1;
        bool uzytkownik2;
        bool changed;
        public string pozmode;
        private Sterowanie other;

        public Ustawienia()
        {
            InitializeComponent();
            changed = false;
        }

        public Ustawienia(Sterowanie other)
        {
            this.other = other;
            InitializeComponent();
            if(other.PozMode == "G90")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else if(other.PozMode == "G91")
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(uzytkownik1)
            {
                radioButton2.Checked = false;
                radioButton1.Checked = true;
                other.PozMode = "G90";
                other.Posuw = textBox1.Text;
                changed = true;
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(uzytkownik2)
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
                other.PozMode = "G91";
                changed = true;
            }
        }

        private void RadioButton1_MouseDown(object sender, MouseEventArgs e)
        {
            uzytkownik1 = true;
            uzytkownik2 = false;
        }

        private void radioButton2_MouseDown(object sender, MouseEventArgs e)
        {
            uzytkownik1 = false;
            uzytkownik2 = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ustawienia_FormClosing(object sender, FormClosingEventArgs e)
        {
            other.Posuw_Update(textBox1.Text);
            if (changed)
            {
                other.WSP_Update();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int x;
            if (textBox1.Text.Contains("-"))
            {
                Int32.TryParse(textBox1.Text.ToString(), out x);
                if (x < 0)
                {
                    MessageBox.Show("NIE WOLNO SCHODZIĆ PONIŻEJ ZERA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Text = "0";
                }
            }
        }
    }
}
