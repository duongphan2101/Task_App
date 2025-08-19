using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Task_App.Model;
using Task_App.Response;
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

        private ApiClientDAO apiClientDAO;

        private readonly Color defaultColor = Color.FromArgb(0, 0, 0, 0);
        private readonly Color hoverColor = Color.FromArgb(64, 64, 64);
        private readonly Color clickColor = Color.FromArgb(80, 80, 80);
        private readonly Color selectedColor = Color.FromArgb(80, 80, 80);
        private ToolTip toolTip = new ToolTip();

        private NguoiDung nd;

        private readonly Dictionary<string, string> buttonTooltips = new Dictionary<string, string>
        {
            { "btnDashboard", "Trang tổng quan" },
            { "btn_Task_DaGiao", "Công việc đã giao" },
            { "btn_Task_DuocGiao", "Công việc được giao" },
        };

        public Home(NguoiDung nd, ApiClientDAO apiClientDAO)
        {
            InitializeComponent();

            this.nd = nd;
            maNguoiDung = nd.MaNguoiDung;
            SetUserInfo(nd);
            this.apiClientDAO = apiClientDAO;

            btn_Task_DaGiao.Tag = true;
            btn_Task_DaGiao.BackColor = selectedColor;

            LoadNotifications();

            LoadContent(new Create_Task_Control(nd, apiClientDAO));
        }

        private void SetUserInfo(NguoiDung nd)
        {
            lblName.Text = nd.HoTen;
            lblPhongBan.Text = nd.PhongBan.TenPhongBan ?? "Chưa có";
            lblChucVu.Text = nd.ChucVu.TenChucVu ?? "Chưa có";
            lblDonVi.Text = nd.DonVi.TenDonVi ?? "Chưa có";
        }

        private void LoadContent(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            panel_Center_Main.Controls.Clear();
            panel_Center_Main.Controls.Add(control);
        }

        private void OnBtnMouseEnter(object sender, EventArgs e)
        {
            var targetBtn = (Guna2ImageButton)sender;
            if (targetBtn.Tag is null) {
                targetBtn.BackColor = hoverColor;
            }
            if (buttonTooltips.TryGetValue(targetBtn.Name, out string tooltipText))
            {
                toolTip.SetToolTip(targetBtn, tooltipText);
            }
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

            SuspendLayout();

            if (clickedBtn.Name == btnDashboard.Name)
                //LoadContent(new DashboardControlViewer(tcpClientDAO, maNguoiDung));
                LoadContent(new Dashboard(apiClientDAO, maNguoiDung));

            if (clickedBtn.Name == btn_Task_DaGiao.Name)
                LoadContent(new Create_Task_Control(nd, apiClientDAO));

            if (clickedBtn.Name == btn_Task_DuocGiao.Name)
                LoadContent(new Task_Duoc_Giao_Control(nd, apiClientDAO));

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

        private async void LoadNotifications()
        {
            var resTop8ThongBao = await apiClientDAO.GetTop8ThongBaoById(maNguoiDung);
            List<ThongBaoNguoiDung> lstThongBao = resTop8ThongBao.Data;
            if(lstThongBao == null || lstThongBao.Count == 0)
            {
                Console.WriteLine("Loi: " + resTop8ThongBao.Message);
            }
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

                item.Click += (s, e) => OnNtfySelected(s, e, tb.MaThongBao, tb.MaChiTietCV);
            }

            // Thêm item "Xem tất cả"
            ToolStripMenuItem xemTatCaItem = new ToolStripMenuItem("Xem tất cả");
            xemTatCaItem.Tag = "XemTatCa";
            xemTatCaItem.Font = new Font(xemTatCaItem.Font, FontStyle.Bold);
            xemTatCaItem.ForeColor = Color.DarkBlue;
            xemTatCaItem.Click += OnNtfySeenAllBtnClickedAsync;
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
        private async void OnNtfySeenAllBtnClickedAsync(object sender, EventArgs e)
        {
            foreach (var ntfyItem in menu_ThongBao.Items.OfType<ToolStripMenuItem>())
            {
                if (!(ntfyItem.Tag is ThongBaoNguoiDung tb))
                    continue;

                //CongViecDAO.UpdateTrangThaiThongBao(tb.MaThongBao); 
                var resUpdateTrangThaiThongBao = await apiClientDAO.UpdateTrangThaiThongBao111(tb.MaThongBao);
                if (!resUpdateTrangThaiThongBao.Success)
                {
                    Console.WriteLine("Loi khi update " + resUpdateTrangThaiThongBao.Message);
                }

            }

            LoadNotifications();
        }

        private async Task OnNtfySelected(object sender, EventArgs e, int tb, int maChiTietCV)
        {
            var resUpdateTrangThaiThongBao = await apiClientDAO.UpdateTrangThaiThongBao111(tb);
            if (resUpdateTrangThaiThongBao.Success)
            {
                LoadNotifications();
            }
            else
            {
                Console.WriteLine("Loi khi update " + resUpdateTrangThaiThongBao.Message);
            }
            var resIsGiaoViec = await apiClientDAO.getIsGiaoViec(maNguoiDung, maChiTietCV);
            var isGiaoViec = resIsGiaoViec.Success;
            var resChiTietCV = await apiClientDAO.GetChiTietConViecAsync(maChiTietCV);
            string maCongViec = resChiTietCV.Data.MaCongViec;
            Task_Duoc_Giao_Control tdg = new Task_Duoc_Giao_Control(nd, apiClientDAO);
            Modal_ChiTiet_CongViec modal = new Modal_ChiTiet_CongViec(nd, maCongViec, maChiTietCV, maNguoiDung, isGiaoViec, apiClientDAO, tdg);
            modal.ShowDialog();

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
