using DotNetEnv;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using TcpServerApp.database;
using TcpServerApp.Model;


namespace TcpServerApp.DAO
{
    internal class CongViecDAO
    {
        private readonly connectDB conn = new connectDB();

        public DataTable GetCongViecDaGiaoByUserId(int id)
        {
            string query = @"
                SELECT 
                    cv.maCongViec,
                    cv.ngayGiao,
                    CASE WHEN cv.lapLai = 1 THEN N'Có' ELSE N'Không' END AS lapLai,
                    cv.tanSuat,
                    ct.ngayNhanCongViec,
                    ct.ngayKetThucCongViec,
                    ct.maChiTietCV,
                    ct.tieuDe,
                    ct.noiDung,
                    ct.ngayHoanThanh,                    
                    ct.soNgayHoanThanh,
                    ct.trangThai,
                    ct.tienDo,
                    nguoiNhan.maNguoiDung AS nguoiNhanID,
                    nguoiNhan.hoTen AS nguoiNhan_HoTen,
                    nguoiNhan.email AS nguoiNhan_Email,
                    dv.tenDonVi,
                    pb.tenPhongBan,
                    cvu.tenChucVu
                FROM CongViec cv
                JOIN ChiTietCongViec ct ON cv.maCongViec = ct.maCongViec
                JOIN Email e ON ct.maChiTietCV = e.maChiTietCV
                JOIN NguoiLienQuanCongViec nlcq ON cv.maCongViec = nlcq.maCongViec AND nlcq.vaiTro = 'to'
                JOIN NguoiDung nguoiNhan ON nlcq.maNguoiDung = nguoiNhan.maNguoiDung
                LEFT JOIN DonVi dv ON nguoiNhan.maDonVi = dv.maDonVi
                LEFT JOIN PhongBan pb ON nguoiNhan.maPhongBan = pb.maPhongBan
                LEFT JOIN ChucVu cvu ON nguoiNhan.maChucVu = cvu.maChucVu
                WHERE cv.nguoiGiao = @nguoiGiao AND e.trangThai = 1
                ORDER BY cv.ngayGiao DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@nguoiGiao", id)
            };

            return conn.ExecuteQuery(query, parameters);
        }

        public DataTable GetCongViecDaGiaoByUserId_SortByTrangThai(int id)
        {
            string query = @"
                SELECT 
                    cv.maCongViec,
                    cv.ngayGiao,
                    CASE WHEN cv.lapLai = 1 THEN N'Có' ELSE N'Không' END AS lapLai,
                    cv.tanSuat,
                    ct.ngayNhanCongViec,
                    ct.ngayKetThucCongViec,
                    ct.maChiTietCV,
                    ct.tieuDe,
                    ct.noiDung,
                    ct.ngayHoanThanh,                    
                    ct.soNgayHoanThanh,
                    ct.trangThai,
                    ct.tienDo,
                    nguoiNhan.maNguoiDung AS nguoiNhanID,
                    nguoiNhan.hoTen AS nguoiNhan_HoTen,
                    nguoiNhan.email AS nguoiNhan_Email,
                    dv.tenDonVi,
                    pb.tenPhongBan,
                    cvu.tenChucVu
                FROM CongViec cv
                JOIN ChiTietCongViec ct ON cv.maCongViec = ct.maCongViec
                JOIN NguoiLienQuanCongViec nlcq ON cv.maCongViec = nlcq.maCongViec AND nlcq.vaiTro = 'to'
                JOIN Email e ON ct.maChiTietCV = e.maChiTietCV
                JOIN NguoiDung nguoiNhan ON nlcq.maNguoiDung = nguoiNhan.maNguoiDung
                LEFT JOIN DonVi dv ON nguoiNhan.maDonVi = dv.maDonVi
                LEFT JOIN PhongBan pb ON nguoiNhan.maPhongBan = pb.maPhongBan
                LEFT JOIN ChucVu cvu ON nguoiNhan.maChucVu = cvu.maChucVu
                WHERE cv.nguoiGiao = @nguoiGiao AND e.trangThai = 1
                ORDER BY CAST(ct.trangThai AS INT) ASC";

            SqlParameter[] parameters = {
                new SqlParameter("@nguoiGiao", id)
            };

            return conn.ExecuteQuery(query, parameters);
        }

        public DataTable GetCongViecDuocGiaoByUserId(int id)
        {
            string query = @"
            SELECT 
                cv.maCongViec,
                cv.ngayGiao,
                CASE WHEN cv.lapLai = 1 THEN N'Có' ELSE N'Không' END AS lapLai,
                cv.tanSuat,
                ct.ngayNhanCongViec,
                ct.ngayKetThucCongViec,
                ct.maChiTietCV,
                ct.tieuDe,
                ct.noiDung,
                ct.ngayHoanThanh,                    
                ct.soNgayHoanThanh,
                ct.trangThai,
                ct.tienDo,

                -- Người giao
                nguoiGiao.hoTen AS nguoiGiao_HoTen,
                nguoiGiao.email AS nguoiGiao_Email,

                -- Người nhận
                nguoiNhan.maNguoiDung AS nguoiNhanID,
                nguoiNhan.hoTen AS nguoiNhan_HoTen,
                nguoiNhan.email AS nguoiNhan_Email,

                dv.tenDonVi,
                pb.tenPhongBan,
                cvu.tenChucVu

            FROM CongViec cv
            JOIN ChiTietCongViec ct ON cv.maCongViec = ct.maCongViec
            JOIN Email e ON ct.maChiTietCV = e.maChiTietCV
            JOIN NguoiDung nguoiGiao ON cv.nguoiGiao = nguoiGiao.maNguoiDung
            JOIN NguoiLienQuanCongViec nlcq ON cv.maCongViec = nlcq.maCongViec AND nlcq.maNguoiDung = @nguoiNhan
            JOIN NguoiDung nguoiNhan ON nguoiNhan.maNguoiDung = nlcq.maNguoiDung
            LEFT JOIN DonVi dv ON nguoiGiao.maDonVi = dv.maDonVi
            LEFT JOIN PhongBan pb ON nguoiGiao.maPhongBan = pb.maPhongBan
            LEFT JOIN ChucVu cvu ON nguoiGiao.maChucVu = cvu.maChucVu
            WHERE e.trangThai = 1   

            ORDER BY cv.ngayGiao DESC";

            SqlParameter[] parameters = {
            new SqlParameter("@nguoiNhan", id)
        };

            return conn.ExecuteQuery(query, parameters);
        }

