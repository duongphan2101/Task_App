using System;
using System.Data;
using System.Data.SqlClient;
using TcpServerApp.Model;
using TcpServerApp.database;
using System.Collections.Generic;

namespace TcpServerApp.DAO
{
    public class NguoiDungDAO
    {
        private connectDB conn = new connectDB();

        public NguoiDung GetNguoiDungById(int maNguoiDung)
        {
            string query = @"
                SELECT 
                    nd.maNguoiDung,
                    nd.hoTen,
                    nd.email,
                    nd.matKhau,
                    nd.laLanhDao,
                    nd.maDonVi,
                    nd.maPhongBan,
                    nd.maChucVu,
                    dv.tenDonVi,
                    pb.tenPhongBan,
                    cv.tenChucVu
                FROM NguoiDung nd
                LEFT JOIN DonVi dv ON nd.maDonVi = dv.maDonVi
                LEFT JOIN PhongBan pb ON nd.maPhongBan = pb.maPhongBan
                LEFT JOIN ChucVu cv ON nd.maChucVu = cv.maChucVu
                WHERE nd.maNguoiDung = @maNguoiDung";

            SqlParameter[] parameters = {
                new SqlParameter("@maNguoiDung", maNguoiDung)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                return new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(row["maNguoiDung"]),
                    HoTen = row["hoTen"].ToString(),
                    Email = row["email"].ToString(),
                    MatKhau = row["matKhau"].ToString(),
                    LaLanhDao = row["laLanhDao"] != DBNull.Value ? (bool?)Convert.ToBoolean(row["laLanhDao"]) : null,
                    MaDonVi = row["maDonVi"]?.ToString(),
                    MaPhongBan = row["maPhongBan"]?.ToString(),
                    MaChucVu = row["maChucVu"]?.ToString(),

                    DonVi = new DonVi
                    {
                        MaDonVi = row["maDonVi"]?.ToString(),
                        TenDonVi = row["tenDonVi"]?.ToString()
                    },
                    PhongBan = new PhongBan
                    {
                        MaPhongBan = row["maPhongBan"]?.ToString(),
                        TenPhongBan = row["tenPhongBan"]?.ToString()
                    },
                    ChucVu = new ChucVu
                    {
                        MaChucVu = row["maChucVu"]?.ToString(),
                        TenChucVu = row["tenChucVu"]?.ToString()
                    }
                };
            }

            return null;
        }

        public List<NguoiDung> getDanhSachNguoiDungByDonViVaPhongBan(string maDonVi, string maPhongBan, string currentUserEmail)
        {
            string query = @"
            SELECT u.*, d.tenDonVi, p.tenPhongBan, c.tenChucVu
            FROM NguoiDung u
            LEFT JOIN DonVi d ON u.maDonVi = d.maDonVi
            LEFT JOIN PhongBan p ON u.maPhongBan = p.maPhongBan
            LEFT JOIN ChucVu c ON u.maChucVu = c.maChucVu
            WHERE u.maDonVi = @maDonVi 
              AND u.maPhongBan = @maPhongBan 
              AND u.email <> @currentUserEmail";

            SqlParameter[] parameters = {
                new SqlParameter("@maDonVi", maDonVi),
                new SqlParameter("@maPhongBan", maPhongBan),
                new SqlParameter("@currentUserEmail", currentUserEmail)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            List<NguoiDung> result = new List<NguoiDung>();

            foreach (DataRow row in dt.Rows)
            {
                var nguoiDung = new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(row["maNguoiDung"]),
                    HoTen = row["hoTen"].ToString(),
                    Email = row["email"].ToString(),
                    MatKhau = row["matKhau"].ToString(),
                    LaLanhDao = row["laLanhDao"] != DBNull.Value ? (bool?)Convert.ToBoolean(row["laLanhDao"]) : null,
                    MaDonVi = row["maDonVi"]?.ToString(),
                    MaPhongBan = row["maPhongBan"]?.ToString(),
                    MaChucVu = row["maChucVu"]?.ToString(),

                    DonVi = new DonVi
                    {
                        MaDonVi = row["maDonVi"]?.ToString(),
                        TenDonVi = row["tenDonVi"]?.ToString()
                    },
                    PhongBan = new PhongBan
                    {
                        MaPhongBan = row["maPhongBan"]?.ToString(),
                        TenPhongBan = row["tenPhongBan"]?.ToString()
                    },
                    ChucVu = new ChucVu
                    {
                        MaChucVu = row["maChucVu"]?.ToString(),
                        TenChucVu = row["tenChucVu"]?.ToString()
                    }
                };

                result.Add(nguoiDung);
            }

            return result;
        }

