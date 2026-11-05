using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Otopark_Proje
{
    /// <summary>
    /// Formlara "ComboBox Öğeleri Düzenle" butonu ekleyen ve popup açan yardımcı sınıf.
    /// </summary>
    public static class ComboBoxDuzenleHelper
    {
        /// <summary>
        /// Forma dinamik bir "Öğeleri Düzenle" butonu ekler ve tıklandığında
        /// ComboBoxDuzenleForm popup'ını açarak ilgili ComboBox'ların öğelerini düzenletir.
        /// </summary>
        /// <param name="hedefForm">Butonun ekleneceği form</param>
        /// <param name="formBasligi">Popup formun başlığı</param>
        /// <param name="comboBoxListesi">ComboBox görünen adı → ComboBox referansı eşlemesi</param>
        /// <param name="konum">Butonun form üzerindeki konumu</param>
        /// <param name="boyut">Butonun boyutu</param>
        public static Button ButonEkleVeBagla(
            Form hedefForm,
            string formBasligi,
            Dictionary<string, ComboBox> comboBoxListesi,
            Point konum,
            Size boyut)
        {
            Button duzenleButon = new Button
            {
                Text = "Öğeleri Düzenle",
                Location = konum,
                Size = boyut,
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Poppins", 8F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Name = "comboBoxDuzenleButon"
            };
            duzenleButon.FlatAppearance.BorderSize = 0;

            duzenleButon.Click += (sender, e) =>
            {
                ComboBoxDuzenleForm duzenleForm = new ComboBoxDuzenleForm(formBasligi, comboBoxListesi);
                if (duzenleForm.ShowDialog() == DialogResult.OK)
                {
                    // Tüm ComboBox'ları güncelle
                    foreach (var kvp in comboBoxListesi)
                    {
                        string comboAd = kvp.Key;
                        ComboBox comboBox = kvp.Value;

                        // DataSource ile doldurulan ComboBox'ları atla (veritabanından gelen veriler)
                        if (comboBox.DataSource != null) continue;

                        if (duzenleForm.GuncelVeriler.ContainsKey(comboAd))
                        {
                            // Mevcut seçimi kaydet
                            string mevcutSecim = comboBox.Text;

                            // Öğeleri güncelle
                            comboBox.Items.Clear();
                            foreach (string oge in duzenleForm.GuncelVeriler[comboAd])
                            {
                                comboBox.Items.Add(oge);
                            }

                            // Eski seçimi geri yükle (eğer hâlâ listedeyse)
                            int idx = comboBox.FindStringExact(mevcutSecim);
                            if (idx >= 0)
                                comboBox.SelectedIndex = idx;
                            else
                                comboBox.SelectedIndex = -1;
                        }
                    }
                }
            };

            hedefForm.Controls.Add(duzenleButon);
            duzenleButon.BringToFront();
            return duzenleButon;
        }

        /// <summary>
        /// ComboBox düzenleme popup'ını açar ve kapandığında ComboBox'ları günceller.
        /// Designer'da tanımlı butonların Click olayından çağrılır.
        /// </summary>
        /// <param name="formBasligi">Popup formun başlığı</param>
        /// <param name="comboBoxListesi">ComboBox görünen adı → ComboBox referansı eşlemesi</param>
        public static void PopupAc(string formBasligi, Dictionary<string, ComboBox> comboBoxListesi)
        {
            ComboBoxDuzenleForm duzenleForm = new ComboBoxDuzenleForm(formBasligi, comboBoxListesi);
            if (duzenleForm.ShowDialog() == DialogResult.OK)
            {
                foreach (var kvp in comboBoxListesi)
                {
                    string comboAd = kvp.Key;
                    ComboBox comboBox = kvp.Value;

                    if (comboBox.DataSource != null) continue;

                    if (duzenleForm.GuncelVeriler.ContainsKey(comboAd))
                    {
                        string mevcutSecim = comboBox.Text;

                        comboBox.Items.Clear();
                        foreach (string oge in duzenleForm.GuncelVeriler[comboAd])
                        {
                            comboBox.Items.Add(oge);
                        }

                        int idx = comboBox.FindStringExact(mevcutSecim);
                        if (idx >= 0)
                            comboBox.SelectedIndex = idx;
                        else
                            comboBox.SelectedIndex = -1;
                    }
                }
            }
        }
    }
}
