-- INDEX olu�turma
-- indexler bir tablodaki bir s�tunun tarama h�z�n� artt�r�r
-- ama g�ncelleme i�lemini de ayr�ca yava�lat�r
create index idx_firstname on Employees (FirstName)

-- indexi kald�rmak i�in
DROP INDEX Employees.idx_firstname;