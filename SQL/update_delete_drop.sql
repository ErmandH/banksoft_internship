-- UPDATE iþlemþ
UPDATE Customers
SET ContactName = 'Alfred Schmidt', City= 'Frankfurt'
WHERE CustomerID = 1;

-- DELETE iþlemi
DELETE FROM Customers WHERE CustomerName='Alfreds Futterkiste';

-- Tablo silme iþlemi
DROP TABLE Customers;