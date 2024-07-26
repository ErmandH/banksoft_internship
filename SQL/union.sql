-- UNION
-- Seçilen sütunlardaki deðerleri birleþtirir ayný deðerler 1 kere alýnýr
-- Altta Customers tablosunda örneðin 50 farklý þehir, Suppliersta 40 farklý þehir ve bu þehirlerin hepsi unique isimlerse
-- UNION iþlemi sonucu 90 satýrlýk bir veri oluþucak
SELECT City FROM Customers
UNION
SELECT City FROM Suppliers
ORDER BY City;

-- UNION ALL yaparsak duplicate veriler de gelir

-- UNION WHERE
-- Ülkesi Almanya olan kayýtlarýn Þehirlerini birleþtir
SELECT City, Country FROM Customers
WHERE Country='Germany'
UNION
SELECT City, Country FROM Suppliers
WHERE Country='Germany'
ORDER BY City;

