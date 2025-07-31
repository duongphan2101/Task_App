using DotNetEnv;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Task_App.DTO;
using Task_App.Model;

namespace Task_App.TaskApp_Dao
{
    public class TcpClientDAO
    {
        private TcpClient client;
        private NetworkStream stream;

        public bool IsConnected => client != null && client.Connected;

        public bool Connect(string host = "127.0.0.1", int port = 5000)
        {
            try
            {
                client = new TcpClient(host, port);
                stream = client.GetStream();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                stream?.Close();
                client?.Close();
            }
        }

        public string SendAndReceive(string command, object data = null)
        {
            if (!IsConnected)
                throw new InvalidOperationException("Chưa kết nối tới server");

            var payload = new RequestWrapper
            {
                Command = command,
                Data = data
            };

            string json = JsonConvert.SerializeObject(payload);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
            byte[] lengthPrefix = BitConverter.GetBytes(jsonBytes.Length); // 4 bytes độ dài

            // Gửi độ dài trước
            stream.Write(lengthPrefix, 0, lengthPrefix.Length);
            // Gửi nội dung JSON sau
            stream.Write(jsonBytes, 0, jsonBytes.Length);

            // Nhận độ dài trước
            byte[] lenBuffer = new byte[4];
            stream.Read(lenBuffer, 0, 4);
            int length = BitConverter.ToInt32(lenBuffer, 0);

            // Đọc dữ liệu theo đúng độ dài
            byte[] buffer = new byte[length];
            int totalRead = 0;
            while (totalRead < length)
            {
                int read = stream.Read(buffer, totalRead, length - totalRead);
                if (read == 0) break;
                totalRead += read;
            }

            return Encoding.UTF8.GetString(buffer).Trim();
        }

        public DataTable ConvertToDataTable<T>(List<T> items)
        {
            var table = new DataTable(typeof(T).Name);
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in items)
            {
                var row = table.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }

        public NguoiDung GetNguoiDung(int maNguoiDung)
        {
            string response = SendAndReceive("nguoiDung", maNguoiDung);

            try
            {
                return JsonConvert.DeserializeObject<NguoiDung>(response);
            }
            catch
            {
                return null;
            }
        }

        public int CheckLogin(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            string response = SendAndReceive("checkLogin", loginData);
            if (int.TryParse(response, out int maNguoiDung) && maNguoiDung != -1)
            {
                return maNguoiDung;
            }

            return -1;
        }

        public List<NguoiDung> getDanhSachNguoiDungByDonViVaPhongBan(string maDonVi, string maPhongBan, string currentUserEmail)
        {
            var requestData = new
            {
                MaDonVi = maDonVi,
                MaPhongBan = maPhongBan,
                CurrentUserEmail = currentUserEmail
            };
            string response = SendAndReceive("getDanhSachNguoiDungByDonViVaPhongBan", requestData);
            try
            {
                return JsonConvert.DeserializeObject<List<NguoiDung>>(response);
            }
            catch
            {
                return new List<NguoiDung>();
            }
        }

        public string GenerateMaCongViecFromLast(string maDonVi, string maPhongBan)
        {
            var requestData = new
            {
                MaDonVi = maDonVi,
                MaPhongBan = maPhongBan
            };

            string response = SendAndReceive("generateMaCongViecFromLast", requestData);

            try
            {
                var result = JsonConvert.DeserializeObject<dynamic>(response);
                return result.MaCongViec;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xử lý response generateMaCongViecFromLast: " + ex.Message);
                return null;
            }
        }

        public NguoiDung GetNguoiDungByEmail(string email)
        {
            var requestData = new
            {
                Email = email
            };
            string response = SendAndReceive("getNguoiDungByEmail", requestData);
            try
            {
                return JsonConvert.DeserializeObject<NguoiDung>(response);
            }
            catch
            {
                return null;
            }
        }

