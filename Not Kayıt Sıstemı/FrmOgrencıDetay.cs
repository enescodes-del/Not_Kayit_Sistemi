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
    public partial class FrmOgrencıDetay : Form
    {
        public FrmOgrencıDetay()
        {
            InitializeComponent();
        }
        public string numara;

         SqlConnection baglantı = new  SqlConnection(@"Data Source=ENESIN_BILGISAY\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True  ");
        private void FrmOgrencıDetay_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            baglantı.Open();
            SqlCommand komut = new SqlCommand("Select * from TBL_DERS where OGRNO =@p1",baglantı );
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                LblSınav1.Text = dr[4].ToString();
                LblSınav2.Text = dr[5].ToString();
                LblSınav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                LblDurum.Text = dr[8].ToString();
            }
        }
    }
}
