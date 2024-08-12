if exists(select 1 from sys.procedures where name = 'scsp_GetAllScrums')
	drop proc scsp_GetAllScrums
go

CREATE PROCEDURE scsp_GetAllScrums
AS
BEGIN
    SELECT *
    FROM ehScrums;
END
GO