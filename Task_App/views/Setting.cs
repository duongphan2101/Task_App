using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_App.DTO;
using Task_App.Model;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class User_Setting : UserControl
    {
        private NguoiDung nd;
        private ApiClientDAO apiClientDAO;
        bool isPasswordHidden_Email = true;
        bool isPasswordHidden_Account = true;
        public User_Setting(NguoiDung nd, ApiClientDAO apiClientDAO)
        {
            InitializeComponent();

            this.nd = nd;
            this.apiClientDAO = apiClientDAO;
        }

        private void User_Setting_Load(object sender, EventArgs e)
        {
            txt_path.Text = Duong_Dan.DuongDan;
            txt_PassAccount.Text = tmp.mk;

            string path = Path.Combine(Application.StartupPath, "tmpCredential.dll");
            int savedUserId = 0;
            string savedPwd = "";

            if (File.Exists(path))
            {
                byte[] readData = File.ReadAllBytes(path);
                using (var ms = new MemoryStream(readData))
                {
                    var bf = new BinaryFormatter();
                    var loaded = (UserCredential)bf.Deserialize(ms);

                    savedPwd = loaded.GetPassword();
                    savedUserId = loaded.UserId;
                }
            }

            int currentUserId = nd.MaNguoiDung;

            if (savedUserId == currentUserId)
            {
                DTO.TmpPass.Pwd = savedPwd;
                txt_passEmail.Text = savedPwd;
            }
            else
            {
                DTO.TmpPass.Pwd = "";
                txt_passEmail.Text = "";
            }

            txt_passEmail.PasswordChar = '●';
            txt_passEmail.IconRight = Task_App.Properties.Resources.notshow;
            txt_passEmail.IconRightCursor = Cursors.Hand;

            txt_passEmail.IconRightClick += Txt_passEmail_IconRightClick;

            txt_PassAccount.PasswordChar = '●';
            txt_PassAccount.IconRight = Task_App.Properties.Resources.notshow;
            txt_PassAccount.IconRightCursor = Cursors.Hand;

            txt_PassAccount.IconRightClick += txt_PassAccount_IconRightClick;

        }

        private void txt_path_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Chọn thư mục lưu trữ";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Duong_Dan.DuongDan = dialog.SelectedPath;
                    txt_path.Text = Duong_Dan.DuongDan;

                    Console.WriteLine("Path update: " + Duong_Dan.DuongDan);
                }
            }
        }

        private void Txt_passEmail_IconRightClick(object sender, EventArgs e)
        {
            if (isPasswordHidden_Email)
            {
                // Hiển thị password dạng text
                txt_passEmail.PasswordChar = '\0';
                txt_passEmail.IconRight = Task_App.Properties.Resources.show;
                isPasswordHidden_Email = false;
            }
            else
            {
                // Ẩn password
                txt_passEmail.PasswordChar = '●';
                txt_passEmail.IconRight = Task_App.Properties.Resources.notshow;
                isPasswordHidden_Email = true;
            }
        }


        private void txt_PassAccount_IconRightClick(object sender, EventArgs e)
        {
            if (isPasswordHidden_Account)
            {
                // Hiển thị password dạng text
                txt_PassAccount.PasswordChar = '\0';
                txt_PassAccount.IconRight = Task_App.Properties.Resources.show;
                isPasswordHidden_Account = false;
            }
            else
            {
                // Ẩn password
                txt_PassAccount.PasswordChar = '●';
                txt_PassAccount.IconRight = Task_App.Properties.Resources.notshow;
                isPasswordHidden_Account = true;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "tmpCredential.dll");

                // Lấy dữ liệu hiện tại
                int currentUserId = nd.MaNguoiDung;
                string currentPwd = txt_passEmail.Text;

                // Cập nhật vào DTO để dùng lại trong app
                DTO.TmpPass.Pwd = currentPwd;

                // Tạo credential mới
                var credential = new UserCredential
                {
                    UserId = currentUserId,
                    // Giả sử class UserCredential có setter cho password
                    // Nếu chỉ có method SetPassword thì gọi SetPassword(currentPwd)
                };
                credential.SetPassword(currentPwd);

                // Serialize và ghi file
                using (var ms = new MemoryStream())
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(ms, credential);
                    File.WriteAllBytes(path, ms.ToArray());
                }

                MessageBox.Show("Lưu thông tin đăng nhập thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lưu thông tin: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

        }

        private async void btn_savePassAccount_Click(object sender, EventArgs e)
        {
            string pass = txt_PassAccount.Text.Trim();
            var res = await apiClientDAO.UpdatePassAcount(nd.Email, pass);
            if (res.Success)
            {
                MessageBox.Show("Cập nhật mật khẩu tài khoản thành công.", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.FindForm()?.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine("Loi: " + res.Message);
                return;
            }
        }
    }
}
