if exists (select 1 from sysobjects where name = 'rpsp_GetWfEmployeeTimeReports')
	drop proc rpsp_GetWfEmployeeTimeReports
go
create PROC rpsp_GetWfEmployeeTimeReports
	@TarihBasla	char(8) = '',		--YYYYAAGG, Bos: Hepsi
	@TarihBitir	char(8) = '',		--YYYYAAGG, Bos: Hepsi
	@BankaKodu	char(20) = '',		--Bos: Hepsi
	@Ilgili		char(20) = ''		--Bos: Hepsi
AS
BEGIN
	SELECT 
		WFNo 'WF No',
		ehScrums.EmployeeCode 'Ýlgili',
		SUM(Completed) 'Tamamlanan',
		MIN([WorkDate]) 'Baþlangýç Tarihi',
		MAX([WorkDate]) 'Bitiþ Tarihi'
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
		ehScrums.EmployeeCode
	ORDER BY ehScrums.EmployeeCode,MIN([WorkDate])
END
go


