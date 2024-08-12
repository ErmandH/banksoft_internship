if exists (select 1 from sys.tables where name = 'ehEmployees')
	drop table ehEmployees
go

CREATE TABLE ehEmployees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeCode char(20) NOT NULL,
    EmployeeName char(100),
    EmployeeSurname char(100)
);