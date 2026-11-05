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
    public partial class KasaIslemleriForm : Form
    {
        int secilenKasaID = 0;

        public KasaIslemleriForm()
        {
            InitializeComponent();
        }

        private void KasaIslemleriForm_Load(object sender, EventArgs e)
        {
            // Personel combobox'ını veritabanından doldur
            try
            {
                Form1.BaglantiAc();
                SqlCommand cmd = new SqlCommand("SELECT PersonelId, Ad + ' ' + Soyad AS AdSoyad FROM Personeller WHERE Durum=N'Aktif'", Form1.Baglanti);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                Kasa_Personel.DisplayMember = "AdSoyad";
                Kasa_Personel.ValueMember = "PersonelId";
                Kasa_Personel.DataSource = dt;
                Kasa_Personel.SelectedIndex = -1;
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

            Form1.KayitListele(Kasa_DataGrid, "Kasa");
        }

        // --- COMBOBOX ÖĞELERİ DÜZENLEME ---
        private void Kasa_OgeleriDuzenle_Click(object sender, EventArgs e)
        {
            var comboBoxListesi = new Dictionary<string, ComboBox>
            {
                { "İşlem Tipi", Kasa_IslemTipi },
                { "Kategori", Kasa_Kategori },
                { "Ödeme Yöntemi", Kasa_OdemeYontemi }
            };
            ComboBoxDuzenleHelper.PopupAc("Kasa İşlemleri - ComboBox Düzenle", comboBoxListesi);
        }

        // --- YENİ KAYIT EKLEME ---
        private void Kasa_KayitEkle_Click(object sender, EventArgs e)
        {
            bool bosAlanVarMi = false;

            Kasa_IslemTipi.BackColor = Color.White;
            Kasa_Tutar.BackColor = Color.White;
            Kasa_Aciklama.BackColor = Color.White;
            Kasa_Kategori.BackColor = Color.White;
            Kasa_Personel.BackColor = Color.White;
            Kasa_FisNo.BackColor = Color.White;
            Kasa_OdemeYontemi.BackColor = Color.White;

            if (string.IsNullOrWhiteSpace(Kasa_IslemTipi.Text)) { Kasa_IslemTipi.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Kasa_Tutar.Text)) { Kasa_Tutar.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Kasa_Aciklama.Text)) { Kasa_Aciklama.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Kasa_Kategori.Text)) { Kasa_Kategori.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (Kasa_Personel.SelectedIndex == -1) { Kasa_Personel.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Kasa_FisNo.Text)) { Kasa_FisNo.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Kasa_OdemeYontemi.Text)) { Kasa_OdemeYontemi.BackColor = Color.LightPink; bosAlanVarMi = true; }

            if (bosAlanVarMi)
            {
                MessageBox.Show("Lütfen kırmızı ile işaretlenmiş zorunlu alanları doldurun!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Form1.BaglantiAc();

                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM Kasa WHERE FisNo=@pF", Form1.Baglanti);
                kontrolKomut.Parameters.AddWithValue("@pF", Kasa_FisNo.Text);
                int kayitSayisi = (int)kontrolKomut.ExecuteScalar();
                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu fiş numarasıyla bir kayıt zaten mevcut!", "Kayıt Mevcut", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sorgu = "INSERT INTO Kasa (IslemTipi, Tutar, Aciklama, Kategori, IslemTarihi, PersonelId, FisNo, OdemeYontemi) " +
                               "VALUES (@pIslemTipi, @pTutar, @pAciklama, @pKategori, @pIslemTarihi, @pPersonelId, @pFisNo, @pOdemeYontemi)";

                SqlCommand EkleKomut = new SqlCommand(sorgu, Form1.Baglanti);

                EkleKomut.Parameters.AddWithValue("@pIslemTipi", Kasa_IslemTipi.Text);

                decimal tutar = 0;
                decimal.TryParse(Kasa_Tutar.Text, out tutar);
                EkleKomut.Parameters.AddWithValue("@pTutar", tutar);

                EkleKomut.Parameters.AddWithValue("@pAciklama", string.IsNullOrWhiteSpace(Kasa_Aciklama.Text) ? (object)DBNull.Value : Kasa_Aciklama.Text);
                EkleKomut.Parameters.AddWithValue("@pKategori", string.IsNullOrWhiteSpace(Kasa_Kategori.Text) ? (object)DBNull.Value : Kasa_Kategori.Text);
                EkleKomut.Parameters.AddWithValue("@pIslemTarihi", Kasa_IslemTarihi.Value);

                if (Kasa_Personel.SelectedValue != null)
                    EkleKomut.Parameters.AddWithValue("@pPersonelId", Kasa_Personel.SelectedValue);
                else
                    EkleKomut.Parameters.AddWithValue("@pPersonelId", DBNull.Value);

                EkleKomut.Parameters.AddWithValue("@pFisNo", string.IsNullOrWhiteSpace(Kasa_FisNo.Text) ? (object)DBNull.Value : Kasa_FisNo.Text);
                EkleKomut.Parameters.AddWithValue("@pOdemeYontemi", Kasa_OdemeYontemi.Text);

                if (EkleKomut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kasa kaydı başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(Kasa_DataGrid, "Kasa");
                    Kasa_VerileriTemizle_Click(sender, e);
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
        private void Kasa_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = Kasa_DataGrid.Rows[e.RowIndex];

                secilenKasaID = Convert.ToInt32(satir.Cells["KasaId"].Value);

                // İşlem Tipi
                string islemTipi = satir.Cells["IslemTipi"].Value.ToString();
                for (int i = 0; i < Kasa_IslemTipi.Items.Count; i++)
                {
                    if (Kasa_IslemTipi.Items[i].ToString() == islemTipi)
                    {
                        Kasa_IslemTipi.SelectedIndex = i;
                        break;
                    }
                }

                Kasa_Tutar.Text = satir.Cells["Tutar"].Value.ToString();
                Kasa_Aciklama.Text = satir.Cells["Aciklama"].Value?.ToString() ?? "";

                // Kategori
                string kategori = satir.Cells["Kategori"].Value?.ToString() ?? "";
                Kasa_Kategori.Text = kategori;

                Kasa_IslemTarihi.Value = Convert.ToDateTime(satir.Cells["IslemTarihi"].Value);

                // Personel
                if (satir.Cells["PersonelId"].Value != DBNull.Value)
                {
                    Kasa_Personel.SelectedValue = satir.Cells["PersonelId"].Value;
                }

                Kasa_FisNo.Text = satir.Cells["FisNo"].Value?.ToString() ?? "";

                // Ödeme Yöntemi
                string odemeYontemi = satir.Cells["OdemeYontemi"].Value.ToString();
                for (int i = 0; i < Kasa_OdemeYontemi.Items.Count; i++)
                {
                    if (Kasa_OdemeYontemi.Items[i].ToString() == odemeYontemi)
                    {
                        Kasa_OdemeYontemi.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        // --- KAYIT GÜNCELLEME ---
        private void Kasa_KayitGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenKasaID == 0)
            {
                MessageBox.Show("Lütfen güncellemek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = Kasa_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitGuncelleForm guncelleForm = new KayitGuncelleForm("Kasa", "Kasa İşlemi", "KasaId", secilenKasaID, seciliSatir);
            if (guncelleForm.ShowDialog() == DialogResult.OK && guncelleForm.Kaydedildi)
            {
                Form1.KayitListele(Kasa_DataGrid, "Kasa");
                Kasa_VerileriTemizle_Click(sender, e);
            }
        }

private void Kasa_KayitSil_Click(object sender, EventArgs e)
        {
            if (secilenKasaID == 0)
            {
                MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = Kasa_DataGrid.CurrentRow;
            if (seciliSatir == null) return;
            KayitSilOnayForm onayForm = new KayitSilOnayForm("Kasa Kaydı", seciliSatir);
            if (onayForm.ShowDialog() == DialogResult.OK && onayForm.Onaylandi)
            {
                try
                {
                    Form1.BaglantiAc();
                    SqlCommand SilKomut = new SqlCommand("DELETE FROM Kasa WHERE KasaId=@pID", Form1.Baglanti);
                    SilKomut.Parameters.AddWithValue("@pID", secilenKasaID);
                    SilKomut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(Kasa_DataGrid, "Kasa");
                    Kasa_VerileriTemizle_Click(sender, e);
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

        // --- VERİLERİ TEMİZLE ---
        private void Kasa_VerileriTemizle_Click(object sender, EventArgs e)
        {
            Kasa_Tutar.Clear();
            Kasa_Aciklama.Clear();
            Kasa_FisNo.Clear();
            Kasa_IslemTipi.SelectedIndex = -1;
            Kasa_Kategori.SelectedIndex = -1;
            Kasa_OdemeYontemi.SelectedIndex = -1;
            Kasa_Personel.SelectedIndex = -1;
            Kasa_IslemTarihi.Value = DateTime.Now;

            Kasa_IslemTipi.BackColor = Color.White;
            Kasa_Tutar.BackColor = Color.White;
            Kasa_Aciklama.BackColor = Color.White;
            Kasa_Kategori.BackColor = Color.White;
            Kasa_Personel.BackColor = Color.White;
            Kasa_FisNo.BackColor = Color.White;
            Kasa_OdemeYontemi.BackColor = Color.White;

            secilenKasaID = 0;
            Kasa_DataGrid.ClearSelection();
        }

        // Tutar alanına sadece rakam ve virgül/nokta girişine izin ver
        private void Kasa_Tutar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
