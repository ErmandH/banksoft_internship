if exists(select 1 from sys.procedures where name = 'scsp_DeleteScrumConfig')
drop proc scsp_DeleteScrumConfig
go

CREATE PROCEDURE scsp_DeleteScrumConfig
    @ScrumConfigID int
AS
BEGIN
    DELETE FROM ehScrumConfigs WHERE ScrumConfigID = @ScrumConfigID;
END
go