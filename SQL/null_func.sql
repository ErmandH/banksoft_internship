-- NULL fonksiyonlar
-- IFNULL(), eðer verdiðimiz deðer NULL ise ikinci parametreye yazdýðýmýz þeyi döndürüyo

SELECT ProductName, UnitPrice * (UnitsInStock + ISNULL(UnitsOnOrder, 0))
FROM Products;

