-- GROUP BY

-- Group BY belirtti�imiz s�tuna g�re grupluyo yani �rne�in DepartmentId = 1 olanlar� grupluyo 
-- ve bizden bir fonksiyon kullanmam�z� bekliyo �rn AVG(Salary) diyerek DepartmentId = 1 olanlar�n ortalama maa��n� toplay�p
-- bize AvgSalary olarak g�steriyo tabloda
SELECT DepartmentID, AVG(Salary) AS AvgSalary
FROM Employees
GROUP BY DepartmentID