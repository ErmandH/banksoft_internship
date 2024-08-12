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
        TeamCode 'Tak�m Kodu',
        Filename 'Dosya Ad�',
        ScrumStartDate 'Scrum Haftas�',
        WFNo 'WF No',
        BankCode 'Banka Kodu',
        Subject 'Konu',
        EmployeeCode '�lgili',
        Description 'A��klama',
        Priority '�ncelik',
        Status 'Stat�',
        Start 'Plan',
        Completed 'Tamamlanan',
        WorkDate '�� Tarihi',
        InsertDate 'Kay�t Tarihi'
	FROM 
		ehScrums
	WHERE (@TarihBasla = '' or [WorkDate] >= @TarihBasla)
	and (@TarihBitir = '' or [WorkDate] <= @TarihBitir)
	and (@BankaKodu = '' or @BankaKodu = ehScrums.BankCode)
	and (@Ilgili = '' or @Ilgili = ehScrums.EmployeeCode)
END
GO