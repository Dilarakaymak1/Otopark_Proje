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
    public partial class KullanicilarForm : Form
    {
        int secilenKullaniciID = 0;

        public KullanicilarForm()
        {
            InitializeComponent();
        }

        private void KullanicilarForm_Load(object sender, EventArgs e)
        {
            // Personel combobox'ını doldur
            try
            {
                Form1.BaglantiAc();
                SqlCommand cmd = new SqlCommand("SELECT PersonelId, Ad + ' ' + Soyad AS AdSoyad FROM Personeller WHERE Durum=N'Aktif'", Form1.Baglanti);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                Kullanici_Personel.DisplayMember = "AdSoyad";
                Kullanici_Personel.ValueMember = "PersonelId";
                Kullanici_Personel.DataSource = dt;
                Kullanici_Personel.SelectedIndex = -1;
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Personel listesi yüklenirken hata oluştu:\n" + Hata.Message, "Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Form1.Baglanti.State == ConnectionState.Open)
                    Form1.Baglanti.Close();
            }

            Form1.KayitListele(Kullanici_DataGrid, "Kullanicilar");
        }

        // --- YENİ KAYIT EKLEME ---
        private void Kullanici_KayitEkle_Click(object sender, EventArgs e)
        {
            bool bosAlanVarMi = false;

            Kullanici_KullaniciAdi.BackColor = Color.White;
            Kullanici_Sifre.BackColor = Color.White;
            Kullanici_Rol.BackColor = Color.White;
            Kullanici_Personel.BackColor = Color.White;
            Kullanici_AktifMi.BackColor = Color.White;
            Kullanici_Aciklama.BackColor = Color.White;

            if (string.IsNullOrWhiteSpace(Kullanici_KullaniciAdi.Text)) { Kullanici_KullaniciAdi.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Kullanici_Sifre.Text)) { Kullanici_Sifre.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Kullanici_Rol.Text)) { Kullanici_Rol.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (Kullanici_Personel.SelectedIndex == -1) { Kullanici_Personel.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Kullanici_AktifMi.Text)) { Kullanici_AktifMi.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Kullanici_Aciklama.Text)) { Kullanici_Aciklama.BackColor = Color.LightPink; bosAlanVarMi = true; }

            if (bosAlanVarMi)
            {
                MessageBox.Show("Lütfen kırmızı ile işaretlenmiş zorunlu alanları doldurun!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Form1.BaglantiAc();

                // Aynı kullanıcı adı var mı kontrol et
                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi=@pK", Form1.Baglanti);
                kontrolKomut.Parameters.AddWithValue("@pK", Kullanici_KullaniciAdi.Text);
                int kayitSayisi = (int)kontrolKomut.ExecuteScalar();
                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor!", "Kayıt Mevcut", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sorgu = "INSERT INTO Kullanicilar (PersonelId, KullaniciAdi, SifreHash, Rol, AktifMi, Aciklama) " +
                               "VALUES (@pPersonelId, @pKullaniciAdi, @pSifreHash, @pRol, @pAktifMi, @pAciklama)";

                SqlCommand EkleKomut = new SqlCommand(sorgu, Form1.Baglanti);

                if (Kullanici_Personel.SelectedValue != null)
                    EkleKomut.Parameters.AddWithValue("@pPersonelId", Kullanici_Personel.SelectedValue);
                else
                    EkleKomut.Parameters.AddWithValue("@pPersonelId", DBNull.Value);

                EkleKomut.Parameters.AddWithValue("@pKullaniciAdi", Kullanici_KullaniciAdi.Text);
                EkleKomut.Parameters.AddWithValue("@pSifreHash", Kullanici_Sifre.Text);
                EkleKomut.Parameters.AddWithValue("@pRol", Kullanici_Rol.Text);

                bool aktifMi = (Kullanici_AktifMi.Text == "Aktif");
                EkleKomut.Parameters.AddWithValue("@pAktifMi", aktifMi);

                EkleKomut.Parameters.AddWithValue("@pAciklama", string.IsNullOrWhiteSpace(Kullanici_Aciklama.Text) ? (object)DBNull.Value : Kullanici_Aciklama.Text);

                if (EkleKomut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kullanıcı kaydı başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(Kullanici_DataGrid, "Kullanicilar");
                    Kullanici_VerileriTemizle_Click(sender, e);
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Kayıt eklenirken hata oluştu:\n" + Hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Form1.Baglanti.State == ConnectionState.Open)
                    Form1.Baglanti.Close();
            }
        }

        // --- DATAGRIDVIEW'DEN SEÇİM ---
        private void Kullanici_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = Kullanici_DataGrid.Rows[e.RowIndex];

                secilenKullaniciID = Convert.ToInt32(satir.Cells["KullaniciId"].Value);

                if (satir.Cells["PersonelId"].Value != DBNull.Value)
                    Kullanici_Personel.SelectedValue = satir.Cells["PersonelId"].Value;

                Kullanici_KullaniciAdi.Text = satir.Cells["KullaniciAdi"].Value.ToString();
                Kullanici_Sifre.Text = ""; // Güvenlik: Şifreyi gösterme

                string rol = satir.Cells["Rol"].Value.ToString();
                for (int i = 0; i < Kullanici_Rol.Items.Count; i++)
                {
                    if (Kullanici_Rol.Items[i].ToString() == rol)
                    {
                        Kullanici_Rol.SelectedIndex = i;
                        break;
                    }
                }

                bool aktifMi = Convert.ToBoolean(satir.Cells["AktifMi"].Value);
                Kullanici_AktifMi.Text = aktifMi ? "Aktif" : "Pasif";

                Kullanici_Aciklama.Text = satir.Cells["Aciklama"].Value?.ToString() ?? "";
            }
        }

        // --- KAYIT GÜNCELLEME ---
        private void Kullanici_KayitGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenKullaniciID == 0)
            {
                MessageBox.Show("Lütfen güncellemek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = Kullanici_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitGuncelleForm guncelleForm = new KayitGuncelleForm("Kullanicilar", "Kullanıcı", "KullaniciId", secilenKullaniciID, seciliSatir);
            if (guncelleForm.ShowDialog() == DialogResult.OK && guncelleForm.Kaydedildi)
            {
                Form1.KayitListele(Kullanici_DataGrid, "Kullanicilar");
                Kullanici_VerileriTemizle_Click(sender, e);
            }
        }

        private void Kullanici_KayitSil_Click(object sender, EventArgs e)
        {
            if (secilenKullaniciID == 0)
            {
                MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = Kullanici_DataGrid.CurrentRow;
            if (seciliSatir == null) return;
            KayitSilOnayForm onayForm = new KayitSilOnayForm("Kullanıcı Kaydı", seciliSatir);
            if (onayForm.ShowDialog() == DialogResult.OK && onayForm.Onaylandi)
            {
                try
                {
                    Form1.BaglantiAc();
                    SqlCommand SilKomut = new SqlCommand("DELETE FROM Kullanicilar WHERE KullaniciId=@pID", Form1.Baglanti);
                    SilKomut.Parameters.AddWithValue("@pID", secilenKullaniciID);
                    SilKomut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(Kullanici_DataGrid, "Kullanicilar");
                    Kullanici_VerileriTemizle_Click(sender, e);
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

        // --- ARAMA YAPMA ---
        private void AramaYapma_Click(object sender, EventArgs e)
        {

        }

            // --- VERİLERİ TEMİZLE ---
        private void Kullanici_VerileriTemizle_Click(object sender, EventArgs e)
        {
            Kullanici_KullaniciAdi.Clear();
            Kullanici_Sifre.Clear();
            Kullanici_Aciklama.Clear();
            Kullanici_Personel.SelectedIndex = -1;
            Kullanici_Rol.SelectedIndex = -1;
            Kullanici_AktifMi.SelectedIndex = -1;

            Kullanici_KullaniciAdi.BackColor = Color.White;
            Kullanici_Sifre.BackColor = Color.White;
            Kullanici_Rol.BackColor = Color.White;
            Kullanici_Personel.BackColor = Color.White;
            Kullanici_AktifMi.BackColor = Color.White;
            Kullanici_Aciklama.BackColor = Color.White;

            secilenKullaniciID = 0;
            Kullanici_DataGrid.ClearSelection();
        }

        private void Kullanici_Personel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rol_ekle_but_Click(object sender, EventArgs e)
        {
            var comboBoxListesi = new Dictionary<string, ComboBox>
            {
                { "Rol", Kullanici_Rol },
                { "Aktif Durumu", Kullanici_AktifMi }
            };

            ComboBoxDuzenleForm duzenleForm = new ComboBoxDuzenleForm("Kullanıcı - ComboBox Düzenle", comboBoxListesi);
            if (duzenleForm.ShowDialog() == DialogResult.OK)
            {
                foreach (var kvp in comboBoxListesi)
                {
                    string comboAd = kvp.Key;
                    ComboBox comboBox = kvp.Value;
                    if (comboBox.DataSource != null) continue;

                    if (duzenleForm.GuncelVeriler.ContainsKey(comboAd))
                    {
                        string mevcutSecim = comboBox.Text;
                        comboBox.Items.Clear();
                        foreach (string oge in duzenleForm.GuncelVeriler[comboAd])
                        {
                            comboBox.Items.Add(oge);
                        }
                        int idx = comboBox.FindStringExact(mevcutSecim);
                        if (idx >= 0) comboBox.SelectedIndex = idx;
                        else comboBox.SelectedIndex = -1;
                    }
                }
            }
        }
    }
}

