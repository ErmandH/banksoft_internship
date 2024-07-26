//#include <stdio.h>
//#include <stdlib.h>
//#include <windows.h>
//#include <sql.h>
//#include <sqlext.h>
//
//// Hata kontrolü için yardýmcý fonksiyon
//void checkError(SQLRETURN ret, SQLSMALLINT handleType, SQLHANDLE handle, const char* msg) {
//    if (ret != SQL_SUCCESS && ret != SQL_SUCCESS_WITH_INFO) {
//        SQLWCHAR sqlState[6], errorMsg[SQL_MAX_MESSAGE_LENGTH];
//        SQLINTEGER nativeError;
//        SQLSMALLINT textLength;
//
//        SQLGetDiagRec(handleType, handle, 1, sqlState, &nativeError, errorMsg, sizeof(errorMsg), &textLength);
//        fprintf(stderr, "%s: %s (%d)\n", msg, errorMsg, nativeError);
//        scanf("%s");
//        exit(EXIT_FAILURE);
//    }
//}
//
//
//
//
//int main() {
//    // ODBC ortam handle'ý. ODBC iþlevlerinin çalýþabilmesi için gerekli.
//    SQLHENV hEnv = NULL;
//
//    // Veri tabaný baðlantý handle'ý. Belirli bir veri tabanýna baðlanmak için kullanýlýr.
//    SQLHDBC hDbc = NULL;
//
//    // Statement handle, queryler için
//    SQLHSTMT hStmt = NULL;
//
//    // Fonksiyonlarýn dönüþ deðerini tutar
//    SQLRETURN ret;
//    
//    // Environment Handle oluþturma
//    ret = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &hEnv);
//    checkError(ret, SQL_HANDLE_ENV, hEnv, "Error allocating environment handle");
//
//    ret = SQLSetEnvAttr(hEnv, SQL_ATTR_ODBC_VERSION, (void*)SQL_OV_ODBC3, 0);
//    checkError(ret, SQL_HANDLE_ENV, hEnv, "Error setting ODBC version");
//
//    // Connection baþlatma
//    ret = SQLAllocHandle(SQL_HANDLE_DBC, hEnv, &hDbc);
//    checkError(ret, SQL_HANDLE_DBC, hDbc, "Error allocating connection handle");
//
//    // Baðlantý dizesi
//    SQLWCHAR connectionString[] = L"Provider=MSDASQL;DRIVER={SQL Server};SERVER=BSS02;DATABASE=BPersoEgitim;Trusted_Connection=SSPI;";
//
//    // Baðlanma
//    ret = SQLDriverConnect(hDbc, NULL, (SQLWCHAR*)connectionString, SQL_NTS, NULL, 0, NULL, SQL_DRIVER_NOPROMPT);
//    checkError(ret, SQL_HANDLE_DBC, hDbc, "Error connecting to database");
//
//    // 3. SQL ifadesi (statement) hazýrlama ve çalýþtýrma
//    ret = SQLAllocHandle(SQL_HANDLE_STMT, hDbc, &hStmt);
//    checkError(ret, SQL_HANDLE_STMT, hStmt, "Error allocating statement handle");
//
//    SQLWCHAR sqlQuery[] = L"SELECT * FROM Patients";
//    ret = SQLExecDirect(hStmt, (SQLWCHAR*)sqlQuery, SQL_NTS);
//    checkError(ret, SQL_HANDLE_STMT, hStmt, "Error executing SQL query");
//
//    // 4. Sonuçlarý iþleme
//    SQLINTEGER id;
//    SQLCHAR name[256];
//    while (SQLFetch(hStmt) == SQL_SUCCESS) {
//        ret = SQLGetData(hStmt, 1, SQL_C_LONG, &id, 0, NULL);
//        checkError(ret, SQL_HANDLE_STMT, hStmt, "Error fetching id");
//        
//        ret = SQLGetData(hStmt, 2, SQL_C_CHAR, name, sizeof(name), NULL);
//        checkError(ret, SQL_HANDLE_STMT, hStmt, "Error fetching name");
//
//        printf("ID: %d, Name: %s\n", id, name);
//    }
//
//    // 5. Baðlantýyý kapatma
//    SQLFreeHandle(SQL_HANDLE_STMT, hStmt);
//    SQLDisconnect(hDbc);
//    SQLFreeHandle(SQL_HANDLE_DBC, hDbc);
//    SQLFreeHandle(SQL_HANDLE_ENV, hEnv);
//
//    scanf("%s");
//    return 0;
//}
