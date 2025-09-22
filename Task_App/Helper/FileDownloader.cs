using System;
using System.IO;
using System.Threading.Tasks;
using Task_App.TaskApp_Dao;

namespace Task_App.Helper
{
    public static class FileDownloader
    {
        public static async Task<bool> SaveFileAsync(ApiClientDAO api, string filePath, string originalFileName, string saveFolder = null)
        {
            if (string.IsNullOrEmpty(saveFolder))
            {
                saveFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Downloads"
                );
            }

            var fileBytes = await api.DownloadFileAsync(filePath, originalFileName);

            if (fileBytes != null)
            {
                Directory.CreateDirectory(saveFolder);

                string savePath = Path.Combine(saveFolder, originalFileName);

                File.WriteAllBytes(savePath, fileBytes);

                Console.WriteLine("File đã tải về: " + savePath);
                return true;
            }

            return false;
        }
    }

}
