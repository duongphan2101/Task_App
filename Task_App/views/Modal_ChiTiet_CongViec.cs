using Guna.UI2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_App.DTO;
using Task_App.Model;
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

        private ApiClientDAO apiClientDAO;

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
        private ChiTietCongViecFullDto ctcvdto;

        private List<PhanHoiCongViec> lstFeedback = new List<PhanHoiCongViec>();
        private NguoiDung nd;
        public Modal_ChiTiet_CongViec(NguoiDung nd ,string maCongViec, int maChiTietCV, int maNguoiDung, bool task, ApiClientDAO apiClientDAO, Task_Duoc_Giao_Control tdg)
        {
            InitializeComponent();
            this.nd = nd;
            this.maCongViec = maCongViec;
            this.maNguoiDung = maNguoiDung;
            this.maChiTietCV = maChiTietCV;
            this.task = task;
            this.apiClientDAO = apiClientDAO;
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

        private async Task loadData()
        {
            var chiTietRes = await apiClientDAO.GetChiTietConViecAsync(maChiTietCV);
            var chiTietdeRes = await apiClientDAO.GetChiTietConViec(maChiTietCV);
             ctcv = chiTietdeRes.Data;

            var ChiTiet = chiTietRes.Data;
            ctcvdto = ChiTiet;

            var nlqRes = await apiClientDAO.GetNguoiLienQuanCongViecAsync(maCongViec);

            var nlq = nlqRes.Data;

            List<NguoiLienQuanDTO> lstCc = new List<NguoiLienQuanDTO>();
            List<NguoiLienQuanDTO> lstBcc = new List<NguoiLienQuanDTO>();
            List<NguoiLienQuanDTO> lstTo = new List<NguoiLienQuanDTO>();

            var resPhanHoiCongViec = await apiClientDAO.GetPhanHoiCongViecAsync(maCongViec);
            lstFeedback = resPhanHoiCongViec.PhanHoiCongViec;

            flow_FeedBack.Controls.Clear();
            renderedFeedbackIds.Clear();

            var seen = new HashSet<int>();
            foreach (var fb in resPhanHoiCongViec.PhanHoiCongViec.OrderBy(f => f.ThoiGian))
            {
                if (!seen.Add(fb.MaPhanHoi)) continue;
                bool isMe = fb.NguoiDung.HoTen == nd.HoTen;
                await AddFeedback(fb, isMe);
            }

            HienThiDanhSachFile();

            //DaGiao
            if (task)
            {

                    lblTieuDe.Text = ChiTiet.TieuDe.ToString();
                    txtNoiDung.Text = ChiTiet.NoiDung.ToString();
                    //lblThoiGianHoanThanh.Text = Convert.ToDateTime(row["hanHoanThanh"]).ToString("dd/MM/yyyy HH:mm");
                    lblThoiGianHoanThanh.Text = "Thời gian hoàn thành: Chưa có";

                    progress_Bar.Value = Convert.ToInt32(ChiTiet.TienDo);
                    progress_Bar.ShowPercentage = true;
                    lblNgayBatDau.Text = Convert.ToDateTime(ChiTiet.NgayNhanCongViec).ToString("dd/MM/yyyy");
                    lblHanHoanThanh.Text = " - " + Convert.ToDateTime(ChiTiet.NgayKetThucCongViec).ToString("dd/MM/yyyy");
                    int trangThai = Convert.ToInt32(ChiTiet.TrangThai);
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

                    int mucDo = Convert.ToInt32(ChiTiet.MucDoUuTien);
                    string md = "";

                    if (mucDo == 0)
                    {
                        md = "Bình thường";
                        lblMucDo.ForeColor = Color.Green;
                        panel_MucDo.BorderColor = Color.Green;
                    }
                    else if (mucDo == 1)
                    {
                        md = "Quan trọng";
                        lblMucDo.ForeColor = Color.Orange;
                        panel_MucDo.BorderColor = Color.Orange;
                    }
                    else if (mucDo == 2)
                    {
                        md = "Khẩn cấp";
                        lblMucDo.ForeColor = Color.Red;
                        panel_MucDo.BorderColor = Color.Red;
                    }

                lblMucDo.Text = md;

                List<NguoiLienQuanDTO> lst = nlq;
                if (lst.Count > 0)
                {
                    foreach (NguoiLienQuanDTO nlqcv in lst)
                    {
                        if (nlqcv.vaiTro == "to")
                        {
                            lstTo.Add(nlqcv);
                        }
                        if (nlqcv.vaiTro == "cc")
                        {
                            lstCc.Add(nlqcv);
                        }
                        if (nlqcv.vaiTro == "bcc")
                        {
                            lstBcc.Add(nlqcv);
                        }
                    }
                    lblCc.Text = "CC: " + string.Join(", ", lstCc.Select(x => x.hoTen));
                    lblBc.Text = "BCC: " + string.Join(", ", lstBcc.Select(x => x.hoTen));
                    lblNguoi.Text = "To: " + string.Join(", ", lstTo.Select(x => x.hoTen));

                }

                btnHoanThanh.Visible = false;
            }
            //DuocGiao
            else
            {

                    lblTieuDe.Text = ChiTiet.TieuDe.ToString();
                    txtNoiDung.Text = ChiTiet.NoiDung.ToString();

                    lblThoiGianHoanThanh.Text = "Thời gian hoàn thành: Chưa có";

                    progress_Bar.Value = Convert.ToInt32(ChiTiet.TienDo);
                    progress_Bar.ShowPercentage = true;
                    lblNgayBatDau.Text = Convert.ToDateTime(ChiTiet.NgayNhanCongViec).ToString("dd/MM/yyyy");
                    lblHanHoanThanh.Text = " - " + Convert.ToDateTime(ChiTiet.NgayKetThucCongViec).ToString("dd/MM/yyyy");
                    lblNguoi.Text = "From: " + ChiTiet.NguoiGiao_HoTen.ToString();

                    btnHoanThanh.Enabled = false;
                    btnHoanThanh.BackColor = Color.Transparent;
                    int trangThai = Convert.ToInt32(ChiTiet.TrangThai);

                if (trangThai == 0)
                {
                    tt = "Chưa xử lí";
                    btnHoanThanh.FillColor = Color.LightSkyBlue;
                    btnHoanThanh.Enabled = true;
                    btnHoanThanh.Text = "Nhận việc";
                    ctcv.MaChiTietCV = maChiTietCV;
                    ctcv.MaCongViec = ChiTiet.MaCongViec;
                    ctcv.TrangThai = 1;
                    ctcv.TienDo = 0;
                    ctcv.NgayHoanThanh = DateTime.Now;
                    ctcv.NgayNhanCongViec = ChiTiet.NgayNhanCongViec;
                    ctcv.SoNgayHoanThanh = Convert.ToInt32(ChiTiet.SoNgayHoanThanh);
                    ctcv.NoiDung = ChiTiet.NoiDung;
                    ctcv.TieuDe = ChiTiet.TieuDe;
                    ctcv.MucDoUuTien = Convert.ToInt32(ChiTiet.MucDoUuTien);
                    
                }
                else if (trangThai == 1)
                {
                    tt = "Đang xử lí";
                    btnHoanThanh.FillColor = Color.LightGreen;
                    btnHoanThanh.Enabled = true;
                    btnHoanThanh.Text = "Hoàn thành";
                    //ctcv = tcpClientDAO.getChiTietCongViecById(maChiTietCV);

                    ctcv.MaChiTietCV = maChiTietCV;
                    ctcv.MaCongViec = ChiTiet.MaCongViec;
                    ctcv.TrangThai = 2;
                    ctcv.TienDo = 100;
                    ctcv.NgayHoanThanh = DateTime.Now;
                    ctcv.NgayNhanCongViec = ChiTiet.NgayNhanCongViec;
                    ctcv.SoNgayHoanThanh = Convert.ToInt32(ChiTiet.SoNgayHoanThanh);
                    ctcv.NoiDung = ChiTiet.NoiDung;
                    ctcv.TieuDe = ChiTiet.TieuDe;
                    ctcv.MucDoUuTien = Convert.ToInt32(ChiTiet.MucDoUuTien);
                    ctcv.NgayKetThucCongViec = ChiTiet.NgayKetThucCongViec;
                }
                else if (trangThai == 2)
                {
                    tt = "Hoàn thành";
                    lblThoiGianHoanThanh.Text = "Hoàn thành lúc: " + Convert.ToDateTime(ChiTiet.NgayHoanThanh).ToString("dd/MM/yyyy HH:mm");
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

                    int mucDo = Convert.ToInt32(ChiTiet.MucDoUuTien);
                    string md = "";

                    if (mucDo == 0)
                    {
                        md = "Bình thường";
                        lblMucDo.ForeColor = Color.Green;
                        panel_MucDo.BorderColor = Color.Green;
                    }
                    else if (mucDo == 1)
                    {
                        md = "Quan trọng";
                        lblMucDo.ForeColor = Color.Orange;
                        panel_MucDo.BorderColor = Color.Orange;
                    }
                    else if (mucDo == 2)
                    {
                        md = "Khẩn cấp";
                        lblMucDo.ForeColor = Color.Red;
                        panel_MucDo.BorderColor = Color.Red;
                    }

                    lblMucDo.Text = md;

                List<NguoiLienQuanDTO> lst = nlq;
                if (lst.Count > 0)
                {
                    foreach (NguoiLienQuanDTO nlqcv in lst)
                    {
                        if (nlqcv.vaiTro == "to")
                        {
                            lstTo.Add(nlqcv);
                        }
                        if (nlqcv.vaiTro == "cc")
                        {
                            lstCc.Add(nlqcv);
                        }
                        if (nlqcv.vaiTro == "bcc")
                        {
                            lstBcc.Add(nlqcv);
                        }
                    }
                    lblCc.Text = "CC: " + string.Join(", ", lstCc.Select(x => x.hoTen));
                    lblBc.Text = "BCC: " + string.Join(", ", lstBcc.Select(x => x.hoTen));
                    //lblNguoi.Text = "To: " + string.Join(", ", lstTo.Select(x => x.hoTen));
                }

            }
        }

        private async Task HienThiDanhSachFile()
        {
            List<TepDinhKemEmail> lstTep = new List<TepDinhKemEmail> ();
            var res = await apiClientDAO.GetTepDinhKemEmailByMaCongViecAsync(maCongViec);
            lstTep = res?.Data ?? new List<TepDinhKemEmail>();

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

        private async void btnHoanThanh_Click(object sender, EventArgs e)
        {

            if (tt == "Chưa xử lí")
            {
                var res = await apiClientDAO.UpdateTrangThaiCongViecAsync(ctcv);
                if (res.Success)
                {
                    MessageBox.Show("Bạn đã nhận việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Close();
                    loadData();
                    tdg.LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Loi " + res.Message);
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
                    var res = await apiClientDAO.UpdateTrangThaiCongViecAsync(ctcv);
                    if (res.Success)
                    {
                        MessageBox.Show("Công việc đã được đánh dấu là hoàn thành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.Close();
                        loadData();
                        tdg.LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine(res.Message);
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

        private async void progress_Bar_Click(object sender, EventArgs e)
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
                        var res = await apiClientDAO.UpdateTrangThaiCongViecAsync(ctcv);
                        if (res.Success)
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
                    ctcv.TrangThai = 1;
                    var res = await apiClientDAO.UpdateTrangThaiCongViecAsync(ctcv);
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
        
        private HashSet<int> renderedFeedbackIds = new HashSet<int>();
        private async Task AddFeedback(PhanHoiCongViec fb, bool isMe)
        {
            if (flow_FeedBack.Controls.OfType<Guna2Panel>().Any(p => p.Tag != null && p.Tag.ToString() == fb.MaPhanHoi.ToString()))
                return;

            if (!renderedFeedbackIds.Add(fb.MaPhanHoi)) return;

            Guna2Panel feedbackPanel = new Guna2Panel
            {
                Width = flow_FeedBack.ClientSize.Width - 40,
                Padding = new Padding(5),
                Margin = new Padding(5),
                BorderRadius = 0,
                FillColor = Color.Transparent,
                BorderThickness = 0,
                Tag = fb.MaPhanHoi
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
                var res = await apiClientDAO.GetTepTinByAsync(maTep);
                TepTin tep = res.TepTin;
                if (tep == null)
                {
                    Console.WriteLine("Lỗi: " + res.Message);
                    return;
                }

                string extension = Path.GetExtension(tep.TenTepGoc).ToLower();
                Image iconImage = Properties.Resources.icons8_document_48;
                if (extension == ".pdf") iconImage = Properties.Resources.pdf_icon;
                else if (extension == ".doc" || extension == ".docx") iconImage = Properties.Resources.word_icon;
                else if (extension == ".xls" || extension == ".xlsx") iconImage = Properties.Resources.excel_icon;
                else if (".jpg.jpeg.png.bmp.gif".Contains(extension)) iconImage = Properties.Resources.image_icon;

                PictureBox icon = new PictureBox
                {
                    Image = iconImage,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(48, 48),
                    Margin = new Padding(0, 2, 5, 0)
                };

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
                    try { System.Diagnostics.Process.Start(tep.DuongDan); }
                    catch (Exception ex) { MessageBox.Show("Không thể mở tệp: " + ex.Message); }
                };

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
                contentControl = new Label
                {
                    Text = "[Không xác định loại phản hồi]",
                    ForeColor = Color.Gray
                };
            }

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

        private async void btn_Send_Click(object sender, EventArgs e)
        {
            string mess = txtMessage.Text;
            string path = Path.Combine(Application.StartupPath, "tmpCredential.dll");
            int savedUserId = 0;

            if (File.Exists(path))
            {
                byte[] readData = File.ReadAllBytes(path);
                using (var ms = new MemoryStream(readData))
                {
                    var bf = new BinaryFormatter();
                    var loaded = (UserCredential)bf.Deserialize(ms);

                    DTO.TmpPass.Pwd = loaded.GetPassword();
                    savedUserId = loaded.UserId;
                }
            }
            string pwd = DTO.TmpPass.Pwd;
            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu của email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RealPass rp = new RealPass(maNguoiDung);
                rp.ShowDialog();
                return;
            }


            var resChiTiet = await apiClientDAO.GetChiTietConViec(maChiTietCV);
            CongViec cv = resChiTiet.Data.CongViec;
            NguoiDung ndx = nd;

            // Nếu có file đính kèm
            if (lstTepDaChon.Count > 0)
            {
                foreach (TepTin t in lstTepDaChon)
                {
                    // Gửi file trực tiếp lên BE
                    var resUpload = await apiClientDAO.UploadFile(t.DuongDan, maCongViec.ToString());
                    if (resUpload.Success)
                    {
                        TepTin tepDaTao = resUpload.Data;
                        Console.WriteLine("Upload thành công: " + tepDaTao.TenTep);

                        // Tạo phản hồi gắn kèm file
                        PhanHoiCongViec phcv = new PhanHoiCongViec
                        {
                            MaCongViec = maCongViec,
                            CongViec = cv,
                            MaNguoiDung = maNguoiDung,
                            NguoiDung = nd,
                            NoiDung = tepDaTao.MaTep.ToString(),
                            ThoiGian = DateTime.Now,
                            Loai = "Attach"
                        };

                        await apiClientDAO.CreatePhanHoiCongViec(phcv);
                    }
                    else
                    {
                        Console.WriteLine("Upload thất bại: " + resUpload.Message);
                    }
                }
            }

            // Nếu có text feedback
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

                await apiClientDAO.CreatePhanHoiCongViec(phcv);
            }

            // Sau khi tạo PhanHoiCongViec
            if (!string.IsNullOrWhiteSpace(mess) || lstTepDaChon.Count > 0)
            {
                // Lấy email gốc của công việc
                var resEmail = await apiClientDAO.GetEmailByChiTietCVAsync(maChiTietCV);
                var emailGoc = resEmail?.Data?.FirstOrDefault();

                // Tạo MessageId mới cho reply
                string newMessageId = $"{Guid.NewGuid()}@intimexhcm.com";

                Email emailReply = new Email
                {
                    MaChiTietCV = maChiTietCV,
                    MaEmail = GenerateMaEmailFromLast("ReplyTo_" + emailGoc.MaEmail),
                    NguoiGui = maNguoiDung,
                    NoiDung = mess,
                    TieuDe = "Reply_" + emailGoc.TieuDe,
                    NgayGui = DateTime.Now,
                    MessageId = newMessageId,
                    InReplyTo = emailGoc?.MessageId,
                    References = emailGoc != null
                        ? (string.IsNullOrEmpty(emailGoc.References)
                            ? emailGoc.MessageId
                            : emailGoc.References + " " + emailGoc.MessageId)
                        : null
                };

                var resEmailReply = await apiClientDAO.CreateEmail(emailReply);
                if (resEmailReply.Success)
                {
                    Console.WriteLine("TẠO Email Reply Thành Công");

                    // Lấy danh sách người nhận từ email gốc
                    var resNguoiNhan = await apiClientDAO.GetNguoiNhanEmailByMaEmailAsync(emailGoc.MaEmail);
                    var lstNguoiNhanGoc = resNguoiNhan?.Data ?? new List<NguoiNhanEmail>();

                    // Build danh sách người nhận cho email reply
                    var lstNguoiNhanReply = new List<NguoiNhanEmail>();

                    if (emailGoc.NguoiGui == maNguoiDung)
                    {
                        // Case 1: Người login là NGƯỜI GỬI email gốc
                        // → reply tới tất cả người nhận của email gốc
                        foreach (var nnx in lstNguoiNhanGoc)
                        {
                            var nnReply = new NguoiNhanEmail
                            {
                                MaEmail = emailReply.MaEmail,
                                MaNguoiDung = nnx.MaNguoiDung,
                                VaiTro = nnx.VaiTro,
                                Email = nnx.Email,
                                NguoiDung = nnx.NguoiDung
                            };
                            await apiClientDAO.CreateNguoiNhanEmail(nnReply);
                            lstNguoiNhanReply.Add(nnReply);
                        }
                    }
                    else
                    {
                        // Case 2: Người login là NGƯỜI NHẬN email gốc
                        // → reply lại cho NGƯỜI GỬI email gốc
                        {
                            // Lấy thông tin người gửi gốc từ API (đảm bảo có email)
                            var resNguoiGui = await apiClientDAO.GetGetNguoiDungByIdAsync(emailGoc.NguoiGui);
                            var nguoiGui = resNguoiGui?.Data;

                            if (nguoiGui == null || string.IsNullOrWhiteSpace(nguoiGui.Email))
                            {
                                Console.WriteLine($"[ERROR] Không tìm thấy email của người gửi gốc (MaNguoiGui={emailGoc.NguoiGui}). Bỏ qua gửi mail.");
                            }
                            else
                            {
                                var nnReply = new NguoiNhanEmail
                                {
                                    MaEmail = emailReply.MaEmail,
                                    MaNguoiDung = emailGoc.NguoiGui,
                                    VaiTro = "to",
                                    Email = emailReply,
                                    NguoiDung = nguoiGui
                                };

                                var createRes = await apiClientDAO.CreateNguoiNhanEmail(nnReply);

                                // Log kỹ để debug — cần using Newtonsoft.Json;
                                //Console.WriteLine("== CreateNguoiNhanEmail result ==");
                                //Console.WriteLine(JsonConvert.SerializeObject(createRes, Formatting.Indented));
                                //Console.WriteLine("== nnReply object ==");
                                //Console.WriteLine(JsonConvert.SerializeObject(nnReply, Formatting.Indented));

                                lstNguoiNhanReply.Add(nnReply);
                            }
                        }

                    }

                    // Gọi hàm gửi email reply
                    var replyRequest = new ReplyEmailRequest
                    {
                        Email = emailReply,
                        DanhSachNguoiNhanEmail = lstNguoiNhanReply,
                        DanhSachTepDinhKem = lstTepDaChon.Select(t => new TepDinhKemEmail
                        {
                            MaEmail = emailReply.MaEmail,
                            MaTep = t.MaTep,
                            TepTin = t,
                            Email = emailReply
                        }).ToList(),
                        CurrentUser = nd,
                        TaskId = maChiTietCV,
                        MK = pwd,
                        InReplyTo = emailReply.InReplyTo,
                        References = emailReply.References
                    };

                    var resSend = await apiClientDAO.ReplyEmailAsync(replyRequest);
                    if (resSend.Success)
                    {
                        Console.WriteLine("GỬI Email Reply Thành Công!");
                    }
                    else
                    {
                        Console.WriteLine("GỬI Email Reply Thất Bại: " + resSend.Message);
                    }
                }
                else
                {
                    Console.WriteLine("LỖI khi tạo Email Reply: " + resEmailReply.Message);
                }
            }

            // Gửi thông báo cho người liên quan
            var resListNLQ = await apiClientDAO.GetNguoiLienQuanCongViecAsync(maCongViec);
            var lstLienQuanCongViec = resListNLQ.Data;
            var nguoiNhanThongBao = new HashSet<int> { cv.NguoiGiao, maNguoiDung };

            foreach (var nlq in lstLienQuanCongViec)
                nguoiNhanThongBao.Add(nlq.maNguoiDung);

            foreach (int maNguoiNhan in nguoiNhanThongBao)
            {
                if (maNguoiNhan == maNguoiDung) continue;

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

                await apiClientDAO.CreateThongBao(tb);
            }

            lstTepDaChon.Clear();
            HienThiDanhSachFile2();
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

        public string GenerateMaEmailFromLast(string maCongViec)
        {
            string timePart = DateTime.Now.ToString("yyyyMMddHHmmss");
            string randomPart = Guid.NewGuid().ToString("N").Substring(0, 6);
            return $"{maCongViec}_{timePart}_{randomPart}";
        }



    }
}
