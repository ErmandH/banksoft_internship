-- HAVING komutu aggregate functionlar ile where kullan�lamad��� i�in kullan�lan komut
-- a�a��da �lkeleri gruplay�p sadece 5 ten fazla m��terisi bulunan �lkeleri listeleyen komut var
SELECT COUNT(CustomerID), Country
FROM Customers
GROUP BY Country
HAVING COUNT(CustomerID) > 5;




-- EXISTS
-- Bir kay�t�n var olup olmad���n� kontrol eder, var ise TRUE d�ner
-- Products taki SupplierId si olan ve fiyat� 20 den k���k Supplierlar var ise listele
SELECT SupplierName
FROM Suppliers
WHERE EXISTS (SELECT ProductName FROM Products WHERE Products.SupplierID = Suppliers.supplierID AND Price < 20);




-- CASE kullan�m�
-- Birden fazla �artl� value return etmek i�in kullan�yoruz
SELECT OrderID, Quantity,
CASE
    WHEN Quantity > 30 THEN 'The quantity is greater than 30' -- 30 dan b�y�kse bunu d�nd�r
    WHEN Quantity = 30 THEN 'The quantity is 30' -- e�itse bunu d�nd�r
    ELSE 'The quantity is under 30' -- hi�biri de�ilse bunu d�nd�r
END AS QuantityText
FROM OrderDetails;

