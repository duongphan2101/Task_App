using DevExpress.XtraPrinting.Native.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task_App.DTO;
using Task_App.Model;
using Task_App.Response;

namespace Task_App.TaskApp_Dao
{
    public class ApiClientDAO
    {
        private readonly HttpClient client;

        public ApiClientDAO()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5036/");
        }

        public async Task<LoginResponse> CheckLoginAsync(string email, string password)
        {
            var loginData = new { email = email, matKhau = password };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("api/auth/login", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);

                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                    {
                        // Lưu token nếu cần
                        return loginResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối đến server API: " + ex.Message);
            }

            return null;
        }

        public async Task<ApiResponse> Register(NguoiDung nd)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(nd),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("api/auth/register", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var apiResp = JsonConvert.DeserializeObject<ApiResponse>(result);
                    return apiResp ?? new ApiResponse
                    {
                        Success = false,
                        Message = "Server trả về dữ liệu rỗng."
                    };
                }

                return new ApiResponse
                {
                    Success = false,
                    Message = $"Đăng ký thất bại ({(int)response.StatusCode}): {result}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi kết nối đến server API: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse> UpdatePassAcount(string email, string password)
        {
            try
            {
                var loginData = new { email = email, matKhau = password };

                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(loginData),
                    Encoding.UTF8,
                    "application/json"
                );

                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                    var response = await client.PostAsync("api/auth/update-mat-khau-nguoi-dung", jsonContent);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<ApiResponse>(result);
                    }

                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Cập nhật Pass thất bại ({(int)response.StatusCode}): {result}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi cập nhật Pass: " + ex.Message
                };
            }
        }

        public async Task<ViecDaGiaoResponse> GetViecDaGiaoAsync(int maNguoiDung, bool locTheoNgay)
        {
            try
            {
                var response = await client.GetAsync($"api/task/da-giao/{maNguoiDung}?sortByDate={locTheoNgay}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ViecDaGiaoResponse>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy công việc đã giao: " + ex.Message);
            }
            return null;
        }

        public async Task<ViecDuocGiaoResponse> GetViecDuocGiaoAsync(int maNguoiDung, bool locTheoNgay)
        {
            try
            {
                var response = await client.GetAsync($"api/task/duoc-giao/{maNguoiDung}?sortByDate={locTheoNgay}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ViecDuocGiaoResponse>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy công việc được giao: " + ex.Message);
            }
            return null;
        }

        public async Task<ChiTietCongViecResponse> GetChiTietConViecAsync(int maChiTietCV)
        {
            try
            {
                var response = await client.GetAsync($"api/task/chi-tiet-cong-viec/{maChiTietCV}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ChiTietCongViecResponse>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy chi tiết công việc: " + ex.Message);
            }
            return null;
        }

        public async Task<Object_Response<ChiTietCongViec>> GetChiTietConViec(int maChiTietCV)
        {
            try
            {
                var response = await client.GetAsync($"api/task/chitiet-congviec/{maChiTietCV}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Object_Response<ChiTietCongViec>>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy chi tiết công việc: " + ex.Message);
            }
            return null;
        }
        
        public async Task<NguoiLienQuanCongViecResponse> GetNguoiLienQuanCongViecAsync(string maCongViec)
        {
            try
            {
                var response = await client.GetAsync($"api/task/nguoi-lien-quan/{maCongViec}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<NguoiLienQuanCongViecResponse>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy người liên quan công việc: " + ex.Message);
            }
            return null;
        }

        public async Task<ApiResponse> UpdateTrangThaiCongViecAsync(ChiTietCongViec updateData)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(updateData),
                    Encoding.UTF8,
                    "application/json"
                );

                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                    var response = await client.PostAsync("api/task/update-trang-thai", jsonContent);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<ApiResponse>(result);
                    }

                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Cập nhật thất bại ({(int)response.StatusCode}): {result}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi cập nhật trạng thái công việc: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse> UpdateTrangThaiEmail(Email e)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(e),
                    Encoding.UTF8,
                    "application/json"
                );

                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                    var response = await client.PostAsync("api/task/update-trang-thai-email", jsonContent);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<ApiResponse>(result);
                    }

                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Cập nhật email thất bại ({(int)response.StatusCode}): {result}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi cập nhật trạng thái email: " + ex.Message
                };
            }
        }
        
        public async Task<ApiResponse> UpdateTrangThaiThongBao111(int tb)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(tb),
                    Encoding.UTF8,
                    "application/json"
                );

                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                    var response = await client.PostAsync("api/task/update-trang-thai-thong-bao-111", jsonContent);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<ApiResponse>(result);
                    }

                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Cập nhật trang thai thong bao thất bại ({(int)response.StatusCode}): {result}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi cập nhật trạng thái thongBao: " + ex.Message
                };
            }
        }

        public async Task<TepTinResponse> GetTepTinByAsync(int maTep)
        {
            try
            {
                var response = await client.GetAsync($"api/task/tep/{maTep}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TepTinResponse>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy Tep Tin: " + ex.Message);
            }
            return null;
        }

        public async Task<Object_Response<NguoiDung>> GetGetNguoiDungByIdAsync(int id)
        {
            try
            {
                var response = await client.GetAsync($"api/task/get-nguoi-dung/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Object_Response<NguoiDung>>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy NguoiDung: " + ex.Message);
            }
            return null;
        }

        public async Task<TepDinhKemEmailResponse> GetTepDinhKemEmailByMaCongViecAsync(string maCongViec)
        {
            try
            {
                var response = await client.GetAsync($"api/task/tep-dinh-kem-email/{maCongViec}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TepDinhKemEmailResponse>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy Tep Dinh Kem Email: " + ex.Message);
            }
            return null;
        }

        public async Task<PhanHoiCongViecResponse> GetPhanHoiCongViecAsync(string maCongViec)
        {
            try
            {
                var response = await client.GetAsync($"api/task/phan-hoi-cong-viec/{maCongViec}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PhanHoiCongViecResponse>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy phản hồi công việc: " + ex.Message);
            }
            return null;
        }

        public async Task<Object_Response<string>> CreateCongViec(CongViec cv, NguoiDung nd)
        {
            try
            {
                var payload = new
                {
                    CongViec = cv,
                    NguoiDung = nd
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/tao-cong-viec", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<string>>(result);
                }

                return new Object_Response<string>
                {
                    Success = false,
                    Message = $"Tạo công việc thất bại ({(int)response.StatusCode}): {result}",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<string>
                {
                    Success = false,
                    Message = "Lỗi khi tạo công việc: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<Object_Response<int>> CreateChiTietCongViec(ChiTietCongViec cv)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(cv),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/tao-chi-tiet-cong-viec", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"Tạo chi tiết công việc thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi tạo chi tiết công việc: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<ApiResponse> CreateNguoiLienQuanCongViec(NguoiLienQuanCongViec cv)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(cv),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/tao-nguoi-lien-quan-cong-viec", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                return new ApiResponse
                {
                    Success = false,
                    Message = $"Tạo NLQ công việc thất bại ({(int)response.StatusCode}): {result}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi tạo NLQ công việc: " + ex.Message
                };
            }
        }

        public async Task<Object_Response<int>> CreateTepTin(TepTin cv)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(cv),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/tao-tep-tin", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"Tạo TepTin thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi tạo TepTin: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<ApiResponse> CreateEmail(Email cv)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(cv),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/tao-email", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                return new ApiResponse
                {
                    Success = false,
                    Message = $"Tạo Email thất bại ({(int)response.StatusCode}): {result}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi tạo Email: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse> CreateNguoiNhanEmail(NguoiNhanEmail cv)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(cv),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/tao-nguoi-nhan-email", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                return new ApiResponse
                {
                    Success = false,
                    Message = $"Tạo NNEmail thất bại ({(int)response.StatusCode}): {result}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi tạo NNEmail: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse> CreateTepDinhKem(TepDinhKemEmail cv)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(cv),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/tao-tep-dinh-kem", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                return new ApiResponse
                {
                    Success = false,
                    Message = $"Tạo TepDinhKem thất bại ({(int)response.StatusCode}): {result}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi tạo TepDinhKem: " + ex.Message
                };
            }
        }
        
        public async Task<ApiResponse> CreateThongBao(ThongBaoNguoiDung cv)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(cv),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/tao-thong-bao", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                return new ApiResponse
                {
                    Success = false,
                    Message = $"Tạo ThongBao thất bại ({(int)response.StatusCode}): {result}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi tạo ThongBao: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse> CreatePhanHoiCongViec(PhanHoiCongViec cv)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(cv),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/tao-phan-hoi-cong-viec", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                return new ApiResponse
                {
                    Success = false,
                    Message = $"Tạo PhanHoiCongViec thất bại ({(int)response.StatusCode}): {result}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi tạo PhanHoiCongViec: " + ex.Message
                };
            }
        }

        public async Task<Object_Response<List<string>>> GetUserListByDonViPhongBan(string maDonVi, string maPhongBan, string email)
        {
            try
            {
                var body = new
                {
                    MaDonVi = maDonVi,
                    MaPhongBan = maPhongBan,
                    Email = email
                };

                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(body),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("api/task/user-list-donvi-phongban", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    var dataObj = JsonConvert.DeserializeObject<Object_Response<List<string>>>(result);

                    return dataObj ?? new Object_Response<List<string>>
                    {
                        Data = new List<string>(),
                        Message = "No data",
                        Success = false
                    };
                }
                else
                {
                    Console.WriteLine("API trả về lỗi: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy UserList-DonViPhongBan: " + ex.Message);
            }

            return new Object_Response<List<string>>
            {
                Data = new List<string>(),
                Message = "Error",
                Success = false
            };
        }

        public async Task<Object_Response<List<NguoiDung>>> getNguoiDungByEmails(List<string> emails)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(emails),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("api/task/get-nguoi-dung-by-email", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Object_Response<List<NguoiDung>>>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy UserList-byEmail: " + ex.Message);
            }
            return null;
        }

        public async Task<Object_Response<List<NguoiNhanEmail>>> GetNguoiNhanEmailByMaEmailAsync(string maEmail)
        {
            try
            {
                var response = await client.GetAsync($"api/task/get-nguoi-nhan/{maEmail}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Object_Response<List<NguoiNhanEmail>>>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách người nhận Email: " + ex.Message);
            }
            return null;
        }

        public async Task<ApiResponse> sendEmail(Email email, List<NguoiNhanEmail> lstNNE, List<TepDinhKemEmail> lstTDK, NguoiDung currentUser, int taskId, string mk)
        {
            try
            {
                var payload = new
                {
                    Email = email,
                    DanhSachNguoiNhanEmail = lstNNE,
                    DanhSachTepDinhKem = lstTDK,
                    CurrentUser = currentUser,
                    TaskId = taskId,
                    MK = mk
                };

                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                    var response = await client.PostAsync("api/task/gui-email", jsonContent);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<ApiResponse>(result);
                    }

                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Gui Email That Bai ({(int)response.StatusCode}): {result}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi Gui Email: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse> ReplyEmailAsync(ReplyEmailRequest req)
        {
            try
            {
                //var json = JsonConvert.SerializeObject(req, Formatting.Indented);
                //Console.WriteLine("=== REPLY REQ ===");
                //Console.WriteLine(json);

                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(req),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/reply-email", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(result);
                }

                return new ApiResponse
                {
                    Success = false,
                    Message = $"Reply Email Thất bại ({(int)response.StatusCode}): {result}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi Reply Email: " + ex.Message
                };
            }
        }
        
        public async Task<Object_Response<List<Email>>> GetEmailByChiTietCVAsync(int maChiTietCV)
        {
            try
            {
                var response = await client.GetAsync($"api/task/get-email-by-chitietcv/{maChiTietCV}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Object_Response<List<Email>>>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy Email: " + ex.Message);
            }
            return null;
        }

        public async Task<ApiResponse> getIsGiaoViec(int maNguoiDung, int maChiTietCV)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    MaChiTietCV = maChiTietCV
                };

                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                    var response = await client.PostAsync("api/task/get-is-giao-viec", jsonContent);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<ApiResponse>(result);
                    }

                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"getIsGiaoViec thất bại ({(int)response.StatusCode}): {result}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Loi khi getIsGiaoViec: " + ex.Message
                };
            }
        }

        public async Task<Object_Response<List<ThongBaoNguoiDung>>> GetTop8ThongBaoById(int id)
        {
            try
            {
                var response = await client.GetAsync($"api/task/get-top-8-thong-bao/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Object_Response<List<ThongBaoNguoiDung>>>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy Top8ThongBao: " + ex.Message);
            }
            return null;
        }

        public async Task<Object_Response<TepTin>> UploadFile(string filePath, string maCongViec)
        {
            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    // gửi thêm text field
                    content.Add(new StringContent(maCongViec), "maCongViec");

                    // mở file stream
                    using (var fileStream = File.OpenRead(filePath))
                    {
                        var streamContent = new StreamContent(fileStream);
                        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                        content.Add(streamContent, "file", Path.GetFileName(filePath));

                        var response = await client.PostAsync("api/task/upload-file", content);

                        string result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Server response: " + result);

                        if (response.IsSuccessStatusCode)
                        {
                            return JsonConvert.DeserializeObject<Object_Response<TepTin>>(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi upload file: " + ex.Message);
            }

            return null;
        }
        public async Task<byte[]> DownloadFileAsync(string filePath, string originalFileName)
        {
            try
            {
                // Tạo request body
                var requestObj = new
                {
                    FilePath = filePath,
                    OriginalFileName = originalFileName
                };

                var json = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Gọi API download qua POST
                var response = await client.PostAsync("api/task/download-file", content);

                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu file dạng byte[]
                    var fileBytes = await response.Content.ReadAsByteArrayAsync();
                    return fileBytes;
                }
                else
                {
                    Console.WriteLine("Download thất bại: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi download file: " + ex.Message);
            }

            return null;
        }

        public async Task<Object_Response<int>> SoTaskTrongTuan(int maNguoiDung)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/task/so-task-trong-tuan/{maNguoiDung}");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"SoTaskTrongTuan thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Loi khi SoTaskTrongTuan: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskTrongThang(int maNguoiDung)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/task/so-task-trong-thang/{maNguoiDung}");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"SoTaskTrongThang thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Loi khi SoTaskTrongThang: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskTrongNam(int maNguoiDung)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/task/so-task-trong-nam/{maNguoiDung}");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"SoTaskTrongNam thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Loi khi SoTaskTrongNam: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskDaGiaoTrongTuan(int maNguoiDung)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/task/so-task-da-giao-trong-tuan/{maNguoiDung}");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"SoTaskDaGiaoTrongTuan thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Loi khi SoTaskDaGiaoTrongTuan: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskDaGiaoTrongThang(int maNguoiDung)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/task/so-task-da-giao-trong-thang/{maNguoiDung}");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"SoTaskDaGiaoTrongThang thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Loi khi SoTaskDaGiaoTrongThang: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskDaGiaoTrongNam(int maNguoiDung)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/task/so-task-da-giao-trong-nam/{maNguoiDung}");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"SoTaskDaGiaoTrongNam thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Loi khi SoTaskDaGiaoTrongNam: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskChuaXuLiByFilter(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/so-task-chua-xu-li-fillter", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"get task chua xu li fillter thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi get task chua xu li fillter: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskDangXuLiByFilter(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/so-task-dang-xu-li-fillter", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"get task dang xu li fillter thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi get task dang xu li fillter: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskHoanThanhByFilter(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/so-task-hoan-thanh-fillter", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"get task hoan thanh fillter thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi get task hoan thanh fillter: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskTreByFilter(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/so-task-tre-fillter", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"get task tre fillter thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi get task tre fillter: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskDaGiaoChuaXuLiByFilter(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/so-task-da-giao-chua-xu-li-fillter", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"get task da giao chua xu li fillter thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi get task chua xu li fillter: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskDaGiaoDangXuLiByFilter(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/so-task-da-giao-dang-xu-li-fillter", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"get task da giao dang xu li fillter thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi get task da giao dang xu li fillter: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskDaGiaoHoanThanhByFilter(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/so-task-da-giao-hoan-thanh-fillter", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"get task da giao hoan thanh fillter thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi get task da giao hoan thanh fillter: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<int>> SoTaskDaGiaoTreByFilter(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/so-task-da-giao-tre-fillter", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<int>>(result);
                }

                return new Object_Response<int>
                {
                    Success = false,
                    Message = $"get task da giao tre fillter thất bại ({(int)response.StatusCode}): {result}",
                    Data = 0
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<int>
                {
                    Success = false,
                    Message = "Lỗi khi get task da giao tre fillter: " + ex.Message,
                    Data = 0
                };
            }
        }

        public async Task<Object_Response<List<TaskDto>>> TaskDaGiao(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/task-da-giao", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<List<TaskDto>>>(result);
                }

                return new Object_Response<List<TaskDto>>
                {
                    Success = false,
                    Message = $"get task da giao thất bại ({(int)response.StatusCode}): {result}",
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<List<TaskDto>>
                {
                    Success = false,
                    Message = "Lỗi khi get task da giao: " + ex.Message,
                };
            }
        }

        public async Task<Object_Response<List<TaskDto>>> TaskDuocGiao(int maNguoiDung, DateTime start, DateTime end)
        {
            try
            {
                var payload = new
                {
                    MaNguoiDung = maNguoiDung,
                    NgayBatDau = start,
                    NgayKetThuc = end
                };
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.PostAsync("api/task/task-duoc-giao", jsonContent);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<List<TaskDto>>>(result);
                }

                return new Object_Response<List<TaskDto>>
                {
                    Success = false,
                    Message = $"get task duoc giao thất bại ({(int)response.StatusCode}): {result}",
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<List<TaskDto>>
                {
                    Success = false,
                    Message = "Lỗi khi get task duoc giao: " + ex.Message,
                };
            }
        }

        public async Task<Object_Response<List<DonVi>>> GetDonVi()
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/task/get-don-vi");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<List<DonVi>>>(result);
                }

                return new Object_Response<List<DonVi>>
                {
                    Success = false,
                    Message = $"GETDONVI thất bại ({(int)response.StatusCode}): {result}",
                    Data = new List<DonVi>()
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<List<DonVi>>
                {
                    Success = false,
                    Message = "Loi khi GETDONVI: " + ex.Message,
                    Data = new List<DonVi>()
                };
            }
        }

        public async Task<Object_Response<List<PhongBan>>> GetPhongBan()
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/task/get-phong-ban");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<List<PhongBan>>>(result);
                }

                return new Object_Response<List<PhongBan>>
                {
                    Success = false,
                    Message = $"GETPHONGBAN thất bại ({(int)response.StatusCode}): {result}",
                    Data = new List<PhongBan>()
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<List<PhongBan>>
                {
                    Success = false,
                    Message = "Loi khi GETPHONGBAN: " + ex.Message,
                    Data = new List<PhongBan>()
                };
            }
        }

        public async Task<Object_Response<List<ChucVu>>> GetChucVu()
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/task/get-chuc-vu");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<List<ChucVu>>>(result);
                }

                return new Object_Response<List<ChucVu>>
                {
                    Success = false,
                    Message = $"GETCHUCVU thất bại ({(int)response.StatusCode}): {result}",
                    Data = new List<ChucVu>()
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<List<ChucVu>>
                {
                    Success = false,
                    Message = "Loi khi GETCHUCVU: " + ex.Message,
                    Data = new List<ChucVu>()
                };
            }
        }

        public async Task<Object_Response<List<NguoiDung>>> GetAccountInactive()
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/auth/get-account-inactive");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<List<NguoiDung>>>(result);
                }

                return new Object_Response<List<NguoiDung>>
                {
                    Success = false,
                    Message = $"GETACCOUNTINACTIVE thất bại ({(int)response.StatusCode}): {result}",
                    Data = new List<NguoiDung>()
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<List<NguoiDung>>
                {
                    Success = false,
                    Message = "Loi khi GETACCOUNTINACTIVE: " + ex.Message,
                    Data = new List<NguoiDung>()
                };
            }
        }

        public async Task<ApiResponse> UpdateTrangThaiNguoiDung(NguoiDung nd)
        {
            try
            {
                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(nd),
                    Encoding.UTF8,
                    "application/json"
                );

                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                    var response = await client.PostAsync("api/task/update-nguoi-dung", jsonContent);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<ApiResponse>(result);
                    }

                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Cập nhật nguoidung thất bại ({(int)response.StatusCode}): {result}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi khi cập nhật nguoidung email: " + ex.Message
                };
            }
        }

        public async Task<Object_Response<List<NguoiDung>>> GetAccountActive()
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

                var response = await client.GetAsync($"api/auth/get-account-active");
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Object_Response<List<NguoiDung>>>(result);
                }

                return new Object_Response<List<NguoiDung>>
                {
                    Success = false,
                    Message = $"GETACCOUNTACTIVE thất bại ({(int)response.StatusCode}): {result}",
                    Data = new List<NguoiDung>()
                };
            }
            catch (Exception ex)
            {
                return new Object_Response<List<NguoiDung>>
                {
                    Success = false,
                    Message = "Loi khi GETACCOUNTACTIVE: " + ex.Message,
                    Data = new List<NguoiDung>()
                };
            }
        }


    }
}
