if exists(select 1 from sys.procedures where name = 'sp_GetAllBanks')
drop proc sp_GetAllScrums
go

if exists(select 1 from sys.procedures where name = 'sp_InsertBank')
drop proc sp_InsertScrum
go

if exists(select 1 from sys.procedures where name = 'sp_UpdateBank')
drop proc sp_UpdateScrum
go

if exists(select 1 from sys.procedures where name = 'sp_DeleteBank')
drop proc sp_DeleteScrum
go



CREATE PROCEDURE sp_GetAllBanks
AS
BEGIN
    SELECT BankID, Code, Name
    FROM ehBanks;
END;
GO

CREATE PROCEDURE sp_InsertBank
    @Code NVARCHAR(MAX),
    @Name NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO ehBanks (Code, Name)
    VALUES (@Code, @Name);

    SELECT SCOPE_IDENTITY() AS NewBankID;
END;
GO

CREATE PROCEDURE sp_UpdateBank
    @BankID INT,
    @Code NVARCHAR(MAX),
    @Name NVARCHAR(MAX)
AS
BEGIN
    UPDATE ehBanks
    SET Code = @Code, Name = @Name
    WHERE BankID = @BankID;
END;
GO

CREATE PROCEDURE sp_DeleteBank
    @BankID INT
AS
BEGIN
    DELETE FROM ehBanks
    WHERE BankID = @BankID;
END;
GO

