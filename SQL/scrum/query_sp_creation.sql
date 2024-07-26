-- query sp creation

CREATE PROC sp_GetWfTimeReport AS
BEGIN
	SELECT 
		WFNo,
		ScrumStartDate,
		TeamCode,
		ehScrums.Bank,
		SUM(Completed),
		MIN(Date),
		MAX(Date)
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
		ScrumStartDate,
		ehScrums.Bank,
		TeamCode
	ORDER BY WFNo
END
go