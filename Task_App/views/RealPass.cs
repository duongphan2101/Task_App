using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Task_App.Model;

namespace Task_App.views
{
    public partial class RealPass : Form
    {
        private bool showPass = false;
        private int userId;
        public RealPass(int userId)
        {
            InitializeComponent();

            this.userId = userId;
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

            if (string.IsNullOrEmpty(tmp))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DTO.TmpPass.Pwd = tmp;

            var credential = new UserCredential { UserId = userId };
            credential.SetPassword(tmp);

            byte[] data;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, credential);
                data = ms.ToArray();
            }

            string path = Path.Combine(Application.StartupPath, "tmpCredential.dll");
            File.WriteAllBytes(path, data);

            MessageBox.Show("Lưu mật khẩu email thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

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
