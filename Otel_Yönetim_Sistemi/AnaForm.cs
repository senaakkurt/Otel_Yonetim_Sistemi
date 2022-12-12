using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel_Yönetim_Sistemi
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
             Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MusteriBilgi musteriBilgi = new MusteriBilgi();
            musteriBilgi.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PersonelBilgi personelBilgi = new PersonelBilgi();
            personelBilgi.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OdaBilgi odaBilgi = new OdaBilgi();
            odaBilgi.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RezervasyonBilgi rezBilgi = new RezervasyonBilgi();
            rezBilgi.Show();
            this.Hide();
        }
    }
}
