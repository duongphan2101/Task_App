using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
        bool isPasswordHidden_Pass = true;
        bool isPasswordHidden_RePass = true;

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

            bool re = Invalid(hoTen, email, pass, repass, out var err);
            if (re)
            {
                MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public bool Invalid(string username, string email, string password, string rePassword, out string errorMessage)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rePassword))
            {
                errorMessage = "Vui lòng nhập đầy đủ thông tin!";
                return false;
            }
            var usernamePattern = new Regex(@"^[\p{L}\s'-]+$");

            if (string.IsNullOrWhiteSpace(username))
            {
                errorMessage = "Tên không được để trống.";
                return true;
            }

            if (!usernamePattern.IsMatch(username))
            {
                errorMessage = "Tên không được chứa số hoặc ký tự lạ (chỉ chữ, khoảng trắng, -, ').";
                return true;
            }

            // Email: phải kết thúc chính xác bằng @intimexhcm.com
            var emailPattern = new Regex(@"^[A-Za-z0-9._%+\-]+@intimexhcm\.com$", RegexOptions.IgnoreCase);
            if (string.IsNullOrWhiteSpace(email))
            {
                errorMessage = "Email không được để trống.";
                return true;
            }

            if (!emailPattern.IsMatch(email))
            {
                errorMessage = "Email phải là một địa chỉ thuộc domain @intimexhcm.com.";
                return true;
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rePassword))
            {
                errorMessage = "Mật khẩu và xác nhận mật khẩu không được để trống.";
                return true;
            }

            if (password != rePassword)
            {
                errorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.";
                return true;
            }


            errorMessage = null;
            return false;
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
            txt_Pass.PasswordChar = '●';
            txt_Pass.IconRight = Task_App.Properties.Resources.notshow;
            txt_Pass.IconRightCursor = Cursors.Hand;

            txt_Pass.IconRightClick += Txt_Pass_IconRightClick;

            txt_RePass.PasswordChar = '●';
            txt_RePass.IconRight = Task_App.Properties.Resources.notshow;
            txt_RePass.IconRightCursor = Cursors.Hand;

            txt_RePass.IconRightClick += txt_RePass_IconRightClick;
        }

        private void Txt_Pass_IconRightClick(object sender, EventArgs e)
        {
            if (isPasswordHidden_Pass)
            {
                // Hiển thị password dạng text
                txt_Pass.PasswordChar = '\0';
                txt_Pass.IconRight = Task_App.Properties.Resources.show;
                isPasswordHidden_Pass = false;
            }
            else
            {
                // Ẩn password
                txt_Pass.PasswordChar = '●';
                txt_Pass.IconRight = Task_App.Properties.Resources.notshow;
                isPasswordHidden_Pass = true;
            }
        }

        private void txt_RePass_IconRightClick(object sender, EventArgs e)
        {
            if (isPasswordHidden_RePass)
            {
                // Hiển thị password dạng text
                txt_RePass.PasswordChar = '\0';
                txt_RePass.IconRight = Task_App.Properties.Resources.show;
                isPasswordHidden_RePass = false;
            }
            else
            {
                // Ẩn password
                txt_RePass.PasswordChar = '●';
                txt_RePass.IconRight = Task_App.Properties.Resources.notshow;
                isPasswordHidden_RePass = true;
            }
        }

    }
}