        public List<NguoiDung> getDanhSachNguoiDungByEmails(List<string> emails)
        {
            var requestData = new
            {
                Emails = emails
            };
            string response = SendAndReceive("getDanhSachNguoiDungByEmails", requestData);
            try
            {
                return JsonConvert.DeserializeObject<List<NguoiDung>>(response);
            }
            catch
            {
                return new List<NguoiDung>();
            }
        }

        public string GenerateMaEmailFromLast(string maCongViec)
        {
            var requestData = new
            {
                MaCongViec = maCongViec
            };
            string response = SendAndReceive("generateMaEmailFromLast", requestData);
            return response;
        }

        public List<ThongBaoNguoiDung> GetTop8ThongBaoById(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung
            };
            string response = SendAndReceive("getTop8ThongBaoById", requestData);
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };

                return JsonConvert.DeserializeObject<List<ThongBaoNguoiDung>>(response, settings);
            }
            catch
            {
                return new List<ThongBaoNguoiDung>();
            }
        }

        public bool UpdateTrangThaiThongBao(int maThongBao)
        {
            var requestData = new
            {
                MaThongBao = maThongBao
            };
            string response = SendAndReceive("updateTrangThaiThongBao", requestData);
            return response.Equals("true", StringComparison.OrdinalIgnoreCase);
        }
        
        public DataTable GetCongViecDaGiaoByUserId(int id)
        {
            var requestData = new
            {
                MaNguoiDung = id
            };

            string response = SendAndReceive("getCongViecDaGiaoByUserId", requestData);

            try
            {
                var congViecs = JsonConvert.DeserializeObject<List<CongViecDaGiaoDTO>>(response);
                return ConvertToDataTable(congViecs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize công việc: " + ex.Message);
                return new DataTable();
            }
        }

        public DataTable GetCongViecDaGiaoByUserId_SortByTrangThai(int id)
        {
            var requestData = new
            {
                MaNguoiDung = id
            };
            string response = SendAndReceive("getCongViecDaGiaoByUserId_SortByTrangThai", requestData);
            try
            {
                var congViecs = JsonConvert.DeserializeObject<List<CongViecDaGiaoDTO>>(response);
                return ConvertToDataTable(congViecs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize công việc: " + ex.Message);
                return new DataTable();
            }
        }

        public DataTable GetCongViecDuocGiaoByUserId(int id)
        {
            var requestData = new
            {
                MaNguoiDung = id
            };
            string response = SendAndReceive("GetCongViecDuocGiaoByUserId", requestData);
            try
            {
                var congViecs = JsonConvert.DeserializeObject<List<CongViecDuocGiaoDTO>>(response);
                return ConvertToDataTable(congViecs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize công việc: " + ex.Message);
                return new DataTable();
            }
        }

        public DataTable GetCongViecDuocGiao_SortByTrangThai(int id)
        {
            var requestData = new
            {
                MaNguoiDung = id
            };
            string response = SendAndReceive("GetCongViecDuocGiao_SortByTrangThai", requestData);
            try
            {
                var congViecs = JsonConvert.DeserializeObject<List<CongViecDuocGiaoDTO>>(response);
                return ConvertToDataTable(congViecs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize công việc: " + ex.Message);
                return new DataTable();
            }
        }

        public DataTable GetCongViecByIdCongViec(int maChiTietCV)
        {
            var requestData = new
            {
                MaChiTietCV = maChiTietCV
            };
            string response = SendAndReceive("GetCongViecByIdCongViec", requestData);
            try
            {
                var congViecs = JsonConvert.DeserializeObject<List<CongViecChiTietDTO>>(response);
                return ConvertToDataTable(congViecs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize công việc: " + ex.Message);
                return new DataTable();
            }
        }
        
        public CongViec getCongViecById(string maCongViec)
        {
            var requestData = new
            {
                MaCongViec = maCongViec
            };
            string response = SendAndReceive("getCongViecById", requestData);
            try
            {
                return JsonConvert.DeserializeObject<CongViec>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize công việc: " + ex.Message);
                return null;
            }
        }

        public ChiTietCongViec getChiTietCongViecById(int maCTCV)
        {
            var requestData = new
            {
                MaChiTietCV = maCTCV
            };
            string response = SendAndReceive("getChiTietCongViecById", requestData);
            try
            {
                return JsonConvert.DeserializeObject<ChiTietCongViec>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize chi tiết công việc: " + ex.Message);
                return null;
            }
        }
        
        public DataTable GetDanhSachNguoiLienQuanByIdCongViec(string maCongViec)
        {
            var requestData = new
            {
                MaCongViec = maCongViec
            }; 
            string response = SendAndReceive("GetDanhSachNguoiLienQuanByIdCongViec", requestData);
            try
            {
                var nguoiLienQuan = JsonConvert.DeserializeObject<List<NguoiLienQuanDTO>>(response);
                return ConvertToDataTable(nguoiLienQuan);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize danh sách người liên quan: " + ex.Message);
                return new DataTable();
            }
        }

        public List<NguoiLienQuanCongViec> GetListNguoiLienQuanByIdCongViec(string maCongViec)
        {
            var requestData = new
            {
                MaCongViec = maCongViec
            };
            string response = SendAndReceive("GetListNguoiLienQuanByIdCongViec", requestData);
            try
            {
                return JsonConvert.DeserializeObject<List<NguoiLienQuanCongViec>>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize danh sách người liên quan: " + ex.Message);
                return new List<NguoiLienQuanCongViec>();
            }
        }

        public bool sendEmail(Email em, List<NguoiNhanEmail> lstNguoiNhanEmail, List<TepDinhKemEmail> lstFile, NguoiDung u)
        {
            var requestData = new
            {
                Email = em,
                NguoiNhanEmail = lstNguoiNhanEmail,
                TepDinhKemEmail = lstFile,
                NguoiDung = u
            };
            string response = SendAndReceive("sendEmail", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi gửi email: " + response);
                return false;
            }
        }

        public bool createCongViec(CongViec cv)
        {
            var requestData = new
            {
                CongViec = cv
            };
            string response = SendAndReceive("createCongViec", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi tạo công việc: " + response);
                return false;
            }
        }

        public int CreateChiTietCongViec(ChiTietCongViec ct)
        {
            var requestData = new
            {
                ChiTietCongViec = ct
            };
            string response = SendAndReceive("createChiTietCongViec", requestData);
            if (int.TryParse(response, out int maChiTietCongViec) && maChiTietCongViec > 0)
            {
                return maChiTietCongViec;
            }
            else
            {
                Console.WriteLine("Lỗi khi tạo chi tiết công việc: " + response);
                return -1;
            }
        }

        public List<ChiTietCongViec> TaoChiTietCongViecTheoTanSuat(CongViec congViec, int soNgayHoanThanh)
        {
            var requestData = new
            {
                CongViec = congViec,
                SoNgayHoanThanh = soNgayHoanThanh
            };
            string response = SendAndReceive("TaoChiTietCongViecTheoTanSuat", requestData);
            try
            {
                return JsonConvert.DeserializeObject<List<ChiTietCongViec>>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tạo chi tiết công việc theo tần suất: " + ex.Message);
                return new List<ChiTietCongViec>();
            }
        }

        public bool CreateNguoiLienQuan(NguoiLienQuanCongViec n)
        {
            var requestData = new
            {
                NguoiLienQuanCongViec = n
            };
            string response = SendAndReceive("createNguoiLienQuan", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi tạo người liên quan: " + response);
                return false;
            }
        }

        public bool CreateEmail(Email e)
        {
            var requestData = new
            {
                Email = e
            };
            string response = SendAndReceive("createEmail", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi tạo email: " + response);
                return false;
            }
        }

        public bool CreateNguoiNhanEmail(NguoiNhanEmail n)
        {
            var requestData = new
            {
                NguoiNhanEmail = n
            };
            string response = SendAndReceive("createNguoiNhanEmail", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi tạo người nhận email: " + response);
                return false;
            }
        }

        public int CreateTepTin(TepTin t)
        {
            var requestData = new
            {
                TepTin = t
            };
            string response = SendAndReceive("createTepTin", requestData);
            if (int.TryParse(response, out int maTepTin) && maTepTin > 0)
            {
                return maTepTin;
            }
            else
            {
                Console.WriteLine("Lỗi khi tạo tập tin: " + response);
                return -1;
            }
        }

        public bool CreateTepTinDinhKem(TepDinhKemEmail tdk)
        {
            var requestData = new
            {
                TepDinhKemEmail = tdk
            };
            string response = SendAndReceive("createTepTinDinhKem", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi tạo tập tin đính kèm: " + response);
                return false;
            }
        }

        public bool createPhanHoiCongViec(PhanHoiCongViec ph)
        {
            var requestData = new
            {
                PhanHoiCongViec = ph
            };
            string response = SendAndReceive("createPhanHoiCongViec", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi tạo phản hồi công việc: " + response);
                return false;
            }
        }

        public bool CreateThongBao(ThongBaoNguoiDung tb)
        {
            var requestData = new
            {
                ThongBaoNguoiDung = tb
            };
            string response = SendAndReceive("createThongBao", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi tạo thông báo: " + response);
                return false;
            }
        }

        public bool UpdateTrangThaiCongViec(ChiTietCongViec ct)
        {
            var requestData = new
            {
                ChiTietCongViec = ct
            };
            string response = SendAndReceive("updateTrangThaiCongViec", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái công việc: " + response);
                return false;
            }
        }

        public bool UpdateOnlyTrangThaiCongViec(ChiTietCongViec ct)
        {
            var requestData = new
            {
                ChiTietCongViec = ct
            };
            string response = SendAndReceive("updateOnlyTrangThaiCongViec", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái công việc: " + response);
                return false;
            }
        }

        public bool UpdateHanHoanThanhTask(ChiTietCongViec ct)
        {
            var requestData = new
            {
                ChiTietCongViec = ct
            };
            string response = SendAndReceive("updateHanHoanThanhTask", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi cập nhật hạn hoàn thành công việc: " + response);
                return false;
            }
        }

        public bool UpdateTrangThaiEmail(Email e)
        {
            var requestData = new
            {
                Email = e
            };
            string response = SendAndReceive("updateTrangThaiEmail", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái email: " + response);
                return false;
            }
        }

        public bool UpdateTienDoCongViec(ChiTietCongViec ct)
        {
            var requestData = new
            {
                ChiTietCongViec = ct
            };
            string response = SendAndReceive("updateTienDoCongViec", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi cập nhật tiến độ công việc: " + response);
                return false;
            }
        }

        public List<TepDinhKemEmail> GetTepDinhKemEmailByMaCongViec(string maCongViec)
        {
            var requestData = new
            {
                MaCongViec = maCongViec
            };
            string response = SendAndReceive("GetTepDinhKemEmailByMaCongViec", requestData);
            try
            {
                return JsonConvert.DeserializeObject<List<TepDinhKemEmail>>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy tập đính kèm email: " + ex.Message);
                return new List<TepDinhKemEmail>();
            }
        }

        public List<PhanHoiCongViec> GetFeedbacksByMaCongViec(string maCongViec)
        {
            var requestData = new
            {
                MaCongViec = maCongViec
            };
            string response = SendAndReceive("GetFeedbacksByMaCongViec", requestData);
            try
            {
                return JsonConvert.DeserializeObject<List<PhanHoiCongViec>>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy phản hồi công việc: " + ex.Message);
                return new List<PhanHoiCongViec>();
            }
        }

        public List<NguoiNhanEmail> GetListNguoiNhanEmail(string maEmail)
        {
            var requestData = new
            {
                MaEmail = maEmail
            };
            string response = SendAndReceive("GetListNguoiNhanEmail", requestData);
            try
            {
                return JsonConvert.DeserializeObject<List<NguoiNhanEmail>>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách người nhận email: " + ex.Message);
                return new List<NguoiNhanEmail>();
            }
        }

        public bool IsNguoiLienQuanExists(string maCongViec, int maNguoiDung, string vaiTro)
        {
            var requestData = new
            {
                MaCongViec = maCongViec,
                MaNguoiDung = maNguoiDung,
                VaiTro = vaiTro
            };
            string response = SendAndReceive("IsNguoiLienQuanExists", requestData);
            return response.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsNguoiNhanEmailExists(string maEmail, int maNguoiDung, string vaiTro)
        {
            var requestData = new
            {
                MaEmail = maEmail,
                MaNguoiDung = maNguoiDung,
                VaiTro = vaiTro
            };
            string response = SendAndReceive("IsNguoiNhanEmailExists", requestData);
            return response.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        public List<ThongBaoNguoiDung> GetListThongBaoById(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung
            };
            string response = SendAndReceive("GetListThongBaoById", requestData);
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                return JsonConvert.DeserializeObject<List<ThongBaoNguoiDung>>(response, settings);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách thông báo: " + ex.Message);
                return new List<ThongBaoNguoiDung>();
            }
        }

        public int SoTaskTrongTuan(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung
            };
            string response = SendAndReceive("getsotasktrongtuan", requestData);
            if (int.TryParse(response, out int soTaskTrongTuan) && soTaskTrongTuan >= 0)
            {
                return soTaskTrongTuan;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task trong tuần: " + response);
                return -1;
            }
        }

        public int SoTaskTrongThang(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
            };
            string response = SendAndReceive("getsotasktrongthang", requestData);
            if (int.TryParse(response, out int soTaskTrongThang) && soTaskTrongThang >= 0)
            {
                return soTaskTrongThang;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task trong tháng: " + response);
                return -1;
            }
        }

        public int SoTaskTrongNam(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
            };
            string response = SendAndReceive("getsotasktrongnam", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task trong năm: " + response);
                return -1;
            }
        }

        public int SoTaskChuaXuLi(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
            };
            string response = SendAndReceive("getsotaskchuaxuly", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task chua xu li: " + response);
                return -1;
            }
        }

        public int SoTaskdangXuLi(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung
            };
            string response = SendAndReceive("getsotaskdangxuly", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task dang xu li: " + response);
                return -1;
            }
        }

        public int SoTaskDaHoanThanh(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung
            };
            string response = SendAndReceive("getsotaskdahoanthanh", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task da hoan thanh: " + response);
                return -1;
            }
        }

        public int SoTaskTreHan(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung
            };
            string response = SendAndReceive("getsotaskdatre", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task tre han: " + response);
                return -1;
            }
        }

        public DataTable getNguoiGiao(int id)
        {
            var requestData = new
            {
                MaNguoiDung = id
            };

            string response = SendAndReceive("getnguoigiao", requestData);

            try
            {
                var nguoiGiaos = JsonConvert.DeserializeObject <List<NguoiGiaoDTO>>(response);
                return ConvertToDataTable(nguoiGiaos);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi deserialize công việc: " + ex.Message);
                return new DataTable();
            }
        }

        public int SoTaskChuaXuLiByFilter(int maNguoiDung, DateTime? start, DateTime? end)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
                NgayBatDau = start,
                NgayKetThuc = end
            };
            string response = SendAndReceive("getsotaskchuxulibyfilter", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task chua xu li by filter: " + response);
                return -1;
            }
        }

        public int SoTaskDangXuLiByFilter(int maNguoiDung, DateTime? start, DateTime? end)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
                NgayBatDau = start,
                NgayKetThuc = end
            };
            string response = SendAndReceive("getsotaskdangxulibyfilter", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task dang xu li by filter: " + response);
                return -1;
            }
        }

        public int SoTaskDaHoanThanhByFilter(int maNguoiDung, DateTime? start, DateTime? end)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
                NgayBatDau = start,
                NgayKetThuc = end
            };
            string response = SendAndReceive("getsotaskdahoanthanhbyfilter", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task da hoan thanh by filter: " + response);
                return -1;
            }
        }

        public int SoTaskTreHanByFilter(int maNguoiDung, DateTime? start, DateTime? end)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
                NgayBatDau = start,
                NgayKetThuc = end
            };
            string response = SendAndReceive("getsotasktrehanbyfilter", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task tre han by filter: " + response);
                return -1;
            }
        }

        public int SoTaskDaGiaoTrongTuan(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung
            };
            string response = SendAndReceive("getsotaskdagiaotrongtuan", requestData);
            if (int.TryParse(response, out int soTaskTrongTuan) && soTaskTrongTuan >= 0)
            {
                return soTaskTrongTuan;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task da giao trong tuần: " + response);
                return -1;
            }
        }

        public int SoTaskDaGiaoTrongThang(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
            };
            string response = SendAndReceive("getsotaskdagiaotrongthang", requestData);
            if (int.TryParse(response, out int soTaskTrongThang) && soTaskTrongThang >= 0)
            {
                return soTaskTrongThang;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task da giao trong tháng: " + response);
                return -1;
            }
        }

        public int SoTaskDaGiaoTrongNam(int maNguoiDung)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
            };
            string response = SendAndReceive("getsotaskdagiaotrongnam", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task da giao trong năm: " + response);
                return -1;
            }
        }

        public int SoTaskDaGiaoChuaXuLiByFilter(int maNguoiDung, DateTime? start, DateTime? end)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
                NgayBatDau = start,
                NgayKetThuc = end
            };
            string response = SendAndReceive("getsotaskdagiaochuaxulibyfilter", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task chua xu li by filter: " + response);
                return -1;
            }
        }

        public int SoTaskDaGiaoDangXuLiByFilter(int maNguoiDung, DateTime? start, DateTime? end)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
                NgayBatDau = start,
                NgayKetThuc = end
            };
            string response = SendAndReceive("getsotaskdagiaodangxulibyfilter", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task dang xu li by filter: " + response);
                return -1;
            }
        }

        public int SoTaskDaGiaoHoanThanhByFilter(int maNguoiDung, DateTime? start, DateTime? end)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
                NgayBatDau = start,
                NgayKetThuc = end
            };
            string response = SendAndReceive("getsotaskdagiaohoanthanhbyfilter", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task da hoan thanh by filter: " + response);
                return -1;
            }
        }

        public int SoTaskDaGiaoTreHanByFilter(int maNguoiDung, DateTime? start, DateTime? end)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
                NgayBatDau = start,
                NgayKetThuc = end
            };
            string response = SendAndReceive("getsotaskdagiaotrehanbyfilter", requestData);
            if (int.TryParse(response, out int soTaskTrongNam) && soTaskTrongNam >= 0)
            {
                return soTaskTrongNam;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy số task tre han by filter: " + response);
                return -1;
            }
        }

        public bool getIsGiaoViec(int maNguoiDung, int maCTCV)
        {
            var requestData = new
            {
                MaNguoiDung = maNguoiDung,
                MaCTCV = maCTCV
            };
            string response = SendAndReceive("getisgiaoviec", requestData);
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Lỗi khi getIsGiaoViec: " + response);
                return false;
            }
        }

        public string getMaCongViec(int maCTCV)
        {
            var requestData = new
            {
                MaCTCV = maCTCV
            };
            string response = SendAndReceive("getmacongviec", requestData);
            if (!string.IsNullOrEmpty(response))
            {
                return response;
            }
            else
            {
                Console.WriteLine("Lỗi khi lấy mã công việc: " + response);
                return null;
            }
        }



    }
}
