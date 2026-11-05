using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Otopark_Proje.Forms
{
    public class FrmPersoneller : Form
    {
        // ─── Renkler ────────────────────────────────────────────────
        private readonly Color RENK_BASLIK = Color.FromArgb(26, 42, 82);
        private readonly Color RENK_ACCENT = Color.FromArgb(41, 98, 255);
        private readonly Color RENK_YESIL = Color.FromArgb(39, 174, 96);
        private readonly Color RENK_KIRMIZI = Color.FromArgb(192, 57, 43);
        private readonly Color RENK_TURUNCU = Color.FromArgb(211, 84, 0);
        private readonly Color RENK_GRI = Color.FromArgb(127, 140, 141);
        private readonly Color RENK_INPUT_BG = Color.FromArgb(240, 242, 248);
        private readonly Color RENK_SATIR1 = Color.White;
        private readonly Color RENK_SATIR2 = Color.FromArgb(245, 248, 255);

        // ─── Durum ──────────────────────────────────────────────────
        private int _seciliId = -1;
        private bool _duzenleme = true;  // Form açılışında yeni kayıt modunda başla

        // ─── Kontroller ─────────────────────────────────────────────
        // Üst başlık
        private Panel pnlBaslik;
        private Label lblBaslik;
        private Button btnKapat;
        // Giriş formu (üst bölüm)
        private Panel pnlForm;
        private Label lblFormBaslik;
        // Alanlar
        private Label lblAd, lblSoyad, lblTC, lblTel, lblGorev, lblGiris, lblMaas, lblDurum, lblAdres;
        private TextBox txtAd, txtSoyad, txtTC, txtTel, txtMaas, txtAdres;
        private ComboBox cmbGorev, cmbDurum;
        private DateTimePicker dtpGiris;
        // Butonlar
        private Panel pnlButonlar;
        private Button btnYeni, btnKaydet, btnDuzenle, btnSil, btnTemizle;
        private Label lblMesaj;
        // Alt liste (DataGridView)
        private Panel pnlListe;
        private Panel pnlAramaBar;
        private TextBox txtArama;
        private Label lblAramaIcon;
        private Label lblKayitSayisi;
        private DataGridView dgv;

        // ────────────────────────────────────────────────────────────
        public FrmPersoneller()
        {
            InitializeComponents();
            KontrrolleriAyarla();
            DugmeleriGuncelle();
            PersonelleriYukle();
        }

        // ════════════════════════════════════════════════════════════
        //  ARAYÜZ KURULUMU
        // ════════════════════════════════════════════════════════════
        private void InitializeComponents()
        {
            // ── Form ────────────────────────────────────────────────
            this.Text = "Personel Yönetimi | Otopark Sistemi";
            this.Size = new Size(1100, 780);
            this.MinimumSize = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.Font = new Font("Segoe UI", 9f);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Icon = SystemIcons.Application;

            // ── ÜST BAŞLIK ──────────────────────────────────────────
            pnlBaslik = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = RENK_BASLIK
            };

            lblBaslik = new Label
            {
                Text = "  👤  Personel Yönetimi",
                Font = new Font("Segoe UI", 15f, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            btnKapat = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(192, 57, 43),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(50, 50),
                Dock = DockStyle.Right,
                Cursor = Cursors.Hand
            };
            btnKapat.FlatAppearance.BorderSize = 0;
            btnKapat.Click += (s, e) => this.Close();

            pnlBaslik.Controls.Add(lblBaslik);
            pnlBaslik.Controls.Add(btnKapat);

            // ── FORM PANELİ (Üst - Veri Girişi) ────────────────────
            pnlForm = new Panel
            {
                Dock = DockStyle.Top,
                Height = 340,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 10)
            };

            lblFormBaslik = new Label
            {
                Text = "Personel Bilgileri",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = RENK_BASLIK,
                Location = new Point(20, 15),
                AutoSize = true
            };

            // Mesaj etiketi
            lblMesaj = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(20, 300),
                Size = new Size(700, 22),
                ForeColor = RENK_YESIL
            };

            // ── ALAN TANIMI: (İkili sütun düzeni) ──────────────────
            // Satır 1: Ad | Soyad
            lblAd = YeniEtiket("Ad *");
            txtAd = YeniTextBox();
            lblSoyad = YeniEtiket("Soyad *");
            txtSoyad = YeniTextBox();

            // Satır 2: TC No | Telefon
            lblTC = YeniEtiket("TC Kimlik No");
            txtTC = YeniTextBox();
            lblTel = YeniEtiket("Telefon");
            txtTel = YeniTextBox();

            // Satır 3: Görevi | İşe Giriş Tarihi
            lblGorev = YeniEtiket("Görevi *");
            cmbGorev = new ComboBox
            {
                Font = new Font("Segoe UI", 10f),
                BackColor = RENK_INPUT_BG,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(220, 30)
            };
            cmbGorev.Items.AddRange(new object[]
            { "Yönetici", "Kasiyer", "Güvenlik", "Yıkamacı",
              "Park Görevlisi", "Muhasebeci", "Teknisyen", "Diğer" });

            lblGiris = YeniEtiket("İşe Giriş Tarihi");
            dtpGiris = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10f),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                Size = new Size(220, 30)
            };

            // Satır 4: Maaş | Durum
            lblMaas = YeniEtiket("Maaş (₺)");
            txtMaas = YeniTextBox();
            txtMaas.Text = "0";

            lblDurum = YeniEtiket("Durum");
            cmbDurum = new ComboBox
            {
                Font = new Font("Segoe UI", 10f),
                BackColor = RENK_INPUT_BG,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(220, 30)
            };
            cmbDurum.Items.AddRange(new object[] { "Aktif", "İzinli", "Ayrıldı" });
            cmbDurum.SelectedIndex = 0;

            // Satır 5: Adres (geniş)
            lblAdres = YeniEtiket("Adres");
            txtAdres = new TextBox
            {
                Font = new Font("Segoe UI", 10f),
                BackColor = RENK_INPUT_BG,
                BorderStyle = BorderStyle.None,
                Multiline = false,
                Size = new Size(480, 30)
            };

            // Pozisyonları ata
            int colL = 20, colR = 560, rowH = 58, startY = 45;

            PozisyonAta(lblAd, txtAd, colL, startY);
            PozisyonAta(lblSoyad, txtSoyad, colR, startY);

            PozisyonAta(lblTC, txtTC, colL, startY + rowH);
            PozisyonAta(lblTel, txtTel, colR, startY + rowH);

            lblGorev.Location = new Point(colL, startY + rowH * 2);
            cmbGorev.Location = new Point(colL, startY + rowH * 2 + 22);
            lblGiris.Location = new Point(colR, startY + rowH * 2);
            dtpGiris.Location = new Point(colR, startY + rowH * 2 + 22);

            PozisyonAta(lblMaas, txtMaas, colL, startY + rowH * 3);
            lblDurum.Location = new Point(colR, startY + rowH * 3);
            cmbDurum.Location = new Point(colR, startY + rowH * 3 + 22);

            lblAdres.Location = new Point(colL, startY + rowH * 4);
            txtAdres.Location = new Point(colL, startY + rowH * 4 + 22);

            // Hepsini forma ekle
            pnlForm.Controls.AddRange(new Control[]
            {
                lblFormBaslik,
                lblAd, txtAd, lblSoyad, txtSoyad,
                lblTC, txtTC, lblTel, txtTel,
                lblGorev, cmbGorev, lblGiris, dtpGiris,
                lblMaas, txtMaas, lblDurum, cmbDurum,
                lblAdres, txtAdres,
                lblMesaj
            });

            // ── BUTON PANELİ ────────────────────────────────────────
            pnlButonlar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(250, 251, 255),
                Padding = new Padding(20, 10, 20, 10)
            };

            btnYeni = YeniButon("＋  Yeni Personel", RENK_ACCENT, 0);
            btnKaydet = YeniButon("💾  Kaydet", RENK_YESIL, 150);
            btnDuzenle = YeniButon("✏  Düzenle", RENK_TURUNCU, 300);
            btnSil = YeniButon("🗑  Sil", RENK_KIRMIZI, 450);
            btnTemizle = YeniButon("✖  İptal / Temizle", RENK_GRI, 590);

            btnYeni.Click += BtnYeni_Click;
            btnKaydet.Click += BtnKaydet_Click;
            btnDuzenle.Click += BtnDuzenle_Click;
            btnSil.Click += BtnSil_Click;
            btnTemizle.Click += BtnTemizle_Click;

            pnlButonlar.Controls.AddRange(new Control[]
            { btnYeni, btnKaydet, btnDuzenle, btnSil, btnTemizle });

            // ── LISTE PANELİ (Alt - DataGridView) ───────────────────
            pnlListe = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 15)
            };

            // Arama barı
            pnlAramaBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                BackColor = Color.FromArgb(240, 242, 248)
            };
            lblAramaIcon = new Label
            {
                Text = "🔍",
                Font = new Font("Segoe UI", 12f),
                Location = new Point(8, 10),
                AutoSize = true
            };
            txtArama = new TextBox
            {
                Font = new Font("Segoe UI", 10f),
                BackColor = Color.FromArgb(240, 242, 248),
                BorderStyle = BorderStyle.None,
                Location = new Point(36, 12),
                Width = 320,
                ForeColor = Color.FromArgb(100, 110, 130)
            };
            txtArama.Text = "Ad, Soyad, TC veya Görev ile arayın...";
            txtArama.GotFocus += (s, e) =>
            {
                if (txtArama.Text == "Ad, Soyad, TC veya Görev ile arayın...")
                { txtArama.Text = ""; txtArama.ForeColor = Color.FromArgb(30, 39, 46); }
            };
            txtArama.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtArama.Text))
                { txtArama.Text = "Ad, Soyad, TC veya Görev ile arayın..."; txtArama.ForeColor = Color.FromArgb(100, 110, 130); }
            };
            txtArama.TextChanged += (s, e) =>
            {
                string q = txtArama.Text == "Ad, Soyad, TC veya Görev ile arayın..." ? "" : txtArama.Text;
                PersonelleriYukle(q);
            };

            lblKayitSayisi = new Label
            {
                Text = "0 kayıt",
                Font = new Font("Segoe UI", 9f),
                ForeColor = RENK_GRI,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(700, 12),
                AutoSize = true
            };

            pnlAramaBar.Controls.AddRange(new Control[]
            { lblAramaIcon, txtArama, lblKayitSayisi });

            // DataGridView
            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(220, 225, 235),
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ColumnHeadersHeight = 38,
                RowTemplate = { Height = 34 },
                Font = new Font("Segoe UI", 9f),
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                EnableHeadersVisualStyles = false
            };
            // Header stili
            dgv.ColumnHeadersDefaultCellStyle.BackColor = RENK_BASLIK;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            // Zebra satır
            dgv.DefaultCellStyle.BackColor = RENK_SATIR1;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 220, 255);
            dgv.DefaultCellStyle.SelectionForeColor = RENK_BASLIK;
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = RENK_SATIR2;
            dgv.SelectionChanged += Dgv_SelectionChanged;

            pnlListe.Controls.Add(dgv);
            pnlListe.Controls.Add(pnlAramaBar);

            // Ayırıcı çizgi
            Panel pnlAyrac = new Panel
            {
                Dock = DockStyle.Top,
                Height = 4,
                BackColor = Color.FromArgb(221, 226, 240)
            };

            // ── FORM'A EKLE (sıralama önemli: Fill en son) ──────────
            this.Controls.Add(pnlListe);
            this.Controls.Add(pnlAyrac);
            this.Controls.Add(pnlButonlar);
            this.Controls.Add(pnlForm);
            this.Controls.Add(pnlBaslik);
        }

        // ════════════════════════════════════════════════════════════
        //  KONTROLLER BAŞLANGIÇ AYARLARI
        // ════════════════════════════════════════════════════════════
        private void KontrrolleriAyarla()
        {
            // Tüm input arka planları için border efekti - her TextBox için panel sar
            // (basit yaklaşım: BackColor + Padding ayarı)
            foreach (Control c in pnlForm.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.Enter += (s, e) => ((TextBox)s).BackColor = Color.FromArgb(230, 235, 255);
                    tb.Leave += (s, e) => ((TextBox)s).BackColor = RENK_INPUT_BG;
                }
            }
        }

        // ════════════════════════════════════════════════════════════
        //  VERİTABANI İŞLEMLERİ
        // ════════════════════════════════════════════════════════════
        private void PersonelleriYukle(string arama = "")
        {
            try
            {
                string sql = @"
                    SELECT
                        PersonelId      AS [ID],
                        Ad              AS [Ad],
                        Soyad           AS [Soyad],
                        TCNo            AS [TC No],
                        Telefon         AS [Telefon],
                        Gorevi          AS [Görevi],
                        CONVERT(VARCHAR,IseGirisTarihi,104) AS [İşe Giriş],
                        CONVERT(NVARCHAR(20), Maas, 0) + ' TL' AS [Maas],
                        Durum           AS [Durum]
                    FROM Personeller
                    WHERE (
                        @Arama = '' OR
                        Ad    LIKE '%'+@Arama+'%' OR
                        Soyad LIKE '%'+@Arama+'%' OR
                        TCNo  LIKE '%'+@Arama+'%' OR
                        Gorevi LIKE '%'+@Arama+'%' OR
                        Telefon LIKE '%'+@Arama+'%'
                    )
                    ORDER BY PersonelId DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, new[]
                {
                    new SqlParameter("@Arama", arama ?? "")
                });

                dgv.DataSource = dt;
                lblKayitSayisi.Text = dt.Rows.Count + " kayıt";

                // Durum sütununa renk ver
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    string durum = row.Cells["Durum"].Value?.ToString();
                    if (durum == "Aktif")
                        row.Cells["Durum"].Style.ForeColor = RENK_YESIL;
                    else if (durum == "Ayrıldı")
                        row.Cells["Durum"].Style.ForeColor = RENK_KIRMIZI;
                    else
                        row.Cells["Durum"].Style.ForeColor = RENK_TURUNCU;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Liste yüklenemedi:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PersonelKaydet()
        {
            if (!Dogrula()) return;

            try
            {
                if (_seciliId == -1)
                {
                    // INSERT
                    string sql = @"
                        INSERT INTO Personeller
                            (Ad, Soyad, TCNo, Telefon, Gorevi, IseGirisTarihi, Maas, Durum, Adres)
                        VALUES
                            (@Ad, @Soyad, @TCNo, @Telefon, @Gorevi, @Giris, @Maas, @Durum, @Adres)";
                    DatabaseHelper.ExecuteNonQuery(sql, Parametreler());
                    MesajGoster("✔  Personel başarıyla eklendi.", true);
                }
                else
                {
                    // UPDATE
                    string sql = @"
                        UPDATE Personeller SET
                            Ad=@Ad, Soyad=@Soyad, TCNo=@TCNo, Telefon=@Telefon,
                            Gorevi=@Gorevi, IseGirisTarihi=@Giris, Maas=@Maas,
                            Durum=@Durum, Adres=@Adres
                        WHERE PersonelId=@Id";
                    var p = Parametreler();
                    Array.Resize(ref p, p.Length + 1);
                    p[p.Length - 1] = new SqlParameter("@Id", _seciliId);
                    DatabaseHelper.ExecuteNonQuery(sql, p);
                    MesajGoster("✔  Personel bilgileri güncellendi.", true);
                }

                _seciliId = -1;
                _duzenleme = false;
                Temizle();
                DugmeleriGuncelle();
                PersonelleriYukle(AramaMetni());
            }
            catch (Exception ex)
            {
                MesajGoster("✘  Hata: " + ex.Message, false);
            }
        }

        private void PersonelSil()
        {
            if (_seciliId == -1)
            {
                MessageBox.Show("Lütfen önce listeden bir personel seçin.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ad = txtAd.Text + " " + txtSoyad.Text;
            DialogResult dr = MessageBox.Show(
                $"«{ad}» adlı personelin ilişiği kesilecek ve durumu 'Ayrıldı' olarak işaretlenecek.\nEmin misiniz?",
                "İlişik Kesme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr != DialogResult.Yes) return;

            try
            {
                // Diğer tablolara FK bağlı olduğundan kalıcı silme yerine
                // "Ayrıldı" olarak işaretliyoruz (soft delete)
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE Personeller SET Durum = N'Ayrıldı' WHERE PersonelId = @Id",
                    new[] { new SqlParameter("@Id", _seciliId) });

                MesajGoster("✔  Personelin ilişiği kesildi (Ayrıldı olarak işaretlendi).", true);
                _seciliId = -1;
                _duzenleme = false;
                Temizle();
                DugmeleriGuncelle();
                PersonelleriYukle(AramaMetni());
            }
            catch (Exception ex)
            {
                MesajGoster("✘  Hata: " + ex.Message, false);
            }
        }

        // ════════════════════════════════════════════════════════════
        //  OLAY YÖNETİCİLERİ
        // ════════════════════════════════════════════════════════════
        private void BtnYeni_Click(object sender, EventArgs e)
        {
            _seciliId = -1;
            _duzenleme = true;
            Temizle();
            DugmeleriGuncelle();
            if (dgv.CurrentRow != null) dgv.ClearSelection();
            txtAd.Focus();
            lblMesaj.Text = "Yeni personel bilgilerini doldurun, ardından Kaydet'e basın.";
            lblMesaj.ForeColor = RENK_ACCENT;
        }

        private void BtnKaydet_Click(object sender, EventArgs e) => PersonelKaydet();

        private void BtnDuzenle_Click(object sender, EventArgs e)
        {
            // Artık kullanılmıyor - satıra tıklamak zaten formu doldurur ve kaydet aktif
            if (_seciliId != -1)
                txtAd.Focus();
        }

        private void BtnSil_Click(object sender, EventArgs e) => PersonelSil();

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            _seciliId = -1;
            _duzenleme = true;
            Temizle();
            DugmeleriGuncelle();
            if (dgv.CurrentRow != null) dgv.ClearSelection();
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null || dgv.CurrentRow.Index < 0) return;

            DataGridViewRow row = dgv.CurrentRow;
            _seciliId = Convert.ToInt32(row.Cells["ID"].Value);

            // Form alanlarını doldur
            txtAd.Text = row.Cells["Ad"].Value?.ToString() ?? "";
            txtSoyad.Text = row.Cells["Soyad"].Value?.ToString() ?? "";
            txtTC.Text = row.Cells["TC No"].Value?.ToString() ?? "";
            txtTel.Text = row.Cells["Telefon"].Value?.ToString() ?? "";
            cmbGorev.Text = row.Cells["Görevi"].Value?.ToString() ?? "";
            cmbDurum.Text = row.Cells["Durum"].Value?.ToString() ?? "";
            txtMaas.Text = (row.Cells["Maas"].Value?.ToString() ?? "").Replace(" TL", "");

            // İşe giriş tarihini parse et
            if (DateTime.TryParseExact(
                row.Cells["İşe Giriş"].Value?.ToString(),
                "dd.MM.yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime dt))
                dtpGiris.Value = dt;

            // Adres ayrıca çek (griddde yok)
            DataTable adresDt = DatabaseHelper.ExecuteQuery(
                "SELECT Adres FROM Personeller WHERE PersonelId = @Id",
                new[] { new SqlParameter("@Id", _seciliId) });
            txtAdres.Text = adresDt.Rows.Count > 0 ? adresDt.Rows[0]["Adres"]?.ToString() ?? "" : "";

            _duzenleme = true;
            DugmeleriGuncelle();
            lblMesaj.Text = "Bilgileri düzenleyip Kaydet'e basabilirsiniz.";
            lblMesaj.ForeColor = RENK_ACCENT;
        }

        // ════════════════════════════════════════════════════════════
        //  YARDIMCI METOTLAR
        // ════════════════════════════════════════════════════════════
        private bool Dogrula()
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            { MesajGoster("✘  Ad alanı zorunludur.", false); txtAd.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtSoyad.Text))
            { MesajGoster("✘  Soyad alanı zorunludur.", false); txtSoyad.Focus(); return false; }
            if (!string.IsNullOrWhiteSpace(txtTC.Text) && txtTC.Text.Trim().Length != 11)
            { MesajGoster("✘  TC No 11 haneli olmalıdır.", false); txtTC.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(cmbGorev.Text))
            { MesajGoster("✘  Görev seçilmesi zorunludur.", false); cmbGorev.Focus(); return false; }
            if (!decimal.TryParse(txtMaas.Text.Trim(), out _))
            { MesajGoster("✘  Maaş geçerli bir sayı olmalıdır.", false); txtMaas.Focus(); return false; }
            return true;
        }

        private SqlParameter[] Parametreler()
        {
            return new[]
            {
                new SqlParameter("@Ad",     txtAd.Text.Trim()),
                new SqlParameter("@Soyad",  txtSoyad.Text.Trim()),
                new SqlParameter("@TCNo",   txtTC.Text.Trim()),
                new SqlParameter("@Telefon",txtTel.Text.Trim()),
                new SqlParameter("@Gorevi", cmbGorev.Text.Trim()),
                new SqlParameter("@Giris",  dtpGiris.Value.Date),
                new SqlParameter("@Maas",   decimal.Parse(txtMaas.Text.Trim())),
                new SqlParameter("@Durum",  cmbDurum.Text.Trim()),
                new SqlParameter("@Adres",  txtAdres.Text.Trim())
            };
        }

        private void Temizle()
        {
            txtAd.Text = txtSoyad.Text = txtTC.Text =
            txtTel.Text = txtAdres.Text = "";
            txtMaas.Text = "0";
            cmbGorev.SelectedIndex = -1;
            cmbDurum.SelectedIndex = 0;
            dtpGiris.Value = DateTime.Today;
            lblMesaj.Text = "";
        }

        private void DugmeleriGuncelle()
        {
            bool secildi = _seciliId != -1;

            // Butonlar: Yeni her zaman aktif, Kaydet her zaman aktif,
            // Düzenle ve Sil sadece bir kayıt seçiliyken aktif
            btnYeni.Enabled = true;
            btnKaydet.Enabled = true;
            btnDuzenle.Enabled = false;  // Artık kullanılmıyor - çift tıkla düzenleme yeterli
            btnSil.Enabled = secildi;
            btnTemizle.Enabled = true;

            // Input alanları her zaman yazılabilir
            txtAd.ReadOnly = false;
            txtSoyad.ReadOnly = false;
            txtTC.ReadOnly = false;
            txtTel.ReadOnly = false;
            txtMaas.ReadOnly = false;
            txtAdres.ReadOnly = false;
            cmbGorev.Enabled = true;
            cmbDurum.Enabled = true;
            dtpGiris.Enabled = true;

            // Tüm input alanları açık renkte
            txtAd.BackColor = RENK_INPUT_BG;
            txtSoyad.BackColor = RENK_INPUT_BG;
            txtTC.BackColor = RENK_INPUT_BG;
            txtTel.BackColor = RENK_INPUT_BG;
            txtMaas.BackColor = RENK_INPUT_BG;
            txtAdres.BackColor = RENK_INPUT_BG;
        }

        private void MesajGoster(string mesaj, bool basarili)
        {
            lblMesaj.Text = mesaj;
            lblMesaj.ForeColor = basarili ? RENK_YESIL : RENK_KIRMIZI;
            Timer t = new Timer { Interval = 4000 };
            t.Tick += (s, e) => { lblMesaj.Text = ""; t.Stop(); t.Dispose(); };
            t.Start();
        }

        private string AramaMetni()
        {
            return txtArama.Text == "Ad, Soyad, TC veya Görev ile arayın..." ? "" : txtArama.Text;
        }

        // ─── Yardımcı oluşturucular ──────────────────────────────────
        private Label YeniEtiket(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 85, 110),
                AutoSize = true
            };
        }

        private TextBox YeniTextBox()
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 10f),
                BackColor = RENK_INPUT_BG,
                BorderStyle = BorderStyle.None,
                Size = new Size(220, 28)
            };
        }

        private Button YeniButon(string text, Color renk, int x)
        {
            Button b = new Button
            {
                Text = text,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = renk,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(136, 38),
                Location = new Point(x, 11),
                Cursor = Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = ControlPaint.Light(renk, 0.1f);
            return b;
        }

        private void PozisyonAta(Label lbl, Control input, int x, int y)
        {
            lbl.Location = new Point(x, y);
            input.Location = new Point(x, y + 22);
        }
    }
}
