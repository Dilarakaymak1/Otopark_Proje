Add-Type -AssemblyName System.Data

$connStr = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Otopark;Integrated Security=True;"
$conn = New-Object System.Data.SqlClient.SqlConnection($connStr)
$conn.Open()

function Exec([string]$sql, [hashtable]$prms = @{}) {
    $cmd = $conn.CreateCommand()
    $cmd.CommandText = $sql
    foreach ($k in $prms.Keys) {
        $v = $prms[$k]
        if ($v -eq $null) { $v = [DBNull]::Value }
        $cmd.Parameters.AddWithValue($k, $v) | Out-Null
    }
    $cmd.ExecuteNonQuery() | Out-Null
}

Write-Host "Temizleniyor..." -ForegroundColor Yellow
Exec "DELETE FROM [Kasa]"
Exec "DELETE FROM [AracYikama]"
Exec "DELETE FROM [OtoparkHareketleri]"
Exec "DELETE FROM [Tarifeler]"
Exec "DELETE FROM [Kullanicilar]"
Exec "DELETE FROM [Aboneler]"
Exec "DELETE FROM [TelefonRehberi]"
Exec "DELETE FROM [Personeller]"
Exec "DBCC CHECKIDENT ('Personeller',       RESEED, 0)"
Exec "DBCC CHECKIDENT ('Kullanicilar',      RESEED, 0)"
Exec "DBCC CHECKIDENT ('Aboneler',          RESEED, 0)"
Exec "DBCC CHECKIDENT ('Tarifeler',         RESEED, 0)"
Exec "DBCC CHECKIDENT ('OtoparkHareketleri',RESEED, 0)"
Exec "DBCC CHECKIDENT ('AracYikama',        RESEED, 0)"
Exec "DBCC CHECKIDENT ('Kasa',              RESEED, 0)"
Exec "DBCC CHECKIDENT ('TelefonRehberi',    RESEED, 0)"
Write-Host "Temizlendi." -ForegroundColor Green

# 1. PERSONELLER
$sqlP = "INSERT INTO Personeller (Ad,Soyad,TCNo,Telefon,Gorevi,IseGirisTarihi,Maas,Durum,Adres) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i)"
Exec $sqlP @{"@a" = "Ahmet"; "@b" = "Yilmaz"; "@c" = "12345678901"; "@d" = "05321112233"; "@e" = "Yonetici"; "@f" = [datetime]"2022-01-15"; "@g" = 18500.00; "@h" = "Aktif"; "@i" = "Ataturk Mah. No:12 Kadikoy Istanbul" }
Exec $sqlP @{"@a" = "Fatma"; "@b" = "Kaya"; "@c" = "23456789012"; "@d" = "05412223344"; "@e" = "Kasiyer"; "@f" = [datetime]"2023-03-01"; "@g" = 12000.00; "@h" = "Aktif"; "@i" = "Bagcilar Mah. No:5 Bagcilar Istanbul" }
Exec $sqlP @{"@a" = "Mehmet"; "@b" = "Demir"; "@c" = "34567890123"; "@d" = "05553334455"; "@e" = "Guvenlik"; "@f" = [datetime]"2021-07-20"; "@g" = 13500.00; "@h" = "Aktif"; "@i" = "Yenibosna Mah. No:8 Bahcelievler Istanbul" }
Exec $sqlP @{"@a" = "Ayse"; "@b" = "Celik"; "@c" = "45678901234"; "@d" = "05334445566"; "@e" = "Muhasebeci"; "@f" = [datetime]"2023-09-10"; "@g" = 15000.00; "@h" = "Izinli"; "@i" = "Istiklal Cad. No:47 Besiktas Istanbul" }
Exec $sqlP @{"@a" = "Mustafa"; "@b" = "Sahin"; "@c" = "56789012345"; "@d" = "05465556677"; "@e" = "Yikamaci"; "@f" = [datetime]"2024-02-05"; "@g" = 11000.00; "@h" = "Aktif"; "@i" = "Gunesli Mah. No:21 Bagcilar Istanbul" }
Write-Host "  Personeller OK" -ForegroundColor Cyan

