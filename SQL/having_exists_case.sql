-- HAVING komutu aggregate functionlar ile where kullanýlamadýðý için kullanýlan komut
-- aþaðýda Ülkeleri gruplayýp sadece 5 ten fazla müþterisi bulunan Ülkeleri listeleyen komut var
SELECT COUNT(CustomerID), Country
FROM Customers
GROUP BY Country
HAVING COUNT(CustomerID) > 5;




-- EXISTS
-- Bir kayýtýn var olup olmadýðýný kontrol eder, var ise TRUE döner
-- Products taki SupplierId si olan ve fiyatý 20 den küçük Supplierlar var ise listele
SELECT SupplierName
FROM Suppliers
WHERE EXISTS (SELECT ProductName FROM Products WHERE Products.SupplierID = Suppliers.supplierID AND Price < 20);




-- CASE kullanýmý
-- Birden fazla þartlý value return etmek için kullanýyoruz
SELECT OrderID, Quantity,
CASE
    WHEN Quantity > 30 THEN 'The quantity is greater than 30' -- 30 dan büyükse bunu döndür
    WHEN Quantity = 30 THEN 'The quantity is 30' -- eþitse bunu döndür
    ELSE 'The quantity is under 30' -- hiçbiri deðilse bunu döndür
END AS QuantityText
FROM OrderDetails;

