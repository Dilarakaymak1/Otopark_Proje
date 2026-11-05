using System;

namespace Otopark_Proje.Models
{
    /// <summary>
    /// Personeller tablosunu temsil eden model sınıfı.
    /// </summary>
    public class Personel
    {
        public int PersonelId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TCNo { get; set; }
        public string Telefon { get; set; }
        public string Gorevi { get; set; }
        public DateTime IseGirisTarihi { get; set; } = DateTime.Now;
        public decimal Maas { get; set; }
        public string Durum { get; set; } = "Aktif";
        public string Adres { get; set; }

        public string TamAd => $"{Ad} {Soyad}";
    }
}