# 2. KULLANICILAR
$hash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92"
$sqlK = "INSERT INTO Kullanicilar (PersonelId,KullaniciAdi,SifreHash,Rol,SonGirisTarihi,HesabiOlusturanId,OlusturulmaTarihi,AktifMi,Aciklama) VALUES (@a,@b,@c,@d,@e,@f,@g,1,@h)"
Exec $sqlK @{"@a" = 1; "@b" = "ahmet.yilmaz"; "@c" = $hash; "@d" = "Admin"; "@e" = [datetime]"2026-03-04 09:15"; "@f" = [DBNull]::Value; "@g" = [datetime]"2022-01-15"; "@h" = "Sistem yoneticisi" }
Exec $sqlK @{"@a" = 2; "@b" = "fatma.kaya"; "@c" = $hash; "@d" = "Kasiyer"; "@e" = [datetime]"2026-03-04 08:00"; "@f" = 1; "@g" = [datetime]"2023-03-01"; "@h" = "Gunduz vardiyasi" }
Exec $sqlK @{"@a" = 3; "@b" = "mehmet.demir"; "@c" = $hash; "@d" = "Gozetmen"; "@e" = [datetime]"2026-03-03 22:00"; "@f" = 1; "@g" = [datetime]"2021-07-20"; "@h" = "Gece guvenlik" }
Exec $sqlK @{"@a" = 4; "@b" = "ayse.celik"; "@c" = $hash; "@d" = "Kasiyer"; "@e" = [datetime]"2026-02-28 09:30"; "@f" = 1; "@g" = [datetime]"2023-09-10"; "@h" = "Izinde" }
Exec $sqlK @{"@a" = 5; "@b" = "mustafa.sahin"; "@c" = $hash; "@d" = "Kasiyer"; "@e" = [datetime]"2026-03-04 07:45"; "@f" = 1; "@g" = [datetime]"2024-02-05"; "@h" = "Yikama sorumlusu" }
Write-Host "  Kullanicilar OK" -ForegroundColor Cyan

# 3. ABONELER
$sqlA = "INSERT INTO Aboneler (Ad,Soyad,TCNo,Telefon,Plaka,AracMarka,BaslangicTarihi,BitisTarihi,AktifMi) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,1)"
Exec $sqlA @{"@a" = "Erkan"; "@b" = "Bozkurt"; "@c" = "67890123456"; "@d" = "05326001122"; "@e" = "34ABC001"; "@f" = "Toyota Corolla"; "@g" = [datetime]"2026-01-01"; "@h" = [datetime]"2026-12-31" }
Exec $sqlA @{"@a" = "Selin"; "@b" = "Arslan"; "@c" = "78901234567"; "@d" = "05427002233"; "@e" = "06DEF123"; "@f" = "Renault Clio"; "@g" = [datetime]"2026-02-01"; "@h" = [datetime]"2026-07-31" }
Exec $sqlA @{"@a" = "Huseyin"; "@b" = "Koc"; "@c" = "89012345678"; "@d" = "05538003344"; "@e" = "35GHI456"; "@f" = "BMW 3 Serisi"; "@g" = [datetime]"2025-12-01"; "@h" = [datetime]"2026-05-31" }
Exec $sqlA @{"@a" = "Zeynep"; "@b" = "Polat"; "@c" = "90123456789"; "@d" = "05059004455"; "@e" = "16JKL789"; "@f" = "Volkswagen Golf"; "@g" = [datetime]"2026-03-01"; "@h" = [datetime]"2026-08-31" }
Exec $sqlA @{"@a" = "Kemal"; "@b" = "Erdogan"; "@c" = "01234567890"; "@d" = "05351005566"; "@e" = "41MNO321"; "@f" = "Ford Focus"; "@g" = [datetime]"2025-11-01"; "@h" = [datetime]"2026-04-30" }
Write-Host "  Aboneler OK" -ForegroundColor Cyan

