-- ============================================================
--  PERSONELLER TABLOSU - VERİ TEMİZLE VE YENİDEN DOLDUR
--  Türkçe karakter uyumlu 10 kayıt
--  Tarih: 2026-03-05
--  NOT: N'' prefix'i NVARCHAR kolonlarda Türkçe karakterleri
--       doğru kaydeder (ş, ç, ğ, ı, ö, ü, İ, Ş, Ç, Ğ vb.)
-- ============================================================

USE [Otopark];
GO

-- ============================================================
-- 1. Bağlı tablolardaki FK kayıtlarını temizle
-- ============================================================
-- Önce FK'ya bağlı tablolardaki PersonelId referanslarını NULL yap
UPDATE [dbo].[OtoparkHareketleri]  SET [IslemYapanPersonelId] = NULL WHERE [IslemYapanPersonelId] IS NOT NULL;
UPDATE [dbo].[AracYikama]          SET [YikamaciPersonelId]   = NULL WHERE [YikamaciPersonelId]   IS NOT NULL;
UPDATE [dbo].[Kasa]                SET [PersonelId]           = NULL WHERE [PersonelId]           IS NOT NULL;

-- Kullanicilar tablosundaki PersonelId referanslarını NULL yap
UPDATE [dbo].[Kullanicilar] SET [PersonelId] = NULL WHERE [PersonelId] IS NOT NULL;
GO

-- ============================================================
-- 2. Personeller tablosunu temizle
-- ============================================================
DELETE FROM [dbo].[Personeller];

-- IDENTITY sayacını sıfırla (PersonelId 1'den başlasın)
DBCC CHECKIDENT ('[dbo].[Personeller]', RESEED, 0);
GO

-- ============================================================
-- 3. 10 adet Türkçe uyumlu kayıt ekle
--    Sütunlar: Ad, Soyad, TCNo, Telefon, Gorevi,
--              IseGirisTarihi, Maas, Durum, Adres
-- ============================================================
INSERT INTO [dbo].[Personeller]
    ([Ad], [Soyad], [TCNo], [Telefon], [Gorevi], [IseGirisTarihi], [Maas], [Durum], [Adres])
VALUES
    (
        N'Ahmet',
        N'Yılmaz',
        N'12345678901',
        N'05321112233',
        N'Yönetici',
        '2021-03-15',
        22500.00,
        N'Aktif',
        N'Atatürk Mah. Cumhuriyet Cad. No:12 Kadıköy/İstanbul'
    ),
    (
        N'Fatma',
        N'Şahin',
        N'23456789012',
        N'05412223344',
        N'Kasiyer',
        '2022-06-01',
        14000.00,
        N'Aktif',
        N'Bağcılar Mah. Barbaros Sok. No:5 Bağcılar/İstanbul'
    ),
    (
        N'Mehmet',
        N'Çelik',
        N'34567890123',
        N'05553334455',
        N'Güvenlik Görevlisi',
        '2020-11-20',
        15500.00,
        N'Aktif',
        N'Yenibosna Mah. Taşocağı Yolu No:8 Bahçelievler/İstanbul'
    ),
    (
        N'Ayşe',
        N'Kaya',
        N'45678901234',
        N'05334445566',
        N'Muhasebeci',
        '2023-01-10',
        17000.00,
        N'İzinli',
        N'Merkez Mah. İstiklal Cad. No:47 Beşiktaş/İstanbul'
    ),
    (
        N'Mustafa',
        N'Demir',
        N'56789012345',
        N'05465556677',
        N'Yıkamacı',
        '2024-02-05',
        12500.00,
        N'Aktif',
        N'Güneşli Mah. Dereboyu Cad. No:21 Bağcılar/İstanbul'
    ),
    (
        N'Zeynep',
        N'Öztürk',
        N'67890123456',
        N'05326667788',
        N'Kasiyer',
        '2023-08-15',
        13500.00,
        N'Aktif',
        N'Çamlıca Mah. Gül Sk. No:3 Üsküdar/İstanbul'
    ),
    (
        N'İbrahim',
        N'Arslan',
        N'78901234567',
        N'05427778899',
        N'Güvenlik Görevlisi',
        '2022-04-01',
        15000.00,
        N'Aktif',
        N'Karşıyaka Mah. Atatürk Blv. No:55 Nilüfer/Bursa'
    ),
    (
        N'Emine',
        N'Koç',
        N'89012345678',
        N'05538889900',
        N'Temizlik Görevlisi',
        '2023-05-20',
        11000.00,
        N'Aktif',
        N'Şirintepe Mah. Çınar Cad. No:9 Esenler/İstanbul'
    ),
    (
        N'Hüseyin',
        N'Güngör',
        N'90123456789',
        N'05059990011',
        N'Yıkamacı',
        '2021-09-12',
        12000.00,
        N'Ayrıldı',
        N'Sultançiftliği Mah. Mimar Sinan Cad. No:17 Gaziosmanpaşa/İstanbul'
    ),
    (
        N'Gülşen',
        N'Erdoğan',
        N'01234567891',
        N'05351001122',
        N'Kasiyer',
        '2024-07-01',
        13000.00,
        N'Aktif',
        N'Bahçeşehir Mah. Gölet Sk. No:6 Başakşehir/İstanbul'
    );
GO

-- ============================================================
-- KONTROL: Eklenen kayıtları görüntüle
-- ============================================================
SELECT
    [PersonelId],
    [Ad],
    [Soyad],
    [TCNo],
    [Telefon],
    [Gorevi],
    CONVERT(VARCHAR(10), [IseGirisTarihi], 104) AS [IseGirisTarihi],
    [Maas],
    [Durum],
    [Adres]
FROM [dbo].[Personeller]
ORDER BY [PersonelId];
GO

PRINT N'✅ Personeller tablosu temizlendi ve 10 adet Türkçe uyumlu kayıt başarıyla eklendi.';
GO
