if exists(select 1 from sys.procedures where name = 'scsp_UpdateEmployee')
drop proc scsp_UpdateEmployee
go

CREATE PROCEDURE scsp_UpdateEmployee
    @EmployeeID INT,
    @EmployeeCode VARCHAR(20),
    @EmployeeName VARCHAR(100),
    @EmployeeSurname VARCHAR(100)
AS
BEGIN
    UPDATE ehEmployees
    SET EmployeeCode = @EmployeeCode, EmployeeName = @EmployeeName, EmployeeSurname = @EmployeeSurname
    WHERE EmployeeID = @EmployeeID;
END;
GO