# 4. TARIFELER
$sqlT = "INSERT INTO Tarifeler (TarifeAdi,Ucret,ParaBirimi,GecerlilikBaslangic,Aciklama,OlusturanId,OlusturulmaTarihi,AktifMi) VALUES (@a,@b,'TL','2026-01-01',@c,1,'2026-01-01',1)"
Exec $sqlT @{"@a" = "Saatlik Normal"; "@b" = 25.00; "@c" = "Her saat basi 25 TL" }
Exec $sqlT @{"@a" = "Gunluk Maksimum"; "@b" = 150.00; "@c" = "24 saat ust limit 150 TL" }
Exec $sqlT @{"@a" = "Aylik Abonelik"; "@b" = 800.00; "@c" = "Aylik paket sinırsız giris-cikis" }
Exec $sqlT @{"@a" = "Motosiklet Saatlik"; "@b" = 10.00; "@c" = "Motosiklet icin saatlik tarife" }
Exec $sqlT @{"@a" = "Gece Tarifesi"; "@b" = 15.00; "@c" = "22:00-08:00 arasi indirimli tarife" }
Write-Host "  Tarifeler OK" -ForegroundColor Cyan

# 5. OTOPARK HAREKETLERI
$sqlOH = "INSERT INTO OtoparkHareketleri (Plaka,GirisSaati,CikisSaati,Ucret,AboneMi,ParkYeriNo,Durum,IslemYapanPersonelId,IslemTarihi) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,GETDATE())"
Exec $sqlOH @{"@a" = "34ABC001"; "@b" = [datetime]"2026-03-05 07:30"; "@c" = [datetime]"2026-03-05 09:45"; "@d" = 55.00; "@e" = 1; "@f" = "A-12"; "@g" = "Cikti"; "@h" = 2 }
Exec $sqlOH @{"@a" = "34KLM852"; "@b" = [datetime]"2026-03-05 08:15"; "@c" = [datetime]"2026-03-05 12:00"; "@d" = 95.00; "@e" = 0; "@f" = "B-07"; "@g" = "Cikti"; "@h" = 2 }
Exec $sqlOH @{"@a" = "06DEF123"; "@b" = [datetime]"2026-03-05 09:00"; "@c" = [DBNull]::Value; "@d" = [DBNull]::Value; "@e" = 1; "@f" = "A-05"; "@g" = "Iceride"; "@h" = 2 }
Exec $sqlOH @{"@a" = "35RST741"; "@b" = [datetime]"2026-03-05 10:30"; "@c" = [datetime]"2026-03-05 11:15"; "@d" = 25.00; "@e" = 0; "@f" = "C-03"; "@g" = "Cikti"; "@h" = 5 }
Exec $sqlOH @{"@a" = "41MNO321"; "@b" = [datetime]"2026-03-05 11:00"; "@c" = [DBNull]::Value; "@d" = [DBNull]::Value; "@e" = 1; "@f" = "B-11"; "@g" = "Iceride"; "@h" = 2 }
Write-Host "  OtoparkHareketleri OK" -ForegroundColor Cyan

# 6. ARAC YIKAMA
$sqlY = "INSERT INTO AracYikama (Plaka,YikamaTipi,Fiyat,YikamaciPersonelId,BaslangicSaati,BitisSaati,Durum,OdemeAlindiMi,Tarih) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,'2026-03-05')"
Exec $sqlY @{"@a" = "34KLM852"; "@b" = "Ic-Dis"; "@c" = 250.00; "@d" = 5; "@e" = [datetime]"2026-03-05 08:30"; "@f" = [datetime]"2026-03-05 09:00"; "@g" = "Tamamlandi"; "@h" = 1 }
Exec $sqlY @{"@a" = "34ABC001"; "@b" = "Dis"; "@c" = 120.00; "@d" = 5; "@e" = [datetime]"2026-03-05 09:15"; "@f" = [datetime]"2026-03-05 09:35"; "@g" = "Tamamlandi"; "@h" = 1 }
Exec $sqlY @{"@a" = "06DEF123"; "@b" = "Ic"; "@c" = 150.00; "@d" = 5; "@e" = [datetime]"2026-03-05 10:00"; "@f" = [datetime]"2026-03-05 10:25"; "@g" = "Tamamlandi"; "@h" = 1 }
Exec $sqlY @{"@a" = "35RST741"; "@b" = "Dis"; "@c" = 120.00; "@d" = 5; "@e" = [datetime]"2026-03-05 11:30"; "@f" = [DBNull]::Value; "@g" = "Yikanıyor"; "@h" = 0 }
Exec $sqlY @{"@a" = "41MNO321"; "@b" = "Ic-Dis"; "@c" = 250.00; "@d" = 5; "@e" = [datetime]"2026-03-05 12:00"; "@f" = [DBNull]::Value; "@g" = "Bekliyor"; "@h" = 0 }
Write-Host "  AracYikama OK" -ForegroundColor Cyan

