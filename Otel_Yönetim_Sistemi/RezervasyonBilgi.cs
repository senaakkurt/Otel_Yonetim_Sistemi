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
    public partial class RezervasyonBilgi : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\senaa\Documents\Oteldb.mdf;Integrated Security=True;Connect Timeout=30");
        public void populate()
        {
            Con.Open();
            string Myquery = "select * from Rezervasyon_tbl";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RezervasyonGridView.DataSource = ds.Tables[0];
            Con.Close();
        }
        public RezervasyonBilgi()
        {
            InitializeComponent();
        }
        DateTime dateTime;

        public void fillOdacombo()
        {
            Con.Open();
            string odadurum = "Boş";
            SqlCommand cmd = new SqlCommand("select OdaId from Oda_tbl where OdaDurum = '"+odadurum+"' ",Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("OdaId", typeof(int));
            dt.Load(rdr);
            odanocb.ValueMember = "OdaId";
            odanocb.DataSource = dt;
            Con.Close();
        }

        public void fillMustericombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select MusteriAdi from Musteri_tbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("MusteriAdi", typeof(string));
            dt.Load(rdr);
            musteriadicb.ValueMember = "MusteriAdi";
            musteriadicb.DataSource = dt;
            Con.Close();
        }
        
        private void giristp_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(giristp.Value,dateTime);
            if (res < 0)
                MessageBox.Show("Rezervasyon için Yanlış Tarih");
        }

        private void cikistp_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(cikistp.Value, giristp.Value);
            if (res < 0)
                MessageBox.Show("Yanlış Tarih. Bir kez daha kontrol edin.");
        }

        public void odadurumguncelle()
        {
            Con.Open();
            string yenidurum = "Dolu";
            string myquery = "UPDATE Oda_tbl set OdaDurum ='" + yenidurum + "', where OdaId  = " +Convert.ToInt32(odanocb.SelectedItem.ToString()) + " ;"; //selectedvalue da olabilir
            SqlCommand cmd = new SqlCommand(myquery, Con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Rezervasyon Başarıyla Düzenlendi");
            Con.Close();
            fillOdacombo();
        }
        public void silininceodaguncelle()
        {
            Con.Open();
            string yenidurum = "Boş";
            int odaid = Convert.ToInt32(rezidtb.Text = RezervasyonGridView.SelectedRows[0].Cells[2].Value.ToString());
            string myquery = "UPDATE Oda_tbl set OdaDurum ='" + yenidurum + "', where OdaId  = " + odaid + " ;";
            SqlCommand cmd = new SqlCommand(myquery, Con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Rezervasyon Başarıyla Düzenlendi");
            Con.Close();
            fillOdacombo();
        }
        private void RekleBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO Rezervasyon_tbl ( RezId , Musteri , Oda , GirisTarihi ,CikisTarihi , Yemek , Fiyat ) VALUES ({rezidtb.Text},'{musteriadicb.SelectedValue.ToString()}',{odanocb.SelectedValue.ToString()},'{giristp.Value}','{cikistp.Value}','{yemekcb.SelectedItem.ToString()}',{fiyattb.Text})" ,Con );
            cmd.ExecuteNonQuery();
            MessageBox.Show("Rezervasyon Başarıyla Eklendi");
            Con.Close();
            odadurumguncelle();
            populate();
            
        }

        private void RezervasyonGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rezidtb.Text = RezervasyonGridView.CurrentRow.Cells[0].Value.ToString();
            rezidtb.Text = RezervasyonGridView.CurrentRow.Cells[1].Value.ToString();
            rezidtb.Text = RezervasyonGridView.CurrentRow.Cells[2].Value.ToString();
            populate();
        }

        private void RezervasyonBilgi_Load(object sender, EventArgs e)
        {
            dateTime = giristp.Value;
            fillOdacombo();
            fillMustericombo();
            populate();
            Tarihlbl.Text = DateTime.Now.ToLongDateString();
        }

        private void RsilBtn_Click(object sender, EventArgs e)
        {
            if (rezidtb.Text == "")
            {
                MessageBox.Show("Silinecek Rezervasyonu Girin");
            }
            else
            {
                Con.Open();
                string query = "delete from Rezervasyon_tbl where RezId = " + rezidtb.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Rezervasyon Başarıyla Silindi");
                Con.Close();
                silininceodaguncelle();
                populate();
            }
        }

        private void RduzenleBtn_Click(object sender, EventArgs e)
        {
            if(rezidtb.Text == "")
            {
                MessageBox.Show("Boş ResId. Rezervasyon Id girin.");
            }
            else
            {
                Con.Open();
                string myquery = "UPDATE Rezervasyon_tbl set Musteri ='" + musteriadicb.SelectedValue.ToString() + "', Oda ='" + odanocb.SelectedValue.ToString() + "', GirisTarihi ='" + giristp.Value.ToString() + "', CikisTarihi ='" + cikistp.Value.ToString() + "', Yemek = '" + yemekcb.SelectedItem.ToString() + "', Fiyat = '" + fiyattb.Text + "', where RezId  = " + rezidtb.Text + " ;";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Rezervasyon Başarıyla Düzenlendi");
                Con.Close();
                populate();
            }
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Con.Open();
            string Myquery = "select * from Rezervasyon_tbl where ResId = '" + RezAramatb.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RezervasyonGridView.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void RezervasyonGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            rezidtb.Text = RezervasyonGridView.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaForm anaform = new AnaForm();
            anaform.Show();
            this.Hide();
        }

    }
}
