-- ============================================================
--  OTOPARK OTOMASYONSİSTEMİ - ÖRNEK VERİ (SEED DATA)
--  Her tabloya 5 adet gerçekçi veri
--  Tarih: 2026-03-05
-- ============================================================

USE [Otopark];
GO

-- ============================================================
-- 1. PERSONELLER (FK yok, önce girilmeli)
-- ============================================================
SET IDENTITY_INSERT [dbo].[Personeller] OFF;

INSERT INTO [dbo].[Personeller]
    (Ad, Soyad, TCNo, Telefon, Gorevi, IseGirisTarihi, Maas, Durum, Adres)
VALUES
    (N'Ahmet',   N'Yılmaz',  N'12345678901', N'0532 111 22 33', N'Yönetici',        '2022-01-15', 18500.00, N'Aktif',   N'Atatürk Mah. Cumhuriyet Cad. No:12 Kadıköy/İstanbul'),
    (N'Fatma',   N'Kaya',    N'23456789012', N'0541 222 33 44', N'Kasiyer',         '2023-03-01', 12000.00, N'Aktif',   N'Bağcılar Mah. Barbaros Sok. No:5 Bağcılar/İstanbul'),
    (N'Mehmet',  N'Demir',   N'34567890123', N'0555 333 44 55', N'Güvenlik',        '2021-07-20', 13500.00, N'Aktif',   N'Yenibosna Mah. Taşocağı Yolu No:8 Bahçelievler/İstanbul'),
    (N'Ayşe',    N'Çelik',   N'45678901234', N'0533 444 55 66', N'Muhasebeci',      '2023-09-10', 15000.00, N'İzinli',  N'Merkez Mah. İstiklal Cad. No:47 Beşiktaş/İstanbul'),
    (N'Mustafa', N'Şahin',   N'56789012345', N'0546 555 66 77', N'Yıkamacı',        '2024-02-05', 11000.00, N'Aktif',   N'Güneşli Mah. Dereboyu Cad. No:21 Bağcılar/İstanbul');
GO

-- ============================================================
-- 2. KULLANICILAR (PersonelId FK → Personeller)
-- ============================================================
INSERT INTO [dbo].[Kullanicilar]
    (PersonelId, KullaniciAdi, SifreHash, Rol, SonGirisTarihi, HesabiOlusturanId, OlusturulmaTarihi, AktifMi, Aciklama)
