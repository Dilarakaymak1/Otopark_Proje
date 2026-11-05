-- ============================================================
--  OTOPARK OTOMASYONSystem - VERİTABANI TABLO OLUŞTURMA
--  Veritabanı: Otopark (SQL Server LocalDB)
--  Tarih: 2026-03-05
-- ============================================================

USE [Otopark];
GO

-- ============================================================
-- 1. PERSONELLER TABLOSU
--    (Diğer tablolar PersonelId'ye FK ile bağlanacağından önce oluşturulmalı)
-- ============================================================
CREATE TABLE [dbo].[Personeller]
(
    [PersonelId]        INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Ad]                NVARCHAR(50)   NOT NULL,
    [Soyad]             NVARCHAR(50)   NOT NULL,
    [TCNo]              NCHAR(11)      NOT NULL UNIQUE,
    [Telefon]           NVARCHAR(15)   NULL,
    [Gorevi]            NVARCHAR(50)   NOT NULL,         -- (Kasiyer, Güvenlik, Yıkamacı vb.)
    [IseGirisTarihi]    DATETIME       NOT NULL DEFAULT GETDATE(),
    [Maas]              DECIMAL(10,2)  NOT NULL DEFAULT 0.00,
    [Durum]             NVARCHAR(20)   NOT NULL DEFAULT N'Aktif',  -- (Aktif, İzinli, Ayrıldı)
    [Adres]             NVARCHAR(250)  NULL
);
GO

-- ============================================================
-- 2. KULLANICILAR TABLOSU
--    (Giriş ekranı ve kullanıcı yönetimi)
-- ============================================================
CREATE TABLE [dbo].[Kullanicilar]
(
    [KullaniciId]           INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [PersonelId]            INT            NULL REFERENCES [dbo].[Personeller]([PersonelId]),
    [KullaniciAdi]          NVARCHAR(50)   NOT NULL UNIQUE,
    [SifreHash]             NVARCHAR(256)  NOT NULL,
    [Rol]                   NVARCHAR(30)   NOT NULL DEFAULT N'Kasiyer',  -- (Admin, Kasiyer, Gözetmen)
    [SonGirisTarihi]        DATETIME       NULL,
    [HesabiOlusturanId]     INT            NULL,         -- KullaniciId referansı (self-ref)
    [OlusturulmaTarihi]     DATETIME       NOT NULL DEFAULT GETDATE(),
    [AktifMi]               BIT            NOT NULL DEFAULT 1,
    [Aciklama]              NVARCHAR(200)  NULL
);
GO

-- ============================================================
-- 3. ABONELER TABLOSU
--    (Müşteri ve Abone Menüsü)
-- ============================================================
CREATE TABLE [dbo].[Aboneler]
(
    [AboneId]           INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Ad]                NVARCHAR(50)   NOT NULL,
    [Soyad]             NVARCHAR(50)   NOT NULL,
    [TCNo]              NCHAR(11)      NULL,
    [Telefon]           NVARCHAR(15)   NULL,
    [Plaka]             NVARCHAR(15)   NOT NULL,
    [AracMarka]         NVARCHAR(50)   NULL,
    [BaslangicTarihi]   DATETIME       NOT NULL DEFAULT GETDATE(),
    [BitisTarihi]       DATETIME       NOT NULL,
    [AktifMi]           BIT            NOT NULL DEFAULT 1
);
GO

-- ============================================================
-- 4. TARIFELER TABLOSU
--    (Sistem ve Tarifeler Menüsü)
-- ============================================================
CREATE TABLE [dbo].[Tarifeler]
(
    [TarifeId]               INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [TarifeAdi]              NVARCHAR(100)  NOT NULL,
    [Ucret]                  DECIMAL(10,2)  NOT NULL DEFAULT 0.00,
    [ParaBirimi]             NVARCHAR(5)    NOT NULL DEFAULT N'TL',
    [GecerlilikBaslangic]    DATETIME       NOT NULL DEFAULT GETDATE(),
    [GecerlilikBitis]        DATETIME       NULL,
    [Aciklama]               NVARCHAR(250)  NULL,
    [OlusturanId]            INT            NULL REFERENCES [dbo].[Kullanicilar]([KullaniciId]),
    [OlusturulmaTarihi]      DATETIME       NOT NULL DEFAULT GETDATE(),
    [AktifMi]                BIT            NOT NULL DEFAULT 1
);
GO

