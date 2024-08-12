if exists (select 1 from sysobjects where name = 'rpsp_GetWfTimeReports')
	drop proc rpsp_GetWfTimeReports
go
create PROC rpsp_GetWfTimeReports 
	@TarihBasla	char(8) = '',		--YYYYAAGG, Bos: Hepsi
	@TarihBitir	char(8) = '',		--YYYYAAGG, Bos: Hepsi
	@BankaKodu	char(20) = '',		--Bos: Hepsi
	@Ilgili		char(20) = ''		--Bos: Hepsi
AS
BEGIN
	SELECT 
		WFNo 'WF No',
		ehScrums.BankCode '�lgili',
		SUM(Completed) 'Tamamlanan',
		MIN([WorkDate]) 'Ba�lang�� Tarihi',
		MAX([WorkDate]) 'Biti� Tarihi'
	FROM 
		ehScrums
	INNER JOIN ehEmployees ON 
			ehScrums.EmployeeCode = ehEmployees.EmployeeCode
	INNER JOIN 	ehBanks ON ehScrums.BankCode = ehBanks.BankCode

	WHERE WFNo IS NOT NULL

	and (@TarihBasla = '' or [WorkDate] >= @TarihBasla)
	and (@TarihBitir = '' or [WorkDate] <= @TarihBitir)
	and (@BankaKodu = '' or @BankaKodu = ehScrums.BankCode)
	and (@Ilgili = '' or @Ilgili = ehScrums.EmployeeCode)

	GROUP BY
		WFNo,
		ehScrums.BankCode
	ORDER BY WFNo
END
go