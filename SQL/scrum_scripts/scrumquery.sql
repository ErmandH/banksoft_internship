-- scrums queries
select 
	Employee,
	WFNo,
	MAX(Start) 'Plan',
	SUM(Completed) 'Ger�ekle�en',
	MAX(Start) - SUM(Completed) 'Sapma',
	MIN(Date) 'Ba�lang��', 
	MAX(Date) 'Biti�'
from
	ehScrums 
where
	ScrumStartDate = '2024-02-26'
group by
	Employee,
	WFNo
