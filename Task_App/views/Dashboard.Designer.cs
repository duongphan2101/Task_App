using DevExpress.Utils.Extensions;
using System.Collections.Generic;
using System.Windows.Forms;
using static Task_App.views.TimelineControl;

namespace Task_App.views
{
    partial class Dashboard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dashboard_panel = new Guna.UI2.WinForms.Guna2Panel();
            this.main_panel = new Guna.UI2.WinForms.Guna2Panel();
            this.main_panel_panel = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_timeLine = new Guna.UI2.WinForms.Guna2Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.startDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.endDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.right_panel = new Guna.UI2.WinForms.Guna2Panel();
            this.right_panel_panel = new Guna.UI2.WinForms.Guna2Panel();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.flow_Head = new System.Windows.Forms.FlowLayoutPanel();
            this.head_panel_2 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_panel_2 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.lblImage_2 = new System.Windows.Forms.Label();
            this.head_panel_item2 = new Guna.UI2.WinForms.Guna2Panel();
            this.head_panel_item1 = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_item2_2 = new System.Windows.Forms.Label();
            this.panel_item2_1 = new System.Windows.Forms.Label();
            this.lblSoTaskDangXuLi = new System.Windows.Forms.Label();
            this.head_panel_3 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_panel_3 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_item3 = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_item3_2 = new System.Windows.Forms.Label();
            this.panel_item3_1 = new System.Windows.Forms.Label();
            this.lblImage_3 = new System.Windows.Forms.Label();
            this.head_panel_4 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_panel_4 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_item4 = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_item4_2 = new System.Windows.Forms.Label();
            this.panel_item4_1 = new System.Windows.Forms.Label();
            this.lblImage_4 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dashboard_panel_dagiao = new Guna.UI2.WinForms.Guna2Panel();
            this.main_panel_dagiao = new Guna.UI2.WinForms.Guna2Panel();
            this.main_panel_panel_dagiao = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_timeLine_DaGiao = new Guna.UI2.WinForms.Guna2Panel();
            this.flowLayoutPanel1_dagiao = new System.Windows.Forms.FlowLayoutPanel();
            this.startDate_dagiao = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.endDate_dagiao = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.right_panel_dagiao = new Guna.UI2.WinForms.Guna2Panel();
            this.right_panel_panel_dagiao = new Guna.UI2.WinForms.Guna2Panel();
            this.chart2_dagiao = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.flow_Head_dagiao = new System.Windows.Forms.FlowLayoutPanel();
            this.head_panel_2_dagiao = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_panel_2_dagiao = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.lblImage_2_dagiao = new System.Windows.Forms.Label();
            this.head_panel_item2_dagiao = new Guna.UI2.WinForms.Guna2Panel();
            this.head_panel_item1_dagiao = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_item2_2_dagiao = new System.Windows.Forms.Label();
            this.panel_item2_1_dagiao = new System.Windows.Forms.Label();
            this.lblSoTaskDangXuLi_dagiao = new System.Windows.Forms.Label();
            this.head_panel_3_dagiao = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_panel_3_dagiao = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_item3_dagiao = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_item3_2_dagiao = new System.Windows.Forms.Label();
            this.panel_item3_1_dagiao = new System.Windows.Forms.Label();
            this.lblImage_3_dagiao = new System.Windows.Forms.Label();
            this.head_panel_4_dagiao = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_panel_4_dagiao = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.head_panel_item4_dagiao = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_item4_2_dagiao = new System.Windows.Forms.Label();
            this.panel_item4_1_dagiao = new System.Windows.Forms.Label();
            this.lblImage_4_dagiao = new System.Windows.Forms.Label();
            this.chart1_dagiao = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.dashboard_panel.SuspendLayout();
            this.main_panel.SuspendLayout();
            this.main_panel_panel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.right_panel.SuspendLayout();
            this.right_panel_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.flow_Head.SuspendLayout();
            this.head_panel_2.SuspendLayout();
            this.head_panel_panel_2.SuspendLayout();
            this.head_panel_item2.SuspendLayout();
            this.head_panel_item1.SuspendLayout();
            this.head_panel_3.SuspendLayout();
            this.head_panel_panel_3.SuspendLayout();
            this.head_panel_item3.SuspendLayout();
            this.head_panel_4.SuspendLayout();
            this.head_panel_panel_4.SuspendLayout();
            this.head_panel_item4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.dashboard_panel_dagiao.SuspendLayout();
            this.main_panel_dagiao.SuspendLayout();
            this.main_panel_panel_dagiao.SuspendLayout();
            this.flowLayoutPanel1_dagiao.SuspendLayout();
            this.right_panel_dagiao.SuspendLayout();
            this.right_panel_panel_dagiao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2_dagiao)).BeginInit();
            this.flow_Head_dagiao.SuspendLayout();
            this.head_panel_2_dagiao.SuspendLayout();
            this.head_panel_panel_2_dagiao.SuspendLayout();
            this.head_panel_item2_dagiao.SuspendLayout();
            this.head_panel_item1_dagiao.SuspendLayout();
            this.head_panel_3_dagiao.SuspendLayout();
            this.head_panel_panel_3_dagiao.SuspendLayout();
            this.head_panel_item3_dagiao.SuspendLayout();
            this.head_panel_4_dagiao.SuspendLayout();
            this.head_panel_panel_4_dagiao.SuspendLayout();
            this.head_panel_item4_dagiao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1_dagiao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dashboard_panel
            // 
            this.dashboard_panel.Controls.Add(this.main_panel);
            this.dashboard_panel.Controls.Add(this.right_panel);
            this.dashboard_panel.Controls.Add(this.flow_Head);
            this.dashboard_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboard_panel.FillColor = System.Drawing.Color.White;
            this.dashboard_panel.Location = new System.Drawing.Point(0, 0);
            this.dashboard_panel.Margin = new System.Windows.Forms.Padding(0);
            this.dashboard_panel.Name = "dashboard_panel";
            this.dashboard_panel.Size = new System.Drawing.Size(714, 464);
            this.dashboard_panel.TabIndex = 0;
            // 
            // main_panel
            // 
            this.main_panel.Controls.Add(this.main_panel_panel);
            this.main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel.FillColor = System.Drawing.Color.White;
            this.main_panel.Location = new System.Drawing.Point(0, 81);
            this.main_panel.Margin = new System.Windows.Forms.Padding(0);
            this.main_panel.Name = "main_panel";
            this.main_panel.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.main_panel.Size = new System.Drawing.Size(414, 383);
            this.main_panel.TabIndex = 3;
            // 
            // main_panel_panel
            // 
            this.main_panel_panel.Controls.Add(this.panel_timeLine);
            this.main_panel_panel.Controls.Add(this.flowLayoutPanel1);
            this.main_panel_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel_panel.FillColor = System.Drawing.Color.White;
            this.main_panel_panel.Location = new System.Drawing.Point(8, 8);
            this.main_panel_panel.Margin = new System.Windows.Forms.Padding(0);
            this.main_panel_panel.Name = "main_panel_panel";
            this.main_panel_panel.Size = new System.Drawing.Size(398, 367);
            this.main_panel_panel.TabIndex = 0;
            // 
            // panel_timeLine
            // 
            this.panel_timeLine.AutoScroll = true;
            this.panel_timeLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_timeLine.FillColor = System.Drawing.Color.White;
            this.panel_timeLine.Location = new System.Drawing.Point(0, 40);
            this.panel_timeLine.Margin = new System.Windows.Forms.Padding(0);
            this.panel_timeLine.Name = "panel_timeLine";
            this.panel_timeLine.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel_timeLine.Size = new System.Drawing.Size(398, 327);
            this.panel_timeLine.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.startDate);
            this.flowLayoutPanel1.Controls.Add(this.endDate);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(398, 40);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // startDate
            // 
            this.startDate.BackColor = System.Drawing.Color.White;
            this.startDate.Checked = true;
            this.startDate.FillColor = System.Drawing.Color.White;
            this.startDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.startDate.Location = new System.Drawing.Point(4, 4);
            this.startDate.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.startDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.startDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(180, 32);
            this.startDate.TabIndex = 0;
            this.startDate.Value = new System.DateTime(2025, 7, 26, 16, 12, 35, 624);
            this.startDate.ValueChanged += new System.EventHandler(this.startDate_ValueChanged);
            // 
            // endDate
            // 
            this.endDate.BackColor = System.Drawing.Color.White;
            this.endDate.Checked = true;
            this.endDate.FillColor = System.Drawing.Color.White;
            this.endDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.endDate.Location = new System.Drawing.Point(200, 4);
            this.endDate.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.endDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.endDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(180, 32);
            this.endDate.TabIndex = 1;
            this.endDate.Value = new System.DateTime(2025, 7, 26, 16, 12, 37, 911);
            this.endDate.ValueChanged += new System.EventHandler(this.endDate_ValueChanged);
            // 
            // right_panel
            // 
            this.right_panel.Controls.Add(this.right_panel_panel);
            this.right_panel.Dock = System.Windows.Forms.DockStyle.Right;
            this.right_panel.FillColor = System.Drawing.Color.White;
            this.right_panel.Location = new System.Drawing.Point(414, 81);
            this.right_panel.Margin = new System.Windows.Forms.Padding(0);
            this.right_panel.Name = "right_panel";
            this.right_panel.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.right_panel.Size = new System.Drawing.Size(300, 383);
            this.right_panel.TabIndex = 2;
            // 
            // right_panel_panel
            // 
            this.right_panel_panel.Controls.Add(this.chart2);
            this.right_panel_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.right_panel_panel.FillColor = System.Drawing.Color.White;
            this.right_panel_panel.Location = new System.Drawing.Point(8, 8);
            this.right_panel_panel.Margin = new System.Windows.Forms.Padding(0);
            this.right_panel_panel.Name = "right_panel_panel";
            this.right_panel_panel.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.right_panel_panel.Size = new System.Drawing.Size(284, 367);
            this.right_panel_panel.TabIndex = 0;
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.Transparent;
            this.chart2.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart2.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.chart2.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            this.chart2.BorderSkin.BorderWidth = 0;
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            this.chart2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart2.Enabled = false;
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(8, 8);
            this.chart2.Margin = new System.Windows.Forms.Padding(0);
            this.chart2.Name = "chart2";
            this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(268, 351);
            this.chart2.TabIndex = 0;
            this.chart2.Text = "chart2";
            // 
            // flow_Head
            // 
            this.flow_Head.AutoSize = true;
            this.flow_Head.BackColor = System.Drawing.Color.Transparent;
            this.flow_Head.Controls.Add(this.head_panel_2);
            this.flow_Head.Controls.Add(this.head_panel_3);
            this.flow_Head.Controls.Add(this.head_panel_4);
            this.flow_Head.Dock = System.Windows.Forms.DockStyle.Top;
            this.flow_Head.Location = new System.Drawing.Point(0, 0);
            this.flow_Head.Margin = new System.Windows.Forms.Padding(0);
            this.flow_Head.Name = "flow_Head";
            this.flow_Head.Size = new System.Drawing.Size(714, 81);
            this.flow_Head.TabIndex = 0;
            this.flow_Head.Resize += new System.EventHandler(this.flow_Head_Resize);
            // 
            // head_panel_2
            // 
            this.head_panel_2.Controls.Add(this.head_panel_panel_2);
            this.head_panel_2.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_2.FillColor2 = System.Drawing.Color.Transparent;
            this.head_panel_2.FillColor3 = System.Drawing.Color.Transparent;
            this.head_panel_2.FillColor4 = System.Drawing.Color.Transparent;
            this.head_panel_2.Location = new System.Drawing.Point(0, 0);
            this.head_panel_2.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_2.Name = "head_panel_2";
            this.head_panel_2.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.head_panel_2.Size = new System.Drawing.Size(225, 81);
            this.head_panel_2.TabIndex = 1;
            // 
            // head_panel_panel_2
            // 
            this.head_panel_panel_2.BorderRadius = 15;
            this.head_panel_panel_2.Controls.Add(this.lblImage_2);
            this.head_panel_panel_2.Controls.Add(this.head_panel_item2);
            this.head_panel_panel_2.Controls.Add(this.lblSoTaskDangXuLi);
            this.head_panel_panel_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_panel_2.FillColor = System.Drawing.Color.RoyalBlue;
            this.head_panel_panel_2.FillColor2 = System.Drawing.Color.RoyalBlue;
            this.head_panel_panel_2.FillColor3 = System.Drawing.Color.RoyalBlue;
            this.head_panel_panel_2.FillColor4 = System.Drawing.Color.RoyalBlue;
            this.head_panel_panel_2.Location = new System.Drawing.Point(8, 8);
            this.head_panel_panel_2.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_panel_2.Name = "head_panel_panel_2";
            this.head_panel_panel_2.Size = new System.Drawing.Size(209, 65);
            this.head_panel_panel_2.TabIndex = 0;
            // 
            // lblImage_2
            // 
            this.lblImage_2.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImage_2.Image = ((System.Drawing.Image)(resources.GetObject("lblImage_2.Image")));
            this.lblImage_2.Location = new System.Drawing.Point(147, 0);
            this.lblImage_2.Margin = new System.Windows.Forms.Padding(0);
            this.lblImage_2.Name = "lblImage_2";
            this.lblImage_2.Size = new System.Drawing.Size(62, 65);
            this.lblImage_2.TabIndex = 3;
            this.lblImage_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // head_panel_item2
            // 
            this.head_panel_item2.Controls.Add(this.head_panel_item1);
            this.head_panel_item2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_item2.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_item2.Location = new System.Drawing.Point(0, 0);
            this.head_panel_item2.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_item2.Name = "head_panel_item2";
            this.head_panel_item2.Size = new System.Drawing.Size(209, 65);
            this.head_panel_item2.TabIndex = 1;
            // 
            // head_panel_item1
            // 
            this.head_panel_item1.Controls.Add(this.panel_item2_2);
            this.head_panel_item1.Controls.Add(this.panel_item2_1);
            this.head_panel_item1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_item1.Location = new System.Drawing.Point(0, 0);
            this.head_panel_item1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.head_panel_item1.Name = "head_panel_item1";
            this.head_panel_item1.Size = new System.Drawing.Size(209, 65);
            this.head_panel_item1.TabIndex = 0;
            // 
            // panel_item2_2
            // 
            this.panel_item2_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_item2_2.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item2_2.ForeColor = System.Drawing.Color.White;
            this.panel_item2_2.Location = new System.Drawing.Point(0, 28);
            this.panel_item2_2.Margin = new System.Windows.Forms.Padding(0);
            this.panel_item2_2.Name = "panel_item2_2";
            this.panel_item2_2.Size = new System.Drawing.Size(209, 37);
            this.panel_item2_2.TabIndex = 1;
            this.panel_item2_2.Text = "7 Task";
            this.panel_item2_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_item2_1
            // 
            this.panel_item2_1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_item2_1.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item2_1.ForeColor = System.Drawing.Color.White;
            this.panel_item2_1.Location = new System.Drawing.Point(0, 0);
            this.panel_item2_1.Margin = new System.Windows.Forms.Padding(0);
            this.panel_item2_1.Name = "panel_item2_1";
            this.panel_item2_1.Size = new System.Drawing.Size(209, 28);
            this.panel_item2_1.TabIndex = 0;
            this.panel_item2_1.Text = "Trong Tuần";
            this.panel_item2_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoTaskDangXuLi
            // 
            this.lblSoTaskDangXuLi.Location = new System.Drawing.Point(0, 0);
            this.lblSoTaskDangXuLi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSoTaskDangXuLi.Name = "lblSoTaskDangXuLi";
            this.lblSoTaskDangXuLi.Size = new System.Drawing.Size(75, 19);
            this.lblSoTaskDangXuLi.TabIndex = 2;
            // 
            // head_panel_3
            // 
            this.head_panel_3.BackColor = System.Drawing.Color.Transparent;
            this.head_panel_3.Controls.Add(this.head_panel_panel_3);
            this.head_panel_3.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_3.FillColor2 = System.Drawing.Color.Transparent;
            this.head_panel_3.FillColor3 = System.Drawing.Color.Transparent;
            this.head_panel_3.FillColor4 = System.Drawing.Color.Transparent;
            this.head_panel_3.Location = new System.Drawing.Point(225, 0);
            this.head_panel_3.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_3.Name = "head_panel_3";
            this.head_panel_3.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.head_panel_3.Size = new System.Drawing.Size(225, 81);
            this.head_panel_3.TabIndex = 1;
            // 
            // head_panel_panel_3
            // 
            this.head_panel_panel_3.BorderRadius = 15;
            this.head_panel_panel_3.Controls.Add(this.head_panel_item3);
            this.head_panel_panel_3.Controls.Add(this.lblImage_3);
            this.head_panel_panel_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_panel_3.FillColor = System.Drawing.Color.LimeGreen;
            this.head_panel_panel_3.FillColor2 = System.Drawing.Color.LimeGreen;
            this.head_panel_panel_3.FillColor3 = System.Drawing.Color.LimeGreen;
            this.head_panel_panel_3.FillColor4 = System.Drawing.Color.LimeGreen;
            this.head_panel_panel_3.Location = new System.Drawing.Point(8, 8);
            this.head_panel_panel_3.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_panel_3.Name = "head_panel_panel_3";
            this.head_panel_panel_3.Size = new System.Drawing.Size(209, 65);
            this.head_panel_panel_3.TabIndex = 0;
            // 
            // head_panel_item3
            // 
            this.head_panel_item3.Controls.Add(this.panel_item3_2);
            this.head_panel_item3.Controls.Add(this.panel_item3_1);
            this.head_panel_item3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_item3.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_item3.Location = new System.Drawing.Point(0, 0);
            this.head_panel_item3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.head_panel_item3.Name = "head_panel_item3";
            this.head_panel_item3.Size = new System.Drawing.Size(147, 65);
            this.head_panel_item3.TabIndex = 1;
            // 
            // panel_item3_2
            // 
            this.panel_item3_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_item3_2.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item3_2.ForeColor = System.Drawing.Color.White;
            this.panel_item3_2.Location = new System.Drawing.Point(0, 28);
            this.panel_item3_2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.panel_item3_2.Name = "panel_item3_2";
            this.panel_item3_2.Size = new System.Drawing.Size(147, 37);
            this.panel_item3_2.TabIndex = 1;
            this.panel_item3_2.Text = "34 Task";
            this.panel_item3_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_item3_1
            // 
            this.panel_item3_1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_item3_1.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item3_1.ForeColor = System.Drawing.Color.White;
            this.panel_item3_1.Location = new System.Drawing.Point(0, 0);
            this.panel_item3_1.Margin = new System.Windows.Forms.Padding(0);
            this.panel_item3_1.Name = "panel_item3_1";
            this.panel_item3_1.Size = new System.Drawing.Size(147, 28);
            this.panel_item3_1.TabIndex = 0;
            this.panel_item3_1.Text = "Trong Tháng";
            this.panel_item3_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblImage_3
            // 
            this.lblImage_3.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImage_3.Image = ((System.Drawing.Image)(resources.GetObject("lblImage_3.Image")));
            this.lblImage_3.Location = new System.Drawing.Point(147, 0);
            this.lblImage_3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImage_3.Name = "lblImage_3";
            this.lblImage_3.Size = new System.Drawing.Size(62, 65);
            this.lblImage_3.TabIndex = 0;
            this.lblImage_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // head_panel_4
            // 
            this.head_panel_4.BackColor = System.Drawing.Color.Transparent;
            this.head_panel_4.Controls.Add(this.head_panel_panel_4);
            this.head_panel_4.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_4.FillColor2 = System.Drawing.Color.Transparent;
            this.head_panel_4.FillColor3 = System.Drawing.Color.Transparent;
            this.head_panel_4.FillColor4 = System.Drawing.Color.Transparent;
            this.head_panel_4.Location = new System.Drawing.Point(450, 0);
            this.head_panel_4.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_4.Name = "head_panel_4";
            this.head_panel_4.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.head_panel_4.Size = new System.Drawing.Size(225, 81);
            this.head_panel_4.TabIndex = 2;
            // 
            // head_panel_panel_4
            // 
            this.head_panel_panel_4.BorderRadius = 15;
            this.head_panel_panel_4.Controls.Add(this.head_panel_item4);
            this.head_panel_panel_4.Controls.Add(this.lblImage_4);
            this.head_panel_panel_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_panel_4.FillColor = System.Drawing.Color.Tomato;
            this.head_panel_panel_4.FillColor2 = System.Drawing.Color.Tomato;
            this.head_panel_panel_4.FillColor3 = System.Drawing.Color.Tomato;
            this.head_panel_panel_4.FillColor4 = System.Drawing.Color.Tomato;
            this.head_panel_panel_4.Location = new System.Drawing.Point(8, 8);
            this.head_panel_panel_4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.head_panel_panel_4.Name = "head_panel_panel_4";
            this.head_panel_panel_4.Size = new System.Drawing.Size(209, 65);
            this.head_panel_panel_4.TabIndex = 0;
            // 
            // head_panel_item4
            // 
            this.head_panel_item4.Controls.Add(this.panel_item4_2);
            this.head_panel_item4.Controls.Add(this.panel_item4_1);
            this.head_panel_item4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_item4.Location = new System.Drawing.Point(0, 0);
            this.head_panel_item4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.head_panel_item4.Name = "head_panel_item4";
            this.head_panel_item4.Size = new System.Drawing.Size(149, 65);
            this.head_panel_item4.TabIndex = 1;
            // 
            // panel_item4_2
            // 
            this.panel_item4_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_item4_2.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item4_2.ForeColor = System.Drawing.Color.White;
            this.panel_item4_2.Location = new System.Drawing.Point(0, 28);
            this.panel_item4_2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.panel_item4_2.Name = "panel_item4_2";
            this.panel_item4_2.Size = new System.Drawing.Size(149, 37);
            this.panel_item4_2.TabIndex = 1;
            this.panel_item4_2.Text = "218 Task";
            this.panel_item4_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_item4_1
            // 
            this.panel_item4_1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_item4_1.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item4_1.ForeColor = System.Drawing.Color.White;
            this.panel_item4_1.Location = new System.Drawing.Point(0, 0);
            this.panel_item4_1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.panel_item4_1.Name = "panel_item4_1";
            this.panel_item4_1.Size = new System.Drawing.Size(149, 28);
            this.panel_item4_1.TabIndex = 0;
            this.panel_item4_1.Text = "Trong Năm";
            this.panel_item4_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblImage_4
            // 
            this.lblImage_4.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImage_4.Image = ((System.Drawing.Image)(resources.GetObject("lblImage_4.Image")));
            this.lblImage_4.Location = new System.Drawing.Point(149, 0);
            this.lblImage_4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImage_4.Name = "lblImage_4";
            this.lblImage_4.Size = new System.Drawing.Size(60, 65);
            this.lblImage_4.TabIndex = 0;
            this.lblImage_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart1.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            this.chart1.BorderSkin.BorderWidth = 0;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Enabled = false;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(10, 10);
            this.chart1.Margin = new System.Windows.Forms.Padding(0);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(514, 433);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // dashboard_panel_dagiao
            // 
            this.dashboard_panel_dagiao.Controls.Add(this.main_panel_dagiao);
            this.dashboard_panel_dagiao.Controls.Add(this.right_panel_dagiao);
            this.dashboard_panel_dagiao.Controls.Add(this.flow_Head_dagiao);
            this.dashboard_panel_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboard_panel_dagiao.FillColor = System.Drawing.Color.White;
            this.dashboard_panel_dagiao.Location = new System.Drawing.Point(0, 0);
            this.dashboard_panel_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.dashboard_panel_dagiao.Name = "dashboard_panel_dagiao";
            this.dashboard_panel_dagiao.Size = new System.Drawing.Size(714, 464);
            this.dashboard_panel_dagiao.TabIndex = 0;
            // 
            // main_panel_dagiao
            // 
            this.main_panel_dagiao.Controls.Add(this.main_panel_panel_dagiao);
            this.main_panel_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel_dagiao.FillColor = System.Drawing.Color.White;
            this.main_panel_dagiao.Location = new System.Drawing.Point(0, 81);
            this.main_panel_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.main_panel_dagiao.Name = "main_panel_dagiao";
            this.main_panel_dagiao.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.main_panel_dagiao.Size = new System.Drawing.Size(414, 383);
            this.main_panel_dagiao.TabIndex = 3;
            // 
            // main_panel_panel_dagiao
            // 
            this.main_panel_panel_dagiao.BackColor = System.Drawing.Color.Transparent;
            this.main_panel_panel_dagiao.Controls.Add(this.panel_timeLine_DaGiao);
            this.main_panel_panel_dagiao.Controls.Add(this.flowLayoutPanel1_dagiao);
            this.main_panel_panel_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel_panel_dagiao.FillColor = System.Drawing.Color.White;
            this.main_panel_panel_dagiao.Location = new System.Drawing.Point(8, 8);
            this.main_panel_panel_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.main_panel_panel_dagiao.Name = "main_panel_panel_dagiao";
            this.main_panel_panel_dagiao.Size = new System.Drawing.Size(398, 367);
            this.main_panel_panel_dagiao.TabIndex = 0;
            // 
            // panel_timeLine_DaGiao
            // 
            this.panel_timeLine_DaGiao.AutoScroll = true;
            this.panel_timeLine_DaGiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_timeLine_DaGiao.FillColor = System.Drawing.Color.White;
            this.panel_timeLine_DaGiao.Location = new System.Drawing.Point(0, 40);
            this.panel_timeLine_DaGiao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_timeLine_DaGiao.Name = "panel_timeLine_DaGiao";
            this.panel_timeLine_DaGiao.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel_timeLine_DaGiao.Size = new System.Drawing.Size(398, 327);
            this.panel_timeLine_DaGiao.TabIndex = 2;
            // 
            // flowLayoutPanel1_dagiao
            // 
            this.flowLayoutPanel1_dagiao.AutoSize = true;
            this.flowLayoutPanel1_dagiao.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1_dagiao.Controls.Add(this.startDate_dagiao);
            this.flowLayoutPanel1_dagiao.Controls.Add(this.endDate_dagiao);
            this.flowLayoutPanel1_dagiao.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1_dagiao.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1_dagiao.Name = "flowLayoutPanel1_dagiao";
            this.flowLayoutPanel1_dagiao.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1_dagiao.Size = new System.Drawing.Size(398, 40);
            this.flowLayoutPanel1_dagiao.TabIndex = 1;
            // 
            // startDate_dagiao
            // 
            this.startDate_dagiao.BackColor = System.Drawing.Color.White;
            this.startDate_dagiao.Checked = true;
            this.startDate_dagiao.FillColor = System.Drawing.Color.White;
            this.startDate_dagiao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.startDate_dagiao.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.startDate_dagiao.Location = new System.Drawing.Point(4, 4);
            this.startDate_dagiao.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.startDate_dagiao.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.startDate_dagiao.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.startDate_dagiao.Name = "startDate_dagiao";
            this.startDate_dagiao.Size = new System.Drawing.Size(180, 32);
            this.startDate_dagiao.TabIndex = 0;
            this.startDate_dagiao.Value = new System.DateTime(2025, 7, 26, 16, 12, 35, 624);
            this.startDate_dagiao.ValueChanged += new System.EventHandler(this.startDate_dagiao_ValueChanged);
            // 
            // endDate_dagiao
            // 
            this.endDate_dagiao.BackColor = System.Drawing.Color.White;
            this.endDate_dagiao.Checked = true;
            this.endDate_dagiao.FillColor = System.Drawing.Color.White;
            this.endDate_dagiao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.endDate_dagiao.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.endDate_dagiao.Location = new System.Drawing.Point(200, 4);
            this.endDate_dagiao.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.endDate_dagiao.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.endDate_dagiao.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.endDate_dagiao.Name = "endDate_dagiao";
            this.endDate_dagiao.Size = new System.Drawing.Size(180, 32);
            this.endDate_dagiao.TabIndex = 1;
            this.endDate_dagiao.Value = new System.DateTime(2025, 7, 26, 16, 12, 37, 911);
            this.endDate_dagiao.ValueChanged += new System.EventHandler(this.endDate_dagiao_ValueChanged);
            // 
            // right_panel_dagiao
            // 
            this.right_panel_dagiao.Controls.Add(this.right_panel_panel_dagiao);
            this.right_panel_dagiao.Dock = System.Windows.Forms.DockStyle.Right;
            this.right_panel_dagiao.FillColor = System.Drawing.Color.White;
            this.right_panel_dagiao.Location = new System.Drawing.Point(414, 81);
            this.right_panel_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.right_panel_dagiao.Name = "right_panel_dagiao";
            this.right_panel_dagiao.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.right_panel_dagiao.Size = new System.Drawing.Size(300, 383);
            this.right_panel_dagiao.TabIndex = 2;
            // 
            // right_panel_panel_dagiao
            // 
            this.right_panel_panel_dagiao.Controls.Add(this.chart2_dagiao);
            this.right_panel_panel_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.right_panel_panel_dagiao.FillColor = System.Drawing.Color.White;
            this.right_panel_panel_dagiao.Location = new System.Drawing.Point(8, 8);
            this.right_panel_panel_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.right_panel_panel_dagiao.Name = "right_panel_panel_dagiao";
            this.right_panel_panel_dagiao.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.right_panel_panel_dagiao.Size = new System.Drawing.Size(284, 367);
            this.right_panel_panel_dagiao.TabIndex = 0;
            // 
            // chart2_dagiao
            // 
            this.chart2_dagiao.BackColor = System.Drawing.Color.Transparent;
            this.chart2_dagiao.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart2_dagiao.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.chart2_dagiao.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            this.chart2_dagiao.BorderSkin.BorderWidth = 0;
            chartArea3.Name = "ChartArea1_dagiao";
            this.chart2_dagiao.ChartAreas.Add(chartArea3);
            this.chart2_dagiao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chart2_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart2_dagiao.Enabled = false;
            legend3.Name = "Legend1_dagiao";
            this.chart2_dagiao.Legends.Add(legend3);
            this.chart2_dagiao.Location = new System.Drawing.Point(8, 8);
            this.chart2_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.chart2_dagiao.Name = "chart2_dagiao";
            this.chart2_dagiao.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series3.ChartArea = "ChartArea1_dagiao";
            series3.Legend = "Legend1_dagiao";
            series3.Name = "Series1_dagiao";
            this.chart2_dagiao.Series.Add(series3);
            this.chart2_dagiao.Size = new System.Drawing.Size(268, 351);
            this.chart2_dagiao.TabIndex = 0;
            this.chart2_dagiao.Text = "chart2_dagiao ";
            // 
            // flow_Head_dagiao
            // 
            this.flow_Head_dagiao.AutoSize = true;
            this.flow_Head_dagiao.BackColor = System.Drawing.Color.Transparent;
            this.flow_Head_dagiao.Controls.Add(this.head_panel_2_dagiao);
            this.flow_Head_dagiao.Controls.Add(this.head_panel_3_dagiao);
            this.flow_Head_dagiao.Controls.Add(this.head_panel_4_dagiao);
            this.flow_Head_dagiao.Dock = System.Windows.Forms.DockStyle.Top;
            this.flow_Head_dagiao.Location = new System.Drawing.Point(0, 0);
            this.flow_Head_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.flow_Head_dagiao.Name = "flow_Head_dagiao";
            this.flow_Head_dagiao.Size = new System.Drawing.Size(714, 81);
            this.flow_Head_dagiao.TabIndex = 0;
            this.flow_Head_dagiao.Resize += new System.EventHandler(this.flow_Head_dagiao_Resize);
            // 
            // head_panel_2_dagiao
            // 
            this.head_panel_2_dagiao.Controls.Add(this.head_panel_panel_2_dagiao);
            this.head_panel_2_dagiao.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_2_dagiao.FillColor2 = System.Drawing.Color.Transparent;
            this.head_panel_2_dagiao.FillColor3 = System.Drawing.Color.Transparent;
            this.head_panel_2_dagiao.FillColor4 = System.Drawing.Color.Transparent;
            this.head_panel_2_dagiao.Location = new System.Drawing.Point(0, 0);
            this.head_panel_2_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_2_dagiao.Name = "head_panel_2_dagiao";
            this.head_panel_2_dagiao.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.head_panel_2_dagiao.Size = new System.Drawing.Size(225, 81);
            this.head_panel_2_dagiao.TabIndex = 1;
            // 
            // head_panel_panel_2_dagiao
            // 
            this.head_panel_panel_2_dagiao.BorderRadius = 15;
            this.head_panel_panel_2_dagiao.Controls.Add(this.lblImage_2_dagiao);
            this.head_panel_panel_2_dagiao.Controls.Add(this.head_panel_item2_dagiao);
            this.head_panel_panel_2_dagiao.Controls.Add(this.lblSoTaskDangXuLi_dagiao);
            this.head_panel_panel_2_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_panel_2_dagiao.FillColor = System.Drawing.Color.RoyalBlue;
            this.head_panel_panel_2_dagiao.FillColor2 = System.Drawing.Color.RoyalBlue;
            this.head_panel_panel_2_dagiao.FillColor3 = System.Drawing.Color.RoyalBlue;
            this.head_panel_panel_2_dagiao.FillColor4 = System.Drawing.Color.RoyalBlue;
            this.head_panel_panel_2_dagiao.Location = new System.Drawing.Point(8, 8);
            this.head_panel_panel_2_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_panel_2_dagiao.Name = "head_panel_panel_2_dagiao";
            this.head_panel_panel_2_dagiao.Size = new System.Drawing.Size(209, 65);
            this.head_panel_panel_2_dagiao.TabIndex = 0;
            // 
            // lblImage_2_dagiao
            // 
            this.lblImage_2_dagiao.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImage_2_dagiao.Image = ((System.Drawing.Image)(resources.GetObject("lblImage_2_dagiao.Image")));
            this.lblImage_2_dagiao.Location = new System.Drawing.Point(147, 0);
            this.lblImage_2_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.lblImage_2_dagiao.Name = "lblImage_2_dagiao";
            this.lblImage_2_dagiao.Size = new System.Drawing.Size(62, 65);
            this.lblImage_2_dagiao.TabIndex = 0;
            this.lblImage_2_dagiao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // head_panel_item2_dagiao
            // 
            this.head_panel_item2_dagiao.Controls.Add(this.head_panel_item1_dagiao);
            this.head_panel_item2_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_item2_dagiao.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_item2_dagiao.Location = new System.Drawing.Point(0, 0);
            this.head_panel_item2_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_item2_dagiao.Name = "head_panel_item2_dagiao";
            this.head_panel_item2_dagiao.Size = new System.Drawing.Size(209, 65);
            this.head_panel_item2_dagiao.TabIndex = 1;
            // 
            // head_panel_item1_dagiao
            // 
            this.head_panel_item1_dagiao.Controls.Add(this.panel_item2_2_dagiao);
            this.head_panel_item1_dagiao.Controls.Add(this.panel_item2_1_dagiao);
            this.head_panel_item1_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_item1_dagiao.Location = new System.Drawing.Point(0, 0);
            this.head_panel_item1_dagiao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.head_panel_item1_dagiao.Name = "head_panel_item1_dagiao";
            this.head_panel_item1_dagiao.Size = new System.Drawing.Size(209, 65);
            this.head_panel_item1_dagiao.TabIndex = 0;
            // 
            // panel_item2_2_dagiao
            // 
            this.panel_item2_2_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_item2_2_dagiao.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item2_2_dagiao.ForeColor = System.Drawing.Color.White;
            this.panel_item2_2_dagiao.Location = new System.Drawing.Point(0, 28);
            this.panel_item2_2_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.panel_item2_2_dagiao.Name = "panel_item2_2_dagiao";
            this.panel_item2_2_dagiao.Size = new System.Drawing.Size(209, 37);
            this.panel_item2_2_dagiao.TabIndex = 1;
            this.panel_item2_2_dagiao.Text = "7 Task";
            this.panel_item2_2_dagiao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_item2_1_dagiao
            // 
            this.panel_item2_1_dagiao.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_item2_1_dagiao.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item2_1_dagiao.ForeColor = System.Drawing.Color.White;
            this.panel_item2_1_dagiao.Location = new System.Drawing.Point(0, 0);
            this.panel_item2_1_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.panel_item2_1_dagiao.Name = "panel_item2_1_dagiao";
            this.panel_item2_1_dagiao.Size = new System.Drawing.Size(209, 28);
            this.panel_item2_1_dagiao.TabIndex = 0;
            this.panel_item2_1_dagiao.Text = "Trong Tuần";
            this.panel_item2_1_dagiao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoTaskDangXuLi_dagiao
            // 
            this.lblSoTaskDangXuLi_dagiao.Location = new System.Drawing.Point(0, 0);
            this.lblSoTaskDangXuLi_dagiao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSoTaskDangXuLi_dagiao.Name = "lblSoTaskDangXuLi_dagiao";
            this.lblSoTaskDangXuLi_dagiao.Size = new System.Drawing.Size(75, 19);
            this.lblSoTaskDangXuLi_dagiao.TabIndex = 2;
            // 
            // head_panel_3_dagiao
            // 
            this.head_panel_3_dagiao.BackColor = System.Drawing.Color.Transparent;
            this.head_panel_3_dagiao.Controls.Add(this.head_panel_panel_3_dagiao);
            this.head_panel_3_dagiao.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_3_dagiao.FillColor2 = System.Drawing.Color.Transparent;
            this.head_panel_3_dagiao.FillColor3 = System.Drawing.Color.Transparent;
            this.head_panel_3_dagiao.FillColor4 = System.Drawing.Color.Transparent;
            this.head_panel_3_dagiao.Location = new System.Drawing.Point(225, 0);
            this.head_panel_3_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_3_dagiao.Name = "head_panel_3_dagiao";
            this.head_panel_3_dagiao.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.head_panel_3_dagiao.Size = new System.Drawing.Size(225, 81);
            this.head_panel_3_dagiao.TabIndex = 1;
            // 
            // head_panel_panel_3_dagiao
            // 
            this.head_panel_panel_3_dagiao.BorderRadius = 15;
            this.head_panel_panel_3_dagiao.Controls.Add(this.head_panel_item3_dagiao);
            this.head_panel_panel_3_dagiao.Controls.Add(this.lblImage_3_dagiao);
            this.head_panel_panel_3_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_panel_3_dagiao.FillColor = System.Drawing.Color.LimeGreen;
            this.head_panel_panel_3_dagiao.FillColor2 = System.Drawing.Color.LimeGreen;
            this.head_panel_panel_3_dagiao.FillColor3 = System.Drawing.Color.LimeGreen;
            this.head_panel_panel_3_dagiao.FillColor4 = System.Drawing.Color.LimeGreen;
            this.head_panel_panel_3_dagiao.Location = new System.Drawing.Point(8, 8);
            this.head_panel_panel_3_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_panel_3_dagiao.Name = "head_panel_panel_3_dagiao";
            this.head_panel_panel_3_dagiao.Size = new System.Drawing.Size(209, 65);
            this.head_panel_panel_3_dagiao.TabIndex = 0;
            // 
            // head_panel_item3_dagiao
            // 
            this.head_panel_item3_dagiao.Controls.Add(this.panel_item3_2_dagiao);
            this.head_panel_item3_dagiao.Controls.Add(this.panel_item3_1_dagiao);
            this.head_panel_item3_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_item3_dagiao.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_item3_dagiao.Location = new System.Drawing.Point(0, 0);
            this.head_panel_item3_dagiao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.head_panel_item3_dagiao.Name = "head_panel_item3_dagiao";
            this.head_panel_item3_dagiao.Size = new System.Drawing.Size(147, 65);
            this.head_panel_item3_dagiao.TabIndex = 1;
            // 
            // panel_item3_2_dagiao
            // 
            this.panel_item3_2_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_item3_2_dagiao.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item3_2_dagiao.ForeColor = System.Drawing.Color.White;
            this.panel_item3_2_dagiao.Location = new System.Drawing.Point(0, 28);
            this.panel_item3_2_dagiao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.panel_item3_2_dagiao.Name = "panel_item3_2_dagiao";
            this.panel_item3_2_dagiao.Size = new System.Drawing.Size(147, 37);
            this.panel_item3_2_dagiao.TabIndex = 1;
            this.panel_item3_2_dagiao.Text = "34 Task";
            this.panel_item3_2_dagiao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_item3_1_dagiao
            // 
            this.panel_item3_1_dagiao.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_item3_1_dagiao.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item3_1_dagiao.ForeColor = System.Drawing.Color.White;
            this.panel_item3_1_dagiao.Location = new System.Drawing.Point(0, 0);
            this.panel_item3_1_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.panel_item3_1_dagiao.Name = "panel_item3_1_dagiao";
            this.panel_item3_1_dagiao.Size = new System.Drawing.Size(147, 28);
            this.panel_item3_1_dagiao.TabIndex = 0;
            this.panel_item3_1_dagiao.Text = "Trong Tháng";
            this.panel_item3_1_dagiao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblImage_3_dagiao
            // 
            this.lblImage_3_dagiao.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImage_3_dagiao.Image = ((System.Drawing.Image)(resources.GetObject("lblImage_3_dagiao.Image")));
            this.lblImage_3_dagiao.Location = new System.Drawing.Point(147, 0);
            this.lblImage_3_dagiao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImage_3_dagiao.Name = "lblImage_3_dagiao";
            this.lblImage_3_dagiao.Size = new System.Drawing.Size(62, 65);
            this.lblImage_3_dagiao.TabIndex = 0;
            this.lblImage_3_dagiao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // head_panel_4_dagiao
            // 
            this.head_panel_4_dagiao.BackColor = System.Drawing.Color.Transparent;
            this.head_panel_4_dagiao.Controls.Add(this.head_panel_panel_4_dagiao);
            this.head_panel_4_dagiao.FillColor = System.Drawing.Color.Transparent;
            this.head_panel_4_dagiao.FillColor2 = System.Drawing.Color.Transparent;
            this.head_panel_4_dagiao.FillColor3 = System.Drawing.Color.Transparent;
            this.head_panel_4_dagiao.FillColor4 = System.Drawing.Color.Transparent;
            this.head_panel_4_dagiao.Location = new System.Drawing.Point(450, 0);
            this.head_panel_4_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.head_panel_4_dagiao.Name = "head_panel_4_dagiao";
            this.head_panel_4_dagiao.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.head_panel_4_dagiao.Size = new System.Drawing.Size(225, 81);
            this.head_panel_4_dagiao.TabIndex = 2;
            // 
            // head_panel_panel_4_dagiao
            // 
            this.head_panel_panel_4_dagiao.BorderRadius = 15;
            this.head_panel_panel_4_dagiao.Controls.Add(this.head_panel_item4_dagiao);
            this.head_panel_panel_4_dagiao.Controls.Add(this.lblImage_4_dagiao);
            this.head_panel_panel_4_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_panel_4_dagiao.FillColor = System.Drawing.Color.Tomato;
            this.head_panel_panel_4_dagiao.FillColor2 = System.Drawing.Color.Tomato;
            this.head_panel_panel_4_dagiao.FillColor3 = System.Drawing.Color.Tomato;
            this.head_panel_panel_4_dagiao.FillColor4 = System.Drawing.Color.Tomato;
            this.head_panel_panel_4_dagiao.Location = new System.Drawing.Point(8, 8);
            this.head_panel_panel_4_dagiao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.head_panel_panel_4_dagiao.Name = "head_panel_panel_4_dagiao";
            this.head_panel_panel_4_dagiao.Size = new System.Drawing.Size(209, 65);
            this.head_panel_panel_4_dagiao.TabIndex = 0;
            // 
            // head_panel_item4_dagiao
            // 
            this.head_panel_item4_dagiao.Controls.Add(this.panel_item4_2_dagiao);
            this.head_panel_item4_dagiao.Controls.Add(this.panel_item4_1_dagiao);
            this.head_panel_item4_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.head_panel_item4_dagiao.Location = new System.Drawing.Point(0, 0);
            this.head_panel_item4_dagiao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.head_panel_item4_dagiao.Name = "head_panel_item4_dagiao";
            this.head_panel_item4_dagiao.Size = new System.Drawing.Size(149, 65);
            this.head_panel_item4_dagiao.TabIndex = 1;
            // 
            // panel_item4_2_dagiao
            // 
            this.panel_item4_2_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_item4_2_dagiao.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item4_2_dagiao.ForeColor = System.Drawing.Color.White;
            this.panel_item4_2_dagiao.Location = new System.Drawing.Point(0, 28);
            this.panel_item4_2_dagiao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.panel_item4_2_dagiao.Name = "panel_item4_2_dagiao";
            this.panel_item4_2_dagiao.Size = new System.Drawing.Size(149, 37);
            this.panel_item4_2_dagiao.TabIndex = 1;
            this.panel_item4_2_dagiao.Text = "218 Task";
            this.panel_item4_2_dagiao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_item4_1_dagiao
            // 
            this.panel_item4_1_dagiao.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_item4_1_dagiao.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_item4_1_dagiao.ForeColor = System.Drawing.Color.White;
            this.panel_item4_1_dagiao.Location = new System.Drawing.Point(0, 0);
            this.panel_item4_1_dagiao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.panel_item4_1_dagiao.Name = "panel_item4_1_dagiao";
            this.panel_item4_1_dagiao.Size = new System.Drawing.Size(149, 28);
            this.panel_item4_1_dagiao.TabIndex = 0;
            this.panel_item4_1_dagiao.Text = "Trong Năm";
            this.panel_item4_1_dagiao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblImage_4_dagiao
            // 
            this.lblImage_4_dagiao.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImage_4_dagiao.Image = ((System.Drawing.Image)(resources.GetObject("lblImage_4_dagiao.Image")));
            this.lblImage_4_dagiao.Location = new System.Drawing.Point(149, 0);
            this.lblImage_4_dagiao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImage_4_dagiao.Name = "lblImage_4_dagiao";
            this.lblImage_4_dagiao.Size = new System.Drawing.Size(60, 65);
            this.lblImage_4_dagiao.TabIndex = 0;
            this.lblImage_4_dagiao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart1_dagiao
            // 
            this.chart1_dagiao.BackColor = System.Drawing.Color.Transparent;
            this.chart1_dagiao.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart1_dagiao.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.chart1_dagiao.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            this.chart1_dagiao.BorderSkin.BorderWidth = 0;
            chartArea4.Name = "ChartArea1_dagiao";
            this.chart1_dagiao.ChartAreas.Add(chartArea4);
            this.chart1_dagiao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chart1_dagiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1_dagiao.Enabled = false;
            legend4.Name = "Legend1_dagiao";
            this.chart1_dagiao.Legends.Add(legend4);
            this.chart1_dagiao.Location = new System.Drawing.Point(10, 10);
            this.chart1_dagiao.Margin = new System.Windows.Forms.Padding(0);
            this.chart1_dagiao.Name = "chart1_dagiao";
            series4.ChartArea = "ChartArea1_dagiao";
            series4.Legend = "Legend1_dagiao";
            series4.Name = "Series1_dagiao";
            this.chart1_dagiao.Series.Add(series4);
            this.chart1_dagiao.Size = new System.Drawing.Size(514, 433);
            this.chart1_dagiao.TabIndex = 1;
            this.chart1_dagiao.Text = "chart1_dagiao";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabControl1.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(8, 8);
            this.xtraTabControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            this.xtraTabControl1.Size = new System.Drawing.Size(714, 486);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.Click += new System.EventHandler(this.xtraTabControl1_Click);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.dashboard_panel_dagiao);
            this.xtraTabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(714, 464);
            this.xtraTabPage2.Text = "Đã Giao";
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.dashboard_panel);
            this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(714, 464);
            this.xtraTabPage1.Text = "Được Giao";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.xtraTabControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Dashboard";
            this.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.Size = new System.Drawing.Size(730, 502);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.dashboard_panel.ResumeLayout(false);
            this.dashboard_panel.PerformLayout();
            this.main_panel.ResumeLayout(false);
            this.main_panel_panel.ResumeLayout(false);
            this.main_panel_panel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.right_panel.ResumeLayout(false);
            this.right_panel_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.flow_Head.ResumeLayout(false);
            this.head_panel_2.ResumeLayout(false);
            this.head_panel_panel_2.ResumeLayout(false);
            this.head_panel_item2.ResumeLayout(false);
            this.head_panel_item1.ResumeLayout(false);
            this.head_panel_3.ResumeLayout(false);
            this.head_panel_panel_3.ResumeLayout(false);
            this.head_panel_item3.ResumeLayout(false);
            this.head_panel_4.ResumeLayout(false);
            this.head_panel_panel_4.ResumeLayout(false);
            this.head_panel_item4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.dashboard_panel_dagiao.ResumeLayout(false);
            this.dashboard_panel_dagiao.PerformLayout();
            this.main_panel_dagiao.ResumeLayout(false);
            this.main_panel_panel_dagiao.ResumeLayout(false);
            this.main_panel_panel_dagiao.PerformLayout();
            this.flowLayoutPanel1_dagiao.ResumeLayout(false);
            this.right_panel_dagiao.ResumeLayout(false);
            this.right_panel_panel_dagiao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2_dagiao)).EndInit();
            this.flow_Head_dagiao.ResumeLayout(false);
            this.head_panel_2_dagiao.ResumeLayout(false);
            this.head_panel_panel_2_dagiao.ResumeLayout(false);
            this.head_panel_item2_dagiao.ResumeLayout(false);
            this.head_panel_item1_dagiao.ResumeLayout(false);
            this.head_panel_3_dagiao.ResumeLayout(false);
            this.head_panel_panel_3_dagiao.ResumeLayout(false);
            this.head_panel_item3_dagiao.ResumeLayout(false);
            this.head_panel_4_dagiao.ResumeLayout(false);
            this.head_panel_panel_4_dagiao.ResumeLayout(false);
            this.head_panel_item4_dagiao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1_dagiao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel dashboard_panel;
        private Guna.UI2.WinForms.Guna2Panel dashboard_panel_dagiao;
        private System.Windows.Forms.FlowLayoutPanel flow_Head;
        private System.Windows.Forms.FlowLayoutPanel flow_Head_dagiao;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_2;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_2_dagiao;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_3;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_3_dagiao;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_panel_2;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_panel_2_dagiao;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_panel_3;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_panel_3_dagiao;
        private System.Windows.Forms.Label lblSoTaskDangXuLi;
        private System.Windows.Forms.Label lblSoTaskDangXuLi_dagiao;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_4;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_4_dagiao;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_panel_4;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel head_panel_panel_4_dagiao;
        private Guna.UI2.WinForms.Guna2Panel main_panel;
        private Guna.UI2.WinForms.Guna2Panel main_panel_dagiao;
        private Guna.UI2.WinForms.Guna2Panel right_panel;
        private Guna.UI2.WinForms.Guna2Panel right_panel_dagiao;
        private Guna.UI2.WinForms.Guna2Panel main_panel_panel;
        private Guna.UI2.WinForms.Guna2Panel main_panel_panel_dagiao;
        private Guna.UI2.WinForms.Guna2Panel right_panel_panel;
        private Guna.UI2.WinForms.Guna2Panel right_panel_panel_dagiao;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1_dagiao;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2_dagiao;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1_dagiao;
        private Guna.UI2.WinForms.Guna2DateTimePicker startDate;
        private Guna.UI2.WinForms.Guna2DateTimePicker startDate_dagiao;
        private Guna.UI2.WinForms.Guna2DateTimePicker endDate;
        private Guna.UI2.WinForms.Guna2DateTimePicker endDate_dagiao;
        private System.Windows.Forms.Label lblImage_3;
        private System.Windows.Forms.Label lblImage_3_dagiao;
        private System.Windows.Forms.Label lblImage_4;
        private System.Windows.Forms.Label lblImage_4_dagiao;
        private Guna.UI2.WinForms.Guna2Panel head_panel_item3;
        private Guna.UI2.WinForms.Guna2Panel head_panel_item3_dagiao;
        private System.Windows.Forms.Label panel_item3_2;
        private System.Windows.Forms.Label panel_item3_2_dagiao;
        private System.Windows.Forms.Label panel_item3_1;
        private System.Windows.Forms.Label panel_item3_1_dagiao;
        private Guna.UI2.WinForms.Guna2Panel head_panel_item4;
        private Guna.UI2.WinForms.Guna2Panel head_panel_item4_dagiao;
        private System.Windows.Forms.Label panel_item4_1;
        private System.Windows.Forms.Label panel_item4_1_dagiao;
        private System.Windows.Forms.Label lblImage_2;
        private System.Windows.Forms.Label lblImage_2_dagiao;
        private System.Windows.Forms.Label panel_item4_2;
        private System.Windows.Forms.Label panel_item4_2_dagiao;
        private Guna.UI2.WinForms.Guna2Panel head_panel_item2;
        private Guna.UI2.WinForms.Guna2Panel head_panel_item2_dagiao;
        private Guna.UI2.WinForms.Guna2Panel head_panel_item1;
        private Guna.UI2.WinForms.Guna2Panel head_panel_item1_dagiao;
        private System.Windows.Forms.Label panel_item2_2;
        private System.Windows.Forms.Label panel_item2_2_dagiao;
        private System.Windows.Forms.Label panel_item2_1;
        private System.Windows.Forms.Label panel_item2_1_dagiao;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Guna.UI2.WinForms.Guna2Panel panel_timeLine_DaGiao;
        private Guna.UI2.WinForms.Guna2Panel panel_timeLine;
    }
}
