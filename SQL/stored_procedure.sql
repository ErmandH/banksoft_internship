-- Stored Procedure Olu�turma
-- Olu�turulan SP saklan�yo dbde ve her yerde �a��rabiliyoruz
create procedure SelectAllEmployees
as
select * from Employees -- buraya SQL komutu yaz�l�r
go

exec SelectAllEmployees
go

-- Parametreli olu�turma
CREATE PROCEDURE GetEmployeeByDepartmentId @DepartmentID int -- parametre
AS
SELECT * FROM Employees WHERE DepartmentID = @DepartmentID
GO

exec GetEmployeeByDepartmentId @DepartmentID = 1


-- Birden fazla parametreli procedure olu�turma
CREATE PROCEDURE SelectAllCustomers @City nvarchar(30), @PostalCode nvarchar(10)
AS
SELECT * FROM Customers WHERE City = @City AND PostalCode = @PostalCode
GO

EXEC SelectAllCustomers @City = 'London', @PostalCode = 'WA1 1DP'

