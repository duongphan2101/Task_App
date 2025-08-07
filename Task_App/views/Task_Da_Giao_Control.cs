using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Task_App.Services;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class Create_Task_Control : UserControl
    {
        private readonly TcpClientDAO tcpClientDAO;
        private readonly CongViecService CongViecService;

        private readonly int maNguoiDung;
        private bool locTheoNgay = true;

        public Create_Task_Control(int idUser, TcpClientDAO tcpClientDAO)
        {
            maNguoiDung = idUser;
            CongViecService = new CongViecService(tcpClientDAO);
            this.tcpClientDAO = tcpClientDAO;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
        }

        public void LoadData()
        {
            DataTable dt = CongViecService.GetCongViecDaGiao(maNguoiDung, !locTheoNgay);

            dt.Columns.Add("trangThaiText", typeof(string));
            dt.Columns.Add("mucDoUuTienText", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                switch (row["trangThai"] as int?)
                {
                    case 0:
                        row["trangThaiText"] = "Chưa xử lý";
                        break;
                    case 1:
                        row["trangThaiText"] = "Đang xử lý";
                        break;
                    case 2:
                        row["trangThaiText"] = "Hoàn thành";
                        break;
                    case 3:
                        row["trangThaiText"] = "Trễ";
                        break;
                    case 4:
                        row["trangThaiText"] = "Đã hủy";
                        break;
                    default:
                        row["trangThaiText"] = "Không xác định";
                        break;
                }

                switch (row["mucDoUuTien"] as int?)
                {
                    case 0:
                        row["mucDoUuTienText"] = "Bình thường";
                        break;
                    case 1:
                        row["mucDoUuTienText"] = "Quan trọng";
                        break;
                    case 2:
                        row["mucDoUuTienText"] = "Khẩn cấp";
                        break;
                    default:
                        row["mucDoUuTienText"] = "Không xác định";
                        break;
                }
            }

            // Chọn các cột cần hiển thị
            DataTable dtDisplay = dt.DefaultView.ToTable(false, "maCongViec",
                "maChiTietCV", "tieuDe", "ngayNhanCongViec", "ngayKetThucCongViec", "trangThaiText", "mucDoUuTienText", "tienDo",
                "nguoiNhan_HoTen", "tenDonVi", "tenPhongBan", "tenChucVu");

            // Gán dữ liệu vào DataGridView
            data_test.DataSource = dtDisplay;

            // Đổi header
            data_test.Columns["maCongViec"].HeaderText = "Mã CV";
            data_test.Columns["maChiTietCV"].HeaderText = "Mã CTCV";
            data_test.Columns["tieuDe"].HeaderText = "Tiêu đề";
            data_test.Columns["ngayNhanCongViec"].HeaderText = "Ngày Nhận";
            data_test.Columns["ngayKetThucCongViec"].HeaderText = "Ngày KTCV";
            data_test.Columns["trangThaiText"].HeaderText = "Trạng thái";
            data_test.Columns["mucDoUuTienText"].HeaderText = "Mức độ ưu tiên";
            data_test.Columns["tienDo"].HeaderText = "Tiến độ (%)";
            data_test.Columns["nguoiNhan_HoTen"].HeaderText = "Người nhận";
            data_test.Columns["tenDonVi"].HeaderText = "Đơn vị";
            data_test.Columns["tenPhongBan"].HeaderText = "Phòng ban";
            data_test.Columns["tenChucVu"].HeaderText = "Chức vụ";

            // Thêm cột xem thông tin chi tiết
            if (data_test.Columns["btnAction"] == null) {
                var btnColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Thao tác",
                    Text = "Xem",
                    Name = "btnAction",
                    UseColumnTextForButtonValue = true
                };
                data_test.Columns.Add(btnColumn);
            }

            // Căn giữa
            data_test.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            data_test.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            data_test.AllowUserToAddRows = false;
            data_test.Refresh();
        }

        private void data_test_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var buttonCell = data_test[e.ColumnIndex, e.RowIndex];
            if (buttonCell.OwningColumn.Name != "btnAction") return;
            if (buttonCell.Tag != null) return;

            var row = buttonCell.OwningRow;
            var maCongViec = (row.Cells["maCongViec"].Value as string) ?? string.Empty;
            var maCTCV = (row.Cells["maChiTietCV"].Value as int?) ?? -1;
            
            buttonCell.Tag = maCongViec;
            Task_Duoc_Giao_Control tdgiao = new Task_Duoc_Giao_Control(maNguoiDung, tcpClientDAO);
            var modal = new Modal_ChiTiet_CongViec(maCongViec, maCTCV, maNguoiDung, true, tcpClientDAO, tdgiao);
            modal.FormClosed += (s, args) => { buttonCell.Tag = null; };
            modal.Show();
        }

        private void data_test_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (data_test.Rows[e.RowIndex].DataBoundItem == null)
                return;

            var row = data_test.Rows[e.RowIndex];
            string trangThai = (row.Cells["trangThaiText"].Value as string) ?? string.Empty;
            switch (trangThai)
            {
                case "Chưa xử lý":
                    row.DefaultCellStyle.BackColor = Color.White;
                    break;
                case "Đang xử lý":
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    break;
                case "Hoàn thành":
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    break;
                case "Trễ":
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                    break;
                case "Đã hủy":
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    break;
            }

            if (data_test.Columns[e.ColumnIndex].Name == "mucDoUuTienText")
            {
                string mucDoUuTien = (e.Value as string) ?? string.Empty;
                switch (mucDoUuTien)
                {
                    case "Bình thường":
                        e.CellStyle.ForeColor = Color.Green;
                        break;
                    case "Quan trọng":
                        e.CellStyle.ForeColor = Color.Orange;
                        break;
                    case "Khẩn cấp":
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.Black;
                        break;
                }
            }

        }

        private void btn_AddTask_Click(object sender, EventArgs e)
        {
            var modal = new Modal_Create_Task(maNguoiDung, tcpClientDAO, this);
            modal.ShowDialog();
            modal.FormClosed += (s, args) => LoadData();
        }

        private void radio_Xep_Theo_Ngay_CheckedChanged(object sender, EventArgs e)
        {
            locTheoNgay = radio_Xep_Theo_Ngay.Checked;
            LoadData();
        }

        private void radio_DaHoanThanh_CheckedChanged(object sender, EventArgs e)
        {
            locTheoNgay = !radio_DaHoanThanh.Checked;
            LoadData();
        }

        private void btn_AddTask_MouseHover(object sender, EventArgs e)
        {
           ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btn_AddTask, "Thêm công việc mới");
        }

    }
}
