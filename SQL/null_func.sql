-- NULL fonksiyonlar
-- IFNULL(), e�er verdi�imiz de�er NULL ise ikinci parametreye yazd���m�z �eyi d�nd�r�yo

SELECT ProductName, UnitPrice * (UnitsInStock + ISNULL(UnitsOnOrder, 0))
FROM Products;

