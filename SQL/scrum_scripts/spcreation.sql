if exists(select 1 from sys.procedures where name = 'scsp_GetAllScrums')
drop proc scsp_GetAllScrums
go

if exists(select 1 from sys.procedures where name = 'scsp_GetScrumsByDate')
drop proc scsp_GetScrumsByDate
go


if exists(select 1 from sys.procedures where name = 'scsp_InsertScrum')
drop proc scsp_InsertScrum
go

if exists(select 1 from sys.procedures where name = 'scsp_UpdateScrum')
drop proc scsp_UpdateScrum
go

if exists(select 1 from sys.procedures where name = 'scsp_DeleteScrum')
drop proc scsp_DeleteScrum
go


CREATE PROCEDURE scsp_GetAllScrums
AS
BEGIN
    SELECT *
    FROM ehScrums;
END
GO

CREATE PROCEDURE scsp_GetScrumsByDate
    @TeamCode varchar(255),
	@Date date
AS
BEGIN
    SELECT * FROM ehScrums WHERE TeamCode = @TeamCode and Date = @Date
END
go


CREATE PROCEDURE scsp_InsertScrum
	@TeamCode varchar(255),
    @Filename varchar(MAX),
    @ScrumStartDate date,
    @WFNo int,
    @Bank varchar(MAX),
    @Subject varchar(MAX),
    @Employee varchar(MAX),
    @Description varchar(MAX),
    @Priority decimal(8,2),
    @Status varchar(MAX),
    @Start decimal(8,2),
    @Completed decimal(8,2),
    @Date date
AS
BEGIN
    INSERT INTO ehScrums (TeamCode ,Filename, ScrumStartDate, WFNo, Bank, Subject, Employee, Description, Priority, Status, Start, Completed, Date, InsertDate)
    VALUES (@TeamCode, @Filename, @ScrumStartDate, @WFNo, @Bank, @Subject, @Employee, @Description, @Priority, @Status, @Start, @Completed, @Date, GETDATE());
END
GO


CREATE PROCEDURE scsp_UpdateScrum
    @ScrumID int,
	@TeamCode varchar(255),
    @Filename varchar(MAX),
    @ScrumStartDate date,
    @WFNo int,
    @Bank varchar(MAX),
    @Subject varchar(MAX),
    @Employee varchar(MAX),
    @Description varchar(MAX),
    @Priority decimal(8,2),
    @Status varchar(MAX),
    @Start decimal(8,2),
    @Completed decimal(8,2),
    @Date date
AS
BEGIN
    UPDATE ehScrums 
    SET TeamCode = @TeamCode ,Filename = @Filename, ScrumStartDate = @ScrumStartDate, WFNo = @WFNo, Bank = @Bank, Subject = @Subject, Employee = @Employee, 
        Description = @Description, Priority = @Priority, Status = @Status, Start = @Start, 
        Completed = @Completed, Date = @Date
    WHERE ScrumID = @ScrumID;
END
GO


CREATE PROCEDURE scsp_DeleteScrum
    @ScrumID int
AS
BEGIN
    DELETE FROM ehScrums WHERE ScrumID = @ScrumID;
END
GO


--- ehScrumsConfig

if exists(select 1 from sys.procedures where name = 'scsp_GetAllScrumConfigs')
drop proc scsp_GetAllScrumConfigs
go

if exists(select 1 from sys.procedures where name = 'scsp_InsertScrumConfig')
drop proc scsp_InsertScrumConfig
go

if exists(select 1 from sys.procedures where name = 'scsp_UpdateScrumConfig')
drop proc scsp_UpdateScrumConfig
go

if exists(select 1 from sys.procedures where name = 'scsp_DeleteScrumConfig')
drop proc scsp_DeleteScrumConfig
go

if exists(select 1 from sys.procedures where name = 'scsp_GetScrumConfigByTeam')
drop proc scsp_GetScrumConfigByTeam
go

CREATE PROCEDURE scsp_GetAllScrumConfigs
AS
BEGIN
    SELECT * FROM ehScrumConfigs;
END
go

CREATE PROCEDURE scsp_GetScrumConfigByTeam
    @TeamCode varchar(255)
AS
BEGIN
    SELECT * FROM ehScrumConfigs WHERE TeamCode = @TeamCode;
END
go

CREATE PROCEDURE scsp_InsertScrumConfig
    @TeamCode VARCHAR(255),
    @WFNoColNo INT,
    @BankColNo INT,
    @SubjectColNo INT,
    @EmployeeColNo INT,
    @DescriptionColNo INT,
    @PriorityColNo INT,
    @StatusColNo INT,
    @StartColNo INT,
    @DateStartColNo INT,
    @DataStartRowNo INT
AS
BEGIN
    INSERT INTO ehScrumConfigs (TeamCode, WFNoColNo, BankColNo, SubjectColNo, EmployeeColNo, DescriptionColNo, PriorityColNo, StatusColNo, StartColNo, DateStartColNo, DataStartRowNo, InsertDate)
    VALUES (@TeamCode, @WFNoColNo, @BankColNo, @SubjectColNo, @EmployeeColNo, @DescriptionColNo, @PriorityColNo, @StatusColNo, @StartColNo, @DateStartColNo, @DataStartRowNo, GETDATE());
END
GO


CREATE PROCEDURE scsp_UpdateScrumConfig
    @ScrumConfigID INT,
    @TeamCode VARCHAR(255),
    @WFNoColNo INT,
    @BankColNo INT,
    @SubjectColNo INT,
    @EmployeeColNo INT,
    @DescriptionColNo INT,
    @PriorityColNo INT,
    @StatusColNo INT,
    @StartColNo INT,
    @DateStartColNo INT,
    @DataStartRowNo INT
AS
BEGIN
    UPDATE ehScrumConfigs
    SET TeamCode = @TeamCode,
        WFNoColNo = @WFNoColNo,
        BankColNo = @BankColNo,
        SubjectColNo = @SubjectColNo,
        EmployeeColNo = @EmployeeColNo,
        DescriptionColNo = @DescriptionColNo,
        PriorityColNo = @PriorityColNo,
        StatusColNo = @StatusColNo,
        StartColNo = @StartColNo,
        DateStartColNo = @DateStartColNo,
        DataStartRowNo = @DataStartRowNo
    WHERE ScrumConfigID = @ScrumConfigID;
END
GO


CREATE PROCEDURE scsp_DeleteScrumConfig
    @ScrumConfigID int
AS
BEGIN
    DELETE FROM ehScrumConfigs WHERE ScrumConfigID = @ScrumConfigID;
END
go

