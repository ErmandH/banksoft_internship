-- Stored Procedure Oluþturma
-- Oluþturulan SP saklanýyo dbde ve her yerde çaðýrabiliyoruz
create procedure SelectAllEmployees
as
select * from Employees -- buraya SQL komutu yazýlýr
go

exec SelectAllEmployees
go

-- Parametreli oluþturma
CREATE PROCEDURE GetEmployeeByDepartmentId @DepartmentID int -- parametre
AS
SELECT * FROM Employees WHERE DepartmentID = @DepartmentID
GO

exec GetEmployeeByDepartmentId @DepartmentID = 1


-- Birden fazla parametreli procedure oluþturma
CREATE PROCEDURE SelectAllCustomers @City nvarchar(30), @PostalCode nvarchar(10)
AS
SELECT * FROM Customers WHERE City = @City AND PostalCode = @PostalCode
GO

EXEC SelectAllCustomers @City = 'London', @PostalCode = 'WA1 1DP'

