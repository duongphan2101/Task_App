using System;
using System.Windows.Forms;

namespace Task_App.views
{
    public partial class RealPass : Form
    {
        private bool showPass = false;
        public RealPass()
        {
            InitializeComponent();
        }

        private void RealPass_Load(object sender, EventArgs e)
        {
            if (!showPass)
            {
                txtTmpPass.PasswordChar = '●';
            }
            else
            {
                txtTmpPass.PasswordChar = '\0';
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string tmp = txtTmpPass.Text.Trim();
            if(string.IsNullOrEmpty(tmp))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu tạm thời!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DTO.TmpPass.Pwd = tmp;
            MessageBox.Show("Mật khẩu của email sẽ được lưu ở phiên làm việc hiện tại và lưu tại máy, nên khi tắt app " +
                "mật khẩu sẽ được tự động xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            Console.WriteLine("Pass : " + DTO.TmpPass.Pwd);
        }

        private void eyes_Click(object sender, EventArgs e)
        {
            if (!showPass)
            {
                txtTmpPass.PasswordChar = '\0';
                showPass = true;
                eyes.Image = Properties.Resources.show;
            }
            else
            {
                txtTmpPass.PasswordChar = '●';
                showPass = false;
                eyes.Image = Properties.Resources.notshow;
            }
        }

    }
}
