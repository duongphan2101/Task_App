using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Task_App.Model;

namespace Task_App.Helper
{
    public static class Zipper
    {
        public static string ZipFiles(List<TepTin> files, string maCongViec)
        {
            string zipPath = Path.Combine(Path.GetTempPath(), $"cv_{maCongViec}_{DateTime.Now.Ticks}.zip");
            using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    zip.CreateEntryFromFile(file.DuongDan, Path.GetFileName(file.DuongDan));
                }
            }
            return zipPath;
        }
        public static string CompressFile(string sourceFilePath, string targetFolder, string zipFileName)
        {
                string zipPath = Path.Combine(targetFolder, zipFileName + ".zip");

                using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Create))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    {
                        string fileName = Path.GetFileName(sourceFilePath);
                        archive.CreateEntryFromFile(sourceFilePath, fileName, CompressionLevel.Optimal);
                    }
                }

                return zipPath;
        }

    }
}
