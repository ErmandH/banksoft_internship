if exists (select 1 from sys.tables where name = 'ehScrums')
	drop table ehScrums
go
create table ehScrums (
	ScrumID int identity(1,1) PRIMARY KEY,
	TeamCode varchar(255),
	Filename  varchar(MAX),
	ScrumStartDate date,
	WFNo int,
	Bank varchar(MAX),
	Subject varchar(MAX),
	Employee varchar(MAX),
	Description varchar(MAX),
	Priority decimal(8,2),
	Status varchar(MAX),
	Start decimal(8,2),
	Completed decimal(8,2),
	Date date,
	InsertDate datetime
)

if exists (select 1 from sys.tables where name = 'ehScrumConfigs')
	drop table ehScrumConfigs
go
create table ehScrumConfigs (
	ScrumConfigID int identity(1,1) PRIMARY KEY,
	TeamCode varchar(255) not null,
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

select * from ehScrumConfigs