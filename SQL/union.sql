-- UNION
-- Se�ilen s�tunlardaki de�erleri birle�tirir ayn� de�erler 1 kere al�n�r
-- Altta Customers tablosunda �rne�in 50 farkl� �ehir, Suppliersta 40 farkl� �ehir ve bu �ehirlerin hepsi unique isimlerse
-- UNION i�lemi sonucu 90 sat�rl�k bir veri olu�ucak
SELECT City FROM Customers
UNION
SELECT City FROM Suppliers
ORDER BY City;

-- UNION ALL yaparsak duplicate veriler de gelir

-- UNION WHERE
-- �lkesi Almanya olan kay�tlar�n �ehirlerini birle�tir
SELECT City, Country FROM Customers
WHERE Country='Germany'
UNION
SELECT City, Country FROM Suppliers
WHERE Country='Germany'
ORDER BY City;

