using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Task_App.Response;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class Dashboard : UserControl
    {
        private ApiClientDAO apiClientDAO;
        private int maNguoiDung;
        private DateTime start;
        private DateTime end;
        private Dashboard_data data;

        public Dashboard(ApiClientDAO apiClientDAO, int maNguoiDung)
        {
            InitializeComponent();
            this.apiClientDAO = apiClientDAO;
            this.maNguoiDung = maNguoiDung;

            start = DateTime.Now.AddDays(-30);
            end = DateTime.Now;

            startDate.Value = start;
            endDate.Value = end;

            startDate_dagiao.Value = start;
            endDate_dagiao.Value = end;

            SetDataDashboard();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private async Task SetDataDashboard()
        {
            var resTaskTrongTuan = await apiClientDAO.SoTaskTrongTuan(maNguoiDung);
            var resTaskTrongThang = await apiClientDAO.SoTaskTrongThang(maNguoiDung);
            var resTaskTrongNam = await apiClientDAO.SoTaskTrongNam(maNguoiDung);

            var resTaskDaGiaoTrongTuan = await apiClientDAO.SoTaskDaGiaoTrongTuan(maNguoiDung);
            var resTaskDaGiaoTrongThang = await apiClientDAO.SoTaskDaGiaoTrongThang(maNguoiDung);
            var resTaskDaGiaoTrongNam = await apiClientDAO.SoTaskDaGiaoTrongNam(maNguoiDung);

            var resTaskChuaXuLi = await apiClientDAO.SoTaskChuaXuLiByFilter(maNguoiDung, start, end);
            var resTaskDangXuLi = await apiClientDAO.SoTaskDangXuLiByFilter(maNguoiDung, start, end);
            var resTaskHoanThanh = await apiClientDAO.SoTaskHoanThanhByFilter(maNguoiDung, start, end);
            var resTaskTre = await apiClientDAO.SoTaskTreByFilter(maNguoiDung, start, end);

            var resTaskDaGiaoChuaXuLi = await apiClientDAO.SoTaskDaGiaoChuaXuLiByFilter(maNguoiDung, start, end);
            var resTaskDaGiaoDangXuLi = await apiClientDAO.SoTaskDaGiaoDangXuLiByFilter(maNguoiDung, start, end);
            var resTaskDaGiaoHoanThanh = await apiClientDAO.SoTaskDaGiaoHoanThanhByFilter(maNguoiDung, start, end);
            var resTaskDaGiaoTre = await apiClientDAO.SoTaskDaGiaoTreByFilter(maNguoiDung, start, end);

            data = new Dashboard_data
            {
                SoTaskTrongTuan = resTaskTrongTuan.Data,
                SoTaskTrongThang = resTaskTrongThang.Data,
                SoTaskTrongNam = resTaskTrongNam.Data,
                SoTaskDaGiaoTrongTuan = resTaskDaGiaoTrongTuan.Data,
                SoTaskDaGiaoTrongThang = resTaskDaGiaoTrongThang.Data,
                SoTaskDaGiaoTrongNam = resTaskDaGiaoTrongNam.Data,

                SoTaskChuaXuLiFillter = resTaskChuaXuLi.Data,
                SoTaskDangXuLiFillter = resTaskDangXuLi.Data,
                SoTaskHoanThanhFillter = resTaskHoanThanh.Data,
                SoTaskTreFillter = resTaskTre.Data,

                SoTaskDaGiaoChuaXuLiFillter = resTaskDaGiaoChuaXuLi.Data,
                SoTaskDaGiaoDangXuLiFillter = resTaskDaGiaoDangXuLi.Data,
                SoTaskDaGiaoHoanThanhFillter = resTaskDaGiaoHoanThanh.Data,
                SoTaskDaGiaoTreFillter = resTaskDaGiaoTre.Data,
            };

        }

        private async void loadData()
        {
            var resTaskTrongTuan = await apiClientDAO.SoTaskTrongTuan(maNguoiDung);
            var resTaskTrongThang = await apiClientDAO.SoTaskTrongThang(maNguoiDung);
            var resTaskTrongNam = await apiClientDAO.SoTaskTrongNam(maNguoiDung);

            var resTaskDaGiaoTrongTuan = await apiClientDAO.SoTaskDaGiaoTrongTuan(maNguoiDung);
            var resTaskDaGiaoTrongThang = await apiClientDAO.SoTaskDaGiaoTrongThang(maNguoiDung);
            var resTaskDaGiaoTrongNam = await apiClientDAO.SoTaskDaGiaoTrongNam(maNguoiDung);

            var resTaskChuaXuLi = await apiClientDAO.SoTaskChuaXuLiByFilter(maNguoiDung, start, end);
            var resTaskDangXuLi = await apiClientDAO.SoTaskDangXuLiByFilter(maNguoiDung, start, end);
            var resTaskHoanThanh = await apiClientDAO.SoTaskHoanThanhByFilter(maNguoiDung, start, end);
            var resTaskTre = await apiClientDAO.SoTaskTreByFilter(maNguoiDung, start, end);

            var resTaskDaGiaoChuaXuLi = await apiClientDAO.SoTaskDaGiaoChuaXuLiByFilter(maNguoiDung, start, end);
            var resTaskDaGiaoDangXuLi = await apiClientDAO.SoTaskDaGiaoDangXuLiByFilter(maNguoiDung, start, end);
            var resTaskDaGiaoHoanThanh = await apiClientDAO.SoTaskDaGiaoHoanThanhByFilter(maNguoiDung, start, end);
            var resTaskDaGiaoTre = await apiClientDAO.SoTaskDaGiaoTreByFilter(maNguoiDung, start, end);

            int soTaskTrongTuan = resTaskTrongTuan.Data;
            int soTaskTrongThang = resTaskTrongThang.Data;
            int soTaskTrongNam = resTaskTrongNam.Data;

            int soTaskDaGiaoTrongTuan = resTaskDaGiaoTrongTuan.Data;
            int soTaskDaGiaoTrongThang = resTaskDaGiaoTrongThang.Data;
            int soTaskDaGiaoTrongNam = resTaskDaGiaoTrongNam.Data;

            int soTaskcxl = resTaskChuaXuLi.Data;
            int soTaskdxl = resTaskDangXuLi.Data;
            int soTaskHoanThanh = resTaskHoanThanh.Data;
            int soTaskTreHan = resTaskTre.Data;       

            int soTaskcxl_dagiao = resTaskDaGiaoChuaXuLi.Data;
            int soTaskdxl_dagiao = resTaskDaGiaoDangXuLi.Data;
            int soTaskHoanThanh_dagiao = resTaskDaGiaoHoanThanh.Data;
            int soTaskTreHan_dagiao = resTaskDaGiaoTre.Data;

            LoadChartData(soTaskcxl, soTaskdxl, soTaskHoanThanh, soTaskTreHan);
            LoadPieChart(soTaskcxl, soTaskdxl, soTaskHoanThanh, soTaskTreHan);

            //LoadChartData_dagiao(soTaskcxl_dagiao, soTaskdxl_dagiao, soTaskHoanThanh_dagiao, soTaskTreHan_dagiao);
            LoadTimeLine();
            LoadPieChart_dagiao(soTaskcxl_dagiao, soTaskdxl_dagiao, soTaskHoanThanh_dagiao, soTaskTreHan_dagiao);

            panel_item2_1.Text = "Trong Tuần";
            panel_item2_2.Text = soTaskTrongTuan.ToString() + " Công việc";
            panel_item3_2.Text = soTaskTrongThang.ToString() + " Công việc";
            panel_item4_2.Text = soTaskTrongNam.ToString() + " Công việc";

            panel_item2_2_dagiao.Text = soTaskDaGiaoTrongTuan.ToString() + " Công việc";
            panel_item3_2_dagiao.Text = soTaskDaGiaoTrongThang.ToString() + " Công việc";
            panel_item4_2_dagiao.Text = soTaskDaGiaoTrongNam.ToString() + " Công việc";
        }

        private void LoadTimeLine()
        {
            TimelineControl timeline = new TimelineControl();
            timeline.BackColor = Color.White;
            // Add vào panel
            main_panel_panel_dagiao.Controls.Add(timeline);
            main_panel_panel_dagiao.Dock = DockStyle.Fill;
        }

        private void LoadChartData(int cxl, int dxl, int ht, int tre)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();

            chart1.Titles.Add("Số lượng");
            chart1.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chart1.Titles[0].ForeColor = Color.DimGray;

            ChartArea area = chart1.ChartAreas[0];
            area.AxisX.Title = "Trạng thái";
            area.AxisY.Title = "Số lượng công việc";
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 10);
            area.AxisX.TitleFont = new Font("Segoe UI", 11, FontStyle.Bold);
            area.AxisY.TitleFont = new Font("Segoe UI", 11, FontStyle.Bold);
            area.BackColor = Color.Transparent;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.Interval = 1;
            area.AxisX.IsMarginVisible = true;

            // Series duy nhất
            Series series = new Series("Task")
            {
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 9),
                ["PointWidth"] = "0.6"
            };

            // Thêm từng cột với màu riêng
            var dpTuan = series.Points.AddXY("Chưa Xử Lí", cxl);
            series.Points[dpTuan].Color = Color.MediumSlateBlue;

            var dpThang = series.Points.AddXY("Đang Xử Lí", dxl);
            series.Points[dpThang].Color = Color.MediumSeaGreen;

            var dpNam = series.Points.AddXY("Hoàn Thành", ht);
            series.Points[dpNam].Color = Color.LightSeaGreen;

            var dpTre = series.Points.AddXY("Trễ Hạn", tre);
            series.Points[dpTre].Color = Color.LightPink;

            chart1.Series.Add(series);
        }

        private void LoadPieChart(int cxl, int dxl, int ht, int tre)
        {
            chart2.Series.Clear();
            chart2.Titles.Clear();
            chart2.Legends.Clear();
            chart2.ChartAreas.Clear();

            chart2.Dock = DockStyle.Fill;

            ChartArea area = new ChartArea("PieArea");
            area.BackColor = Color.Transparent;
            area.Position = new ElementPosition(0, 0, 100, 100);
            area.InnerPlotPosition = new ElementPosition(15, 20, 70, 80);

            chart2.ChartAreas.Add(area);

            chart2.Legends.Add(new Legend("Legend")
            {
                Docking = Docking.Right,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.Transparent
            });

            chart2.Titles.Add("Tỷ lệ trạng thái công việc");
            chart2.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chart2.Titles[0].ForeColor = Color.DimGray;

            Series pieSeries = new Series("TaskStatus")
            {
                ChartType = SeriesChartType.Pie,
                Font = new Font("Segoe UI", 9),
                ["PieLabelStyle"] = "Inside",
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black
            };

            int total = cxl + dxl + ht + tre;

            pieSeries.Points.AddXY("Chưa xử lý", cxl);
            pieSeries.Points.AddXY("Đang xử lý", dxl);
            pieSeries.Points.AddXY("Hoàn thành", ht);
            pieSeries.Points.AddXY("Trễ hạn", tre);

            // Gán màu sau khi đã AddXY
            pieSeries.Points[0].Color = Color.LightGray;
            pieSeries.Points[1].Color = Color.SkyBlue;
            pieSeries.Points[2].Color = Color.LightGreen;
            pieSeries.Points[3].Color = Color.Salmon;

            for (int i = 0; i < pieSeries.Points.Count; i++)
            {
                double value = pieSeries.Points[i].YValues[0];
                double percent = total > 0 ? (value / total) * 100 : 0;

                // Label trong biểu đồ: chỉ phần trăm
                pieSeries.Points[i].Label = $"{percent:0.#}%";
                pieSeries.Points[i].LegendText = pieSeries.Points[i].AxisLabel;
                pieSeries.Points[i].ToolTip = $"{pieSeries.Points[i].AxisLabel}: {value} task";
            }


            chart2.Series.Add(pieSeries);
        }

        private void LoadChartData_dagiao(int cxl, int dxl, int ht, int tre)
        {
            chart1_dagiao.Series.Clear();
            chart1_dagiao.Titles.Clear();
            chart1_dagiao.Legends.Clear();

            chart1_dagiao.Titles.Add("Số lượng");
            chart1_dagiao.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chart1_dagiao.Titles[0].ForeColor = Color.DimGray;

            ChartArea area = chart1_dagiao.ChartAreas[0];
            area.AxisX.Title = "Trạng thái";
            area.AxisY.Title = "Số lượng công việc";
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 10);
            area.AxisX.TitleFont = new Font("Segoe UI", 11, FontStyle.Bold);
            area.AxisY.TitleFont = new Font("Segoe UI", 11, FontStyle.Bold);
            area.BackColor = Color.Transparent;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.Interval = 1;
            area.AxisX.IsMarginVisible = true;

            // Series duy nhất
            Series series = new Series("Task")
            {
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 9),
                ["PointWidth"] = "0.6"
            };

            // Thêm từng cột với màu riêng
            var dpTuan = series.Points.AddXY("Chưa Xử Lí", cxl);
            series.Points[dpTuan].Color = Color.MediumSlateBlue;

            var dpThang = series.Points.AddXY("Đang Xử Lí", dxl);
            series.Points[dpThang].Color = Color.MediumSeaGreen;

            var dpNam = series.Points.AddXY("Hoàn Thành", ht);
            series.Points[dpNam].Color = Color.LightSeaGreen;

            var dpTre = series.Points.AddXY("Trễ Hạn", tre);
            series.Points[dpTre].Color = Color.LightPink;

            chart1_dagiao.Series.Add(series);
        }

        private void LoadPieChart_dagiao(int cxl, int dxl, int ht, int tre)
        {
            chart2_dagiao.Series.Clear();
            chart2_dagiao.Titles.Clear();
            chart2_dagiao.Legends.Clear();
            chart2_dagiao.ChartAreas.Clear();

            chart2_dagiao.Dock = DockStyle.Fill;

            ChartArea area = new ChartArea("PieArea");
            area.BackColor = Color.Transparent;
            area.Position = new ElementPosition(0, 0, 100, 100);
            area.InnerPlotPosition = new ElementPosition(15, 20, 70, 80);

            chart2_dagiao.ChartAreas.Add(area);

            chart2_dagiao.Legends.Add(new Legend("Legend")
            {
                Docking = Docking.Right,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.Transparent
            });

            chart2_dagiao.Titles.Add("Tỷ lệ trạng thái công việc");
            chart2_dagiao.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chart2_dagiao.Titles[0].ForeColor = Color.DimGray;

            Series pieSeries = new Series("TaskStatus")
            {
                ChartType = SeriesChartType.Pie,
                Font = new Font("Segoe UI", 9),
                ["PieLabelStyle"] = "Inside",
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black
            };

            int total = cxl + dxl + ht + tre;

            pieSeries.Points.AddXY("Chưa xử lý", cxl);
            pieSeries.Points.AddXY("Đang xử lý", dxl);
            pieSeries.Points.AddXY("Hoàn thành", ht);
            pieSeries.Points.AddXY("Trễ hạn", tre);

            // Gán màu sau khi đã AddXY
            pieSeries.Points[0].Color = Color.LightGray;
            pieSeries.Points[1].Color = Color.SkyBlue;
            pieSeries.Points[2].Color = Color.LightGreen;
            pieSeries.Points[3].Color = Color.Salmon;

            for (int i = 0; i < pieSeries.Points.Count; i++)
            {
                double value = pieSeries.Points[i].YValues[0];
                double percent = total > 0 ? (value / total) * 100 : 0;

                // Label trong biểu đồ: chỉ phần trăm
                pieSeries.Points[i].Label = $"{percent:0.#}%";
                pieSeries.Points[i].LegendText = pieSeries.Points[i].AxisLabel;
                pieSeries.Points[i].ToolTip = $"{pieSeries.Points[i].AxisLabel}: {value} task";
            }


            chart2_dagiao.Series.Add(pieSeries);
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            int width = dashboard_panel.Width;
            main_panel.Width = (int)(width * 0.4);
            right_panel.Width = width - main_panel.Width;

            int width_dagiao = dashboard_panel_dagiao.Width;
            main_panel_dagiao.Width = (int)(width_dagiao * 0.4);
            right_panel_dagiao.Width = width_dagiao - main_panel_dagiao.Width;
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            start = startDate.Value;   
            loadData();
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            end = endDate.Value;
            loadData();
        }

        private void startDate_dagiao_ValueChanged(object sender, EventArgs e)
        {
            start = startDate_dagiao.Value;
            loadData();
        }

        private void endDate_dagiao_ValueChanged(object sender, EventArgs e)
        {
            end = endDate_dagiao.Value;
            loadData();
        }

        private void flow_Head_dagiao_Resize(object sender, EventArgs e)
        {
            int panelWidth = flow_Head_dagiao.Width / 3;
            int panelHeight = 100;

            head_panel_2_dagiao.Width = panelWidth;
            head_panel_3_dagiao.Width = panelWidth;
            head_panel_4_dagiao.Width = panelWidth;

            head_panel_2_dagiao.Height = panelHeight;
            head_panel_3_dagiao.Height = panelHeight;
            head_panel_4_dagiao.Height = panelHeight;
        }

        private void flow_Head_Resize(object sender, EventArgs e)
        {
            int panelWidth = flow_Head.Width / 3;
            int panelHeight = 100;

            head_panel_2.Width = panelWidth;
            head_panel_3.Width = panelWidth;
            head_panel_4.Width = panelWidth;

            head_panel_2.Height = panelHeight;
            head_panel_3.Height = panelHeight;
            head_panel_4.Height = panelHeight;
        }
    }
}
