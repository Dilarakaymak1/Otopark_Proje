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
    public partial class TelefonRehberiForm : Form
    {
        int secilenKisiID = 0;

        public TelefonRehberiForm()
        {
            InitializeComponent();
        }

        private void TelefonRehberiForm_Load(object sender, EventArgs e)
        {
            Form1.KayitListele(Rehber_DataGrid, "TelefonRehberi");
        }

        // --- YENİ KAYIT EKLEME ---
        private void Rehber_KayitEkle_Click(object sender, EventArgs e)
        {
            bool bosAlanVarMi = false;

            Rehber_Ad.BackColor = Color.White;
            Rehber_Soyad.BackColor = Color.White;
            Rehber_KurumAdi.BackColor = Color.White;
            Rehber_Telefon1.BackColor = Color.White;
            Rehber_Telefon2.BackColor = Color.White;
            Rehber_Eposta.BackColor = Color.White;
            Rehber_Adres.BackColor = Color.White;
            Rehber_Aciklama.BackColor = Color.White;

            if (string.IsNullOrWhiteSpace(Rehber_Ad.Text)) { Rehber_Ad.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Rehber_Soyad.Text)) { Rehber_Soyad.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Rehber_KurumAdi.Text)) { Rehber_KurumAdi.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (!Rehber_Telefon1.MaskCompleted) { Rehber_Telefon1.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (!Rehber_Telefon2.MaskCompleted) { Rehber_Telefon2.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Rehber_Eposta.Text)) { Rehber_Eposta.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Rehber_Adres.Text)) { Rehber_Adres.BackColor = Color.LightPink; bosAlanVarMi = true; }
            if (string.IsNullOrWhiteSpace(Rehber_Aciklama.Text)) { Rehber_Aciklama.BackColor = Color.LightPink; bosAlanVarMi = true; }

            if (bosAlanVarMi)
            {
                MessageBox.Show("Lütfen en az Telefon 1 alanını doldurun!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Form1.BaglantiAc();

                SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM TelefonRehberi WHERE Telefon1=@pT", Form1.Baglanti);
                kontrolKomut.Parameters.AddWithValue("@pT", Rehber_Telefon1.Text);
                int kayitSayisi = (int)kontrolKomut.ExecuteScalar();
                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu telefon numarasıyla kayıtlı bir kişi zaten mevcut!", "Kayıt Mevcut", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sorgu = "INSERT INTO TelefonRehberi (Ad, Soyad, KurumAdi, Telefon1, Telefon2, Eposta, Adres, Aciklama) " +
                               "VALUES (@pAd, @pSoyad, @pKurumAdi, @pTelefon1, @pTelefon2, @pEposta, @pAdres, @pAciklama)";

                SqlCommand EkleKomut = new SqlCommand(sorgu, Form1.Baglanti);

                EkleKomut.Parameters.AddWithValue("@pAd", string.IsNullOrWhiteSpace(Rehber_Ad.Text) ? (object)DBNull.Value : Rehber_Ad.Text);
                EkleKomut.Parameters.AddWithValue("@pSoyad", string.IsNullOrWhiteSpace(Rehber_Soyad.Text) ? (object)DBNull.Value : Rehber_Soyad.Text);
                EkleKomut.Parameters.AddWithValue("@pKurumAdi", string.IsNullOrWhiteSpace(Rehber_KurumAdi.Text) ? (object)DBNull.Value : Rehber_KurumAdi.Text);
                EkleKomut.Parameters.AddWithValue("@pTelefon1", Rehber_Telefon1.Text);
                EkleKomut.Parameters.AddWithValue("@pTelefon2", string.IsNullOrWhiteSpace(Rehber_Telefon2.Text) || !Rehber_Telefon2.MaskCompleted ? (object)DBNull.Value : Rehber_Telefon2.Text);
                EkleKomut.Parameters.AddWithValue("@pEposta", string.IsNullOrWhiteSpace(Rehber_Eposta.Text) ? (object)DBNull.Value : Rehber_Eposta.Text);
                EkleKomut.Parameters.AddWithValue("@pAdres", string.IsNullOrWhiteSpace(Rehber_Adres.Text) ? (object)DBNull.Value : Rehber_Adres.Text);
                EkleKomut.Parameters.AddWithValue("@pAciklama", string.IsNullOrWhiteSpace(Rehber_Aciklama.Text) ? (object)DBNull.Value : Rehber_Aciklama.Text);

                if (EkleKomut.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Rehber kaydı başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(Rehber_DataGrid, "TelefonRehberi");
                    Rehber_VerileriTemizle_Click(sender, e);
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
        private void Rehber_DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = Rehber_DataGrid.Rows[e.RowIndex];

                secilenKisiID = Convert.ToInt32(satir.Cells["KisiId"].Value);

                Rehber_Ad.Text = satir.Cells["Ad"].Value?.ToString() ?? "";
                Rehber_Soyad.Text = satir.Cells["Soyad"].Value?.ToString() ?? "";
                Rehber_KurumAdi.Text = satir.Cells["KurumAdi"].Value?.ToString() ?? "";
                Rehber_Telefon1.Text = satir.Cells["Telefon1"].Value?.ToString() ?? "";
                Rehber_Telefon2.Text = satir.Cells["Telefon2"].Value?.ToString() ?? "";
                Rehber_Eposta.Text = satir.Cells["Eposta"].Value?.ToString() ?? "";
                Rehber_Adres.Text = satir.Cells["Adres"].Value?.ToString() ?? "";
                Rehber_Aciklama.Text = satir.Cells["Aciklama"].Value?.ToString() ?? "";
            }
        }

        // --- KAYIT GÜNCELLEME ---
        private void Rehber_KayitGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenKisiID == 0)
            {
                MessageBox.Show("Lütfen güncellemek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = Rehber_DataGrid.CurrentRow;
            if (seciliSatir == null) return;

            KayitGuncelleForm guncelleForm = new KayitGuncelleForm("TelefonRehberi", "Rehber Kaydı", "KisiId", secilenKisiID, seciliSatir);
            if (guncelleForm.ShowDialog() == DialogResult.OK && guncelleForm.Kaydedildi)
            {
                Form1.KayitListele(Rehber_DataGrid, "TelefonRehberi");
                Rehber_VerileriTemizle_Click(sender, e);
            }
        }

private void Rehber_KayitSil_Click(object sender, EventArgs e)
        {
            if (secilenKisiID == 0)
            {
                MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow seciliSatir = Rehber_DataGrid.CurrentRow;
            if (seciliSatir == null) return;
            KayitSilOnayForm onayForm = new KayitSilOnayForm("Telefon Rehberi Kaydı", seciliSatir);
            if (onayForm.ShowDialog() == DialogResult.OK && onayForm.Onaylandi)
            {
                try
                {
                    Form1.BaglantiAc();
                    SqlCommand SilKomut = new SqlCommand("DELETE FROM TelefonRehberi WHERE KisiId=@pID", Form1.Baglanti);
                    SilKomut.Parameters.AddWithValue("@pID", secilenKisiID);
                    SilKomut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(Rehber_DataGrid, "TelefonRehberi");
                    Rehber_VerileriTemizle_Click(sender, e);
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
        private void Rehber_VerileriTemizle_Click(object sender, EventArgs e)
        {
            Rehber_Ad.Clear();
            Rehber_Soyad.Clear();
            Rehber_KurumAdi.Clear();
            Rehber_Telefon1.Clear();
            Rehber_Telefon2.Clear();
            Rehber_Eposta.Clear();
            Rehber_Adres.Clear();
            Rehber_Aciklama.Clear();

            Rehber_Ad.BackColor = Color.White;
            Rehber_Soyad.BackColor = Color.White;
            Rehber_KurumAdi.BackColor = Color.White;
            Rehber_Telefon1.BackColor = Color.White;
            Rehber_Telefon2.BackColor = Color.White;
            Rehber_Eposta.BackColor = Color.White;
            Rehber_Adres.BackColor = Color.White;
            Rehber_Aciklama.BackColor = Color.White;

            secilenKisiID = 0;
            Rehber_DataGrid.ClearSelection();
        }
    }
}
