if exists(select 1 from sys.procedures where name = 'scsp_GetScrumConfigByTeam')
	drop proc scsp_GetScrumConfigByTeam
go

CREATE PROCEDURE scsp_GetScrumConfigByTeam
    @TeamCode char(20)
AS
BEGIN
    SELECT * FROM ehScrumConfigs WHERE TeamCode = @TeamCode;
END
go