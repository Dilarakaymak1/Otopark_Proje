
namespace vtUygulama
{
    partial class KayitGuncelleForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPerID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblYildiz2 = new System.Windows.Forms.Label();
            this.lblYildiz1 = new System.Windows.Forms.Label();
            this.cmbBirim = new System.Windows.Forms.ComboBox();
            this.txtTel = new System.Windows.Forms.MaskedTextBox();
            this.dtDtar = new System.Windows.Forms.DateTimePicker();
            this.cmbDyer = new System.Windows.Forms.ComboBox();
            this.radK = new System.Windows.Forms.RadioButton();
            this.radE = new System.Windows.Forms.RadioButton();
            this.txtSoyad = new System.Windows.Forms.TextBox();
            this.txtAd = new System.Windows.Forms.TextBox();
            this.txtTcno = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtPerID);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblYildiz2);
            this.panel1.Controls.Add(this.lblYildiz1);
            this.panel1.Controls.Add(this.cmbBirim);
            this.panel1.Controls.Add(this.txtTel);
            this.panel1.Controls.Add(this.dtDtar);
            this.panel1.Controls.Add(this.cmbDyer);
            this.panel1.Controls.Add(this.radK);
            this.panel1.Controls.Add(this.radE);
            this.panel1.Controls.Add(this.txtSoyad);
            this.panel1.Controls.Add(this.txtAd);
            this.panel1.Controls.Add(this.txtTcno);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(24, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 456);
            this.panel1.TabIndex = 10;
            // 
            // txtPerID
            // 
            this.txtPerID.Enabled = false;
            this.txtPerID.Location = new System.Drawing.Point(172, 20);
            this.txtPerID.Name = "txtPerID";
            this.txtPerID.Size = new System.Drawing.Size(100, 26);
            this.txtPerID.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 20);
            this.label9.TabIndex = 10;
            this.label9.Text = "PerID";
            // 
            // lblYildiz2
            // 
            this.lblYildiz2.AutoSize = true;
            this.lblYildiz2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblYildiz2.ForeColor = System.Drawing.Color.Red;
            this.lblYildiz2.Location = new System.Drawing.Point(316, 306);
            this.lblYildiz2.Name = "lblYildiz2";
            this.lblYildiz2.Size = new System.Drawing.Size(21, 26);
            this.lblYildiz2.TabIndex = 9;
            this.lblYildiz2.Text = "*";
            this.lblYildiz2.Visible = false;
            // 
            // lblYildiz1
            // 
            this.lblYildiz1.AutoSize = true;
            this.lblYildiz1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblYildiz1.ForeColor = System.Drawing.Color.Red;
            this.lblYildiz1.Location = new System.Drawing.Point(316, 208);
            this.lblYildiz1.Name = "lblYildiz1";
            this.lblYildiz1.Size = new System.Drawing.Size(21, 26);
            this.lblYildiz1.TabIndex = 9;
            this.lblYildiz1.Text = "*";
            this.lblYildiz1.Visible = false;
            // 
            // cmbBirim
            // 
            this.cmbBirim.FormattingEnabled = true;
            this.cmbBirim.Items.AddRange(new object[] {
            "Arşiv",
            "Bilgi İşlem",
            "İnsan Kaynakları",
            "Muhasebe",
            "Öğrenci İşleri"});
            this.cmbBirim.Location = new System.Drawing.Point(172, 399);
            this.cmbBirim.Name = "cmbBirim";
            this.cmbBirim.Size = new System.Drawing.Size(157, 28);
            this.cmbBirim.TabIndex = 8;
            this.cmbBirim.Text = "Birim Seçiniz";
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(172, 351);
            this.txtTel.Mask = "(9999) 000-0000";
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(155, 26);
            this.txtTel.TabIndex = 7;
            // 
            // dtDtar
            // 
            this.dtDtar.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDtar.Location = new System.Drawing.Point(172, 303);
            this.dtDtar.Name = "dtDtar";
            this.dtDtar.Size = new System.Drawing.Size(138, 26);
            this.dtDtar.TabIndex = 6;
            // 
            // cmbDyer
            // 
            this.cmbDyer.FormattingEnabled = true;
            this.cmbDyer.Items.AddRange(new object[] {
            "Ankara",
            "Bartın",
            "Bolu",
            "İstanbul",
            "İzmir",
            "Kastamonu",
            "Konya",
            "Zonguldak"});
            this.cmbDyer.Location = new System.Drawing.Point(172, 253);
            this.cmbDyer.Name = "cmbDyer";
            this.cmbDyer.Size = new System.Drawing.Size(138, 28);
            this.cmbDyer.TabIndex = 5;
            this.cmbDyer.Text = "Şehir Seçiniz";
            // 
            // radK
            // 
            this.radK.AutoSize = true;
            this.radK.Location = new System.Drawing.Point(226, 208);
            this.radK.Name = "radK";
            this.radK.Size = new System.Drawing.Size(37, 24);
            this.radK.TabIndex = 4;
            this.radK.TabStop = true;
            this.radK.Text = "K";
            this.radK.UseVisualStyleBackColor = true;
            // 
            // radE
            // 
            this.radE.AutoSize = true;
            this.radE.Location = new System.Drawing.Point(172, 207);
            this.radE.Name = "radE";
            this.radE.Size = new System.Drawing.Size(38, 24);
            this.radE.TabIndex = 3;
            this.radE.TabStop = true;
            this.radE.Text = "E";
            this.radE.UseVisualStyleBackColor = true;
            // 
            // txtSoyad
            // 
            this.txtSoyad.Location = new System.Drawing.Point(172, 159);
            this.txtSoyad.Name = "txtSoyad";
            this.txtSoyad.Size = new System.Drawing.Size(138, 26);
            this.txtSoyad.TabIndex = 2;
            // 
            // txtAd
            // 
            this.txtAd.Location = new System.Drawing.Point(172, 111);
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(138, 26);
            this.txtAd.TabIndex = 2;
            // 
            // txtTcno
            // 
            this.txtTcno.Location = new System.Drawing.Point(172, 63);
            this.txtTcno.Mask = "00000000000";
            this.txtTcno.Name = "txtTcno";
            this.txtTcno.Size = new System.Drawing.Size(115, 26);
            this.txtTcno.TabIndex = 1;
            this.txtTcno.ValidatingType = typeof(int);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 402);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Birim";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 354);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Telefon";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Cinsiyet";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 306);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Doğum Tarihi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Soyad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 258);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Doğum Yeri";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tc No";
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Location = new System.Drawing.Point(24, 498);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(376, 51);
            this.btnGuncelle.TabIndex = 11;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = true;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // KayitGuncelleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 569);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "KayitGuncelleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kayıt Güncelle Formu";
            this.Load += new System.EventHandler(this.KayitGuncelleForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblYildiz2;
        private System.Windows.Forms.Label lblYildiz1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnGuncelle;
        public System.Windows.Forms.ComboBox cmbBirim;
        public System.Windows.Forms.MaskedTextBox txtTel;
        public System.Windows.Forms.DateTimePicker dtDtar;
        public System.Windows.Forms.ComboBox cmbDyer;
        public System.Windows.Forms.RadioButton radK;
        public System.Windows.Forms.RadioButton radE;
        public System.Windows.Forms.TextBox txtSoyad;
        public System.Windows.Forms.TextBox txtAd;
        public System.Windows.Forms.MaskedTextBox txtTcno;
        public System.Windows.Forms.TextBox txtPerID;
    }
}