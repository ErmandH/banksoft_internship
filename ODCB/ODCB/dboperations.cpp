
#include "dboperations.h"

void checkError(SQLRETURN ret, SQLSMALLINT handleType, SQLHANDLE handle, const char* msg) {
    if (ret != SQL_SUCCESS && ret != SQL_SUCCESS_WITH_INFO) {
        SQLWCHAR sqlState[6], errorMsg[SQL_MAX_MESSAGE_LENGTH];
        SQLINTEGER nativeError;
        SQLSMALLINT textLength;

        SQLGetDiagRec(handleType, handle, 1, sqlState, &nativeError, errorMsg, sizeof(errorMsg), &textLength);
        fprintf(stderr, "%s: %s (%d)\n", msg, errorMsg, nativeError);
        scanf("%s");
        exit(EXIT_FAILURE);
     }
}

void  connectToDatabase(SQLWCHAR* connString, SQLHENV& hEnv, SQLHDBC& hDbc){
    SQLRETURN ret;
    // Environment Handle oluþturma
    ret = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &hEnv);
    checkError(ret, SQL_HANDLE_ENV, hEnv, "Error allocating environment handle");

    ret = SQLSetEnvAttr(hEnv, SQL_ATTR_ODBC_VERSION, (void*)SQL_OV_ODBC3, 0);
    checkError(ret, SQL_HANDLE_ENV, hEnv, "Error setting ODBC version");

    // Connection baþlatma
    ret = SQLAllocHandle(SQL_HANDLE_DBC, hEnv, &hDbc);
    checkError(ret, SQL_HANDLE_DBC, hDbc, "Error allocating connection handle");


    // Baðlanma
    ret = SQLDriverConnect(hDbc, NULL, (SQLWCHAR*)connString, SQL_NTS, NULL, 0, NULL, SQL_DRIVER_NOPROMPT);
    checkError(ret, SQL_HANDLE_DBC, hDbc, "Error connecting to database");
}

void      printPatients(SQLHDBC& hDbc)
{
    SQLRETURN ret;
    // Statement handle, queryler için
    SQLHSTMT hStmt = NULL;

    SQLWCHAR sqlQuery[] = L"SELECT * FROM Patients";

    //Statement handle olusturma
    ret = SQLAllocHandle(SQL_HANDLE_STMT, hDbc, &hStmt);
    checkError(ret, SQL_HANDLE_STMT, hStmt, "Error allocating statement handle");

    ret = SQLExecDirect(hStmt, (SQLWCHAR*)sqlQuery, SQL_NTS);
    checkError(ret, SQL_HANDLE_STMT, hStmt, "Error executing SQL query");

    t_patient p;

    while (SQLFetch(hStmt) == SQL_SUCCESS) {
        SQLGetData(hStmt, 1, SQL_C_LONG, &(p.patient_id), 0, NULL);    
        SQLGetData(hStmt, 2, SQL_C_CHAR, p.first_name, sizeof(p.first_name), NULL);
        SQLGetData(hStmt, 3, SQL_C_CHAR, p.last_name, sizeof(p.last_name), NULL);
        SQLGetData(hStmt, 4, SQL_C_CHAR, p.gender, sizeof(p.gender), NULL);
        SQLGetData(hStmt, 5, SQL_C_CHAR, p.phone_number, sizeof(p.phone_number), NULL);

        printf("ID: %d, FirstName: %s, LastName: %s, Gender: %s, PhoneNumber: %s\n",
           p.patient_id, p.first_name, p.last_name, p.gender, p.phone_number);
    }
    printf("\n\n\n");
    SQLFreeHandle(SQL_HANDLE_STMT, hStmt);
}



void   addPatient(t_patient patient, SQLHDBC& hDbc)
{
    SQLHSTMT hStmt = NULL;
    SQLRETURN ret;
    //Statement handle olusturma
    ret = SQLAllocHandle(SQL_HANDLE_STMT, hDbc, &hStmt);    

    SQLWCHAR sqlQuery[] = L"INSERT INTO Patients (FirstName, LastName, Gender, PhoneNumber) VALUES (?, ?, ?, ?)";

    // SQL sorgusunu hazýrlama
    ret = SQLPrepare(hStmt, sqlQuery, SQL_NTS);

    // Parametreleri baðlama
    ret = SQLBindParameter(hStmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_VARCHAR, sizeof(patient.first_name), 0, patient.first_name, sizeof(patient.first_name), NULL);
    ret = SQLBindParameter(hStmt, 2, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_VARCHAR, sizeof(patient.last_name), 0, patient.last_name, sizeof(patient.last_name), NULL);
    ret = SQLBindParameter(hStmt, 3, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_VARCHAR, sizeof(patient.gender), 0, patient.gender, sizeof(patient.gender), NULL);
    ret = SQLBindParameter(hStmt, 4, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_VARCHAR, sizeof(patient.phone_number), 0, patient.phone_number, sizeof(patient.phone_number), NULL);
    
    ret = SQLExecute(hStmt);

    ret = SQLFreeHandle(SQL_HANDLE_STMT, hStmt);

    std::cout << "Patient added successfully." << std::endl;
}

