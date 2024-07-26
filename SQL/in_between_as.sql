-- IN ile birden fazla de�eri sorgulayabiliyoruz
SELECT * FROM Customers
WHERE Country IN ('Germany', 'France', 'UK');

-- NOT IN
SELECT * FROM Customers
WHERE Country NOT IN ('Germany', 'France', 'UK');

-- Kar��la�t�r�lacak de�erleri dinamik bir �ekilde alma
SELECT * FROM Customers
WHERE CustomerID IN (SELECT CustomerID FROM Orders);

-- BETWEEN
SELECT * FROM Products
WHERE Price BETWEEN 10 AND 20;
WHERE Price NOT BETWEEN 10 AND 20;

SELECT * FROM Orders
WHERE OrderDate BETWEEN '1996-07-01' AND '1996-07-31';

-- AS => Aliases (isim verme, label takma)
-- s�tunun tabloda hangi isimle g�sterilece�ini belirtiyor
SELECT CustomerID AS ID, CustomerName AS Customer
FROM Customers;

-- Bo�luklu label verme
SELECT ProductName AS "My Great Products"
FROM Products;

