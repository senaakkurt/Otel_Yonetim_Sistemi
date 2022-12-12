using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Otel_Yönetim_Sistemi
{
    public partial class Form1 : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\senaa\Documents\Oteldb.mdf;Integrated Security=True;Connect Timeout=30");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from Personel_tbl where PersonelAdi='" + kullaniciAdi.Text + "'and PersonelSifre='" + sifre.Text + "' ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString()=="1")
            {
                AnaForm af = new AnaForm();
                af.Show();
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre ");
            }
            Con.Close();

        }
    }
}
