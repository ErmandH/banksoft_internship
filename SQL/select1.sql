/*
	SELECT - extracts data from a database
	UPDATE - updates data in a database
	DELETE - deletes data from a database
	INSERT INTO - inserts new data into a database
	CREATE DATABASE - creates a new database
	ALTER DATABASE - modifies a database
	CREATE TABLE - creates a new table
	ALTER TABLE - modifies a table
	DROP TABLE - deletes a table
	CREATE INDEX - creates an index (search key)
	DROP INDEX - deletes an index
*/

-- Tablo olu�turma
/* 
	CREATE TABLE ermand_tbl1(
		ID int,
		FirstName varchar(255),
		LastName varchar(255),
		Address varchar(255),
		City varchar(255),
		Age int,
		Salary int
	)
*/

-- SELECT komutu veri okumak i�in kullan�l�r
-- SELECT FirstName, City FROM ermand_tbl1;

-- SELECT DISTINCT ayn� de�erli s�tunlardan sadece birini al�r yani Country = Turkey olan 3 sat�r varsa sadece 1 ini al�r
-- SELECT DISTINCT Country FROM Customers;

-- WHERE komutu ko�ullu sorgulama i�in kullan�l�r. WHERE den sonra ko�ul yaz�l�r
-- SELECT * FROM Customers WHERE CustomerID > 80

