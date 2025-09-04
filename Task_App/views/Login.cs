using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_App.DTO;
using Task_App.Model;
using Task_App.Response;
using Task_App.TaskApp_Dao;
using Task_App.views;

namespace Task_App
{
    public partial class FormLogin : Form
    {
        private readonly EmailAddressAttribute emailChecker = new EmailAddressAttribute();
        private readonly ApiClientDAO apiClientDAO;
        private LoginResponse response;

        public FormLogin(ApiClientDAO apiClientDAO)
        {
            InitializeComponent();
            this.apiClientDAO = apiClientDAO;
            txtEmail.Focus();
        }

        private void OnLoginBtnPressed(object sender, EventArgs e)
        {
            Login();
        }

        private void OnKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            Login();
        }

        private void OnExitBtnPressed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async Task Login()
        {
            string email = txtEmail.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (email != "admin")
            {
                if (emailChecker.IsValid(email) == false)
                {
                    MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var loginResult = await apiClientDAO.CheckLoginAsync(email, pass);
            response = loginResult;

            if (loginResult == null)
            {
                MessageBox.Show("Sai email hoặc mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GlobalSession.Token = response.Token;

            txtPass.Text = string.Empty;
            txtEmail.Focus();

            Hide();

            var resLogin = await apiClientDAO.GetGetNguoiDungByIdAsync(response.UserId);
            NguoiDung nd = resLogin.Data;
            tmp.mk = pass;

            if(nd.TrangThai == 0)
            {
                MessageBox.Show("Tài khoản chưa được kích hoạt, vui lòng liên hệ admin để kích hoạt tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (nd.IsAdmin == 0)
            {
                var homeForm = new Home(nd, apiClientDAO);
                homeForm.FormClosed += (s, args) => Show();
                homeForm.Show();
            }
            else
            {
                var adminForm = new AdminForm(nd, apiClientDAO);
                adminForm.FormClosed += (s, args) => Show();
                adminForm.Show();
            }
        }

        private void btn_DangKy_Click(object sender, EventArgs e)
        {
            var registerForm = new Register(apiClientDAO);
            registerForm.FormClosed += (s, args) => Show();
            registerForm.Show();
        }
    }
}
