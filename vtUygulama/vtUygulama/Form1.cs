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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Bağlantı Cümleciği Tanımlama
        public static SqlConnection Baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=vt2A;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
        //Veritabanı Bağlantı Açma Metodu
        public static void BaglantiAc()
        {
            try
            {
                //Bağlantı Durum Kontrolü
                if (Baglanti.State == ConnectionState.Closed)
                    Baglanti.Open();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message,"Bağlantı Açma Hata Penceresi");
            }
        }

        //Personel tablosundaki verileri listeleyen method
        public void KayitListele()
        {
            try
            {
                BaglantiAc();                                           //Veritanı aç
                DataSet ds = new DataSet();                             //Dataseti oluştur
                string Sorgu = "Select * from Personel";                //Sql komutu yaz
                SqlDataAdapter da = new SqlDataAdapter(Sorgu, Baglanti);//SqlDataAdapter OLuştur
                da.Fill(ds, "Personel");                                //DataSeti Personel tablosu ile doldur
                dataGridView1.DataSource = ds.Tables["Personel"];       //DataGride verileri yükle
                Baglanti.Close();                                       //Veritabanı kapat

            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message,"Kayıt Listele Hata Penceresi");
            }                
        }

        //Personel Tablosunda TextBoxlara girilen verileri BİREBİR arayan method
        public void KayitAra()
        {
            try
            {
                BaglantiAc();                                           
                DataSet ds = new DataSet();                             
                string SorguTum = "Select * from Personel";
                string SorguAd = "Select * from Personel where Ad='"+txtAd.Text+"'";
                string SorguSoyad = "Select * from Personel where Soyad='"+txtSoyad.Text+"'";
                string SorguAdSoyad = "Select * from Personel where Ad='" + txtAd.Text + "' and Soyad='"+txtSoyad.Text+"' " ;

                if (chkAd.Checked == true && chkSoyad.Checked == true)
                    SorguTum = SorguAdSoyad;
                else if (chkAd.Checked == true)
                    SorguTum = SorguAd;
                else if (chkSoyad.Checked == true)
                    SorguTum = SorguSoyad;
                                   
                SqlDataAdapter da = new SqlDataAdapter(SorguTum, Baglanti);
                da.Fill(ds, "Personel");                                
                dataGridView1.DataSource = ds.Tables["Personel"];       
                Baglanti.Close();                                       

            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message, "Kayıt Arama Hata Penceresi");
            }
        }

        //Personel Tablosunda TextBoxlara girilen verileri
        //seçili alanda arama türüne göre YAZARKEN arayan method
        public void HizliAra()
        {
            try
            {
                BaglantiAc();
                DataSet ds = new DataSet();
                string SorguTum = "Select * from Personel";
                string SorguTcnoilebaslayan = "Select * from Personel where Tcno Like '"+txtHizliara.Text+"%' ";
                string SorguTcnoilebiten = "Select * from Personel where Tcno Like '%" + txtHizliara.Text + "' ";
                string SorguTcnoiceren = "Select * from Personel where Tcno Like '%" + txtHizliara.Text + "%' ";

                SqlDataAdapter da = new SqlDataAdapter(SorguTcnoilebaslayan, Baglanti);
                da.Fill(ds, "Personel");
                dataGridView1.DataSource = ds.Tables["Personel"];
                Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message, "Kayıt Arama Hata Penceresi");
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            KayitListele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            KayitEkleForm frmEkle = new KayitEkleForm();
            frmEkle.ShowDialog();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Baglanti.Close();
            KayitListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            KayitSilForm frmSil = new KayitSilForm();

            frmSil.txtPerID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frmSil.txtTcno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frmSil.txtAd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frmSil.txtSoyad.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            frmSil.ShowDialog();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            KayitGuncelleForm frmGuncelle = new KayitGuncelleForm();
            frmGuncelle.txtPerID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frmGuncelle.txtTcno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frmGuncelle.txtAd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frmGuncelle.txtSoyad.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "E")
                frmGuncelle.radE.Checked = true;
            else
                frmGuncelle.radK.Checked = true;
            frmGuncelle.cmbDyer.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            frmGuncelle.dtDtar.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            frmGuncelle.txtTel.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            frmGuncelle.cmbBirim.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();

            frmGuncelle.ShowDialog();
        }

        private void chkAd_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAd.Checked==true)
            {
                txtAd.Enabled = true;
                txtAd.Focus();
            }
            else
            {
                txtAd.Enabled = false;
                txtAd.Text = "";
            }

        }

        private void chkSoyad_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSoyad.Checked == true)
            {      
                txtSoyad.Enabled = true;
                txtSoyad.Focus();
            }
            else
            {
                txtSoyad.Enabled = false;
                txtSoyad.Text = "";
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            KayitAra();
        }

        private void radTcno_CheckedChanged(object sender, EventArgs e)
        {
            lblArananalan.Text = "Tc No:";
            txtHizliara.Text = "";
            txtHizliara.Focus();
        }

        private void radDyer_CheckedChanged(object sender, EventArgs e)
        {
            lblArananalan.Text = "Doğum Yeri:";
            txtHizliara.Text = "";
            txtHizliara.Focus();
        }

        private void txtHizliara_TextChanged(object sender, EventArgs e)
        {
            HizliAra();
        }
    }
}
