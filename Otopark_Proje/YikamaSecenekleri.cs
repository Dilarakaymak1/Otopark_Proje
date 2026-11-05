using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Veritabanı işlemleri için gerekli
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otopark_Proje
{
    public partial class YikamaSecenekleri : Form
    {
        // Güncelleme işlemi için kullanacağımız hafıza değişkeni (Görseldeki YikamaId sütunu)
        int secilenYikamaID = 0;

        public YikamaSecenekleri()
        {
            InitializeComponent();
        }

        // Form açıldığında verileri listele
        private void YikamaSecenekleri_Load(object sender, EventArgs e)
        {
            Form1.KayitListele(YikamaSecenek_DataGrid, "AracYikama");
        }

        // --- COMBOBOX ÖĞELERİ DÜZENLEME ---
        private void YikamaSecenek_OgeleriDuzenle_Click(object sender, EventArgs e)
        {
            var comboBoxListesi = new Dictionary<string, ComboBox>
            {
                { "Ödeme Durumu", YikamaSecenek_OdemeDurum },
                { "Yıkama Personeli", YikamaSecenek_Personel },
                { "Yıkama Tipi", YikamaSecenek_YikamaTipi },
                { "Yıkama Durumu", YikamaSecenek_YikamaDurum }
            };
            ComboBoxDuzenleHelper.PopupAc("Yıkama Seçenekleri - ComboBox Düzenle", comboBoxListesi);
        }

        // --- YENİ KAYIT EKLEME BUTONU ---
        private void YikamaSecenek_KayitEkle_Button_Click(object sender, EventArgs e)
        {
            // --- 1. ADIM: BOŞ ALAN KONTROLÜ (VALİDASYON) ---
            bool bosAlanVarMi = false;

            // Önce tüm kutuların rengini varsayılana (beyaz) çevirelim
            YikamaSecenek_Plaka.BackColor = Color.White;
            YikamaSecenek_YikamaTipi.BackColor = Color.White;
            YikamaSecenek_Fiyat.BackColor = Color.White;
            YikamaSecenek_Personel.BackColor = Color.White;
            YikamaSecenek_YikamaDurum.BackColor = Color.White;
            YikamaSecenek_OdemeDurum.BackColor = Color.White;

            // Tek tek boş mu diye kontrol edip, boşsa rengini değiştiriyoruz
            if (!YikamaSecenek_Plaka.MaskCompleted) { YikamaSecenek_Plaka.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(YikamaSecenek_YikamaTipi.Text)) { YikamaSecenek_YikamaTipi.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(YikamaSecenek_Fiyat.Text)) { YikamaSecenek_Fiyat.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(YikamaSecenek_Personel.Text)) { YikamaSecenek_Personel.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(YikamaSecenek_YikamaDurum.Text)) { YikamaSecenek_YikamaDurum.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(YikamaSecenek_OdemeDurum.Text)) { YikamaSecenek_OdemeDurum.BackColor = Color.LightPink; bosAlanVarMi = true; }

            // Eğer herhangi bir alan boşsa, işlemi durdur
            if (bosAlanVarMi)
            {
                MessageBox.Show("Lütfen kırmızı ile işaretlenmiş zorunlu alanları doldurun!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. ADIM: VERİTABANINA KAYIT EKLEME ---
            try
            {
                Form1.BaglantiAc();

                // Aynı plaka ile devam eden yıkama var mı kontrol et
                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM AracYikama WHERE Plaka=@pP AND Durum=N'Devam Ediyor'", Form1.Baglanti);
                kontrolKomut.Parameters.AddWithValue("@pP", YikamaSecenek_Plaka.Text);
                int kayitSayisi = (int)kontrolKomut.ExecuteScalar();
                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu plakalı araç için devam eden bir yıkama işlemi zaten var!", "Kayıt Mevcut", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tablo adı: AracYikama. Sütun isimleri görselden alındı.
                string sorgu = "INSERT INTO AracYikama (Plaka, YikamaTipi, Fiyat, YikamaciPersonel, BaslangicSaati, BitisSaati, Durum, OdemeAlindiMi, Tarih) " +
                               "VALUES (@pPlaka, @pYikamaTipi, @pFiyat, @pPersonel, @pBaslangicSaati, @pBitisSaati, @pDurum, @pOdemeAlindiMi, @pTarih)";

                SqlCommand EkleKomut = new SqlCommand(sorgu, Form1.Baglanti);

                EkleKomut.Parameters.AddWithValue("@pPlaka", YikamaSecenek_Plaka.Text);
                EkleKomut.Parameters.AddWithValue("@pYikamaTipi", YikamaSecenek_YikamaTipi.Text);

                // Fiyatı ondalıklı (decimal) sayıya çevirerek gönderiyoruz
                EkleKomut.Parameters.AddWithValue("@pFiyat", Convert.ToDecimal(YikamaSecenek_Fiyat.Text));
                EkleKomut.Parameters.AddWithValue("@pPersonel", YikamaSecenek_Personel.Text);
                EkleKomut.Parameters.AddWithValue("@pBaslangicSaati", YikamaSecenek_Ssaat.Value);
                EkleKomut.Parameters.AddWithValue("@pBitisSaati", YikamaSecenek_Esaat.Value);
                EkleKomut.Parameters.AddWithValue("@pDurum", YikamaSecenek_YikamaDurum.Text);

                // Görselde 'True/False' (bit) tutulduğunu görüyoruz. ComboBox'ta "Ödendi" veya "Evet" seçiliyse True yaparız.
                // Eğer kendi ComboBox'ında "True/False" yazıyorsa doğrudan == "True" yapabilirsin.
                bool odemeAlindiMi = (YikamaSecenek_OdemeDurum.Text == "Ödendi" || YikamaSecenek_OdemeDurum.Text == "True") ? true : false;
                EkleKomut.Parameters.AddWithValue("@pOdemeAlindiMi", odemeAlindiMi);

                EkleKomut.Parameters.AddWithValue("@pTarih", YikamaSecenek_Tarih.Value.Date); // Sadece tarihi alır

                if (EkleKomut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Yıkama kaydı başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(YikamaSecenek_DataGrid, "AracYikama"); // Datagrid'i yenile

                    // İsteğe bağlı: Kayıt eklendikten sonra kutuları temizlemek için aşağıdaki fonksiyonu çağırabiliriz
                    YikamaSecenek_VerileriSil_Click(sender, e);
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Kayıt eklenirken bir hata oluştu:\n" + Hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Form1.Baglanti.State == System.Data.ConnectionState.Open)
                {
                    Form1.Baglanti.Close();
                }
            }
        }

        // --- DATAGRIDVIEW'DEN VERİ SEÇME (Tıklandığında araçlara doldurma) ---
        // Not: CellContentClick sadece yazının üstüne tıklayınca çalışır. Daha iyi bir deneyim için
        // DataGridView'in events (şimşek) bölümünden CellClick olayını açıp bu kodu ona taşıyabilirsin.
        private void YikamaSecenek_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = YikamaSecenek_DataGrid.Rows[e.RowIndex];

                // Görseldeki ID sütununun adı "YikamaId"
                secilenYikamaID = Convert.ToInt32(satir.Cells["YikamaId"].Value);

                YikamaSecenek_Plaka.Text = satir.Cells["Plaka"].Value.ToString();

                if(satir.Cells["YikamaTipi"].Value.ToString() == "İç")
                    YikamaSecenek_YikamaTipi.Text = YikamaSecenek_YikamaTipi.Items[0].ToString();
                else if (satir.Cells["YikamaTipi"].Value.ToString() == "Dış")
                    YikamaSecenek_YikamaTipi.Text = YikamaSecenek_YikamaTipi.Items[1].ToString();
                else
                    YikamaSecenek_YikamaTipi.Text = YikamaSecenek_YikamaTipi.Items[2].ToString();

                YikamaSecenek_Fiyat.Text = satir.Cells["Fiyat"].Value.ToString();
               

                if (satir.Cells["YikamaciPersonel"].Value.ToString() == "Ali Yılmaz")
                    YikamaSecenek_Personel.Text = YikamaSecenek_Personel.Items[0].ToString();
                else if (satir.Cells["YikamaciPersonel"].Value.ToString() == "Veli Topal")
                    YikamaSecenek_Personel.Text = YikamaSecenek_Personel.Items[1].ToString();
                else
                    YikamaSecenek_Personel.Text = YikamaSecenek_Personel.Items[2].ToString();

                YikamaSecenek_Ssaat.Value = Convert.ToDateTime(satir.Cells["BaslangicSaati"].Value);
                YikamaSecenek_Esaat.Value = Convert.ToDateTime(satir.Cells["BitisSaati"].Value);

                if (satir.Cells["Durum"].Value.ToString() == "Tamamlandı")
                    YikamaSecenek_YikamaDurum.Text = YikamaSecenek_YikamaDurum.Items[0].ToString();
                else if (satir.Cells["Durum"].Value.ToString() == "Devam Ediyor")
                    YikamaSecenek_YikamaDurum.Text = YikamaSecenek_YikamaDurum.Items[1].ToString();
                else
                    YikamaSecenek_YikamaDurum.Text = YikamaSecenek_YikamaDurum.Items[2].ToString();

                // Ödeme durumu CheckBox yerine ComboBox kullandığın için text olarak geri ayarlıyoruz
                if (Convert.ToBoolean(satir.Cells["OdemeAlindiMi"].Value) == true)
                    YikamaSecenek_OdemeDurum.Text = YikamaSecenek_OdemeDurum.Items[0].ToString(); // Kendi combobox'ındaki uygun yazıyla değiştir ("True" vs)
                else
                    YikamaSecenek_OdemeDurum.Text = YikamaSecenek_OdemeDurum.Items[1].ToString();

                YikamaSecenek_Tarih.Value = Convert.ToDateTime(satir.Cells["Tarih"].Value);
            }
        }

        // --- KAYIT GÜNCELLEME BUTONU ---
        private void YikamaSecenek_KayitGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenYikamaID == 0)
            {
                MessageBox.Show("Lütfen güncellemek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = YikamaSecenek_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitGuncelleForm guncelleForm = new KayitGuncelleForm("AracYikama", "Yıkama Seçeneği", "YikamaId", secilenYikamaID, seciliSatir);
            if (guncelleForm.ShowDialog() == DialogResult.OK && guncelleForm.Kaydedildi)
            {
                Form1.KayitListele(YikamaSecenek_DataGrid, "AracYikama");
                YikamaSecenek_VerileriSil_Click(sender, e);
            }
        }

        private void YikamaSecenek_KayitSil_Click(object sender, EventArgs e)
        {
            if (secilenYikamaID == 0)
            {
                MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = YikamaSecenek_DataGrid.CurrentRow;
            if (seciliSatir == null) return;
            KayitSilOnayForm onayForm = new KayitSilOnayForm("Yıkama Kaydı", seciliSatir);
            if (onayForm.ShowDialog() == DialogResult.OK && onayForm.Onaylandi)
            {
                try
                {
                    Form1.BaglantiAc();
                    SqlCommand SilKomut = new SqlCommand("DELETE FROM AracYikama WHERE YikamaId=@pID", Form1.Baglanti);
                    SilKomut.Parameters.AddWithValue("@pID", secilenYikamaID);
                    SilKomut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(YikamaSecenek_DataGrid, "AracYikama");
                    YikamaSecenek_VerileriSil_Click(sender, e);
                }
                catch (Exception Hata)
                {
                    MessageBox.Show("Kayıt silinirken hata oluştu:\n" + Hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (Form1.Baglanti.State == ConnectionState.Open)
                        Form1.Baglanti.Close();
                }
            }
        }

        // --- VERİLERİ TEMİZLEME BUTONU ---
        private void YikamaSecenek_VerileriSil_Click(object sender, EventArgs e)
        {
            YikamaSecenek_Plaka.Text = "";
            YikamaSecenek_Fiyat.Clear();
            YikamaSecenek_YikamaTipi.SelectedIndex = -1;
            YikamaSecenek_Personel.SelectedIndex = -1;
            YikamaSecenek_YikamaDurum.SelectedIndex = -1;
            YikamaSecenek_OdemeDurum.SelectedIndex = -1;
            YikamaSecenek_Ssaat.Value = DateTime.Now;
            YikamaSecenek_Esaat.Value = DateTime.Now;
            YikamaSecenek_Tarih.Value = DateTime.Now;
            YikamaSecenek_Plaka.BackColor = Color.White;
            YikamaSecenek_YikamaTipi.BackColor = Color.White;
            YikamaSecenek_Fiyat.BackColor = Color.White;
            YikamaSecenek_Personel.BackColor = Color.White;
            YikamaSecenek_YikamaDurum.BackColor = Color.White;
            YikamaSecenek_OdemeDurum.BackColor = Color.White;
            secilenYikamaID = 0;
            YikamaSecenek_DataGrid.ClearSelection();
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void YikamaSecenek_Plaka_TextChanged(object sender, EventArgs e) { }
        private void YikamaSecenek_Fiyat_TextChanged(object sender, EventArgs e) { }
        private void YikamaSecenek_YikamaDurum_SelectedIndexChanged(object sender, EventArgs e) { }
        private void YikamaSecenek_Ssaat_ValueChanged(object sender, EventArgs e) { }
        private void YikamaSecenek_OdemeDurum_SelectedIndexChanged(object sender, EventArgs e) { }
        private void YikamaSecenek_Esaat_ValueChanged(object sender, EventArgs e) { }
        private void YikamaSecenek_Tarih_ValueChanged(object sender, EventArgs e) { }
        private void YikamaSecenek_Personel_SelectedIndexChanged(object sender, EventArgs e) { }
        private void YikamaSecenek_YikamaTipi_SelectedIndexChanged(object sender, EventArgs e) { }
        private void YikamaSecenek_DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void YikamaSecenek_Fiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}