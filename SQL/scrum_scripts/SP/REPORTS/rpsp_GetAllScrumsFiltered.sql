if exists(select 1 from sys.procedures where name = 'rpsp_GetAllScrumsFiltered')
	drop proc rpsp_GetAllScrumsFiltered
go

CREATE PROCEDURE rpsp_GetAllScrumsFiltered
	@TarihBasla	char(8) = '',		--YYYYAAGG, Bos: Hepsi
	@TarihBitir	char(8) = '',		--YYYYAAGG, Bos: Hepsi
	@BankaKodu	char(20) = '',		--Bos: Hepsi
	@Ilgili		char(20) = ''		--Bos: Hepsi
AS
BEGIN
    SELECT 
		ScrumID,
        TeamCode 'Takým Kodu',
        Filename 'Dosya Adý',
        ScrumStartDate 'Scrum Haftasý',
        WFNo 'WF No',
        BankCode 'Banka Kodu',
        Subject 'Konu',
        EmployeeCode 'Ýlgili',
        Description 'Açýklama',
        Priority 'Öncelik',
        Status 'Statü',
        Start 'Plan',
        Completed 'Tamamlanan',
        WorkDate 'Ýþ Tarihi',
        InsertDate 'Kayýt Tarihi'
	FROM 
		ehScrums
	WHERE (@TarihBasla = '' or [WorkDate] >= @TarihBasla)
	and (@TarihBitir = '' or [WorkDate] <= @TarihBitir)
	and (@BankaKodu = '' or @BankaKodu = ehScrums.BankCode)
	and (@Ilgili = '' or @Ilgili = ehScrums.EmployeeCode)
END
GO