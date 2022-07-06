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

namespace Not_Kayıt_Sıstemı
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection(@"Data Source=ENESIN_BILGISAY\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True  ");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'dbNotKayıtDataSet.TBL_DERS' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tBL_DERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBL_DERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("inset into TBL_DERS (OGRNO,OGRAD,OGRSOYAD) values (@P1,@P2,@P3)", baglantı);
            komut.Parameters.AddWithValue("@P1", MskNumara.Text);
            komut.Parameters.AddWithValue("@P2",TxtAd.Text);
            komut.Parameters.AddWithValue("@P1", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBL_DERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBL_DERS);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secılen = dataGridView1.SelectedCells[0].RowIndex;

            MskNumara.Text = dataGridView1.Rows[secılen].Cells[2].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secılen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secılen].Cells[3].Value.ToString();
            TxtSınav1.Text = dataGridView1.Rows[secılen].Cells[4].Value.ToString();
            TxtSınav2.Text = dataGridView1.Rows[secılen].Cells[5].Value.ToString();
            TxtSınav3.Text = dataGridView1.Rows[secılen].Cells[6].Value.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            s1 = Convert.ToDouble(TxtSınav1.Text);
            s2 = Convert.ToDouble(TxtSınav2.Text);
            s3 = Convert.ToDouble(TxtSınav3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text = ortalama.ToString();

            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglantı.Open();
            SqlCommand komut = new SqlCommand("update TBL_DERS set OGRS1 = @P1, OGRS2 = @P2, OGRS3 = @P3, ORTALAMA = @P4, DURUM = @P5 where OGRNO = @P6",baglantı);
            komut.Parameters.AddWithValue("@P1", TxtSınav1.Text);
            komut.Parameters.AddWithValue("@P2", TxtSınav2.Text);
            komut.Parameters.AddWithValue("@P3", TxtSınav3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", MskNumara.Text);

            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tBL_DERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBL_DERS);

        }
    }
}
