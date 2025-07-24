using System;
using System.Windows.Forms;
using Task_App.TaskApp_Dao;

namespace Task_App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static TcpClientDAO TcpDao;
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TcpDao = new TcpClientDAO();
            bool connected = TcpDao.Connect("127.0.0.1", 5000);

            if (!connected)
            {
                MessageBox.Show("Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng hoặc server đang chạy!", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new FormLogin(TcpDao));
        }
    }


}
