using DevExpress.DashboardCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using Task_App.DTO;
using Task_App.TaskApp_Dao;
using DevExpress.DashboardWin;

namespace Task_App.views
{
    public partial class Dashboard_Express : DevExpress.DashboardCommon.Dashboard
    {
        internal object dateFilterDashboardItem;
        private TcpClientDAO _dao;
        private int _maNguoiDung;
        private DateTime _currentStartDate;
        private DateTime _currentEndDate;

        public Dashboard_Express(TcpClientDAO dao, int maNguoiDung)
        {
            InitializeComponent();

            _dao = dao;
            _maNguoiDung = maNguoiDung;

            // Set default date range
            _currentStartDate = DateTime.Today.AddDays(-30);
            _currentEndDate = DateTime.Today;

            // Setup DateFilter
            LoadDateFilter();

            // Load initial data
            LoadDataWithDateRange(_currentStartDate, _currentEndDate);
        }

        private void LoadDateFilter()
        {
            // Setup DateFilter properties
            this.dateFilterDashboardItem1.ComponentName = "dateFilterDashboardItem1";
            this.dateFilterDashboardItem1.DataItemRepository.Clear();
            this.dateFilterDashboardItem1.Name = "Date Filter";
            this.dateFilterDashboardItem1.ParentContainer = this.dashboardItemGroup2;

            this.dateFilterDashboardItem1.FilterType = DateFilterType.Between;

            this.dateFilterDashboardItem1.DateTimePeriods.Clear();
            this.dateFilterDashboardItem1.DateTimePeriods.AddRange(new DateTimePeriod[] {
                //DateTimePeriod.CreateLastWeek(),
                DateTimePeriod.CreateLastMonth(),
                DateTimePeriod.CreateThisMonth(),
                DateTimePeriod.CreateThisYear(),
                DateTimePeriod.CreateLastYear()
            });

            // Create a simple data source cho DateFilter để có thể interact
            var dateData = new List<DateModel>();
            for (int i = 0; i < 365; i++)
            {
                dateData.Add(new DateModel { Date = DateTime.Today.AddDays(-i) });
            }

            var dateDataSource = new DashboardObjectDataSource("DateData", typeof(DateModel));
            dateDataSource.DataSource = dateData;

            if (!this.DataSources.Any(ds => ds.ComponentName == dateDataSource.ComponentName))
                this.DataSources.Add(dateDataSource);

            this.dateFilterDashboardItem1.DataSource = dateDataSource;
            this.dateFilterDashboardItem1.Dimension = new Dimension("Date");
        }

        private void LoadDataWithDateRange(DateTime startDate, DateTime endDate)
        {
            _currentStartDate = startDate;
            _currentEndDate = endDate;

            // Gọi các method từ DAO với tham số date range
            int cxl = _dao.SoTaskChuaXuLi(_maNguoiDung);
            int dxl = _dao.SoTaskdangXuLi(_maNguoiDung);
            int ht = _dao.SoTaskDaHoanThanh(_maNguoiDung);
            int tre = _dao.SoTaskTreHan(_maNguoiDung);

            int ht_filter = _dao.SoTaskDaHoanThanhByFilter(_maNguoiDung, startDate, endDate);
            int tre_filter = _dao.SoTaskTreHanByFilter(_maNguoiDung, startDate, endDate);

            // Reload charts với data mới
            LoadPieChart(cxl, dxl, ht, tre);
            LoadBarChart(ht_filter, tre_filter);
        }

