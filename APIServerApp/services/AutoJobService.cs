using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIServerApp.Context;
using APIServerApp.controllers;
using APIServerApp.Helper;
using APIServerApp.Model;
using Microsoft.EntityFrameworkCore;

namespace APIServerApp.services
{
    public class AutoJobService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AutoJobService> _logger;
        private readonly IEmailService _emailService;
        private readonly Auto _auto;

        public AutoJobService(AppDbContext context, ILogger<AutoJobService> logger, IEmailService emailService)
        {
            _context = context;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Auto_SendEmail()
        {
            var emails = await (from e in _context.Emails
                                join nd in _context.NguoiDungs on e.NguoiGui equals nd.MaNguoiDung into ndGroup
                                from nd in ndGroup.DefaultIfEmpty()
                                where e.TrangThai == 0 && e.NgayGui == DateTime.Now.Date
                                select new { e, nd })
                                .ToListAsync();

            foreach (var item in emails)
            {
                try
                {
                    var email = item.e;
                    email.NguoiGuiObj = item.nd;

                    var lstNguoiNhanEmail = await _emailService.GetListNguoiNhanEmail(email.MaEmail);
                    var lstFile = await _emailService.GetTepDinhKemEmailByMaCongViec(email.MaEmail);

                    bool sent = _auto.SendEmail(email, lstNguoiNhanEmail, lstFile, item.nd);
                    if (sent)
                    {
                        email.TrangThai = 1;
                        _context.Emails.Update(email);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi gửi email");
                }
            }
        }

        public async Task Auto_SendEmail_NhacNho()
        {
            var query = await (from ctcv in _context.ChiTietCongViecs
                               join nlq in _context.NguoiLienQuanCongViecs on ctcv.MaCongViec equals nlq.MaCongViec
                               join nd in _context.NguoiDungs on nlq.MaNguoiDung equals nd.MaNguoiDung
                               where (ctcv.TrangThai == 0 || ctcv.TrangThai == 1)
                                       && ctcv.SoNgayHoanThanh >= 3
                                       && nlq.VaiTro == "to"
                                       && ctcv.NgayKetThucCongViec.Value.Date == DateTime.Now.AddDays(1).Date
                               select new { ctcv, nd })
                            .ToListAsync();

            foreach (var item in query)
            {
                try
                {
                    var ct = item.ctcv;
                    var nn = item.nd;

                    var e = new Email
                    {
                        MaEmail = Guid.NewGuid().ToString(),
                        NguoiGui = 6,
                        MaChiTietCV = ct.MaChiTietCV,
                        TieuDe = "Nhắc nhở công việc sắp đến hạn",
                        NoiDung = $@"Xin chào {nn.HoTen}, công việc {ct.TieuDe} sẽ đến hạn vào {ct.NgayKetThucCongViec:dd/MM/yyyy}.",
                        NgayGui = DateTime.Now,
                        TrangThai = 0
                    };

                    await _context.Emails.AddAsync(e);
                    await _context.SaveChangesAsync();

                    var lstNguoiNhanEmail = new List<NguoiNhanEmail>
                    {
                        new NguoiNhanEmail { MaEmail = e.MaEmail, MaNguoiDung = nn.MaNguoiDung, VaiTro = "to", Email = e, NguoiDung = nn }
                    };

                    string email = Environment.GetEnvironmentVariable("EMAIL_ADDRESS");
                    string pass = Environment.GetEnvironmentVariable("EMAIL_SECRET_PASS");

                    var u = new NguoiDung { Email = email, MatKhau = pass };

                    bool sent = _auto.SendEmail(e, lstNguoiNhanEmail, new List<TepDinhKemEmail>(), u);
                    if (sent)
                    {
                        var tb = new ThongBaoNguoiDung
                        {
                            MaChiTietCV = ct.MaChiTietCV,
                            MaNguoiDung = nn.MaNguoiDung,
                            NoiDung = $"Thông báo công việc {ct.TieuDe} sắp đến hạn!",
                            NgayThongBao = DateTime.Now,
                            TrangThai = 0
                        };
                        await _context.ThongBaoNguoiDungs.AddAsync(tb);

                        e.TrangThai = 1;
                        _context.Emails.Update(e);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi gửi nhắc nhở");
                }
            }
        }

        public async Task CapNhatTrangThaiTreHan()
        {
            var treHanList = await (from cv in _context.ChiTietCongViecs
                                    where (cv.TrangThai == 0 || cv.TrangThai == 1)
                                        && DateTime.Now.Date > cv.NgayKetThucCongViec.Value.AddDays(1).Date
                                    select cv)
                                .ToListAsync();

            foreach (var cv in treHanList)
            {
                cv.TrangThai = 3;
            }

            _context.ChiTietCongViecs.UpdateRange(treHanList);
            await _context.SaveChangesAsync();
        }
    }

}