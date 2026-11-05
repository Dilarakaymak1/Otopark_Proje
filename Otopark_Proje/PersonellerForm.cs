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
    public partial class PersonellerForm : Form
    {
        int secilenPersonelID = 0;

        public PersonellerForm()
        {
            InitializeComponent();

            // --- YENİ EKLENEN KISIM: Ad ve Soyad kutucuklarını harf kontrolüne bağlıyoruz ---
            Personel_Ad.KeyPress += new KeyPressEventHandler(this.Personel_Ad_KeyPress);
            Personel_Soyad.KeyPress += new KeyPressEventHandler(this.Personel_Soyad_KeyPress);
        }

        private void PersonellerForm_Load(object sender, EventArgs e)
        {
            Form1.KayitListele(Personel_DataGrid, "Personeller");
        }

        // --- COMBOBOX ÖĞELERİ DÜZENLEME ---
        private void Personel_OgeleriDuzenle_Click(object sender, EventArgs e)
        {
            var comboBoxListesi = new Dictionary<string, ComboBox>
            {
                { "Görevi", Personel_Gorevi },
                { "Durum", Personel_Durum }
            };
            ComboBoxDuzenleHelper.PopupAc("Personel Kayıt - ComboBox Düzenle", comboBoxListesi);
        }

        // --- YENİ KAYIT EKLEME ---
        private void Personel_KayitEkle_Click(object sender, EventArgs e)
        {
            // --- 1. ADIM: BOŞ ALAN KONTROLÜ (VALİDASYON) ---
            bool bosAlanVarMi = false;

            // Önce tüm kutuların rengini varsayılana (beyaz) çevirelim
            Personel_Ad.BackColor = Color.White;
            Personel_Soyad.BackColor = Color.White;
            Personel_TCNo.BackColor = Color.White;
            Personel_Tel.BackColor = Color.White;
            Personel_Gorevi.BackColor = Color.White;
            Personel_Maas.BackColor = Color.White;
            Personel_Durum.BackColor = Color.White;
            Personel_Adres.BackColor = Color.White;

            // Tek tek boş mu diye kontrol edip, boşsa rengini değiştiriyoruz
            if (string.IsNullOrWhiteSpace(Personel_Ad.Text)) { Personel_Ad.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Personel_Soyad.Text)) { Personel_Soyad.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Personel_TCNo.Text) || Personel_TCNo.Text.Length != 11) { Personel_TCNo.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (!Personel_Tel.MaskCompleted) { Personel_Tel.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Personel_Gorevi.Text)) { Personel_Gorevi.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Personel_Maas.Text)) { Personel_Maas.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Personel_Durum.Text)) { Personel_Durum.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Personel_Adres.Text)) { Personel_Adres.BackColor = Color.LightPink; bosAlanVarMi = true; }

            if (bosAlanVarMi)
            {
                MessageBox.Show("Lütfen kırmızı ile işaretlenmiş zorunlu alanları doldurun!\n(TC No 11 haneli, Telefon eksiksiz olmalı)", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. ADIM: VERİTABANINA KAYIT EKLEME ---
            try
            {
                Form1.BaglantiAc();

                // Aynı TC No ile kayıt var mı kontrol et
                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM Personeller WHERE TCNo=@pTC", Form1.Baglanti);
                kontrolKomut.Parameters.AddWithValue("@pTC", Personel_TCNo.Text);
                int kayitSayisi = (int)kontrolKomut.ExecuteScalar();
                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu TC numarasıyla kayıtlı bir personel zaten mevcut!", "Kayıt Mevcut", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sorgu = "INSERT INTO Personeller (Ad, Soyad, TCNo, Telefon, Gorevi, IseGirisTarihi, Maas, Durum, Adres) " +
                               "VALUES (@pAd, @pSoyad, @pTCNo, @pTelefon, @pGorevi, @pIseGirisTarihi, @pMaas, @pDurum, @pAdres)";

                SqlCommand EkleKomut = new SqlCommand(sorgu, Form1.Baglanti);

                EkleKomut.Parameters.AddWithValue("@pAd", Personel_Ad.Text);
                EkleKomut.Parameters.AddWithValue("@pSoyad", Personel_Soyad.Text);
                EkleKomut.Parameters.AddWithValue("@pTCNo", Personel_TCNo.Text);
                EkleKomut.Parameters.AddWithValue("@pTelefon", Personel_Tel.Text);
                EkleKomut.Parameters.AddWithValue("@pGorevi", Personel_Gorevi.Text);
                EkleKomut.Parameters.AddWithValue("@pIseGirisTarihi", Personel_IseGirisTarihi.Value);

                decimal maas = 0;
                decimal.TryParse(Personel_Maas.Text, out maas);
                EkleKomut.Parameters.AddWithValue("@pMaas", maas);

                EkleKomut.Parameters.AddWithValue("@pDurum", Personel_Durum.Text);
                EkleKomut.Parameters.AddWithValue("@pAdres", string.IsNullOrWhiteSpace(Personel_Adres.Text) ? (object)DBNull.Value : Personel_Adres.Text);

                if (EkleKomut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Personel kaydı başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // --- 3. ADIM: DATAGRIDVIEW'I GÜNCELLEME ---
                    Form1.KayitListele(Personel_DataGrid, "Personeller");
                    Personel_VerileriTemizle_Click(sender, e);
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Kayıt eklenirken bir hata oluştu:\n" + Hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Form1.Baglanti.State == ConnectionState.Open)
                    Form1.Baglanti.Close();
            }
        }

        // --- DATAGRIDVIEW'DEN VERİ SEÇME (Tıklandığında araçlara doldurma) ---
        private void Personel_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = Personel_DataGrid.Rows[e.RowIndex];

                // 1. ID'yi değişkene al
                secilenPersonelID = Convert.ToInt32(satir.Cells["PersonelId"].Value);

                // 2. Diğer hücrelerdeki verileri formdaki araçlara doldur
                Personel_Ad.Text = satir.Cells["Ad"].Value.ToString();
                Personel_Soyad.Text = satir.Cells["Soyad"].Value.ToString();
                Personel_TCNo.Text = satir.Cells["TCNo"].Value.ToString();
                Personel_Tel.Text = satir.Cells["Telefon"].Value?.ToString() ?? "";

                // Görevi ComboBox
                string gorevi = satir.Cells["Gorevi"].Value.ToString();
                for (int i = 0; i < Personel_Gorevi.Items.Count; i++)
                {
                    if (Personel_Gorevi.Items[i].ToString() == gorevi)
                    {
                        Personel_Gorevi.SelectedIndex = i;
                        break;
                    }
                }

                Personel_IseGirisTarihi.Value = Convert.ToDateTime(satir.Cells["IseGirisTarihi"].Value);
                Personel_Maas.Text = satir.Cells["Maas"].Value.ToString();

                // Durum ComboBox
                string durum = satir.Cells["Durum"].Value.ToString();
                for (int i = 0; i < Personel_Durum.Items.Count; i++)
                {
                    if (Personel_Durum.Items[i].ToString() == durum)
                    {
                        Personel_Durum.SelectedIndex = i;
                        break;
                    }
                }

                Personel_Adres.Text = satir.Cells["Adres"].Value?.ToString() ?? "";
            }
        }

        // --- KAYIT GÜNCELLEME ---
        private void Personel_KayitGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenPersonelID == 0)
            {
                MessageBox.Show("Lütfen güncellemek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = Personel_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitGuncelleForm guncelleForm = new KayitGuncelleForm("Personeller", "Personel Kaydı", "PersonelId", secilenPersonelID, seciliSatir);
            if (guncelleForm.ShowDialog() == DialogResult.OK && guncelleForm.Kaydedildi)
            {
                Form1.KayitListele(Personel_DataGrid, "Personeller");
                Personel_VerileriTemizle_Click(sender, e);
            }
        }

        // --- KAYIT SİLME ---
        private void Personel_KayitSil_Click(object sender, EventArgs e)
        {
            if (secilenPersonelID == 0)
            {
                MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow seciliSatir = Personel_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitSilOnayForm onayForm = new KayitSilOnayForm("Personel Kaydı", seciliSatir);
            if (onayForm.ShowDialog() == DialogResult.OK && onayForm.Onaylandi)
            {
                try
                {
                    Form1.BaglantiAc();
                    SqlCommand SilKomut = new SqlCommand("DELETE FROM Personeller WHERE PersonelId=@pID", Form1.Baglanti);
                    SilKomut.Parameters.AddWithValue("@pID", secilenPersonelID);
                    SilKomut.ExecuteNonQuery();

                    MessageBox.Show("Kayıt başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(Personel_DataGrid, "Personeller");
                    Personel_VerileriTemizle_Click(sender, e);
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
        private void Personel_VerileriTemizle_Click(object sender, EventArgs e)
        {
            // Eğer alanlar zaten boşsa onay sorma
            bool alanlarBos = string.IsNullOrWhiteSpace(Personel_Ad.Text) && string.IsNullOrWhiteSpace(Personel_Soyad.Text) && secilenPersonelID == 0;
            if (!alanlarBos)
            {
                var sonuc = MessageBox.Show(
                    "Formdaki tüm alanlar temizlenecek.\nDevam etmek istiyor musunuz?",
                    "Verileri Temizle",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (sonuc == DialogResult.No) return;
            }
            // Textboxları temizle
            Personel_Ad.Clear();
            Personel_Soyad.Clear();
            Personel_TCNo.Clear();
            Personel_Tel.Clear();
            Personel_Maas.Clear();
            Personel_Adres.Clear();

            // Comboboxları sıfırla
            Personel_Gorevi.SelectedIndex = -1;
            Personel_Durum.SelectedIndex = -1;

            // Tarihleri bugüne sıfırla
            Personel_IseGirisTarihi.Value = DateTime.Now;

            // Renkleri sıfırla
            Personel_Ad.BackColor = Color.White;
            Personel_Soyad.BackColor = Color.White;
            Personel_TCNo.BackColor = Color.White;
            Personel_Tel.BackColor = Color.White;
            Personel_Gorevi.BackColor = Color.White;
            Personel_Maas.BackColor = Color.White;
            Personel_Durum.BackColor = Color.White;
            Personel_Adres.BackColor = Color.White;

            // Hafızadaki Seçili ID'yi sıfırla
            secilenPersonelID = 0;

            // Tablodaki seçimi kaldır
            Personel_DataGrid.ClearSelection();
        }

        // TC No sadece rakam girişine izin ver
        private void Personel_TCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Maaş sadece rakam ve nokta/virgül girişine izin ver
        private void Personel_Maas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        // --- YENİ EKLENEN KISIM: AD İÇİN SADECE HARF KONTROLÜ ---
        private void Personel_Ad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // --- YENİ EKLENEN KISIM: SOYAD İÇİN SADECE HARF KONTROLÜ ---
        private void Personel_Soyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}