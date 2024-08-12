if exists (select 1 from sys.tables where name = 'ehScrumConfigs')
	drop table ehScrumConfigs
go
create table ehScrumConfigs (
	ScrumConfigID int identity(1,1) PRIMARY KEY,
	TeamCode char(20) not null,
	WFNoColNo int not null,
	BankColNo int not null,
	SubjectColNo int not null,
	EmployeeColNo int not null,
	DescriptionColNo int not null,
	PriorityColNo int not null,
	StatusColNo int not null,
	StartColNo int not null,
	DateStartColNo int not null,
	DataStartRowNo int not null,
	InsertDate datetime
)
go