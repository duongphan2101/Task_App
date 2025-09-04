using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_App.DTO;

namespace Task_App.views
{
    public partial class User_Setting : UserControl
    {
        public User_Setting()
        {
            InitializeComponent();
        }

        private void User_Setting_Load(object sender, EventArgs e)
        {
            txt_path.Text = Duong_Dan.DuongDan;
            txt_passEmail.Text = DTO.TmpPass.Pwd;
            //tableLayoutPanel1.Size = new System.Drawing.Size(main_flow.Width, main_flow.Height/4);
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

    }
}
