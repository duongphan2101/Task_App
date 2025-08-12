using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
                using (var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(updateData),
                    Encoding.UTF8,
                    "application/json"
                ))
                {
                    // Kèm token nếu cần
                    //client.DefaultRequestHeaders.Authorization =
                    //    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalSession.Token);

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
                    Message = "Lỗi khi cập nhật người liên quan công việc: " + ex.Message
                };
            }
        }





    }
}
