using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otopark_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Bağlantı Cümleciği Tanımlama
        public static SqlConnection Baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Otopark;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        //Veritabanı Bağlantı Açma Metodu
        static public void BaglantiAc()
        {
            try
            {
                //Bağlantı Durum Kontrolü
                if (Baglanti.State == ConnectionState.Closed)
                    Baglanti.Open();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Veritabanı bağlantısı açılırken hata oluştu:\n" + Hata.Message, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void KayitListele(DataGridView _KayitTablosuGrid, string _TabloAdi)
        {
            try
            {
                BaglantiAc();                                           //Veritanı aç
                DataSet ds = new DataSet();                             //Dataseti oluştur
                string Sorgu = "Select * from " + _TabloAdi;                //Sql komutu yaz
                SqlDataAdapter da = new SqlDataAdapter(Sorgu, Baglanti);//SqlDataAdapter OLuştur
                da.Fill(ds, _TabloAdi);                                //DataSeti Personel tablosu ile doldur
                _KayitTablosuGrid.DataSource = ds.Tables[_TabloAdi];       //DataGride verileri yükle
                Baglanti.Close();                                       //Veritabanı kapat

            }
            catch (Exception Hata)
            {
                MessageBox.Show("Kayıtlar listelenirken hata oluştu:\n" + Hata.Message, "Listeleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void EkranDegistirFonc(Form _GecisYapilacakEkran, Panel _panel)
        {
            _panel.Controls.Clear();

            _GecisYapilacakEkran.TopLevel = false;

            _GecisYapilacakEkran.FormBorderStyle = FormBorderStyle.None;


            _panel.Controls.Add(_GecisYapilacakEkran);
            _GecisYapilacakEkran.Show();
        }

        private void yeniAboneEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YeniAboneEkle aboneEkleform = new YeniAboneEkle();

            EkranDegistirFonc(aboneEkleform, panel1);
        }

        private void aboneKayıtlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboneKayitlari aboneKayitlari = new AboneKayitlari();

            EkranDegistirFonc(aboneKayitlari, panel1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // İkonları yükle (Designer uyumluluğu için burada yapılıyor)
            string ikonYolu = System.IO.Path.Combine(Application.StartupPath, @"..\..\ikonlar\ledicons");

            try
            {
                // MenuStrip üst menü ikonları
                anaEkranOtoparkToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "car.png"));
                müşteriAboneToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "group.png"));
                araçYıkamaToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "basket.png"));
                finansKasaToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "money.png"));
                personelİKToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "user.png"));
                sistemVeTarifelerToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "cog.png"));
                araçlarToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "hammer_screwdriver.png"));
                kullanıcıProfilimToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "user_business.png"));
                yardımToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "help.png"));

                // MenuStrip alt menü ikonları
                anlıkDolulukDurumuToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "chart_pie.png"));
                araçGirişToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "door_in.png"));
                araçÇıkışToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "door.png"));
                kayıpBiletİşlemleriToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "ticket.png"));
                yeniAboneEkleToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "add.png"));
                aboneKayıtlarıToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "book_addresses.png"));
                abonelikSüresiYaklaşanlarToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "clock.png"));
                içYıkamaToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "color_swatch_1.png"));
                yıkamaDurumTakibiToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "eye.png"));
                anlıkKasaDurumuToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "safe.png"));
                gelirGiderİşlemleriToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "coins.png"));
                günlükKasaRaporuToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "report.png"));
                personelKayıtToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "vcard.png"));
                mesaiTakibiToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "clock.png"));
                avansMaaşTanımlamaToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "money_dollar.png"));
                personelGiderleriToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "chart_bar.png"));
                genelTarifeAyarlarıToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "calculator.png"));
                abonmanAyarlarıToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "bookmark.png"));
                otoYıkamaAyarıToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "color_swatch_1.png"));
                vergiAyarlarıToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "money_euro.png"));
                personelYetkilendirmeToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "shield.png"));
                telefonRehberiToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "telephone.png"));
                telefonKayıtlarıToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "book.png"));
                şifreDeğiştirToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "key.png"));
                oturumuKapatToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "lock.png"));
                yazılımHakkındaToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "lightbulb.png"));
                iletişimToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "email.png"));
                programıKapatToolStripMenuItem.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "cross.png"));

                // ToolStrip buton ikonları
                tsbOtoparkHareketleri.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "car.png"));
                tsbAboneEkle.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "add.png"));
                tsbAboneKayitlari.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "book_addresses.png"));
                tsbYikamaSecenekleri.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "basket.png"));
                tsbKasaIslemleri.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "coins.png"));
                tsbPersoneller.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "user.png"));
                tsbKullanicilar.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "shield.png"));
                tsbTarifeler.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "calculator.png"));
                tsbTelefonRehberi.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(ikonYolu, "telephone.png"));
            }
            catch (Exception ex)
            {
                // İkon dosyaları bulunamazsa uygulama yine de çalışsın
                System.Diagnostics.Debug.WriteLine("İkon yükleme hatası: " + ex.Message);
            }
        }

        private void içYıkamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YikamaSecenekleri yikamaSecenekleri = new YikamaSecenekleri();

            EkranDegistirFonc(yikamaSecenekleri, panel1);
        }

        // --- YENİ FORMLAR İÇİN MENÜ CLICK OLAYLARI ---

        private void araçGirişToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtoparkHareketleriForm otoparkForm = new OtoparkHareketleriForm();
            EkranDegistirFonc(otoparkForm, panel1);
        }

        private void gelirGiderİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KasaIslemleriForm kasaForm = new KasaIslemleriForm();
            EkranDegistirFonc(kasaForm, panel1);
        }

        private void genelTarifeAyarlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TarifelerForm tarifeForm = new TarifelerForm();
            EkranDegistirFonc(tarifeForm, panel1);
        }

        private void personelYetkilendirmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KullanicilarForm kullaniciForm = new KullanicilarForm();
            EkranDegistirFonc(kullaniciForm, panel1);
        }

        private void telefonRehberiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TelefonRehberiForm rehberForm = new TelefonRehberiForm();
            EkranDegistirFonc(rehberForm, panel1);
        }

        private void personelKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonellerForm personelForm = new PersonellerForm();
            EkranDegistirFonc(personelForm, panel1);
        }

        private void araçÇıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void araçYıkamaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // --- TOOLSTRIP BUTON CLICK OLAYLARI ---

        private void tsbOtoparkHareketleri_Click(object sender, EventArgs e)
        {
            OtoparkHareketleriForm otoparkForm = new OtoparkHareketleriForm();
            EkranDegistirFonc(otoparkForm, panel1);
        }

        private void tsbAboneEkle_Click(object sender, EventArgs e)
        {
            YeniAboneEkle aboneEkleform = new YeniAboneEkle();
            EkranDegistirFonc(aboneEkleform, panel1);
        }

        private void tsbAboneKayitlari_Click(object sender, EventArgs e)
        {
            AboneKayitlari aboneKayitlari = new AboneKayitlari();
            EkranDegistirFonc(aboneKayitlari, panel1);
        }

        private void tsbYikamaSecenekleri_Click(object sender, EventArgs e)
        {
            YikamaSecenekleri yikamaSecenekleri = new YikamaSecenekleri();
            EkranDegistirFonc(yikamaSecenekleri, panel1);
        }

        private void tsbKasaIslemleri_Click(object sender, EventArgs e)
        {
            KasaIslemleriForm kasaForm = new KasaIslemleriForm();
            EkranDegistirFonc(kasaForm, panel1);
        }

        private void tsbPersoneller_Click(object sender, EventArgs e)
        {
            PersonellerForm personelForm = new PersonellerForm();
            EkranDegistirFonc(personelForm, panel1);
        }

        private void tsbTarifeler_Click(object sender, EventArgs e)
        {
            TarifelerForm tarifeForm = new TarifelerForm();
            EkranDegistirFonc(tarifeForm, panel1);
        }

        private void tsbKullanicilar_Click(object sender, EventArgs e)
        {
            KullanicilarForm kullaniciForm = new KullanicilarForm();
            EkranDegistirFonc(kullaniciForm, panel1);
        }

        private void tsbTelefonRehberi_Click(object sender, EventArgs e)
        {
            TelefonRehberiForm rehberForm = new TelefonRehberiForm();
            EkranDegistirFonc(rehberForm, panel1);
        }

        // --- PROGRAM ÇIKIŞ VE OTURUM İŞLEMLERİ ---

        private void programıKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sonuc = MessageBox.Show(
                "Programı kapatmak istediğinizden emin misiniz?",
                "Programı Kapat",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void oturumuKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sonuc = MessageBox.Show(
                "Oturumunuzu kapatmak istediğinizden emin misiniz?\nKaydedilmemiş değişiklikler kaybolabilir.",
                "Oturumu Kapat",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (sonuc == DialogResult.Yes)
            {
                panel1.Controls.Clear();
                MessageBox.Show("Oturum başarıyla kapatıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var sonuc = MessageBox.Show(
                    "Programdan çıkmak istediğinizden emin misiniz?",
                    "Çıkış Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (sonuc == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
