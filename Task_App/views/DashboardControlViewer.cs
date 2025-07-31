using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using System;
using System.Linq;
using System.Windows.Forms;
using Task_App.TaskApp_Dao;
using Task_App.views;

namespace Task_App
{
    public partial class DashboardControlViewer : UserControl
    {
        private TcpClientDAO tcpClientDAO;
        private int maNguoiDung;
        private Dashboard_Express dashboard;
        public DashboardControlViewer(TcpClientDAO dao, int maNguoiDung)
        {
            InitializeComponent();
            this.tcpClientDAO = dao;
            this.maNguoiDung = maNguoiDung;

            LoadDashboard();
        }

        private void LoadDashboard()
        {
            viewer.Dock = DockStyle.Fill;

            dashboard = new Dashboard_Express(tcpClientDAO, maNguoiDung);
            viewer.Dashboard = dashboard;

            var range = dashboard.GetCurrentDateRange();

            dashboard.SetDateRange(DateTime.Today.AddDays(-7), DateTime.Today);

            dashboard.SetDateTimePeriod("thismonth");
            dashboard.RefreshData();

            //dashboard.OnDateFilterChanged(newStartDate, newEndDate);
            this.Controls.Add(viewer);
        }

    }
}

