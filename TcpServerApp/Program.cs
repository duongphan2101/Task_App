using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
                                        data["SoNgayHoanThanh"]?.ToObject<int>() ?? 0,
                                        data["MucDoUuTien"]?.ToObject<int>() ?? 0
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

                            case "getsotasktrongtuan":
                                try
                                {
                                    var data_getsotasktrongtuan = (JObject)request.Data;
                                    int maNguoiDung_getsotasktrongtuan = data_getsotasktrongtuan["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int soTaskTrongTuan = CongViecDAO.getSoTaskTrongTuanById(maNguoiDung_getsotasktrongtuan);
                                    reply = JsonConvert.SerializeObject(soTaskTrongTuan, Formatting.None);
                                }catch(Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotasktrongthang":
                                try
                                {
                                    var data_getsotasktrongthang = (JObject)request.Data;
                                    int maNguoiDung_getsotasktrongthang = data_getsotasktrongthang["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int soTaskTrongThang = CongViecDAO.getSoTaskTrongThangById(maNguoiDung_getsotasktrongthang);
                                    reply = JsonConvert.SerializeObject(soTaskTrongThang, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongThangById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotasktrongnam":
                                try
                                {
                                    var data_getsotasktrongnam = (JObject)request.Data;
                                    int maNguoiDung_getsotasktrongnam = data_getsotasktrongnam["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int soTaskTrongNam = CongViecDAO.getSoTaskTrongNamById(maNguoiDung_getsotasktrongnam);
                                    reply = JsonConvert.SerializeObject(soTaskTrongNam, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongNamById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskchuaxuly":
                                try
                                {
                                    var data_getsotaskchuaxuly = (JObject)request.Data;
                                    int maNguoiDung_getsotaskchuaxuly = data_getsotaskchuaxuly["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int getsotaskchuaxuly = CongViecDAO.getSoTaskChuaXuLyById(maNguoiDung_getsotaskchuaxuly);
                                    reply = JsonConvert.SerializeObject(getsotaskchuaxuly, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskChuaXuLiById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdangxuly":
                                try
                                {
                                    var data_getsotaskdangxuly = (JObject)request.Data;
                                    int maNguoiDung_getsotaskdangxuly = data_getsotaskdangxuly["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int getsotaskdangxuly = CongViecDAO.getSoTaskDangXuLyById(maNguoiDung_getsotaskdangxuly);
                                    reply = JsonConvert.SerializeObject(getsotaskdangxuly, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskDangXuLiById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdahoanthanh":
                                try
                                {
                                    var data_getsotaskdahoanthanh = (JObject)request.Data;
                                    int maNguoiDung_getsotaskdahoanthanh = data_getsotaskdahoanthanh["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int getsotaskdahoanthanh = CongViecDAO.getSoTaskHoanThanhById(maNguoiDung_getsotaskdahoanthanh);
                                    reply = JsonConvert.SerializeObject(getsotaskdahoanthanh, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskDaHoanThanhById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdatre":
                                try
                                {
                                    var data_getsotaskdatre = (JObject)request.Data;
                                    int maNguoiDung_getsotaskdatre = data_getsotaskdatre["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int getsotaskdatre = CongViecDAO.getSoTaskTreById(maNguoiDung_getsotaskdatre);
                                    reply = JsonConvert.SerializeObject(getsotaskdatre, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTreById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getnguoigiao":
                                try
                                {
                                    int maNguoiGiao_getnguoigiao = ((JObject)request.Data)["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var nguoiGiao = CongViecDAO.GetNguoiGiaoViec(maNguoiGiao_getnguoigiao);
                                    reply = JsonConvert.SerializeObject(nguoiGiao, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi GetNguoiGiaoById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(null);
                                }
                                break;

                            case "getsotaskdahoanthanhbyfilter":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maNguoiDung = data["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var start = data["NgayBatDau"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    var end = data["NgayKetThuc"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    int soTask = CongViecDAO.getSoTaskHoanThanhByIdByFilter(maNguoiDung, start, end);
                                    reply = JsonConvert.SerializeObject(soTask, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanByfilter: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotasktrehanbyfilter":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maNguoiDung = data["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var start = data["NgayBatDau"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    var end = data["NgayKetThuc"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    int soTask = CongViecDAO.getSoTaskTreHanByIdByFilter(maNguoiDung, start, end);
                                    reply = JsonConvert.SerializeObject(soTask, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanByfilter: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskchuxulibyfilter":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maNguoiDung = data["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var start = data["NgayBatDau"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    var end = data["NgayKetThuc"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    int soTask = CongViecDAO.getSoTaskChuaXuLiByIdByFilter(maNguoiDung, start, end);
                                    reply = JsonConvert.SerializeObject(soTask, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanByfilter: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdangxulibyfilter":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maNguoiDung = data["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var start = data["NgayBatDau"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    var end = data["NgayKetThuc"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    int soTask = CongViecDAO.getSoTaskDangXuLiByIdByFilter(maNguoiDung, start, end);
                                    reply = JsonConvert.SerializeObject(soTask, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanByfilter: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdagiaohoanthanhbyfilter":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maNguoiDung = data["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var start = data["NgayBatDau"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    var end = data["NgayKetThuc"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    int soTask = CongViecDAO.getSoTaskDaGiaoHoanThanhByIdByFilter(maNguoiDung, start, end);
                                    reply = JsonConvert.SerializeObject(soTask, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanByfilter: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdagiaotrehanbyfilter":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maNguoiDung = data["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var start = data["NgayBatDau"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    var end = data["NgayKetThuc"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    int soTask = CongViecDAO.getSoTaskDaGiaoTreHanByIdByFilter(maNguoiDung, start, end);
                                    reply = JsonConvert.SerializeObject(soTask, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanByfilter: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdagiaochuaxulibyfilter":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maNguoiDung = data["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var start = data["NgayBatDau"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    var end = data["NgayKetThuc"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    int soTask = CongViecDAO.getSoTaskDaGiaoChuaXuLiByIdByFilter(maNguoiDung, start, end);
                                    reply = JsonConvert.SerializeObject(soTask, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanByfilter: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdagiaodangxulibyfilter":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maNguoiDung = data["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    var start = data["NgayBatDau"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    var end = data["NgayKetThuc"]?.ToObject<DateTime>() ?? DateTime.Now;
                                    int soTask = CongViecDAO.getSoTaskDaGiaoDangXuLiByIdByFilter(maNguoiDung, start, end);
                                    reply = JsonConvert.SerializeObject(soTask, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanByfilter: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdagiaotrongtuan":
                                try
                                {
                                    var data_getsotasktrongtuan = (JObject)request.Data;
                                    int maNguoiDung_getsotasktrongtuan = data_getsotasktrongtuan["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int soTaskTrongTuan = CongViecDAO.getSoTaskDaGiaoTrongTuanById(maNguoiDung_getsotasktrongtuan);
                                    reply = JsonConvert.SerializeObject(soTaskTrongTuan, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongTuanById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdagiaotrongthang":
                                try
                                {
                                    var data_getsotasktrongthang = (JObject)request.Data;
                                    int maNguoiDung_getsotasktrongthang = data_getsotasktrongthang["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int soTaskTrongThang = CongViecDAO.getSoTaskDaGiaoTrongThangById(maNguoiDung_getsotasktrongthang);
                                    reply = JsonConvert.SerializeObject(soTaskTrongThang, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongThangById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getsotaskdagiaotrongnam":
                                try
                                {
                                    var data_getsotasktrongnam = (JObject)request.Data;
                                    int maNguoiDung_getsotasktrongnam = data_getsotasktrongnam["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int soTaskTrongNam = CongViecDAO.getSoTaskDaGiaoTrongNamById(maNguoiDung_getsotasktrongnam);
                                    reply = JsonConvert.SerializeObject(soTaskTrongNam, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getSoTaskTrongNamById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getisgiaoviec":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maNguoiDung = data["MaNguoiDung"]?.ToObject<int>() ?? 0;
                                    int maCTCV = data["MaCTCV"]?.ToObject<int>() ?? 0;
                                    var result = CongViecDAO.getIsGiaoViec(maNguoiDung, maCTCV);

                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getIsGiaoViec: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "getmacongviec":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maCTCV = data["MaCTCV"]?.ToObject<int>() ?? 0;
                                    var result = CongViecDAO.getMaCongViecByMaChiTietCV(maCTCV);

                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getMaCOngViec: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "gettepbymatep":
                                try
                                {
                                    var data = (JObject)request.Data;
                                    int maCTCV = data["MaTep"]?.ToObject<int>() ?? 0;
                                    var result = CongViecDAO.GetTepTinById(maCTCV);

                                    reply = JsonConvert.SerializeObject(result, Formatting.None);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi getTepById: " + ex.Message);
                                    reply = JsonConvert.SerializeObject(0);
                                }
                                break;

                            case "uploadfile":
                                try
                                {
                                    // Parse JSON chứa metadata file
                                    var data = (JObject)request.Data;
                                    string fileName = data["FileName"]?.ToString();
                                    int fileSize = data["FileSize"]?.ToObject<int>() ?? 0;

                                    if (string.IsNullOrWhiteSpace(fileName) || fileSize <= 0)
                                    {
                                        reply = JsonConvert.SerializeObject("Thông tin file không hợp lệ.");
                                        break;
                                    }

                                    // Nhận tiếp dữ liệu file sau khi nhận JSON
                                    byte[] fileBuffer = new byte[fileSize];
                                    int totalReceived = 0;
                                    while (totalReceived < fileSize)
                                    {
                                        int bytesRead = stream.Read(fileBuffer, totalReceived, fileSize - totalReceived);
                                        if (bytesRead == 0)
                                        {
                                            Console.WriteLine("Client ngắt kết nối giữa lúc gửi file.");
                                            reply = JsonConvert.SerializeObject("Lỗi trong quá trình nhận file.");
                                            break;
                                        }
                                        totalReceived += bytesRead;
                                    }

                                    // Lưu file vào thư mục Attachments (MyDocuments/Attachments)
                                    string saveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Attachments");
                                    Directory.CreateDirectory(saveFolder);
                                    string filePath = Path.Combine(saveFolder, fileName);
                                    File.WriteAllBytes(filePath, fileBuffer);

                                    Console.WriteLine($"Đã lưu file: {filePath}");
                                    reply = JsonConvert.SerializeObject("Upload thành công.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Lỗi upload file: " + ex.Message);
                                    reply = JsonConvert.SerializeObject("Lỗi upload file: " + ex.Message);
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
