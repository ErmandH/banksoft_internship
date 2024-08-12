if exists(select 1 from sys.procedures where name = 'scsp_InsertScrum')
drop proc scsp_InsertScrum
go

CREATE PROCEDURE scsp_InsertScrum
    @TeamCode CHAR(20),
    @Filename CHAR(100),
    @ScrumStartDate DATE,
    @WFNo INT,
    @BankCode CHAR(20),
    @Subject CHAR(255),
    @EmployeeCode CHAR(20),
    @Description CHAR(1020),
    @Priority DECIMAL(8,2),
    @Status CHAR(255),
    @Start DECIMAL(8,2),
    @Completed DECIMAL(8,2),
    @WorkDate DATE
AS
BEGIN
    INSERT INTO ehScrums (
        TeamCode, Filename, ScrumStartDate, WFNo, BankCode, Subject, EmployeeCode, 
        Description, Priority, Status, Start, Completed, WorkDate, InsertDate
    )
    VALUES (
        @TeamCode, @Filename, @ScrumStartDate, @WFNo, @BankCode, @Subject, @EmployeeCode,
        @Description, @Priority, @Status, @Start, @Completed, @WorkDate, GETDATE()
    );
END;
GO