        public DataTable GetCongViecDuocGiao_SortByTrangThai(int id)
        {
            string query = @"
                SELECT 
                cv.maCongViec,
                cv.ngayGiao,
                CASE WHEN cv.lapLai = 1 THEN N'Có' ELSE N'Không' END AS lapLai,
                cv.tanSuat,
                ct.ngayNhanCongViec,
                ct.ngayKetThucCongViec,
                ct.maChiTietCV,
                ct.tieuDe,
                ct.noiDung,
                ct.ngayHoanThanh,                    
                ct.soNgayHoanThanh,
                ct.trangThai,
                ct.tienDo,

                nguoiGiao.hoTen AS nguoiGiao_HoTen,
                nguoiGiao.email AS nguoiGiao_Email,

                nguoiNhan.maNguoiDung AS nguoiNhanID,
                nguoiNhan.hoTen AS nguoiNhan_HoTen,
                nguoiNhan.email AS nguoiNhan_Email,

                dv.tenDonVi,
                pb.tenPhongBan,
                cvu.tenChucVu

            FROM CongViec cv
            JOIN ChiTietCongViec ct ON cv.maCongViec = ct.maCongViec
            JOIN Email e ON ct.maChiTietCV = e.maChiTietCV
            JOIN NguoiDung nguoiGiao ON cv.nguoiGiao = nguoiGiao.maNguoiDung
            JOIN NguoiLienQuanCongViec nlcq ON cv.maCongViec = nlcq.maCongViec AND nlcq.maNguoiDung = @nguoiNhan
            JOIN NguoiDung nguoiNhan ON nguoiNhan.maNguoiDung = nlcq.maNguoiDung
            LEFT JOIN DonVi dv ON nguoiGiao.maDonVi = dv.maDonVi
            LEFT JOIN PhongBan pb ON nguoiGiao.maPhongBan = pb.maPhongBan
            LEFT JOIN ChucVu cvu ON nguoiGiao.maChucVu = cvu.maChucVu
            WHERE e.trangThai = 1   

                ORDER BY 
                    CASE CAST(ct.trangThai AS INT)
                        WHEN 0 THEN 0
                        WHEN 1 THEN 1
                        WHEN 2 THEN 2
                        WHEN 3 THEN 3
                        ELSE 4
                END";

            SqlParameter[] parameters = {
                new SqlParameter("@nguoiNhan", id)
            };

            return conn.ExecuteQuery(query, parameters);
        }

        public DataTable GetCongViecByIdCongViec(int maChiTietCV)
        {
            string query = @"
            SELECT 
                cv.maCongViec,
                    cv.ngayGiao,
                    nguoiGiao.hoTen AS nguoiGiao_HoTen,
                    CASE WHEN cv.lapLai = 1 THEN N'Có' ELSE N'Không' END AS lapLai,
                    cv.tanSuat,
                    ct.ngayNhanCongViec,
                    ct.ngayKetThucCongViec,
                    ct.maChiTietCV,
                    ct.tieuDe,
                    ct.noiDung,
                    ct.ngayHoanThanh,                    
                    ct.soNgayHoanThanh,
                    ct.trangThai,
                    ct.tienDo,
                    nguoiNhan.maNguoiDung AS nguoiNhanID,
                    nguoiNhan.hoTen AS nguoiNhan_HoTen,
                    nguoiNhan.email AS nguoiNhan_Email,
                    dv.tenDonVi,
                    pb.tenPhongBan,
                    cvu.tenChucVu
            FROM CongViec cv
            JOIN ChiTietCongViec ct ON cv.maCongViec = ct.maCongViec

            JOIN NguoiDung nguoiGiao ON cv.nguoiGiao = nguoiGiao.maNguoiDung
            LEFT JOIN DonVi dv ON nguoiGiao.maDonVi = dv.maDonVi
            LEFT JOIN PhongBan pb ON nguoiGiao.maPhongBan = pb.maPhongBan
            LEFT JOIN ChucVu cvu ON nguoiGiao.maChucVu = cvu.maChucVu

            LEFT JOIN NguoiLienQuanCongViec nlcq ON cv.maCongViec = nlcq.maCongViec AND nlcq.vaiTro = 'to'
            LEFT JOIN NguoiDung nguoiNhan ON nlcq.maNguoiDung = nguoiNhan.maNguoiDung
            LEFT JOIN DonVi dv_nhan ON nguoiNhan.maDonVi = dv_nhan.maDonVi
            LEFT JOIN PhongBan pb_nhan ON nguoiNhan.maPhongBan = pb_nhan.maPhongBan
            LEFT JOIN ChucVu cvu_nhan ON nguoiNhan.maChucVu = cvu_nhan.maChucVu

            WHERE ct.maChiTietCV = @maChiTietCV";

            SqlParameter[] parameters = {
            new SqlParameter("@maChiTietCV", maChiTietCV)
            };

            return conn.ExecuteQuery(query, parameters);
        }

