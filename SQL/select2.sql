-- ORDER BY --
-- Belli bir s�tuna g�re sort i�lemi yapmaya sa�lar
SELECT * FROM Products ORDER BY Price;

-- DESC Descending s�ralama yapar (b�y�kten k����e), ASC ascending s�ralama yapar (k���kten b�y��e)
SELECT * FROM Products ORDER BY Price DESC;

-- Birden fazla s�tun ile s�ralanabilir
SELECT * FROM Customers
ORDER BY Country ASC, CustomerName DESC;


-- SELECT TOP ilk 3 �n� al�r
SELECT TOP 3 * FROM Customers;

-- AND operator� conditionlar i�in
SELECT *
FROM ermand_tbl1
WHERE Country = 'Spain' AND CustomerName LIKE 'G%';

-- OR operat�r�
SELECT *
FROM Customers
WHERE Country = 'Germany' OR Country = 'Spain'

-- NOT operat�r�
-- if !(Country == "Spain")
SELECT * FROM Customers
WHERE NOT Country = 'Spain';

-- NULL kontrol�
SELECT column_names
FROM table_name
WHERE column_name IS NULL;

-- NOT NULL kontrol�
SELECT column_names
FROM table_name
WHERE column_name IS NOT NULL;