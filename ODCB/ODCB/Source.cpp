#include "dboperations.h"

int main() {
    // ODBC ortam handle'ý. ODBC iþlevlerinin çalýþabilmesi için gerekli.
    SQLHENV hEnv = NULL;

    // Veri tabaný baðlantý handle'ý. Belirli bir veri tabanýna baðlanmak için kullanýlýr.
    SQLHDBC hDbc = NULL;

    // Fonksiyonlarýn dönüþ deðerini tutar
    SQLRETURN ret;
    
    SQLWCHAR connectionString[] = L"Provider=MSDASQL;DRIVER={SQL Server};SERVER=BSS02;DATABASE=BPersoEgitim;Trusted_Connection=SSPI;";
    connectToDatabase(connectionString, hEnv, hDbc);
    printPatients(hDbc);

    t_patient patient;
    strcpy((char*)patient.first_name, "John");
    strcpy((char*)patient.last_name, "Doe");
    strcpy((char*)patient.gender, "Male");
    strcpy((char*)patient.phone_number, "123456789");
    addPatient(patient, hDbc);
    printf("\n\n\n");
    printPatients(hDbc);
    // 5. Baðlantýyý kapatma
    SQLDisconnect(hDbc);
    SQLFreeHandle(SQL_HANDLE_DBC, hDbc);
    SQLFreeHandle(SQL_HANDLE_ENV, hEnv);

    scanf("%s");
    return 0;
}
