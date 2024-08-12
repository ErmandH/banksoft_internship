if exists(select 1 from sys.procedures where name = 'scsp_GetAllScrumConfigs')
drop proc scsp_GetAllScrumConfigs
go

CREATE PROCEDURE scsp_GetAllScrumConfigs
AS
BEGIN
    SELECT * FROM ehScrumConfigs
	ORDER BY TeamCode
END
go