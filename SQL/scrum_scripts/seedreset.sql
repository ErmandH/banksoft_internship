delete from ehScrums

select * from ehScrums order by WorkDate
-- Identity seed s�f�rlama
DBCC CHECKIDENT ('[ehScrums]', RESEED, 0);
GO