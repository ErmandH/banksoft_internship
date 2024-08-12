if exists(select 1 from sys.procedures where name = 'scsp_InsertEmployee')
drop proc scsp_InsertEmployee
go

CREATE PROCEDURE scsp_InsertEmployee
    @EmployeeCode char(20),
    @EmployeeName char(100),
    @EmployeeSurname char(100)
AS
BEGIN
    INSERT INTO ehEmployees (EmployeeCode, EmployeeName, EmployeeSurname)
    VALUES (@EmployeeCode, @EmployeeName, @EmployeeSurname);
END;
GO