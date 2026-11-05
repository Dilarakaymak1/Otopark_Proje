
namespace Otopark_Proje
{
    partial class YikamaSecenekleri
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.YikamaSecenek_DataGrid = new System.Windows.Forms.DataGridView();
            this.YikamaSecenek_KayitGuncelle = new System.Windows.Forms.Button();
            this.YikamaSecenek_VerileriSil = new System.Windows.Forms.Button();
            this.YikamaSecenek_KayitEkle_Button = new System.Windows.Forms.Button();
            this.YikamaSecenek_KayitSil = new System.Windows.Forms.Button();
            this.YikamaSecenek_OdemeDurum = new System.Windows.Forms.ComboBox();
            this.YikamaSecenek_Esaat = new System.Windows.Forms.DateTimePicker();
            this.YikamaSecenek_Ssaat = new System.Windows.Forms.DateTimePicker();
            this.YikamaSecenek_Plaka = new System.Windows.Forms.MaskedTextBox();
            this.YikamaSecenek_Fiyat = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.YikamaSecenek_Personel = new System.Windows.Forms.ComboBox();
            this.YikamaSecenek_YikamaTipi = new System.Windows.Forms.ComboBox();
            this.YikamaSecenek_YikamaDurum = new System.Windows.Forms.ComboBox();
            this.YikamaSecenek_Tarih = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.YikamaSecenek_DataGrid)).BeginInit();
            this.YikamaSecenek_OgeleriDuzenle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // YikamaSecenek_OgeleriDuzenle
            // 
            this.YikamaSecenek_OgeleriDuzenle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.YikamaSecenek_OgeleriDuzenle.FlatAppearance.BorderSize = 0;
            this.YikamaSecenek_OgeleriDuzenle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.YikamaSecenek_OgeleriDuzenle.Font = new System.Drawing.Font("Poppins", 8F, System.Drawing.FontStyle.Bold);
            this.YikamaSecenek_OgeleriDuzenle.ForeColor = System.Drawing.Color.White;
            this.YikamaSecenek_OgeleriDuzenle.Location = new System.Drawing.Point(12, 322);
            this.YikamaSecenek_OgeleriDuzenle.Name = "YikamaSecenek_OgeleriDuzenle";
            this.YikamaSecenek_OgeleriDuzenle.Size = new System.Drawing.Size(312, 33);
            this.YikamaSecenek_OgeleriDuzenle.TabIndex = 40;
            this.YikamaSecenek_OgeleriDuzenle.Text = "Öğeleri Düzenle";
            this.YikamaSecenek_OgeleriDuzenle.UseVisualStyleBackColor = false;
            this.YikamaSecenek_OgeleriDuzenle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.YikamaSecenek_OgeleriDuzenle.Click += new System.EventHandler(this.YikamaSecenek_OgeleriDuzenle_Click);
            // 
            // YikamaSecenek_DataGrid
            // 
            this.YikamaSecenek_DataGrid.AllowUserToAddRows = false;
            this.YikamaSecenek_DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.YikamaSecenek_DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.YikamaSecenek_DataGrid.Location = new System.Drawing.Point(12, 360);
            this.YikamaSecenek_DataGrid.Name = "YikamaSecenek_DataGrid";
            this.YikamaSecenek_DataGrid.ReadOnly = true;
            this.YikamaSecenek_DataGrid.RowHeadersWidth = 51;
            this.YikamaSecenek_DataGrid.RowTemplate.Height = 24;
            this.YikamaSecenek_DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.YikamaSecenek_DataGrid.Size = new System.Drawing.Size(948, 223);
            this.YikamaSecenek_DataGrid.TabIndex = 39;
            this.YikamaSecenek_DataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.YikamaSecenek_DataGrid_CellClick);
            this.YikamaSecenek_DataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.YikamaSecenek_DataGrid_CellContentClick);
            // 
            // YikamaSecenek_KayitGuncelle
            // 
            this.YikamaSecenek_KayitGuncelle.Location = new System.Drawing.Point(754, 34);
            this.YikamaSecenek_KayitGuncelle.Name = "YikamaSecenek_KayitGuncelle";
            this.YikamaSecenek_KayitGuncelle.Size = new System.Drawing.Size(206, 55);
            this.YikamaSecenek_KayitGuncelle.TabIndex = 38;
            this.YikamaSecenek_KayitGuncelle.Text = "Seçili Kayıtı Güncelle";
            this.YikamaSecenek_KayitGuncelle.UseVisualStyleBackColor = true;
            this.YikamaSecenek_KayitGuncelle.Click += new System.EventHandler(this.YikamaSecenek_KayitGuncelle_Click);
            // 
            // YikamaSecenek_VerileriSil
            // 
            this.YikamaSecenek_VerileriSil.Location = new System.Drawing.Point(419, 207);
            this.YikamaSecenek_VerileriSil.Name = "YikamaSecenek_VerileriSil";
            this.YikamaSecenek_VerileriSil.Size = new System.Drawing.Size(312, 33);
            this.YikamaSecenek_VerileriSil.TabIndex = 37;
            this.YikamaSecenek_VerileriSil.Text = "Veri Alanlarını Temizle";
            this.YikamaSecenek_VerileriSil.UseVisualStyleBackColor = true;
            this.YikamaSecenek_VerileriSil.Click += new System.EventHandler(this.YikamaSecenek_VerileriSil_Click);
            // 
            // YikamaSecenek_KayitEkle_Button
            // 
            this.YikamaSecenek_KayitEkle_Button.Location = new System.Drawing.Point(754, 95);
            this.YikamaSecenek_KayitEkle_Button.Name = "YikamaSecenek_KayitEkle_Button";
            this.YikamaSecenek_KayitEkle_Button.Size = new System.Drawing.Size(206, 55);
            this.YikamaSecenek_KayitEkle_Button.TabIndex = 36;
            this.YikamaSecenek_KayitEkle_Button.Text = "Yeni Kayıt Ekle";
            this.YikamaSecenek_KayitEkle_Button.UseVisualStyleBackColor = true;
            this.YikamaSecenek_KayitEkle_Button.Click += new System.EventHandler(this.YikamaSecenek_KayitEkle_Button_Click);
            // 
            // YikamaSecenek_KayitSil
            // 
            this.YikamaSecenek_KayitSil.BackColor = System.Drawing.Color.IndianRed;
            this.YikamaSecenek_KayitSil.ForeColor = System.Drawing.Color.White;
            this.YikamaSecenek_KayitSil.Location = new System.Drawing.Point(754, 156);
            this.YikamaSecenek_KayitSil.Name = "YikamaSecenek_KayitSil";
            this.YikamaSecenek_KayitSil.Size = new System.Drawing.Size(206, 55);
            this.YikamaSecenek_KayitSil.TabIndex = 40;
            this.YikamaSecenek_KayitSil.Text = "Seçili Kaydı Sil";
            this.YikamaSecenek_KayitSil.UseVisualStyleBackColor = false;
            this.YikamaSecenek_KayitSil.Click += new System.EventHandler(this.YikamaSecenek_KayitSil_Click);
            // 
            // YikamaSecenek_OdemeDurum
            // 
            this.YikamaSecenek_OdemeDurum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.YikamaSecenek_OdemeDurum.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.YikamaSecenek_OdemeDurum.FormattingEnabled = true;
            this.YikamaSecenek_OdemeDurum.Items.AddRange(new object[] {
            "Evet",
            "Hayır"});
            this.YikamaSecenek_OdemeDurum.Location = new System.Drawing.Point(195, 216);
            this.YikamaSecenek_OdemeDurum.Name = "YikamaSecenek_OdemeDurum";
            this.YikamaSecenek_OdemeDurum.Size = new System.Drawing.Size(169, 24);
            this.YikamaSecenek_OdemeDurum.TabIndex = 35;
            this.YikamaSecenek_OdemeDurum.SelectedIndexChanged += new System.EventHandler(this.YikamaSecenek_OdemeDurum_SelectedIndexChanged);
            // 
            // YikamaSecenek_Esaat
            // 
            this.YikamaSecenek_Esaat.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.YikamaSecenek_Esaat.Location = new System.Drawing.Point(562, 169);
            this.YikamaSecenek_Esaat.Name = "YikamaSecenek_Esaat";
            this.YikamaSecenek_Esaat.Size = new System.Drawing.Size(169, 22);
            this.YikamaSecenek_Esaat.TabIndex = 34;
            this.YikamaSecenek_Esaat.Value = new System.DateTime(2026, 3, 12, 0, 0, 0, 0);
            this.YikamaSecenek_Esaat.ValueChanged += new System.EventHandler(this.YikamaSecenek_Esaat_ValueChanged);
            // 
            // YikamaSecenek_Ssaat
            // 
            this.YikamaSecenek_Ssaat.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.YikamaSecenek_Ssaat.Location = new System.Drawing.Point(195, 169);
            this.YikamaSecenek_Ssaat.Name = "YikamaSecenek_Ssaat";
            this.YikamaSecenek_Ssaat.Size = new System.Drawing.Size(169, 22);
            this.YikamaSecenek_Ssaat.TabIndex = 33;
            this.YikamaSecenek_Ssaat.ValueChanged += new System.EventHandler(this.YikamaSecenek_Ssaat_ValueChanged);
            // 
            // YikamaSecenek_Plaka
            // 
            this.YikamaSecenek_Plaka.Location = new System.Drawing.Point(195, 34);
            this.YikamaSecenek_Plaka.Mask = ">00 L?? 0099";
            this.YikamaSecenek_Plaka.Name = "YikamaSecenek_Plaka";
            this.YikamaSecenek_Plaka.Size = new System.Drawing.Size(169, 22);
            this.YikamaSecenek_Plaka.TabIndex = 30;
            this.YikamaSecenek_Plaka.TextChanged += new System.EventHandler(this.YikamaSecenek_Plaka_TextChanged);
            // 
            // YikamaSecenek_Fiyat
            // 
            this.YikamaSecenek_Fiyat.Location = new System.Drawing.Point(195, 73);
            this.YikamaSecenek_Fiyat.MaxLength = 11;
            this.YikamaSecenek_Fiyat.Name = "YikamaSecenek_Fiyat";
            this.YikamaSecenek_Fiyat.Size = new System.Drawing.Size(169, 22);
            this.YikamaSecenek_Fiyat.TabIndex = 32;
            this.YikamaSecenek_Fiyat.TextChanged += new System.EventHandler(this.YikamaSecenek_Fiyat_TextChanged);
            this.YikamaSecenek_Fiyat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.YikamaSecenek_Fiyat_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(440, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 25);
            this.label9.TabIndex = 19;
            this.label9.Text = "Bitiş Saati";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(33, 216);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 25);
            this.label10.TabIndex = 20;
            this.label10.Text = "Ödeme Alındı Mı?";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(40, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 25);
            this.label5.TabIndex = 21;
            this.label5.Text = "Baslangıç Saati";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(455, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 25);
            this.label8.TabIndex = 22;
            this.label8.Text = "Tarih";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(69, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 25);
            this.label4.TabIndex = 23;
            this.label4.Text = "Durum";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(414, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 25);
            this.label7.TabIndex = 24;
            this.label7.Text = "Yıkama Personeli";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(76, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 25);
            this.label3.TabIndex = 25;
            this.label3.Text = "Fiyat";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(434, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 25);
            this.label6.TabIndex = 26;
            this.label6.Text = "Yıkama Tipi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(74, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 25);
            this.label2.TabIndex = 27;
            this.label2.Text = "Plaka";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-79, -42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 58);
            this.label1.TabIndex = 18;
            this.label1.Text = "YENİ ABONE EKLE";
            // 
            // YikamaSecenek_Personel
            // 
            this.YikamaSecenek_Personel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.YikamaSecenek_Personel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.YikamaSecenek_Personel.FormattingEnabled = true;
            this.YikamaSecenek_Personel.Items.AddRange(new object[] {
            "Ali Yılmaz",
            "Veli Topal",
            "Ahmet Kaynarca"});
            this.YikamaSecenek_Personel.Location = new System.Drawing.Point(562, 73);
            this.YikamaSecenek_Personel.Name = "YikamaSecenek_Personel";
            this.YikamaSecenek_Personel.Size = new System.Drawing.Size(169, 24);
            this.YikamaSecenek_Personel.TabIndex = 35;
            this.YikamaSecenek_Personel.SelectedIndexChanged += new System.EventHandler(this.YikamaSecenek_Personel_SelectedIndexChanged);
            // 
            // YikamaSecenek_YikamaTipi
            // 
            this.YikamaSecenek_YikamaTipi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.YikamaSecenek_YikamaTipi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.YikamaSecenek_YikamaTipi.FormattingEnabled = true;
            this.YikamaSecenek_YikamaTipi.Items.AddRange(new object[] {
            "İç",
            "Dış",
            "İç-Dış"});
            this.YikamaSecenek_YikamaTipi.Location = new System.Drawing.Point(562, 33);
            this.YikamaSecenek_YikamaTipi.Name = "YikamaSecenek_YikamaTipi";
            this.YikamaSecenek_YikamaTipi.Size = new System.Drawing.Size(169, 24);
            this.YikamaSecenek_YikamaTipi.TabIndex = 35;
            this.YikamaSecenek_YikamaTipi.SelectedIndexChanged += new System.EventHandler(this.YikamaSecenek_YikamaTipi_SelectedIndexChanged);
            // 
            // YikamaSecenek_YikamaDurum
            // 
            this.YikamaSecenek_YikamaDurum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.YikamaSecenek_YikamaDurum.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.YikamaSecenek_YikamaDurum.FormattingEnabled = true;
            this.YikamaSecenek_YikamaDurum.Items.AddRange(new object[] {
            "Tamamlandı",
            "Devam Ediyor",
            "Bekliyor"});
            this.YikamaSecenek_YikamaDurum.Location = new System.Drawing.Point(195, 118);
            this.YikamaSecenek_YikamaDurum.Name = "YikamaSecenek_YikamaDurum";
            this.YikamaSecenek_YikamaDurum.Size = new System.Drawing.Size(169, 24);
            this.YikamaSecenek_YikamaDurum.TabIndex = 35;
            this.YikamaSecenek_YikamaDurum.SelectedIndexChanged += new System.EventHandler(this.YikamaSecenek_YikamaDurum_SelectedIndexChanged);
            // 
            // YikamaSecenek_Tarih
            // 
            this.YikamaSecenek_Tarih.Location = new System.Drawing.Point(562, 118);
            this.YikamaSecenek_Tarih.Name = "YikamaSecenek_Tarih";
            this.YikamaSecenek_Tarih.Size = new System.Drawing.Size(169, 22);
            this.YikamaSecenek_Tarih.TabIndex = 34;
            this.YikamaSecenek_Tarih.Value = new System.DateTime(2026, 3, 12, 0, 0, 0, 0);
            this.YikamaSecenek_Tarih.ValueChanged += new System.EventHandler(this.YikamaSecenek_Tarih_ValueChanged);
            // 
            // YikamaSecenekleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 600);
            this.Controls.Add(this.YikamaSecenek_OgeleriDuzenle);
            this.Controls.Add(this.YikamaSecenek_DataGrid);
            this.Controls.Add(this.YikamaSecenek_KayitGuncelle);
            this.Controls.Add(this.YikamaSecenek_KayitSil);
            this.Controls.Add(this.YikamaSecenek_VerileriSil);
            this.Controls.Add(this.YikamaSecenek_KayitEkle_Button);
            this.Controls.Add(this.YikamaSecenek_YikamaTipi);
            this.Controls.Add(this.YikamaSecenek_Personel);
            this.Controls.Add(this.YikamaSecenek_YikamaDurum);
            this.Controls.Add(this.YikamaSecenek_OdemeDurum);
            this.Controls.Add(this.YikamaSecenek_Tarih);
            this.Controls.Add(this.YikamaSecenek_Esaat);
            this.Controls.Add(this.YikamaSecenek_Ssaat);
            this.Controls.Add(this.YikamaSecenek_Plaka);
            this.Controls.Add(this.YikamaSecenek_Fiyat);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "YikamaSecenekleri";
            this.Text = "YikamaSecenekleri";
            this.Load += new System.EventHandler(this.YikamaSecenekleri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.YikamaSecenek_DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView YikamaSecenek_DataGrid;
        private System.Windows.Forms.Button YikamaSecenek_OgeleriDuzenle;
        private System.Windows.Forms.Button YikamaSecenek_KayitGuncelle;
        private System.Windows.Forms.Button YikamaSecenek_VerileriSil;
        private System.Windows.Forms.Button YikamaSecenek_KayitEkle_Button;
        private System.Windows.Forms.Button YikamaSecenek_KayitSil;
        private System.Windows.Forms.ComboBox YikamaSecenek_OdemeDurum;
        private System.Windows.Forms.DateTimePicker YikamaSecenek_Esaat;
        private System.Windows.Forms.DateTimePicker YikamaSecenek_Ssaat;
        private System.Windows.Forms.MaskedTextBox YikamaSecenek_Plaka;
        private System.Windows.Forms.TextBox YikamaSecenek_Fiyat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox YikamaSecenek_Personel;
        private System.Windows.Forms.ComboBox YikamaSecenek_YikamaTipi;
        private System.Windows.Forms.ComboBox YikamaSecenek_YikamaDurum;
        private System.Windows.Forms.DateTimePicker YikamaSecenek_Tarih;
    }
}