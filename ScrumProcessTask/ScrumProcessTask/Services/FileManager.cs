using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banksoft.Logging;

namespace ScrumProcessTask.Services
{
    public class FileManager
    {

        public List<string> GetExcelFilesUnderDirectory(string dirPath)
        {
            string[] excelExtensions = { "*.xls", "*.xlsx" };
            // çakışan dosyaları silmek için
            HashSet<string> excelFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (string ext in excelExtensions)
            {
                var files = Directory.GetFiles(dirPath, ext, SearchOption.TopDirectoryOnly);

                if (files != null)
                {
                    foreach (string file in files)
                    {
                        // Geçici dosyaları alma
                        if (!Path.GetFileName(file).StartsWith("~$"))
                        {
                            Logger.Log2TraceFile("Dosya bulundu: " + Path.GetFileName(file));
                            excelFiles.Add(file);
                        }
                    }
                }
            }
            return new List<string>(excelFiles);
        }

        public void MoveFilesToFolder(List<string> files, string dirPath, string moveDir)
        {
            foreach (string file in files)
                MoveFileToFolder(file, dirPath, moveDir);
        }

        public void MoveFileToFolder(string file, string dirPath, string moveDir)
        {
            string moveFolderPath = Path.Combine(dirPath, moveDir);

            // klasör yoksa oluştur
            if (!Directory.Exists(moveFolderPath))
            {
                Directory.CreateDirectory(moveFolderPath);
            }

            string destFilePath = Path.Combine(moveFolderPath, Path.GetFileName(file));
            try
            {
                File.Move(file, destFilePath);
                Logger.Log2TraceFile(string.Format("Dosya taşındı: {0} -> {1}", file, destFilePath));
            }
            catch (Exception ex)
            {
                Logger.Log2TraceFile(string.Format("Dosya taşınamadı: {0}. Hata: {1}", file, ex.Message));
				throw ex;
            }

        }
    }
}
