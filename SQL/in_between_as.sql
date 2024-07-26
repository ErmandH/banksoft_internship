-- IN ile birden fazla deðeri sorgulayabiliyoruz
SELECT * FROM Customers
WHERE Country IN ('Germany', 'France', 'UK');

-- NOT IN
SELECT * FROM Customers
WHERE Country NOT IN ('Germany', 'France', 'UK');

-- Karþýlaþtýrýlacak deðerleri dinamik bir þekilde alma
SELECT * FROM Customers
WHERE CustomerID IN (SELECT CustomerID FROM Orders);

-- BETWEEN
SELECT * FROM Products
WHERE Price BETWEEN 10 AND 20;
WHERE Price NOT BETWEEN 10 AND 20;

SELECT * FROM Orders
WHERE OrderDate BETWEEN '1996-07-01' AND '1996-07-31';

-- AS => Aliases (isim verme, label takma)
-- sütunun tabloda hangi isimle gösterileceðini belirtiyor
SELECT CustomerID AS ID, CustomerName AS Customer
FROM Customers;

-- Boþluklu label verme
SELECT ProductName AS "My Great Products"
FROM Products;

