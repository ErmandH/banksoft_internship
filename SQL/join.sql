-- JOIN komutu iliþkili sütunu bulunan tablolarý birleþtirmeye yarar
/*
	(INNER) JOIN: Returns records that have matching values in both tables
	LEFT (OUTER) JOIN: Returns all records from the left table, and the matched records from the right table
	RIGHT (OUTER) JOIN: Returns all records from the right table, and the matched records from the left table
	FULL (OUTER) JOIN: Returns all records when there is a match in either left or right table
*/

-- INNER JOIN
SELECT Employees.EmployeeID , Departments.DepartmentName, Employees.FirstName ,Employees.Salary
FROM Employees
INNER JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID;

-- LEFT JOIN
-- LEFT JOIN de koyduðumuz þart ne olursa olsun soldaki tabloyu veriyo yani DepartmanID si 10 olan hiçbir kayýt olmasa bile
-- tüm Employees leri döndürüyo ama include ettiðimiz tablo bulunamadýysa o tablodaki deðerler NULL oluyor
SELECT Employees.EmployeeID , Departments.DepartmentName, Employees.FirstName ,Employees.Salary
FROM Employees
LEFT JOIN Departments ON Employees.DepartmentID = 10;


-- RIGHT JOIN
-- Üstteki þeyler bu sefer sað tablo için geçerli yani Departments için
SELECT Employees.EmployeeID , Departments.DepartmentName, Employees.FirstName ,Employees.Salary
FROM Employees
RIGHT JOIN Departments ON Employees.DepartmentID = 10;

-- FULL JOIN
-- Þart saðlanmasa bile her iki tabloyu da döndürür, þartlarýn uymadýðý kayýtlardaki sütünlar NULL gözükür
SELECT Employees.EmployeeID , Departments.DepartmentName, Employees.FirstName ,Employees.Salary
FROM Employees
FULL JOIN Departments ON Employees.DepartmentID = 3;

