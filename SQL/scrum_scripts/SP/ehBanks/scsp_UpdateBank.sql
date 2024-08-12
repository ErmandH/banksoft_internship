if exists(select 1 from sys.procedures where name = 'scsp_UpdateBank')
drop proc scsp_UpdateBank
go


CREATE PROCEDURE scsp_UpdateBank
    @BankID INT,
    @BankCode char(20),
    @BankName char(255)
AS
BEGIN
    UPDATE ehBanks
    SET BankCode = @BankCode, BankName = @BankName
    WHERE BankID = @BankID;
END;
GO