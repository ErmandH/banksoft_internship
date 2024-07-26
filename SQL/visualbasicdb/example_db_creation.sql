-- Ermand Haruni 02/07/2024



-- Patient table creation

if exists (select 1 from sys.tables where name = 'Patients')
	drop table Patients

create table Patients (
	PatientID int identity(1,1) PRIMARY KEY,
	FirstName varchar(100),
	LastName varchar(100),
	Gender varchar(5),
	PhoneNumber varchar(30)
)


-- Doctor table creation
if exists (select 1 from sys.tables where name = 'Doctors')
	drop table Doctors

create table Doctors (
	DoctorID int identity(1,1) PRIMARY KEY,
    FirstName varchar(50),
    LastName varchar(50),
    PhoneNumber varchar(30),
    Email varchar(100),
	Branch varchar(100),
)


-- Appointment table creation
if exists (select 1 from sys.tables where name = 'Appointments')
	drop table Appointments

create table Appointments (
	AppointmentID int identity(1,1) PRIMARY KEY,
    PatientID int,
    DoctorID int,
    AppointmentDateTime datetime,
    Notes varchar(MAX),
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
)


-- Users table creation
if exists (select 1 from sys.tables where name = 'AppUsers')
	drop table AppUsers

create table AppUsers (
	UserID int identity(1,1) PRIMARY KEY,
    Username varchar(255),
	Password varchar(255)
)