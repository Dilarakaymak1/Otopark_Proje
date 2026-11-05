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
    public partial class AramaYapmaForm : Form
    {
        public AramaYapmaForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            arananDurum_Combo.Enabled = true;
            arananDurum_Combo.Visible = true;

            arananMarka.Enabled = false;
            arananMarka.Visible = false;
            
            arananPlaka_TextBox.Enabled = false;
            arananPlaka_TextBox.Visible = false;

            arananTel_textBox.Enabled = false;
            arananTel_textBox.Visible = false;

            ArananTarih_DatePicker.Enabled = false;
            ArananTarih_DatePicker.Visible = false;

            aramaKutusu_textBox.Enabled = false;
            aramaKutusu_textBox.Visible = false;

            arananKelime_Label.Text = "Abonelik Durumu Arama";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            arananDurum_Combo.Enabled = false;
            arananDurum_Combo.Visible = false;

            arananMarka.Enabled = true;
            arananMarka.Visible = true;

            arananPlaka_TextBox.Enabled = false;
            arananPlaka_TextBox.Visible = false;

            arananTel_textBox.Enabled = false;
            arananTel_textBox.Visible = false;

            ArananTarih_DatePicker.Enabled = false;
            ArananTarih_DatePicker.Visible = false;
            
            aramaKutusu_textBox.Enabled = false;
            aramaKutusu_textBox.Visible = false;

            arananKelime_Label.Text = "Araç Markası Arama";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            arananDurum_Combo.Enabled = false;
            arananDurum_Combo.Visible = false;

            arananMarka.Enabled = false;
            arananMarka.Visible = false;

            arananPlaka_TextBox.Enabled = false;
            arananPlaka_TextBox.Visible = false;

            arananTel_textBox.Enabled = false;
            arananTel_textBox.Visible = false;

            ArananTarih_DatePicker.Enabled = true;
            ArananTarih_DatePicker.Visible = true;

            aramaKutusu_textBox.Enabled = false;
            aramaKutusu_textBox.Visible = false;

            arananKelime_Label.Text = "Bitiş Tarihi Arama";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            arananDurum_Combo.Enabled = false;
            arananDurum_Combo.Visible = false;

            arananMarka.Enabled = false;
            arananMarka.Visible = false;

            arananPlaka_TextBox.Enabled = true;
            arananPlaka_TextBox.Visible = true;

            arananTel_textBox.Enabled = false;
            arananTel_textBox.Visible = false;

            ArananTarih_DatePicker.Enabled = false;
            ArananTarih_DatePicker.Visible = false;

            aramaKutusu_textBox.Enabled = false;
            aramaKutusu_textBox.Visible = false;

            arananKelime_Label.Text = "Plaka Arama";
        }

        private void AramaYapmaForm_Load(object sender, EventArgs e)
        {

        }

        private void AramaYapmaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            YeniAboneEkle.YeniAboneEkle_aramaAcik = false;
        }

        private void ara_butonu_birebir_Click(object sender, EventArgs e)
        {
            try
            {
                // Temel sorgumuzu başlatıyoruz. 'WHERE 1=1' her zaman doğru olan bir koşuldur, 
                // sonrasına 'AND' ile şartları eklememizi çok kolaylaştırır.
                string sorgu = "SELECT * FROM Aboneler WHERE 1=1";

                // Komut nesnemizi oluşturuyoruz
                SqlCommand komut = new SqlCommand();

                // --- DİNAMİK ŞARTLARIN EKLENMESİ ---
                // Kullanıcı hangi CheckBox'ı seçtiyse, sadece o alanı sorguya ve parametrelere ekliyoruz.

                if (Ad_CehckBox.Checked)
                {
                    sorgu += " AND Ad = @Ad";
                    komut.Parameters.AddWithValue("@Ad", ad_textBox.Text);
                }

                if (soyad_CehckBox.Checked)
                {
                    sorgu += " AND Soyad = @Soyad";
                    komut.Parameters.AddWithValue("@Soyad", soyad_textBox.Text);
                }

                if (tc_CehckBox.Checked)
                {
                    sorgu += " AND TCNo = @TCNo";
                    komut.Parameters.AddWithValue("@TCNo", tc_textBox.Text);
                }

                if (Tel_CehckBox.Checked)
                {
                    sorgu += " AND Telefon = @Telefon";
                    komut.Parameters.AddWithValue("@Telefon", telefon_textBox.Text);
                }

                if (Marka_CheckBox.Checked)
                {
                    sorgu += " AND AracMarka = @AracMarka";
                    komut.Parameters.AddWithValue("@AracMarka", marka_combobox.SelectedText);
                }

                if (plaka_CehckBox.Checked)
                {
                    sorgu += " AND Plaka = @Plaka";
                    komut.Parameters.AddWithValue("@Plaka", plaka_textBox.Text);
                }

                if (sTarih_CehckBox.Checked)
                {
                    sorgu += " AND BaslangicTarihi = @BaslangicTarihi";
                    // Not: Eğer TextBox kullanıyorsan girilen metnin geçerli bir tarih formatında olduğundan emin ol.
                    komut.Parameters.AddWithValue("@BaslangicTarihi", Convert.ToDateTime(starih_datepicker.Text));
                }

                if (eTarih_CehckBox.Checked)
                {
                    sorgu += " AND BitisTarihi = @BitisTarihi";
                    komut.Parameters.AddWithValue("@BitisTarihi", Convert.ToDateTime(etarih_datepicker.Text));
                }

                if (abonelikDurum_CehckBox.Checked)
                {
                    sorgu += " AND AktifMi = @AktifMi";
                    komut.Parameters.AddWithValue("@AktifMi", durum_combobox.SelectedText);
                }

                // --- VERİTABANI İŞLEMLERİ ---
                Form1.BaglantiAc(); // Önceki projendeki bağlantı açma metodunu kullandığını varsayıyorum
                komut.Connection = Form1.Baglanti;
                komut.CommandText = sorgu; // Dinamik oluşturduğumuz sorguyu komuta atıyoruz

                // Verileri DataGridView'a çekmek için DataAdapter kullanıyoruz
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Sonuçları ekrana yansıt
                aramasonuclari_dataGridView.DataSource = dt;

                Form1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Arama sırasında bir hata oluştu:\n" + Hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string seciliAlan = "";
                object aranacakDeger = null;
                bool metinAramasiMi = true; // Sadece metinlerde "İçeren/Başlayan" mantığı aranır

                // 1. Hangi RadioButton seçili ve değer atamaları
                if (Ad_RadioButton.Checked)
                {
                    seciliAlan = "Ad";
                    aranacakDeger = aramaKutusu_textBox.Text.Trim();
                }
                else if (Soyad_RadioButton.Checked)
                {
                    seciliAlan = "Soyad";
                    aranacakDeger = aramaKutusu_textBox.Text.Trim();
                }
                else if (TC_RadioButton.Checked)
                {
                    seciliAlan = "TCNo";
                    aranacakDeger = aramaKutusu_textBox.Text.Trim();
                }
                else if (Telefon_RadioButton.Checked)
                {
                    seciliAlan = "Telefon";
                    // MaskedTextBox'taki gereksiz parantez, tire ve boşlukları temizleyip sadece rakamları alıyoruz
                    string telMetni = arananTel_textBox.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

                    if (string.IsNullOrEmpty(telMetni))
                        aranacakDeger = "";
                    else
                        aranacakDeger = telMetni;

                    metinAramasiMi = true;
                }
                else if (plaka_radioButton.Checked) // Plaka
                {
                    seciliAlan = "Plaka";
                    aranacakDeger = arananPlaka_TextBox.Text.Trim();
                }
                else if (marka_radioButton.Checked) // Marka
                {
                    seciliAlan = "AracMarka";
                    aranacakDeger = arananMarka.Text.Trim(); // ComboBox'tan seçilen metin
                    metinAramasiMi = false; // Marka için tam eşleşme aranır
                }
                else if (durum_radioButton.Checked) // Durum
                {
                    seciliAlan = "AktifMi";
                    aranacakDeger = arananDurum_Combo.Text.Trim();
                    metinAramasiMi = false; // Durum için tam eşleşme aranır
                }
                else if (starih_radioButton.Checked) // Başlangıç Tarihi
                {
                    seciliAlan = "BaslangicTarihi";
                    aranacakDeger = ArananTarih_DatePicker.Value.Date;
                    metinAramasiMi = false; // Tarihlerde tam eşleşme aranır
                }
                else if (etarih_radioButton.Checked) // Bitiş Tarihi
                {
                    seciliAlan = "BitisTarihi";
                    aranacakDeger = ArananTarih_DatePicker.Value.Date;
                    metinAramasiMi = false; // Tarihlerde tam eşleşme aranır
                }
                else
                {
                    // KONTROL 1: Hiçbir RadioButton seçilmemişse
                    MessageBox.Show("Lütfen arama yapmak için sol taraftan bir alan seçiniz (Ad, Plaka vb.).", "Eksik Seçim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Aranacak kelime/değer boş mu kontrolü
                if (aranacakDeger == null || aranacakDeger.ToString() == "")
                {
                    // KONTROL 2: Aranacak alan boş bırakılmışsa
                    MessageBox.Show("Lütfen aranacak değeri giriniz veya listeden seçiniz.", "Boş Değer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Arama Türü Kontrolü ve SQL Sorgusunu Hazırlama
                string sqlSorgu = "";
                SqlCommand komut = new SqlCommand();

                if (metinAramasiMi)
                {
                    // KONTROL 3: Arama türü (İçeren/Başlayan) seçilmemişse
                    // NOT: Arama türü ComboBox'ının adını cmbAramaTuru olarak varsaydım. Tasarımda farklıysa burayı güncellemelisin.
                    if (aramaTuru_combobox.SelectedIndex == -1 || aramaTuru_combobox.Text == "")
                    {
                        MessageBox.Show("Lütfen üst kısımdan arama türünü seçiniz (İle Başlayan, İçeren vb.).", "Eksik Seçim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string aramaTuru = aramaTuru_combobox.Text;
                    string sqlParametreDegeri = "";

                    if (aramaTuru == "İle Başlayan")
                        sqlParametreDegeri = aranacakDeger.ToString() + "%";
                    else if (aramaTuru == "İle Biten")
                        sqlParametreDegeri = "%" + aranacakDeger.ToString();
                    else // "İçeren"
                        sqlParametreDegeri = "%" + aranacakDeger.ToString() + "%";

                    sqlSorgu = $"SELECT * FROM Aboneler WHERE {seciliAlan} LIKE @ArananMetin";
                    komut.Parameters.AddWithValue("@ArananMetin", sqlParametreDegeri);
                }
                else
                {
                    // Tarih, Marka veya Durum gibi tam eşleşme gerektiren alanlar
                    sqlSorgu = $"SELECT * FROM Aboneler WHERE {seciliAlan} = @ArananDeger";

                    // Eğer aranan değer tarih ise veritabanına DateTime formatında gönder, değilse String gönder
                    if (aranacakDeger is DateTime)
                        komut.Parameters.Add("@ArananDeger", SqlDbType.Date).Value = aranacakDeger;
                    else
                        komut.Parameters.AddWithValue("@ArananDeger", aranacakDeger.ToString());
                }

                // 4. Veritabanı İşlemleri
                // Projende bağlantıyı yönettiğin ana formdan (Form1 varsayıldı) bağlantıyı açıyoruz
                Form1.BaglantiAc();
                komut.Connection = Form1.Baglanti;
                komut.CommandText = sqlSorgu;

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Sonuçları üstte yazdığın Birebir aramadaki gibi aynı DataGridView'a yansıtıyoruz
                aramasonuclari_dataGridView.DataSource = dt;

                Form1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Arama sırasında bir hata oluştu:\n" + Hata.Message, "Sistem Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void etarih_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            arananDurum_Combo.Enabled = false;
            arananDurum_Combo.Visible = false;

            arananMarka.Enabled = false;
            arananMarka.Visible = false;

            arananPlaka_TextBox.Enabled = false;
            arananPlaka_TextBox.Visible = false;

            arananTel_textBox.Enabled = false;
            arananTel_textBox.Visible = false;

            ArananTarih_DatePicker.Enabled = true;
            ArananTarih_DatePicker.Visible = true;

            aramaKutusu_textBox.Enabled = false;
            aramaKutusu_textBox.Visible = false;

            arananKelime_Label.Text = "Başlangıç Tarihi Arama";
        }

        private void Telefon_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            arananDurum_Combo.Enabled = false;
            arananDurum_Combo.Visible = false;

            arananMarka.Enabled = false;
            arananMarka.Visible = false;

            arananPlaka_TextBox.Enabled = false;
            arananPlaka_TextBox.Visible = false;

            arananTel_textBox.Enabled = true;
            arananTel_textBox.Visible = true;

            ArananTarih_DatePicker.Enabled = false;
            ArananTarih_DatePicker.Visible = false;

            aramaKutusu_textBox.Enabled = false;
            aramaKutusu_textBox.Visible = false;

            arananKelime_Label.Text = "Telefon Arama";
        }

        private void Ad_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            arananDurum_Combo.Enabled = false;
            arananDurum_Combo.Visible = false;

            arananMarka.Enabled = false;
            arananMarka.Visible = false;

            arananPlaka_TextBox.Enabled = false;
            arananPlaka_TextBox.Visible = false;

            arananTel_textBox.Enabled = false;
            arananTel_textBox.Visible = false;

            ArananTarih_DatePicker.Enabled = false;
            ArananTarih_DatePicker.Visible = false;

            aramaKutusu_textBox.Enabled = true;
            aramaKutusu_textBox.Visible = true;

            arananKelime_Label.Text = "Ad Arama";
        }

        private void Soyad_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            arananDurum_Combo.Enabled = false;
            arananDurum_Combo.Visible = false;

            arananMarka.Enabled = false;
            arananMarka.Visible = false;

            arananPlaka_TextBox.Enabled = false;
            arananPlaka_TextBox.Visible = false;

            arananTel_textBox.Enabled = false;
            arananTel_textBox.Visible = false;

            ArananTarih_DatePicker.Enabled = false;
            ArananTarih_DatePicker.Visible = false;

            aramaKutusu_textBox.Enabled = true;
            aramaKutusu_textBox.Visible = true;

            arananKelime_Label.Text = "Soyad Arama";
        }

        private void TC_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            arananDurum_Combo.Enabled = false;
            arananDurum_Combo.Visible = false;

            arananMarka.Enabled = false;
            arananMarka.Visible = false;

            arananPlaka_TextBox.Enabled = false;
            arananPlaka_TextBox.Visible = false;

            arananTel_textBox.Enabled = false;
            arananTel_textBox.Visible = false;

            ArananTarih_DatePicker.Enabled = false;
            ArananTarih_DatePicker.Visible = false;

            aramaKutusu_textBox.Enabled = true;
            aramaKutusu_textBox.Visible = true;

            arananKelime_Label.Text = "TC Arama";
        }
    }
}
