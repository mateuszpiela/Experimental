using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;
#pragma warning disable 0649

namespace Manual_CNC
{

    public partial class Sterowanie : Form
    {
        public string COM;
        public string BAUDRATE;
        public int INTBD;
        public bool VIRTUAL;
        SerialPort port;

        public string TrybPosuwu;
        
        public string PozMode;
        public string Posuw;
        public string dziesietne;
        //WSPOLRZEDNE PROGRAMU
        double softx;
        double softy;
        int softz;

        //WSPOLRZEDNE MASZYNY
        double machx;
        double machy;
        int machz;

        private Wybor_COM other;

        public Sterowanie(string com,string baudrate,bool Virtual,Wybor_COM other)
        {
            this.other = other;
            InitializeComponent();
            Posuw = "50";
            PozMode = "G90";
            VIRTUAL = Virtual;
            dziesietne = "0.100";
            softx = 0.000;
            softy = 0.000;
            softz = 0;
            machx = 0.000;
            machy = 0.000;
            machz = 0;
            checkBox5.Checked = true;
            TrybPosuwu = "G00";
            if (Virtual)
            {
                MessageBox.Show("Uwaga uruchromiono tryb Demonstracyjny funkcje typu połączenie COM zostaną wyłączone !", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                COM = com;
                BAUDRATE = baudrate;
                Int32.TryParse(BAUDRATE, out INTBD);
                port = new SerialPort(COM,INTBD);
                //port.NewLine = "\r\n";
                port.Open();
                if (port.IsOpen)
                {
                    port.WriteLine("G54");
                    port.WriteLine(PozMode);
                    port.WriteLine("F" + Posuw);
                }
                checkBox1.Checked = true;
            }



        }
        public void Posuw_Update(string pos)
        {
            if (!VIRTUAL)
            {
                port.WriteLine("F" + pos);
                port.ReadLine();
            }
        }
        public void WSP_Update()
        {
            //Zerowanie maszyny 0 po zmianie Trybu 
            machx = 0.000;
            machy = 0.000;
            machz = 0;
            if (!VIRTUAL)
            {
                port.WriteLine(PozMode);
            }

            if(PozMode == "G91")
            {
                trackBar1.Maximum = 1;
                trackBar1.Minimum = -1;
                trackBar1.Value = 0;
            }
            else
            {
                trackBar1.Maximum = 40;
                trackBar1.Minimum = -40;
                trackBar1.Value = 0;
            }

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (!VIRTUAL)
            {
                if (port.IsOpen)
                {
                    port.Close();
                    checkBox1.Checked = false;
                }
                else
                {
                    port.PortName = COM;
                    port.BaudRate = INTBD;
                    port.Open();
                    checkBox1.Checked = true;
                }
            }
        }
        public void Gora()
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            switch (dziesietne)
            {
                    case "0.100":
                    softx = softx - 0.100;
                    softx = Math.Round(softx, 3);
                    label5.Text = softx.ToString();
                    break;
                    case "1":
                    softx = softx - 1;
                    softx = Math.Round(softx, 1);
                    label5.Text = softx.ToString();
                    break;
                    case "10":
                    softx = softx - 10;
                    softx = Math.Round(softx, 1);
                    label5.Text = softx.ToString();
                    break;
            }
            if (PozMode == "G90")
            {
                machx = softx;
                label11.Text = machx.ToString();
                if(!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " X" + machx);
                }
            }
            else if (PozMode == "G91")
            {
                switch (dziesietne)
                {
                    case "0.100":
                        machx = machx - 0.100;
                        label11.Text = machx.ToString();
                        break;
                    case "1":
                        machx = machx - 1;
                        label11.Text = machx.ToString();
                        break;
                    case "10":
                        machx = machx - 10;
                        label11.Text = machx.ToString();
                        break;
                }
                if(!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " X" + machx);

                }
                label11.Text = "0.000";
                machx = 0.000;
            }
            
        }
        public void Prawo()
        {
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();
            }
            switch (dziesietne)
            {
                case "0.100":
                    softy = softy + 0.100;
                    softy = Math.Round(softy, 3);
                    label6.Text = softy.ToString();
                    break;
                case "1":
                    softy = softy + 1;
                    softy = Math.Round(softy, 1);
                    label6.Text = softy.ToString();
                    break;
                case "10":
                    softy = softy + 10;
                    softy = Math.Round(softy, 1);
                    label6.Text = softy.ToString();
                    break;
            }
            if (PozMode == "G90")
            {
                machy = softy;
                label12.Text = machy.ToString();
                if (!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " Y" + machy);
                }
            }
            else if (PozMode == "G91")
            {
                switch (dziesietne)
                {
                    case "0.100":
                        machy = machy + 0.100;
                        label12.Text = machy.ToString();
                        break;
                    case "1":
                        machy = machy + 1;
                        label12.Text = machy.ToString();
                        break;
                    case "10":
                        machy = machy + 10;
                        label12.Text = machy.ToString();
                        break;
                }
                if (!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " Y" + machy);
                }
                label12.Text = "0.000";
                machy = 0.000;
            }
        }
        public void Dol()
        {
            if (!backgroundWorker3.IsBusy)
            {
                backgroundWorker3.RunWorkerAsync();
            }
            switch (dziesietne)
            {
                case "0.100":
                    softx = softx + 0.100;
                    softx = Math.Round(softx, 3);
                    label5.Text = softx.ToString();
                    break;
                case "1":
                    softx = softx + 1;
                    softx = Math.Round(softx, 1);
                    label5.Text = softx.ToString();
                    break;
                case "10":
                    softx = softx + 10;
                    softx = Math.Round(softx, 1);
                    label5.Text = softx.ToString();
                    break;
            }
            if (PozMode == "G90")
            {
                machx = softx;
                label11.Text = machx.ToString();
                if (!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " X" + machx);
                }
            }
            else if (PozMode == "G91")
            {
                switch (dziesietne)
                {
                    case "0.100":
                        machx = machx + 0.100;
                        label11.Text = machx.ToString();
                        break;
                    case "1":
                        machx = machx + 1;
                        label11.Text = machx.ToString();
                        break;
                    case "10":
                        machx = machx + 10;
                        label11.Text = machx.ToString();
                        break;
                }
                if (!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " X" + machx);
                }
                label11.Text = "0.000";
                machx = 0.000;
            }

        }
        public void Lewo()
        {
            if (!backgroundWorker4.IsBusy)
            {
                backgroundWorker4.RunWorkerAsync();
            }
            switch (dziesietne)
            {
                case "0.100":
                    softy = softy - 0.100;
                    softy = Math.Round(softy, 3);
                    label6.Text = softy.ToString();
                    break;
                case "1":
                    softy = softy - 1;
                    softy = Math.Round(softy, 1);
                    label6.Text = softy.ToString();
                    break;
                case "10":
                    softy = softy - 10;
                    softy = Math.Round(softy, 1);
                    label6.Text = softy.ToString();
                    break;

            }
            if (PozMode == "G90")
            {
                machy = softy;
                label12.Text = machy.ToString();
                if (!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " Y" + machy);
                }
            }
            else if (PozMode == "G91")
            {
                switch (dziesietne)
                {
                    case "0.100":
                        machy = machy - 0.100;
                        label12.Text = machy.ToString();
                        break;
                    case "1":
                        machy = machy - 1;
                        label12.Text = machy.ToString();
                        break;
                    case "10":
                        machy = machy - 10;
                        label12.Text = machy.ToString();
                        break;
                }
                if (!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " Y" + machy);
                }
                label12.Text = "0.000";
                machy = 0.000;
            }


        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Gora();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Prawo();
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            Dol();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Lewo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Ustawienia sett = new Ustawienia(this);
            sett.Show();
        }

        bool executed;
        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            int x;
            x = trackBar1.Value;
            
            
            if (PozMode == "G90")
            {
                softz = x;
                machz = softz;
                label7.Text = softz.ToString();
                label13.Text = machz.ToString();
                if (!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " Z" + machz);
                }
            }
            else if (PozMode == "G91" && executed == false)
            {
                executed = true;
                int y;

                if (trackBar1.Value < 0)
                {
                    if (dziesietne == "1" || dziesietne == "10")
                    {
                        Int32.TryParse(dziesietne, out y);
                    }
                    else
                    {
                        y = 1;
                    }

                    softz = softz - y;
                    machz = machz - y;
                    label7.Text = softz.ToString();
                    trackBar1.Value = 0;
                }
                else
                {
                    if (dziesietne == "1" || dziesietne == "10")
                    {
                        Int32.TryParse(dziesietne, out y);
                    }
                    else
                    {
                        y = 1;
                    }
                    softz = softz + y;
                    machz = machz + y;
                    label7.Text = softz.ToString();
                    trackBar1.Value = 0;
                }
                label13.Text = machz.ToString();
                if (!VIRTUAL)
                {
                    port.WriteLine(TrybPosuwu + " Z" + machz);
                }
                machz = 0;
                label13.Text = machz.ToString();
                
            }

        }


        bool UzytkownikA;
        bool UzytkownikB;
        bool UzytkownikC;

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(UzytkownikA)
            {
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                dziesietne = checkBox2.Text.ToString();
                UpdateTick();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(UzytkownikB)
            {
                checkBox2.Checked = false;
                checkBox4.Checked = false;
                dziesietne = checkBox3.Text.ToString();
                UpdateTick();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (UzytkownikC)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                dziesietne = checkBox4.Text.ToString();
                UpdateTick();
            }
        }
        private void UpdateTick()
        {
            if (PozMode == "G90")
            {
                if (dziesietne == "1")
                {
                    trackBar1.TickFrequency = 1;
                    trackBar1.LargeChange = 1;
                }
                else if (dziesietne == "10")
                {
                    trackBar1.TickFrequency = 10;
                    trackBar1.LargeChange = 10;
                }
                else
                {
                    trackBar1.TickFrequency = 1;
                    trackBar1.LargeChange = 1;
                }
            }

        }
        private void checkBox2_MouseDown(object sender, MouseEventArgs e)
        {
            UzytkownikA = true;
            UzytkownikB = false;
            UzytkownikC = false;
        }

        private void checkBox3_MouseDown(object sender, MouseEventArgs e)
        {
            UzytkownikA = false;
            UzytkownikB = true;
            UzytkownikC = false;
        }

        private void checkBox4_MouseDown(object sender, MouseEventArgs e)
        {
            UzytkownikA = false;
            UzytkownikB = false;
            UzytkownikC = true;
        }

        private void Sterowanie_FormClosed(object sender, FormClosedEventArgs e)
        {

            System.Windows.Forms.Application.Exit();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            port.Close();
            port.Dispose();
            other.Display();
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                Gora();
                return true;
            }
            if (keyData == Keys.Down)
            {
                Dol();
                return true;
            }
            if (keyData == Keys.Left)
            {
                Lewo();
                return true;
            }
            if (keyData == Keys.Right)
            {
                Prawo();
                return true;
            }
            if (keyData == Keys.Add)
            {
                executed = false;
                if (!backgroundWorker5.IsBusy)
                {
                    backgroundWorker5.RunWorkerAsync();
                }
                trackBar1.Value = trackBar1.Value + trackBar1.TickFrequency;
                return true;
            }
            if (keyData == Keys.Subtract)
            {
                executed = false;
                if (!backgroundWorker6.IsBusy)
                {
                    backgroundWorker6.RunWorkerAsync();
                }
                trackBar1.Value = trackBar1.Value - trackBar1.TickFrequency;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            executed = false;
            if (!backgroundWorker5.IsBusy)
            {
                backgroundWorker5.RunWorkerAsync();
            }
            trackBar1.Value = trackBar1.Value + trackBar1.TickFrequency;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            executed = false;
            if (!backgroundWorker6.IsBusy)
            {
                backgroundWorker6.RunWorkerAsync();
            }
            trackBar1.Value = trackBar1.Value - trackBar1.TickFrequency;
        }

        public bool UzytkownikD;
        public bool UzytkownikE;

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (UzytkownikD)
            {
                TrybPosuwu = "G00";
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (UzytkownikE)
            {
                TrybPosuwu = "G01";
            }
        }

        private void checkBox6_MouseDown(object sender, MouseEventArgs e)
        {
            UzytkownikD = false;
            UzytkownikE = true;
            checkBox5.Checked = false;
        }

        private void checkBox5_MouseDown(object sender, MouseEventArgs e)
        {
            UzytkownikD = true;
            UzytkownikE = false;
            checkBox6.Checked = false;
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            executed = false;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            button2.ForeColor = Color.FromArgb(0, 255, 0);
            System.Threading.Thread.Sleep(100);
            button2.ForeColor = Color.Black;
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            button5.ForeColor = Color.FromArgb(0, 255, 0);
            System.Threading.Thread.Sleep(100);
            button5.ForeColor = Color.Black;
        }

        private void backgroundWorker3_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            button3.ForeColor = Color.FromArgb(0, 255, 0);
            System.Threading.Thread.Sleep(100);
            button3.ForeColor = Color.Black;
        }

        private void backgroundWorker4_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            button4.ForeColor = Color.FromArgb(0, 255, 0);
            System.Threading.Thread.Sleep(100);
            button4.ForeColor = Color.Black;
        }

        private void backgroundWorker5_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            button8.ForeColor = Color.FromArgb(0, 255, 0);
            System.Threading.Thread.Sleep(100);
            button8.ForeColor = Color.Black;
        }

        private void backgroundWorker6_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            button9.ForeColor = Color.FromArgb(0, 255, 0);
            System.Threading.Thread.Sleep(100);
            button9.ForeColor = Color.Black;
        }


        public string READ_COM
        {
            get
            {
                string x;
                x = port.ReadLine();
                return x;
            }
        }
        public void Write_COM(string x)
        {
            port.WriteLine(x);
        }
    }
}
#pragma warning restore 0649

