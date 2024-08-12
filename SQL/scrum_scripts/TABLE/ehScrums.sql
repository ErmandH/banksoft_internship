if exists (select 1 from sys.tables where name = 'ehScrums')
	drop table ehScrums
go
create table ehScrums (
	ScrumID int identity(1,1) PRIMARY KEY,
	TeamCode char(20),
	Filename  char(100),
	ScrumStartDate date,
	WFNo int,
	BankCode char(20),
	Subject char(255),
	EmployeeCode char(20),
	Description char(1020),
	Priority decimal(8,2),
	Status char(255),
	Start decimal(8,2),
	Completed decimal(8,2),
	WorkDate date,
	InsertDate datetime
)