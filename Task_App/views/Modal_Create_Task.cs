using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Task_App.Model;
using Task_App.Services;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class Modal_Create_Task : Form
    {
        //private readonly NguoiDungService NguoiDungService = new NguoiDungService();
        //private readonly NguoiDungDAO NguoiDungDAO = new NguoiDungDAO();
        //private readonly CongViecDAO CongViecDAO = new CongViecDAO();
        private readonly TcpClientDAO tcpClientDAO;

        private int maNguoiDung;
        private List<string> selectedEmail_to = new List<string>();
        private List<string> selectedEmail_cc = new List<string>();
        private List<string> selectedEmail_bcc = new List<string>();
        private List<TepDinhKemEmail> lstTepDinhKem = new List<TepDinhKemEmail>();
        private List<TepTin> lstTepDaChon = new List<TepTin>();
        private int mucDo = 0; // 0: Bình thường, 1: Quan trọng, 2: Khẩn cấp

        private Create_Task_Control tdg;

        public Modal_Create_Task(int id, TcpClientDAO tcpClientDAO, Create_Task_Control tdg)
        {
            maNguoiDung = id;
            this.tcpClientDAO = tcpClientDAO;
            this.tdg = tdg;
            InitializeComponent();
            radio_Khong.Checked = true;
            emailPopup.Visible = false;
            DeadLine.Value = DateTime.Now;
            StartDay.Value = DateTime.Now;

            cbMucDo.Items.Add("Bình Thường");
            cbMucDo.Items.Add("Quan Trọng");
            cbMucDo.Items.Add("Khẩn Cấp");
            cbMucDo.SelectedIndex = 0;

            txtTieuDe.Focus();
        }

        private List<string> emailSuggestions()
        {
            List<string> lstStr = new List<string>();
            NguoiDung u = tcpClientDAO.GetNguoiDung(maNguoiDung);
            List<NguoiDung> lst = tcpClientDAO.getDanhSachNguoiDungByDonViVaPhongBan(u.DonVi.MaDonVi, u.PhongBan.MaPhongBan, u.Email);
            foreach(NguoiDung nd in lst)
            {
                lstStr.Add(nd.Email);
            }
            return lstStr;
        }

        private void Modal_Create_Task_Load(object sender, EventArgs e)
        {
            // Nếu muốn load danh sách từ DB, bạn có thể gọi tại đây
            // emailSuggestions = nguoiDungService.getAllEmails();
        }
        
        private void HienThiDanhSachFile()
        {
            int stt = 1;
            flowFiles.Controls.Clear();

            foreach (TepTin tep in lstTepDaChon)
            {
                Panel panel = new Panel();
                panel.Width = 60;
                panel.Height = flowFiles.Height;
                panel.Margin = new Padding(5);
                panel.BackColor = Color.Transparent;

                string extension = Path.GetExtension(tep.TenTep).ToLower();


                Image iconImage = Properties.Resources.icons8_document_48;

                if (extension == ".pdf")
                    iconImage = Properties.Resources.pdf_icon;
                else if (extension == ".doc" || extension == ".docx")
                    iconImage = Properties.Resources.word_icon;
                else if (extension == ".xls" || extension == ".xlsx")
                    iconImage = Properties.Resources.excel_icon;
                else if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp" || extension == ".gif")
                    iconImage = Properties.Resources.image_icon;
                else
                    iconImage = Properties.Resources.icons8_document_48;

                PictureBox icon = new PictureBox();
                icon.Width = 48;
                icon.Height = 48;
                icon.SizeMode = PictureBoxSizeMode.StretchImage;
                icon.Image = iconImage;
                icon.Location = new Point(0, 0);
                icon.BackColor = Color.Transparent;
                icon.Cursor = Cursors.Hand;

                Button btnXoa = new Button();
                btnXoa.Text = "";
                btnXoa.Width = 15;
                btnXoa.Height = 15;
                btnXoa.BackColor = Color.Red;
                btnXoa.ForeColor = Color.White;
                btnXoa.FlatStyle = FlatStyle.Flat;
                btnXoa.FlatAppearance.BorderSize = 0;
                btnXoa.Font = new Font("Arial", 8, FontStyle.Bold);
                btnXoa.Location = new Point(icon.Right, icon.Top);
                btnXoa.Cursor = Cursors.Hand;
                btnXoa.TabIndex = 3;

                btnXoa.Click += (s, e) =>
                {
                    lstTepDaChon.Remove(tep);
                    HienThiDanhSachFile();
                };

                Label lbl = new Label();
                lbl.Text = ShortenFileName(tep.TenTep, 8);
                lbl.TextAlign = ContentAlignment.TopCenter;
                lbl.Width = panel.Width;
                lbl.Height = 30;
                lbl.Location = new Point(0, icon.Bottom + 5);
                lbl.Font = new Font("Arial", 8);
                lbl.AutoEllipsis = true;

                panel.Controls.Add(icon);
                panel.Controls.Add(btnXoa);
                panel.Controls.Add(lbl);
                flowFiles.Controls.Add(panel);

                stt++;
            }

        }

        private string ShortenFileName(string fileName, int maxLength)
        {
            if (fileName.Length <= maxLength)
                return fileName;
            else
                return fileName.Substring(0, maxLength) + "...";
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            TepTin tep = btn.Tag as TepTin;

            if (tep != null)
            {
                lstTepDaChon.Remove(tep);
                HienThiDanhSachFile();
            }
        }

        private void btn_File_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn tệp";
            openFileDialog.Filter =
             "Tất cả tệp thường dùng|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx|" +
             "Ảnh|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
             "PDF|*.pdf|" +
             "Word|*.doc;*.docx|" +
             "Excel|*.xls;*.xlsx|" +
             "PowerPoint|*.ppt;*.pptx|" +
             "Tất cả các tệp|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                int stt = 1;
                foreach (string filePath in openFileDialog.FileNames)
                {
                    // Tránh trùng file
                    if (!lstTepDaChon.Any(f => f.DuongDan == filePath))
                    {
                        TepTin tep = new TepTin
                        {
                            MaTep = stt,
                            TenTep = Path.GetFileName(filePath),
                            DuongDan = filePath
                        };
                        lstTepDaChon.Add(tep);
                    }
                    stt++;
                }

                HienThiDanhSachFile();
            }
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            string tieuDe = txtTieuDe.Text;
            string noiDung = txtNoiDung.Text;
            bool dinhKy = false;
            bool success = true;
            string tanSuat = null;
            if (radio_Khong.Checked)
            {
                dinhKy = false;
            }
            else
            {
                dinhKy = true;
            }

            int soNgayHoanThanh = LaySoNgayHoanThanh(txtHanHoanThanh.Text);

            DateTime ngayBatDau = StartDay.Value;
            DateTime ngayKetThuc = DeadLine.Value;
            if (DeadLine.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày hoàn thành phải sau thời điểm hiện tại.");
                return;
            }

            NguoiDung currentUser = tcpClientDAO.GetNguoiDung(maNguoiDung);

            selectedEmail_to = txtEmailInput.Text
            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Where(x => IsValidEmail(x))
            .Distinct()
            .ToList();

            selectedEmail_cc = txtEmailCC.Text
            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Where(x => IsValidEmail(x))
            .Distinct()
            .ToList();

            selectedEmail_bcc = txtEmailBcc.Text
            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Where(x => IsValidEmail(x))
            .Distinct()
            .ToList();

            CongViec congViec = new CongViec();
            congViec.MaCongViec = tcpClientDAO.GenerateMaCongViecFromLast(currentUser.DonVi.MaDonVi, currentUser.PhongBan.MaPhongBan);
            congViec.NguoiGiao = currentUser.MaNguoiDung;
            congViec.NguoiGiaoObj = currentUser;
            congViec.NgayGiao = ngayBatDau;
            congViec.NgayBatDau = ngayBatDau;
            congViec.NgayKetThuc = ngayKetThuc;
            congViec.LapLai = dinhKy;

            if (radio_Ngay.Checked)
            {
                tanSuat = "ngay";

            }else if (radio_Tuan.Checked)
            {
                tanSuat = "tuan";

            }else if (radio_Thang.Checked)
            {
                tanSuat = "thang";
            }else if (radio_Nam.Checked)
            {
                tanSuat = "nam";
            }

            congViec.TanSuat = tanSuat;

            if (tcpClientDAO.createCongViec(congViec))
            {
                Console.WriteLine("Tạo Task Thành Công!");
            }
            else
            {
                Console.WriteLine("Tạo Task Thất Bại");
            }

            //
            if (dinhKy)
            {
                CongViecService service = new CongViecService(tcpClientDAO);

                List<ChiTietCongViec> danhSachChiTiet = tcpClientDAO.TaoChiTietCongViecTheoTanSuat(congViec, soNgayHoanThanh, mucDo);

                foreach (ChiTietCongViec ct in danhSachChiTiet)
                {
                    ct.TieuDe = tieuDe;
                    ct.NoiDung = noiDung;
                    ct.TrangThai = 0;
                    ct.TienDo = 0;
                    ct.MaCongViec = congViec.MaCongViec;
                    ct.CongViec = congViec;
                    ct.SoNgayHoanThanh = soNgayHoanThanh;

                    //ct.HanHoanThanh = TinhHanHoanThanh(txtHanHoanThanh.Text, ngayBatDau);

                    int maChiTietMoi = tcpClientDAO.CreateChiTietCongViec(ct);
                    if (maChiTietMoi > 0)
                    {
                        ct.MaChiTietCV = maChiTietMoi;
                        Console.WriteLine("Tạo Task_Detail Thành Công!");
                    }
                    else
                    {
                        Console.WriteLine("Tạo Task_Detail Thất Bại");
                    }

                    //TO
                    List<NguoiDung> lstNguoiNhan_to = tcpClientDAO.getDanhSachNguoiDungByEmails(selectedEmail_to);
                    foreach (NguoiDung nd in lstNguoiNhan_to)
                    {
                        NguoiLienQuanCongViec nlqcv = new NguoiLienQuanCongViec();
                        nlqcv.MaCongViec = congViec.MaCongViec;
                        nlqcv.MaNguoiDung = nd.MaNguoiDung;
                        nlqcv.VaiTro = "to";
                        nlqcv.CongViec = congViec;
                        nlqcv.NguoiDung = nd;
                        if (!tcpClientDAO.IsNguoiLienQuanExists(congViec.MaCongViec, nd.MaNguoiDung, "to"))
                        {
                            if (tcpClientDAO.CreateNguoiLienQuan(nlqcv))
                            {
                                Console.WriteLine("Tạo NLQCV_to Thành Công!");
                            }
                            else
                            {
                                Console.WriteLine("Tạo NLQCV_to Thất Bại");
                            }
                        }
                    }

                    //CC
                    List<NguoiDung> lstNguoiNhan_cc = tcpClientDAO.getDanhSachNguoiDungByEmails(selectedEmail_cc);
                    foreach (NguoiDung nd in lstNguoiNhan_cc)
                    {
                        NguoiLienQuanCongViec nlqcv = new NguoiLienQuanCongViec();
                        nlqcv.MaCongViec = congViec.MaCongViec;
                        nlqcv.MaNguoiDung = nd.MaNguoiDung;
                        nlqcv.VaiTro = "cc";
                        nlqcv.CongViec = congViec;
                        nlqcv.NguoiDung = nd;
                        if (!tcpClientDAO.IsNguoiLienQuanExists(congViec.MaCongViec, nd.MaNguoiDung, "cc"))
                        {
                            if (tcpClientDAO.CreateNguoiLienQuan(nlqcv))
                            {
                                Console.WriteLine("Tạo NLQCV_cc Thành Công!");
                            }
                            else
                            {
                                Console.WriteLine("Tạo NLQCV_cc Thất Bại");
                            }
                        }
                    }

                    //BCC
                    List<NguoiDung> lstNguoiNhan_bcc = tcpClientDAO.getDanhSachNguoiDungByEmails(selectedEmail_bcc);
                    foreach (NguoiDung nd in lstNguoiNhan_bcc)
                    {
                        NguoiLienQuanCongViec nlqcv = new NguoiLienQuanCongViec();
                        nlqcv.MaCongViec = congViec.MaCongViec;
                        nlqcv.MaNguoiDung = nd.MaNguoiDung;
                        nlqcv.VaiTro = "bcc";
                        nlqcv.CongViec = congViec;
                        nlqcv.NguoiDung = nd;
                        if (!tcpClientDAO.IsNguoiLienQuanExists(congViec.MaCongViec, nd.MaNguoiDung, "bcc"))
                        {
                            if (tcpClientDAO.CreateNguoiLienQuan(nlqcv))
                            {
                                Console.WriteLine("Tạo NLQCV_bcc Thành Công!");
                            }
                            else
                            {
                                Console.WriteLine("Tạo NLQCV_bcc Thất Bại");
                            }
                        }
                    }

                    Email email1 = new Email();
                    email1.MaEmail = GenerateMaEmailFromLast(congViec.MaCongViec);
                    email1.NguoiGui = currentUser.MaNguoiDung;
                    email1.MaChiTietCV = ct.MaChiTietCV;
                    email1.TieuDe = ct.TieuDe;
                    email1.NoiDung = ct.NoiDung;
                    email1.NgayGui = ct.NgayNhanCongViec;
                    email1.trangThai = 0;
                    email1.ChiTietCongViec = ct;
                    email1.NguoiGuiObj = currentUser;

                    if (tcpClientDAO.CreateEmail(email1))
                    {
                        Console.WriteLine("Tạo Email_DinhKy Thành Công!");
                    }
                    else
                    {
                        Console.WriteLine("Tạo Email_DinhKy Thất Bại");
                    }

                    List<NguoiNhanEmail> lstNguoiNhanEmail = new List<NguoiNhanEmail>();

                    foreach (NguoiDung nd in lstNguoiNhan_to)
                    {
                        if (!tcpClientDAO.IsNguoiNhanEmailExists(email1.MaEmail, nd.MaNguoiDung, "to"))
                        {

                            NguoiNhanEmail nguoiNhanEmail = new NguoiNhanEmail();
                            nguoiNhanEmail.MaEmail = email1.MaEmail;
                            nguoiNhanEmail.MaNguoiDung = nd.MaNguoiDung;
                            nguoiNhanEmail.VaiTro = "to";
                            nguoiNhanEmail.NguoiDung = nd;
                            nguoiNhanEmail.Email = email1;

                            if (tcpClientDAO.CreateNguoiNhanEmail(nguoiNhanEmail))
                            {
                                Console.WriteLine("Tạo NNE_to Thành Công!");
                                success = true;
                            }
                            else
                            {
                                Console.WriteLine("Tạo NNE_to Thất Bại");
                                success = false;
                            }
                            lstNguoiNhanEmail.Add(nguoiNhanEmail);

                            ThongBaoNguoiDung tb = new ThongBaoNguoiDung();
                            tb.MaChiTietCV = ct.MaChiTietCV;
                            tb.MaNguoiDung = nd.MaNguoiDung;
                            tb.NoiDung = $@"Bạn được giao công việc '{ct.TieuDe}' từ {congViec.NguoiGiaoObj.HoTen}";
                            tb.NgayThongBao = (DateTime)ct.NgayNhanCongViec;
                            tb.TrangThai = 0;

                            tb.ChiTietCongViec = ct;
                            tb.NguoiDung = nd;

                            createNotifications(tb);
                        }
                    }

                    foreach (NguoiDung nd in lstNguoiNhan_cc)
                    {
                        if (!tcpClientDAO.IsNguoiNhanEmailExists(email1.MaEmail, nd.MaNguoiDung, "cc"))
                        {

                            NguoiNhanEmail nguoiNhanEmail = new NguoiNhanEmail();
                            nguoiNhanEmail.MaEmail = email1.MaEmail;
                            nguoiNhanEmail.MaNguoiDung = nd.MaNguoiDung;
                            nguoiNhanEmail.VaiTro = "cc";
                            nguoiNhanEmail.NguoiDung = nd;
                            nguoiNhanEmail.Email = email1;
                            if (tcpClientDAO.CreateNguoiNhanEmail(nguoiNhanEmail))
                            {
                                Console.WriteLine("Tạo NNE_cc Thành Công!");
                                success = true;
                            }
                            else
                            {
                                Console.WriteLine("Tạo NNE_cc Thất Bại");
                                success = false;
                            }
                            lstNguoiNhanEmail.Add(nguoiNhanEmail);

                            ThongBaoNguoiDung tb = new ThongBaoNguoiDung();
                            tb.MaChiTietCV = ct.MaChiTietCV;
                            tb.MaNguoiDung = nd.MaNguoiDung;
                            tb.NoiDung = $@"Bạn được thông báo về công việc '{ct.TieuDe}' từ {congViec.NguoiGiaoObj.HoTen}";
                            tb.NgayThongBao = (DateTime)ct.NgayNhanCongViec;
                            tb.TrangThai = 0;

                            tb.ChiTietCongViec = ct;
                            tb.NguoiDung = nd;

                            createNotifications(tb);
                        }
                    }

                    foreach (NguoiDung nd in lstNguoiNhan_bcc)
                    {
                        if (!tcpClientDAO.IsNguoiNhanEmailExists(email1.MaEmail, nd.MaNguoiDung, "bcc"))
                        {

                            NguoiNhanEmail nguoiNhanEmail = new NguoiNhanEmail();
                            nguoiNhanEmail.MaEmail = email1.MaEmail;
                            nguoiNhanEmail.MaNguoiDung = nd.MaNguoiDung;
                            nguoiNhanEmail.VaiTro = "bcc";
                            nguoiNhanEmail.NguoiDung = nd;
                            nguoiNhanEmail.Email = email1;
                            if (tcpClientDAO.CreateNguoiNhanEmail(nguoiNhanEmail))
                            {
                                Console.WriteLine("Tạo NNE_bcc Thành Công!");
                                success = true;
                            }
                            else
                            {
                                Console.WriteLine("Tạo NNE_bcc Thất Bại");
                                success = false;
                            }
                            lstNguoiNhanEmail.Add(nguoiNhanEmail);

                            ThongBaoNguoiDung tb = new ThongBaoNguoiDung();
                            tb.MaChiTietCV = ct.MaChiTietCV;
                            tb.MaNguoiDung = nd.MaNguoiDung;
                            tb.NoiDung = $@"Bạn được thông báo ẩn danh về công việc '{ct.TieuDe}' từ {congViec.NguoiGiaoObj.HoTen}";
                            tb.NgayThongBao = (DateTime)ct.NgayNhanCongViec;
                            tb.TrangThai = 0;

                            tb.ChiTietCongViec = ct;
                            tb.NguoiDung = nd;

                            createNotifications(tb);
                        }
                    }

                    int stt = 1;
                    string targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Attachments");
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    foreach (TepTin t in lstTepDaChon)
                    {
                        string fileName = Path.GetFileName(t.DuongDan);
                        string newFileName = $"{email1.MaEmail}_{stt}{Path.GetExtension(fileName)}";
                        string destPath = Path.Combine(targetFolder, newFileName);

                        File.Copy(t.DuongDan, destPath, true);

                        t.DuongDan = destPath;
                        t.TenTep = newFileName;
                        t.TenTepGoc = fileName;

                        int maTep = tcpClientDAO.CreateTepTin(t);
                        if (maTep > 0)
                        {
                            t.MaTep = maTep;
                            Console.WriteLine("Tạo Tệp Thành Công!");
                        }
                        else
                        {
                            Console.WriteLine("Tạo Tệp Thất Bại");
                        }

                        TepDinhKemEmail tepDinhKem = new TepDinhKemEmail
                        {
                            MaEmail = email1.MaEmail,
                            MaTep = t.MaTep,
                            TepTin = t,
                            Email = email1
                        };

                        if (tcpClientDAO.CreateTepTinDinhKem(tepDinhKem))
                        {
                            Console.WriteLine("Tạo TDK Thành Công!");
                        }
                        else
                        {
                            Console.WriteLine("Tạo TDK Thất Bại");
                        }

                        lstTepDinhKem.Add(tepDinhKem);
                        stt++;
                    }

                    if (success)
                    {

                        //MessageBox.Show("Tạo task phân công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.Close();
                        tdg.LoadData();
                        empty();

                    }
                    else
                    {
                        MessageBox.Show("Tạo task thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            //
            else
            {
                ChiTietCongViec chiTietCongViec = new ChiTietCongViec();
                chiTietCongViec.MaCongViec = congViec.MaCongViec;
                chiTietCongViec.TieuDe = tieuDe;
                chiTietCongViec.NoiDung = noiDung;
                chiTietCongViec.NgayNhanCongViec = congViec.NgayBatDau;
                chiTietCongViec.NgayKetThucCongViec = TinhHanHoanThanh(txtHanHoanThanh.Text, (DateTime)congViec.NgayBatDau);
                chiTietCongViec.NgayHoanThanh = null;
                //chuaxuly = 0, dangxuly = 1, hoanthanh = 2, tre = 3, huy = 4
                chiTietCongViec.TrangThai = 0;
                chiTietCongViec.TienDo = 0;
                chiTietCongViec.CongViec = congViec;
                chiTietCongViec.SoNgayHoanThanh = soNgayHoanThanh;
                chiTietCongViec.MucDoUuTien = mucDo;

                int maChiTietMoi = tcpClientDAO.CreateChiTietCongViec(chiTietCongViec);
                if (maChiTietMoi > 0)
                {
                    chiTietCongViec.MaChiTietCV = maChiTietMoi;
                    Console.WriteLine("Tạo Task_Detail Thành Công!");
                }
                else
                {
                    Console.WriteLine("Tạo Task_Detail Thất Bại");
                }


                //TO
                List<NguoiDung> lstNguoiNhan_to = tcpClientDAO.getDanhSachNguoiDungByEmails(selectedEmail_to);
                foreach (NguoiDung nd in lstNguoiNhan_to)
                {
                    NguoiLienQuanCongViec nlqcv = new NguoiLienQuanCongViec();
                    nlqcv.MaCongViec = congViec.MaCongViec;
                    nlqcv.MaNguoiDung = nd.MaNguoiDung;
                    nlqcv.VaiTro = "to";
                    nlqcv.CongViec = congViec;
                    nlqcv.NguoiDung = nd;
                    if (tcpClientDAO.CreateNguoiLienQuan(nlqcv))
                    {
                        Console.WriteLine("Tạo NLQCV_to Thành Công!");
                    }
                    else
                    {
                        Console.WriteLine("Tạo NLQCV_to Thất Bại");
                    }
                }

                //CC
                List<NguoiDung> lstNguoiNhan_cc = tcpClientDAO.getDanhSachNguoiDungByEmails(selectedEmail_cc);
                foreach (NguoiDung nd in lstNguoiNhan_cc)
                {
                    NguoiLienQuanCongViec nlqcv = new NguoiLienQuanCongViec();
                    nlqcv.MaCongViec = congViec.MaCongViec;
                    nlqcv.MaNguoiDung = nd.MaNguoiDung;
                    nlqcv.VaiTro = "cc";
                    nlqcv.CongViec = congViec;
                    nlqcv.NguoiDung = nd;
                    if (tcpClientDAO.CreateNguoiLienQuan(nlqcv))
                    {
                        Console.WriteLine("Tạo NLQCV_cc Thành Công!");
                    }
                    else
                    {
                        Console.WriteLine("Tạo NLQCV_cc Thất Bại");
                    }
                }

                //BCC
                List<NguoiDung> lstNguoiNhan_bcc = tcpClientDAO.getDanhSachNguoiDungByEmails(selectedEmail_bcc);
                foreach (NguoiDung nd in lstNguoiNhan_bcc)
                {
                    NguoiLienQuanCongViec nlqcv = new NguoiLienQuanCongViec();
                    nlqcv.MaCongViec = congViec.MaCongViec;
                    nlqcv.MaNguoiDung = nd.MaNguoiDung;
                    nlqcv.VaiTro = "bcc";
                    nlqcv.CongViec = congViec;
                    nlqcv.NguoiDung = nd;
                    if (tcpClientDAO.CreateNguoiLienQuan(nlqcv))
                    {
                        Console.WriteLine("Tạo NLQCV_bcc Thành Công!");
                    }
                    else
                    {
                        Console.WriteLine("Tạo NLQCV_bcc Thất Bại");
                    }
                }

                Email email1 = new Email();
                email1.MaEmail = GenerateMaEmailFromLast(congViec.MaCongViec);
                email1.NguoiGui = currentUser.MaNguoiDung;
                email1.MaChiTietCV = chiTietCongViec.MaChiTietCV;
                email1.TieuDe = chiTietCongViec.TieuDe;
                email1.NoiDung = chiTietCongViec.NoiDung;
                email1.NgayGui = congViec.NgayGiao;
                email1.trangThai = 1;
                email1.ChiTietCongViec = chiTietCongViec;
                email1.NguoiGuiObj = currentUser;

                if (tcpClientDAO.CreateEmail(email1))
                {
                    Console.WriteLine("Tạo Email Thành Công!");
                }
                else
                {
                    Console.WriteLine("Tạo Email Thất Bại");
                }

                List<NguoiNhanEmail> lstNguoiNhanEmail = new List<NguoiNhanEmail>();

                foreach (NguoiDung nd in lstNguoiNhan_to)
                {
                    NguoiNhanEmail nguoiNhanEmail = new NguoiNhanEmail();
                    nguoiNhanEmail.MaEmail = email1.MaEmail;
                    nguoiNhanEmail.MaNguoiDung = nd.MaNguoiDung;
                    nguoiNhanEmail.VaiTro = "to";
                    nguoiNhanEmail.NguoiDung = nd;
                    nguoiNhanEmail.Email = email1;
                    if (tcpClientDAO.CreateNguoiNhanEmail(nguoiNhanEmail))
                    {
                        Console.WriteLine("Tạo NNE_to Thành Công!");
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("Tạo NNE_to Thất Bại");
                        success = false;
                    }
                    lstNguoiNhanEmail.Add(nguoiNhanEmail);

                    ThongBaoNguoiDung tb = new ThongBaoNguoiDung();
                    tb.MaChiTietCV = chiTietCongViec.MaChiTietCV;
                    tb.MaNguoiDung = nd.MaNguoiDung;
                    tb.NoiDung = $@"Bạn được giao công việc '{chiTietCongViec.TieuDe}' từ {congViec.NguoiGiaoObj.HoTen}";
                    tb.NgayThongBao = DateTime.Now;
                    tb.TrangThai = 0;

                    tb.ChiTietCongViec = chiTietCongViec;
                    tb.NguoiDung = nd;

                    createNotifications(tb);

                }

                foreach (NguoiDung nd in lstNguoiNhan_cc)
                {
                    NguoiNhanEmail nguoiNhanEmail = new NguoiNhanEmail();
                    nguoiNhanEmail.MaEmail = email1.MaEmail;
                    nguoiNhanEmail.MaNguoiDung = nd.MaNguoiDung;
                    nguoiNhanEmail.VaiTro = "cc";
                    nguoiNhanEmail.NguoiDung = nd;
                    nguoiNhanEmail.Email = email1;
                    if (tcpClientDAO.CreateNguoiNhanEmail(nguoiNhanEmail))
                    {
                        Console.WriteLine("Tạo NNE_cc Thành Công!");
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("Tạo NNE_cc Thất Bại");
                        success = false;
                    }
                    lstNguoiNhanEmail.Add(nguoiNhanEmail);

                    ThongBaoNguoiDung tb = new ThongBaoNguoiDung();
                    tb.MaChiTietCV = chiTietCongViec.MaChiTietCV;
                    tb.MaNguoiDung = nd.MaNguoiDung;
                    tb.NoiDung = $@"Bạn được thông báo về công việc '{chiTietCongViec.TieuDe}' từ {congViec.NguoiGiaoObj.HoTen}";
                    tb.NgayThongBao = DateTime.Now;
                    tb.TrangThai = 0;

                    tb.ChiTietCongViec = chiTietCongViec;
                    tb.NguoiDung = nd;

                    createNotifications(tb);
                }

                foreach (NguoiDung nd in lstNguoiNhan_bcc)
                {
                    NguoiNhanEmail nguoiNhanEmail = new NguoiNhanEmail();
                    nguoiNhanEmail.MaEmail = email1.MaEmail;
                    nguoiNhanEmail.MaNguoiDung = nd.MaNguoiDung;
                    nguoiNhanEmail.VaiTro = "bcc";
                    nguoiNhanEmail.NguoiDung = nd;
                    nguoiNhanEmail.Email = email1;
                    if (tcpClientDAO.CreateNguoiNhanEmail(nguoiNhanEmail))
                    {
                        Console.WriteLine("Tạo NNE_bcc Thành Công!");
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("Tạo NNE_bcc Thất Bại");
                        success = false;
                    }
                    lstNguoiNhanEmail.Add(nguoiNhanEmail);

                    ThongBaoNguoiDung tb = new ThongBaoNguoiDung();
                    tb.MaChiTietCV = chiTietCongViec.MaChiTietCV;
                    tb.MaNguoiDung = nd.MaNguoiDung;
                    tb.NoiDung = $@"Bạn được thông báo ẩn danh về công việc '{chiTietCongViec.TieuDe}' từ {congViec.NguoiGiaoObj.HoTen}";
                    tb.NgayThongBao = DateTime.Now;
                    tb.TrangThai = 0;

                    tb.ChiTietCongViec = chiTietCongViec;
                    tb.NguoiDung = nd;

                    createNotifications(tb);
                }

                int stt = 1;
                string targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Attachments");
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                foreach (TepTin t in lstTepDaChon)
                {
                    string fileName = Path.GetFileName(t.DuongDan);
                    string newFileName = $"{email1.MaEmail}_{stt}{Path.GetExtension(fileName)}";
                    string destPath = Path.Combine(targetFolder, newFileName);

                    File.Copy(t.DuongDan, destPath, true);

                    t.DuongDan = destPath;
                    t.TenTep = newFileName;
                    t.TenTepGoc = fileName;

                    int maTep = tcpClientDAO.CreateTepTin(t);
                    if (maTep > 0)
                    {
                        t.MaTep = maTep;
                        Console.WriteLine("Tạo Tệp Thành Công!");
                    }
                    else
                    {
                        Console.WriteLine("Tạo Tệp Thất Bại");
                    }

                    TepDinhKemEmail tepDinhKem = new TepDinhKemEmail
                    {
                        MaEmail = email1.MaEmail,
                        MaTep = t.MaTep,
                        TepTin = t,
                        Email = email1
                    };

                    if (tcpClientDAO.CreateTepTinDinhKem(tepDinhKem))
                    {
                        Console.WriteLine("Tạo TDK Thành Công!");
                    }
                    else
                    {
                        Console.WriteLine("Tạo TDK Thất Bại");
                    }

                    lstTepDinhKem.Add(tepDinhKem);
                    stt++;
                }

                if (success)
                {
                    bool sendEmail = tcpClientDAO.sendEmail(email1, lstNguoiNhanEmail, lstTepDinhKem, currentUser);
                    bool updateEmail = tcpClientDAO.UpdateTrangThaiEmail(email1);
                    if (sendEmail && updateEmail)
                    {
                        MessageBox.Show("Đã gửi email công việc đến người người được phân công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tdg.LoadData();
                        empty();

                    }
                    else
                    {
                        MessageBox.Show("Gửi email thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Tạo task thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            //

        }

        private void empty()
        {
            txtTieuDe.Text = "";
            txtNoiDung.Text = "";
            txtEmailInput.Text = "";
            txtEmailCC.Text = "";
            txtEmailBcc.Text = "";
            txtHanHoanThanh.Text = "";
            lstTepDaChon.Clear();
            lstTepDinhKem.Clear();
            mucDo = 0;
            cbMucDo.Text = "Bình thường";
            cbMucDo.SelectedIndex = 0;
        }

        private void createNotifications(ThongBaoNguoiDung tb)
        {
            tcpClientDAO.CreateThongBao(tb);
        }

        private void txtEmailInput_TextChanged(object sender, EventArgs e)
        {
            string fullText = txtEmailInput.Text;

            string[] parts = fullText.Split(new[] { ';' }, StringSplitOptions.None);
            string currentInput = parts.Last().Trim().ToLower();

            if (string.IsNullOrWhiteSpace(currentInput))
            {
                emailPopup.Hide();
                return;
            }

            var emailsInTo = txtEmailBcc.Text
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLower());

            var emailsInCc = txtEmailCC.Text
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLower());

            var emailsToExclude = emailsInTo.Concat(emailsInCc).ToHashSet();

            var matches = emailSuggestions()
                .Where(email => email.ToLower().Contains(currentInput) && !emailsToExclude.Contains(email.ToLower()))
                .Distinct()
                .ToList();

            if (matches.Count > 0)
            {
                emailPopup.Items.Clear();

                foreach (var email in matches)
                {
                    var item = new ToolStripMenuItem(email);
                    item.Click += (s, ev) =>
                    {
                        AddEmailToTextBox(email);
                        emailPopup.Hide();
                    };
                    emailPopup.Items.Add(item);
                }

                var point = txtEmailInput.PointToScreen(new Point(0, txtEmailInput.Height));
                emailPopup.Show(point);
            }
            else
            {
                emailPopup.Hide();
            }
        }

        private void AddEmailToTextBox(string selectedEmail)
        {
            var rawParts = txtEmailInput.Text.Split(new[] { ';' }, StringSplitOptions.None)
                                             .Select(p => p.Trim())
                                             .ToList();

            rawParts.RemoveAt(rawParts.Count - 1);

            var validEmails = rawParts
                .Where(e => IsValidEmail(e))
                .Distinct()
                .ToList();

            if (IsValidEmail(selectedEmail) && !validEmails.Contains(selectedEmail))
            {
                validEmails.Add(selectedEmail);
            }

            txtEmailInput.Text = string.Join("; ", validEmails) + "; ";
            txtEmailInput.SelectionStart = txtEmailInput.Text.Length;

            selectedEmail_to = validEmails;
        }

        private void AddEmailToTextBoxCC(string selectedEmail)
        {
            var rawParts = txtEmailCC.Text.Split(new[] { ';' }, StringSplitOptions.None)
                                             .Select(p => p.Trim())
                                             .ToList();

            rawParts.RemoveAt(rawParts.Count - 1);

            var validEmails = rawParts
                .Where(e => IsValidEmail(e))
                .Distinct()
                .ToList();

            if (IsValidEmail(selectedEmail) && !validEmails.Contains(selectedEmail))
            {
                validEmails.Add(selectedEmail);
            }

            txtEmailCC.Text = string.Join("; ", validEmails) + "; ";
            txtEmailCC.SelectionStart = txtEmailCC.Text.Length;

            selectedEmail_cc = validEmails;
        }

        private void AddEmailToTextBoxBCC(string selectedEmail)
        {
            var rawParts = txtEmailBcc.Text.Split(new[] { ';' }, StringSplitOptions.None)
                                             .Select(p => p.Trim())
                                             .ToList();

            rawParts.RemoveAt(rawParts.Count - 1);

            var validEmails = rawParts
                .Where(e => IsValidEmail(e))
                .Distinct()
                .ToList();

            if (IsValidEmail(selectedEmail) && !validEmails.Contains(selectedEmail))
            {
                validEmails.Add(selectedEmail);
            }

            txtEmailBcc.Text = string.Join("; ", validEmails) + "; ";
            txtEmailBcc.SelectionStart = txtEmailCC.Text.Length;

            selectedEmail_bcc = validEmails;
        }

        private void txtEmailInput_Enter(object sender, EventArgs e)
        {
            txtEmailInput_TextChanged(sender, EventArgs.Empty);
        }

        private void txtEmailInput_Leave(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                if (!emailPopup.Bounds.Contains(Cursor.Position))
                {
                    emailPopup.Hide();
                }
                RemoveDuplicateEmails(txtEmailInput, txtEmailCC, txtEmailBcc);
            }));
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void txtEmailCC_TextChanged(object sender, EventArgs e)
        {
            string fullText = txtEmailCC.Text;

            string[] parts = fullText.Split(new[] { ';' }, StringSplitOptions.None);
            string currentInput = parts.Last().Trim().ToLower();

            if (string.IsNullOrWhiteSpace(currentInput))
            {
                emailPopup.Hide();
                return;
            }

            var emailsInTo = txtEmailInput.Text
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLower());

            var emailsInCc = txtEmailBcc.Text
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLower());

            var emailsToExclude = emailsInTo.Concat(emailsInCc).ToHashSet();

            var matches = emailSuggestions()
                .Where(email => email.ToLower().Contains(currentInput) && !emailsToExclude.Contains(email.ToLower()))
                .Distinct()
                .ToList();

            if (matches.Count > 0)
            {
                emailPopup.Items.Clear();

                foreach (var email in matches)
                {
                    var item = new ToolStripMenuItem(email);
                    item.Click += (s, ev) =>
                    {
                        AddEmailToTextBoxCC(email);
                        emailPopup.Hide();
                    };
                    emailPopup.Items.Add(item);
                }

                var point = txtEmailCC.PointToScreen(new Point(0, txtEmailCC.Height));
                emailPopup.Show(point);
            }
            else
            {
                emailPopup.Hide();
            }
        }

        private void txtEmailCC_Enter(object sender, EventArgs e)
        {
            txtEmailCC_TextChanged(sender, EventArgs.Empty);
        }

        private void txtEmailCC_Leave(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                if (!emailPopup.Bounds.Contains(Cursor.Position))
                {
                    emailPopup.Hide();
                }
                RemoveDuplicateEmails(txtEmailCC, txtEmailInput, txtEmailBcc);
            }));
        }

        private void txtEmailBcc_TextChanged(object sender, EventArgs e)
        {
            string fullText = txtEmailBcc.Text;

            // Tách các email hiện tại trong Bcc (để giữ lại)
            string[] parts = fullText.Split(new[] { ';' }, StringSplitOptions.None);
            string currentInput = parts.Last().Trim().ToLower();

            if (string.IsNullOrWhiteSpace(currentInput))
            {
                emailPopup.Hide();
                return;
            }

            // Danh sách email đã có trong To và CC (không cho gợi ý)
            var emailsInTo = txtEmailInput.Text
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLower());

            var emailsInCc = txtEmailCC.Text
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLower());

            var emailsToExclude = emailsInTo.Concat(emailsInCc).ToHashSet();

            var matches = emailSuggestions()
                .Where(email => email.ToLower().Contains(currentInput) && !emailsToExclude.Contains(email.ToLower()))
                .Distinct()
                .ToList();

            if (matches.Count > 0)
            {
                emailPopup.Items.Clear();

                foreach (var email in matches)
                {
                    var item = new ToolStripMenuItem(email);
                    item.Click += (s, ev) =>
                    {
                        AddEmailToTextBoxBCC(email);
                        emailPopup.Hide();
                    };
                    emailPopup.Items.Add(item);
                }

                var point = txtEmailBcc.PointToScreen(new Point(0, txtEmailBcc.Height));
                emailPopup.Show(point);
            }
            else
            {
                emailPopup.Hide();
            }
        }

        private void txtEmailBcc_Enter(object sender, EventArgs e)
        {
            txtEmailBcc_TextChanged(sender, EventArgs.Empty);
        }

        private void txtEmailBcc_Leave(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                if (!emailPopup.Bounds.Contains(Cursor.Position))
                {
                    emailPopup.Hide();
                }
                RemoveDuplicateEmails(txtEmailBcc, txtEmailCC, txtEmailInput);
            }));
        }

        private void RemoveDuplicateEmails(Guna.UI2.WinForms.Guna2TextBox targetBox, Guna.UI2.WinForms.Guna2TextBox otherBox1, Guna.UI2.WinForms.Guna2TextBox otherBox2)
        {
            var targetEmails = targetBox.Text
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLower())
                .Where(IsValidEmail)
                .Distinct()
                .ToList();

            var otherEmails = otherBox1.Text
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Concat(otherBox2.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => x.Trim().ToLower())
                .Where(IsValidEmail)
                .Distinct()
                .ToHashSet();

            var filteredEmails = targetEmails
                .Where(email => !otherEmails.Contains(email))
                .ToList();

            targetBox.Text = string.Join("; ", filteredEmails) + (filteredEmails.Count > 0 ? ";" : "");
        }

        public DateTime TinhHanHoanThanh(string txtSoNgayHoanThanh, DateTime ngayBatDau)
        {
            int soNgay;
            if (string.IsNullOrWhiteSpace(txtSoNgayHoanThanh))
            {
                return ngayBatDau.AddDays(1);
            }
            if (!int.TryParse(txtSoNgayHoanThanh.Trim(), out soNgay) || soNgay < 0)
            {
                MessageBox.Show("Số ngày hoàn thành không hợp lệ. Vui lòng nhập số nguyên >= 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return ngayBatDau.AddDays(1);
            }

            return ngayBatDau.AddDays(soNgay);
        }

        public int LaySoNgayHoanThanh(string txtSoNgayHoanThanh)
        {
            int soNgay;
            if (string.IsNullOrWhiteSpace(txtSoNgayHoanThanh))
            {
                return 1; // mặc định
            }
            if (!int.TryParse(txtSoNgayHoanThanh.Trim(), out soNgay) || soNgay < 0)
            {
                MessageBox.Show("Số ngày hoàn thành không hợp lệ. Vui lòng nhập số nguyên >= 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 1;
            }

            return soNgay;
        }
        
        public string GenerateMaEmailFromLast(string maCongViec)
        {
            string timePart = DateTime.Now.ToString("yyyyMMddHHmmss");
            string randomPart = Guid.NewGuid().ToString("N").Substring(0, 6);
            return $"{maCongViec}_{timePart}_{randomPart}";
        }

        private void cbMucDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMucDo.SelectedItem == null)
                return;

            string selectedValue = cbMucDo.SelectedItem.ToString().Trim().ToLower();

            switch (selectedValue)
            {
                case "bình thường":
                    mucDo = 0;
                    break;
                case "quan trọng":
                    mucDo = 1;
                    break;
                case "khẩn cấp":
                    mucDo = 2;
                    break;
                default:
                    mucDo = 0;
                    break;
            }
        }

    }
}
