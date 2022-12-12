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
    public partial class PersonelBilgi : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\senaa\Documents\Oteldb.mdf;Integrated Security=True;Connect Timeout=30");
        public void populate()
        {
            Con.Open();
            string Myquery = "select * from Personel_tbl";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            PersonelGridView.DataSource = ds.Tables[0];
            Con.Close();
        }
        public PersonelBilgi()
        {
            InitializeComponent();
        }

        private void PersonelBilgi_Load(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Now.ToLongDateString();
            populate();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("insert into Personel_tbl values(" + personelidtb.Text + ", '" + personeladitb.Text + "', '" + personelteltb.Text + "', '" + personelsifretb.Text + "', '" + personelcinsiyetcb.SelectedItem.ToString() + "')", Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Personel Başarıyla Eklendi");
            Con.Close();
            populate();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            string myquery = "UPDATE Personel_tbl set PersonelAdi ='" + personeladitb.Text + "', PersonelTel ='" + personelteltb.Text + "', PersonelSifre ='" + personelsifretb.Text + "', PersonelCinsiyet ='" + personelcinsiyetcb.SelectedItem.ToString() + "' where PersonelId  = " + personelidtb.Text + " ;";
            SqlCommand cmd = new SqlCommand(myquery, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Personel Başarıyla Düzenlendi");
            Con.Close();
            populate();
        }

        //sıkıntılıı
        private void PersonelGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            personelidtb.Text = PersonelGridView.CurrentRow.Cells[0].Value.ToString();
            personeladitb.Text = PersonelGridView.CurrentRow.Cells[1].Value.ToString();
            personelteltb.Text = PersonelGridView.CurrentRow.Cells[2].Value.ToString();
            personelsifretb.Text = PersonelGridView.CurrentRow.Cells[3].Value.ToString();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "delete from Personel_tbl where PersonelId = " + personelidtb.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Personel Başarıyla Silindi");
            Con.Close();
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Con.Open();
            string Myquery = "select * from Personel_tbl where PersonelAdi = '" + PersonelArmtb.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
            SqlCommandBuilder cbuilder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            PersonelGridView.DataSource = ds.Tables[0];
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
