-- UPDATE i�lem�
UPDATE Customers
SET ContactName = 'Alfred Schmidt', City= 'Frankfurt'
WHERE CustomerID = 1;

-- DELETE i�lemi
DELETE FROM Customers WHERE CustomerName='Alfreds Futterkiste';

-- Tablo silme i�lemi
DROP TABLE Customers;