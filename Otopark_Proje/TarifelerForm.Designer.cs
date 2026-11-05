
namespace Otopark_Proje
{
    partial class TarifelerForm
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
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Tarife_TarifeAdi = new System.Windows.Forms.TextBox();
            this.Tarife_Ucret = new System.Windows.Forms.TextBox();
            this.Tarife_ParaBirimi = new System.Windows.Forms.TextBox();
            this.Tarife_Aciklama = new System.Windows.Forms.TextBox();
            this.Tarife_GecerlilikBaslangic = new System.Windows.Forms.DateTimePicker();
            this.Tarife_GecerlilikBitis = new System.Windows.Forms.DateTimePicker();
            this.Tarife_Olusturan = new System.Windows.Forms.ComboBox();
            this.Tarife_AktifMi = new System.Windows.Forms.ComboBox();
            this.Tarife_KayitEkle = new System.Windows.Forms.Button();
            this.Tarife_KayitSil = new System.Windows.Forms.Button();
            this.Tarife_KayitGuncelle = new System.Windows.Forms.Button();
            this.Tarife_VerileriTemizle = new System.Windows.Forms.Button();
            this.Tarife_DataGrid = new System.Windows.Forms.DataGridView();
            this.Tarife_OgeleriDuzenle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Tarife_DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "TARİFE AYARLARI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(44, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tarife Adı";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(78, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ücret";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(34, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Para Birimi";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(50, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Açıklama";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(393, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "Geçerlilik Başlangıç";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(421, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 25);
            this.label7.TabIndex = 6;
            this.label7.Text = "Geçerlilik Bitiş";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(439, 179);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 25);
            this.label8.TabIndex = 7;
            this.label8.Text = "Oluşturan";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(455, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 25);
            this.label9.TabIndex = 8;
            this.label9.Text = "Durum";
            // 
            // Tarife_TarifeAdi
            // 
            this.Tarife_TarifeAdi.Location = new System.Drawing.Point(195, 90);
            this.Tarife_TarifeAdi.Name = "Tarife_TarifeAdi";
            this.Tarife_TarifeAdi.Size = new System.Drawing.Size(169, 22);
            this.Tarife_TarifeAdi.TabIndex = 9;
            // 
            // Tarife_Ucret
            // 
            this.Tarife_Ucret.Location = new System.Drawing.Point(195, 136);
            this.Tarife_Ucret.Name = "Tarife_Ucret";
            this.Tarife_Ucret.Size = new System.Drawing.Size(169, 22);
            this.Tarife_Ucret.TabIndex = 10;
            this.Tarife_Ucret.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tarife_Ucret_KeyPress);
            // 
            // Tarife_ParaBirimi
            // 
            this.Tarife_ParaBirimi.Location = new System.Drawing.Point(195, 182);
            this.Tarife_ParaBirimi.Name = "Tarife_ParaBirimi";
            this.Tarife_ParaBirimi.Size = new System.Drawing.Size(169, 22);
            this.Tarife_ParaBirimi.TabIndex = 11;
            this.Tarife_ParaBirimi.Text = "TL";
            // 
            // Tarife_Aciklama
            // 
            this.Tarife_Aciklama.Location = new System.Drawing.Point(195, 228);
            this.Tarife_Aciklama.Name = "Tarife_Aciklama";
            this.Tarife_Aciklama.Size = new System.Drawing.Size(169, 22);
            this.Tarife_Aciklama.TabIndex = 12;
            // 
            // Tarife_GecerlilikBaslangic
            // 
            this.Tarife_GecerlilikBaslangic.Location = new System.Drawing.Point(562, 87);
            this.Tarife_GecerlilikBaslangic.Name = "Tarife_GecerlilikBaslangic";
            this.Tarife_GecerlilikBaslangic.Size = new System.Drawing.Size(169, 22);
            this.Tarife_GecerlilikBaslangic.TabIndex = 13;
            // 
            // Tarife_GecerlilikBitis
            // 
            this.Tarife_GecerlilikBitis.Location = new System.Drawing.Point(562, 133);
            this.Tarife_GecerlilikBitis.Name = "Tarife_GecerlilikBitis";
            this.Tarife_GecerlilikBitis.Size = new System.Drawing.Size(169, 22);
            this.Tarife_GecerlilikBitis.TabIndex = 14;
            // 
            // Tarife_Olusturan
            // 
            this.Tarife_Olusturan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tarife_Olusturan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Tarife_Olusturan.FormattingEnabled = true;
            this.Tarife_Olusturan.Location = new System.Drawing.Point(562, 179);
            this.Tarife_Olusturan.Name = "Tarife_Olusturan";
            this.Tarife_Olusturan.Size = new System.Drawing.Size(169, 24);
            this.Tarife_Olusturan.TabIndex = 15;
            // 
            // Tarife_AktifMi
            // 
            this.Tarife_AktifMi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tarife_AktifMi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Tarife_AktifMi.FormattingEnabled = true;
            this.Tarife_AktifMi.Items.AddRange(new object[] {
            "Aktif",
            "Pasif"});
            this.Tarife_AktifMi.Location = new System.Drawing.Point(562, 225);
            this.Tarife_AktifMi.Name = "Tarife_AktifMi";
            this.Tarife_AktifMi.Size = new System.Drawing.Size(169, 24);
            this.Tarife_AktifMi.TabIndex = 16;
            // 
            // Tarife_KayitEkle
            // 
            this.Tarife_KayitEkle.Location = new System.Drawing.Point(754, 87);
            this.Tarife_KayitEkle.Name = "Tarife_KayitEkle";
            this.Tarife_KayitEkle.Size = new System.Drawing.Size(206, 55);
            this.Tarife_KayitEkle.TabIndex = 17;
            this.Tarife_KayitEkle.Text = "Yeni Kayıt Ekle";
            this.Tarife_KayitEkle.UseVisualStyleBackColor = true;
            this.Tarife_KayitEkle.Click += new System.EventHandler(this.Tarife_KayitEkle_Click);
            // 
            // Tarife_KayitGuncelle
            // 
            this.Tarife_KayitGuncelle.Location = new System.Drawing.Point(754, 148);
            this.Tarife_KayitGuncelle.Name = "Tarife_KayitGuncelle";
            this.Tarife_KayitGuncelle.Size = new System.Drawing.Size(206, 55);
            this.Tarife_KayitGuncelle.TabIndex = 18;
            this.Tarife_KayitGuncelle.Text = "Seçili Kayıtı Güncelle";
            this.Tarife_KayitGuncelle.UseVisualStyleBackColor = true;
            this.Tarife_KayitGuncelle.Click += new System.EventHandler(this.Tarife_KayitGuncelle_Click);
            // 
            // Tarife_KayitSil
            // 
            this.Tarife_KayitSil.BackColor = System.Drawing.Color.IndianRed;
            this.Tarife_KayitSil.ForeColor = System.Drawing.Color.White;
            this.Tarife_KayitSil.Location = new System.Drawing.Point(754, 209);
            this.Tarife_KayitSil.Name = "Tarife_KayitSil";
            this.Tarife_KayitSil.Size = new System.Drawing.Size(206, 55);
            this.Tarife_KayitSil.TabIndex = 23;
            this.Tarife_KayitSil.Text = "Seçili Kaydı Sil";
            this.Tarife_KayitSil.UseVisualStyleBackColor = false;
            this.Tarife_KayitSil.Click += new System.EventHandler(this.Tarife_KayitSil_Click);
            // 
            // Tarife_VerileriTemizle
            // 
            this.Tarife_VerileriTemizle.Location = new System.Drawing.Point(429, 259);
            this.Tarife_VerileriTemizle.Name = "Tarife_VerileriTemizle";
            this.Tarife_VerileriTemizle.Size = new System.Drawing.Size(312, 33);
            this.Tarife_VerileriTemizle.TabIndex = 19;
            this.Tarife_VerileriTemizle.Text = "Veri Alanlarını Temizle";
            this.Tarife_VerileriTemizle.UseVisualStyleBackColor = true;
            this.Tarife_VerileriTemizle.Click += new System.EventHandler(this.Tarife_VerileriTemizle_Click);
            // 
            // Tarife_OgeleriDuzenle
            // 
            this.Tarife_OgeleriDuzenle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.Tarife_OgeleriDuzenle.FlatAppearance.BorderSize = 0;
            this.Tarife_OgeleriDuzenle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Tarife_OgeleriDuzenle.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold);
            this.Tarife_OgeleriDuzenle.ForeColor = System.Drawing.Color.White;
            this.Tarife_OgeleriDuzenle.Location = new System.Drawing.Point(12, 362);
            this.Tarife_OgeleriDuzenle.Name = "Tarife_OgeleriDuzenle";
            this.Tarife_OgeleriDuzenle.Size = new System.Drawing.Size(312, 33);
            this.Tarife_OgeleriDuzenle.TabIndex = 25;
            this.Tarife_OgeleriDuzenle.Text = "Öğeleri Düzenle";
            this.Tarife_OgeleriDuzenle.UseVisualStyleBackColor = false;
            this.Tarife_OgeleriDuzenle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Tarife_OgeleriDuzenle.Click += new System.EventHandler(this.Tarife_OgeleriDuzenle_Click);
            // 
            // Tarife_DataGrid
            // 
            this.Tarife_DataGrid.AllowUserToAddRows = false;
            this.Tarife_DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tarife_DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tarife_DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Tarife_DataGrid.ReadOnly = true;
            this.Tarife_DataGrid.Location = new System.Drawing.Point(12, 400);
            this.Tarife_DataGrid.Name = "Tarife_DataGrid";
            this.Tarife_DataGrid.RowHeadersWidth = 51;
            this.Tarife_DataGrid.RowTemplate.Height = 24;
            this.Tarife_DataGrid.Size = new System.Drawing.Size(948, 223);
            this.Tarife_DataGrid.TabIndex = 20;
            this.Tarife_DataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tarife_DataGrid_CellClick);
            // 
            // TarifelerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 640);
            this.Controls.Add(this.Tarife_OgeleriDuzenle);
            this.Controls.Add(this.Tarife_DataGrid);
            this.Controls.Add(this.Tarife_VerileriTemizle);
            this.Controls.Add(this.Tarife_KayitSil);
            this.Controls.Add(this.Tarife_KayitGuncelle);
            this.Controls.Add(this.Tarife_KayitEkle);
            this.Controls.Add(this.Tarife_AktifMi);
            this.Controls.Add(this.Tarife_Olusturan);
            this.Controls.Add(this.Tarife_GecerlilikBitis);
            this.Controls.Add(this.Tarife_GecerlilikBaslangic);
            this.Controls.Add(this.Tarife_Aciklama);
            this.Controls.Add(this.Tarife_ParaBirimi);
            this.Controls.Add(this.Tarife_Ucret);
            this.Controls.Add(this.Tarife_TarifeAdi);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TarifelerForm";
            this.Text = "TarifelerForm";
            this.Load += new System.EventHandler(this.TarifelerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tarife_DataGrid)).EndInit();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Tarife_TarifeAdi;
        private System.Windows.Forms.TextBox Tarife_Ucret;
        private System.Windows.Forms.TextBox Tarife_ParaBirimi;
        private System.Windows.Forms.TextBox Tarife_Aciklama;
        private System.Windows.Forms.DateTimePicker Tarife_GecerlilikBaslangic;
        private System.Windows.Forms.DateTimePicker Tarife_GecerlilikBitis;
        private System.Windows.Forms.ComboBox Tarife_Olusturan;
        private System.Windows.Forms.ComboBox Tarife_AktifMi;
        private System.Windows.Forms.Button Tarife_KayitEkle;
        private System.Windows.Forms.Button Tarife_KayitGuncelle;
        private System.Windows.Forms.Button Tarife_VerileriTemizle;
        private System.Windows.Forms.Button Tarife_KayitSil;
        private System.Windows.Forms.DataGridView Tarife_DataGrid;
        private System.Windows.Forms.Button Tarife_OgeleriDuzenle;
    }
}
