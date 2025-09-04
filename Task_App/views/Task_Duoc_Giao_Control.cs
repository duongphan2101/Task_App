using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_App.DTO;
using Task_App.Model;
using Task_App.Response;
using Task_App.Services;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class Task_Duoc_Giao_Control : UserControl
    {
        private NguoiDung nd;
        private Task_Duoc_Giao_Control duocGiaoControl;
        private bool locTheoNgay = true;
        private CongViecService congViecService;
        private int maChiTietCV;
        private TcpClientDAO tcpClientDAO;
        private ApiClientDAO apiClientDAO;

        public Task_Duoc_Giao_Control(NguoiDung nd, ApiClientDAO apiClientDAO)
        {
            this.nd = nd;
            this.apiClientDAO = apiClientDAO;
            InitializeComponent();
        }

        public async void LoadData()
        {
            var response = await apiClientDAO.GetViecDuocGiaoAsync(nd.MaNguoiDung, locTheoNgay);

            if (response.Data == null || !response.Data.Any())
            {
                Console.WriteLine("Không có dữ liệu. Msg: " + response.Message);
                this.Controls.Clear();
                this.BackColor = Color.White;
                Label lbl = new Label();
                lbl.Text = "Không có dữ liệu";
                lbl.AutoSize = false;
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                this.Controls.Add(lbl);
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("Tahoma", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                return;
            }

            var displayData = response.Data
                .SelectMany(cv => cv.ChiTiet.Select(ct => new
                {
                    MaCongViec = cv.MaCongViec,
                    NgayGiao = cv.NgayGiao,
                    MaChiTietCV = ct.MaChiTietCV,
                    SoNgayHoanThanh = ct.SoNgayHoanThanh,
                    TrangThaiText = GetTrangThaiText(ct.TrangThai),
                    TienDo = ct.TienDo,
                    MucDoUuTienText = GetMucDoUuTienText(ct.MucDoUuTien),
                }))
                .ToList();

            duocGiao_GridView.DataSource = displayData;

            duocGiao_GridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            duocGiao_GridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            duocGiao_GridView.Columns["MaCongViec"].HeaderText = "Mã CV";
            duocGiao_GridView.Columns["NgayGiao"].HeaderText = "Ngày Giao";
            duocGiao_GridView.Columns["MaChiTietCV"].HeaderText = "Mã CTCV";
            duocGiao_GridView.Columns["SoNgayHoanThanh"].HeaderText = "Số ngày hoàn thành";
            duocGiao_GridView.Columns["TrangThaiText"].HeaderText = "Trạng thái";
            duocGiao_GridView.Columns["MucDoUuTienText"].HeaderText = "Mức độ ưu tiên";
            duocGiao_GridView.Columns["TienDo"].HeaderText = "Tiến độ (%)";

            duocGiao_GridView.AllowUserToAddRows = false;
            AddActionButtonColumn();
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

        private void AddActionButtonColumn()
        {
            // Tránh thêm nhiều lần nếu load lại
            if (duocGiao_GridView.Columns.Contains("btnAction")) return;

            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Thao tác";
            btnColumn.Text = "Xem";
            btnColumn.Name = "btnAction";
            btnColumn.UseColumnTextForButtonValue = true;

            duocGiao_GridView.Columns.Add(btnColumn);
        }

        private void Task_Duoc_Giao_Control_Load(object sender, EventArgs e)
        {
            LoadData();
            radio_TheoNgay.Checked = true;
        }

        private void duocGiao_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                duocGiao_GridView.Columns[e.ColumnIndex].Name == "btnAction")
            {
                DataGridViewRow row = duocGiao_GridView.Rows[e.RowIndex];
                string maCongViec = row.Cells["maCongViec"].Value?.ToString();
                int maChiTietCV = Convert.ToInt32(
                    row.Cells["maChiTietCV"].Value?.ToString());
                bool task = false;

                Modal_ChiTiet_CongViec modal = new Modal_ChiTiet_CongViec(nd ,maCongViec, maChiTietCV, nd.MaNguoiDung, task, apiClientDAO, this);
                modal.Show();
                modal.FormClosed += (s, args) => LoadData();
            }
        }

        private void radio_TheoTrangThai_CheckedChanged(object sender, EventArgs e)
        {
            locTheoNgay = radio_TheoNgay.Checked;
            LoadData();
        }

        private void radio_TheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            locTheoNgay = !radio_TheoTrangThai.Checked;
            LoadData();
        }

        private void duocGiao_GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (duocGiao_GridView.Rows[e.RowIndex].DataBoundItem == null)
                return;

            var row = duocGiao_GridView.Rows[e.RowIndex];
            string trangThai = row.Cells["trangThaiText"].Value?.ToString();

            if (trangThai == "Chưa xử lý")
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            else if (trangThai == "Đang xử lý")
            {
                row.DefaultCellStyle.BackColor = Color.LightBlue;
            }
            else if (trangThai == "Hoàn thành")
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else if (trangThai == "Trễ")
            {
                row.DefaultCellStyle.BackColor = Color.LightPink;
            }
            else if (trangThai == "Đã hủy")
            {
                row.DefaultCellStyle.BackColor = Color.LightGray;
            }

            if (duocGiao_GridView.Columns[e.ColumnIndex].Name == "mucDoUuTienText")
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
    }
}
