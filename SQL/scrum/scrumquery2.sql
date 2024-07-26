-- HSBCScrum 42082
-- ChipScrum 20687
select Bank from ehScrums group by Bank 

select * from ehEmployees

WITH EmployeeWeeklySummary AS (
    SELECT
        WFNo,
        TeamCode,
        ScrumStartDate,
        Employee,
        MAX(Start) AS PlannedStart,
        SUM(Completed) AS TotalCompleted
    FROM
        ehScrums
    GROUP BY
        WFNo,
        TeamCode,
        ScrumStartDate,
        Employee
)
SELECT
    WFNo,
    TeamCode,
    ScrumStartDate,
    SUM(PlannedStart) AS 'Plan',
    SUM(TotalCompleted) AS 'Tamamlanan'
FROM
    EmployeeWeeklySummary
WHERE ScrumStartDate = '2024-07-16'
GROUP BY
    WFNo,
    TeamCode,
    ScrumStartDate
ORDER BY
    WFNo,
    ScrumStartDate;
