-- SQL AGGREGATE FUNCTIONS

/*
	MIN() - returns the smallest value within the selected column
	MAX() - returns the largest value within the selected column
	COUNT() - returns the number of rows in a set
	SUM() - returns the total sum of a numerical column
	AVG() - returns the average value of a numerical column
*/

-- MIN
SELECT MIN(Price)
FROM Employees;

-- MAX
SELECT MAX(Price)
FROM Products;

-- COUNT(*) sat�r say�s�n� bulmaya yarar
SELECT COUNT(*)
FROM Products;

-- Price � 20 den b�y�k olanlar�n say�s�
SELECT COUNT(ProductID)
FROM Products
WHERE Price > 20;

-- CategoryID si 1 olanlar�n say�s�, 2 olanlar�n say�s� gibi onlar� g�steriyor gruplayarak
SELECT COUNT(*) AS [Number of records], CategoryID
FROM Products
GROUP BY CategoryID;
