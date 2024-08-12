if exists (select 1 from sys.tables where name = 'ehBanks')
	drop table ehBanks
go

CREATE TABLE ehBanks (
    BankID INT PRIMARY KEY IDENTITY(1,1),
    BankCode char(20) NOT NULL,
    BankName char(255)
);