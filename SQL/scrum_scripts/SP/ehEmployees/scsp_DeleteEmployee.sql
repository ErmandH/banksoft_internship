if exists(select 1 from sys.procedures where name = 'scsp_DeleteEmployee')
	drop proc scsp_DeleteEmployee
go

CREATE PROCEDURE scsp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    DELETE FROM ehEmployees
    WHERE EmployeeID = @EmployeeID;
END;
GO
