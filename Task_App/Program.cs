using System;
using System.Windows.Forms;
using Task_App.DTO;
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

            Duong_Dan.Init();

            var apiDao = new ApiClientDAO();
            Application.Run(new FormLogin(apiDao));
            
        }



    }


}
