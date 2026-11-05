-- ============================================================
--  OTOPARK SİSTEMİ - SENTETİK VERİ (Her tablo için 10 kayıt)
--  Tarih: 2026-03-12
-- ============================================================

USE [Otopark];
GO

-- ============================================================
-- 1. PERSONELLER (10 kayıt)
-- ============================================================
INSERT INTO Personeller (Ad, Soyad, TCNo, Telefon, Gorevi, IseGirisTarihi, Maas, Durum, Adres) VALUES
(N'Ahmet',    N'Yılmaz',    N'12345678901', N'(532) 100-1001', N'Yönetici',       '2024-01-15', 35000.00, N'Aktif',   N'Atatürk Mah. Cumhuriyet Cad. No:10 İstanbul'),
(N'Mehmet',   N'Kaya',      N'23456789012', N'(542) 200-2002', N'Kasiyer',        '2024-03-01', 22000.00, N'Aktif',   N'Fatih Mah. İnönü Sk. No:5 Ankara'),
(N'Ayşe',     N'Demir',     N'34567890123', N'(553) 300-3003', N'Kasiyer',        '2024-05-10', 22500.00, N'Aktif',   N'Bahçelievler Mah. Gül Sk. No:3 İzmir'),
(N'Fatma',    N'Çelik',     N'45678901234', N'(505) 400-4004', N'Güvenlik',       '2024-02-20', 20000.00, N'Aktif',   N'Kemalpaşa Mah. Barış Cad. No:22 Bursa'),
(N'Hasan',    N'Şahin',     N'56789012345', N'(546) 500-5005', N'Yıkamacı',       '2024-06-01', 19500.00, N'Aktif',   N'Yeni Mah. Yıldız Sk. No:8 Antalya'),
(N'Hüseyin',  N'Öztürk',    N'67890123456', N'(537) 600-6006', N'Park Görevlisi', '2024-04-15', 19000.00, N'Aktif',   N'Zafer Mah. Özgürlük Cad. No:15 Kocaeli'),
(N'Zeynep',   N'Aydın',     N'78901234567', N'(544) 700-7007', N'Muhasebeci',     '2024-07-01', 28000.00, N'Aktif',   N'Merkez Mah. Pınar Sk. No:11 Eskişehir'),
(N'Ali',      N'Arslan',    N'89012345678', N'(555) 800-8008', N'Güvenlik',       '2024-08-10', 20500.00, N'İzinli',  N'Konak Mah. Deniz Cad. No:7 İzmir'),
(N'Emine',    N'Koç',       N'90123456789', N'(538) 900-9009', N'Teknisyen',      '2024-09-01', 23000.00, N'Aktif',   N'Çankaya Mah. Güneş Sk. No:4 Ankara'),
(N'Mustafa',  N'Yıldız',    N'10234567890', N'(541) 110-1100', N'Park Görevlisi', '2025-01-15', 19500.00, N'Ayrıldı', N'Karşıyaka Mah. Akasya Sk. No:9 Bursa');
GO

-- ============================================================
-- 2. KULLANICILAR (10 kayıt) - PersonelId 1-10'a bağlı
-- ============================================================
INSERT INTO Kullanicilar (PersonelId, KullaniciAdi, SifreHash, Rol, AktifMi, Aciklama) VALUES
(1,  N'ahmet.yilmaz',   N'hash_admin_001',    N'Admin',    1, N'Sistem yöneticisi'),
(2,  N'mehmet.kaya',    N'hash_kasiyer_002',   N'Kasiyer',  1, N'Gündüz vardiyası'),
(3,  N'ayse.demir',     N'hash_kasiyer_003',   N'Kasiyer',  1, N'Akşam vardiyası'),
(4,  N'fatma.celik',    N'hash_gozetmen_004',  N'Gözetmen', 1, N'Güvenlik gözetmeni'),
(5,  N'hasan.sahin',    N'hash_kasiyer_005',   N'Kasiyer',  1, N'Yıkama bölümü kasiyeri'),
(6,  N'huseyin.ozturk', N'hash_kasiyer_006',   N'Kasiyer',  1, N'Park bölümü kasiyeri'),
(7,  N'zeynep.aydin',   N'hash_admin_007',     N'Admin',    1, N'Muhasebe yöneticisi'),
(8,  N'ali.arslan',     N'hash_gozetmen_008',  N'Gözetmen', 0, N'İzinli personel'),
(9,  N'emine.koc',      N'hash_kasiyer_009',   N'Kasiyer',  1, N'Teknik destek kasiyeri'),
(10, N'mustafa.yildiz', N'hash_kasiyer_010',   N'Kasiyer',  0, N'Ayrılan personel');
GO

