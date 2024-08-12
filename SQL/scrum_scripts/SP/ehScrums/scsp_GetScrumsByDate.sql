if exists(select 1 from sys.procedures where name = 'scsp_GetScrumsByDate')
drop proc scsp_GetScrumsByDate
go

CREATE PROCEDURE scsp_GetScrumsByDate
    @TeamCode char(20),
	@WorkDate date
AS
BEGIN
    SELECT * FROM ehScrums WHERE TeamCode = @TeamCode and WorkDate = @WorkDate
END
go