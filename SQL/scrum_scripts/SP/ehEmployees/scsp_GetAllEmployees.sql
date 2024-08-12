if exists(select 1 from sys.procedures where name = 'scsp_GetAllEmployees')
	drop proc scsp_GetAllEmployees
go

CREATE PROCEDURE scsp_GetAllEmployees
AS
BEGIN
    SELECT *
    FROM ehEmployees
	ORDER BY EmployeeCode
END;
GO