
namespace Otopark_Proje
{
    partial class KullanicilarForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Kullanici_Personel = new System.Windows.Forms.ComboBox();
            this.Kullanici_KullaniciAdi = new System.Windows.Forms.TextBox();
            this.Kullanici_Sifre = new System.Windows.Forms.TextBox();
            this.Kullanici_Rol = new System.Windows.Forms.ComboBox();
            this.Kullanici_AktifMi = new System.Windows.Forms.ComboBox();
            this.Kullanici_Aciklama = new System.Windows.Forms.TextBox();
            this.Kullanici_KayitEkle = new System.Windows.Forms.Button();
            this.Kullanici_KayitSil = new System.Windows.Forms.Button();
            this.Kullanici_KayitGuncelle = new System.Windows.Forms.Button();
            this.Kullanici_VerileriTemizle = new System.Windows.Forms.Button();
            this.Kullanici_DataGrid = new System.Windows.Forms.DataGridView();
            this.rol_ekle_but = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Kullanici_DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "KULLANICI YÖNETİMİ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(53, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Personel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(22, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Kullanıcı Adı";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(72, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Şifre";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(468, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Rol";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(455, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "Durum";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(443, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 25);
            this.label7.TabIndex = 6;
            this.label7.Text = "Açıklama";
            // 
            // Kullanici_Personel
            // 
            this.Kullanici_Personel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Kullanici_Personel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Kullanici_Personel.FormattingEnabled = true;
            this.Kullanici_Personel.Location = new System.Drawing.Point(195, 87);
            this.Kullanici_Personel.Name = "Kullanici_Personel";
            this.Kullanici_Personel.Size = new System.Drawing.Size(169, 24);
            this.Kullanici_Personel.TabIndex = 7;
            this.Kullanici_Personel.SelectedIndexChanged += new System.EventHandler(this.Kullanici_Personel_SelectedIndexChanged);
            // 
            // Kullanici_KullaniciAdi
            // 
            this.Kullanici_KullaniciAdi.Location = new System.Drawing.Point(195, 136);
            this.Kullanici_KullaniciAdi.Name = "Kullanici_KullaniciAdi";
            this.Kullanici_KullaniciAdi.Size = new System.Drawing.Size(169, 22);
            this.Kullanici_KullaniciAdi.TabIndex = 8;
            // 
            // Kullanici_Sifre
            // 
            this.Kullanici_Sifre.Location = new System.Drawing.Point(195, 182);
            this.Kullanici_Sifre.Name = "Kullanici_Sifre";
            this.Kullanici_Sifre.PasswordChar = '*';
            this.Kullanici_Sifre.Size = new System.Drawing.Size(169, 22);
            this.Kullanici_Sifre.TabIndex = 9;
            // 
            // Kullanici_Rol
            // 
            this.Kullanici_Rol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Kullanici_Rol.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Kullanici_Rol.FormattingEnabled = true;
            this.Kullanici_Rol.Items.AddRange(new object[] {
            "Admin",
            "Kasiyer",
            "Gözetmen"});
            this.Kullanici_Rol.Location = new System.Drawing.Point(562, 87);
            this.Kullanici_Rol.Name = "Kullanici_Rol";
            this.Kullanici_Rol.Size = new System.Drawing.Size(169, 24);
            this.Kullanici_Rol.TabIndex = 10;
            // 
            // Kullanici_AktifMi
            // 
            this.Kullanici_AktifMi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Kullanici_AktifMi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Kullanici_AktifMi.FormattingEnabled = true;
            this.Kullanici_AktifMi.Items.AddRange(new object[] {
            "Aktif",
            "Pasif"});
            this.Kullanici_AktifMi.Location = new System.Drawing.Point(562, 133);
            this.Kullanici_AktifMi.Name = "Kullanici_AktifMi";
            this.Kullanici_AktifMi.Size = new System.Drawing.Size(169, 24);
            this.Kullanici_AktifMi.TabIndex = 11;
            // 
            // Kullanici_Aciklama
            // 
            this.Kullanici_Aciklama.Location = new System.Drawing.Point(562, 182);
            this.Kullanici_Aciklama.Name = "Kullanici_Aciklama";
            this.Kullanici_Aciklama.Size = new System.Drawing.Size(169, 22);
            this.Kullanici_Aciklama.TabIndex = 12;
            // 
            // Kullanici_KayitEkle
            // 
            this.Kullanici_KayitEkle.Location = new System.Drawing.Point(754, 87);
            this.Kullanici_KayitEkle.Name = "Kullanici_KayitEkle";
            this.Kullanici_KayitEkle.Size = new System.Drawing.Size(206, 55);
            this.Kullanici_KayitEkle.TabIndex = 13;
            this.Kullanici_KayitEkle.Text = "Yeni Kayıt Ekle";
            this.Kullanici_KayitEkle.UseVisualStyleBackColor = true;
            this.Kullanici_KayitEkle.Click += new System.EventHandler(this.Kullanici_KayitEkle_Click);
            // 
            // Kullanici_KayitSil
            // 
            this.Kullanici_KayitSil.BackColor = System.Drawing.Color.IndianRed;
            this.Kullanici_KayitSil.ForeColor = System.Drawing.Color.White;
            this.Kullanici_KayitSil.Location = new System.Drawing.Point(754, 209);
            this.Kullanici_KayitSil.Name = "Kullanici_KayitSil";
            this.Kullanici_KayitSil.Size = new System.Drawing.Size(206, 55);
            this.Kullanici_KayitSil.TabIndex = 23;
            this.Kullanici_KayitSil.Text = "Seçili Kaydı Sil";
            this.Kullanici_KayitSil.UseVisualStyleBackColor = false;
            this.Kullanici_KayitSil.Click += new System.EventHandler(this.Kullanici_KayitSil_Click);
            // 
            // Kullanici_KayitGuncelle
            // 
            this.Kullanici_KayitGuncelle.Location = new System.Drawing.Point(754, 148);
            this.Kullanici_KayitGuncelle.Name = "Kullanici_KayitGuncelle";
            this.Kullanici_KayitGuncelle.Size = new System.Drawing.Size(206, 55);
            this.Kullanici_KayitGuncelle.TabIndex = 14;
            this.Kullanici_KayitGuncelle.Text = "Seçili Kayıtı Güncelle";
            this.Kullanici_KayitGuncelle.UseVisualStyleBackColor = true;
            this.Kullanici_KayitGuncelle.Click += new System.EventHandler(this.Kullanici_KayitGuncelle_Click);
            // 
            // Kullanici_VerileriTemizle
            // 
            this.Kullanici_VerileriTemizle.Location = new System.Drawing.Point(429, 218);
            this.Kullanici_VerileriTemizle.Name = "Kullanici_VerileriTemizle";
            this.Kullanici_VerileriTemizle.Size = new System.Drawing.Size(312, 33);
            this.Kullanici_VerileriTemizle.TabIndex = 15;
            this.Kullanici_VerileriTemizle.Text = "Veri Alanlarını Temizle";
            this.Kullanici_VerileriTemizle.UseVisualStyleBackColor = true;
            this.Kullanici_VerileriTemizle.Click += new System.EventHandler(this.Kullanici_VerileriTemizle_Click);
            // 
            // Kullanici_DataGrid
            // 
            this.Kullanici_DataGrid.AllowUserToAddRows = false;
            this.Kullanici_DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Kullanici_DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Kullanici_DataGrid.Location = new System.Drawing.Point(12, 360);
            this.Kullanici_DataGrid.Name = "Kullanici_DataGrid";
            this.Kullanici_DataGrid.ReadOnly = true;
            this.Kullanici_DataGrid.RowHeadersWidth = 51;
            this.Kullanici_DataGrid.RowTemplate.Height = 24;
            this.Kullanici_DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Kullanici_DataGrid.Size = new System.Drawing.Size(948, 223);
            this.Kullanici_DataGrid.TabIndex = 16;
            this.Kullanici_DataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Kullanici_DataGrid_CellClick);
            // 
            // rol_ekle_but
            // 
            this.rol_ekle_but.Location = new System.Drawing.Point(22, 322);
            this.rol_ekle_but.Name = "rol_ekle_but";
            this.rol_ekle_but.Size = new System.Drawing.Size(312, 33);
            this.rol_ekle_but.TabIndex = 24;
            this.rol_ekle_but.Text = "Rol Kaydı Ekle";
            this.rol_ekle_but.UseVisualStyleBackColor = true;
            this.rol_ekle_but.Click += new System.EventHandler(this.rol_ekle_but_Click);
            // 
            // KullanicilarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 600);
            this.Controls.Add(this.rol_ekle_but);
            this.Controls.Add(this.Kullanici_DataGrid);
            this.Controls.Add(this.Kullanici_VerileriTemizle);
            this.Controls.Add(this.Kullanici_KayitSil);
            this.Controls.Add(this.Kullanici_KayitGuncelle);
            this.Controls.Add(this.Kullanici_KayitEkle);
            this.Controls.Add(this.Kullanici_Aciklama);
            this.Controls.Add(this.Kullanici_AktifMi);
            this.Controls.Add(this.Kullanici_Rol);
            this.Controls.Add(this.Kullanici_Sifre);
            this.Controls.Add(this.Kullanici_KullaniciAdi);
            this.Controls.Add(this.Kullanici_Personel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "KullanicilarForm";
            this.Text = "KullanicilarForm";
            this.Load += new System.EventHandler(this.KullanicilarForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Kullanici_DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Kullanici_Personel;
        private System.Windows.Forms.TextBox Kullanici_KullaniciAdi;
        private System.Windows.Forms.TextBox Kullanici_Sifre;
        private System.Windows.Forms.ComboBox Kullanici_Rol;
        private System.Windows.Forms.ComboBox Kullanici_AktifMi;
        private System.Windows.Forms.TextBox Kullanici_Aciklama;
        private System.Windows.Forms.Button Kullanici_KayitEkle;
        private System.Windows.Forms.Button Kullanici_KayitGuncelle;
        private System.Windows.Forms.Button Kullanici_VerileriTemizle;
        private System.Windows.Forms.Button Kullanici_KayitSil;
        private System.Windows.Forms.DataGridView Kullanici_DataGrid;
        private System.Windows.Forms.Button rol_ekle_but;
    }
}
