🚗 Otopark Yönetim Sistemi

Microsoft SQL Server veritabanı üzerinde çalışan, Visual Studio ile geliştirilmiş bir otopark yönetim uygulaması. Araç giriş-çıkış takibi, abonelik yönetimi, araç yıkama hizmeti, kasa (gelir-gider) kayıtları, personel ve kullanıcı yönetimi gibi bir otoparkın günlük işlerini tek bir yerden yönetmeyi amaçlar.

📌 İçindekiler
Özellikler
Kullanılan Teknolojiler
Proje Yapısı
Veritabanı
Kurulum
Kullanım
Ekran Görüntüleri
Katkıda Bulunma
İletişim
✨ Özellikler
Otopark hareketleri: Araç plakası, giriş/çıkış saati, park yeri ve ücret bilgisiyle giriş-çıkış takibi
Abone yönetimi: Abone ve araç bilgileri, abonelik başlangıç/bitiş tarihleri, aktiflik durumu
Tarife yönetimi: Tarife adı, ücret, para birimi ve geçerlilik tarih aralığı
Araç yıkama: Yıkama tipi, fiyat, yıkamacı personel, başlangıç/bitiş saati, ödeme durumu
Kasa: Gelir/gider kayıtları, kategori, ödeme yöntemi, fiş numarası ve silinen kayıtların işaretlenmesi
Personel yönetimi: Personel bilgileri, görev, maaş ve çalışma durumu
Kullanıcı ve rol yönetimi: Kullanıcı adı ve şifre hash'i ile giriş, rol bazlı yetkilendirme
Telefon rehberi: Kişi ve kurum iletişim kayıtları
🛠 Kullanılan Teknolojiler
Katman	Teknoloji
Dil	C#
Platform	.NET [Framework / Core sürümü]
Arayüz	[Windows Formları / WPF]
Veritabanı	Microsoft SQL Server 2019 (Express)
IDE	Visual Studio
📁 Proje Yapısı
Otopark_Proje/
├── Database/          # Veritabanı paketi (Otopark.dacpac)
├── Otopark_Proje/     # Ana uygulama (arayüz ve iş mantığı)
├── vtUygulama/        # Veritabanı işlemleri için uygulama
├── Otopark_Proje.sln  # Visual Studio çözüm dosyası
├── .gitignore
├── .gitattributes
└── README.md
🗄 Veritabanı

Veritabanı adı: Otopark (SQL Server 2019 Express'ten dışa aktarılmış DACPAC, sürüm 1.0.0.0). Toplam 8 tablo bulunur.

Tablolar
Tablo	Açıklama
Personeller	Çalışan bilgileri (ad, soyad, TC no, telefon, görev, işe giriş tarihi, maaş, durum, adres)
Kullanicilar	Uygulama giriş hesapları (kullanıcı adı, şifre hash'i, rol, son giriş tarihi, aktiflik)
Aboneler	Abone bilgileri ve araç plakası, abonelik süresi, aktiflik durumu
Tarifeler	Ücret tarifeleri (ücret, para birimi, geçerlilik aralığı)
OtoparkHareketleri	Araç giriş-çıkış kayıtları, park yeri, ücret ve işlemi yapan personel
AracYikama	Yıkama hizmeti kayıtları, fiyat, durum ve ödeme bilgisi
Kasa	Gelir/gider işlemleri, kategori, ödeme yöntemi, fiş no
TelefonRehberi	Kişi/kurum telefon ve iletişim bilgileri
İlişkiler
Personeller ──< Kullanicilar          (Kullanicilar.PersonelId)
Personeller ──< OtoparkHareketleri    (OtoparkHareketleri.IslemYapanPersonelId)
Personeller ──< Kasa                  (Kasa.PersonelId)
Kullanicilar ──< Tarifeler            (Tarifeler.OlusturanId)

Aboneler, ve tabloları yabancı anahtar içermez. , ve tabloları arasındaki eşleşme alanı üzerinden uygulama tarafında yapılır.AracYikamaTelefonRehberiAbonelerOtoparkHareketleriAracYikamaPlaka

Kısıtlar
Her tablonun birincil anahtarı otomatik artan (IDENTITY) alanıdır.int
Personeller.TCNo ve alanları benzersizdir (UNIQUE).Kullanicilar.KullaniciAdi
Varsayılan değerler
Alan	Varsayılan
Personeller.Durum	Aktif
Personeller.Maas	0.00
Kullanicilar.Rol	Kasiyer
Kullanicilar.AktifMi	1 (aktif)
Tarifeler.ParaBirimi	TL
Tarifeler.AktifMi	1 (aktif)
OtoparkHareketleri.Durum	İçeride
OtoparkHareketleri.AboneMi	0 (abone değil)
Kasa.OdemeYontemi	Nakit
Kasa.SilindiMi	0
AracYikama.OdemeAlindiMi	0 (ödenmedi)
Tüm tarih alanları (, , vb.)*TarihiGirisSaatiBaslangicSaati	Kayıt anındaki tarih/saat (GETDATE())
⚙️ Kurulum
Gereksinimler
Visual Studio 2019 veya üzeri
.NET [sürüm] SDK / Framework
SQL Server 2019 (Express sürümü yeterlidir) ve SQL Server Management Studio (SSMS)
Adımlar
Depoyu klonlayın
Bash
 git clone https://github.com/Dilarakaymak1/Otopark_Proje.git  CD Otopark_Proje
Veritabanını oluşturun (DACPAC)
SSMS'te Databases üzerine sağ tıklayıp Deploy Data-tier Application... seçeneğini seçin.
Database klasöründeki dosyasını seçin ve veritabanı adını olarak bırakın..dacpacOtopark
Bağlantı dizesini düzenleyin
Projedeki bağlantı ayarını kendi sunucu adınıza göre güncelleyin:
csharp
 Server=.\SQLEXPRESS;Veritabanı=Otopark;Trusted_Connection=Doğru;
Projeyi açın ve çalıştırın
Otopark_Proje.sln dosyasını Visual Studio ile açın.
Başlangıç projesini olarak ayarlayın.Otopark_Proje
F5 ile çalıştırın.
▶️ Kullanım
Uygulamayı başlatın ve kullanıcı adı/şifre ile giriş yapın.
Araç girişinde plaka ve park yeri bilgisini girerek kayıt oluşturun.
Araç çıkışında sistem giriş-çıkış süresine ve geçerli tarifeye göre ücreti hesaplar.
Abone araçlar için abone kaydı oluşturun; abone araçlar hareket kaydında olarak işaretlenir.AboneMi
Yıkama hizmetleri ve gelir-gider işlemleri ilgili ekranlardan kaydedilir.
Personel, kullanıcı ve tarife tanımlarını yönetici yetkisiyle düzenleyin.
🖼 Ekran Görüntüleri

Ekran görüntülerini depoya ekleyip yollarını güncelleyin.

Giriş Ekranı	Ana Ekran
Giriş	Ana Ekran
🤝 Katkıda Bulunma
Depoyu fork'layın
Yeni bir dal oluşturun (git checkout -b ozellik/yeni-ozellik)
Değişikliklerinizi commit'leyin (git commit -m "Yeni özellik eklendi")
Dalınızı push'layın (git push origin ozellik/yeni-ozellik)
Pull Request açın
