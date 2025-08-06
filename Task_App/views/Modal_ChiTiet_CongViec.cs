using Guna.UI2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Task_App.Model;
using Task_App.Services;
using Task_App.TaskApp_Dao;


namespace Task_App.views
{
    public partial class Modal_ChiTiet_CongViec : Form
    {
        private string maCongViec;
        private int maNguoiDung;
        private int maChiTietCV;
        private bool task;

        private List<TepTin> lstTepDaChon = new List<TepTin>();

        private TcpClientDAO tcpClientDAO;

        private string tt = "";
        private ChiTietCongViec ctcv = new ChiTietCongViec();
        private int tienDoReport = 0;
        ToolTip tip = new ToolTip();

        private Timer animationTimer;
        private bool isPopupOpen = true;
        private int animationStep = 180;
        private int collapsedWidth = 30;
        private int originalPopupWidth;
        private bool isExpanding = false;
        private Task_Duoc_Giao_Control tdg;

        private List<PhanHoiCongViec> lstFeedback = new List<PhanHoiCongViec>();

        public Modal_ChiTiet_CongViec(string maCongViec, int maChiTietCV, int maNguoiDung, bool task, TcpClientDAO tcpClientDAO, Task_Duoc_Giao_Control tdg)
        {
            InitializeComponent();
            this.maCongViec = maCongViec;
            this.maNguoiDung = maNguoiDung;
            this.maChiTietCV = maChiTietCV;
            this.task = task;
            this.tcpClientDAO = tcpClientDAO;
            this.tdg = tdg;

            this.Load += Modal_ChiTiet_CongViec_Load;

            animationTimer = new Timer();
            animationTimer.Interval = 3;
            animationTimer.Tick += AnimationTimer_Tick;
            popupContainerControl.Show();
            originalPopupWidth = popupContainerControl.Width;
        }

        private void Modal_ChiTiet_CongViec_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            CongViecService service = new CongViecService(tcpClientDAO);
            DataTable dt = service.GetCongViecChiTiet(maChiTietCV);
            DataTable ds = service.GetDanhSachNguoiLienQuanCongViecByIdCongViec(maCongViec);

            List<NguoiLienQuanCongViec> lstCc = new List<NguoiLienQuanCongViec>();
            List<NguoiLienQuanCongViec> lstBcc = new List<NguoiLienQuanCongViec>();
            List<NguoiLienQuanCongViec> lstTo = new List<NguoiLienQuanCongViec>();
            lstFeedback = tcpClientDAO.GetFeedbacksByMaCongViec(maCongViec);

            NguoiDung nd = tcpClientDAO.GetNguoiDung(maNguoiDung);
            flow_FeedBack.Controls.Clear();
            foreach (PhanHoiCongViec fb in lstFeedback)
            {
                bool isMe = fb.NguoiDung.HoTen == nd.HoTen;
                AddFeedback(fb, isMe);
            }

            HienThiDanhSachFile();

            //DaGiao
            if (task)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblTieuDe.Text = row["tieuDe"].ToString();
                    txtNoiDung.Text = row["noiDung"].ToString();
                    //lblThoiGianHoanThanh.Text = Convert.ToDateTime(row["hanHoanThanh"]).ToString("dd/MM/yyyy HH:mm");
                    lblThoiGianHoanThanh.Text = "Thời gian hoàn thành: Chưa có";

                    progress_Bar.Value = Convert.ToInt32(row["tienDo"]);
                    progress_Bar.ShowPercentage = true;
                    lblNgayBatDau.Text = Convert.ToDateTime(row["ngayNhanCongViec"]).ToString("dd/MM/yyyy");
                    lblHanHoanThanh.Text = " - " + Convert.ToDateTime(row["ngayKetThucCongViec"]).ToString("dd/MM/yyyy");
                    int trangThai = Convert.ToInt32(row["trangThai"]);
                    string tt = "";

                    if (trangThai == 0)
                    {
                        tt = "Chưa xử lí";
                    }
                    else if (trangThai == 1)
                    {
                        tt = "Đang xử lí";
                    }
                    else if (trangThai == 2)
                    {
                        tt = "Hoàn thành";
                    }
                    else if (trangThai == 3)
                    {
                        tt = "Trễ";
                    }
                    else
                    {
                        tt = "Hủy";
                    }

