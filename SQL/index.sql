-- INDEX oluþturma
-- indexler bir tablodaki bir sütunun tarama hýzýný arttýrýr
-- ama güncelleme iþlemini de ayrýca yavaþlatýr
create index idx_firstname on Employees (FirstName)

-- indexi kaldýrmak için
DROP INDEX Employees.idx_firstname;