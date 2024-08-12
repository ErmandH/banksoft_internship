if exists(select 1 from sys.procedures where name = 'scsp_InsertBank')
	drop proc scsp_InsertBank
go

CREATE PROCEDURE scsp_InsertBank
    @BankCode CHAR(20),
    @BankName CHAR(255)
AS
BEGIN
    INSERT INTO ehBanks (BankCode, BankName)
    VALUES (@BankCode, @BankName);
END;
GO