# 7. KASA
$sqlKs = "INSERT INTO Kasa (IslemTipi,Tutar,Aciklama,Kategori,IslemTarihi,PersonelId,FisNo,OdemeYontemi,SilindiMi) VALUES (@a,@b,@c,@d,GETDATE(),@e,@f,@g,0)"
Exec $sqlKs @{"@a" = "Gelir"; "@b" = 55.00; "@c" = "34KLM852 park ucreti"; "@d" = "Park Ucreti"; "@e" = 2; "@f" = "F-001"; "@g" = "Nakit" }
Exec $sqlKs @{"@a" = "Gelir"; "@b" = 95.00; "@c" = "34KLM852 park ucreti"; "@d" = "Park Ucreti"; "@e" = 2; "@f" = "F-002"; "@g" = "Kredi Karti" }
Exec $sqlKs @{"@a" = "Gelir"; "@b" = 250.00; "@c" = "34KLM852 yikama Ic-Dis"; "@d" = "Yikama"; "@e" = 5; "@f" = "F-003"; "@g" = "Nakit" }
Exec $sqlKs @{"@a" = "Gelir"; "@b" = 120.00; "@c" = "34ABC001 yikama Dis"; "@d" = "Yikama"; "@e" = 5; "@f" = "F-004"; "@g" = "Kredi Karti" }
Exec $sqlKs @{"@a" = "Gider"; "@b" = 3500.00; "@c" = "Mart ayi elektrik faturasi"; "@d" = "Fatura"; "@e" = 1; "@f" = "F-005"; "@g" = "EFT/Havale" }
Write-Host "  Kasa OK" -ForegroundColor Cyan

# 8. TELEFON REHBERI
$sqlR = "INSERT INTO TelefonRehberi (Ad,Soyad,KurumAdi,Telefon1,Telefon2,Eposta,Adres,Aciklama,KayitTarihi) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,'2026-01-10')"
Exec $sqlR @{"@a" = "Servis"; "@b" = [DBNull]::Value; "@c" = "Turk Elektrik Dagitim AS"; "@d" = "02124440000"; "@e" = "02124440001"; "@f" = "info@tedas.gov.tr"; "@g" = "Esentepe Ankara"; "@h" = "Elektrik ariza bildirimi" }
Exec $sqlR @{"@a" = "Musteri"; "@b" = "Hizm."; "@c" = "IGDAS Dogalgaz"; "@d" = "02121870000"; "@e" = [DBNull]::Value; "@f" = "musteri@igdas.com.tr"; "@g" = "Kagithane Istanbul"; "@h" = "Dogalgaz ariza bildirimi" }
Exec $sqlR @{"@a" = "Kadir"; "@b" = "Usta"; "@c" = "Kadir Arac Bakim"; "@d" = "05352228899"; "@e" = "02123124455"; "@f" = "kadir.usta@hotmail.com"; "@g" = "Topcapi San. Sit. A Blok"; "@h" = "Forklift ve arac bakim" }
Exec $sqlR @{"@a" = "Emniyet"; "@b" = [DBNull]::Value; "@c" = "Bagcilar Ilce Emniyet Md."; "@d" = "02124467700"; "@e" = "00000000155"; "@f" = "bagcilar@emniyet.gov.tr"; "@g" = "Bagcilar Merkez Istanbul"; "@h" = "Guvenlik acil cagri" }
Exec $sqlR @{"@a" = "Mudur"; "@b" = "Sekreteri"; "@c" = "Bagcilar Belediyesi"; "@d" = "02124418800"; "@e" = "02124418801"; "@f" = "iletisim@bagcilar.bel.tr"; "@g" = "Bagcilar Merkez Istanbul"; "@h" = "Izin ve ruhsat islemleri" }
Write-Host "  TelefonRehberi OK" -ForegroundColor Cyan

$conn.Close()
Write-Host ""
Write-Host "TAMAMLANDI! Tum tablolara 5 kayit eklendi." -ForegroundColor Green