        public string GenerateMaCongViecFromLast(string maDonVi, string maPhongBan)
        {
            string prefix = maDonVi + maPhongBan;

            string query = @"
                SELECT TOP 1 maCongViec 
                FROM CongViec 
                WHERE maCongViec LIKE @prefix + '%'
                ORDER BY maCongViec DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@prefix", prefix)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            int newStt = 1;

            if (dt.Rows.Count > 0)
            {
                string lastMaCV = dt.Rows[0]["maCongViec"].ToString();

                string[] parts = lastMaCV.Split('_');
                if (parts.Length == 2 && int.TryParse(parts[1], out int lastStt))
                {
                    newStt = lastStt + 1;
                }
            }

            string maCongViec;
            do
            {
                maCongViec = $"{prefix}_{newStt}";
                string checkQuery = "SELECT COUNT(*) FROM CongViec WHERE maCongViec = @ma";
                SqlParameter[] checkParams = {
            new SqlParameter("@ma", maCongViec)
        };
                int count = (int)conn.ExecuteScalar(checkQuery, checkParams);
                if (count > 0)
                    newStt++; // Nếu trùng thì tăng tiếp
                else
                    break;    // Nếu không trùng thì dùng mã này
            }
            while (true);

            return maCongViec;
        }

        public NguoiDung GetNguoiDungByEmail(string email)
        {
            string query = @"
                SELECT 
                    nd.maNguoiDung,
                    nd.hoTen,
                    nd.email,
                    nd.matKhau,
                    nd.laLanhDao,
                    nd.maDonVi,
                    nd.maPhongBan,
                    nd.maChucVu,
                    dv.tenDonVi,
                    pb.tenPhongBan,
                    cv.tenChucVu
                FROM NguoiDung nd
                LEFT JOIN DonVi dv ON nd.maDonVi = dv.maDonVi
                LEFT JOIN PhongBan pb ON nd.maPhongBan = pb.maPhongBan
                LEFT JOIN ChucVu cv ON nd.maChucVu = cv.maChucVu
                WHERE nd.email = @email";

            SqlParameter[] parameters = {
                new SqlParameter("@email", email)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                return new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(row["maNguoiDung"]),
                    HoTen = row["hoTen"].ToString(),
                    Email = row["email"].ToString(),
                    MatKhau = row["matKhau"].ToString(),
                    LaLanhDao = row["laLanhDao"] != DBNull.Value ? (bool?)Convert.ToBoolean(row["laLanhDao"]) : null,
                    MaDonVi = row["maDonVi"]?.ToString(),
                    MaPhongBan = row["maPhongBan"]?.ToString(),
                    MaChucVu = row["maChucVu"]?.ToString(),

                    DonVi = new DonVi
                    {
                        MaDonVi = row["maDonVi"]?.ToString(),
                        TenDonVi = row["tenDonVi"]?.ToString()
                    },
                    PhongBan = new PhongBan
                    {
                        MaPhongBan = row["maPhongBan"]?.ToString(),
                        TenPhongBan = row["tenPhongBan"]?.ToString()
                    },
                    ChucVu = new ChucVu
                    {
                        MaChucVu = row["maChucVu"]?.ToString(),
                        TenChucVu = row["tenChucVu"]?.ToString()
                    }
                };
            }

            return null;
        }

        public List<NguoiDung> getDanhSachNguoiDungByEmails(List<string> emails)
        {
            if (emails == null || emails.Count == 0)
                return new List<NguoiDung>();

            List<string> paramNames = new List<string>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            for (int i = 0; i < emails.Count; i++)
            {
                string paramName = "@email" + i;
                paramNames.Add(paramName);
                parameters.Add(new SqlParameter(paramName, emails[i]));
            }

            string query = $@"
                SELECT u.*, d.tenDonVi, p.tenPhongBan, c.tenChucVu
                FROM NguoiDung u
                LEFT JOIN DonVi d ON u.maDonVi = d.maDonVi
                LEFT JOIN PhongBan p ON u.maPhongBan = p.maPhongBan
                LEFT JOIN ChucVu c ON u.maChucVu = c.maChucVu
                WHERE u.email IN ({string.Join(",", paramNames)})";

            DataTable dt = conn.ExecuteQuery(query, parameters.ToArray());

            List<NguoiDung> result = new List<NguoiDung>();

            foreach (DataRow row in dt.Rows)
            {
                var nguoiDung = new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(row["maNguoiDung"]),
                    HoTen = row["hoTen"].ToString(),
                    Email = row["email"].ToString(),
                    MatKhau = row["matKhau"].ToString(),
                    LaLanhDao = row["laLanhDao"] != DBNull.Value ? (bool?)Convert.ToBoolean(row["laLanhDao"]) : null,
                    MaDonVi = row["maDonVi"]?.ToString(),
                    MaPhongBan = row["maPhongBan"]?.ToString(),
                    MaChucVu = row["maChucVu"]?.ToString(),

                    DonVi = new DonVi
                    {
                        MaDonVi = row["maDonVi"]?.ToString(),
                        TenDonVi = row["tenDonVi"]?.ToString()
                    },
                    PhongBan = new PhongBan
                    {
                        MaPhongBan = row["maPhongBan"]?.ToString(),
                        TenPhongBan = row["tenPhongBan"]?.ToString()
                    },
                    ChucVu = new ChucVu
                    {
                        MaChucVu = row["maChucVu"]?.ToString(),
                        TenChucVu = row["tenChucVu"]?.ToString()
                    }
                };

                result.Add(nguoiDung);
            }

            return result;
        }

        public string GenerateMaEmailFromLast(string maCongViec)
        {
            string timePart = DateTime.Now.ToString("yyyyMMddHHmmss");
            string randomPart = Guid.NewGuid().ToString("N").Substring(0, 6);
            return $"{maCongViec}_{timePart}_{randomPart}";
        }


    }
}
