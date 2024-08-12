-- query sp creation
if exists(select 1 from sys.procedures where name = 'sp_GetWfTimeReports')
drop proc sp_GetWfTimeReports
go

if exists(select 1 from sys.procedures where name = 'sp_GetWfTimeReportsByWeek')
drop proc sp_GetWfTimeReportsByWeek
go

alter PROC sp_GetWfTimeReportsByWeek AS
BEGIN
	SELECT 
		WFNo,
		ehScrums.Bank,
		ScrumStartDate,
		SUM(Completed) as CompletedSum,
		MIN(Date) as StartDate,
		MAX(Date) as FinishDate
	FROM 
		ehScrums
	INNER JOIN 
		ehEmployees 
	ON 
		ehScrums.Employee = ehEmployees.Code
	INNER JOIN
		ehBanks
	ON ehScrums.Bank = ehBanks.Code
	WHERE WFNo IS NOT NULL AND WFNo=100435
	GROUP BY
		WFNo,
		ScrumStartDate,
		ehScrums.Bank,
	ORDER BY WFNo
END
go




go



CREATE PROC sp_GetWfEmployeeReports AS
BEGIN
	SELECT 
		WFNo,
		TeamCode,
		ehScrums.Bank,
		ehScrums.Employee,
		SUM(Completed) as CompletedSum,
		MIN(Date) as StartDate,
		MAX(Date) as FinishDate
	FROM 
		ehScrums
	INNER JOIN 
		ehEmployees 
	ON 
		ehScrums.Employee = ehEmployees.Code
	INNER JOIN
		ehBanks
	ON ehScrums.Bank = ehBanks.Code
	WHERE WFNo IS NOT NULL
	GROUP BY
		WFNo,
		ehScrums.Bank,
		TeamCode,
		Employee
	ORDER BY WFNo
END
go