        public CongViec getCongViecById(string maCongViec)
        {
            string query = @"SELECT 
                        maCongViec,
                        nguoiGiao,
                        ngayGiao,
                        lapLai,
                        tanSuat,
                        ngayBatDau,
                        ngayKetThuc
                    FROM CongViec
                    WHERE maCongViec = @maCongViec";

            SqlParameter[] parameters = {
                new SqlParameter("@maCongViec", maCongViec)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            CongViec cv = new CongViec
            {
                MaCongViec = row["maCongViec"].ToString(),
                NguoiGiao = Convert.ToInt32(row["nguoiGiao"]),
                NgayGiao = row["ngayGiao"] != DBNull.Value ? Convert.ToDateTime(row["ngayGiao"]) : (DateTime?)null,
                LapLai = row["lapLai"] != DBNull.Value ? Convert.ToBoolean(row["lapLai"]) : (bool?)null,
                TanSuat = row["tanSuat"] != DBNull.Value ? row["tanSuat"].ToString() : null,
                NgayBatDau = row["ngayBatDau"] != DBNull.Value ? Convert.ToDateTime(row["ngayBatDau"]) : (DateTime?)null,
                NgayKetThuc = row["ngayKetThuc"] != DBNull.Value ? Convert.ToDateTime(row["ngayKetThuc"]) : (DateTime?)null,
            };

            return cv;
        }

        public ChiTietCongViec getChiTietCongViecById(int maCTCV)
        {
            string query = @"
            SELECT ct.*, cv.nguoiGiao, cv.ngayGiao, cv.lapLai, cv.tanSuat, cv.ngayBatDau, cv.ngayKetThuc
            FROM ChiTietCongViec ct
            JOIN CongViec cv ON ct.maCongViec = cv.maCongViec
            WHERE ct.maChiTietCV = @maCTCV";

            SqlParameter[] parameters = {
                new SqlParameter("@maCTCV", maCTCV)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            CongViec congViec = new CongViec
            {
                MaCongViec = row["maCongViec"].ToString(),
                NguoiGiao = Convert.ToInt32(row["nguoiGiao"]),
                NgayGiao = row["ngayGiao"] != DBNull.Value ? Convert.ToDateTime(row["ngayGiao"]) : (DateTime?)null,
                LapLai = row["lapLai"] != DBNull.Value ? Convert.ToBoolean(row["lapLai"]) : (bool?)null,
                TanSuat = row["tanSuat"] != DBNull.Value ? row["tanSuat"].ToString() : null,
                NgayBatDau = row["ngayBatDau"] != DBNull.Value ? Convert.ToDateTime(row["ngayBatDau"]) : (DateTime?)null,
                NgayKetThuc = row["ngayKetThuc"] != DBNull.Value ? Convert.ToDateTime(row["ngayKetThuc"]) : (DateTime?)null
            };

            ChiTietCongViec chiTiet = new ChiTietCongViec
            {
                MaChiTietCV = Convert.ToInt32(row["maChiTietCV"]),
                MaCongViec = row["maCongViec"].ToString(),
                TieuDe = row["tieuDe"].ToString(),
                NoiDung = row["noiDung"].ToString(),
                NgayNhanCongViec = row["ngayNhanCongViec"] != DBNull.Value ? Convert.ToDateTime(row["ngayNhanCongViec"]) : (DateTime?)null,
                NgayKetThucCongViec = row["ngayKetThucCongViec"] != DBNull.Value ? Convert.ToDateTime(row["ngayKetThucCongViec"]) : (DateTime?)null,
                NgayHoanThanh = row["ngayHoanThanh"] != DBNull.Value ? Convert.ToDateTime(row["ngayHoanThanh"]) : (DateTime?)null,
                SoNgayHoanThanh = Convert.ToInt32(row["soNgayHoanThanh"]),
                TrangThai = Convert.ToInt32(row["trangThai"]),
                TienDo = row["tienDo"] != DBNull.Value ? Convert.ToInt32(row["tienDo"]) : (int?)null,
                CongViec = congViec
            };

            return chiTiet;
        }

        public DataTable GetDanhSachNguoiLienQuanByIdCongViec(string maCongViec)
        {
            string query = @"
            SELECT 
                nd.maNguoiDung,
                nd.hoTen,
                nd.email,
                dv.tenDonVi,
                pb.tenPhongBan,
                cvu.tenChucVu,
                nlcq.vaiTro
            FROM NguoiLienQuanCongViec nlcq
            JOIN NguoiDung nd ON nlcq.maNguoiDung = nd.maNguoiDung
            LEFT JOIN DonVi dv ON nd.maDonVi = dv.maDonVi
            LEFT JOIN PhongBan pb ON nd.maPhongBan = pb.maPhongBan
            LEFT JOIN ChucVu cvu ON nd.maChucVu = cvu.maChucVu
            WHERE nlcq.maCongViec = @maCongViec";

            SqlParameter[] parameters = {
            new SqlParameter("@maCongViec", maCongViec)
            };

            return conn.ExecuteQuery(query, parameters);
        }

        public List<NguoiLienQuanCongViec> GetListNguoiLienQuanByIdCongViec(string maCongViec)
        {
            var danhSach = new List<NguoiLienQuanCongViec>();

            string query = @"
            SELECT 
                nd.maNguoiDung,
                nd.hoTen,
                nd.email,
                nd.maPhongBan,
                nd.maDonVi,
                nd.maChucVu,
                nd.laLanhDao,

                dv.maDonVi,
                dv.tenDonVi,

                pb.maPhongBan,
                pb.tenPhongBan,

                cvu.maChucVu,
                cvu.tenChucVu,

                nlcq.vaiTro
            FROM NguoiLienQuanCongViec nlcq
            JOIN NguoiDung nd ON nlcq.maNguoiDung = nd.maNguoiDung
            LEFT JOIN DonVi dv ON nd.maDonVi = dv.maDonVi
            LEFT JOIN PhongBan pb ON nd.maPhongBan = pb.maPhongBan
            LEFT JOIN ChucVu cvu ON nd.maChucVu = cvu.maChucVu
            WHERE nlcq.maCongViec = @maCongViec";

            using (SqlConnection connection = conn.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@maCongViec", maCongViec);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nguoiDung = new NguoiDung
                        {
                            MaNguoiDung = reader.GetInt32(reader.GetOrdinal("maNguoiDung")),
                            HoTen = reader["hoTen"]?.ToString(),
                            Email = reader["email"]?.ToString(),
                            MaPhongBan = reader["maPhongBan"]?.ToString(),
                            MaDonVi = reader["maDonVi"]?.ToString(),
                            MaChucVu = reader["maChucVu"]?.ToString(),
                            LaLanhDao = reader["laLanhDao"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["laLanhDao"]) : null,

                            DonVi = new DonVi
                            {
                                MaDonVi = reader["maDonVi"]?.ToString(),
                                TenDonVi = reader["tenDonVi"]?.ToString()
                            },

                            PhongBan = new PhongBan
                            {
                                MaPhongBan = reader["maPhongBan"]?.ToString(),
                                TenPhongBan = reader["tenPhongBan"]?.ToString()
                            },

                            ChucVu = new ChucVu
                            {
                                MaChucVu = reader["maChucVu"]?.ToString(),
                                TenChucVu = reader["tenChucVu"]?.ToString()
                            }
                        };

                        var nlk = new NguoiLienQuanCongViec
                        {
                            MaCongViec = maCongViec,
                            MaNguoiDung = nguoiDung.MaNguoiDung,
                            VaiTro = reader["vaiTro"]?.ToString(),
                            NguoiDung = nguoiDung
                        };

                        danhSach.Add(nlk);
                    }
                }
            }

            return danhSach;
        }

        public bool sendEmail(Email em, List<NguoiNhanEmail> lstNguoiNhanEmail, List<TepDinhKemEmail> lstFile, NguoiDung u)
        {
            Env.Load("../../.env");

            string CLIENT = Env.GetString("SMTP_CLIENT");
            int PORT = Convert.ToInt32(Env.GetString("SMTP_PORT"));

            if (em != null && lstNguoiNhanEmail != null)
            {
                try
                {
                    var message = new MimeMessage();

                    message.From.Add(new MailboxAddress("", u.Email));
                    message.Subject = em.TieuDe ?? "";

                    // To / Cc / Bcc
                    foreach (NguoiNhanEmail nguoiNhan in lstNguoiNhanEmail)
                    {
                        var email = nguoiNhan.NguoiDung.Email;
                        switch (nguoiNhan.VaiTro)
                        {
                            case "to":
                                message.To.Add(new MailboxAddress("", email));
                                break;
                            case "cc":
                                message.Cc.Add(new MailboxAddress("", email));
                                break;
                            case "bcc":
                                message.Bcc.Add(new MailboxAddress("", email));
                                break;
                        }
                    }

                    // Body + file đính kèm
                    var builder = new BodyBuilder
                    {
                        HtmlBody = em.NoiDung ?? "",
                        TextBody = Regex.Replace(em.NoiDung ?? "", "<.*?>", string.Empty)
                    };

                    // Đính kèm file
                    if (lstFile != null && lstFile.Count > 0)
                    {
                        foreach (var tep in lstFile)
                        {
                            string path = tep.TepTin?.DuongDan;
                            string tenTepGoc = tep.TepTin?.TenTepGoc;

                            if (!string.IsNullOrEmpty(path) && File.Exists(path))
                            {
                                builder.Attachments.Add(new MimePart()
                                {
                                    Content = new MimeContent(File.OpenRead(path)),
                                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                                    ContentTransferEncoding = ContentEncoding.Base64,
                                    FileName = tenTepGoc
                                });
                            }
                        }
                    }

                    message.Body = builder.ToMessageBody();

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        client.Connect(CLIENT, PORT, MailKit.Security.SecureSocketOptions.StartTls);
                        client.Authenticate(u.Email, u.MatKhau);
                        client.Send(message);
                        client.Disconnect(true);
                    }

                    //export .eml
                    string targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Attachments");
                    if (!Directory.Exists(targetFolder))
                        Directory.CreateDirectory(targetFolder);

                    // Thư mục tạm để chứa .eml
                    string tempFolder = Path.Combine(Path.GetTempPath(), $"EmailTemp_{Guid.NewGuid()}");
                    Directory.CreateDirectory(tempFolder);

                    string safeSubject = SanitizeFileName(em.TieuDe ?? "email");
                    string emlFileName = $"{safeSubject}_{DateTime.Now:yyyyMMdd_HHmmss}.eml";
                    string emlPath = Path.Combine(tempFolder, emlFileName);

                    using (var stream = File.Create(emlPath))
                    {
                        message.WriteTo(stream);
                    }

                    string zipPath = Path.Combine(targetFolder, Path.GetFileNameWithoutExtension(emlFileName) + ".zip");

                    if (File.Exists(zipPath))
                        File.Delete(zipPath);

                    ZipFile.CreateFromDirectory(tempFolder, zipPath, CompressionLevel.Optimal, includeBaseDirectory: false);

                    Directory.Delete(tempFolder, true);

                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi gửi email (MailKit): " + ex.Message);
                    return false;
                }
            }

