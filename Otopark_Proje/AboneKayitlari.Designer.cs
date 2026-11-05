
namespace Otopark_Proje
{
    partial class AboneKayitlari
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
            this.label1 = new System.Windows.Forms.Label();
            this.AboneKayitlari_DataGrid = new System.Windows.Forms.DataGridView();
            this.AboneKayitlari_KayitSil = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AboneKayitlari_DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 58);
            this.label1.TabIndex = 3;
            this.label1.Text = "ABONELER";
            // 
            // AboneKayitlari_DataGrid
            // 
            this.AboneKayitlari_DataGrid.AllowUserToAddRows = false;
            this.AboneKayitlari_DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AboneKayitlari_DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AboneKayitlari_DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AboneKayitlari_DataGrid.ReadOnly = true;
            this.AboneKayitlari_DataGrid.Location = new System.Drawing.Point(22, 70);
            this.AboneKayitlari_DataGrid.Name = "AboneKayitlari_DataGrid";
            this.AboneKayitlari_DataGrid.RowHeadersWidth = 51;
            this.AboneKayitlari_DataGrid.RowTemplate.Height = 24;
            this.AboneKayitlari_DataGrid.Size = new System.Drawing.Size(948, 294);
            this.AboneKayitlari_DataGrid.TabIndex = 4;
            // 
            this.AboneKayitlari_KayitSil.BackColor = System.Drawing.Color.IndianRed;
            this.AboneKayitlari_KayitSil.ForeColor = System.Drawing.Color.White;
            this.AboneKayitlari_KayitSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AboneKayitlari_KayitSil.Location = new System.Drawing.Point(22, 386);
            this.AboneKayitlari_KayitSil.Name = "AboneKayitlari_KayitSil";
            this.AboneKayitlari_KayitSil.Size = new System.Drawing.Size(948, 52);
            this.AboneKayitlari_KayitSil.TabIndex = 5;
            this.AboneKayitlari_KayitSil.Text = "Seçili Kaydı Sil";
            this.AboneKayitlari_KayitSil.UseVisualStyleBackColor = false;
            this.AboneKayitlari_KayitSil.Click += new System.EventHandler(this.AboneKayitlari_KayitSil_Click);
            // 
            // AboneKayitlari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 450);
            this.Controls.Add(this.AboneKayitlari_KayitSil);
            this.Controls.Add(this.AboneKayitlari_DataGrid);
            this.Controls.Add(this.label1);
            this.Name = "AboneKayitlari";
            this.Text = "AboneKayitlari";
            this.Load += new System.EventHandler(this.AboneKayitlari_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AboneKayitlari_DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView AboneKayitlari_DataGrid;
        private System.Windows.Forms.Button AboneKayitlari_KayitSil;
    }
}