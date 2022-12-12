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
    public partial class OdaBilgi : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\senaa\Documents\Oteldb.mdf;Integrated Security=True;Connect Timeout=30");

        public void populate()
        {
            Con.Open();
            string Myquery = "select * from Oda_tbl";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            OdaGridView.DataSource = ds.Tables[0];
            Con.Close();
        }
        public OdaBilgi()
        {
            InitializeComponent();
        }

        private void OdaEkleBtn_Click(object sender, EventArgs e)
        {
            string durum;
            if (Dolurb.Checked == true)
                durum = "Dolu";
            else
                durum = "Boş";

            Con.Open();
            SqlCommand cmd = new SqlCommand("insert into Oda_tbl values(" + odaidtb.Text + ", '" + odateltb.Text + "', '" + durum + "', '" + odatipicb.SelectedItem.ToString() + "')", Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Oda Başarıyla Eklendi");
            Con.Close();
            populate();
        }

        private void OdaBilgi_Load(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Now.ToLongDateString();
            populate();
        }

        //sıkıntılııı
        private void OdaGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            odaidtb.Text = OdaGridView.CurrentRow.Cells[0].Value.ToString();
            odaidtb.Text = OdaGridView.CurrentRow.Cells[1].Value.ToString();
            odaidtb.Text = OdaGridView.CurrentRow.Cells[2].Value.ToString();
           
        }

        private void OdaSilBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "delete from Oda_tbl where OdaId = " + odaidtb.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Oda Başarıyla Silindi");
            Con.Close();
            populate();
        }

        private void OdaDuzenleBtn_Click(object sender, EventArgs e)
        {
            string durum;
            if (Dolurb.Checked == true)
                durum = "Dolu";
            else
                durum = "Boş";
            Con.Open();
            string myquery = "UPDATE Oda_tbl set OdaId ='" + odaidtb.Text + "', OdaTel ='" + odateltb.Text + "', OdaTip ='" + odatipicb.SelectedItem.ToString() + "', OdaDurum ='" + durum + "' where OdaId  = " + odaidtb.Text + " ;";
            SqlCommand cmd = new SqlCommand(myquery, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Oda Başarıyla Düzenlendi");
            Con.Close();
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Con.Open();
            string Myquery = "select * from Oda_tbl where OdaId = '" + OdaArmtb.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            OdaGridView.DataSource = ds.Tables[0];
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
