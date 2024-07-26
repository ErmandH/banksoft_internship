-- GROUP BY

-- Group BY belirttiðimiz sütuna göre grupluyo yani örneðin DepartmentId = 1 olanlarý grupluyo 
-- ve bizden bir fonksiyon kullanmamýzý bekliyo örn AVG(Salary) diyerek DepartmentId = 1 olanlarýn ortalama maaþýný toplayýp
-- bize AvgSalary olarak gösteriyo tabloda
SELECT DepartmentID, AVG(Salary) AS AvgSalary
FROM Employees
GROUP BY DepartmentID