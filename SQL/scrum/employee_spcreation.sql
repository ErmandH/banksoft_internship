if exists(select 1 from sys.procedures where name = 'sp_GetAllEmployees')
drop proc sp_GetAllScrums
go


if exists(select 1 from sys.procedures where name = 'sp_InsertEmployee')
drop proc sp_InsertScrum
go

if exists(select 1 from sys.procedures where name = 'sp_UpdateEmployee')
drop proc sp_UpdateScrum
go

if exists(select 1 from sys.procedures where name = 'sp_DeleteEmployee')
drop proc sp_DeleteScrum
go


CREATE PROCEDURE sp_GetAllEmployees
AS
BEGIN
    SELECT EmployeeID, Code, Name, Surname
    FROM ehEmployees;
END;
GO

CREATE PROCEDURE sp_InsertEmployee
    @Code NVARCHAR(MAX),
    @Name NVARCHAR(MAX),
    @Surname NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO ehEmployees (Code, Name, Surname)
    VALUES (@Code, @Name, @Surname);

    SELECT SCOPE_IDENTITY() AS NewEmployeeID;
END;
GO


CREATE PROCEDURE sp_UpdateEmployee
    @EmployeeID INT,
    @Code NVARCHAR(MAX),
    @Name NVARCHAR(MAX),
    @Surname NVARCHAR(MAX)
AS
BEGIN
    UPDATE ehEmployees
    SET Code = @Code, Name = @Name, Surname = @Surname
    WHERE EmployeeID = @EmployeeID;
END;
GO

CREATE PROCEDURE sp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    DELETE FROM ehEmployees
    WHERE EmployeeID = @EmployeeID;
END;
GO
