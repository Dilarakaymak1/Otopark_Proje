using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;        //Ado.Net SqlServer Kütüphanesi

namespace vtUygulama
{
    public partial class KayitEkleForm : Form
    {
        public KayitEkleForm()
        {
            InitializeComponent();
        }

        //Personel tablosuna formdaki alanlarda girilen verileri kaydeden method
        public void KayitEkle()
        {
            try
            {
                Form1.BaglantiAc();
                string Sorgu = "Insert Into Personel(Tcno,Ad,Soyad,Cinsiyet,Dyer,Dtar,Tel,Birim) Values(@Tcno,@Ad,@Soyad,@Cinsiyet,@Dyer,@Dtar,@Tel,@Birim)";
                SqlCommand EkleKomut = new SqlCommand(Sorgu, Form1.Baglanti);
                EkleKomut.Parameters.AddWithValue("@Tcno", txtTcno.Text);
                EkleKomut.Parameters.AddWithValue("@Ad", txtAd.Text);
                EkleKomut.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
                if (radE.Checked == true)
                    EkleKomut.Parameters.AddWithValue("@Cinsiyet", "E");
                else if(radK.Checked==true)
                    EkleKomut.Parameters.AddWithValue("@Cinsiyet", "K");
                EkleKomut.Parameters.AddWithValue("@Dyer", cmbDyer.Text);
                EkleKomut.Parameters.AddWithValue("@Dtar", dtDtar.Value);
                EkleKomut.Parameters.AddWithValue("@Tel", txtTel.Text);
                EkleKomut.Parameters.AddWithValue("@Birim", cmbBirim.Text);

                if (EkleKomut.ExecuteNonQuery() == 1)
                    MessageBox.Show("Kayıt Eklendi","Veri Girişi");
                Form1.Baglanti.Close();

            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message,"Kayıt Ekle Hata Penceresi");
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

            if (dtDtar.Value.Date==DateTime.Today.Date)
            {
                lblYildiz2.Visible = true;
                bos = true;
            }

            if(cmbDyer.Text== "Şehir Seçiniz"||cmbDyer.Text=="")
            {
                cmbDyer.BackColor = Color.Red;
                cmbDyer.Focus();
                bos = true;
            }

            if (radE.Checked==false&&radK.Checked==false)
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

            if (txtAd.Text=="")
            {
                txtAd.BackColor = Color.Red;
                txtAd.Focus();
                bos = true;
            }

            if (txtTcno.Text.Length<11)
            {
                txtTcno.BackColor = Color.Red;
                txtTcno.Focus();
                bos = true;
            }                      
            return bos;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (BoslukKontrol() == true)
                MessageBox.Show("Boş alanları doldurunuz");
            else
                KayitEkle();            
        }
    }
}
