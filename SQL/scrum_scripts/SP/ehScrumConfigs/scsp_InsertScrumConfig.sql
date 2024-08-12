if exists(select 1 from sys.procedures where name = 'scsp_InsertScrumConfig')
drop proc scsp_InsertScrumConfig
go

CREATE PROCEDURE scsp_InsertScrumConfig
    @TeamCode CHAR(20),
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