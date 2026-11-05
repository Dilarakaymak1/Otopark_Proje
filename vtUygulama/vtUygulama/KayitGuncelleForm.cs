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
    public partial class KayitGuncelleForm : Form
    {
        public KayitGuncelleForm()
        {
            InitializeComponent();
        }

        public void KayitGuncelle()
        {
            try
            {
                Form1.BaglantiAc();
                string Sorgu = "Update Personel Set Tcno=@Tcno,Ad=@Ad,Soyad=@Soyad,Cinsiyet=@Cinsiyet,Dyer=@Dyer,Dtar=@Dtar,Tel=@Tel,Birim=@Birim where perID=@perID";
                SqlCommand GuncelleKomut = new SqlCommand(Sorgu,Form1.Baglanti);
                GuncelleKomut.Parameters.AddWithValue("@Tcno", txtTcno.Text);
                GuncelleKomut.Parameters.AddWithValue("@Ad", txtAd.Text);
                GuncelleKomut.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
                if (radE.Checked == true)
                    GuncelleKomut.Parameters.AddWithValue("@Cinsiyet", "E");
                else if (radK.Checked == true)
                    GuncelleKomut.Parameters.AddWithValue("@Cinsiyet", "K");
                GuncelleKomut.Parameters.AddWithValue("@Dyer", cmbDyer.Text);
                GuncelleKomut.Parameters.AddWithValue("@Dtar", dtDtar.Value);
                GuncelleKomut.Parameters.AddWithValue("@Tel", txtTel.Text);
                GuncelleKomut.Parameters.AddWithValue("@Birim", cmbBirim.Text);
                GuncelleKomut.Parameters.AddWithValue("@perID", txtPerID.Text);

                if (GuncelleKomut.ExecuteNonQuery() == 1)
                    MessageBox.Show(txtPerID.Text+" Nolu Kayıt Güncellendi");
                Form1.Baglanti.Close();

            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message,"Kayıt Güncelle Hata Penceresi");
            }

        }


        //Tüm veri giriş alanlarında Boşluk Kontrolü yapan method
        public bool BoslukKontrol()
        {
            bool bos = false;

            txtTcno.BackColor = Color.White;
            txtAd.BackColor = Color.White;
            txtSoyad.BackColor = Color.White;
            cmbDyer.BackColor = Color.White;
            txtTel.BackColor = Color.White;
            cmbBirim.BackColor = Color.White;
            lblYildiz1.Visible = false;
            lblYildiz2.Visible = false;

            if (cmbBirim.Text == "Birim Seçiniz" || cmbBirim.Text == "")
            {
                cmbBirim.BackColor = Color.Red;
                cmbBirim.Focus();
                bos = true;
            }

            if (txtTel.Text.Length < 15)
            {
                txtTel.BackColor = Color.Red;
                txtTel.Focus();
                bos = true;
            }

            if (dtDtar.Value.Date == DateTime.Today.Date)
            {
                lblYildiz2.Visible = true;
                bos = true;
            }

            if (cmbDyer.Text == "Şehir Seçiniz" || cmbDyer.Text == "")
            {
                cmbDyer.BackColor = Color.Red;
                cmbDyer.Focus();
                bos = true;
            }

            if (radE.Checked == false && radK.Checked == false)
            {
                lblYildiz1.Visible = true;
                bos = true;
            }

            if (txtSoyad.Text == "")
            {
                txtSoyad.BackColor = Color.Red;
                txtSoyad.Focus();
                bos = true;
            }

            if (txtAd.Text == "")
            {
                txtAd.BackColor = Color.Red;
                txtAd.Focus();
                bos = true;
            }

            if (txtTcno.Text.Length < 11)
            {
                txtTcno.BackColor = Color.Red;
                txtTcno.Focus();
                bos = true;
            }
            return bos;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (BoslukKontrol() == true)
                MessageBox.Show("Boş alanları doldurunuz");
            else
                KayitGuncelle();
            
        }

        private void KayitGuncelleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
