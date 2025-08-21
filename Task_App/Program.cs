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
        //static void Main()
        //{

        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);

        //    TcpDao = new TcpClientDAO();
        //    bool connected = TcpDao.Connect("127.0.0.1", 5000);

        //    if (!connected)
        //    {
        //        MessageBox.Show("Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng hoặc server đang chạy!", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    Application.Run(new FormLogin(TcpDao));
        //}

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var apiDao = new ApiClientDAO();
            Application.Run(new FormLogin(apiDao));
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //var form = new Form { Width = 800, Height = 600 };

            //// Thêm một Panel để hỗ trợ scroll
            //var panel = new Panel
            //{
            //    Dock = DockStyle.Fill,
            //    AutoScroll = true
            //};

            //var timeline = new TimelineControl
            //{
            //    Dock = DockStyle.Fill,
            //};
            //panel.Controls.Add(timeline);
            //form.Controls.Add(panel);

            //Application.Run(form);

        }



    }


}
