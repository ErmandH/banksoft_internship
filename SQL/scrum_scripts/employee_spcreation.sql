if exists(select 1 from sys.procedures where name = 'scsp_GetAllEmployees')
	drop proc scsp_GetAllScrums
go


if exists(select 1 from sys.procedures where name = 'scsp_InsertEmployee')
drop proc scsp_InsertScrum
go

if exists(select 1 from sys.procedures where name = 'scsp_UpdateEmployee')
drop proc scsp_UpdateScrum
go

if exists(select 1 from sys.procedures where name = 'scsp_DeleteEmployee')
drop proc scsp_DeleteScrum
go


CREATE PROCEDURE scsp_GetAllEmployees
AS
BEGIN
    SELECT EmployeeID, Code, Name, Surname
    FROM ehEmployees;
END;
GO

CREATE PROCEDURE scsp_InsertEmployee
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


CREATE PROCEDURE scsp_UpdateEmployee
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

CREATE PROCEDURE scsp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    DELETE FROM ehEmployees
    WHERE EmployeeID = @EmployeeID;
END;
GO