-- ============================================================
-- 3. ABONELER (10 kayıt)
-- ============================================================
INSERT INTO Aboneler (Ad, Soyad, TCNo, Telefon, Plaka, AracMarka, BaslangicTarihi, BitisTarihi, AktifMi) VALUES
(N'Burak',    N'Kara',      N'11122233344', N'(530) 111-2233', N'34 ABC 123', N'Toyota Corolla',    '2026-01-01', '2026-07-01', 1),
(N'Elif',     N'Beyaz',     N'22233344455', N'(541) 222-3344', N'06 DEF 456', N'Honda Civic',       '2026-02-01', '2026-08-01', 1),
(N'Oğuz',     N'Tan',       N'33344455566', N'(552) 333-4455', N'35 GHI 789', N'Hyundai i20',       '2026-01-15', '2026-07-15', 1),
(N'Selin',    N'Güneş',     N'44455566677', N'(543) 444-5566', N'16 JKL 012', N'Renault Clio',      '2026-03-01', '2026-09-01', 1),
(N'Kemal',    N'Doğan',     N'55566677788', N'(534) 555-6677', N'34 MNO 345', N'Ford Focus',        '2025-12-01', '2026-06-01', 1),
(N'Deniz',    N'Uzun',      N'66677788899', N'(545) 666-7788', N'41 PRS 678', N'Volkswagen Golf',   '2026-02-15', '2026-08-15', 1),
(N'Gökhan',   N'Aksu',      N'77788899900', N'(536) 777-8899', N'34 TUV 901', N'Fiat Egea',         '2026-01-10', '2026-07-10', 1),
(N'Canan',    N'Bulut',     N'88899900011', N'(547) 888-9900', N'06 WXY 234', N'Peugeot 208',       '2025-11-01', '2026-05-01', 0),
(N'Serkan',   N'Yıldırım',  N'99900011122', N'(538) 999-0011', N'35 ZAB 567', N'Opel Astra',        '2026-03-01', '2026-09-01', 1),
(N'Melis',    N'Erdem',     N'10011122233', N'(549) 100-1122', N'34 CDE 890', N'Mercedes C180',     '2026-02-01', '2026-08-01', 1);
GO

-- ============================================================
-- 4. TARIFELER (10 kayıt)
-- ============================================================
INSERT INTO Tarifeler (TarifeAdi, Ucret, ParaBirimi, GecerlilikBaslangic, GecerlilikBitis, Aciklama, OlusturanId, AktifMi) VALUES
(N'Saatlik Park',              25.00,  N'TL', '2026-01-01', '2026-12-31', N'İlk saat sabit ücret',                1, 1),
(N'Günlük Park',              150.00,  N'TL', '2026-01-01', '2026-12-31', N'24 saat geçerli günlük tarife',        1, 1),
(N'Aylık Abonelik',          1500.00,  N'TL', '2026-01-01', '2026-12-31', N'30 gün sınırsız park',                 1, 1),
(N'Yıllık Abonelik',        15000.00,  N'TL', '2026-01-01', '2026-12-31', N'365 gün sınırsız park',                1, 1),
(N'Gece Tarifesi',             75.00,  N'TL', '2026-01-01', '2026-12-31', N'22:00 - 08:00 arası özel tarife',      1, 1),
(N'Hafta Sonu Tarifesi',      100.00,  N'TL', '2026-01-01', '2026-12-31', N'Cumartesi-Pazar günleri',              1, 1),
(N'İç Yıkama',                250.00,  N'TL', '2026-01-01', '2026-12-31', N'İç temizlik hizmeti',                  1, 1),
(N'Dış Yıkama',               150.00,  N'TL', '2026-01-01', '2026-12-31', N'Dış yıkama hizmeti',                   1, 1),
(N'İç-Dış Yıkama',            350.00,  N'TL', '2026-01-01', '2026-12-31', N'Komple araç bakım paketi',             1, 1),
(N'VIP Park',                 300.00,  N'TL', '2026-01-01', '2026-12-31', N'Kapalı otopark VIP bölümü',            1, 0);
GO

