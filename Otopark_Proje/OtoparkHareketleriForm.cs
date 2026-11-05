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
    public partial class OtoparkHareketleriForm : Form
    {
        int secilenIslemID = 0;

        public OtoparkHareketleriForm()
        {
            InitializeComponent();
        }

        private void OtoparkHareketleriForm_Load(object sender, EventArgs e)
        {
            // Personel combobox'ını veritabanından doldur
            try
            {
                Form1.BaglantiAc();
                SqlCommand cmd = new SqlCommand("SELECT PersonelId, Ad + ' ' + Soyad AS AdSoyad FROM Personeller WHERE Durum=N'Aktif'", Form1.Baglanti);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                OtoparkHareket_Personel.DisplayMember = "AdSoyad";
                OtoparkHareket_Personel.ValueMember = "PersonelId";
                OtoparkHareket_Personel.DataSource = dt;
                OtoparkHareket_Personel.SelectedIndex = -1;
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

            Form1.KayitListele(OtoparkHareket_DataGrid, "OtoparkHareketleri");
        }

        // --- COMBOBOX ÖĞELERİ DÜZENLEME ---
        private void OtoparkHareket_OgeleriDuzenle_Click(object sender, EventArgs e)
        {
            var comboBoxListesi = new Dictionary<string, ComboBox>
            {
                { "Abone Mi", OtoparkHareket_AboneMi },
                { "Durum", OtoparkHareket_Durum }
            };
            ComboBoxDuzenleHelper.PopupAc("Otopark Hareketleri - ComboBox Düzenle", comboBoxListesi);
        }

        // --- YENİ KAYIT EKLEME ---
        private void OtoparkHareket_KayitEkle_Click(object sender, EventArgs e)
        {
            bool bosAlanVarMi = false;

            OtoparkHareket_Plaka.BackColor = Color.White;
            OtoparkHareket_Ucret.BackColor = Color.White;
            OtoparkHareket_ParkYeriNo.BackColor = Color.White;
            OtoparkHareket_AboneMi.BackColor = Color.White;
            OtoparkHareket_Durum.BackColor = Color.White;
            OtoparkHareket_Personel.BackColor = Color.White;

            if (!OtoparkHareket_Plaka.MaskCompleted) { OtoparkHareket_Plaka.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(OtoparkHareket_Ucret.Text)) { OtoparkHareket_Ucret.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(OtoparkHareket_ParkYeriNo.Text)) { OtoparkHareket_ParkYeriNo.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(OtoparkHareket_AboneMi.Text)) { OtoparkHareket_AboneMi.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(OtoparkHareket_Durum.Text)) { OtoparkHareket_Durum.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (OtoparkHareket_Personel.SelectedIndex == -1) { OtoparkHareket_Personel.BackColor = Color.LightPink; bosAlanVarMi = true; }

            if (bosAlanVarMi)
            {
                MessageBox.Show("Lütfen kırmızı ile işaretlenmiş zorunlu alanları doldurun!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Form1.BaglantiAc();

                // Aynı plaka ile içeride kayıtlı araç var mı kontrol et
                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM OtoparkHareketleri WHERE Plaka=@pP AND Durum=N'İçeride'", Form1.Baglanti);
                kontrolKomut.Parameters.AddWithValue("@pP", OtoparkHareket_Plaka.Text);
                int kayitSayisi = (int)kontrolKomut.ExecuteScalar();
                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu plakalı araç zaten otoparkta kayıtlı!", "Kayıt Mevcut", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sorgu = "INSERT INTO OtoparkHareketleri (Plaka, GirisSaati, CikisSaati, Ucret, AboneMi, ParkYeriNo, Durum, IslemYapanPersonelId, IslemTarihi) " +
                               "VALUES (@pPlaka, @pGirisSaati, @pCikisSaati, @pUcret, @pAboneMi, @pParkYeriNo, @pDurum, @pPersonelId, @pIslemTarihi)";

                SqlCommand EkleKomut = new SqlCommand(sorgu, Form1.Baglanti);

                EkleKomut.Parameters.AddWithValue("@pPlaka", OtoparkHareket_Plaka.Text);
                EkleKomut.Parameters.AddWithValue("@pGirisSaati", OtoparkHareket_GirisSaati.Value);
                EkleKomut.Parameters.AddWithValue("@pCikisSaati", OtoparkHareket_CikisSaati.Value);

                decimal ucret = 0;
                decimal.TryParse(OtoparkHareket_Ucret.Text, out ucret);
                EkleKomut.Parameters.AddWithValue("@pUcret", ucret);

                bool aboneMi = (OtoparkHareket_AboneMi.Text == "Evet");
                EkleKomut.Parameters.AddWithValue("@pAboneMi", aboneMi);
                EkleKomut.Parameters.AddWithValue("@pParkYeriNo", string.IsNullOrWhiteSpace(OtoparkHareket_ParkYeriNo.Text) ? (object)DBNull.Value : OtoparkHareket_ParkYeriNo.Text);
                EkleKomut.Parameters.AddWithValue("@pDurum", OtoparkHareket_Durum.Text);

                if (OtoparkHareket_Personel.SelectedValue != null)
                    EkleKomut.Parameters.AddWithValue("@pPersonelId", OtoparkHareket_Personel.SelectedValue);
                else
                    EkleKomut.Parameters.AddWithValue("@pPersonelId", DBNull.Value);

                EkleKomut.Parameters.AddWithValue("@pIslemTarihi", OtoparkHareket_IslemTarihi.Value);

                if (EkleKomut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(OtoparkHareket_DataGrid, "OtoparkHareketleri");
                    OtoparkHareket_VerileriTemizle_Click(sender, e);
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
        private void OtoparkHareket_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = OtoparkHareket_DataGrid.Rows[e.RowIndex];

                secilenIslemID = Convert.ToInt32(satir.Cells["IslemId"].Value);

                OtoparkHareket_Plaka.Text = satir.Cells["Plaka"].Value.ToString();
                OtoparkHareket_GirisSaati.Value = Convert.ToDateTime(satir.Cells["GirisSaati"].Value);

                if (satir.Cells["CikisSaati"].Value != DBNull.Value)
                    OtoparkHareket_CikisSaati.Value = Convert.ToDateTime(satir.Cells["CikisSaati"].Value);

                OtoparkHareket_Ucret.Text = satir.Cells["Ucret"].Value.ToString();

                bool aboneMi = Convert.ToBoolean(satir.Cells["AboneMi"].Value);
                OtoparkHareket_AboneMi.Text = aboneMi ? "Evet" : "Hayır";

                OtoparkHareket_ParkYeriNo.Text = satir.Cells["ParkYeriNo"].Value?.ToString() ?? "";

                string durum = satir.Cells["Durum"].Value.ToString();
                for (int i = 0; i < OtoparkHareket_Durum.Items.Count; i++)
                {
                    if (OtoparkHareket_Durum.Items[i].ToString() == durum)
                    {
                        OtoparkHareket_Durum.SelectedIndex = i;
                        break;
                    }
                }

                // Personel seçimi
                if (satir.Cells["IslemYapanPersonelId"].Value != DBNull.Value)
                {
                    OtoparkHareket_Personel.SelectedValue = satir.Cells["IslemYapanPersonelId"].Value;
                }

                OtoparkHareket_IslemTarihi.Value = Convert.ToDateTime(satir.Cells["IslemTarihi"].Value);
            }
        }

        // --- KAYIT GÜNCELLEME ---
        private void OtoparkHareket_KayitGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenIslemID == 0)
            {
                MessageBox.Show("Lütfen güncellemek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = OtoparkHareket_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitGuncelleForm guncelleForm = new KayitGuncelleForm("OtoparkHareketleri", "Otopark Hareketi", "IslemId", secilenIslemID, seciliSatir);
            if (guncelleForm.ShowDialog() == DialogResult.OK && guncelleForm.Kaydedildi)
            {
                Form1.KayitListele(OtoparkHareket_DataGrid, "OtoparkHareketleri");
                OtoparkHareket_VerileriTemizle_Click(sender, e);
            }
        }

        // --- KAYIT SİLME ---
        private void OtoparkHareket_KayitSil_Click(object sender, EventArgs e)
        {
            if (secilenIslemID == 0)
            {
                MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow seciliSatir = OtoparkHareket_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitSilOnayForm onayForm = new KayitSilOnayForm("Otopark Hareketi", seciliSatir);
            if (onayForm.ShowDialog() == DialogResult.OK && onayForm.Onaylandi)
            {
                try
                {
                    Form1.BaglantiAc();
                    SqlCommand SilKomut = new SqlCommand("DELETE FROM OtoparkHareketleri WHERE IslemId=@pID", Form1.Baglanti);
                    SilKomut.Parameters.AddWithValue("@pID", secilenIslemID);
                    SilKomut.ExecuteNonQuery();

                    MessageBox.Show("Kayıt başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(OtoparkHareket_DataGrid, "OtoparkHareketleri");
                    OtoparkHareket_VerileriTemizle_Click(sender, e);
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
        private void OtoparkHareket_VerileriTemizle_Click(object sender, EventArgs e)
        {
            bool alanlarBos = string.IsNullOrWhiteSpace(OtoparkHareket_Ucret.Text) && secilenIslemID == 0;
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
            OtoparkHareket_Plaka.Text = "";
            OtoparkHareket_Ucret.Clear();
            OtoparkHareket_ParkYeriNo.Clear();
            OtoparkHareket_AboneMi.SelectedIndex = -1;
            OtoparkHareket_Durum.SelectedIndex = -1;
            OtoparkHareket_Personel.SelectedIndex = -1;
            OtoparkHareket_GirisSaati.Value = DateTime.Now;
            OtoparkHareket_CikisSaati.Value = DateTime.Now;
            OtoparkHareket_IslemTarihi.Value = DateTime.Now;

            OtoparkHareket_Plaka.BackColor = Color.White;
            OtoparkHareket_Ucret.BackColor = Color.White;
            OtoparkHareket_ParkYeriNo.BackColor = Color.White;
            OtoparkHareket_AboneMi.BackColor = Color.White;
            OtoparkHareket_Durum.BackColor = Color.White;
            OtoparkHareket_Personel.BackColor = Color.White;

            secilenIslemID = 0;
            OtoparkHareket_DataGrid.ClearSelection();
        }

        // Ücret alanına sadece rakam ve virgül/nokta girişine izin ver
        private void OtoparkHareket_Ucret_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
