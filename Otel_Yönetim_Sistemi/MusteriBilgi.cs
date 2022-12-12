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
    public partial class MusteriBilgi : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\senaa\Documents\Oteldb.mdf;Integrated Security=True;Connect Timeout=30");
        
        public void populate()
        {
            Con.Open();
            string Myquery = "select * from Musteri_tbl";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            MusteriGridView.DataSource = ds.Tables[0];
            Con.Close();
        }
        public MusteriBilgi()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Tarihlbl.Text = DateTime.Now.ToLongDateString();
        }

        private void MusteriBilgi_Load(object sender, EventArgs e)
        {
            Tarihlbl.Text = DateTime.Now.ToLongDateString();
            timer1.Start();
            populate();
        }

        private void EkleBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("insert into Musteri_tbl values(" + musteriidtb.Text + ", '" + musteriaditb.Text + "', '" + musteritctb.Text + "', '" + musteriteltb.Text + "', '" + musteriulkecb.SelectedItem.ToString() + "')", Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Müşteri Başarıyla Eklendi");
            Con.Close();
            populate();
        }

        private void MusteriGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            musteriidtb.Text = MusteriGridView.CurrentRow.Cells[0].Value.ToString();
            musteriaditb.Text = MusteriGridView.CurrentRow.Cells[1].Value.ToString();
            musteritctb.Text = MusteriGridView.CurrentRow.Cells[2].Value.ToString();
            musteriteltb.Text = MusteriGridView.CurrentRow.Cells[3].Value.ToString();
        }

        private void DuzenleBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            string myquery = "UPDATE Musteri_tbl set MusteriAdi ='" + musteriaditb.Text + "', MusteriTc ='" + musteritctb.Text + "', MusteriTel ='" + musteriteltb.Text + "', MusteriUlke ='" + musteriulkecb.SelectedItem.ToString() + "' where MusteriId  = " + musteriidtb.Text + " ;";
            SqlCommand cmd = new SqlCommand(myquery, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Müşteri Başarıyla Düzenlendi");
            Con.Close();
            populate();
        }

        private void SilBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "delete from Musteri_tbl where MusteriId = " + musteriidtb.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Müşteri Başarıyla Silindi");
            Con.Close();
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Con.Open();
            string Myquery = "select * from Musteri_tbl where MusteriAdi = '"+MusteriAramatb.Text+"'";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            MusteriGridView.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaForm anaform = new AnaForm();
            anaform.Show();
            this.Hide();
        }
    }
}
