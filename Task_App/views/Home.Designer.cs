using System.Windows.Forms;
using Task_App.Model;

namespace Task_App.views
{
    partial class Home
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panel = new System.Windows.Forms.Panel();
            this.panelControlCenter = new DevExpress.XtraEditors.PanelControl();
            this.panel_Center = new System.Windows.Forms.Panel();
            this.panelControl_Main = new DevExpress.XtraEditors.PanelControl();
            this.panel_Center_Main = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_centerTop = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPhongBan = new System.Windows.Forms.Label();
            this.lblChucVu = new System.Windows.Forms.Label();
            this.lblDonVi = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.panelTop_Right = new DevExpress.XtraEditors.PanelControl();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.number = new System.Windows.Forms.Label();
            this.lblNotifications = new System.Windows.Forms.Label();
            this.avt = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.panelControlMenu = new DevExpress.XtraEditors.PanelControl();
            this.panel_Menu = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btn_Task_DaGiao = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btn_Task_DuocGiao = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnExit = new Guna.UI2.WinForms.Guna2ImageButton();
            this.menu_ThongBao = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cccswxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eehToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HomelayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.panelControl1item = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlCenter)).BeginInit();
            this.panelControlCenter.SuspendLayout();
            this.panel_Center.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Main)).BeginInit();
            this.panelControl_Main.SuspendLayout();
            this.panel_centerTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop_Right)).BeginInit();
            this.panelTop_Right.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMenu)).BeginInit();
            this.panelControlMenu.SuspendLayout();
            this.panel_Menu.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HomelayoutControl1ConvertedLayout)).BeginInit();
            this.HomelayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1item)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.panel);
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1417, 880);
            this.panelControl1.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Controls.Add(this.panelControlCenter);
            this.panel.Controls.Add(this.panelControlMenu);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1417, 880);
            this.panel.TabIndex = 4;
            // 
            // panelControlCenter
            // 
            this.panelControlCenter.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControlCenter.Appearance.Options.UseBackColor = true;
            this.panelControlCenter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlCenter.Controls.Add(this.panel_Center);
            this.panelControlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlCenter.Location = new System.Drawing.Point(200, 0);
            this.panelControlCenter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControlCenter.Name = "panelControlCenter";
            this.panelControlCenter.Size = new System.Drawing.Size(1217, 880);
            this.panelControlCenter.TabIndex = 4;
            // 
            // panel_Center
            // 
            this.panel_Center.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel_Center.Controls.Add(this.panelControl_Main);
            this.panel_Center.Controls.Add(this.panel_centerTop);
            this.panel_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Center.Location = new System.Drawing.Point(0, 0);
            this.panel_Center.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Center.Name = "panel_Center";
            this.panel_Center.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.panel_Center.Size = new System.Drawing.Size(1217, 880);
            this.panel_Center.TabIndex = 0;
            // 
            // panelControl_Main
            // 
            this.panelControl_Main.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl_Main.Appearance.Options.UseBackColor = true;
            this.panelControl_Main.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl_Main.Controls.Add(this.panel_Center_Main);
            this.panelControl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl_Main.Location = new System.Drawing.Point(11, 123);
            this.panelControl_Main.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl_Main.Name = "panelControl_Main";
            this.panelControl_Main.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panelControl_Main.Size = new System.Drawing.Size(1195, 747);
            this.panelControl_Main.TabIndex = 1;
            // 
            // panel_Center_Main
            // 
            this.panel_Center_Main.BackColor = System.Drawing.Color.Transparent;
            this.panel_Center_Main.BorderRadius = 16;
            this.panel_Center_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Center_Main.FillColor = System.Drawing.Color.White;
            this.panel_Center_Main.Location = new System.Drawing.Point(0, 20);
            this.panel_Center_Main.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Center_Main.Name = "panel_Center_Main";
            this.panel_Center_Main.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.panel_Center_Main.Size = new System.Drawing.Size(1195, 727);
            this.panel_Center_Main.TabIndex = 0;
            // 
            // panel_centerTop
            // 
            this.panel_centerTop.BorderRadius = 16;
            this.panel_centerTop.Controls.Add(this.tableLayoutPanel1);
            this.panel_centerTop.Controls.Add(this.panelTop_Right);
            this.panel_centerTop.Controls.Add(this.avt);
            this.panel_centerTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_centerTop.FillColor = System.Drawing.Color.White;
            this.panel_centerTop.Location = new System.Drawing.Point(11, 10);
            this.panel_centerTop.Margin = new System.Windows.Forms.Padding(0);
            this.panel_centerTop.Name = "panel_centerTop";
            this.panel_centerTop.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.panel_centerTop.Size = new System.Drawing.Size(1195, 113);
            this.panel_centerTop.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblPhongBan, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblChucVu, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDonVi, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(115, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(969, 93);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // lblPhongBan
            // 
            this.lblPhongBan.AutoSize = true;
            this.lblPhongBan.BackColor = System.Drawing.Color.Transparent;
            this.lblPhongBan.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPhongBan.Font = new System.Drawing.Font("Bahnschrift Condensed", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhongBan.Location = new System.Drawing.Point(67, 46);
            this.lblPhongBan.Margin = new System.Windows.Forms.Padding(0);
            this.lblPhongBan.Name = "lblPhongBan";
            this.lblPhongBan.Size = new System.Drawing.Size(88, 47);
            this.lblPhongBan.TabIndex = 4;
            this.lblPhongBan.Text = "phongBan";
            this.lblPhongBan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChucVu
            // 
            this.lblChucVu.AutoSize = true;
            this.lblChucVu.BackColor = System.Drawing.Color.Transparent;
            this.lblChucVu.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblChucVu.Font = new System.Drawing.Font("Bahnschrift Condensed", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChucVu.Location = new System.Drawing.Point(0, 46);
            this.lblChucVu.Margin = new System.Windows.Forms.Padding(0);
            this.lblChucVu.Name = "lblChucVu";
            this.lblChucVu.Size = new System.Drawing.Size(65, 47);
            this.lblChucVu.TabIndex = 2;
            this.lblChucVu.Text = "chucVu";
            this.lblChucVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDonVi
            // 
            this.lblDonVi.AutoSize = true;
            this.lblDonVi.BackColor = System.Drawing.Color.Transparent;
            this.lblDonVi.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDonVi.Font = new System.Drawing.Font("Bahnschrift Condensed", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonVi.Location = new System.Drawing.Point(67, 0);
            this.lblDonVi.Margin = new System.Windows.Forms.Padding(0);
            this.lblDonVi.Name = "lblDonVi";
            this.lblDonVi.Size = new System.Drawing.Size(54, 46);
            this.lblDonVi.TabIndex = 3;
            this.lblDonVi.Text = "donvi";
            this.lblDonVi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblName.Font = new System.Drawing.Font("Bahnschrift Condensed", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(67, 46);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "hoTen";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelTop_Right
            // 
            this.panelTop_Right.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelTop_Right.Appearance.Options.UseBackColor = true;
            this.panelTop_Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTop_Right.Controls.Add(this.guna2Panel1);
            this.panelTop_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTop_Right.Location = new System.Drawing.Point(1084, 10);
            this.panelTop_Right.Margin = new System.Windows.Forms.Padding(0);
            this.panelTop_Right.Name = "panelTop_Right";
            this.panelTop_Right.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.panelTop_Right.Size = new System.Drawing.Size(100, 93);
            this.panelTop_Right.TabIndex = 7;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.number);
            this.guna2Panel1.Controls.Add(this.lblNotifications);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.Location = new System.Drawing.Point(11, 10);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(78, 73);
            this.guna2Panel1.TabIndex = 0;
            // 
            // number
            // 
            this.number.AutoSize = true;
            this.number.BackColor = System.Drawing.Color.Red;
            this.number.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.number.ForeColor = System.Drawing.Color.White;
            this.number.Location = new System.Drawing.Point(37, 18);
            this.number.Margin = new System.Windows.Forms.Padding(0);
            this.number.Name = "number";
            this.number.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.number.Size = new System.Drawing.Size(34, 25);
            this.number.TabIndex = 1;
            this.number.Text = "44";
            // 
            // lblNotifications
            // 
            this.lblNotifications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNotifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotifications.Image = ((System.Drawing.Image)(resources.GetObject("lblNotifications.Image")));
            this.lblNotifications.Location = new System.Drawing.Point(0, 0);
            this.lblNotifications.Margin = new System.Windows.Forms.Padding(0);
            this.lblNotifications.Name = "lblNotifications";
            this.lblNotifications.Size = new System.Drawing.Size(78, 73);
            this.lblNotifications.TabIndex = 0;
            this.lblNotifications.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnNtfyIconMouseMove);
            // 
            // avt
            // 
            this.avt.BackColor = System.Drawing.Color.Transparent;
            this.avt.Cursor = System.Windows.Forms.Cursors.No;
            this.avt.Dock = System.Windows.Forms.DockStyle.Left;
            this.avt.FillColor = System.Drawing.Color.Transparent;
            this.avt.Image = ((System.Drawing.Image)(resources.GetObject("avt.Image")));
            this.avt.ImageRotate = 0F;
            this.avt.Location = new System.Drawing.Point(11, 10);
            this.avt.Margin = new System.Windows.Forms.Padding(0);
            this.avt.Name = "avt";
            this.avt.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.avt.Size = new System.Drawing.Size(104, 93);
            this.avt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.avt.TabIndex = 0;
            this.avt.TabStop = false;
            // 
            // panelControlMenu
            // 
            this.panelControlMenu.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            this.panelControlMenu.Appearance.Options.UseBackColor = true;
            this.panelControlMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlMenu.Controls.Add(this.panel_Menu);
            this.panelControlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControlMenu.Location = new System.Drawing.Point(0, 0);
            this.panelControlMenu.Margin = new System.Windows.Forms.Padding(0);
            this.panelControlMenu.Name = "panelControlMenu";
            this.panelControlMenu.Size = new System.Drawing.Size(200, 880);
            this.panelControlMenu.TabIndex = 0;
            // 
            // panel_Menu
            // 
            this.panel_Menu.BorderColor = System.Drawing.Color.Transparent;
            this.panel_Menu.Controls.Add(this.tableLayoutPanel2);
            this.panel_Menu.Controls.Add(this.btnExit);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Menu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.panel_Menu.Location = new System.Drawing.Point(0, 0);
            this.panel_Menu.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(200, 880);
            this.panel_Menu.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnDashboard, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Task_DaGiao, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_Task_DuocGiao, 0, 2);
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 278);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 266);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.Transparent;
            this.btnDashboard.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDashboard.HoverState.ImageSize = new System.Drawing.Size(48, 48);
            this.btnDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnDashboard.Image")));
            this.btnDashboard.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnDashboard.ImageRotate = 0F;
            this.btnDashboard.ImageSize = new System.Drawing.Size(32, 32);
            this.btnDashboard.Location = new System.Drawing.Point(0, 0);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnDashboard.Size = new System.Drawing.Size(200, 88);
            this.btnDashboard.TabIndex = 2;
            this.btnDashboard.Click += new System.EventHandler(this.OnBtnMouseClick);
            this.btnDashboard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnBtnMouseDown);
            this.btnDashboard.MouseEnter += new System.EventHandler(this.OnBtnMouseEnter);
            this.btnDashboard.MouseLeave += new System.EventHandler(this.OnBtnMouseLeaveOrUp);
            this.btnDashboard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnBtnMouseLeaveOrUp);
            // 
            // btn_Task_DaGiao
            // 
            this.btn_Task_DaGiao.BackColor = System.Drawing.Color.Transparent;
            this.btn_Task_DaGiao.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btn_Task_DaGiao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Task_DaGiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Task_DaGiao.HoverState.ImageSize = new System.Drawing.Size(48, 48);
            this.btn_Task_DaGiao.Image = ((System.Drawing.Image)(resources.GetObject("btn_Task_DaGiao.Image")));
            this.btn_Task_DaGiao.ImageOffset = new System.Drawing.Point(0, 0);
            this.btn_Task_DaGiao.ImageRotate = 0F;
            this.btn_Task_DaGiao.ImageSize = new System.Drawing.Size(32, 32);
            this.btn_Task_DaGiao.Location = new System.Drawing.Point(0, 88);
            this.btn_Task_DaGiao.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Task_DaGiao.Name = "btn_Task_DaGiao";
            this.btn_Task_DaGiao.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btn_Task_DaGiao.Size = new System.Drawing.Size(200, 88);
            this.btn_Task_DaGiao.TabIndex = 0;
            this.btn_Task_DaGiao.Click += new System.EventHandler(this.OnBtnMouseClick);
            this.btn_Task_DaGiao.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnBtnMouseDown);
            this.btn_Task_DaGiao.MouseEnter += new System.EventHandler(this.OnBtnMouseEnter);
            this.btn_Task_DaGiao.MouseLeave += new System.EventHandler(this.OnBtnMouseLeaveOrUp);
            this.btn_Task_DaGiao.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnBtnMouseLeaveOrUp);
            // 
            // btn_Task_DuocGiao
            // 
            this.btn_Task_DuocGiao.BackColor = System.Drawing.Color.Transparent;
            this.btn_Task_DuocGiao.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btn_Task_DuocGiao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Task_DuocGiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Task_DuocGiao.HoverState.ImageSize = new System.Drawing.Size(48, 48);
            this.btn_Task_DuocGiao.Image = ((System.Drawing.Image)(resources.GetObject("btn_Task_DuocGiao.Image")));
            this.btn_Task_DuocGiao.ImageOffset = new System.Drawing.Point(0, 0);
            this.btn_Task_DuocGiao.ImageRotate = 0F;
            this.btn_Task_DuocGiao.ImageSize = new System.Drawing.Size(32, 32);
            this.btn_Task_DuocGiao.Location = new System.Drawing.Point(0, 176);
            this.btn_Task_DuocGiao.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Task_DuocGiao.Name = "btn_Task_DuocGiao";
            this.btn_Task_DuocGiao.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btn_Task_DuocGiao.Size = new System.Drawing.Size(200, 90);
            this.btn_Task_DuocGiao.TabIndex = 1;
            this.btn_Task_DuocGiao.Click += new System.EventHandler(this.OnBtnMouseClick);
            this.btn_Task_DuocGiao.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnBtnMouseDown);
            this.btn_Task_DuocGiao.MouseEnter += new System.EventHandler(this.OnBtnMouseEnter);
            this.btn_Task_DuocGiao.MouseLeave += new System.EventHandler(this.OnBtnMouseLeaveOrUp);
            this.btn_Task_DuocGiao.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnBtnMouseLeaveOrUp);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExit.HoverState.ImageSize = new System.Drawing.Size(32, 32);
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnExit.ImageRotate = 0F;
            this.btnExit.ImageSize = new System.Drawing.Size(24, 24);
            this.btnExit.Location = new System.Drawing.Point(0, 791);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnExit.Size = new System.Drawing.Size(200, 89);
            this.btnExit.TabIndex = 3;
            this.btnExit.Click += new System.EventHandler(this.OnExitBtnClick);
            // 
            // menu_ThongBao
            // 
            this.menu_ThongBao.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menu_ThongBao.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu_ThongBao.Name = "contextMenuStrip1";
            this.menu_ThongBao.Size = new System.Drawing.Size(61, 4);
            this.menu_ThongBao.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip1_Closed);
            // 
            // cccswxToolStripMenuItem
            // 
            this.cccswxToolStripMenuItem.Name = "cccswxToolStripMenuItem";
            this.cccswxToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // eehToolStripMenuItem
            // 
            this.eehToolStripMenuItem.Name = "eehToolStripMenuItem";
            this.eehToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // HomelayoutControl1ConvertedLayout
            // 
            this.HomelayoutControl1ConvertedLayout.Controls.Add(this.panelControl1);
            this.HomelayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HomelayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.HomelayoutControl1ConvertedLayout.Margin = new System.Windows.Forms.Padding(0);
            this.HomelayoutControl1ConvertedLayout.Name = "HomelayoutControl1ConvertedLayout";
            this.HomelayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(722, 335, 650, 400);
            this.HomelayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.HomelayoutControl1ConvertedLayout.Size = new System.Drawing.Size(1421, 884);
            this.HomelayoutControl1ConvertedLayout.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.BackColor = System.Drawing.Color.WhiteSmoke;
            this.layoutControlGroup1.AppearanceGroup.Options.UseBackColor = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.panelControl1item});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1421, 884);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // panelControl1item
            // 
            this.panelControl1item.Control = this.panelControl1;
            this.panelControl1item.Location = new System.Drawing.Point(0, 0);
            this.panelControl1item.Name = "panelControl1item";
            this.panelControl1item.Size = new System.Drawing.Size(1421, 884);
            this.panelControl1item.TextVisible = false;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 884);
            this.Controls.Add(this.HomelayoutControl1ConvertedLayout);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1439, 931);
            this.Name = "Home";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlCenter)).EndInit();
            this.panelControlCenter.ResumeLayout(false);
            this.panel_Center.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Main)).EndInit();
            this.panelControl_Main.ResumeLayout(false);
            this.panel_centerTop.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop_Right)).EndInit();
            this.panelTop_Right.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMenu)).EndInit();
            this.panelControlMenu.ResumeLayout(false);
            this.panel_Menu.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HomelayoutControl1ConvertedLayout)).EndInit();
            this.HomelayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1item)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Panel panel;
        private DevExpress.XtraEditors.PanelControl panelControlMenu;
        private DevExpress.XtraEditors.PanelControl panelControlCenter;
        private System.Windows.Forms.Panel panel_Center;
        private Guna.UI2.WinForms.Guna2Panel panel_centerTop;
        private Guna.UI2.WinForms.Guna2Panel panel_Menu;
        private DevExpress.XtraEditors.PanelControl panelControl_Main;
        private Guna.UI2.WinForms.Guna2Panel panel_Center_Main;
        private Guna.UI2.WinForms.Guna2ImageButton btn_Task_DaGiao;
        private Guna.UI2.WinForms.Guna2ImageButton btn_Task_DuocGiao;
        private Guna.UI2.WinForms.Guna2CirclePictureBox avt;
        private Guna.UI2.WinForms.Guna2ImageButton btnDashboard;
        private System.Windows.Forms.Label lblChucVu;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhongBan;
        private System.Windows.Forms.Label lblDonVi;
        private DevExpress.XtraEditors.PanelControl panelTop_Right;
        private Guna.UI2.WinForms.Guna2ImageButton btnExit;
        private ContextMenuStrip menu_ThongBao;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label lblNotifications;
        private Label number;
        private ToolStripMenuItem eehToolStripMenuItem;
        private ToolStripMenuItem cccswxToolStripMenuItem;
        private DevExpress.XtraLayout.LayoutControl HomelayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem panelControl1item;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}