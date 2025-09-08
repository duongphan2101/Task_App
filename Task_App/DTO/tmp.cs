using System;
using System.IO;

namespace Task_App.DTO
{
    public static class tmp
    {
        public static string mk {  get; set; }
    }

    public static class TmpPass
    {
        public static string Pwd { get; set; }
    }

    public static class Duong_Dan
        {
            private static readonly string configFile =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

            private static string _duongDan = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Attachments"
            );

            public static string DuongDan
            {
                get
                {
                    // Nếu chưa đọc thì load từ file
                    if (string.IsNullOrEmpty(_duongDan))
                        Load();
                    return _duongDan;
                }
                set
                {
                    _duongDan = value;
                    Save(_duongDan);
                }
            }

            public static void Init()
            {
                if (File.Exists(configFile))
                {
                    // Đọc đường dẫn từ file
                    _duongDan = File.ReadAllText(configFile).Trim();
                }
                else
                {
                    // Tạo folder mặc định
                    if (!Directory.Exists(_duongDan))
                        Directory.CreateDirectory(_duongDan);

                    // Lưu vào file config
                    Save(_duongDan);
                }
            }

            private static void Save(string path)
            {
                File.WriteAllText(configFile, path);
            }

            private static void Load()
            {
                if (File.Exists(configFile))
                    _duongDan = File.ReadAllText(configFile).Trim();
            }
        }

}
