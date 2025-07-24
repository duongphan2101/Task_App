using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Task_App.Services;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class Task_Duoc_Giao_Control : UserControl
    {
        private int maNguoiDung;
        private Task_Duoc_Giao_Control duocGiaoControl;
        private bool locTheoNgay = true;
        private CongViecService congViecService;
        private int maChiTietCV;
        private TcpClientDAO tcpClientDAO;

        public Task_Duoc_Giao_Control(int id, TcpClientDAO tcpClientDAO)
        {
            maNguoiDung = id;
            this.tcpClientDAO = tcpClientDAO;
            congViecService = new CongViecService(tcpClientDAO);
            InitializeComponent();
            this.Load += Task_Duoc_Giao_Control_Load;
        }

        private void LoadData()
        {
            DataTable dt = new DataTable();

            if (locTheoNgay)
            {
                dt = congViecService.GetCongViecDuocGiao(maNguoiDung);
            }
            else
            {
                dt = tcpClientDAO.GetCongViecDuocGiao_SortByTrangThai(maNguoiDung);
            }

            // Thêm cột "trangThaiText" để hiển thị chữ
            dt.Columns.Add("trangThaiText", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                int trangThaiInt = Convert.ToInt32(row["trangThai"]);
                string trangThaiText = "";

                switch (trangThaiInt)
                {
                    case 0:
                        trangThaiText = "Chưa xử lý";
                        break;
                    case 1:
                        trangThaiText = "Đang xử lý";
                        break;
                    case 2:
                        trangThaiText = "Hoàn thành";
                        break;
                    case 3:
                        trangThaiText = "Trễ";
                        break;
                    case 4:
                        trangThaiText = "Đã hủy";
                        break;
                    default:
                        trangThaiText = "Không xác định";
                        break;
                }

                row["trangThaiText"] = trangThaiText;
            }

            DataTable dtDisplay = dt.DefaultView.ToTable(false,
                "maCongViec", "maChiTietCV", "tieuDe", "ngayNhanCongViec", "ngayKetThucCongViec", "trangThaiText", "tienDo", "nguoiGiao_HoTen"
            );

            duocGiao_GridView.DataSource = dtDisplay;

            duocGiao_GridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            duocGiao_GridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            duocGiao_GridView.Columns["maCongViec"].HeaderText = "Mã CV";
            duocGiao_GridView.Columns["maChiTietCV"].HeaderText = "Mã CTCV";
            duocGiao_GridView.Columns["tieuDe"].HeaderText = "Tiêu đề";
            duocGiao_GridView.Columns["ngayNhanCongViec"].HeaderText = "Ngày giao";
            duocGiao_GridView.Columns["ngayKetThucCongViec"].HeaderText = "Ngày Kết Thúc";
            duocGiao_GridView.Columns["trangThaiText"].HeaderText = "Trạng thái";
            duocGiao_GridView.Columns["tienDo"].HeaderText = "Tiến độ (%)";

            duocGiao_GridView.Columns["nguoiGiao_HoTen"].HeaderText = "Người giao";

            // Không cho phép thêm dòng trống
            duocGiao_GridView.AllowUserToAddRows = false;
            AddActionButtonColumn();
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

                Modal_ChiTiet_CongViec modal = new Modal_ChiTiet_CongViec(maCongViec, maChiTietCV, maNguoiDung, task, tcpClientDAO);
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
        }
    }
}
