using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TcpServerApp.DAO;
using TcpServerApp.model;
using TcpServerApp.Model;
using Formatting = Newtonsoft.Json.Formatting;

namespace TcpServerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
            Console.WriteLine("Server đang chạy ở cổng 5000...");

            DailyEmailScheduler dailyEmailScheduler = new DailyEmailScheduler();
            dailyEmailScheduler.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client mới kết nối.");
                Thread t = new Thread(() => HandleClient(client));
                t.Start();
            }
        }

        static void HandleClient(TcpClient client)
        {
            NguoiDungDAO nguoiDungDAO = new NguoiDungDAO();
            LoginDAO loginDAO = new LoginDAO();
            CongViecDAO CongViecDAO = new CongViecDAO();

            var stream = client.GetStream();

            try
            {
                while (true)
                {
                    byte[] lengthBuffer = new byte[4];
                    int readLength = stream.Read(lengthBuffer, 0, 4);
                    if (readLength != 4)
                    {
                        Console.WriteLine("Client đã ngắt kết nối hoặc gói tin không đầy đủ.");
                        break;
                    }

                    int dataLength = BitConverter.ToInt32(lengthBuffer, 0);
                    if (dataLength <= 0 || dataLength > 10_000_000)
                    {
                        Console.WriteLine("Độ dài dữ liệu không hợp lệ: " + dataLength);
                        break;
                    }

                    byte[] dataBuffer = new byte[dataLength];
                    int totalRead = 0;
                    while (totalRead < dataLength)
                    {
                        int bytesRead = stream.Read(dataBuffer, totalRead, dataLength - totalRead);
                        if (bytesRead == 0)
                        {
                            Console.WriteLine("Client ngắt kết nối giữa chừng.");
                            return;
                        }
                        totalRead += bytesRead;
                    }

                    string msg = Encoding.UTF8.GetString(dataBuffer, 0, totalRead).Trim();
                    Console.WriteLine("Client gửi: " + msg);

                    string reply;

                    try
                    {
                        var request = JsonConvert.DeserializeObject<RequestWrapper>(msg);

                        switch (request.Command.ToLower())
                        {
                            case "exit":
                                Console.WriteLine("Client yêu cầu ngắt kết nối.");
                                return;

                            case "hello":
                                reply = "Xin chào từ server!";
                                break;

                            case "getnguoidungbyemail":
                                {
                                    string email = ((JObject)request.Data)["Email"]?.ToString();
                                    NguoiDung nguoiDung = nguoiDungDAO.GetNguoiDungByEmail(email);
                                    reply = JsonConvert.SerializeObject(nguoiDung, Formatting.None);
                                    break;
                                }

                            case "getdanhsachnguoidungbyemails":
                                {
                                    var emails = ((JObject)request.Data)["Emails"]?.ToObject<List<string>>() ?? new List<string>();
                                    List<NguoiDung> nguoiDungs = nguoiDungDAO.getDanhSachNguoiDungByEmails(emails);
                                    reply = JsonConvert.SerializeObject(nguoiDungs, Formatting.None);
                                    break;
                                }

                            case "nguoidung":
                                NguoiDung nd = nguoiDungDAO.GetNguoiDungById(Convert.ToInt32(request.Data));
                                reply = JsonConvert.SerializeObject(nd);
                                break;

                            case "getdanhsachnguoidungbydonvivaphongban":
                                var data_getdanhsachnguoidungbydonvivaphongban = (JObject)request.Data;
                                string maDonVi_getdanhsachnguoidungbydonvivaphongban = data_getdanhsachnguoidungbydonvivaphongban["MaDonVi"]?.ToString();
                                string maPhongBan_getdanhsachnguoidungbydonvivaphongban = data_getdanhsachnguoidungbydonvivaphongban["MaPhongBan"]?.ToString();
                                string currentUserEmail = data_getdanhsachnguoidungbydonvivaphongban["CurrentUserEmail"]?.ToString();
                                var getdanhsachnguoidungbydonvivaphongban = nguoiDungDAO.getDanhSachNguoiDungByDonViVaPhongBan(maDonVi_getdanhsachnguoidungbydonvivaphongban, 
                                    maPhongBan_getdanhsachnguoidungbydonvivaphongban, currentUserEmail);
                                reply = JsonConvert.SerializeObject(getdanhsachnguoidungbydonvivaphongban, Formatting.None);
                                break;

                            case "checklogin":
                                var dataLogin = (JObject)request.Data;
                                int loginId = loginDAO.CheckLogin(dataLogin["Email"]?.ToString(), dataLogin["Password"]?.ToString());
                                reply = JsonConvert.SerializeObject(loginId);
                                break;

                            case "gettop8thongbaobyid":
                                var lstThongBao = CongViecDAO.GetTop8ThongBaoById(((JObject)request.Data)["MaNguoiDung"]?.ToObject<int>() ?? 0);
                                reply = JsonConvert.SerializeObject(lstThongBao, Formatting.None);
                                break;

                            case "updatetrangthaithongbao":
                                bool updateTrangThaiThongBaoResult = CongViecDAO.UpdateTrangThaiThongBao(((JObject)request.Data)["MaThongBao"]?.ToObject<int>() ?? 0);
                                reply = JsonConvert.SerializeObject(updateTrangThaiThongBaoResult);
                                break;

                            case "getcongviecdagiaobyuserid":
                                var CongViecDaGiao = CongViecDAO.GetCongViecDaGiaoByUserId(((JObject)request.Data)["MaNguoiDung"]?.ToObject<int>() ?? 0);
                                reply = JsonConvert.SerializeObject(CongViecDaGiao, Formatting.None);
                                break;

                            case "getcongviecdagiaobyuserid_sortbytrangthai":
                                var CongViecDaGiao_sort = CongViecDAO.GetCongViecDaGiaoByUserId_SortByTrangThai(((JObject)request.Data)["MaNguoiDung"]?.ToObject<int>() ?? 0);
                                Console.WriteLine("HHHH " + CongViecDaGiao_sort);
                                reply = JsonConvert.SerializeObject(CongViecDaGiao_sort, Formatting.None);
                                break;

                            case "getcongviecduocgiaobyuserid":
                                var CongViecDuocGiao = CongViecDAO.GetCongViecDuocGiaoByUserId(((JObject)request.Data)["MaNguoiDung"]?.ToObject<int>() ?? 0);
                                reply = JsonConvert.SerializeObject(CongViecDuocGiao, Formatting.None);
                                break;

                            case "getcongviecduocgiao_sortbytrangthai":
                                var CongViecDuocGiao_sort = CongViecDAO.GetCongViecDuocGiao_SortByTrangThai(((JObject)request.Data)["MaNguoiDung"]?.ToObject<int>() ?? 0);
                                reply = JsonConvert.SerializeObject(CongViecDuocGiao_sort, Formatting.None);
                                break;

                            case "getcongviecbyidcongviec":
                                var CongViecByIdCTCV = CongViecDAO.GetCongViecByIdCongViec(((JObject)request.Data)["MaChiTietCV"]?.ToObject<int>() ?? 0);
                                reply = JsonConvert.SerializeObject(CongViecByIdCTCV, Formatting.None);
                                break;

                            case "getcongviecbyid":
                                var CongViecById = CongViecDAO.getCongViecById(((JObject)request.Data)["MaCongViec"]?.ToString());
                                reply = JsonConvert.SerializeObject(CongViecById, Formatting.None);
                                break;

                            case "getchitietcongviecbyid":
                                var ChiTietCongViecById = CongViecDAO.getChiTietCongViecById(((JObject)request.Data)["MaChiTietCV"]?.ToObject<int>() ?? 0);
                                reply = JsonConvert.SerializeObject(ChiTietCongViecById, Formatting.None);
                                break;

                            case "getdanhsachnguoilienquanbyidcongviec":
                                var getdanhsachnguoilienquanbyidcongviec = CongViecDAO.GetDanhSachNguoiLienQuanByIdCongViec(((JObject)request.Data)["MaCongViec"]?.ToString());
                                reply = JsonConvert.SerializeObject(getdanhsachnguoilienquanbyidcongviec, Formatting.None);
                                break;

                            case "getlistnguoilienquanbyidcongviec":
                                var getlistnguoilienquanbyidcongviec = CongViecDAO.GetListNguoiLienQuanByIdCongViec(((JObject)request.Data)["MaCongViec"]?.ToString());
                                reply = JsonConvert.SerializeObject(getlistnguoilienquanbyidcongviec, Formatting.None);
                                break;

                            case "generatemacongviecfromlast":
                                try
                                {
                                    var data = ((JObject)request.Data).ToObject<dynamic>();
                                    string maDonVi = data.MaDonVi;
                                    string maPhongBan = data.MaPhongBan;

                                    var maCongViec = nguoiDungDAO.GenerateMaCongViecFromLast(maDonVi, maPhongBan);
                                    reply = JsonConvert.SerializeObject(new { MaCongViec = maCongViec }, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi generateMaCongViecFromLast: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "sendemail":
                                {
                                    try
                                    {
                                        var data = (JObject)request.Data;

                                        var sendemail_email = data["Email"]?.ToObject<Email>();
                                        var sendemail_nguoiNhanList = data["NguoiNhanEmail"]?.ToObject<List<NguoiNhanEmail>>();
                                        var sendemail_tepDinhKemList = data["TepDinhKemEmail"]?.ToObject<List<TepDinhKemEmail>>();
                                        var sendemail_nguoiDung = data["NguoiDung"]?.ToObject<NguoiDung>();

                                        bool result = CongViecDAO.sendEmail(sendemail_email, sendemail_nguoiNhanList, sendemail_tepDinhKemList, sendemail_nguoiDung);
                                        reply = JsonConvert.SerializeObject(result, Formatting.None);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Lỗi xử lý sendEmail: " + ex.Message);
                                        reply = JsonConvert.SerializeObject(false);
                                    }
                                    break;
                                }

                            case "createcongviec":
                                try
                                {
                                    var createcongviec = CongViecDAO.createCongViec(((JObject)request.Data)["CongViec"]?.ToObject<CongViec>());
                                    reply = JsonConvert.SerializeObject(createcongviec, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi createCongViec: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "createchitietcongviec":
                                try
                                {
                                    var createchitietcongviec = CongViecDAO.CreateChiTietCongViec(((JObject)request.Data)["ChiTietCongViec"]?.ToObject<ChiTietCongViec>());
                                    reply = JsonConvert.SerializeObject(createchitietcongviec, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi createChiTietCongViec: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(-1);
                                }
                                break;

                            case "taochitietcongviectheotansuat":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    var taochitiet = CongViecDAO.TaoChiTietCongViecTheoTanSuat(
                                        data["CongViec"]?.ToObject<CongViec>(),
                                        data["SoNgayHoanThanh"]?.ToObject<int>() ?? 0
                                    );
                                    reply = JsonConvert.SerializeObject(taochitiet, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi TaoChiTietCongViecTheoTanSuat: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(new List<ChiTietCongViec>());
                                }
                                break;

                            case "createnguoilienquan":
                                try
                                {
                                    var createlienquan = CongViecDAO.CreateNguoiLienQuan(((JObject)request.Data)["NguoiLienQuanCongViec"]?.ToObject<NguoiLienQuanCongViec>());
                                    reply = JsonConvert.SerializeObject(createlienquan, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi createNguoiLienQuan: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "createemail":
                                try
                                {
                                    var email = ((JObject)request.Data)["Email"]?.ToObject<Email>();
                                    var result = CongViecDAO.CreateEmail(email);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)    
                                {
                                    Console.WriteLine("Lỗi createEmail: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "createnguoinhanemail":
                                try
                                {
                                    var nguoiNhan = ((JObject)request.Data)["NguoiNhanEmail"]?.ToObject<NguoiNhanEmail>();
                                    var result = CongViecDAO.CreateNguoiNhanEmail(nguoiNhan);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi createNguoiNhanEmail: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "createteptin":
                                try
                                {
                                    var teptin = ((JObject)request.Data)["TepTin"]?.ToObject<TepTin>();
                                    var result = CongViecDAO.CreateTepTin(teptin);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi createTepTin: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(-1);
                                }
                                break;

                            case "createteptindinhkem":
                                try
                                {
                                    var tepdk = ((JObject)request.Data)["TepDinhKemEmail"]?.ToObject<TepDinhKemEmail>();
                                    var result = CongViecDAO.CreateTepTinDinhKem(tepdk);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi createTepTinDinhKem: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "createphanhoicongviec":
                                try
                                {
                                    var ph = ((JObject)request.Data)["PhanHoiCongViec"]?.ToObject<PhanHoiCongViec>();
                                    var result = CongViecDAO.createPhanHoiCongViec(ph);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi createPhanHoiCongViec: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "createthongbao":
                                try
                                {
                                    var tb = ((JObject)request.Data)["ThongBaoNguoiDung"]?.ToObject<ThongBaoNguoiDung>();
                                    var result = CongViecDAO.CreateThongBao(tb);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi createThongBao: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "updatetrangthaicongviec":
                                try
                                {
                                    var ct = ((JObject)request.Data)["ChiTietCongViec"]?.ToObject<ChiTietCongViec>();
                                    var result = CongViecDAO.UpdateTrangThaiCongViec(ct);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi updateTrangThaiCongViec: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "updateonlytrangthaicongviec":
                                try
                                {
                                    var ct = ((JObject)request.Data)["ChiTietCongViec"]?.ToObject<ChiTietCongViec>();
                                    var result = CongViecDAO.UpdateOnlyTrangThaiCongViec(ct);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi updateOnlyTrangThaiCongViec: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "updatehanhoanthanhtask":
                                try
                                {
                                    var ct = ((JObject)request.Data)["ChiTietCongViec"]?.ToObject<ChiTietCongViec>();
                                    var result = CongViecDAO.UpdateHanHoanThanhTask(ct);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi updateHanHoanThanhTask: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "updatetrangthaiemail":
                                try
                                {
                                    var email = ((JObject)request.Data)["Email"]?.ToObject<Email>();
                                    var result = CongViecDAO.UpdateTrangThaiEmail(email);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi updateTrangThaiEmail: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "updatetiendocongviec":
                                try
                                {
                                    var ct = ((JObject)request.Data)["ChiTietCongViec"]?.ToObject<ChiTietCongViec>();
                                    var result = CongViecDAO.UpdateTienDoCongViec(ct);
                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi updateTienDoCongViec: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "gettepdinhkememailbymacongviec":
                                try
                                {
                                    string maCongViec = ((JObject)request.Data)["MaCongViec"]?.ToString();
                                    var tepList = CongViecDAO.GetTepDinhKemEmailByMaCongViec(maCongViec);
                                    reply = JsonConvert.SerializeObject(tepList, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi GetTepDinhKemEmailByMaCongViec: " + ex.Message);
                                    reply = "[]";
                                }
                                break;

                            case "getfeedbacksbymacongviec":
                                try
                                {
                                    string maCongViec = ((JObject)request.Data)["MaCongViec"]?.ToString();
                                    var feedbacks = CongViecDAO.GetFeedbacksByMaCongViec(maCongViec);
                                    reply = JsonConvert.SerializeObject(feedbacks, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi GetFeedbacksByMaCongViec: " + ex.Message);
                                    reply = "[]";
                                }
                                break;

                            case "getlistnguoinhanemail":
                                try
                                {
                                    string maEmail = ((JObject)request.Data)["MaEmail"]?.ToString();
                                    var nguoiNhans = CongViecDAO.GetListNguoiNhanEmail(maEmail);
                                    reply = JsonConvert.SerializeObject(nguoiNhans, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi GetListNguoiNhanEmail: " + ex.Message);
                                    reply = "[]";
                                }
                                break;

                            case "isnguoilienquanexists":
                                try
                                {
                                    string maCongViec = ((JObject)request.Data)["MaCongViec"]?.ToString();
                                    int maNguoiDung = ((JObject)request.Data)["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    string vaiTro = ((JObject)request.Data)["VaiTro"]?.ToString();

                                    bool exists = CongViecDAO.IsNguoiLienQuanExists(maCongViec, maNguoiDung, vaiTro);
                                    reply = JsonConvert.SerializeObject(exists, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi IsNguoiLienQuanExists: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "isnguoinhanemailexists":
                                try
                                {
                                    string maEmail = ((JObject)request.Data)["MaEmail"]?.ToString();
                                    int maNguoiDung = ((JObject)request.Data)["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    string vaiTro = ((JObject)request.Data)["VaiTro"]?.ToString();

                                    bool exists = CongViecDAO.IsNguoiNhanEmailExists(maEmail, maNguoiDung, vaiTro);
                                    reply = JsonConvert.SerializeObject(exists, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi IsNguoiNhanEmailExists: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(false);
                                }
                                break;

                            case "getlistthongbaobyid":
                                try
                                {
                                    int maNguoiDung = ((JObject)request.Data)["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var listThongBao = CongViecDAO.GetListThongBaoById(maNguoiDung);
                                    reply = JsonConvert.SerializeObject(listThongBao, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi GetListThongBaoById: " + ex.Message);
                                    reply = "[]";
                                }
                                break;

                            default:
                                reply = "Lệnh không hợp lệ.";
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi xử lý request: " + ex.Message);
                        reply = JsonConvert.SerializeObject("Lỗi xử lý request: " + ex.Message);
                    }

                    byte[] replyBytes = Encoding.UTF8.GetBytes(reply);
                    byte[] lengthPrefix = BitConverter.GetBytes(replyBytes.Length);
                    stream.Write(lengthPrefix, 0, 4);
                    stream.Write(replyBytes, 0, replyBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xử lý client: " + ex.Message);
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client đã ngắt kết nối.");
            }
        }

    }
}
