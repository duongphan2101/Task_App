using System;
using System.Data;
using System.Data.SqlClient;
using TcpServerApp.Properties;

namespace TcpServerApp.database
{
    public class connectDB
    {
        private readonly string connectionString = Settings.Default.DbConnString;

        public connectDB()
        {
            TestConnection();
        }

        private void TestConnection()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("✅ Kết nối CSDL thành công!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Kết nối CSDL thất bại: " + ex.Message);
                    throw;
                }
            }
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Lỗi truy vấn SQL: " + ex.Message);
                    throw;
                }
            }

            return dt;
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        return command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Lỗi ExecuteNonQuery: " + ex.Message);
                    throw;
                }
            }
        }

        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection sqlConn = GetConnection())
            {
                try
                {
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        return cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Lỗi ExecuteScalar: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
