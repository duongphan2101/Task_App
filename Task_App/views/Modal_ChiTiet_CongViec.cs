using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        private TcpClientDAO tcpClientDAO;

        private string tt = "";
        private ChiTietCongViec ctcv = new ChiTietCongViec();
        private int tienDoReport = 0;
        ToolTip tip = new ToolTip();

        private Timer animationTimer;
        private bool isPopupOpen = true;
        private int animationStep = 90;
        private int collapsedWidth = 30;
        private int originalPopupWidth;
        private bool isExpanding = false;

        private List<PhanHoiCongViec> lstFeedback = new List<PhanHoiCongViec>();

        public Modal_ChiTiet_CongViec(string maCongViec, int maChiTietCV, int maNguoiDung, bool task, TcpClientDAO tcpClientDAO)
        {
            InitializeComponent();
            this.maCongViec = maCongViec;
            this.maNguoiDung = maNguoiDung;
            this.maChiTietCV = maChiTietCV;
            this.task = task;
            this.tcpClientDAO = tcpClientDAO;

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

                    if(trangThai == 0)
                    {
                        tt = "Chưa xử lí";
                    }else if(trangThai == 1)
                    {
                        tt = "Đang xử lí";
                    }else if (trangThai == 2)
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
                    lblCc.Text = "CC: " + string.Join(", ", lstCc.Select(x=>x.NguoiDung.HoTen));
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
                        lblThoiGianHoanThanh.Text = "Hoàn thành lúc: "+ Convert.ToDateTime(row["ngayHoanThanh"]).ToString("dd/MM/yyyy HH:mm");
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
            // Panel tổng chứa feedback
            Guna2Panel feedbackPanel = new Guna2Panel();
            feedbackPanel.Width = flow_FeedBack.ClientSize.Width - 40;
            feedbackPanel.Padding = new Padding(5);
            feedbackPanel.Margin = new Padding(5);
            feedbackPanel.BorderRadius = 0;
            feedbackPanel.FillColor = Color.Transparent;
            feedbackPanel.BorderThickness = 0;

            // Label người gửi
            Label lblNguoiGui = new Label();
            lblNguoiGui.Text = fb.NguoiDung.HoTen + " - " + fb.ThoiGian;
            lblNguoiGui.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            lblNguoiGui.ForeColor = Color.White;
            lblNguoiGui.AutoSize = true;
            lblNguoiGui.BackColor = Color.Transparent;

            // Label nội dung
            Label lblNoiDung = new Label();
            lblNoiDung.Text = fb.NoiDung;
            lblNoiDung.Font = new Font("Segoe UI", 10);
            lblNoiDung.MaximumSize = new Size(feedbackPanel.Width - 60, 0);
            lblNoiDung.AutoSize = true;
            lblNoiDung.ForeColor = Color.White;
            lblNoiDung.Padding = new Padding(8);
            lblNoiDung.BackColor = Color.Transparent;

            // Panel bo góc chứa nội dung
            Guna2Panel innerPanel = new Guna2Panel();
            innerPanel.AutoSize = true;
            innerPanel.BorderRadius = 10;
            innerPanel.FillColor = isMe ? Color.FromArgb(87,166,255) : Color.FromArgb(65,200,94);
            innerPanel.BorderThickness = 0;
            innerPanel.Margin = new Padding(0);
            innerPanel.Padding = new Padding(5);
            innerPanel.Controls.Add(lblNguoiGui);
            innerPanel.Controls.Add(lblNoiDung);

            // Căn vị trí nội dung
            lblNguoiGui.Location = new Point(5, 5);
            lblNoiDung.Location = new Point(5, lblNguoiGui.Bottom + 2);

            // Container căn trái/phải
            FlowLayoutPanel container = new FlowLayoutPanel();
            container.Width = feedbackPanel.Width;
            container.AutoSize = true;
            container.WrapContents = false;
            container.Margin = new Padding(0);
            container.Padding = new Padding(0);
            container.FlowDirection = isMe ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            container.Controls.Add(innerPanel);
            feedbackPanel.Controls.Add(container);
            flow_FeedBack.Controls.Add(feedbackPanel);

            // Auto scroll
            flow_FeedBack.ScrollControlIntoView(feedbackPanel);
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            string mess = txtMessage.Text;
            //Console.WriteLine("Message: "+mess);

            CongViec cv = tcpClientDAO.getCongViecById(maCongViec);
            NguoiDung nd = tcpClientDAO.GetNguoiDung(maNguoiDung);

            PhanHoiCongViec phcv = new PhanHoiCongViec();
            phcv.MaCongViec = maCongViec;
            phcv.CongViec = cv;
            phcv.MaNguoiDung = maNguoiDung;
            phcv.NguoiDung = nd;
            phcv.NoiDung = mess;
            phcv.ThoiGian = DateTime.Now;
            phcv.Loai = "Feedback";

            ChiTietCongViec ctcv = tcpClientDAO.getChiTietCongViecById(maChiTietCV);

            var lstLienQuanCongViec = tcpClientDAO.GetListNguoiLienQuanByIdCongViec(maCongViec);

            var nguoiNhanThongBao = new HashSet<int>();

            nguoiNhanThongBao.Add(cv.NguoiGiao);

            nguoiNhanThongBao.Add(maNguoiDung);

            foreach (var nlq in lstLienQuanCongViec)
            {
                nguoiNhanThongBao.Add(nlq.MaNguoiDung);
            }

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


            if (!tcpClientDAO.createPhanHoiCongViec(phcv))
            {
                MessageBox.Show("Phản hồi thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            loadData();

            txtMessage.Text = "";
        }

    }
}
