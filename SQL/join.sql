-- JOIN komutu ili�kili s�tunu bulunan tablolar� birle�tirmeye yarar
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
-- LEFT JOIN de koydu�umuz �art ne olursa olsun soldaki tabloyu veriyo yani DepartmanID si 10 olan hi�bir kay�t olmasa bile
-- t�m Employees leri d�nd�r�yo ama include etti�imiz tablo bulunamad�ysa o tablodaki de�erler NULL oluyor
SELECT Employees.EmployeeID , Departments.DepartmentName, Employees.FirstName ,Employees.Salary
FROM Employees
LEFT JOIN Departments ON Employees.DepartmentID = 10;


-- RIGHT JOIN
-- �stteki �eyler bu sefer sa� tablo i�in ge�erli yani Departments i�in
SELECT Employees.EmployeeID , Departments.DepartmentName, Employees.FirstName ,Employees.Salary
FROM Employees
RIGHT JOIN Departments ON Employees.DepartmentID = 10;

-- FULL JOIN
-- �art sa�lanmasa bile her iki tabloyu da d�nd�r�r, �artlar�n uymad��� kay�tlardaki s�t�nlar NULL g�z�k�r
SELECT Employees.EmployeeID , Departments.DepartmentName, Employees.FirstName ,Employees.Salary
FROM Employees
FULL JOIN Departments ON Employees.DepartmentID = 3;

