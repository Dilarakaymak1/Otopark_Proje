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
    public partial class TarifelerForm : Form
    {
        int secilenTarifeID = 0;

        public TarifelerForm()
        {
            InitializeComponent();
        }

        private void TarifelerForm_Load(object sender, EventArgs e)
        {
            // Oluşturan kullanıcı combobox'ını doldur
            try
            {
                Form1.BaglantiAc();
                SqlCommand cmd = new SqlCommand("SELECT KullaniciId, KullaniciAdi FROM Kullanicilar WHERE AktifMi=1", Form1.Baglanti);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                Tarife_Olusturan.DisplayMember = "KullaniciAdi";
                Tarife_Olusturan.ValueMember = "KullaniciId";
                Tarife_Olusturan.DataSource = dt;
                Tarife_Olusturan.SelectedIndex = -1;
            }
            catch (Exception Hata)
            {
                // Tablo henüz boşsa veya yoksa hata vermesini engelle
            }
            finally
            {
                if (Form1.Baglanti.State == ConnectionState.Open)
                    Form1.Baglanti.Close();
            }

            Form1.KayitListele(Tarife_DataGrid, "Tarifeler");
        }

        // --- COMBOBOX ÖĞELERİ DÜZENLEME ---
        private void Tarife_OgeleriDuzenle_Click(object sender, EventArgs e)
        {
            var comboBoxListesi = new Dictionary<string, ComboBox>
            {
                { "Durum (Aktif/Pasif)", Tarife_AktifMi }
            };
            ComboBoxDuzenleHelper.PopupAc("Tarifeler - ComboBox Düzenle", comboBoxListesi);
        }

        // --- YENİ KAYIT EKLEME ---
        private void Tarife_KayitEkle_Click(object sender, EventArgs e)
        {
            bool bosAlanVarMi = false;

            Tarife_TarifeAdi.BackColor = Color.White;
            Tarife_Ucret.BackColor = Color.White;
            Tarife_ParaBirimi.BackColor = Color.White;
            Tarife_Aciklama.BackColor = Color.White;
            Tarife_Olusturan.BackColor = Color.White;
            Tarife_AktifMi.BackColor = Color.White;

            if (string.IsNullOrWhiteSpace(Tarife_TarifeAdi.Text)) { Tarife_TarifeAdi.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Tarife_Ucret.Text)) { Tarife_Ucret.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Tarife_ParaBirimi.Text)) { Tarife_ParaBirimi.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Tarife_Aciklama.Text)) { Tarife_Aciklama.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (Tarife_Olusturan.SelectedIndex == -1) { Tarife_Olusturan.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Tarife_AktifMi.Text)) { Tarife_AktifMi.BackColor = Color.LightPink; bosAlanVarMi = true; }

            if (bosAlanVarMi)
            {
                MessageBox.Show("Lütfen kırmızı ile işaretlenmiş zorunlu alanları doldurun!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Form1.BaglantiAc();

                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM Tarifeler WHERE TarifeAdi=@pT", Form1.Baglanti);
                kontrolKomut.Parameters.AddWithValue("@pT", Tarife_TarifeAdi.Text);
                int kayitSayisi = (int)kontrolKomut.ExecuteScalar();
                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu tarife adı zaten mevcut!", "Kayıt Mevcut", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sorgu = "INSERT INTO Tarifeler (TarifeAdi, Ucret, ParaBirimi, GecerlilikBaslangic, GecerlilikBitis, Aciklama, OlusturanId, AktifMi) " +
                               "VALUES (@pTarifeAdi, @pUcret, @pParaBirimi, @pGecerlilikBaslangic, @pGecerlilikBitis, @pAciklama, @pOlusturanId, @pAktifMi)";

                SqlCommand EkleKomut = new SqlCommand(sorgu, Form1.Baglanti);

                EkleKomut.Parameters.AddWithValue("@pTarifeAdi", Tarife_TarifeAdi.Text);

                decimal ucret = 0;
                decimal.TryParse(Tarife_Ucret.Text, out ucret);
                EkleKomut.Parameters.AddWithValue("@pUcret", ucret);

                EkleKomut.Parameters.AddWithValue("@pParaBirimi", string.IsNullOrWhiteSpace(Tarife_ParaBirimi.Text) ? "TL" : Tarife_ParaBirimi.Text);
                EkleKomut.Parameters.AddWithValue("@pGecerlilikBaslangic", Tarife_GecerlilikBaslangic.Value);
                EkleKomut.Parameters.AddWithValue("@pGecerlilikBitis", Tarife_GecerlilikBitis.Value);
                EkleKomut.Parameters.AddWithValue("@pAciklama", string.IsNullOrWhiteSpace(Tarife_Aciklama.Text) ? (object)DBNull.Value : Tarife_Aciklama.Text);

                if (Tarife_Olusturan.SelectedValue != null)
                    EkleKomut.Parameters.AddWithValue("@pOlusturanId", Tarife_Olusturan.SelectedValue);
                else
                    EkleKomut.Parameters.AddWithValue("@pOlusturanId", DBNull.Value);

                bool aktifMi = (Tarife_AktifMi.Text == "Aktif");
                EkleKomut.Parameters.AddWithValue("@pAktifMi", aktifMi);

                if (EkleKomut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Tarife kaydı başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(Tarife_DataGrid, "Tarifeler");
                    Tarife_VerileriTemizle_Click(sender, e);
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
        private void Tarife_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = Tarife_DataGrid.Rows[e.RowIndex];

                secilenTarifeID = Convert.ToInt32(satir.Cells["TarifeId"].Value);

                Tarife_TarifeAdi.Text = satir.Cells["TarifeAdi"].Value.ToString();
                Tarife_Ucret.Text = satir.Cells["Ucret"].Value.ToString();
                Tarife_ParaBirimi.Text = satir.Cells["ParaBirimi"].Value.ToString();
                Tarife_GecerlilikBaslangic.Value = Convert.ToDateTime(satir.Cells["GecerlilikBaslangic"].Value);

                if (satir.Cells["GecerlilikBitis"].Value != DBNull.Value)
                    Tarife_GecerlilikBitis.Value = Convert.ToDateTime(satir.Cells["GecerlilikBitis"].Value);

                Tarife_Aciklama.Text = satir.Cells["Aciklama"].Value?.ToString() ?? "";

                if (satir.Cells["OlusturanId"].Value != DBNull.Value)
                    Tarife_Olusturan.SelectedValue = satir.Cells["OlusturanId"].Value;

                bool aktifMi = Convert.ToBoolean(satir.Cells["AktifMi"].Value);
                Tarife_AktifMi.Text = aktifMi ? "Aktif" : "Pasif";
            }
        }

        // --- KAYIT GÜNCELLEME ---
        private void Tarife_KayitGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenTarifeID == 0)
            {
                MessageBox.Show("Lütfen güncellemek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = Tarife_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitGuncelleForm guncelleForm = new KayitGuncelleForm("Tarifeler", "Tarife", "TarifeId", secilenTarifeID, seciliSatir);
            if (guncelleForm.ShowDialog() == DialogResult.OK && guncelleForm.Kaydedildi)
            {
                Form1.KayitListele(Tarife_DataGrid, "Tarifeler");
                Tarife_VerileriTemizle_Click(sender, e);
            }
        }

private void Tarife_KayitSil_Click(object sender, EventArgs e)
        {
            if (secilenTarifeID == 0)
            {
                MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = Tarife_DataGrid.CurrentRow;
            if (seciliSatir == null) return;
            KayitSilOnayForm onayForm = new KayitSilOnayForm("Tarife Kaydı", seciliSatir);
            if (onayForm.ShowDialog() == DialogResult.OK && onayForm.Onaylandi)
            {
                try
                {
                    Form1.BaglantiAc();
                    SqlCommand SilKomut = new SqlCommand("DELETE FROM Tarifeler WHERE TarifeId=@pID", Form1.Baglanti);
                    SilKomut.Parameters.AddWithValue("@pID", secilenTarifeID);
                    SilKomut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(Tarife_DataGrid, "Tarifeler");
                    Tarife_VerileriTemizle_Click(sender, e);
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
        private void Tarife_VerileriTemizle_Click(object sender, EventArgs e)
        {
            Tarife_TarifeAdi.Clear();
            Tarife_Ucret.Clear();
            Tarife_Aciklama.Clear();
            Tarife_ParaBirimi.Text = "TL";
            Tarife_AktifMi.SelectedIndex = -1;
            Tarife_Olusturan.SelectedIndex = -1;
            Tarife_GecerlilikBaslangic.Value = DateTime.Now;
            Tarife_GecerlilikBitis.Value = DateTime.Now;

            Tarife_TarifeAdi.BackColor = Color.White;
            Tarife_Ucret.BackColor = Color.White;
            Tarife_ParaBirimi.BackColor = Color.White;
            Tarife_Aciklama.BackColor = Color.White;
            Tarife_Olusturan.BackColor = Color.White;
            Tarife_AktifMi.BackColor = Color.White;

            secilenTarifeID = 0;
            Tarife_DataGrid.ClearSelection();
        }

        // Ücret alanına sadece rakam ve virgül/nokta girişine izin ver
        private void Tarife_Ucret_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
