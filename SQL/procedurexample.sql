create procedure testProc4
	@Ad varchar(255), 
	@Soyad varchar(255),
	@RetVal int output
	as
begin
	set @RetVal = 10
	select @Ad Ad, @Soyad Soyad, @RetVal ReturnValue
	return @RetVal
end

declare @ReturnVal int
declare @age int
exec @age =  testProc4 "Ermand", "Haruni", @ReturnVal

select @age "Return value"
