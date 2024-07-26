delete from ehScrums

select * from ehScrums 
-- Identity seed sýfýrlama
DBCC CHECKIDENT ('[ehScrums]', RESEED, 0);
GO