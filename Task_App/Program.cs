using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_App.TaskApp_Dao;
using Task_App.views;

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

            var apiDao = new ApiClientDAO();
            Application.Run(new FormLogin(apiDao));

        }



    }


}
