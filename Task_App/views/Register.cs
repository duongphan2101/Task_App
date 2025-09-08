using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_App.Model;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class Register : Form
    {
        private readonly EmailAddressAttribute emailChecker = new EmailAddressAttribute();
        private ApiClientDAO apiClientDAO;
        string MaDV = "";
        string MaPB = "";
        string MaCV = "";

        public Register(ApiClientDAO apiClientDAO)
        {
            InitializeComponent();

            this.apiClientDAO = apiClientDAO;
        }

        public async void SetItemComboBox()
        {
            var resDV = await apiClientDAO.GetDonVi();
            var listDV = resDV.Data;

            cb_DV.DataSource = listDV;
            cb_DV.DisplayMember = "TenDonVi";
            cb_DV.ValueMember = "MaDonVi"; 

            cb_DV.SelectedIndex = 0;

            var resPB = await apiClientDAO.GetPhongBan();
            var listPB = resPB.Data;

            cb_PB.DataSource = listPB;
            cb_PB.DisplayMember = "TenPhongBan";
            cb_PB.ValueMember = "MaPhongBan";

            cb_PB.SelectedIndex = 0;

            var resCV = await apiClientDAO.GetChucVu();
            var listCV = resCV.Data;

            cb_CV.DataSource = listCV;
            cb_CV.DisplayMember = "TenChucVu";
            cb_CV.ValueMember = "MaChucVu";

            cb_CV.SelectedIndex = 0;

            txt_Pass.PasswordChar = '●';
            txt_RePass.PasswordChar = '●';
        }

        private async void btn_DangKy_Click(object sender, EventArgs e)
        {
            string hoTen = txt_UserName.Text.Trim();
            string email = txt_Email.Text.Trim();
            string pass = txt_Pass.Text.Trim();
            string repass = txt_RePass.Text.Trim();
            string donvi = cb_DV.SelectedItem.ToString();
            string phongban = cb_PB.SelectedItem.ToString();
            string chucvu = cb_CV.SelectedItem.ToString();

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(repass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (emailChecker.IsValid(email) == false)
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pass != repass)
            {
                MessageBox.Show("Mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NguoiDung nd = new NguoiDung
            {
                HoTen = hoTen,
                Email = email,
                MatKhau = pass,
                MaDonVi = MaDV,
                MaPhongBan = MaPB,
                MaChucVu = MaCV,
                IsAdmin = 0,
                TrangThai = 0,
                LaLanhDao = false
            };

            var resRegister = await apiClientDAO.Register(nd);

            if (resRegister.Success)
            {
                MessageBox.Show("Đăng ký thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                Console.WriteLine(resRegister.Message);
                MessageBox.Show("Đăng ký thất bại! " + resRegister.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cb_DV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_DV.SelectedItem is DonVi dv)
            {
                MaDV = dv.MaDonVi;
                string tenDonVi = dv.TenDonVi;
                Console.WriteLine($"Đã chọn: {tenDonVi} ({MaDV})");
            }
        }

        private void cb_PB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_PB.SelectedItem is PhongBan pb)
            {
                MaPB = pb.MaPhongBan;
                string tenPhongBan = pb.TenPhongBan;
                Console.WriteLine($"Đã chọn: {tenPhongBan} ({MaPB})");
            }

        }

        private void cb_CV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_CV.SelectedItem is ChucVu cv)
            {
                MaCV = cv.MaChucVu;
                string tenChucVu = cv.TenChucVu;
                Console.WriteLine($"Đã chọn: {tenChucVu} ({MaCV})");
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            SetItemComboBox();
        }
    }
}
