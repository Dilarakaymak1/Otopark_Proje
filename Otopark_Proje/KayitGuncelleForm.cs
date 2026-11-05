using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Otopark_Proje
{
    public partial class KayitGuncelleForm : Form
    {
        private string _tabloAdi;
        private string _idSutunu;
        private object _idDegeri;
        private DataGridViewRow _seciliSatir;

        public bool Kaydedildi { get; private set; } = false;

        private static readonly string[] AracMarkalari = {
            "Alfa Romeo","Audi","BMW","Chery","Chevrolet","Citroën","Cupra",
            "Dacia","DS","Fiat","Ford","Honda","Hyundai","Isuzu","Iveco",
            "Jaguar","Jeep","Kia","Land Rover","Lexus","MAN","Mazda",
            "Mercedes-Benz","MG","Mini","Mitsubishi","Nissan","Opel",
            "Peugeot","Porsche","Renault","Seat","Skoda","Smart","SsangYong",
            "Subaru","Suzuki","Tesla","Togg","Toyota","Volkswagen","Volvo","Diğer"
        };

        public KayitGuncelleForm(string tabloAdi, string baslik, string idSutunu, object idDegeri, DataGridViewRow seciliSatir)
        {
            InitializeComponent();
            _tabloAdi = tabloAdi;
            _idSutunu = idSutunu;
            _idDegeri = idDegeri;
            _seciliSatir = seciliSatir;

            this.Text = baslik + " - Kayıt Güncelle";
            lblBaslik.Text = baslik + " bilgilerini düzenleyin:";

            int y = 10;
            int tabIdx = 0;
            int sbWidth = SystemInformation.VerticalScrollBarWidth;
            int innerW = panelBilgiler.ClientSize.Width - sbWidth - 5;
            int lblW = 170;
            int ctrlX = lblW + 20;
            int ctrlW = innerW - ctrlX - 10;
            if (ctrlW < 150) ctrlW = 150;

            foreach (DataGridViewCell cell in seciliSatir.Cells)
            {
                if (!cell.OwningColumn.Visible) continue;

                string colName = cell.OwningColumn.Name;
                string header = cell.OwningColumn.HeaderText;
                object cellVal = cell.Value;
                string strVal = cellVal?.ToString() ?? "";
                Type valType = cellVal?.GetType();

                Label lbl = new Label();
                lbl.Text = header + ":";
                lbl.Location = new Point(10, y + 3);
                lbl.Size = new Size(lblW, 22);
                lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                panelBilgiler.Controls.Add(lbl);

                Control ctrl = OlusturKontrol(colName, _tabloAdi, cellVal, strVal, valType);
                ctrl.Name = "ctrl_" + colName;
                ctrl.Tag = colName;
                ctrl.Location = new Point(ctrlX, y);
                ctrl.Size = new Size(ctrlW, ctrl is ComboBox ? 24 : (ctrl is DateTimePicker ? 25 : 22));
                ctrl.TabIndex = tabIdx++;
                panelBilgiler.Controls.Add(ctrl);

                y += 32;
            }

            int panH = Math.Min(Math.Max(y + 10, 200), 450);
            panelBilgiler.Size = new Size(panelBilgiler.Width, panH);
            this.ClientSize = new Size(this.ClientSize.Width, panelBilgiler.Bottom + 70);

            int totalBtn = btnKaydet.Width + 30 + btnIptal.Width;
            int sx = (this.ClientSize.Width - totalBtn) / 2;
            btnKaydet.Location = new Point(sx, panelBilgiler.Bottom + 15);
            btnIptal.Location = new Point(sx + btnKaydet.Width + 30, panelBilgiler.Bottom + 15);
        }

        private Control OlusturKontrol(string col, string tablo, object val, string str, Type tip)
        {
            // ID sütunu - salt okunur
            if (col == _idSutunu)
            {
                TextBox t = new TextBox { Text = str, ReadOnly = true, BackColor = Color.LightGray, BorderStyle = BorderStyle.FixedSingle };
                return t;
            }

            // Tarih/Saat alanları
            if (tip == typeof(DateTime) || col.Contains("Tarih") || col.Contains("Baslangic") || col.Contains("Bitis") || col.Contains("Saat"))
            {
                DateTimePicker dtp = new DateTimePicker();
                if (col.Contains("Saat"))
                {
                    dtp.CustomFormat = "dd.MM.yyyy HH:mm";
                    dtp.Format = DateTimePickerFormat.Custom;
                }
                if (DateTime.TryParse(str, out DateTime dt)) dtp.Value = dt;
                return dtp;
            }

            // TC No
            if (col == "TCNo" || col == "TcNo")
            {
                MaskedTextBox m = new MaskedTextBox { Mask = "00000000000", Text = str };
                return m;
            }

            // Telefon
            if (col == "Telefon" || col == "Tel")
            {
                MaskedTextBox m = new MaskedTextBox { Mask = "(999) 000-0000", Text = str };
                return m;
            }

            // Plaka
            if (col == "Plaka")
            {
                MaskedTextBox m = new MaskedTextBox { Mask = ">00 L?? 0099", Text = str };
                return m;
            }

            // AracMarka
            if (col == "AracMarka")
            {
                ComboBox c = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Popup };
                c.Items.AddRange(AracMarkalari);
                int idx = c.FindStringExact(str);
                if (idx >= 0) c.SelectedIndex = idx;
                return c;
            }

            // Cinsiyet
            if (col == "Cinsiyet")
                return ComboOlustur(new[] { "Erkek", "Kadın" }, str);

            // Durum (tabloya göre farklı)
            if (col == "Durum")
            {
                if (tablo == "OtoparkHareketleri")
                    return ComboOlustur(new[] { "İçeride", "Çıktı", "İptal" }, str);
                else if (tablo == "AracYikama")
                    return ComboOlustur(new[] { "Tamamlandı", "Devam Ediyor", "İptal" }, str);
                else
                    return ComboOlustur(new[] { "Aktif", "Pasif" }, str);
            }

            // AktifMi
            if (col == "AktifMi")
            {
                ComboBox c = ComboOlustur(new[] { "Aktif", "Pasif" }, "");
                if (tip == typeof(bool) && val != null)
                    c.SelectedIndex = (bool)val ? 0 : 1;
                else
                    c.Text = str;
                return c;
            }

            // Diğer ComboBox alanları
            if (col == "AboneMi")
            {
                ComboBox c = ComboOlustur(new[] { "Evet", "Hayır" }, "");
                if (tip == typeof(bool) && val != null)
                    c.SelectedIndex = (bool)val ? 0 : 1;
                else
                    c.Text = str;
                return c;
            }

            // OdemeAlindiMi (AracYikama tablosunda bit olarak tutulur)
            if (col == "OdemeAlindiMi")
            {
                ComboBox c = ComboOlustur(new[] { "Ödendi", "Ödenmedi" }, "");
                if (tip == typeof(bool) && val != null)
                    c.SelectedIndex = (bool)val ? 0 : 1;
                else
                    c.Text = str;
                return c;
            }

            if (col == "IslemTipi") return ComboOlustur(new[] { "Gelir", "Gider" }, str);
            if (col == "Rol") return ComboOlustur(new[] { "Admin", "Kasiyer", "Gözetmen" }, str);
            if (col == "OdemeYontemi") return ComboOlustur(new[] { "Nakit", "Kredi Kartı", "EFT/Havale" }, str);
            if (col == "Kategori") return ComboOlustur(new[] { "Park Ücreti", "Abonelik", "Yıkama", "Kira", "Maaş", "Diğer" }, str);
            if (col == "ParaBirimi") return ComboOlustur(new[] { "TL", "USD", "EUR" }, str);
            if (col == "YikamaTipi") return ComboOlustur(new[] { "İç Yıkama", "Dış Yıkama", "İç + Dış Yıkama", "Detaylı Yıkama", "Motor Yıkama" }, str);

            // Sayısal alanlar
            if (col == "Ucret" || col == "Tutar" || col == "Maas" || col == "Fiyat" || col == "Adet" || col == "Miktar")
            {
                TextBox t = new TextBox { Text = str, BorderStyle = BorderStyle.FixedSingle };
                t.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.') e.Handled = true; };
                return t;
            }

            // Varsayılan TextBox
            return new TextBox { Text = str, BorderStyle = BorderStyle.FixedSingle };
        }

        private ComboBox ComboOlustur(string[] items, string secili)
        {
            ComboBox c = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Popup };
            c.Items.AddRange(items);
            if (!string.IsNullOrEmpty(secili)) c.Text = secili;
            return c;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.BaglantiAc();
                string set = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Form1.Baglanti;

                foreach (Control ctrl in panelBilgiler.Controls)
                {
                    if (ctrl.Tag == null) continue;
                    string col = ctrl.Tag.ToString();
                    if (col == _idSutunu) continue;

                    if (set.Length > 0) set += ", ";
                    set += col + "=@p" + col;

                    // Orijinal hücre tipini bul
                    DataGridViewCell orig = null;
                    foreach (DataGridViewCell c in _seciliSatir.Cells)
                        if (c.OwningColumn.Name == col) { orig = c; break; }
                    Type origType = orig?.Value?.GetType();

                    if (ctrl is DateTimePicker dtp)
                    {
                        cmd.Parameters.AddWithValue("@p" + col, dtp.Value);
                    }
                    else if (ctrl is ComboBox cmb)
                    {
                        if (col == "AktifMi" && origType == typeof(bool))
                            cmd.Parameters.AddWithValue("@p" + col, cmb.Text == "Aktif");
                        else if (col == "AboneMi" && origType == typeof(bool))
                            cmd.Parameters.AddWithValue("@p" + col, cmb.Text == "Evet");
                        else if (col == "OdemeAlindiMi" && origType == typeof(bool))
                            cmd.Parameters.AddWithValue("@p" + col, cmb.Text == "Ödendi");
                        else
                            cmd.Parameters.AddWithValue("@p" + col, cmb.Text);
                    }
                    else
                    {
                        if (origType == typeof(decimal) || origType == typeof(double) || origType == typeof(float))
                        {
                            decimal n;
                            if (decimal.TryParse(ctrl.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out n))
                                cmd.Parameters.AddWithValue("@p" + col, n);
                            else
                                cmd.Parameters.AddWithValue("@p" + col, ctrl.Text);
                        }
                        else if (origType == typeof(int) || origType == typeof(long))
                        {
                            int iv;
                            if (int.TryParse(ctrl.Text, out iv))
                                cmd.Parameters.AddWithValue("@p" + col, iv);
                            else
                                cmd.Parameters.AddWithValue("@p" + col, ctrl.Text);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p" + col, ctrl.Text);
                        }
                    }
                }

                cmd.CommandText = "UPDATE " + _tabloAdi + " SET " + set + " WHERE " + _idSutunu + "=@pID";
                cmd.Parameters.AddWithValue("@pID", _idDegeri);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Kayıt başarıyla güncellendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kaydedildi = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Kayıt güncellenirken hata oluştu:\n" + Hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Form1.Baglanti.State == ConnectionState.Open)
                    Form1.Baglanti.Close();
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lblBaslik_Click(object sender, EventArgs e)
        {

        }
    }
}
