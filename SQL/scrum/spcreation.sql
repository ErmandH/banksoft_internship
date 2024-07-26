if exists(select 1 from sys.procedures where name = 'sp_GetAllScrums')
drop proc sp_GetAllScrums
go

if exists(select 1 from sys.procedures where name = 'sp_GetScrumsByDate')
drop proc sp_GetScrumsByDate
go


if exists(select 1 from sys.procedures where name = 'sp_InsertScrum')
drop proc sp_InsertScrum
go

if exists(select 1 from sys.procedures where name = 'sp_UpdateScrum')
drop proc sp_UpdateScrum
go

if exists(select 1 from sys.procedures where name = 'sp_DeleteScrum')
drop proc sp_DeleteScrum
go


CREATE PROCEDURE sp_GetAllScrums
AS
BEGIN
    SELECT *
    FROM ehScrums;
END
GO

CREATE PROCEDURE sp_GetScrumsByDate
    @TeamCode varchar(255),
	@Date date
AS
BEGIN
    SELECT * FROM ehScrums WHERE TeamCode = @TeamCode and Date = @Date
END
go

--- ehScrumsConfig
CREATE PROCEDURE sp_InsertScrum
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


CREATE PROCEDURE sp_UpdateScrum
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


CREATE PROCEDURE sp_DeleteScrum
    @ScrumID int
AS
BEGIN
    DELETE FROM ehScrums WHERE ScrumID = @ScrumID;
END
GO


--- ehScrumsConfig

if exists(select 1 from sys.procedures where name = 'sp_GetAllScrumConfigs')
drop proc sp_GetAllScrumConfigs
go

if exists(select 1 from sys.procedures where name = 'sp_InsertScrumConfig')
drop proc sp_InsertScrumConfig
go

if exists(select 1 from sys.procedures where name = 'sp_UpdateScrumConfig')
drop proc sp_UpdateScrumConfig
go

if exists(select 1 from sys.procedures where name = 'sp_DeleteScrumConfig')
drop proc sp_DeleteScrumConfig
go

if exists(select 1 from sys.procedures where name = 'sp_GetScrumConfigByTeam')
drop proc sp_GetScrumConfigByTeam
go

CREATE PROCEDURE sp_GetAllScrumConfigs
AS
BEGIN
    SELECT * FROM ehScrumConfigs;
END
go

CREATE PROCEDURE sp_GetScrumConfigByTeam
    @TeamCode varchar(255)
AS
BEGIN
    SELECT * FROM ehScrumConfigs WHERE TeamCode = @TeamCode;
END
go

CREATE PROCEDURE sp_InsertScrumConfig
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


CREATE PROCEDURE sp_UpdateScrumConfig
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


CREATE PROCEDURE sp_DeleteScrumConfig
    @ScrumConfigID int
AS
BEGIN
    DELETE FROM ehScrumConfigs WHERE ScrumConfigID = @ScrumConfigID;
END
go

