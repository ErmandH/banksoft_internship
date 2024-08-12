if exists(select 1 from sys.procedures where name = 'scsp_GetAllBanks')
	drop proc scsp_GetAllBanks
go

CREATE PROCEDURE scsp_GetAllBanks
AS
BEGIN
    SELECT *
    FROM ehBanks
	ORDER BY BankCode
END;
GO