if exists(select 1 from sys.procedures where name = 'scsp_DeleteScrum')
	drop proc scsp_DeleteScrum
go


CREATE PROCEDURE scsp_DeleteScrum
    @ScrumID int
AS
BEGIN
    DELETE FROM ehScrums WHERE ScrumID = @ScrumID;
END
GO