VALUES
    (1, N'ahmet.yilmaz', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Admin',    '2026-03-04 09:15:00', NULL, '2022-01-15 08:00:00', 1, N'Sistem yöneticisi'),
    (2, N'fatma.kaya',   N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Kasiyer',  '2026-03-04 08:00:00', 1,    '2023-03-01 09:00:00', 1, N'Gündüz vardiyası kasiyer'),
    (3, N'mehmet.demir', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Gözetmen', '2026-03-03 22:00:00', 1,    '2021-07-20 10:00:00', 1, N'Gece vardiyası güvenlik'),
    (4, N'ayse.celik',   N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Kasiyer',  '2026-02-28 09:30:00', 1,    '2023-09-10 08:30:00', 1, N'İzinde'),
    (5, N'mustafa.sahin',N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Kasiyer',  '2026-03-04 07:45:00', 1,    '2024-02-05 08:00:00', 1, N'Yıkama bölümü sorumlusu');
GO

-- ============================================================
-- 3. ABONELER (FK yok)
-- ============================================================
INSERT INTO [dbo].[Aboneler]
    (Ad, Soyad, TCNo, Telefon, Plaka, AracMarka, BaslangicTarihi, BitisTarihi, AktifMi)
VALUES
    (N'Erkan',   N'Bozkurt',  N'67890123456', N'0532 600 11 22', N'34 ABC 001', N'Toyota Corolla', '2026-01-01', '2026-12-31', 1),
    (N'Selin',   N'Arslan',   N'78901234567', N'0542 700 22 33', N'06 DEF 123', N'Renault Clio',   '2026-02-01', '2026-07-31', 1),
    (N'Hüseyin', N'Koç',      N'89012345678', N'0553 800 33 44', N'35 GHI 456', N'BMW 3 Serisi',   '2025-12-01', '2026-05-31', 1),
    (N'Zeynep',  N'Polat',    N'90123456789', N'0505 900 44 55', N'16 JKL 789', N'Volkswagen Golf','2026-03-01', '2026-08-31', 1),
    (N'Kemal',   N'Erdoğan',  N'01234567890', N'0535 100 55 66', N'41 MNO 321', N'Ford Focus',     '2025-11-01', '2026-04-30', 1);
GO

-- ============================================================
-- 4. TARIFELER (OlusturanId FK → Kullanicilar)
-- ============================================================
INSERT INTO [dbo].[Tarifeler]
    (TarifeAdi, Ucret, ParaBirimi, GecerlilikBaslangic, GecerlilikBitis, Aciklama, OlusturanId, OlusturulmaTarihi, AktifMi)
VALUES
    (N'Saatlik Normal',       25.00, N'TL', '2026-01-01', NULL,         N'Her saat başı 25 TL ücret uygulanır.',        1, '2026-01-01', 1),
    (N'Günlük Maksimum',     150.00, N'TL', '2026-01-01', NULL,         N'24 saat üst limit 150 TL.',                   1, '2026-01-01', 1),
    (N'Aylık Abonelik',      800.00, N'TL', '2026-01-01', NULL,         N'Aylık abonelik paketi, sınırsız giriş-çıkış.',1, '2026-01-01', 1),
    (N'Motosiklet Saatlik',   10.00, N'TL', '2026-01-01', NULL,         N'Motosiklet ve scooter için saatlik tarife.',   1, '2026-01-01', 1),
    (N'Gece Tarifesi',        15.00, N'TL', '2026-01-01', NULL,         N'22:00–08:00 arası geçerli indirimli tarife.',  1, '2026-01-01', 1);
GO

-- ============================================================
-- 5. OTOPARK HAREKETLERİ (IslemYapanPersonelId FK → Personeller)
-- ============================================================
INSERT INTO [dbo].[OtoparkHareketleri]
    (Plaka, GirisSaati, CikisSaati, Ucret, AboneMi, ParkYeriNo, Durum, IslemYapanPersonelId, IslemTarihi)
VALUES
    (N'34 ABC 001', '2026-03-05 07:30:00', '2026-03-05 09:45:00',  55.00, 1, N'A-12', N'Çıktı',    2, '2026-03-05 09:45:00'),
    (N'34 KLM 852', '2026-03-05 08:15:00', '2026-03-05 12:00:00',  95.00, 0, N'B-07', N'Çıktı',    2, '2026-03-05 12:00:00'),
    (N'06 DEF 123', '2026-03-05 09:00:00', NULL,                    NULL,  1, N'A-05', N'Içeride',  2, '2026-03-05 09:00:00'),
    (N'35 RST 741', '2026-03-05 10:30:00', '2026-03-05 11:15:00',  25.00, 0, N'C-03', N'Çıktı',    5, '2026-03-05 11:15:00'),
    (N'41 MNO 321', '2026-03-05 11:00:00', NULL,                    NULL,  1, N'B-11', N'Içeride',  2, '2026-03-05 11:00:00');
GO

-- ============================================================
-- 6. ARAÇ YIKAMA (YikamaciPersonelId FK → Personeller)
-- ============================================================
INSERT INTO [dbo].[AracYikama]
    (Plaka, YikamaTipi, Fiyat, YikamaciPersonelId, BaslangicSaati, BitisSaati, Durum, OdemeAlindiMi, Tarih)
VALUES
    (N'34 KLM 852', N'İç-Dış',  250.00, 5, '2026-03-05 08:30:00', '2026-03-05 09:00:00', N'Tamamlandı', 1, '2026-03-05'),
    (N'34 ABC 001', N'Dış',      120.00, 5, '2026-03-05 09:15:00', '2026-03-05 09:35:00', N'Tamamlandı', 1, '2026-03-05'),
    (N'06 DEF 123', N'İç',       150.00, 5, '2026-03-05 10:00:00', '2026-03-05 10:25:00', N'Tamamlandı', 1, '2026-03-05'),
    (N'35 RST 741', N'Dış',      120.00, 5, '2026-03-05 11:30:00', NULL,                  N'Yıkanıyor',  0, '2026-03-05'),
    (N'41 MNO 321', N'İç-Dış',  250.00, 5, '2026-03-05 12:00:00', NULL,                  N'Bekliyor',   0, '2026-03-05');
GO

-- ============================================================
-- 7. KASA (PersonelId FK → Personeller)
-- ============================================================
INSERT INTO [dbo].[Kasa]
    (IslemTipi, Tutar, Aciklama, Kategori, IslemTarihi, PersonelId, FisNo, OdemeYontemi, SilindiMi)
VALUES
    (N'Gelir', 55.00,   N'34 KLM 852 plakalı araç park ücreti',      N'Park Ücreti',  '2026-03-05 12:00:00', 2, N'F-20260305-001', N'Nakit',        0),
    (N'Gelir', 95.00,   N'34 KLM 852 plakalı araç park ücreti',      N'Park Ücreti',  '2026-03-05 12:01:00', 2, N'F-20260305-002', N'Kredi Kartı',  0),
    (N'Gelir', 250.00,  N'34 KLM 852 plakalı araç yıkama (İç-Dış)',  N'Yıkama',       '2026-03-05 09:00:00', 5, N'F-20260305-003', N'Nakit',        0),
    (N'Gelir', 120.00,  N'34 ABC 001 plakalı araç yıkama (Dış)',     N'Yıkama',       '2026-03-05 09:35:00', 5, N'F-20260305-004', N'Kredi Kartı',  0),
    (N'Gider', 3500.00, N'Mart ayı elektrik faturası ödemesi',        N'Fatura',       '2026-03-05 10:00:00', 1, N'F-20260305-005', N'EFT/Havale',   0);
GO

-- ============================================================
-- 8. TELEFON REHBERİ (FK yok)
-- ============================================================
INSERT INTO [dbo].[TelefonRehberi]
    (Ad, Soyad, KurumAdi, Telefon1, Telefon2, Eposta, Adres, Aciklama, KayitTarihi)
VALUES
    (N'Servis',   NULL,       N'Türk Elektrik Dağıtım A.Ş.',  N'0212 444 00 00', N'0212 444 00 01', N'info@tedas.gov.tr',        N'Esentepe, Ankara',                N'Elektrik arıza ve sayaç bildirimi',        '2026-01-10'),
    (N'Çağrı',    N'Merkezi', N'İGDAŞ Doğalgaz',              N'0212 187 00 00', NULL,              N'musteri@igdas.com.tr',     N'Kağıthane, İstanbul',             N'Doğalgaz arıza ve fatura bildirimi',       '2026-01-10'),
    (N'Kadir',    N'Usta',    N'Kadir Araç Bakım Servisi',    N'0535 222 88 99', N'0212 312 44 55', N'kadir.usta@hotmail.com',   N'Topkapı San. Sit. A Blok Şişli',  N'Forklit ve araç bakım servisi',            '2026-01-15'),
    (N'Emniyet',  NULL,       N'Bağcılar İlçe Emniyet Md.',   N'0212 446 77 00', N'155',            N'bağcilar@emniyet.gov.tr',  N'Bağcılar Merkez, İstanbul',       N'Güvenlik ve acil çağrı hattı',            '2026-01-20'),
    (N'Müdür',    N'Sekreter',N'Bağcılar Belediyesi',         N'0212 441 88 00', N'0212 441 88 01', N'iletisim@bagcilar.bel.tr', N'Bağcılar Merkez Mah. İstanbul',   N'İzin ve ruhsat işlemleri',                '2026-01-20');
GO

-- ============================================================
PRINT N'✅ Tüm tablolara 5 adet gerçekçi örnek veri başarıyla eklendi.';
GO