-- ============================================================
-- 5. OTOPARK HAREKETLERİ (10 kayıt)
-- ============================================================
INSERT INTO OtoparkHareketleri (Plaka, GirisSaati, CikisSaati, Ucret, AboneMi, ParkYeriNo, Durum, IslemYapanPersonelId, IslemTarihi) VALUES
(N'34 ABC 123', '2026-03-12 08:00', '2026-03-12 10:30', 50.00,  1, N'A-01', N'Çıktı',    2, '2026-03-12'),
(N'06 DEF 456', '2026-03-12 09:15', '2026-03-12 14:00', 100.00, 1, N'A-05', N'Çıktı',    2, '2026-03-12'),
(N'35 GHI 789', '2026-03-12 07:30', NULL,                0.00,  1, N'B-03', N'İçeride',  6, '2026-03-12'),
(N'34 XYZ 999', '2026-03-12 10:00', '2026-03-12 12:00', 50.00,  0, N'C-07', N'Çıktı',    2, '2026-03-12'),
(N'41 KLM 555', '2026-03-12 11:30', NULL,                0.00,  0, N'B-10', N'İçeride',  6, '2026-03-12'),
(N'16 JKL 012', '2026-03-12 08:45', '2026-03-12 17:00', 150.00, 1, N'A-08', N'Çıktı',    3, '2026-03-12'),
(N'34 MNO 345', '2026-03-12 06:00', NULL,                0.00,  1, N'A-02', N'İçeride',  6, '2026-03-12'),
(N'06 RST 777', '2026-03-12 13:00', '2026-03-12 15:30', 75.00,  0, N'C-01', N'Çıktı',    3, '2026-03-12'),
(N'35 ZAB 567', '2026-03-12 14:00', NULL,                0.00,  1, N'B-06', N'İçeride',  6, '2026-03-12'),
(N'34 CDE 890', '2026-03-12 09:00', '2026-03-12 09:30', 25.00,  1, N'A-10', N'Çıktı',    2, '2026-03-12');
GO

-- ============================================================
-- 6. ARAÇ YIKAMA (10 kayıt)
-- ============================================================
INSERT INTO AracYikama (Plaka, YikamaTipi, Fiyat, YikamaciPersonel, BaslangicSaati, BitisSaati, Durum, OdemeAlindiMi, Tarih) VALUES
(N'34 ABC 123', N'İç-Dış',  350.00, 5, '2026-03-12 08:30', '2026-03-12 10:00', N'Tamamlandı', 1, '2026-03-12'),
(N'06 DEF 456', N'Dış',     150.00, 5, '2026-03-12 09:00', '2026-03-12 09:45', N'Tamamlandı', 1, '2026-03-12'),
(N'35 GHI 789', N'İç',      250.00, 5, '2026-03-12 10:00', '2026-03-12 11:30', N'Tamamlandı', 1, '2026-03-12'),
(N'34 XYZ 999', N'Dış',     150.00, 5, '2026-03-12 11:00', '2026-03-12 11:40', N'Tamamlandı', 0, '2026-03-12'),
(N'41 KLM 555', N'İç-Dış',  350.00, 5, '2026-03-12 12:00', NULL,               N'Yıkanıyor',  0, '2026-03-12'),
(N'16 JKL 012', N'Dış',     150.00, 5, '2026-03-12 13:00', '2026-03-12 13:40', N'Tamamlandı', 1, '2026-03-12'),
(N'34 MNO 345', N'İç',      250.00, 5, '2026-03-12 14:00', NULL,               N'Bekliyor',   0, '2026-03-12'),
(N'06 RST 777', N'Dış',     150.00, 5, '2026-03-12 14:30', '2026-03-12 15:10', N'Tamamlandı', 1, '2026-03-12'),
(N'35 ZAB 567', N'İç-Dış',  350.00, 5, '2026-03-12 15:00', NULL,               N'Bekliyor',   0, '2026-03-12'),
(N'34 CDE 890', N'Dış',     150.00, 5, '2026-03-12 16:00', '2026-03-12 16:40', N'Tamamlandı', 1, '2026-03-12');
GO

