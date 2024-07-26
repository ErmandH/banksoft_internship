-- ORDER BY --
-- Belli bir sütuna göre sort iþlemi yapmaya saðlar
SELECT * FROM Products ORDER BY Price;

-- DESC Descending sýralama yapar (büyükten küçüðe), ASC ascending sýralama yapar (küçükten büyüðe)
SELECT * FROM Products ORDER BY Price DESC;

-- Birden fazla sütun ile sýralanabilir
SELECT * FROM Customers
ORDER BY Country ASC, CustomerName DESC;


-- SELECT TOP ilk 3 ünü alýr
SELECT TOP 3 * FROM Customers;

-- AND operatorü conditionlar için
SELECT *
FROM ermand_tbl1
WHERE Country = 'Spain' AND CustomerName LIKE 'G%';

-- OR operatörü
SELECT *
FROM Customers
WHERE Country = 'Germany' OR Country = 'Spain'

-- NOT operatörü
-- if !(Country == "Spain")
SELECT * FROM Customers
WHERE NOT Country = 'Spain';

-- NULL kontrolü
SELECT column_names
FROM table_name
WHERE column_name IS NULL;

-- NOT NULL kontrolü
SELECT column_names
FROM table_name
WHERE column_name IS NOT NULL;