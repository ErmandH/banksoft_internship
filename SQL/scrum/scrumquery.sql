-- scrums queries
select 
	Employee,
	WFNo,
	MAX(Start) 'Plan',
	SUM(Completed) 'Gerçekleþen',
	MAX(Start) - SUM(Completed) 'Sapma',
	MIN(Date) 'Baþlangýç', 
	MAX(Date) 'Bitiþ'
from
	ehScrums 
where
	ScrumStartDate = '2024-02-26'
group by
	Employee,
	WFNo
