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
    public partial class YeniAboneEkle : Form
    {

        int secilenKayitID = 0;
        public static bool YeniAboneEkle_aramaAcik = false;

        public YeniAboneEkle()
        {
            InitializeComponent();

            // Kutucuklar ile yazdığımız harf kontrolü kodlarını birbirine bağlıyoruz:
            AboneKayit_Ad.KeyPress += new KeyPressEventHandler(this.AboneKayit_Ad_KeyPress);
            AboneKayit_Soyad.KeyPress += new KeyPressEventHandler(this.AboneKayit_Soyad_KeyPress);
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            // 1. Metin kutularını ve Maskeli kutuyu temizle
            AboneKayit_Ad.Clear();
            AboneKayit_Soyad.Clear();
            AboneKayit_TCno.Clear();
            AboneKayit_Tel.Clear();
            AboneKayit_Plaka.Text = "";
            AboneKayit_Marka.SelectedIndex = -1;

            // 2. ComboBox'ların seçimini kaldır
            AboneKayit_Durum.SelectedIndex = -1;

            // 3. Tarihleri bugüne sıfırla
            AboneKayit_STarihi.Value = DateTime.Now;
            AboneKayit_ETarihi.Value = DateTime.Now;

            // 4. Hata uyarısından kalan pembe renkleri beyaza döndür
            AboneKayit_Ad.BackColor = Color.White;
            AboneKayit_Soyad.BackColor = Color.White;
            AboneKayit_TCno.BackColor = Color.White;
            AboneKayit_Tel.BackColor = Color.White;
            AboneKayit_Plaka.BackColor = Color.White;
            AboneKayit_Marka.BackColor = Color.White;
            AboneKayit_Durum.BackColor = Color.White;

            // 5. Hafızadaki Seçili ID'yi sıfırla
            secilenKayitID = 0;

            // Tablodaki mavi seçili satır görüntüsünü kaldırır
            AboneEkle_DataGrid.ClearSelection();
        }

        private void YeniAboneEkle_Load(object sender, EventArgs e)
        {
            Form1.KayitListele(AboneEkle_DataGrid, "Aboneler");
        }

        // --- COMBOBOX ÖĞELERİ DÜZENLEME ---
        private void AboneEkle_OgeleriDuzenle_Click(object sender, EventArgs e)
        {
            var comboBoxListesi = new Dictionary<string, ComboBox>
            {
                { "Araç Markası", AboneKayit_Marka },
                { "Abonelik Durumu", AboneKayit_Durum }
            };
            ComboBoxDuzenleHelper.PopupAc("Yeni Abone Ekle - ComboBox Düzenle", comboBoxListesi);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // --- 1. ADIM: BOŞ ALAN KONTROLÜ (VALİDASYON) ---
            bool bosAlanVarMi = false;

            // Önce tüm kutuların rengini varsayılana (beyaz) çevirelim
            AboneKayit_Ad.BackColor = Color.White;
            AboneKayit_Soyad.BackColor = Color.White;
            AboneKayit_TCno.BackColor = Color.White;
            AboneKayit_Tel.BackColor = Color.White;
            AboneKayit_Plaka.BackColor = Color.White;
            AboneKayit_Marka.BackColor = Color.White;
            AboneKayit_Durum.BackColor = Color.White;

            // Tek tek boş mu diye kontrol edip, boşsa rengini değiştiriyoruz
            if (string.IsNullOrWhiteSpace(AboneKayit_Ad.Text)) { AboneKayit_Ad.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(AboneKayit_Soyad.Text)) { AboneKayit_Soyad.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(AboneKayit_TCno.Text)) { AboneKayit_TCno.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(AboneKayit_Tel.Text)) { AboneKayit_Tel.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (!AboneKayit_Plaka.MaskCompleted) { AboneKayit_Plaka.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(AboneKayit_Marka.Text)) { AboneKayit_Marka.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(AboneKayit_Durum.Text)) { AboneKayit_Durum.BackColor = Color.LightPink; bosAlanVarMi = true; }

            // Eğer herhangi bir alan boşsa, kullanıcıyı uyar ve işlemi durdur
            if (bosAlanVarMi)
            {
                MessageBox.Show("Lütfen kırmızı ile işaretlenmiş zorunlu alanları doldurun!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. ADIM: VERİTABANINA KAYIT EKLEME ---
            try
            {
                Form1.BaglantiAc();

                // Aynı TC veya Plaka ile kayıt var mı kontrol et
                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM Aboneler WHERE TCNo=@pTC OR Plaka=@pP", Form1.Baglanti);
                kontrolKomut.Parameters.AddWithValue("@pTC", AboneKayit_TCno.Text);
                kontrolKomut.Parameters.AddWithValue("@pP", AboneKayit_Plaka.Text);
                int kayitSayisi = (int)kontrolKomut.ExecuteScalar();
                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu TC numarası veya plaka ile kayıtlı bir abone zaten mevcut!", "Kayıt Mevcut", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sorgu = "INSERT INTO Aboneler (Ad, Soyad, TCNo, Telefon, Plaka, AracMarka, BaslangicTarihi, BitisTarihi, AktifMi) " +
                               "VALUES (@pAd, @pSoyad, @pTCNo, @pTelefon, @pPlaka, @pAracMarka, @pBaslangicTarihi, @pBitisTarihi, @pAktifMi)";

                SqlCommand EkleKomut = new SqlCommand(sorgu, Form1.Baglanti);

                EkleKomut.Parameters.AddWithValue("@pAd", AboneKayit_Ad.Text);
                EkleKomut.Parameters.AddWithValue("@pSoyad", AboneKayit_Soyad.Text);
                EkleKomut.Parameters.AddWithValue("@pTCNo", AboneKayit_TCno.Text);
                EkleKomut.Parameters.AddWithValue("@pTelefon", AboneKayit_Tel.Text);
                EkleKomut.Parameters.AddWithValue("@pPlaka", AboneKayit_Plaka.Text);
                EkleKomut.Parameters.AddWithValue("@pAracMarka", AboneKayit_Marka.Text);
                EkleKomut.Parameters.AddWithValue("@pBaslangicTarihi", AboneKayit_STarihi.Value);
                EkleKomut.Parameters.AddWithValue("@pBitisTarihi", AboneKayit_ETarihi.Value);
                EkleKomut.Parameters.AddWithValue("@pAktifMi", AboneKayit_Durum.Text);

                if (EkleKomut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // --- 3. ADIM: DATAGRIDVIEW'I GÜNCELLEME ---
                    Form1.KayitListele(AboneEkle_DataGrid, "Aboneler");
                    btnTemizle_Click(sender, e);
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

        private void YeniAboneEkle_Activated(object sender, EventArgs e)
        {

        }

        private void AboneKayit_TCno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // --- YENİ EKLENEN KISIM: AD İÇİN SADECE HARF KONTROLÜ ---
        private void AboneKayit_Ad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // --- YENİ EKLENEN KISIM: SOYAD İÇİN SADECE HARF KONTROLÜ ---
        private void AboneKayit_Soyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AboneEkle_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = AboneEkle_DataGrid.Rows[e.RowIndex];

                secilenKayitID = Convert.ToInt32(satir.Cells["AboneId"].Value);

                AboneKayit_Ad.Text = satir.Cells["Ad"].Value.ToString();
                AboneKayit_Soyad.Text = satir.Cells["Soyad"].Value.ToString();
                AboneKayit_TCno.Text = satir.Cells["TCNo"].Value.ToString();
                AboneKayit_Tel.Text = satir.Cells["Telefon"].Value.ToString();
                AboneKayit_Plaka.Text = satir.Cells["Plaka"].Value.ToString();
                string marka = satir.Cells["AracMarka"].Value.ToString();
                int markaIndex = AboneKayit_Marka.FindStringExact(marka);
                AboneKayit_Marka.SelectedIndex = markaIndex >= 0 ? markaIndex : AboneKayit_Marka.FindStringExact("Diğer");

                AboneKayit_STarihi.Value = Convert.ToDateTime(satir.Cells["BaslangicTarihi"].Value);
                AboneKayit_ETarihi.Value = Convert.ToDateTime(satir.Cells["BitisTarihi"].Value);

                if (satir.Cells["AktifMi"].Value.ToString() == "Aktif")
                    AboneKayit_Durum.Text = AboneKayit_Durum.Items[0].ToString();
                else
                    AboneKayit_Durum.Text = AboneKayit_Durum.Items[1].ToString();
            }
        }

        private void YeniAboneEkle_KayitGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenKayitID == 0)
            {
                MessageBox.Show("Lütfen güncellemek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = AboneEkle_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitGuncelleForm guncelleForm = new KayitGuncelleForm("Aboneler", "Abone", "AboneId", secilenKayitID, seciliSatir);
            if (guncelleForm.ShowDialog() == DialogResult.OK && guncelleForm.Kaydedildi)
            {
                Form1.KayitListele(AboneEkle_DataGrid, "Aboneler");
                btnTemizle_Click(sender, e);
            }
        }


        private void YeniAboneEkle_KayitSil_Click(object sender, EventArgs e)
        {
            if (secilenKayitID == 0)
            {
                MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = AboneEkle_DataGrid.CurrentRow;
            if (seciliSatir == null) return;
            KayitSilOnayForm onayForm = new KayitSilOnayForm("Abone Kaydı", seciliSatir);
            if (onayForm.ShowDialog() == DialogResult.OK && onayForm.Onaylandi)
            {
                try
                {
                    Form1.BaglantiAc();
                    SqlCommand SilKomut = new SqlCommand("DELETE FROM Aboneler WHERE AboneId=@pID", Form1.Baglanti);
                    SilKomut.Parameters.AddWithValue("@pID", secilenKayitID);
                    SilKomut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(AboneEkle_DataGrid, "Aboneler");
                    btnTemizle_Click(sender, e);
                }
                catch (Exception Hata)
                {
                    MessageBox.Show("Kayıt silinirken hata oluştu:\n" + Hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (Form1.Baglanti.State == System.Data.ConnectionState.Open)
                        Form1.Baglanti.Close();
                }
            }
        }

        private void BirebirAranaYap_Button_Click(object sender, EventArgs e)
        {
            if (YeniAboneEkle_aramaAcik)
            {
                MessageBox.Show("Arama Yapma Sekmesi Açık");
                return;
            }

            AramaYapmaForm aramaYapmaForm = new AramaYapmaForm();
            aramaYapmaForm.HizliArama_GroupBox.Enabled = false;
            aramaYapmaForm.HizliArama_GroupBox.Visible = false;

            aramaYapmaForm.BirebirArama_GroupBox.Enabled = true;
            aramaYapmaForm.BirebirArama_GroupBox.Visible = true;

            aramaYapmaForm.Show();

            YeniAboneEkle_aramaAcik = true;
        }

        private void Hizli_BenzerAranaYap_Button_Click(object sender, EventArgs e)
        {
            if (YeniAboneEkle_aramaAcik)
            {
                MessageBox.Show("Arama Yapma Sekmesi Açık");
                return;
            }

            AramaYapmaForm aramaYapmaForm = new AramaYapmaForm();
            aramaYapmaForm.HizliArama_GroupBox.Enabled = true;
            aramaYapmaForm.HizliArama_GroupBox.Visible = true;

            aramaYapmaForm.BirebirArama_GroupBox.Enabled = false;
            aramaYapmaForm.BirebirArama_GroupBox.Visible = false;

            aramaYapmaForm.Show();

            YeniAboneEkle_aramaAcik = true;
        }

        private void AboneKayit_Ad_TextChanged(object sender, EventArgs e)
        {

        }

        private void AboneKayit_Soyad_TextChanged(object sender, EventArgs e)
        {

        }
    }
}