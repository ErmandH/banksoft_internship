select AppointmentID, AppointmentDateTime, Patients.FirstName + ' ' + Patients.LastName AS PatientName, Doctors.FirstName + ' ' + Doctors.LastName AS DoctorName, Doctors.Branch, Notes from Appointments join Patients on Appointments.PatientID = Patients.PatientID join Doctors on Appointments.DoctorID = Doctors.DoctorID


UPDATE Appointments SET PatientID = 3, DoctorID = 1, AppointmentDateTime = '03/07/2028', Notes = 'dasdadasdad' WHERE AppointmentID = 10