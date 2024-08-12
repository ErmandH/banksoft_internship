if exists(select 1 from sys.procedures where name = 'scsp_DeleteBank')
	drop proc scsp_DeleteBank
go

CREATE PROCEDURE scsp_DeleteBank
    @BankID INT
AS
BEGIN
    DELETE FROM ehBanks
    WHERE BankID = @BankID;
END;
GO