            return false;
        }

        private string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }

        public bool createCongViec(CongViec cv)
        {
            string query = @"INSERT INTO CongViec 
            (maCongViec, nguoiGiao, ngayGiao, lapLai, tanSuat, ngayBatDau, ngayKetThuc)
            VALUES 
            (@maCongViec, @nguoiGiao, @ngayGiao, @lapLai, @tanSuat, @ngayBatDau, @ngayKetThuc)";

            SqlParameter[] parameters = {
                new SqlParameter("@maCongViec", cv.MaCongViec),
                new SqlParameter("@nguoiGiao", cv.NguoiGiao),
                new SqlParameter("@ngayGiao", cv.NgayGiao.HasValue ? (object)cv.NgayGiao.Value : DBNull.Value),
                new SqlParameter("@lapLai", cv.LapLai.HasValue ? (object)cv.LapLai.Value : DBNull.Value),
                new SqlParameter("@tanSuat", string.IsNullOrEmpty(cv.TanSuat) ? (object)DBNull.Value : cv.TanSuat),
                new SqlParameter("@ngayBatDau", cv.NgayBatDau.HasValue ? (object)cv.NgayBatDau.Value : DBNull.Value),
                new SqlParameter("@ngayKetThuc", cv.NgayKetThuc.HasValue ? (object)cv.NgayKetThuc.Value : DBNull.Value),
            };

            int rowsAffected = conn.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public int CreateChiTietCongViec(ChiTietCongViec ct)
        {
            string query = @"
            INSERT INTO ChiTietCongViec 
                (maCongViec, tieuDe, noiDung, ngayNhanCongViec, ngayKetThucCongViec, ngayHoanThanh, soNgayHoanThanh, trangThai, tienDo)
            OUTPUT INSERTED.maChiTietCV
            VALUES 
            (@maCongViec, @tieuDe, @noiDung, @ngayNhanCongViec, @ngayKetThucCongViec, @ngayHoanThanh, @soNgayHoanThanh, @trangThai, @tienDo)";

            SqlParameter[] parameters = {
                new SqlParameter("@maCongViec", ct.MaCongViec ?? (object)DBNull.Value),
                new SqlParameter("@tieuDe", ct.TieuDe ?? (object)DBNull.Value),
                new SqlParameter("@noiDung", ct.NoiDung ?? (object)DBNull.Value),
                new SqlParameter("@ngayNhanCongViec", ct.NgayNhanCongViec.HasValue ? (object)ct.NgayNhanCongViec.Value : DBNull.Value),
                new SqlParameter("@ngayKetThucCongViec", ct.NgayKetThucCongViec.HasValue ? (object)ct.NgayKetThucCongViec.Value : DBNull.Value),
                new SqlParameter("@ngayHoanThanh", ct.NgayHoanThanh.HasValue ? (object)ct.NgayHoanThanh.Value : DBNull.Value),
                new SqlParameter("@soNgayHoanThanh", ct.SoNgayHoanThanh),
                new SqlParameter("@trangThai", ct.TrangThai),
                new SqlParameter("@tienDo", ct.TienDo.HasValue ? (object)ct.TienDo.Value : DBNull.Value)
            };

            object result = conn.ExecuteScalar(query, parameters);
            if (result != null && int.TryParse(result.ToString(), out int maMoi))
            {
                return maMoi;
            }
            return -1;
        }

        public List<ChiTietCongViec> TaoChiTietCongViecTheoTanSuat(CongViec congViec, int soNgayHoanThanh)
        {
            List<ChiTietCongViec> danhSachChiTiet = new List<ChiTietCongViec>();

            if (congViec.NgayBatDau == null || congViec.NgayKetThuc == null || string.IsNullOrWhiteSpace(congViec.TanSuat))
                throw new ArgumentException("Thiếu thông tin công việc định kỳ");

            DateTime ngayBatDau = congViec.NgayBatDau.Value;
            DateTime ngayKetThuc = congViec.NgayKetThuc.Value;
            DateTime ngayLap = ngayBatDau;

            int lapIndex = 1;

            while (ngayLap <= ngayKetThuc)
            {
                DateTime ngayKetThucCongViec = ngayLap.AddDays(soNgayHoanThanh);
                var chiTiet = new ChiTietCongViec
                {
                    NgayNhanCongViec = ngayLap,
                    NgayKetThucCongViec = ngayKetThucCongViec,
                    TrangThai = 0, // 0: chưa xử lý
                    TienDo = 0
                };

                danhSachChiTiet.Add(chiTiet);
                lapIndex++;

                // Tính lần kế tiếp
                switch (congViec.TanSuat.Trim().ToLower())
                {
                    case "ngay":
                        ngayLap = ngayLap.AddDays(1);
                        break;
                    case "tuan":
                        ngayLap = ngayLap.AddDays(7);
                        break;
                    case "thang":
                        ngayLap = ngayLap.AddMonths(1);
                        break;
                    case "nam":
                        ngayLap = ngayLap.AddYears(1);
                        break;
                    default:
                        throw new Exception("Tần suất không hợp lệ: " + congViec.TanSuat);
                }
            }

            return danhSachChiTiet;
        }

        public bool CreateNguoiLienQuan(NguoiLienQuanCongViec n)
        {
            string query = @"
                INSERT INTO NguoiLienQuanCongViec 
                    (maCongViec, maNguoiDung, vaiTro)
                VALUES 
                    (@maCongViec, @maNguoiDung, @vaiTro)";

            SqlParameter[] parameters = {
                new SqlParameter("@maCongViec", n.MaCongViec),
                new SqlParameter("@maNguoiDung", n.MaNguoiDung),
                new SqlParameter("@vaiTro", n.VaiTro ?? (object)DBNull.Value)
            };

            return conn.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool CreateEmail(Email e)
        {
            string query = @"
                INSERT INTO Email 
                    (maEmail, nguoiGui, maChiTietCV, tieuDe, noiDung, ngayGui, trangThai)
                VALUES 
                    (@maEmail, @nguoiGui, @maChiTietCV, @tieuDe, @noiDung, @ngayGui, @trangThai)";

            SqlParameter[] parameters = {
                new SqlParameter("@maEmail", e.MaEmail ?? (object)DBNull.Value),
                new SqlParameter("@nguoiGui", e.NguoiGui),
                new SqlParameter("@maChiTietCV", e.MaChiTietCV),
                new SqlParameter("@tieuDe", e.TieuDe ?? (object)DBNull.Value),
                new SqlParameter("@noiDung", e.NoiDung ?? (object)DBNull.Value),
                new SqlParameter("@ngayGui", e.NgayGui.HasValue ? (object)e.NgayGui.Value : DBNull.Value),
                new SqlParameter("@trangThai", e.trangThai)
            };

            return conn.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool CreateNguoiNhanEmail(NguoiNhanEmail n)
        {
            string query = @"
                INSERT INTO NguoiNhanEmail 
                    (maEmail, maNguoiDung, vaiTro)
                VALUES 
                    (@maEmail, @maNguoiDung, @vaiTro)";

            SqlParameter[] parameters = {
                new SqlParameter("@maEmail", n.MaEmail),
                new SqlParameter("@maNguoiDung", n.MaNguoiDung),
                new SqlParameter("@vaiTro", n.VaiTro ?? (object)DBNull.Value)
            };

            return conn.ExecuteNonQuery(query, parameters) > 0;
        }

        public int CreateTepTin(TepTin t)
        {
            string query = @"
                INSERT INTO TepTin (tenTep, tenTepGoc, duongDan)
                VALUES (@tenTep, @tenTepGoc, @duongDan);
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@tenTep", t.TenTep),
                new SqlParameter("@tenTepGoc", t.TenTepGoc),
                new SqlParameter("@duongDan", t.DuongDan)
            };

            object result = conn.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public bool CreateTepTinDinhKem(TepDinhKemEmail tdk)
        {
            string query = @"
                INSERT INTO TepDinhKemEmail 
                    (maEmail, maTep)
                VALUES 
                    (@maEmail, @maTep)";

            SqlParameter[] parameters = {
                new SqlParameter("@maEmail", tdk.MaEmail),
                new SqlParameter("@maTep", tdk.MaTep)
            };

            return conn.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool createPhanHoiCongViec(PhanHoiCongViec ph)
        {
            string query = @"INSERT INTO PhanHoiCongViec 
            (MaCongViec, MaNguoiDung, NoiDung, ThoiGian, Loai)
            VALUES 
            (@MaCongViec, @MaNguoiDung, @NoiDung, @ThoiGian, @Loai)";

            SqlParameter[] parameters = {
                new SqlParameter("@MaCongViec", ph.MaCongViec),
                new SqlParameter("@MaNguoiDung", ph.MaNguoiDung),
                new SqlParameter("@NoiDung", ph.NoiDung),
                new SqlParameter("@ThoiGian", ph.ThoiGian.HasValue ? (object)ph.ThoiGian.Value : DBNull.Value),
                new SqlParameter("@Loai", string.IsNullOrEmpty(ph.Loai) ? (object)DBNull.Value : ph.Loai)
            };

            int rowsAffected = conn.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public bool CreateThongBao(ThongBaoNguoiDung tb)
        {
            string query = @"
            INSERT INTO ThongBaoNguoiDung
            (noiDung, maChiTietCV, maNguoiDung, trangThai, ngayThongBao)
            VALUES 
            (@noiDung, @maChiTietCV, @maNguoiDung, @trangThai, @ngayThongBao)";

            SqlParameter[] parameters = {
                new SqlParameter("@noiDung", tb.NoiDung ?? (object)DBNull.Value),
                new SqlParameter("@maChiTietCV", tb.MaChiTietCV),
                new SqlParameter("@maNguoiDung", tb.MaNguoiDung),
                new SqlParameter("@trangThai", tb.TrangThai),
                new SqlParameter("@ngayThongBao", tb.NgayThongBao)
            };

            return conn.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateTrangThaiCongViec(ChiTietCongViec ct)
        {
            string query = @"
                UPDATE ChiTietCongViec
                SET trangThai = @trangThai, tienDo = @tienDo, ngayHoanThanh = @ngayHoanThanh
                WHERE maChiTietCV = @maCongViec";

            SqlParameter[] parameters = {
                new SqlParameter("@maCongViec", ct.MaChiTietCV),
                new SqlParameter("@trangThai", ct.TrangThai),
                new SqlParameter("@tienDo", ct.TienDo),
                new SqlParameter("@ngayHoanThanh", ct.NgayHoanThanh)
            };

            int rowsAffected = conn.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public bool UpdateOnlyTrangThaiCongViec(ChiTietCongViec ct)
        {
            string query = @"
                UPDATE ChiTietCongViec
                SET trangThai = @trangThai
                WHERE maChiTietCV = @maCongViec";

            SqlParameter[] parameters = {
                new SqlParameter("@maCongViec", ct.MaChiTietCV),
                new SqlParameter("@trangThai", ct.TrangThai),
            };

            int rowsAffected = conn.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public bool UpdateHanHoanThanhTask(ChiTietCongViec ct)
        {
            string query = @"
                UPDATE ChiTietCongViec
                SET ngayHoanThanh = @ngayHoanThanh
                WHERE maChiTietCV = @maCongViec";

            SqlParameter[] parameters = {
                new SqlParameter("@maCongViec", ct.MaChiTietCV),
                new SqlParameter("@ngayHoanThanh", ct.NgayHoanThanh)
            };

            int rowsAffected = conn.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public bool UpdateTrangThaiEmail(Email e)
        {
            string query = @"
                UPDATE Email
                SET trangThai = @trangThai
                WHERE maEmail = @maEmail";

            SqlParameter[] parameters = {
                new SqlParameter("@maEmail", e.MaEmail),
                new SqlParameter("@trangThai", e.trangThai),

            };

            int rowsAffected = conn.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public bool UpdateTienDoCongViec(ChiTietCongViec ct)
        {
            string query = @"
                UPDATE ChiTietCongViec
                SET tienDo = @tienDo
                WHERE maChiTietCV = @maChiTietCV";

            SqlParameter[] parameters = {
                new SqlParameter("@maChiTietCV", ct.MaChiTietCV),
                new SqlParameter("@tienDo", ct.TienDo)
            };

            int rowsAffected = conn.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public List<TepDinhKemEmail> GetTepDinhKemEmailByMaCongViec(string maCongViec)
        {
            List<TepDinhKemEmail> result = new List<TepDinhKemEmail>();

            string query = @"
             SELECT 
                tdk.maEmail, 
                tdk.maTep,
                tt.tenTep, tt.duongDan, tt.tenTepGoc
                FROM 
                    TepDinhKemEmail tdk
                INNER JOIN 
                    Email e ON tdk.maEmail = e.maEmail
                INNER JOIN 
                    ChiTietCongViec ct ON e.maChiTietCV = ct.maChiTietCV
                INNER JOIN 
                    TepTin tt ON tdk.maTep = tt.maTep
                WHERE 
                ct.maCongViec = @maCongViec";

            SqlParameter[] parameters = {
                new SqlParameter("@maCongViec", maCongViec)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                TepDinhKemEmail tdk = new TepDinhKemEmail
                {
                    MaEmail = row["maEmail"].ToString(),
                    MaTep = Convert.ToInt32(row["maTep"]),
                    TepTin = new TepTin
                    {
                        MaTep = Convert.ToInt32(row["maTep"]),
                        TenTep = row["tenTep"].ToString(),
                        TenTepGoc = row["tenTepGoc"].ToString(),
                        DuongDan = row["duongDan"].ToString(),
                    }
                };

                result.Add(tdk);
            }

            return result;
        }

        public List<PhanHoiCongViec> GetFeedbacksByMaCongViec(string maCongViec)
        {
            var danhSach = new List<PhanHoiCongViec>();

            string query = @"
            SELECT 
                ph.maPhanHoi,
                ph.maCongViec,
                ph.maNguoiDung,
                ph.noiDung,
                ph.thoiGian,
                ph.loai,

                nd.maNguoiDung,
                nd.hoTen,
                nd.email,
                nd.maPhongBan,
                nd.maDonVi,
                nd.maChucVu,
                nd.laLanhDao,

                dv.maDonVi,
                dv.tenDonVi,

                pb.maPhongBan,
                pb.tenPhongBan,

                cvu.maChucVu,
                cvu.tenChucVu
            FROM PhanHoiCongViec ph
            JOIN NguoiDung nd ON ph.maNguoiDung = nd.maNguoiDung
            LEFT JOIN DonVi dv ON nd.maDonVi = dv.maDonVi
            LEFT JOIN PhongBan pb ON nd.maPhongBan = pb.maPhongBan
            LEFT JOIN ChucVu cvu ON nd.maChucVu = cvu.maChucVu
            WHERE ph.maCongViec = @maCongViec
            ORDER BY ph.thoiGian ASC";

            using (SqlConnection connection = conn.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@maCongViec", maCongViec);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nguoiDung = new NguoiDung
                        {
                            MaNguoiDung = reader.GetInt32(reader.GetOrdinal("maNguoiDung")),
                            HoTen = reader["hoTen"]?.ToString(),
                            Email = reader["email"]?.ToString(),
                            MaPhongBan = reader["maPhongBan"]?.ToString(),
                            MaDonVi = reader["maDonVi"]?.ToString(),
                            MaChucVu = reader["maChucVu"]?.ToString(),
                            LaLanhDao = reader["laLanhDao"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["laLanhDao"]) : null,

                            DonVi = new DonVi
                            {
                                MaDonVi = reader["maDonVi"]?.ToString(),
                                TenDonVi = reader["tenDonVi"]?.ToString()
                            },

                            PhongBan = new PhongBan
                            {
                                MaPhongBan = reader["maPhongBan"]?.ToString(),
                                TenPhongBan = reader["tenPhongBan"]?.ToString()
                            },

                            ChucVu = new ChucVu
                            {
                                MaChucVu = reader["maChucVu"]?.ToString(),
                                TenChucVu = reader["tenChucVu"]?.ToString()
                            }
                        };

                        var phanHoi = new PhanHoiCongViec
                        {
                            MaPhanHoi = reader.GetInt32(reader.GetOrdinal("maPhanHoi")),
                            MaCongViec = reader["maCongViec"]?.ToString(),
                            MaNguoiDung = reader.GetInt32(reader.GetOrdinal("maNguoiDung")),
                            NoiDung = reader["noiDung"]?.ToString(),
                            ThoiGian = reader["thoiGian"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["thoiGian"]) : null,
                            Loai = reader["loai"]?.ToString(),
                            NguoiDung = nguoiDung
                        };

                        danhSach.Add(phanHoi);
                    }
                }
            }

            return danhSach;
        }

        public List<NguoiNhanEmail> GetListNguoiNhanEmail(string maEmail)
        {
            var danhSach = new List<NguoiNhanEmail>();

            string query = @"
            SELECT 
                nre.maEmail, 
                nre.maNguoiDung, 
                nre.vaiTro,

                nd.hoTen, nd.email AS emailNguoiDung, nd.matKhau,
                nd.maPhongBan, nd.maDonVi, nd.maChucVu, nd.laLanhDao,

                e.maEmail, e.nguoiGui, e.maChiTietCV, e.tieuDe, e.noiDung, e.ngayGui, e.trangThai
            FROM 
                NguoiNhanEmail nre
            INNER JOIN 
                NguoiDung nd ON nre.maNguoiDung = nd.maNguoiDung
            INNER JOIN 
                Email e ON nre.maEmail = e.maEmail
            WHERE 
            nre.maEmail = @maEmail";

            SqlParameter[] parameters = {
                new SqlParameter("@maEmail", maEmail)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                var nguoiDung = new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(row["maNguoiDung"]),
                    HoTen = row["hoTen"]?.ToString(),
                    Email = row["emailNguoiDung"]?.ToString(),
                    MatKhau = row["matKhau"]?.ToString(),
                    MaPhongBan = row["maPhongBan"]?.ToString(),
                    MaDonVi = row["maDonVi"]?.ToString(),
                    MaChucVu = row["maChucVu"]?.ToString(),
                    LaLanhDao = row["laLanhDao"] != DBNull.Value ? (bool?)Convert.ToBoolean(row["laLanhDao"]) : null
                };

                var email = new Email
                {
                    MaEmail = row["maEmail"]?.ToString(),
                    NguoiGui = Convert.ToInt32(row["nguoiGui"]),
                    MaChiTietCV = Convert.ToInt32(row["maChiTietCV"]),
                    TieuDe = row["tieuDe"]?.ToString(),
                    NoiDung = row["noiDung"]?.ToString(),
                    NgayGui = row["ngayGui"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ngayGui"]) : null,
                    trangThai = Convert.ToInt32(row["trangThai"])
                };

                var nguoiNhan = new NguoiNhanEmail
                {
                    MaEmail = row["maEmail"]?.ToString(),
                    MaNguoiDung = Convert.ToInt32(row["maNguoiDung"]),
                    VaiTro = row["vaiTro"]?.ToString(),
                    NguoiDung = nguoiDung,
                    Email = email
                };

                danhSach.Add(nguoiNhan);
            }

            return danhSach;
        }

        public void Auto_SendEmail()
        {
            string query = @"
            SELECT 
                e.maEmail,
                e.tieuDe,
                e.noiDung,
                e.ngayGui,
                e.trangThai,
                e.maChiTietCV,
                e.nguoiGui,
                nd.maNguoiDung,
                nd.hoTen,
                nd.Email AS EmailNguoiGui,
                nd.MatKhau,
                nd.MaPhongBan,
                nd.MaDonVi,
                nd.MaChucVu,
                nd.LaLanhDao,
                cv.maCongViec as CONGVIEC
            FROM 
                Email e
            LEFT JOIN 
                NguoiDung nd ON e.nguoiGui = nd.maNguoiDung
            LEFT JOIN 
                ChiTietCongViec ctcv ON e.maChiTietCV = ctcv.maChiTietCV
            LEFT JOIN 
                CongViec cv ON ctcv.maCongViec = cv.maCongViec
            WHERE 
                e.trangThai = 0
                AND CAST(e.ngayGui AS DATE) = CAST(GETDATE() AS DATE);";

            DataTable dt = conn.ExecuteQuery(query, null);

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    Email email = new Email
                    {
                        MaEmail = row["maEmail"].ToString(),
                        TieuDe = row["tieuDe"].ToString(),
                        NoiDung = row["noiDung"].ToString(),
                        NgayGui = Convert.ToDateTime(row["ngayGui"]),
                        trangThai = Convert.ToInt32(row["trangThai"]),
                        MaChiTietCV = Convert.ToInt32(row["maChiTietCV"]),
                        NguoiGui = Convert.ToInt32(row["nguoiGui"]),

                        NguoiGuiObj = new NguoiDung
                        {
                            MaNguoiDung = Convert.ToInt32(row["nguoiGui"]),
                            HoTen = row["HoTen"].ToString(),
                            Email = row["EmailNguoiGui"].ToString(),
                            MatKhau = row.Table.Columns.Contains("MatKhau") ? row["MatKhau"].ToString() : null,
                            MaPhongBan = row["MaPhongBan"]?.ToString(),
                            MaDonVi = row["MaDonVi"]?.ToString(),
                            MaChucVu = row["MaChucVu"]?.ToString(),
                            LaLanhDao = row.Table.Columns.Contains("LaLanhDao") ? (bool?)Convert.ToBoolean(row["LaLanhDao"]) : null
                        },

                        ChiTietCongViec = new ChiTietCongViec
                        {
                            MaCongViec = row["CONGVIEC"].ToString()
                        }

                    };

                    List<NguoiNhanEmail> lstNguoiNhanEmail = GetListNguoiNhanEmail(email.MaEmail);

                    List<TepDinhKemEmail> lstFile = GetTepDinhKemEmailByMaCongViec(email.ChiTietCongViec.MaCongViec);

                    NguoiDungDAO nguoiDungDAO = new NguoiDungDAO();
                    NguoiDung u = nguoiDungDAO.GetNguoiDungById(email.NguoiGui);


                    bool sent = sendEmail(email, lstNguoiNhanEmail, lstFile, u);

                    if (sent)
                    {
                        string updateQuery = "UPDATE Email SET trangThai = 1 WHERE maEmail = @maEmail";
                        SqlParameter[] updateParams = {
                    new SqlParameter("@maEmail", email.MaEmail)
                };
                        conn.ExecuteNonQuery(updateQuery, updateParams);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi gửi email: {ex.Message}");
                }
            }
        }

        public void Auto_SendEmail_NhacNho()
        {
            CongViecDAO congViecDAO = new CongViecDAO();
            NguoiDungDAO nguoiDungDAO = new NguoiDungDAO();
            string query = @"
            SELECT *
            FROM ChiTietCongViec ctcv
            INNER JOIN NguoiLienQuanCongViec nlq ON nlq.maCongViec = ctcv.maCongViec
            INNER JOIN NguoiDung nd ON nd.maNguoiDung = nlq.maNguoiDung
            WHERE (ctcv.trangThai = 1 OR ctcv.trangThai = 0)
              AND ctcv.soNgayHoanThanh >= 3
              AND nlq.vaiTro = 'to'
              AND CAST(ctcv.ngayKetThucCongViec AS DATE) = CAST(DATEADD(DAY, 1, GETDATE()) AS DATE)";

            DataTable dt = conn.ExecuteQuery(query, null);
            List<NguoiNhanEmail> lstNguoiNhanEmail = new List<NguoiNhanEmail>();
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    ChiTietCongViec ct = new ChiTietCongViec
                    {
                        MaChiTietCV = Convert.ToInt32(row["maChiTietCV"]),
                        MaCongViec = row["maCongViec"].ToString(),
                        TieuDe = row["tieuDe"].ToString(),
                        NoiDung = row["noiDung"].ToString(),
                        NgayNhanCongViec = row["ngayNhanCongViec"] != DBNull.Value ? Convert.ToDateTime(row["ngayNhanCongViec"]) : (DateTime?)null,
                        NgayKetThucCongViec = row["ngayKetThucCongViec"] != DBNull.Value ? Convert.ToDateTime(row["ngayKetThucCongViec"]) : (DateTime?)null,
                        NgayHoanThanh = row["ngayHoanThanh"] != DBNull.Value ? Convert.ToDateTime(row["ngayHoanThanh"]) : (DateTime?)null,
                        SoNgayHoanThanh = Convert.ToInt32(row["soNgayHoanThanh"]),
                        TrangThai = Convert.ToInt32(row["trangThai"]),
                        TienDo = row["tienDo"] != DBNull.Value ? Convert.ToInt32(row["tienDo"]) : (int?)null,
                    };
                    string maEmail = nguoiDungDAO.GenerateMaEmailFromLast(ct.MaCongViec);

                    NguoiDung nn = nguoiDungDAO.GetNguoiDungById(Convert.ToInt32(row["maNguoiDung"]));


                    Email e = new Email
                    {
                        MaEmail = maEmail,
                        NguoiGui = 6,
                        MaChiTietCV = ct.MaChiTietCV,
                        TieuDe = "Nhắc nhở công việc sắp đến hạn",
                        NoiDung = $@"
                        <p>Xin chào <strong>{nn.HoTen}</strong>,</p>
                        <p>Công việc <strong>{ct.TieuDe}</strong> sẽ đến hạn vào ngày <strong>{ct.NgayKetThucCongViec:dd/MM/yyyy}</strong>.</p>
                        <p>Vui lòng hoàn thành đúng hạn!</p>
                        <p>Trân trọng,<br>Hệ thống quản lý công việc</p>",
                        NgayGui = DateTime.Now,
                        trangThai = 0,
                        ChiTietCongViec = ct,
                        NguoiGuiObj = new NguoiDung
                        {
                            MaNguoiDung = 6
                        }
                    };

                    CreateEmail(e);

                    NguoiNhanEmail nne = new NguoiNhanEmail
                    {
                        MaEmail = e.MaEmail,
                        MaNguoiDung = nn.MaNguoiDung,
                        VaiTro = "to",

                        Email = e,
                        NguoiDung = nn
                    };
                    lstNguoiNhanEmail.Add(nne);

                    //List<NguoiNhanEmail> lstNguoiNhanEmail = GetListNguoiNhanEmail(e.MaEmail);

                    List<TepDinhKemEmail> lstFile = new List<TepDinhKemEmail>();

                    Env.Load("../../.env");

                    string email = Env.GetString("EMAIL_ADDRESS");
                    string pass = Env.GetString("EMAIL_SECRET_PASS");

                    //Console.WriteLine("Email từ .env: " + email);
                    NguoiDung u = new NguoiDung
                    {
                        Email = email,
                        MatKhau = pass
                    };



                    bool sent = sendEmail(e, lstNguoiNhanEmail, lstFile, u);

                    if (sent)
                    {
                        ThongBaoNguoiDung tb = new ThongBaoNguoiDung
                        {
                            MaChiTietCV = ct.MaChiTietCV,
                            MaNguoiDung = nn.MaNguoiDung,
                            NoiDung = $@"Thông báo công việc {ct.TieuDe} sắp đến hạn, Vui lòng hoàn thành đúng hạn!",
                            NgayThongBao = DateTime.Now,
                            TrangThai = 0,

                            ChiTietCongViec = ct,
                            NguoiDung = nn

                        };
                        CreateThongBao(tb);

                        string updateQuery = "UPDATE Email SET trangThai = 1 WHERE maEmail = @maEmail";
                        SqlParameter[] updateParams = {
                    new SqlParameter("@maEmail", e.MaEmail)
                };
                        conn.ExecuteNonQuery(updateQuery, updateParams);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi gửi email: {ex.Message}");
                }
            }
        }

        public void CapNhatTrangThaiTreHan()
        {
            string query = @"
            SELECT maChiTietCV, trangThai
            FROM ChiTietCongViec
            WHERE (trangThai = 0 OR trangThai = 1)
              AND CAST(GETDATE() AS DATE) > DATEADD(DAY, 1, CAST(ngayKetThucCongViec AS DATE))";

            DataTable dt = conn.ExecuteQuery(query, null);

            foreach (DataRow row in dt.Rows)
            {
                ChiTietCongViec cv = new ChiTietCongViec
                {
                    MaChiTietCV = Convert.ToInt32(row["maChiTietCV"]),
                    TrangThai = 3 // Gán trạng thái = 3 (trễ hạn)
                };

                UpdateOnlyTrangThaiCongViec(cv);
            }
        }

        public bool IsNguoiLienQuanExists(string maCongViec, int maNguoiDung, string vaiTro)
        {
            string query = @"SELECT COUNT(*) FROM NguoiLienQuanCongViec 
                     WHERE maCongViec = @maCongViec AND maNguoiDung = @maNguoiDung AND vaiTro = @vaiTro";

            SqlParameter[] parameters = {
                new SqlParameter("@maCongViec", maCongViec),
                new SqlParameter("@maNguoiDung", maNguoiDung),
                new SqlParameter("@vaiTro", vaiTro)
            };

            int count = (int)conn.ExecuteScalar(query, parameters);
            return count > 0;
        }

        public bool IsNguoiNhanEmailExists(string maEmail, int maNguoiDung, string vaiTro)
        {
            string query = @"
                SELECT COUNT(*) 
                FROM NguoiNhanEmail 
                WHERE maEmail = @maEmail AND maNguoiDung = @maNguoiDung AND vaiTro = @vaiTro";

            SqlParameter[] parameters = {
                new SqlParameter("@maEmail", maEmail),
                new SqlParameter("@maNguoiDung", maNguoiDung),
                new SqlParameter("@vaiTro", vaiTro)
            };

            int count = (int)conn.ExecuteScalar(query, parameters);
            return count > 0;
        }

        public List<ThongBaoNguoiDung> GetListThongBaoById(int maNguoiDung)
        {
            var danhSach = new List<ThongBaoNguoiDung>();

            string query = @"
            SELECT
                tbn.maThongBao,
                tbn.maNguoiDung,
                tbn.maChiTietCV,
                tbn.noiDung,
                tbn.ngayThongBao,
                tbn.trangThai AS trangThaiThongBao,

                ct.maCongViec,
                ct.noiDung AS noiDungChiTietCV
            FROM ThongBaoNguoiDung tbn
            LEFT JOIN ChiTietCongViec ct ON tbn.maChiTietCV = ct.maChiTietCV
            LEFT JOIN CongViec cv ON ct.maCongViec = cv.maCongViec
            WHERE 
                tbn.maNguoiDung = @maNguoiDung
                AND tbn.ngayThongBao <= GETDATE()
            ORDER BY tbn.ngayThongBao DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@maNguoiDung", maNguoiDung)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                var chiTietCongViec = new ChiTietCongViec
                {
                    MaChiTietCV = Convert.ToInt32(row["maChiTietCV"]),
                    NoiDung = row["noiDungChiTietCV"]?.ToString(),
                    MaCongViec = row["maCongViec"]?.ToString()
                };

                ThongBaoNguoiDung thongBao = new ThongBaoNguoiDung
                {
                    MaThongBao = Convert.ToInt32(row["maThongBao"]),
                    MaNguoiDung = Convert.ToInt32(row["maNguoiDung"]),
                    MaChiTietCV = Convert.ToInt32(row["maChiTietCV"]),
                    NoiDung = row["noiDung"]?.ToString(),
                    NgayThongBao = (DateTime)row["ngayThongBao"],
                    TrangThai = Convert.ToInt32(row["trangThaiThongBao"]),
                    ChiTietCongViec = chiTietCongViec
                };

                danhSach.Add(thongBao);
            }

            return danhSach;
        }

        public List<ThongBaoNguoiDung> GetTop8ThongBaoById(int maNguoiDung)
        {
            var danhSach = new List<ThongBaoNguoiDung>();

            string query = @"
                SELECT TOP 8
                tbn.maThongBao,
                tbn.maNguoiDung,
                tbn.maChiTietCV,
                tbn.noiDung,
                tbn.ngayThongBao,
                tbn.trangThai AS trangThaiThongBao,

                ct.maCongViec,
                ct.noiDung AS noiDungChiTietCV
            FROM ThongBaoNguoiDung tbn
            LEFT JOIN ChiTietCongViec ct ON tbn.maChiTietCV = ct.maChiTietCV
            LEFT JOIN CongViec cv ON ct.maCongViec = cv.maCongViec
            WHERE 
                tbn.maNguoiDung = @maNguoiDung
                AND tbn.ngayThongBao <= GETDATE()
            ORDER BY tbn.ngayThongBao DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@maNguoiDung", maNguoiDung)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                var chiTietCongViec = new ChiTietCongViec
                {
                    MaChiTietCV = Convert.ToInt32(row["maChiTietCV"]),
                    NoiDung = row["noiDungChiTietCV"]?.ToString(),
                    MaCongViec = row["maCongViec"]?.ToString()
                };

                ThongBaoNguoiDung thongBao = new ThongBaoNguoiDung
                {
                    MaThongBao = Convert.ToInt32(row["maThongBao"]),
                    MaNguoiDung = Convert.ToInt32(row["maNguoiDung"]),
                    MaChiTietCV = Convert.ToInt32(row["maChiTietCV"]),
                    NoiDung = row["noiDung"]?.ToString(),
                    NgayThongBao = (DateTime)row["ngayThongBao"],
                    TrangThai = Convert.ToInt32(row["trangThaiThongBao"]),
                    ChiTietCongViec = chiTietCongViec
                };

                danhSach.Add(thongBao);
            }

            return danhSach;
        }

        public bool UpdateTrangThaiThongBao(int maThongBao)
        {
            string query = @"
                UPDATE ThongBaoNguoiDung
                SET trangThai = 1
                WHERE maThongBao = @maThongBao";

            SqlParameter[] parameters = {
                new SqlParameter("@maThongBao", maThongBao),

            };

            int rowsAffected = conn.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }
}
