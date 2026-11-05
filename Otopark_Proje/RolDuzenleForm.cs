using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Otopark_Proje
{
    public class RolDuzenleForm : Form
    {
        private ListBox rolListBox;
        private TextBox rolTextBox;
        private Button ekleButton;
        private Button guncelleButton;
        private Button silButton;
        private Button kapatButton;
        private Label baslikLabel;
        private Label textBoxLabel;
        private Label listBoxLabel;

        /// <summary>
        /// Düzenleme sonrası güncel rol listesi
        /// </summary>
        public List<string> GuncelRoller { get; private set; }

        public RolDuzenleForm(List<string> mevcutRoller)
        {
            GuncelRoller = new List<string>(mevcutRoller);
            FormTasarimOlustur();
            RolleriYukle();
        }

        private void FormTasarimOlustur()
        {
            // --- Form ayarları ---
            this.Text = "Rol Düzenleme";
            this.Size = new Size(420, 460);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.BackColor = Color.WhiteSmoke;

            // --- Başlık ---
            baslikLabel = new Label
            {
                Text = "ROL YÖNETİMİ",
                Font = new Font("Poppins", 14F, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(380, 40),
                Location = new Point(10, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(baslikLabel);

            // --- ListBox Label ---
            listBoxLabel = new Label
            {
                Text = "Mevcut Roller:",
                Font = new Font("Poppins", 9F, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(15, 55)
            };
            this.Controls.Add(listBoxLabel);

            // --- Rol Listesi (ListBox) ---
            rolListBox = new ListBox
            {
                Location = new Point(15, 80),
                Size = new Size(370, 160),
                Font = new Font("Poppins", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
            rolListBox.SelectedIndexChanged += RolListBox_SelectedIndexChanged;
            this.Controls.Add(rolListBox);

            // --- TextBox Label ---
            textBoxLabel = new Label
            {
                Text = "Rol Adı:",
                Font = new Font("Poppins", 9F, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(15, 250)
            };
            this.Controls.Add(textBoxLabel);

            // --- TextBox ---
            rolTextBox = new TextBox
            {
                Location = new Point(15, 275),
                Size = new Size(370, 28),
                Font = new Font("Poppins", 10F)
            };
            this.Controls.Add(rolTextBox);

            // --- Yeni Kayıt Ekle Butonu ---
            ekleButton = new Button
            {
                Text = "Yeni Kayıt Ekle",
                Location = new Point(15, 315),
                Size = new Size(120, 38),
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
                Location = new Point(145, 315),
                Size = new Size(110, 38),
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
                Location = new Point(265, 315),
                Size = new Size(120, 38),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Poppins", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            silButton.FlatAppearance.BorderSize = 0;
            silButton.Click += SilButton_Click;
            this.Controls.Add(silButton);

            // --- Kapat Butonu ---
            kapatButton = new Button
            {
                Text = "Kaydet ve Kapat",
                Location = new Point(15, 370),
                Size = new Size(370, 38),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Poppins", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            kapatButton.FlatAppearance.BorderSize = 0;
            kapatButton.Click += KapatButton_Click;
            this.Controls.Add(kapatButton);
        }

        private void RolleriYukle()
        {
            rolListBox.Items.Clear();
            foreach (string rol in GuncelRoller)
            {
                rolListBox.Items.Add(rol);
            }
        }

        // --- ListBox'tan seçim yapılınca TextBox'a yaz ---
        private void RolListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rolListBox.SelectedIndex >= 0)
            {
                rolTextBox.Text = rolListBox.SelectedItem.ToString();
            }
        }

        // --- Yeni Kayıt Ekle ---
        private void EkleButton_Click(object sender, EventArgs e)
        {
            string yeniRol = rolTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(yeniRol))
            {
                MessageBox.Show("Lütfen eklemek istediğiniz rol adını yazın!", "Boş Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rolTextBox.Focus();
                return;
            }

            // Aynı isimde rol var mı kontrol et (büyük-küçük harf duyarsız)
            bool mevcutMu = GuncelRoller.Any(r => r.Equals(yeniRol, StringComparison.OrdinalIgnoreCase));
            if (mevcutMu)
            {
                MessageBox.Show("Bu rol adı zaten mevcut!", "Tekrar Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GuncelRoller.Add(yeniRol);
            RolleriYukle();
            rolTextBox.Clear();
            rolTextBox.Focus();
            MessageBox.Show("Yeni rol başarıyla eklendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- Seçili Rolü Güncelle ---
        private void GuncelleButton_Click(object sender, EventArgs e)
        {
            if (rolListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz rolü listeden seçin!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string yeniMetin = rolTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(yeniMetin))
            {
                MessageBox.Show("Rol adı boş olamaz!", "Boş Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rolTextBox.Focus();
                return;
            }

            int secilenIndex = rolListBox.SelectedIndex;
            string eskiRol = GuncelRoller[secilenIndex];

            // Eğer aynı metin ise güncellemeye gerek yok
            if (eskiRol.Equals(yeniMetin, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Yeni rol adı mevcut rol adı ile aynı!", "Değişiklik Yok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Farklı bir kayıtta aynı isim var mı kontrol et
            bool mevcutMu = GuncelRoller
                .Where((r, i) => i != secilenIndex)
                .Any(r => r.Equals(yeniMetin, StringComparison.OrdinalIgnoreCase));

            if (mevcutMu)
            {
                MessageBox.Show("Bu rol adı zaten başka bir kayıtta mevcut!", "Tekrar Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GuncelRoller[secilenIndex] = yeniMetin;
            RolleriYukle();
            rolListBox.SelectedIndex = secilenIndex;
            MessageBox.Show($"'{eskiRol}' → '{yeniMetin}' olarak güncellendi.", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- Seçili Rolü Sil ---
        private void SilButton_Click(object sender, EventArgs e)
        {
            if (rolListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz rolü listeden seçin!", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string silinecekRol = rolListBox.SelectedItem.ToString();

            DialogResult sonuc = MessageBox.Show(
                $"'{silinecekRol}' rolünü silmek istediğinize emin misiniz?",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (sonuc == DialogResult.Yes)
            {
                GuncelRoller.RemoveAt(rolListBox.SelectedIndex);
                RolleriYukle();
                rolTextBox.Clear();
                MessageBox.Show("Rol başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- Kaydet ve Kapat ---
        private void KapatButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RolDuzenleForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "RolDuzenleForm";
            this.Load += new System.EventHandler(this.RolDuzenleForm_Load);
            this.ResumeLayout(false);

        }

        private void RolDuzenleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
