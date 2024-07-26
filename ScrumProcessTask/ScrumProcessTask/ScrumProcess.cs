using System;
                                                                                                     
using System.Data;
using System.Data.SqlClient;

using System.Threading;

using Banksoft.DAL;
using Banksoft.Logging;

using Banksoft.WCFUtil;
using Banksoft.WSCallTasks;
using ScrumProcessTask.Services;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

namespace ScrumProcessTask
{
    public class ScrumProcess : WSCallTask
    {
        private BaseDAL _dalKrediKartlari;
        private ScrumManager scrumManager;
        private FileManager fileManager; 

        private string _code;


        public override bool Init(string taskName, int threadNo)
        {
            try
            {
                _dalKrediKartlari = new BaseDAL("SCRUMDB");
                scrumManager = new ScrumManager(_dalKrediKartlari);
                fileManager = new FileManager();
                return base.Init(taskName, threadNo);
            }
            catch (Exception excp)
            {
                Logger.Log2ErrorFile("ScrumProcessTask->ScrumProcess->Init()", excp);
                OnNotify("ScrumProcessTask->Init() Error!");

                return false;
            }
        }

        protected override WSClientParams GetWSClientParams(string code)
        {
            WSClientParams clientParams = new WSClientParams();

            try
            {
                _code = code;

                using (DataTable dt = (DataTable)_dalKrediKartlari.ExecuteFromXmlQuery("SelectWebServiceCallParam", new SqlParameter("@wsCode", code)))
                {
                    if (dt.Rows.Count == 0)
                    {
                        OnNotify(code + " GetWSClientParams() Error!");
                        Logger.Log2ErrorFile("ScrumProcessTask->ScrumProcess->GetWSClientParams(): " + code + " GetWSClientParams() Error!");

                        return clientParams;
                    }

                    clientParams = new WSClientParams()
                    {
                        Code = code,
                        BindingType = (enmBindingType)Convert.ToInt32(dt.Rows[0]["wsBindingType"]),
                        SecurityMode = (enmSecurityMod)Convert.ToInt32(dt.Rows[0]["wsSecurityMode"]),
                        URL = dt.Rows[0]["wsUrl"].ToString(),
                        Username = dt.Rows[0]["wsUserName"].ToString().Trim(),
                        Password = dt.Rows[0]["wsPassWord"].ToString().Trim(),
                        Certificate = "", // dt.Rows[0]["wsCertificate"].ToString(),
                        PoolingPeriod = 1000 * Convert.ToInt32(dt.Rows[0]["wsLoopWaitPeriodSec"]),
                        MaxParallelCallCount = Convert.ToInt32(dt.Rows[0]["wsMaxParallelCallCount"])
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Log2ErrorFile("ScrumProcessTask->ScrumProcess->GetWSClientParams() " + ex.ToString());
                OnNotify("ScrumProcessTask->ScrumProcess->GetWSClientParams() Error!");
            }

            return clientParams;
        }

        protected override DataTable GetMessageParams()
        {
            DataTable dataTable = new DataTable();
            string folderPath = ConfigurationManager.AppSettings["ScrumsPath"];
            try
            {
                List<string> files = fileManager.GetExcelFilesUnderDirectory(folderPath);
                if (files.Count < 1)
                    return dataTable;

                dataTable.Columns.Add("File", typeof(string));
                foreach (string file in files)
                {
                    DataRow row = dataTable.NewRow();
                    row["File"] = file;
                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Logger.Log2ErrorFile("ScrumProcessTask->ScrumProcess->GetMessageParams(): " + ex.ToString());
                OnNotify("ScrumProcessTask->ScrumProcess->GetMessageParams() Error!");
            }
            return dataTable;
        }

        protected override void ProcessMessage(DataTable messageParams)
        {
            if (messageParams == null || messageParams.Rows.Count < 1)
                return;

            // HATA klasörüne yollamak için bu değişkeni kullanıyorum
            string currentFile = null;
            // excel dosyalarının bulunduğu klasörün yolu
            string folderPath = ConfigurationManager.AppSettings["ScrumsPath"];
            try
            {
                List<string> files = new List<string>();
                // DataTable daki dosyaları files listesine koy
                foreach (DataRow row in messageParams.Rows)
                    files.Add(row["File"].ToString());

                foreach (var file in files)
                {
                    currentFile = file;
					Console.WriteLine("************** Working on " + Path.GetFileNameWithoutExtension(file) + " **************");
					try
					{
						// excel dosyasını veritabanına kaydetme işlemi
						scrumManager.InsertScrumsFromExcelFile(file);
					}
					catch (Exception ex)
					{
						fileManager.MoveFileToFolder(currentFile, folderPath, "HATA");
						continue;
					}
                    
                    Logger.Log2TraceFile(Path.GetFileName(file) + " veritabanına kaydedildi");
                    OnNotify(Path.GetFileName(file) + " veritabanına kaydedildi");

                    // dosyayı OKUNDU klasörüne kaydetme işlemi
                    fileManager.MoveFileToFolder(file, folderPath, "OKUNDU");
                }
                Logger.Log2TraceFile("Veritabanına kayıt işlemleri tamamlandı");
                OnNotify("Veritabanına kayıt işlemleri tamamlandı");
            }
            catch (Exception ex)
            {
                if (currentFile != null)
                    fileManager.MoveFileToFolder(currentFile, folderPath, "HATA");
                
                Logger.Log2ErrorFile("ScrumProcessTask->ScrumProcess->ProcessMessage(): " + ex.ToString());
                OnNotify("ScrumProcessTask->ScrumProcess->ProcessMessage() Error!");
            }
        }

        public void Run(string[] args)
        {
            Init("SCRUM_PROC", 1);
            DataTable dataTable = GetMessageParams();
            ProcessMessage(dataTable);
        }
    }
}
