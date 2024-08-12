delete from ehScrums

select * from ehScrums order by WorkDate
-- Identity seed sýfýrlama
DBCC CHECKIDENT ('[ehScrums]', RESEED, 0);
GO