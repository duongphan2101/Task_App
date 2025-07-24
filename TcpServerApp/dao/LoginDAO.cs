using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpServerApp.database;

namespace TcpServerApp.DAO
{
    public class LoginDAO
    {
        connectDB conn = new connectDB();
        public int CheckLogin(string email, string password)
        {
            string query = @"SELECT maNguoiDung FROM NguoiDung WHERE email = @email AND matKhau = @matKhau";

            SqlParameter[] parameters = {
                new SqlParameter("@email", email),
                new SqlParameter("@matKhau", password)
            };

            DataTable dt = conn.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["maNguoiDung"]);
            }

            return -1;
        }


    }
}
