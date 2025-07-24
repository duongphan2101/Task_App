using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using Task_App.Model;
using Task_App.Services;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class Home : Form
    {
        private static readonly System.Timers.Timer ntfyTimer = new System.Timers.Timer(5_00000)
        {
            Enabled = true,
            AutoReset = true
        };


        private readonly NguoiDung currentUser;
        private readonly int maNguoiDung;

        private readonly Color defaultColor = Color.FromArgb(0, 0, 0, 0);
        private readonly Color hoverColor = Color.FromArgb(64, 64, 64);
        private readonly Color clickColor = Color.FromArgb(80, 80, 80);
        private readonly Color selectedColor = Color.FromArgb(80, 80, 80);
        
        // phần mềm này không chạy 24/7 để tự gửi mail vào lúc 18h
        //private System.Timers.Timer dailyTimer;
        //private bool hasRunToday = false;

        private TcpClientDAO tcpClientDAO;

        public Home(int userId, TcpClientDAO tcpClientDAO)
        {
            InitializeComponent();
            
            maNguoiDung = userId;
            //currentUser = NguonDungService.getNguoiDungById(maNguoiDung);
            currentUser = tcpClientDAO.GetNguoiDung(maNguoiDung);
            SetUserInfo(currentUser);
            this.tcpClientDAO = tcpClientDAO;

            btn_Task_DaGiao.Tag = true;
            btn_Task_DaGiao.BackColor = selectedColor;

            LoadNotifications();
            LoadContent(new Create_Task_Control(maNguoiDung, tcpClientDAO));

            ntfyTimer.Elapsed += OnNtfyTimerElapsed;
        }

        private void SetUserInfo(NguoiDung user)
        {
            lblName.Text = user.HoTen;
            lblPhongBan.Text = user.PhongBan?.TenPhongBan ?? "Chưa có";
            lblChucVu.Text = user.ChucVu?.TenChucVu ?? "Chưa có";
            lblDonVi.Text = user.DonVi?.TenDonVi ?? "Chưa có";
        }

        private void LoadContent(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            panel_Center_Main.Controls.Clear();
            panel_Center_Main.Controls.Add(control);
        }

        // clicking a button will mark it as selected via Button.Tag (nullable) property
        // when the cursor hover, leave or release a button, we only change its background if said button isn't selected/tagged
        private void OnBtnMouseEnter(object sender, EventArgs e)
        {
            var targetBtn = (Guna2ImageButton)sender;
            if (targetBtn.Tag is null) targetBtn.BackColor = hoverColor;
        }
        private void OnBtnMouseLeaveOrUp(object sender, object _)
        {
            var targetBtn = (Guna2ImageButton)sender;
            if (targetBtn.Tag == null) targetBtn.BackColor = defaultColor;
        }
        private void OnBtnMouseDown(object sender, MouseEventArgs e)
        {
            var targetBtn = (Guna2ImageButton)sender;
            targetBtn.BackColor = clickColor;
        }
        private void OnBtnMouseClick(object sender, EventArgs e)
        {
            var clickedBtn = (Guna2ImageButton)sender;

            foreach (var btn in new[] { btn_Task_DaGiao, btn_Task_DuocGiao, btnDashboard })
            {
                if (btn.Name == clickedBtn.Name)
                {
                    btn.Tag = true;
                    btn.BackColor = selectedColor;
                } 
                else
                {
                    btn.Tag = null;
                    btn.BackColor = defaultColor;
                }
            }
            
            // load content based on button name
            // TODO should cache this instead of initializing new instances everytime?
            //   maybe set to a Map instead
            SuspendLayout();
            if (clickedBtn.Name == btnDashboard.Name) LoadContent(new Dashboard());
            if (clickedBtn.Name == btn_Task_DaGiao.Name) LoadContent(new Create_Task_Control(maNguoiDung, tcpClientDAO));
            if (clickedBtn.Name == btn_Task_DuocGiao.Name) LoadContent(new Task_Duoc_Giao_Control(maNguoiDung, tcpClientDAO));
            ResumeLayout();
        }

        private void OnExitBtnClick(object sender, EventArgs e)
        {
            CloseLingeringForms();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            CloseLingeringForms();
        }

        private void CloseLingeringForms()
        {
            for (var i = 0; i < Application.OpenForms.Count; i++)
                if (Application.OpenForms[i].GetType() != typeof(FormLogin))
                    Application.OpenForms[i].Close();
        }

        private void OnNtfyIconMouseMove(object sender, MouseEventArgs e)
        {
            lblNotifications.BackColor = Color.Transparent;

            if (!menu_ThongBao.Visible)
            {
                int x = lblNotifications.Width - menu_ThongBao.Width;
                int y = lblNotifications.Height;

                menu_ThongBao.Show(lblNotifications, new Point(x, y));
            }
        }

        private void LoadNotifications()
        {
            //List<ThongBaoNguoiDung> lstThongBao = CongViecDAO.GetTop8ThongBaoById(maNguoiDung);
            List<ThongBaoNguoiDung> lstThongBao = tcpClientDAO.GetTop8ThongBaoById(maNguoiDung);
            menu_ThongBao.Items.Clear();

            int count = 0;

            foreach (ThongBaoNguoiDung tb in lstThongBao)
            {
                string noiDungGoc = tb.NoiDung;
                string noiDungRutGon = noiDungGoc.Length > 35 ? noiDungGoc.Substring(0, 32) + "..." : noiDungGoc;

                ToolStripMenuItem item = new ToolStripMenuItem(noiDungRutGon);
                item.Tag = tb;
                item.ToolTipText = noiDungGoc;
                menu_ThongBao.Items.Add(item);

                if (tb.TrangThai == 0)
                {
                    count++;
                    item.BackColor = Color.LightGray;
                }

                item.Click += (s, e) => OnNtfySelected(s, e, tb.MaThongBao);
            }

            // Thêm item "Xem tất cả"
            ToolStripMenuItem xemTatCaItem = new ToolStripMenuItem("Xem tất cả");
            xemTatCaItem.Tag = "XemTatCa";
            xemTatCaItem.Font = new Font(xemTatCaItem.Font, FontStyle.Bold);
            xemTatCaItem.ForeColor = Color.DarkBlue;
            xemTatCaItem.Click += OnNtfySeenAllBtnClicked;
            menu_ThongBao.Items.Add(new ToolStripSeparator());
            menu_ThongBao.Items.Add(xemTatCaItem);

            number.Visible = count != 0;
            number.Text = count.ToString();
        }

        // TODO what does this do?
        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            //btnNotifications.BackColor = SystemColors.Control;
        }

        // TODO untested
        private void OnNtfySeenAllBtnClicked(object sender, EventArgs e)
        {
            foreach (var ntfyItem in menu_ThongBao.Items.OfType<ToolStripMenuItem>())
            {
                if (!(ntfyItem.Tag is ThongBaoNguoiDung tb))
                    continue;

                //CongViecDAO.UpdateTrangThaiThongBao(tb.MaThongBao); 
                tcpClientDAO.UpdateTrangThaiThongBao(tb.MaThongBao);
            }

            LoadNotifications();
        }

        private void OnNtfySelected(object sender, EventArgs e, int tb)
        {
            if (tcpClientDAO.UpdateTrangThaiThongBao(tb))
            {
                LoadNotifications();
            }
        }

        private void OnNtfyTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // UI updates require to run on main thread
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new Action(() => LoadNotifications()));
            }
        }

    }
}
