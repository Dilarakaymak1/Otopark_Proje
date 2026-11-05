using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Otopark_Proje
{
    public partial class KayitSilOnayForm : Form
    {
        public bool Onaylandi { get; private set; } = false;

        /// <summary>
        /// Kayıt silme onay formu. Seçili satırın bilgilerini pasif metin kutularında gösterir.
        /// </summary>
        /// <param name="tabloAdi">Tablo başlığı (ör: "Personel Kaydı")</param>
        /// <param name="seciliSatir">DataGridView'den seçilen satır</param>
        public KayitSilOnayForm(string tabloAdi, DataGridViewRow seciliSatir)
        {
            InitializeComponent();

            this.Text = tabloAdi + " - Kayıt Silme Onayı";
            lblBaslik.Text = tabloAdi + " silinecek. Bilgileri kontrol edin:";

            // Seçili satırdaki tüm sütunları panele ekle
            int y = 10;
            // Scrollbar olsa bile TextBox'un taşmaması için genişliği hesapla
            int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;
            int panelInnerWidth = panelBilgiler.ClientSize.Width - scrollBarWidth - 5;
            int labelWidth = 170;
            int txtX = labelWidth + 20;
            int txtWidth = panelInnerWidth - txtX - 10;
            if (txtWidth < 150) txtWidth = 150; // minimum genişlik

            foreach (DataGridViewCell cell in seciliSatir.Cells)
            {
                if (cell.OwningColumn.Visible && cell.Value != null)
                {
                    // Sütun adı label
                    Label lbl = new Label();
                    lbl.Text = cell.OwningColumn.HeaderText + ":";
                    lbl.Location = new Point(10, y + 3);
                    lbl.Size = new Size(labelWidth, 22);
                    lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    panelBilgiler.Controls.Add(lbl);

                    // Değer textbox (ReadOnly)
                    TextBox txt = new TextBox();
                    txt.Text = cell.Value.ToString();
                    txt.Location = new Point(txtX, y);
                    txt.Size = new Size(txtWidth, 22);
                    txt.ReadOnly = true;
                    txt.BackColor = Color.WhiteSmoke;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    panelBilgiler.Controls.Add(txt);

                    y += 30;
                }
            }

            // Panel yüksekliğini ayarla (minimum 200, maksimum 400)
            int panelHeight = Math.Min(Math.Max(y + 10, 200), 400);
            panelBilgiler.Size = new Size(panelBilgiler.Width, panelHeight);

            // Form yüksekliğini ayarla
            this.ClientSize = new Size(this.ClientSize.Width, panelBilgiler.Bottom + 70);

            // Butonları form genişliğine göre ortala
            int toplamButonGenisligi = btnSil.Width + 30 + btnIptal.Width; // 30px ara boşluk
            int baslangicX = (this.ClientSize.Width - toplamButonGenisligi) / 2;
            btnSil.Location = new Point(baslangicX, panelBilgiler.Bottom + 15);
            btnIptal.Location = new Point(baslangicX + btnSil.Width + 30, panelBilgiler.Bottom + 15);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var sonuc = MessageBox.Show(
                "Bu kaydı silmek istediğinizden emin misiniz?\nBu işlem geri alınamaz!",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (sonuc == DialogResult.Yes)
            {
                Onaylandi = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Onaylandi = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void KayitSilOnayForm_Load(object sender, EventArgs e)
        {

        }
    }
}