-- ============================================================
-- 5. OTOPARK HAREKETLERİ TABLOSU
--    (Ana Ekran / Otopark Menüsü - Giriş/Çıkış işlemleri)
-- ============================================================
CREATE TABLE [dbo].[OtoparkHareketleri]
(
    [IslemId]               INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Plaka]                 NVARCHAR(15)   NOT NULL,
    [GirisSaati]            DATETIME       NOT NULL DEFAULT GETDATE(),
    [CikisSaati]            DATETIME       NULL,
    [Ucret]                 DECIMAL(10,2)  NULL DEFAULT 0.00,
    [AboneMi]               BIT            NOT NULL DEFAULT 0,
    [ParkYeriNo]            NVARCHAR(10)   NULL,
    [Durum]                 NVARCHAR(20)   NOT NULL DEFAULT N'Içeride',  -- (İçeride, Çıktı, İptal)
    [IslemYapanPersonelId]  INT            NULL REFERENCES [dbo].[Personeller]([PersonelId]),
    [IslemTarihi]           DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- 6. ARAÇ YIKAMA TABLOSU
--    (Araç Yıkama Menüsü)
-- ============================================================
CREATE TABLE [dbo].[AracYikama]
(
    [YikamaId]              INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Plaka]                 NVARCHAR(15)   NOT NULL,
    [YikamaTipi]            NVARCHAR(20)   NOT NULL DEFAULT N'Dış',     -- (İç, Dış, İç-Dış)
    [Fiyat]                 DECIMAL(10,2)  NOT NULL DEFAULT 0.00,
    [YikamaciPersonelId]    INT            NULL REFERENCES [dbo].[Personeller]([PersonelId]),
    [BaslangicSaati]        DATETIME       NOT NULL DEFAULT GETDATE(),
    [BitisSaati]            DATETIME       NULL,
    [Durum]                 NVARCHAR(20)   NOT NULL DEFAULT N'Bekliyor', -- (Bekliyor, Yıkanıyor, Tamamlandı, İptal)
    [OdemeAlindiMi]         BIT            NOT NULL DEFAULT 0,
    [Tarih]                 DATE           NOT NULL DEFAULT CAST(GETDATE() AS DATE)
);
GO

-- ============================================================
-- 7. KASA TABLOSU
--    (Finans ve Kasa Menüsü)
-- ============================================================
CREATE TABLE [dbo].[Kasa]
(
    [KasaId]            INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [IslemTipi]         NVARCHAR(10)   NOT NULL,               -- (Gelir, Gider)
    [Tutar]             DECIMAL(10,2)  NOT NULL DEFAULT 0.00,
    [Aciklama]          NVARCHAR(300)  NULL,
    [Kategori]          NVARCHAR(50)   NULL,                   -- (Park Ücreti, Abonelik, Yıkama, Kira, Maaş vb.)
    [IslemTarihi]       DATETIME       NOT NULL DEFAULT GETDATE(),
    [PersonelId]        INT            NULL REFERENCES [dbo].[Personeller]([PersonelId]),
    [FisNo]             NVARCHAR(20)   NULL,
    [OdemeYontemi]      NVARCHAR(20)   NOT NULL DEFAULT N'Nakit',  -- (Nakit, Kredi Kartı, EFT/Havale)
    [SilindiMi]         BIT            NOT NULL DEFAULT 0
);
GO

-- ============================================================
-- 8. TELEFON REHBERİ TABLOSU
--    (Araçlar / Telefon Rehberi Menüsü)
-- ============================================================
CREATE TABLE [dbo].[TelefonRehberi]
(
    [KisiId]        INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Ad]            NVARCHAR(50)   NULL,
    [Soyad]         NVARCHAR(50)   NULL,
    [KurumAdi]      NVARCHAR(100)  NULL,
    [Telefon1]      NVARCHAR(15)   NOT NULL,
    [Telefon2]      NVARCHAR(15)   NULL,
    [Eposta]        NVARCHAR(100)  NULL,
    [Adres]         NVARCHAR(250)  NULL,
    [Aciklama]      NVARCHAR(300)  NULL,
    [KayitTarihi]   DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- BAŞARILI MESAJI
-- ============================================================
PRINT N'✅ Tüm tablolar başarıyla oluşturuldu.';
GO
