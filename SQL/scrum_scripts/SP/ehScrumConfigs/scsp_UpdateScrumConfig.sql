if exists(select 1 from sys.procedures where name = 'scsp_UpdateScrumConfig')
drop proc scsp_UpdateScrumConfig
go

CREATE PROCEDURE scsp_UpdateScrumConfig
    @ScrumConfigID INT,
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