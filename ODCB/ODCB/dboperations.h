#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include <sql.h>
#include <sqlext.h>
#include <iostream>

#ifndef DBOPERATIONS_H
#define DBOPERATIONS_H


typedef struct s_patient
{
    SQLINTEGER patient_id;
    SQLCHAR first_name[256];
    SQLCHAR last_name[256];
    SQLCHAR gender[256];
    SQLCHAR phone_number[256];
} t_patient;


void	connectToDatabase(SQLWCHAR* connString, SQLHENV& hEnv, SQLHDBC& hDbc);
void	printPatients(SQLHDBC& hDbc);
void	addPatient(t_patient patient, SQLHDBC& hDbc);

#endif