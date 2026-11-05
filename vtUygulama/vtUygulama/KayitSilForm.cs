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

namespace vtUygulama
{
    public partial class KayitSilForm : Form
    {
        public KayitSilForm()
        {
            InitializeComponent();
        }

        public void KayitSil()
        {
            try
            {
                Form1.BaglantiAc();
                string Sorgu = "Delete from Personel where PerID="+txtPerID.Text;
                SqlCommand SilKomut = new SqlCommand(Sorgu,Form1.Baglanti);
                if (SilKomut.ExecuteNonQuery() == 1)
                    MessageBox.Show("Kayıt Silindi");
                Form1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message, "Kayıt Sil Hata Penceresi");                    
            }
        }


        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult cevap;

            cevap=MessageBox.Show(txtPerID.Text+" Nolu Kayıt Silinecek!\nOnaylıyor musunuz?","Silme Onayı",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);

            if (cevap == DialogResult.Yes)
                KayitSil();
            else
                MessageBox.Show("Silme işlemi iptal edildi","İptal",MessageBoxButtons.OK,MessageBoxIcon.Stop);
        }

        private void KayitSilForm_Load(object sender, EventArgs e)
        {

        }
    }
}
