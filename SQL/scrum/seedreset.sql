delete from ehScrums

select * from ehScrums 
-- Identity seed s�f�rlama
DBCC CHECKIDENT ('[ehScrums]', RESEED, 0);
GO