        private void LoadBarChart(int ht_filter, int tre_filter)
        {
            var dataSource = new DashboardObjectDataSource("BarChartData", typeof(BarChartModel));
            dataSource.DataSource = GetBarChartData(ht_filter, tre_filter);

            Console.WriteLine($"BarChartData: {ht_filter} - {tre_filter}");


            if (this.DataSources.Any(ds => ds.ComponentName == dataSource.ComponentName))
                this.DataSources.Remove(dataSource.ComponentName);

            this.DataSources.Add(dataSource);

            chartDashboardItem1.DataSource = dataSource;
            chartDashboardItem1.Arguments.Clear();
            chartDashboardItem1.Panes[0].Series.Clear();

            chartDashboardItem1.Arguments.Add(new Dimension("TrangThai"));
            chartDashboardItem1.Panes[0].Series.Add(new SimpleSeries(SimpleSeriesType.Bar)
            {
                Value = new Measure("SoLuong")
            });
        }

        private void LoadPieChart(int cxl, int dxl, int ht, int tre)
        {
            var dataSource = new DashboardObjectDataSource("PieChartData", typeof(PieChartModel));
            dataSource.DataSource = GetPieChartData(cxl, dxl, ht, tre);

            if (this.DataSources.Any(ds => ds.ComponentName == dataSource.ComponentName))
                this.DataSources.Remove(dataSource.ComponentName);

            this.DataSources.Add(dataSource);

            pieDashboardItem1.DataSource = dataSource;
            pieDashboardItem1.Arguments.Clear();
            pieDashboardItem1.Values.Clear();

            pieDashboardItem1.Arguments.Add(new Dimension("TrangThai"));
            pieDashboardItem1.Values.Add(new Measure("SoLuong"));
        }

        private List<PieChartModel> GetPieChartData(int cxl, int dxl, int ht, int tre)
        {
            return new List<PieChartModel>
            {
                new PieChartModel { TrangThai = "Chưa xử lý", SoLuong = cxl },
                new PieChartModel { TrangThai = "Đang xử lý", SoLuong = dxl },
                new PieChartModel { TrangThai = "Hoàn thành", SoLuong = ht },
                new PieChartModel { TrangThai = "Trễ hạn", SoLuong = tre }
            };
        }

        private List<BarChartModel> GetBarChartData(int ht, int tre)
        {
            return new List<BarChartModel>
            {
                new BarChartModel { TrangThai = "Đã Hoàn Thành", SoLuong = ht },
                new BarChartModel { TrangThai = "Trễ Hạn", SoLuong = tre }
            };
        }

        // Public methods để control từ bên ngoài
        public DateRange GetCurrentDateRange()
        {
            return new DateRange
            {
                StartDate = _currentStartDate,
                EndDate = _currentEndDate
            };
        }

        public void SetDateRange(DateTime startDate, DateTime endDate)
        {
            LoadDataWithDateRange(startDate, endDate);
        }

        public void SetDateTimePeriod(string periodName)
        {
            DateTime startDate, endDate;
            var now = DateTime.Now;

            switch (periodName.ToLower())
            {
                case "thismonth":
                    startDate = new DateTime(now.Year, now.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                    break;
                case "lastmonth":
                    startDate = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    endDate = new DateTime(now.Year, now.Month, 1).AddDays(-1);
                    break;
                case "thisyear":
                    startDate = new DateTime(now.Year, 1, 1);
                    endDate = new DateTime(now.Year, 12, 31);
                    break;
                case "lastyear":
                    startDate = new DateTime(now.Year - 1, 1, 1);
                    endDate = new DateTime(now.Year - 1, 12, 31);
                    break;
                case "thisweek":
                    int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
                    startDate = now.AddDays(-1 * diff).Date;
                    endDate = startDate.AddDays(6);
                    break;
                case "lastweek":
                    int diffLast = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
                    startDate = now.AddDays(-1 * diffLast - 7).Date;
                    endDate = startDate.AddDays(6);
                    break;
                default:
                    startDate = now.AddDays(-30);
                    endDate = now;
                    break;
            }

            SetDateRange(startDate, endDate);
        }

        public void RefreshData()
        {
            LoadDataWithDateRange(_currentStartDate, _currentEndDate);
        }
    }

    // Helper models
    public class DateRange
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class DateModel
    {
        public DateTime Date { get; set; }
    }
}