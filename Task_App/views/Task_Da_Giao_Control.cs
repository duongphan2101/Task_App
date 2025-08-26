using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_App.Model;
using Task_App.Response;
using Task_App.Services;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class Create_Task_Control : UserControl
    {
        private NguoiDung nd;

        private readonly int maNguoiDung;
        private bool locTheoNgay = true;
        private ApiClientDAO apiClientDAO;

        public Create_Task_Control(NguoiDung nd, ApiClientDAO apiClientDAO)
        {
            this.nd = nd;
            maNguoiDung =  nd.MaNguoiDung;

            //CongViecService = new CongViecService(tcpClientDAO);
            this.apiClientDAO = apiClientDAO;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
        }

        public async Task LoadData()
        {
            var response = await apiClientDAO.GetViecDaGiaoAsync(nd.MaNguoiDung, !locTheoNgay);

            //if (response == null || !response.Success || response.Data == null || response.Data.Count == 0)
            //{
            //    MessageBox.Show("Không có dữ liệu.");
            //    return;
            //}

            // Flatten dữ liệu: mỗi chi tiết công việc thành 1 dòng, lấy thông tin người nhận ở cấp công việc
            var displayData = response.Data
                .SelectMany(cv => cv.ChiTiet.Select(ct => new
                {
                    MaCongViec = cv.MaCongViec,
                    NgayGiao = cv.NgayGiao,
                    //LapLai = cv.LapLai,
                    //TanSuat = cv.TanSuat,

                    MaChiTietCV = ct.MaChiTietCV,
                    //TieuDe = ct.TieuDe,
                    //NoiDung = ct.NoiDung,
                    //NgayNhanCongViec = ct.NgayNhanCongViec,
                    //NgayKetThucCongViec = ct.NgayKetThucCongViec,
                    //NgayHoanThanh = ct.NgayHoanThanh,
                    SoNgayHoanThanh = ct.SoNgayHoanThanh,
                    //TrangThai = ct.TrangThai,
                    TrangThaiText = GetTrangThaiText(ct.TrangThai),
                    TienDo = ct.TienDo,
                    //MucDoUuTien = ct.MucDoUuTien,
                    MucDoUuTienText = GetMucDoUuTienText(ct.MucDoUuTien),

                    NguoiNhan_HoTen = cv.NguoiNhan?.HoTen ?? "",
                    //NguoiNhan_Email = cv.NguoiNhan?.Email ?? "",
                    //TenDonVi = cv.NguoiNhan?.TenDonVi ?? "",
                    //TenPhongBan = cv.NguoiNhan?.TenPhongBan ?? "",
                    //TenChucVu = cv.NguoiNhan?.TenChucVu ?? ""
                }))
                .ToList();

            data_test.DataSource = displayData;

            // Đổi header cho DataGridView
            data_test.Columns["MaCongViec"].HeaderText = "Mã CV";
            data_test.Columns["NgayGiao"].HeaderText = "Ngày giao";
            //data_test.Columns["LapLai"].HeaderText = "Lặp lại";
            //data_test.Columns["TanSuat"].HeaderText = "Tần suất";

            data_test.Columns["MaChiTietCV"].HeaderText = "Mã CTCV";
            //data_test.Columns["TieuDe"].HeaderText = "Tiêu đề";
            //data_test.Columns["NoiDung"].HeaderText = "Nội dung";
            //data_test.Columns["NgayNhanCongViec"].HeaderText = "Ngày nhận";
            //data_test.Columns["NgayKetThucCongViec"].HeaderText = "Ngày kết thúc";
            //data_test.Columns["NgayHoanThanh"].HeaderText = "Ngày hoàn thành";
            data_test.Columns["SoNgayHoanThanh"].HeaderText = "Số ngày hoàn thành";
            data_test.Columns["TrangThaiText"].HeaderText = "Trạng thái";
            data_test.Columns["TienDo"].HeaderText = "Tiến độ (%)";
            data_test.Columns["MucDoUuTienText"].HeaderText = "Mức độ ưu tiên";

            data_test.Columns["NguoiNhan_HoTen"].HeaderText = "Người nhận";
            //data_test.Columns["NguoiNhan_Email"].HeaderText = "Email người nhận";
            //data_test.Columns["TenDonVi"].HeaderText = "Đơn vị";
            //data_test.Columns["TenPhongBan"].HeaderText = "Phòng ban";
            //data_test.Columns["TenChucVu"].HeaderText = "Chức vụ";

            // Thêm nút Xem nếu chưa có
            if (data_test.Columns["btnAction"] == null)
            {
                var btnColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Thao tác",
                    Text = "Xem",
                    Name = "btnAction",
                    UseColumnTextForButtonValue = true
                };
                data_test.Columns.Add(btnColumn);
            }

            data_test.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            data_test.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            data_test.AllowUserToAddRows = false;
            data_test.Refresh();
        }

        private string GetTrangThaiText(int trangThai)
        {
            switch (trangThai)
            {
                case 0: return "Chưa xử lý";
                case 1: return "Đang xử lý";
                case 2: return "Hoàn thành";
                case 3: return "Trễ";
                case 4: return "Đã hủy";
                default: return "Không xác định";
            }
        }

        private string GetMucDoUuTienText(int mucDo)
        {
            switch (mucDo)
            {
                case 0: return "Bình thường";
                case 1: return "Quan trọng";
                case 2: return "Khẩn cấp";
                default: return "Không xác định";
            }
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
            Task_Duoc_Giao_Control tdgiao = new Task_Duoc_Giao_Control(nd, apiClientDAO);
            var modal = new Modal_ChiTiet_CongViec(nd ,maCongViec, maCTCV, maNguoiDung, true, apiClientDAO, tdgiao);
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
            var modal = new Modal_Create_Task(nd, apiClientDAO, this);
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