-- ============================================================
-- 7. KASA (10 kayıt)
-- ============================================================
INSERT INTO Kasa (IslemTipi, Tutar, Aciklama, Kategori, IslemTarihi, PersonelId, FisNo, OdemeYontemi) VALUES
(N'Gelir',  50.00,    N'34 ABC 123 plaka park ücreti',        N'Park Ücreti', '2026-03-12 10:30', 2, N'FIS-001', N'Nakit'),
(N'Gelir',  100.00,   N'06 DEF 456 plaka park ücreti',        N'Park Ücreti', '2026-03-12 14:00', 2, N'FIS-002', N'Kredi Kartı'),
(N'Gelir',  350.00,   N'34 ABC 123 İç-Dış yıkama ücreti',     N'Yıkama',      '2026-03-12 10:00', 5, N'FIS-003', N'Nakit'),
(N'Gelir',  150.00,   N'06 DEF 456 Dış yıkama ücreti',        N'Yıkama',      '2026-03-12 09:45', 5, N'FIS-004', N'Nakit'),
(N'Gelir',  1500.00,  N'Burak Kara aylık abonelik ücreti',     N'Abonelik',    '2026-03-01 09:00', 2, N'FIS-005', N'EFT/Havale'),
(N'Gider',  5000.00,  N'Mart ayı elektrik faturası',           N'Diğer',       '2026-03-10 11:00', 7, N'FIS-006', N'EFT/Havale'),
(N'Gider',  22000.00, N'Mehmet Kaya Mart maaşı',              N'Maaş',        '2026-03-01 09:00', 7, N'FIS-007', N'EFT/Havale'),
(N'Gelir',  75.00,    N'06 RST 777 plaka park ücreti',        N'Park Ücreti', '2026-03-12 15:30', 3, N'FIS-008', N'Kredi Kartı'),
(N'Gider',  3500.00,  N'Otopark kira bedeli',                  N'Kira',        '2026-03-01 10:00', 7, N'FIS-009', N'EFT/Havale'),
(N'Gelir',  250.00,   N'35 GHI 789 İç yıkama ücreti',         N'Yıkama',      '2026-03-12 11:30', 5, N'FIS-010', N'Nakit');
GO

-- ============================================================
-- 8. TELEFON REHBERİ (10 kayıt)
-- ============================================================
INSERT INTO TelefonRehberi (Ad, Soyad, KurumAdi, Telefon1, Telefon2, Eposta, Adres, Aciklama) VALUES
(N'Recep',    N'Acar',      N'Belediye İtfaiye',        N'(212) 110-1111', N'(212) 110-1112', N'itfaiye@belediye.gov.tr',    N'Merkez Mah. Belediye Binası İstanbul',  N'Acil durumlar için'),
(N'Gülnur',   N'Ak',        N'Polis İmdat',             N'(155) 000-0000', NULL,              N'polis@emniyet.gov.tr',       N'Emniyet Müdürlüğü',                     N'Güvenlik olayları'),
(N'Serdar',   N'Taş',       N'Elektrik A.Ş.',           N'(444) 186-0000', N'(212) 186-1862', N'destek@elektrik.com.tr',     N'Enerji Mah. Voltaj Sk. No:1 İstanbul',  N'Elektrik kesintisi'),
(N'Derya',    N'Su',        N'Su İdaresi',              N'(444) 185-0000', NULL,              N'musteri@suiski.gov.tr',      N'Su İdaresi Merkez',                     N'Su arızası'),
(N'Emre',     N'Kılıç',     N'Asansör Bakım Ltd.',      N'(532) 444-5566', N'(212) 333-4455', N'bakım@asansor.com.tr',       N'Sanayi Sitesi B Blok No:12',            N'Asansör arıza ve bakım'),
(N'Sevgi',    N'Güler',     N'Temizlik Hizmetleri',     N'(545) 777-8899', NULL,              N'info@temizlik.com.tr',       N'Organize Sanayi Bölgesi',               N'Genel temizlik hizmeti'),
(N'Kadir',    N'Usta',      N'Boya ve Tadilat',         N'(536) 222-3344', NULL,              N'kadir@ustaboya.com',         N'Esnaf Mah. Çarşı Sk. No:5',             N'Tadilat ve boya işleri'),
(N'Nalan',    N'Öz',        N'Sigorta Acentesi',        N'(542) 666-7788', N'(216) 555-6677', N'nalan@sigorta.com.tr',       N'Finans Mah. Banka Cad. No:20',          N'Araç ve bina sigortası'),
(N'Tuncay',   N'Baran',     N'Güvenlik Kamera Sistemleri',N'(538) 888-9900', NULL,            N'info@guvenlik.com.tr',       N'Teknoloji Mah. Yazılım Sk. No:3',       N'Kamera bakım ve kurulum'),
(N'Pınar',    N'Çetin',     NULL,                         N'(549) 111-2233', NULL,            N'pinar@email.com',            N'Sahil Mah. Deniz Cad. No:45 Antalya',   N'Kişisel iletişim');
GO

PRINT N'✅ Tüm tablolara 10''ar sentetik veri başarıyla eklendi.';
GO
