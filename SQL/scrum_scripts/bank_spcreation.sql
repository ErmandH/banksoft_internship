if exists(select 1 from sys.procedures where name = 'scsp_GetAllBanks')
drop proc scsp_GetAllBanks
go

if exists(select 1 from sys.procedures where name = 'scsp_InsertBank')
drop proc scsp_InsertBank
go

if exists(select 1 from sys.procedures where name = 'scsp_UpdateBank')
drop proc scsp_UpdateBank
go

if exists(select 1 from sys.procedures where name = 'scsp_DeleteBank')
drop proc scsp_DeleteBank
go



CREATE PROCEDURE scsp_GetAllBanks
AS
BEGIN
    SELECT BankID, Code, Name
    FROM ehBanks;
END;
GO

CREATE PROCEDURE scsp_InsertBank
    @Code NVARCHAR(MAX),
    @Name NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO ehBanks (Code, Name)
    VALUES (@Code, @Name);

    SELECT SCOPE_IDENTITY() AS NewBankID;
END;
GO

CREATE PROCEDURE scsp_UpdateBank
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

CREATE PROCEDURE scsp_DeleteBank
    @BankID INT
AS
BEGIN
    DELETE FROM ehBanks
    WHERE BankID = @BankID;
END;
GO

