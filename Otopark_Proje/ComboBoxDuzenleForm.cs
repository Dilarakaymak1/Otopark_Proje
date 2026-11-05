using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Otopark_Proje
{
    /// <summary>
    /// Evrensel ComboBox öğe düzenleme formu.
    /// Bir formda bulunan birden fazla ComboBox'ın öğelerini
    /// tek bir popup üzerinden yönetmeye olanak tanır.
    /// </summary>
    public class ComboBoxDuzenleForm : Form
    {
        private ComboBox comboBoxSecici;
        private ListBox ogelerListBox;
        private TextBox duzenlemeTextBox;
        private Button ekleButton;
        private Button guncelleButton;
        private Button silButton;
        private Button kapatButton;
        private Label baslikLabel;
        private Label seciciLabel;
        private Label listeLabel;
        private Label textBoxLabel;

        // ComboBox adı → referansı eşlemesi
        private Dictionary<string, ComboBox> comboBoxlar;

        // Geçici düzenleme listesi (seçilen ComboBox'ın öğeleri)
        private List<string> aktifOgeler = new List<string>();

        // Değişiklik yapıldığını takip et
        private Dictionary<string, List<string>> guncelVeriler = new Dictionary<string, List<string>>();

        /// <summary>
        /// Düzenleme sonrası güncellenmiş ComboBox verileri (ComboBox adı → yeni öğe listesi)
        /// </summary>
        public Dictionary<string, List<string>> GuncelVeriler => guncelVeriler;

        /// <summary>
        /// Evrensel ComboBox düzenleme formu.
        /// </summary>
        /// <param name="formBasligi">Açılan popup'ın başlık metni</param>
        /// <param name="comboBoxListesi">ComboBox görünen adı → ComboBox referansı</param>
        public ComboBoxDuzenleForm(string formBasligi, Dictionary<string, ComboBox> comboBoxListesi)
        {
            this.comboBoxlar = comboBoxListesi;

            // İlk yüklemede mevcut verileri snapshot olarak al
            foreach (var kvp in comboBoxlar)
            {
                List<string> ogeler = new List<string>();
                foreach (var item in kvp.Value.Items)
                {
                    ogeler.Add(item.ToString());
                }
                guncelVeriler[kvp.Key] = ogeler;
            }

            FormTasarimOlustur(formBasligi);
            ComboBoxSeciciDoldur();
        }

        private void FormTasarimOlustur(string formBasligi)
        {
            // --- Form ayarları ---
            this.Text = formBasligi;
            this.Size = new Size(450, 540);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.BackColor = Color.WhiteSmoke;

            // --- Başlık ---
            baslikLabel = new Label
            {
                Text = "COMBOBOX ÖĞE DÜZENLEYİCİ",
                Font = new Font("Poppins", 12F, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(410, 35),
                Location = new Point(15, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(baslikLabel);

            // --- ComboBox Seçici Label ---
            seciciLabel = new Label
            {
                Text = "Düzenlenecek Alan:",
                Font = new Font("Poppins", 9F, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(15, 50)
            };
            this.Controls.Add(seciciLabel);

            // --- ComboBox Seçici ---
            comboBoxSecici = new ComboBox
            {
                Location = new Point(15, 75),
                Size = new Size(400, 28),
                Font = new Font("Poppins", 9.5F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Popup
            };
            comboBoxSecici.SelectedIndexChanged += ComboBoxSecici_SelectedIndexChanged;
            this.Controls.Add(comboBoxSecici);

            // --- Liste Label ---
            listeLabel = new Label
            {
                Text = "Mevcut Öğeler:",
                Font = new Font("Poppins", 9F, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(15, 110)
            };
            this.Controls.Add(listeLabel);

            // --- Öğeler ListBox ---
            ogelerListBox = new ListBox
            {
                Location = new Point(15, 135),
                Size = new Size(400, 160),
                Font = new Font("Poppins", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
            ogelerListBox.SelectedIndexChanged += OgelerListBox_SelectedIndexChanged;
            this.Controls.Add(ogelerListBox);

            // --- TextBox Label ---
            textBoxLabel = new Label
            {
                Text = "Öğe Adı:",
                Font = new Font("Poppins", 9F, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(15, 305)
            };
            this.Controls.Add(textBoxLabel);

            // --- Düzenleme TextBox ---
            duzenlemeTextBox = new TextBox
            {
                Location = new Point(15, 330),
                Size = new Size(400, 28),
                Font = new Font("Poppins", 10F)
            };
            this.Controls.Add(duzenlemeTextBox);

            // --- Yeni Kayıt Ekle Butonu ---
            ekleButton = new Button
            {
                Text = "Yeni Kayıt Ekle",
                Location = new Point(15, 370),
                Size = new Size(128, 38),
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Poppins", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            ekleButton.FlatAppearance.BorderSize = 0;
            ekleButton.Click += EkleButton_Click;
            this.Controls.Add(ekleButton);

            // --- Güncelle Butonu ---
            guncelleButton = new Button
            {
                Text = "Güncelle",
                Location = new Point(152, 370),
                Size = new Size(118, 38),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Poppins", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            guncelleButton.FlatAppearance.BorderSize = 0;
            guncelleButton.Click += GuncelleButton_Click;
            this.Controls.Add(guncelleButton);

            // --- Sil Butonu ---
            silButton = new Button
            {
                Text = "Seçili Sil",
                Location = new Point(279, 370),
                Size = new Size(136, 38),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Poppins", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            silButton.FlatAppearance.BorderSize = 0;
            silButton.Click += SilButton_Click;
            this.Controls.Add(silButton);

            // --- Kaydet ve Kapat Butonu ---
            kapatButton = new Button
            {
                Text = "Kaydet ve Kapat",
                Location = new Point(15, 425),
                Size = new Size(400, 42),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Poppins", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            kapatButton.FlatAppearance.BorderSize = 0;
            kapatButton.Click += KapatButton_Click;
            this.Controls.Add(kapatButton);
        }

        private void ComboBoxSeciciDoldur()
        {
            comboBoxSecici.Items.Clear();
            foreach (var kvp in comboBoxlar)
            {
                comboBoxSecici.Items.Add(kvp.Key);
            }

            if (comboBoxSecici.Items.Count > 0)
                comboBoxSecici.SelectedIndex = 0;
        }

        // --- ComboBox seçici değişince listeyi güncelle ---
        private void ComboBoxSecici_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSecici.SelectedIndex < 0) return;

            string secilenAd = comboBoxSecici.SelectedItem.ToString();
            aktifOgeler = guncelVeriler[secilenAd];
            OgeleriYukle();
            duzenlemeTextBox.Clear();
        }

        private void OgeleriYukle()
        {
            ogelerListBox.Items.Clear();
            foreach (string oge in aktifOgeler)
            {
                ogelerListBox.Items.Add(oge);
            }
        }

        // --- Listeden seçim yapılınca TextBox'a yaz ---
        private void OgelerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ogelerListBox.SelectedIndex >= 0)
            {
                duzenlemeTextBox.Text = ogelerListBox.SelectedItem.ToString();
            }
        }

        // --- Yeni Kayıt Ekle ---
        private void EkleButton_Click(object sender, EventArgs e)
        {
            if (comboBoxSecici.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen önce düzenlenecek alanı seçin!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string yeniOge = duzenlemeTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(yeniOge))
            {
                MessageBox.Show("Lütfen eklemek istediğiniz öğe adını yazın!", "Boş Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                duzenlemeTextBox.Focus();
                return;
            }

            bool mevcutMu = aktifOgeler.Any(o => o.Equals(yeniOge, StringComparison.OrdinalIgnoreCase));
            if (mevcutMu)
            {
                MessageBox.Show("Bu öğe zaten mevcut!", "Tekrar Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            aktifOgeler.Add(yeniOge);
            OgeleriYukle();
            duzenlemeTextBox.Clear();
            duzenlemeTextBox.Focus();
            MessageBox.Show("Yeni öğe başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- Seçili Öğeyi Güncelle ---
        private void GuncelleButton_Click(object sender, EventArgs e)
        {
            if (comboBoxSecici.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen önce düzenlenecek alanı seçin!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ogelerListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz öğeyi listeden seçin!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string yeniMetin = duzenlemeTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(yeniMetin))
            {
                MessageBox.Show("Öğe adı boş olamaz!", "Boş Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                duzenlemeTextBox.Focus();
                return;
            }

            int secilenIndex = ogelerListBox.SelectedIndex;
            string eskiOge = aktifOgeler[secilenIndex];

            if (eskiOge.Equals(yeniMetin, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Yeni öğe adı mevcut öğe adı ile aynı!", "Değişiklik Yok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool mevcutMu = aktifOgeler
                .Where((o, i) => i != secilenIndex)
                .Any(o => o.Equals(yeniMetin, StringComparison.OrdinalIgnoreCase));

            if (mevcutMu)
            {
                MessageBox.Show("Bu öğe adı zaten başka bir kayıtta mevcut!", "Tekrar Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            aktifOgeler[secilenIndex] = yeniMetin;
            OgeleriYukle();
            ogelerListBox.SelectedIndex = secilenIndex;
            MessageBox.Show($"'{eskiOge}' → '{yeniMetin}' olarak güncellendi.", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- Seçili Öğeyi Sil ---
        private void SilButton_Click(object sender, EventArgs e)
        {
            if (comboBoxSecici.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen önce düzenlenecek alanı seçin!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ogelerListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz öğeyi listeden seçin!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string silinecekOge = ogelerListBox.SelectedItem.ToString();

            DialogResult sonuc = MessageBox.Show(
                $"'{silinecekOge}' öğesini silmek istediğinize emin misiniz?",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (sonuc == DialogResult.Yes)
            {
                aktifOgeler.RemoveAt(ogelerListBox.SelectedIndex);
                OgeleriYukle();
                duzenlemeTextBox.Clear();
                MessageBox.Show("Öğe başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- Kaydet ve Kapat ---
        private void KapatButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
