using DevExpress.XtraEditors.SyntaxEditor;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using Task_App.Model;
using Task_App.Services;
using Task_App.TaskApp_Dao;
using Task_App.views;

namespace Task_App
{
    public partial class FormLogin : Form
    {
        private readonly EmailAddressAttribute emailChecker = new EmailAddressAttribute();
        private readonly TcpClientDAO tcpClientDAO;
        public FormLogin(TcpClientDAO tcpClientDAO)
        {
            InitializeComponent();
            this.tcpClientDAO = tcpClientDAO;
            txtEmail.Focus();
        }

        private void OnLoginBtnPressed(object sender, EventArgs e)
        {
            Login();
        }

        // note that the form itself doesn't trigger this event
        //   and the Form.Enter is NOT the same as the Enter key
        // this event handler is bound to the 2 inputs instead
        private void OnKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            Login();
        }

        private void OnExitBtnPressed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login()
        {
            string email = txtEmail.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (emailChecker.IsValid(email) == false)
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maNguoiDung = tcpClientDAO.CheckLogin(email, pass);
            if (maNguoiDung == -1)
            {
                MessageBox.Show("Sai Email hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtPass.Text = string.Empty;

            Hide();

            var homeForm = new Home(maNguoiDung, tcpClientDAO);
            homeForm.FormClosed += (s, args) => Show();
            homeForm.Show();
        }
    }
}
