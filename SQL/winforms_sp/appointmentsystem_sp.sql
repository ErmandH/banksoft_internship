-- Patient procedures

create proc sp_GetAllPatients as
begin

	select * from Patients

end

go

create proc sp_InsertPatient
	@FirstName varchar(255), 
    @LastName varchar(255),
    @Gender varchar(255),
    @PhoneNumber varchar(255)
	as
begin
	insert into Patients (FirstName, LastName, Gender, PhoneNumber) values(@FirstName, @LastName, @Gender, @PhoneNumber)
end
go

create proc sp_UpdatePatient
	@FirstName varchar(255), 
    @LastName varchar(255),
    @Gender varchar(255),
    @PhoneNumber varchar(255),
    @PatientID int
	as
begin

	UPDATE Patients 
    SET FirstName = @FirstName, LastName = @LastName, Gender = @Gender, PhoneNumber = @PhoneNumber 
    WHERE PatientID = @PatientID

end
go

create proc sp_DeletePatient
	@PatientID int
as
begin
	delete from Patients where PatientID=@PatientID
end
go

-- Doctor procedures
create proc sp_GetAllDoctors as
begin

	select * from Doctors

end
go

create proc sp_InsertDoctor
	@FirstName varchar(255), 
    @LastName varchar(255),
    @PhoneNumber varchar(255),
    @Email varchar(255),
    @Branch varchar(255)
	as
begin

	insert into doctors (FirstName, LastName, PhoneNumber, Email, Branch) VALUES (@FirstName, @LastName, @PhoneNumber, @Email, @Branch)

end
go

CREATE PROCEDURE sp_UpdateDoctor
    @DoctorID int,
    @FirstName varchar(255), 
    @LastName varchar(255),
    @PhoneNumber varchar(255),
    @Email varchar(255),
    @Branch varchar(255)
AS
BEGIN
    UPDATE doctors 
    SET FirstName = @FirstName, 
        LastName = @LastName, 
        PhoneNumber = @PhoneNumber, 
        Email = @Email, 
        Branch = @Branch 
    WHERE DoctorID = @DoctorID
END
go

create proc sp_DeleteDoctor
	@DoctorID int
as
begin
	delete from Doctors where DoctorID=@DoctorID
end
go

-- Appointment procedures
create proc sp_GetAllAppointments as
begin

	select AppointmentID, AppointmentDateTime, 
	Patients.FirstName + ' ' + Patients.LastName AS PatientName, 
	Doctors.FirstName + ' ' + Doctors.LastName AS DoctorName, 
	Doctors.Branch, Notes 
	from Appointments 
	join Patients on Appointments.PatientID = Patients.PatientID 
	join Doctors on Appointments.DoctorID = Doctors.DoctorID

end
go


create proc sp_InsertAppointment
    @PatientID int,
    @DoctorID int,
    @AppointmentDateTime datetime,
    @Notes varchar(255)
as
begin
    INSERT INTO Appointments (PatientID, DoctorID, AppointmentDateTime, Notes)
    VALUES (@PatientID, @DoctorID, @AppointmentDateTime, @Notes)
end
go


create proc sp_UpdateAppointment
    @AppointmentID int,
    @PatientID int,
    @DoctorID int,
    @AppointmentDateTime datetime,
    @Notes varchar(255)
as
begin
    UPDATE Appointments 
    SET PatientID = @PatientID,
        DoctorID = @DoctorID,
        AppointmentDateTime = @AppointmentDateTime,
        Notes = @Notes
    WHERE AppointmentID = @AppointmentID

end
go

create proc sp_DeleteAppointment
	@AppointmentID int
as
begin
	delete from Appointments where AppointmentID=@AppointmentID
end
go


-- Users

create proc sp_GetAllUsers as
begin

	select * from AppUsers

end
go

create proc sp_InsertUser
    @Username varchar(255),
    @Password varchar(255)
as
begin
    INSERT INTO AppUsers (Username, Password)
    VALUES (@Username, @Password)
end
go


create proc sp_UpdateUser
    @UserID int,
    @Username varchar(255),
    @Password varchar(255)
as
begin
    UPDATE AppUsers 
    SET Username = @Username,
	Password = @Password
    WHERE UserID = @UserID

end
go

create proc sp_DeleteUser
	@UserID int
as
begin
	delete from AppUsers where UserID = @UserID
end
go

create proc sp_AuthenticateUser
	@Username varchar(255),
	@Password varchar(255)
as
begin
	select * from AppUsers where Username = @Username AND Password = @Password
end
go

create proc sp_GetUserByUsername
	@Username varchar(255)
as
begin
	select * from AppUsers where Username = @Username
end
go

