using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Task_App.Model;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class AdminForm : Form
    {

        private ApiClientDAO apiClientDAO;
        private NguoiDung nd;

        public AdminForm(NguoiDung nd, ApiClientDAO apiClientDAO)
        {
            InitializeComponent();

            this.nd = nd;
            this.apiClientDAO = apiClientDAO;
        }

        private void txt_TimKiem_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Keyworld: " + txt_TimKiem.Text);
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            loadData();
            loadData1();
        }

        private async void loadData()
        {
            var resAccountInactive = await apiClientDAO.GetAccountInactive();
            var lst = resAccountInactive.Data;

            if (lst == null || !lst.Any())
            {
                Console.WriteLine("Không có dữ liệu. Msg: " + resAccountInactive.Message);
                panel_panel_center.Controls.Clear();
                Label lbl = new Label();
                lbl.Text = "Không có dữ liệu";
                lbl.AutoSize = false;
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                panel_panel_center.Controls.Add(lbl);
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                return;
            }

            var resdonVis = await apiClientDAO.GetDonVi();
            var resphongBans = await apiClientDAO.GetPhongBan();
            var reschucVus = await apiClientDAO.GetChucVu();

            var donVis = resdonVis.Data;
            var phongBans = resphongBans.Data;
            var chucVus = reschucVus.Data;

            data.AutoGenerateColumns = false;
            data.AllowUserToAddRows = false;
            data.Columns.Clear();

            var displayList = lst.Select(x => new
            {
                x.MaNguoiDung,
                x.HoTen,
                x.Email,
                DonVi = donVis.FirstOrDefault(d => d.MaDonVi == x.MaDonVi)?.TenDonVi ?? "",
                PhongBan = phongBans.FirstOrDefault(p => p.MaPhongBan == x.MaPhongBan)?.TenPhongBan ?? "",
                ChucVu = chucVus.FirstOrDefault(c => c.MaChucVu == x.MaChucVu)?.TenChucVu ?? "",
                TrangThai = x.TrangThai == 1 ? "Kích hoạt" : "Chưa kích hoạt"
            }).ToList();

            //data.DataSource = new BindingList<NguoiDung>(lst);
            data.DataSource = displayList;


            // ID
            data.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaNguoiDung",
                HeaderText = "ID",
                Name = "MaNguoiDung",
                ReadOnly = true
            });

            // Họ Tên
            data.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HoTen",
                HeaderText = "Họ Tên",
                Name = "HoTen"
            });

            // Email
            data.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "Email"
            });

            // Đơn Vị = ComboBox
            var donViCol = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "MaDonVi",
                HeaderText = "Đơn Vị",
                Name = "DonVi",
                DataSource = donVis,
                DisplayMember = "TenDonVi",
                ValueMember = "MaDonVi"
            };
            data.Columns.Add(donViCol);

            // Phòng Ban = ComboBox
            var phongBanCol = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "MaPhongBan",
                HeaderText = "Phòng Ban",
                Name = "PhongBan",
                DataSource = phongBans,
                DisplayMember = "TenPhongBan",
                ValueMember = "MaPhongBan"
            };
            data.Columns.Add(phongBanCol);

            // Chức Vụ = ComboBox
            var chucVuCol = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "MaChucVu",
                HeaderText = "Chức Vụ",
                Name = "ChucVu",
                DataSource = chucVus,
                DisplayMember = "TenChucVu",
                ValueMember = "MaChucVu"
            };
            data.Columns.Add(chucVuCol);

            // Trạng Thái = chỉ hiện text
            data.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng Thái",
                Name = "TrangThai",
                ReadOnly = true
            });

            // Nút Kích Hoạt
            if (!data.Columns.Contains("KichHoat"))
            {
                var btnCol = new DataGridViewButtonColumn
                {
                    HeaderText = "Hành động",
                    Text = "Kích hoạt",
                    Name = "KichHoat",
                    UseColumnTextForButtonValue = true
                };
                data.Columns.Add(btnCol);
            }
        }

        private async void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data.Columns[e.ColumnIndex].Name == "KichHoat" && e.RowIndex >= 0)
            {
                var row = data.Rows[e.RowIndex];

                NguoiDung u = new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(row.Cells["MaNguoiDung"].Value),
                    HoTen = row.Cells["HoTen"].Value?.ToString(),
                    Email = row.Cells["Email"].Value?.ToString(),

                    MaDonVi = row.Cells["DonVi"].Value?.ToString(),
                    MaPhongBan = row.Cells["PhongBan"].Value?.ToString(),
                    MaChucVu = row.Cells["ChucVu"].Value?.ToString(),

                    TrangThai = 1
                };

                var res = await apiClientDAO.UpdateTrangThaiNguoiDung(u);
                if (res.Success) 
                {
                    MessageBox.Show($"{res.Message}", "Thông Báo");
                    loadData();
                }
                else
                {
                    MessageBox.Show($"{res.Message}");
                    Console.WriteLine(res.Message);
                    return;
                }


            }
        }

        private void txt_TimKiem_1_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Keyworld: " + txt_TimKiem_1.Text);
        }

        private async void loadData1()
        {
            var resAccountInactive = await apiClientDAO.GetAccountActive();
            var lst = resAccountInactive.Data;

            if (lst == null || !lst.Any())
            {
                Console.WriteLine("Không có dữ liệu. Msg: " + resAccountInactive.Message);
                panel_main_main.Controls.Clear();
                Label lbl = new Label();
                lbl.Text = "Không có dữ liệu";
                lbl.AutoSize = false;
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                panel_panel_center.Controls.Add(lbl);
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                return;
            }

            var resdonVis = await apiClientDAO.GetDonVi();
            var resphongBans = await apiClientDAO.GetPhongBan();
            var reschucVus = await apiClientDAO.GetChucVu();

            var donVis = resdonVis.Data;
            var phongBans = resphongBans.Data;
            var chucVus = reschucVus.Data;

            data1.AutoGenerateColumns = false;
            data1.AllowUserToAddRows = false;
            data1.Columns.Clear();

            var displayList = lst.Select(x => new
            {
                x.MaNguoiDung,
                x.HoTen,
                x.Email,
                DonVi = donVis.FirstOrDefault(d => d.MaDonVi == x.MaDonVi)?.TenDonVi ?? "",
                PhongBan = phongBans.FirstOrDefault(p => p.MaPhongBan == x.MaPhongBan)?.TenPhongBan ?? "",
                ChucVu = chucVus.FirstOrDefault(c => c.MaChucVu == x.MaChucVu)?.TenChucVu ?? "",
                TrangThai = x.TrangThai == 1 ? "Kích hoạt" : "Chưa kích hoạt"
            }).ToList();

            //data.DataSource = new BindingList<NguoiDung>(lst);
            data1.DataSource = displayList;


            // ID
            data1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaNguoiDung",
                HeaderText = "ID",
                Name = "MaNguoiDung",
                ReadOnly = true
            });

            // Họ Tên
            data1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HoTen",
                HeaderText = "Họ Tên",
                Name = "HoTen"
            });

            // Email
            data1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "Email"
            });

            // Đơn Vị = ComboBox
            var donViCol = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "MaDonVi",
                HeaderText = "Đơn Vị",
                Name = "DonVi",
                DataSource = donVis,
                DisplayMember = "TenDonVi",
                ValueMember = "MaDonVi"
            };
            data1.Columns.Add(donViCol);

            // Phòng Ban = ComboBox
            var phongBanCol = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "MaPhongBan",
                HeaderText = "Phòng Ban",
                Name = "PhongBan",
                DataSource = phongBans,
                DisplayMember = "TenPhongBan",
                ValueMember = "MaPhongBan"
            };
            data1.Columns.Add(phongBanCol);

            // Chức Vụ = ComboBox
            var chucVuCol = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "MaChucVu",
                HeaderText = "Chức Vụ",
                Name = "ChucVu",
                DataSource = chucVus,
                DisplayMember = "TenChucVu",
                ValueMember = "MaChucVu"
            };
            data1.Columns.Add(chucVuCol);

            // Nút Kích Hoạt
            if (!data1.Columns.Contains("KichHoat"))
            {
                var btnCol = new DataGridViewButtonColumn
                {
                    HeaderText = "Hành động",
                    Text = "Cập nhật",
                    Name = "KichHoat",
                    UseColumnTextForButtonValue = true
                };
                data1.Columns.Add(btnCol);
            }
        }

        private async void data1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data1.Columns[e.ColumnIndex].Name == "KichHoat" && e.RowIndex >= 0)
            {
                var row = data1.Rows[e.RowIndex];

                NguoiDung u = new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(row.Cells["MaNguoiDung"].Value),
                    HoTen = row.Cells["HoTen"].Value?.ToString(),
                    Email = row.Cells["Email"].Value?.ToString(),

                    MaDonVi = row.Cells["DonVi"].Value?.ToString(),
                    MaPhongBan = row.Cells["PhongBan"].Value?.ToString(),
                    MaChucVu = row.Cells["ChucVu"].Value?.ToString(),

                    TrangThai = 1
                };

                var res = await apiClientDAO.UpdateTrangThaiNguoiDung(u);
                if (res.Success)
                {
                    MessageBox.Show($"{res.Message}", "Thông Báo");
                    loadData1();
                }
                else
                {
                    MessageBox.Show($"{res.Message}");
                    Console.WriteLine(res.Message);
                    return;
                }


            }
        }

    }
}
