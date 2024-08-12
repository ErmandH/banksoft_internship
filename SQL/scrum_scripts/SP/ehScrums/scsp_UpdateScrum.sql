if exists(select 1 from sys.procedures where name = 'scsp_UpdateScrum')
drop proc scsp_UpdateScrum
go



CREATE PROCEDURE scsp_UpdateScrum
    @ScrumID int,
    @TeamCode char(20),
    @Filename char(100),
    @ScrumStartDate date,
    @WFNo int,
    @BankCode char(20),
    @Subject char(255),
    @EmployeeCode char(20),
    @Description char(1020),
    @Priority decimal(8,2),
    @Status char(255),
    @Start decimal(8,2),
    @Completed decimal(8,2),
    @WorkDate date
AS
BEGIN
    UPDATE ehScrums 
    SET TeamCode = @TeamCode, Filename = @Filename, ScrumStartDate = @ScrumStartDate, WFNo = @WFNo, 
        BankCode = @BankCode, Subject = @Subject, EmployeeCode = @EmployeeCode, 
        Description = @Description, Priority = @Priority, Status = @Status, Start = @Start, 
        Completed = @Completed, WorkDate = @WorkDate
    WHERE ScrumID = @ScrumID;
END;
GO