                    lblTrangThai.Text = "Trạng thái: " + tt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin công việc.");
                    this.Close();
                }

                List<NguoiLienQuanCongViec> lst = service.GetListNguoiLienQuanCongViecByIdCongViec(maCongViec);
                if (lst.Count > 0)
                {
                    foreach (NguoiLienQuanCongViec nlq in lst)
                    {
                        if (nlq.VaiTro == "to")
                        {
                            lstTo.Add(nlq);
                        }
                        if (nlq.VaiTro == "cc")
                        {
                            lstCc.Add(nlq);
                        }
                        if (nlq.VaiTro == "bcc")
                        {
                            lstBcc.Add(nlq);
                        }
                    }
                    lblCc.Text = "CC: " + string.Join(", ", lstCc.Select(x => x.NguoiDung.HoTen));
                    lblBc.Text = "BCC: " + string.Join(", ", lstBcc.Select(x => x.NguoiDung.HoTen));
                    lblNguoi.Text = "To: " + string.Join(", ", lstTo.Select(x => x.NguoiDung.HoTen));

                }
                btnHoanThanh.Visible = false;
            }
            //DuocGiao
            else
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblTieuDe.Text = row["tieuDe"].ToString();
                    txtNoiDung.Text = row["noiDung"].ToString();

                    lblThoiGianHoanThanh.Text = "Thời gian hoàn thành: Chưa có";

                    progress_Bar.Value = Convert.ToInt32(row["tienDo"]);
                    progress_Bar.ShowPercentage = true;
                    lblNgayBatDau.Text = Convert.ToDateTime(row["ngayNhanCongViec"]).ToString("dd/MM/yyyy");
                    lblHanHoanThanh.Text = " - " + Convert.ToDateTime(row["ngayKetThucCongViec"]).ToString("dd/MM/yyyy");
                    lblNguoi.Text = "From: " + row["nguoiGiao_HoTen"].ToString();

                    btnHoanThanh.Enabled = false;
                    btnHoanThanh.BackColor = Color.Transparent;
                    int trangThai = Convert.ToInt32(row["trangThai"]);
                    if (trangThai == 0)
                    {
                        tt = "Chưa xử lí";
                        btnHoanThanh.FillColor = Color.LightSkyBlue;
                        btnHoanThanh.Enabled = true;
                        btnHoanThanh.Text = "Nhận việc";

                        ctcv = tcpClientDAO.getChiTietCongViecById(maChiTietCV);
                        ctcv.MaCongViec = row["maCongViec"].ToString();
                        ctcv.TrangThai = 1;
                        ctcv.TienDo = 0;
                        ctcv.NgayHoanThanh = DateTime.Now;
                    }
                    else if (trangThai == 1)
                    {
                        tt = "Đang xử lí";
                        btnHoanThanh.FillColor = Color.LightGreen;
                        btnHoanThanh.Enabled = true;
                        btnHoanThanh.Text = "Hoàn thành";
                        ctcv = tcpClientDAO.getChiTietCongViecById(maChiTietCV);
                        ctcv.MaCongViec = row["maCongViec"].ToString();
                        ctcv.TrangThai = 2;
                        ctcv.TienDo = 100;
                        ctcv.NgayHoanThanh = DateTime.Now;
                    }
                    else if (trangThai == 2)
                    {
                        tt = "Hoàn thành";
                        lblThoiGianHoanThanh.Text = "Hoàn thành lúc: " + Convert.ToDateTime(row["ngayHoanThanh"]).ToString("dd/MM/yyyy HH:mm");
                    }
                    else if (trangThai == 3)
                    {
                        tt = "Trễ";
                        //lblThoiGianHoanThanh.Text = "Hoàn thành lúc: " + Convert.ToDateTime(row["ngayHoanThanh"]).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        tt = "Hủy";
                    }

                    lblTrangThai.Text = "Trạng thái: " + tt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin công việc.");
                    this.Close();
                }

                List<NguoiLienQuanCongViec> lst = service.GetListNguoiLienQuanCongViecByIdCongViec(maCongViec);
                if (lst.Count > 0)
                {
                    foreach (NguoiLienQuanCongViec nlq in lst)
                    {
                        if (nlq.VaiTro == "to")
                        {
                            lstTo.Add(nlq);
                        }
                        if (nlq.VaiTro == "cc")
                        {
                            lstCc.Add(nlq);
                        }
                        if (nlq.VaiTro == "bcc")
                        {
                            lstBcc.Add(nlq);
                        }
                    }
                    lblCc.Text = "CC: " + string.Join(", ", lstCc.Select(x => x.NguoiDung.HoTen));
                    lblBc.Text = "BCC: " + string.Join(", ", lstBcc.Select(x => x.NguoiDung.HoTen));
                    //lblNguoi.Text = "To: " + string.Join(", ", lstTo.Select(x => x.NguoiDung.HoTen));

                }
            }
        }

        private void HienThiDanhSachFile()
        {
            List<TepDinhKemEmail> lstTep = tcpClientDAO.GetTepDinhKemEmailByMaCongViec(maCongViec);
            flow_Files.Controls.Clear();

            foreach (TepDinhKemEmail teps in lstTep)
            {
                TepTin tep = teps.TepTin;
                Panel panel = new Panel();
                panel.Width = 60;
                panel.Height = flow_Files.Height;
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

                Label lbl = new Label();
                lbl.Text = ShortenFileName(tep.TenTepGoc, 8);
                lbl.TextAlign = ContentAlignment.TopCenter;
                lbl.Width = panel.Width;
                lbl.Height = 30;
                lbl.Location = new Point(0, icon.Bottom);
                lbl.Font = new Font("Arial", 8);
                lbl.AutoEllipsis = true;

                panel.Controls.Add(icon);
                panel.Controls.Add(lbl);
                flow_Files.Controls.Add(panel);

                icon.Click += (s, e) =>
                {
                    try
                    {
                        if (File.Exists(tep.DuongDan))
                        {
                            System.Diagnostics.Process.Start(tep.DuongDan);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tệp: " + tep.TenTep);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể mở tệp: " + ex.Message);
                    }
                };

            }

        }

        private string ShortenFileName(string fileName, int maxLength)
        {
            if (fileName.Length <= maxLength)
                return fileName;
            else
                return fileName.Substring(0, maxLength) + "...";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void progress_Bar_Paint(object sender, PaintEventArgs e)
        {
            float percent = ((float)progress_Bar.Value / progress_Bar.Maximum) * 100;
            string text = percent.ToString("0") + "%";
            using (Font font = new Font("Arial", 10))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                SizeF len = e.Graphics.MeasureString(text, font);
                e.Graphics.DrawString(text, font, brush,
                    new PointF(progress_Bar.Width / 2 - len.Width / 2, progress_Bar.Height / 2 - len.Height / 2));
            }
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            if (tt == "Chưa xử lí")
            {
                if (tcpClientDAO.UpdateTrangThaiCongViec(ctcv))
                {
                    MessageBox.Show("Bạn đã nhận việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Close();
                    loadData();
                    tdg.LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tt == "Đang xử lí" || tt == "Trễ")
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn đánh dấu công việc này là 'Hoàn thành' không?",
                    "Xác nhận hoàn thành",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if (tcpClientDAO.UpdateTrangThaiCongViec(ctcv))
                    {
                        MessageBox.Show("Công việc đã được đánh dấu là hoàn thành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.Close();
                        loadData();
                        tdg.LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Trạng thái công việc không hợp lệ để cập nhật.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void progress_Bar_MouseHover(object sender, EventArgs e)
        {
            //
        }

        private void progress_Bar_MouseMove(object sender, MouseEventArgs e)
        {
            if (tt == "Đang xử lí")
            {
                Point mousePosition = progress_Bar.PointToClient(Cursor.Position);
                    int mouseX = mousePosition.X;
                    int width = progress_Bar.Width;

                    tienDoReport = (int)((mouseX / (float)width) * 100);

                    tip.SetToolTip(progress_Bar, tienDoReport + "%");
            }
        }

        private void progress_Bar_Click(object sender, EventArgs e)
        {
            if (tt == "Đang xử lí")
            {
                if (tienDoReport == 100)
                {
                    DialogResult result = MessageBox.Show(
                        "Bạn có chắc chắn tiến độ là 100% không?",
                        "Xác nhận hoàn thành",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        if (tcpClientDAO.UpdateTrangThaiCongViec(ctcv))
                        {
                            MessageBox.Show("Công việc đã được đánh dấu là hoàn thành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //this.Close();
                            loadData();
                            tdg.LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    ctcv.TienDo = tienDoReport;
                    tcpClientDAO.UpdateTienDoCongViec(ctcv);
                    MessageBox.Show("Báo cáo tiến độ công việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Close();
                    loadData();
                    tdg.LoadData();
                }
            }
                
        }

        private void btnOpenClose_Click(object sender, EventArgs e)
        {
            isExpanding = !isPopupOpen;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (isExpanding)
            {
                popupContainerControl.Width += animationStep;
                popupContainerControl.Left = this.ClientSize.Width - popupContainerControl.Width;

                if (popupContainerControl.Width >= originalPopupWidth)
                {
                    popupContainerControl.Width = originalPopupWidth;
                    popupContainerControl.Left = this.ClientSize.Width - popupContainerControl.Width;
                    animationTimer.Stop();
                    isPopupOpen = true;
                    flow_FeedBack.Visible = true;
                    panel_FB_Bottom.Visible = true;
                    popupContainerControl.BackColor = Color.White;
                }
            }
            else
            {
                popupContainerControl.Width -= animationStep;
                popupContainerControl.Left = this.ClientSize.Width - popupContainerControl.Width;

                if (popupContainerControl.Width <= collapsedWidth)
                {
                    popupContainerControl.Width = collapsedWidth;
                    popupContainerControl.Left = this.ClientSize.Width - popupContainerControl.Width;
                    animationTimer.Stop();
                    isPopupOpen = false;
                    flow_FeedBack.Visible = false;
                    panel_FB_Bottom.Visible = false;
                    popupContainerControl.BackColor = Color.Transparent;
                }
            }
        }

        private void AddFeedback(PhanHoiCongViec fb, bool isMe)
        {
            Guna2Panel feedbackPanel = new Guna2Panel
            {
                Width = flow_FeedBack.ClientSize.Width - 40,
                Padding = new Padding(5),
                Margin = new Padding(5),
                BorderRadius = 0,
                FillColor = Color.Transparent,
                BorderThickness = 0
            };

            // Người gửi
            Label lblNguoiGui = new Label
            {
                Text = fb.NguoiDung.HoTen + " - " + fb.ThoiGian,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.White,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Nội dung
            Control contentControl;
            if (fb.Loai == "Feedback")
            {
                Label lblNoiDung = new Label
                {
                    Text = fb.NoiDung,
                    Font = new Font("Segoe UI", 10),
                    MaximumSize = new Size(feedbackPanel.Width - 60, 0),
                    AutoSize = true,
                    ForeColor = Color.White,
                    Padding = new Padding(8),
                    BackColor = Color.Transparent
                };
                contentControl = lblNoiDung;
            }
            else if (fb.Loai == "Attach")
            {
                int maTep = int.Parse(fb.NoiDung);
                TepTin tep = tcpClientDAO.getTepbyId(maTep);
                string extension = Path.GetExtension(tep.TenTepGoc).ToLower();

                // Chọn icon tương ứng
                Image iconImage = Properties.Resources.icons8_document_48;
                if (extension == ".pdf")
                    iconImage = Properties.Resources.pdf_icon;
                else if (extension == ".doc" || extension == ".docx")
                    iconImage = Properties.Resources.word_icon;
                else if (extension == ".xls" || extension == ".xlsx")
                    iconImage = Properties.Resources.excel_icon;
                else if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp" || extension == ".gif")
                    iconImage = Properties.Resources.image_icon;

                // PictureBox cho icon
                PictureBox icon = new PictureBox
                {
                    Image = iconImage,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(48, 48),
                    Margin = new Padding(0, 2, 5, 0)
                };

                // LinkLabel cho tên tệp
                LinkLabel link = new LinkLabel
                {
                    Text = tep.TenTepGoc,
                    Tag = tep.DuongDan,
                    LinkColor = Color.White,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10, FontStyle.Underline),
                    Margin = new Padding(0, 5, 0, 0)
                };

                link.Click += (s, e) =>
                {
                    try
                    {
                        System.Diagnostics.Process.Start(tep.DuongDan);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể mở tệp: " + ex.Message);
                    }
                };

                // Panel xếp ngang icon và link
                FlowLayoutPanel filePanel = new FlowLayoutPanel
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    Margin = new Padding(0),
                    Padding = new Padding(0),
                    BackColor = Color.Transparent
                };

                filePanel.Controls.Add(icon);
                filePanel.Controls.Add(link);

                contentControl = filePanel;
            }

            else
            {
                // Nếu loại không xác định
                contentControl = new Label
                {
                    Text = "[Không xác định loại phản hồi]",
                    ForeColor = Color.Gray
                };
            }

            // Nội dung panel
            Guna2Panel innerPanel = new Guna2Panel
            {
                AutoSize = true,
                BorderRadius = 10,
                FillColor = isMe ? Color.FromArgb(87, 166, 255) : Color.FromArgb(65, 200, 94),
                BorderThickness = 0,
                Margin = new Padding(0),
                Padding = new Padding(5)
            };

            lblNguoiGui.Location = new Point(5, 5);
            contentControl.Location = new Point(5, lblNguoiGui.Bottom + 2);

            innerPanel.Controls.Add(lblNguoiGui);
            innerPanel.Controls.Add(contentControl);

            FlowLayoutPanel container = new FlowLayoutPanel
            {
                Width = feedbackPanel.Width,
                AutoSize = true,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(0),
                FlowDirection = isMe ? FlowDirection.RightToLeft : FlowDirection.LeftToRight
            };

            container.Controls.Add(innerPanel);
            feedbackPanel.Controls.Add(container);
            flow_FeedBack.Controls.Add(feedbackPanel);

            flow_FeedBack.ScrollControlIntoView(feedbackPanel);
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            string mess = txtMessage.Text;

            CongViec cv = tcpClientDAO.getCongViecById(maCongViec);
            NguoiDung nd = tcpClientDAO.GetNguoiDung(maNguoiDung);

            string targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Attachments");
            Directory.CreateDirectory(targetFolder);

            if (lstTepDaChon.Count > 0)
            {
                string zipPath = ZipFiles(lstTepDaChon, maCongViec.ToString());

                SendFileToServer(zipPath);

                foreach (TepTin t in lstTepDaChon)
                {
                    string fileName = Path.GetFileName(t.DuongDan);
                    string extension = Path.GetExtension(fileName);

                    // Tạo tên mới duy nhất
                    string timePart = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string randomPart = Guid.NewGuid().ToString("N").Substring(0, 6);
                    string newFileName = $"{maCongViec}_{timePart}_{randomPart}{extension}";

                    string destPath = Path.Combine(targetFolder, newFileName);
                    File.Copy(t.DuongDan, destPath, true);

                    t.DuongDan = destPath;
                    t.TenTep = newFileName;
                    t.TenTepGoc = fileName;

                    int maTep = tcpClientDAO.CreateTepTin(t);
                    if (maTep > 0)
                    {
                        t.MaTep = maTep;
                        Console.WriteLine("Tạo Tệp PhanHoi Thành Công!");
                    }
                    else
                    {
                        Console.WriteLine("Tạo Tệp PhanHoi Thất Bại");
                    }

                    PhanHoiCongViec phcv = new PhanHoiCongViec
                    {
                        MaCongViec = maCongViec,
                        CongViec = cv,
                        MaNguoiDung = maNguoiDung,
                        NguoiDung = nd,
                        NoiDung = t.MaTep.ToString(),
                        ThoiGian = DateTime.Now,
                        Loai = "Attach"
                    };

                    tcpClientDAO.createPhanHoiCongViec(phcv);
                }
            }

            if (!string.IsNullOrWhiteSpace(mess))
            {
                PhanHoiCongViec phcv = new PhanHoiCongViec
                {
                    MaCongViec = maCongViec,
                    CongViec = cv,
                    MaNguoiDung = maNguoiDung,
                    NguoiDung = nd,
                    NoiDung = mess,
                    ThoiGian = DateTime.Now,
                    Loai = "Feedback"
                };

                tcpClientDAO.createPhanHoiCongViec(phcv);
            }

            var lstLienQuanCongViec = tcpClientDAO.GetListNguoiLienQuanByIdCongViec(maCongViec);
            var nguoiNhanThongBao = new HashSet<int> { cv.NguoiGiao, maNguoiDung };

            foreach (var nlq in lstLienQuanCongViec)
                nguoiNhanThongBao.Add(nlq.MaNguoiDung);

            foreach (int maNguoiNhan in nguoiNhanThongBao)
            {
                if (maNguoiNhan == maNguoiDung)
                    continue;

                ThongBaoNguoiDung tb = new ThongBaoNguoiDung
                {
                    MaChiTietCV = maChiTietCV,
                    MaNguoiDung = maNguoiNhan,
                    NoiDung = "Có phản hồi công việc: " + ctcv.TieuDe,
                    TrangThai = 0,
                    NgayThongBao = DateTime.Now,
                    ChiTietCongViec = ctcv,
                    NguoiDung = nd
                };

                tcpClientDAO.CreateThongBao(tb);
            }

            lstTepDaChon.Clear();
            loadData();

            txtMessage.Text = "";

        }

        private void btnAddFile_Click(object sender, EventArgs e)
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

                HienThiDanhSachFile2();
            }
        }

        private void HienThiDanhSachFile2()
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
                icon.Width = 38;
                icon.Height = 38;
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
                    HienThiDanhSachFile2();
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

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            TepTin tep = btn.Tag as TepTin;

            if (tep != null)
            {
                lstTepDaChon.Remove(tep);
                HienThiDanhSachFile2();
            }
        }

        private string ZipFiles(List<TepTin> files, string maCongViec)
        {
            string zipPath = Path.Combine(Path.GetTempPath(), $"cv_{maCongViec}_{DateTime.Now.Ticks}.zip");
            using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    zip.CreateEntryFromFile(file.DuongDan, Path.GetFileName(file.DuongDan));
                }
            }
            return zipPath;
        }

        void SendFileToServer(string zipFilePath)
        {
            try
            {
                if (!File.Exists(zipFilePath))
                {
                    Console.WriteLine("File không tồn tại.");
                    return;
                }

                string fileName = Path.GetFileName(zipFilePath);
                byte[] fileBytes = File.ReadAllBytes(zipFilePath);
                int fileSize = fileBytes.Length;

                using (TcpClient client = new TcpClient("192.168.1.3", 5000))
                using (NetworkStream stream = client.GetStream())
                {
                    // Tạo gói tin JSON metadata
                    var metadata = new
                    {
                        Command = "uploadfile",
                        Data = new
                        {
                            FileName = fileName,
                            FileSize = fileSize
                        }
                    };

                    string json = JsonConvert.SerializeObject(metadata);
                    byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                    byte[] jsonLength = BitConverter.GetBytes(jsonBytes.Length);

                    // Gửi độ dài + nội dung JSON
                    stream.Write(jsonLength, 0, 4);
                    stream.Write(jsonBytes, 0, jsonBytes.Length);

                    // Gửi nội dung file nhị phân
                    stream.Write(fileBytes, 0, fileBytes.Length);

                    // Nhận phản hồi từ server
                    byte[] replyLengthBuffer = new byte[4];
                    stream.Read(replyLengthBuffer, 0, 4);
                    int replyLength = BitConverter.ToInt32(replyLengthBuffer, 0);

                    byte[] replyBuffer = new byte[replyLength];
                    stream.Read(replyBuffer, 0, replyLength);
                    string reply = Encoding.UTF8.GetString(replyBuffer);

                    Console.WriteLine("Phản hồi từ server: " + reply);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gửi file: " + ex.Message);
            }
        }



    }
}
