using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIServerApp.Context;
using APIServerApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIServerApp.Model;
using DotNetEnv;
using MimeKit;
using System.Text.RegularExpressions;
using System.IO.Compression;

namespace APIServerApp.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public TaskController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("da-giao/{id}")]
        public async Task<IActionResult> GetCongViecDaGiaoByUserId(int id, bool sortByDate)
        {
            var query = _context.CongViecs
                .Where(cv => cv.NguoiGiao == id)
                .OrderByDescending(cv => cv.NgayGiao)
                .Select(cv => new CongViecDaGiaoDto
                {
                    MaCongViec = cv.MaCongViec,
                    NgayGiao = cv.NgayGiao,
                    LapLai = cv.LapLai == true ? "Có" : "Không",
                    TanSuat = cv.TanSuat,
                    ChiTiet = cv.ChiTietCongViecs
                        // .Where(ct => ct.Emails.Any(e => e.TrangThai == 1))
                        .Select(ct => new ChiTietCongViecDto
                        {
                            NgayNhanCongViec = ct.NgayNhanCongViec,
                            NgayKetThucCongViec = ct.NgayKetThucCongViec,
                            MaChiTietCV = ct.MaChiTietCV,
                            TieuDe = ct.TieuDe,
                            NoiDung = ct.NoiDung,
                            NgayHoanThanh = ct.NgayHoanThanh,
                            SoNgayHoanThanh = ct.SoNgayHoanThanh,
                            TrangThai = ct.TrangThai,
                            TienDo = ct.TienDo,
                            MucDoUuTien = ct.MucDoUuTien
                        }).ToList(),
                    NguoiNhan = cv.NguoiLienQuanCongViecs
                        .Where(nlcq => nlcq.VaiTro == "to")
                        .Select(nlcq => new NguoiNhanDto
                        {
                            MaNguoiDung = nlcq.NguoiDung.MaNguoiDung,
                            HoTen = nlcq.NguoiDung.HoTen,
                            Email = nlcq.NguoiDung.Email,
                            TenDonVi = nlcq.NguoiDung.DonVi.TenDonVi,
                            TenPhongBan = nlcq.NguoiDung.PhongBan.TenPhongBan,
                            TenChucVu = nlcq.NguoiDung.ChucVu.TenChucVu
                        }).FirstOrDefault()
                });

            // Sort theo bool
            query = sortByDate
                ? query.OrderByDescending(cv => cv.ChiTiet.FirstOrDefault().TrangThai)
                : query.OrderByDescending(cv => cv.NgayGiao);

            var result = await query.ToListAsync();

            var response = new CongViecDaGiaoResponseDto
            {
                Success = true,
                Message = "Lấy danh sách công việc đã giao thành công",
                Data = result
            };

            return Ok(response);
        }

        [HttpGet("get-nguoi-dung/{id}")]
        public async Task<IActionResult> GetNguoiDungByUserId(int id)
        {
            var query = await _context.NguoiDungs
                .Where(cv => cv.MaNguoiDung == id)
                .Select(cv => new NguoiDung
                {
                    MaNguoiDung = id,
                    HoTen = cv.HoTen,
                    Email = cv.Email,
                    MaChucVu = cv.MaChucVu,
                    ChucVu = cv.ChucVu,
                    DonVi = cv.DonVi,
                    MaDonVi = cv.MaDonVi,
                    PhongBan = cv.PhongBan,
                    MaPhongBan = cv.MaPhongBan,
                    LaLanhDao = cv.LaLanhDao,
                    MatKhau = ""
                }).FirstOrDefaultAsync();


            var response = new Object_Response<NguoiDung>
            {
                Success = true,
                Message = "Lấy danh sách công việc đã giao thành công",
                Data = query
            };

            return Ok(response);
        }

        [HttpGet("duoc-giao/{id}")]
        public async Task<IActionResult> GetCongViecDuocGiaoByUserId(int id, bool sortByDate)
        {
            var query = _context.CongViecs
                .Where(cv => cv.NguoiLienQuanCongViecs
                    .Any(nlq => nlq.MaNguoiDung == id && nlq.VaiTro == "to"))
                .Select(cv => new CongViecDuocGiaoDto
                {
                    MaCongViec = cv.MaCongViec,
                    NgayGiao = cv.NgayGiao,
                    LapLai = cv.LapLai == true ? "Có" : "Không",
                    TanSuat = cv.TanSuat,
                    ChiTiet = cv.ChiTietCongViecs
                        .Select(ct => new ChiTietCongViecDto
                        {
                            NgayNhanCongViec = ct.NgayNhanCongViec,
                            NgayKetThucCongViec = ct.NgayKetThucCongViec,
                            MaChiTietCV = ct.MaChiTietCV,
                            TieuDe = ct.TieuDe,
                            NoiDung = ct.NoiDung,
                            NgayHoanThanh = ct.NgayHoanThanh,
                            SoNgayHoanThanh = ct.SoNgayHoanThanh,
                            TrangThai = ct.TrangThai,
                            TienDo = ct.TienDo,
                            MucDoUuTien = ct.MucDoUuTien
                        }).ToList(),
                    NguoiGiao = cv.NguoiGiao,
                    NguoiLienQuanCongViec = cv.NguoiLienQuanCongViecs
                        .Where(nlq => nlq.VaiTro == "to" && nlq.MaNguoiDung == id)
                        .Select(nlq => new NguoiLienQuanCongViecDto
                        {
                            MaCongViec = cv.MaCongViec,
                            MaNguoiDung = id,
                            VaiTro = nlq.VaiTro
                        }).FirstOrDefault()
                });

            // Sort theo bool
            query = sortByDate
                ? query.OrderByDescending(cv => cv.ChiTiet.FirstOrDefault().TrangThai)
                : query.OrderByDescending(cv => cv.NgayGiao);

            var result = await query.ToListAsync();

            return Ok(new CongViecDuocGiaoResponseDto
            {
                Success = true,
                Message = "Lấy danh sách công việc được giao thành công",
                Data = result
            });
        }

        [HttpGet("chi-tiet-cong-viec/{maChiTietCV}")]
        public async Task<IActionResult> GetCongViecByMaChiTiet(int maChiTietCV)
        {
            var query = _context.ChiTietCongViecs
                .Where(ct => ct.MaChiTietCV == maChiTietCV)
                .Select(ct => new ChiTietCongViecFullDto
                {
                    MaCongViec = ct.MaCongViec,
                    NgayGiao = ct.CongViec.NgayGiao,
                    NguoiGiao_HoTen = ct.CongViec.NguoiGiaoObj.HoTen,
                    LapLai = ct.CongViec.LapLai == true ? "Có" : "Không",
                    TanSuat = ct.CongViec.TanSuat,
                    NgayNhanCongViec = ct.NgayNhanCongViec,
                    NgayKetThucCongViec = ct.NgayKetThucCongViec,
                    MaChiTietCV = ct.MaChiTietCV,
                    TieuDe = ct.TieuDe,
                    NoiDung = ct.NoiDung,
                    NgayHoanThanh = ct.NgayHoanThanh,
                    SoNgayHoanThanh = ct.SoNgayHoanThanh,
                    TrangThai = ct.TrangThai,
                    TienDo = ct.TienDo,
                    MucDoUuTien = ct.MucDoUuTien,
                    NguoiNhan = ct.CongViec.NguoiLienQuanCongViecs
                        .Where(nlq => nlq.VaiTro == "to")
                        .Select(nlq => new NguoiNhanDto
                        {
                            MaNguoiDung = nlq.NguoiDung.MaNguoiDung,
                            HoTen = nlq.NguoiDung.HoTen,
                            Email = nlq.NguoiDung.Email,
                            TenChucVu = nlq.NguoiDung.ChucVu.TenChucVu,
                            TenDonVi = nlq.NguoiDung.DonVi.TenDonVi,
                            TenPhongBan = nlq.NguoiDung.PhongBan.TenPhongBan
                        }).FirstOrDefault(),
                });

            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound(new ChiTietCongViecResponse
                {
                    Success = false,
                    Message = "Không tìm thấy công việc."
                });
            }

            return Ok(new ChiTietCongViecResponse
            {
                Success = true,
                Message = "Lấy chi tiết công việc thành công",
                Data = result
            });
        }

        [HttpGet("chitiet-congviec/{maChiTietCV}")]
        public async Task<IActionResult> GetChiTiet(int maChiTietCV)
        {
            var query = _context.ChiTietCongViecs
                .Where(ct => ct.MaChiTietCV == maChiTietCV)
                .Select(ct => new ChiTietCongViec
                {
                    MaChiTietCV = ct.MaChiTietCV,
                    TienDo = ct.TienDo,
                    TieuDe = ct.TieuDe,
                    NoiDung = ct.NoiDung,
                    NgayHoanThanh = ct.NgayHoanThanh,
                    NgayNhanCongViec = ct.NgayNhanCongViec,
                    NgayKetThucCongViec = ct.NgayKetThucCongViec,
                    SoNgayHoanThanh = ct.SoNgayHoanThanh,
                    CongViec = ct.CongViec,
                    MaCongViec = ct.MaCongViec,
                    MucDoUuTien = ct.MucDoUuTien
                });

            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound(new Object_Response<ChiTietCongViec>
                {
                    Success = false,
                    Message = "Không tìm thấy công việc."
                });
            }

            return Ok(new Object_Response<ChiTietCongViec>
            {
                Success = true,
                Message = "Lấy chi tiết công việc thành công",
                Data = result
            });
        }


        [HttpGet("nguoi-lien-quan/{maCongViec}")]
        public async Task<IActionResult> GetNguoiLienQuanCongViec(string maCongViec)
        {
            var query = _context.NguoiLienQuanCongViecs
                .Where(nlq => nlq.MaCongViec == maCongViec)
                .Select(nlq => new NguoiLienQuanCongViecDTO
                {
                    MaNguoiDung = nlq.NguoiDung.MaNguoiDung,
                    HoTen = nlq.NguoiDung.HoTen,
                    Email = nlq.NguoiDung.Email,
                    TenDonVi = nlq.NguoiDung.DonVi != null ? nlq.NguoiDung.DonVi.TenDonVi : null,
                    TenPhongBan = nlq.NguoiDung.PhongBan != null ? nlq.NguoiDung.PhongBan.TenPhongBan : null,
                    TenChucVu = nlq.NguoiDung.ChucVu != null ? nlq.NguoiDung.ChucVu.TenChucVu : null,
                    VaiTro = nlq.VaiTro
                });

            var result = await query.ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound(new NguoiLienQuanCongViecResponse
                {
                    Success = false,
                    Message = "Không tìm thấy người liên quan trong công việc này"
                });
            }

            return Ok(new NguoiLienQuanCongViecResponse
            {
                Success = true,
                Message = "Lấy danh sách người liên quan thành công",
                Data = result
            });
        }

        [HttpPost("update-trang-thai")]
        public async Task<IActionResult> UpdateTrangThaiCongViecAsync([FromBody] ChiTietCongViec ct)
        {
            var chiTiet = await _context.ChiTietCongViecs
                .FirstOrDefaultAsync(x => x.MaChiTietCV == ct.MaChiTietCV);

            if (chiTiet == null)
            {
                return NotFound(new ApiResponseDto
                {
                    Success = false,
                    Message = "Không tìm thấy công việc"
                });
            }

            chiTiet.TrangThai = ct.TrangThai;
            chiTiet.TienDo = ct.TienDo;
            chiTiet.NgayHoanThanh = ct.NgayHoanThanh;

            await _context.SaveChangesAsync();

            return Ok(new ApiResponseDto
            {
                Success = true,
                Message = "Cập nhật trạng thái công việc thành công"
            });
        }

        [HttpPost("update-trang-thai-email")]
        public async Task<IActionResult> UpdateTrangThaiEmail([FromBody] Email e)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(x => x.MaEmail == e.MaEmail);

            if (email == null)
            {
                return NotFound(new ApiResponseDto
                {
                    Success = false,
                    Message = "Không tìm thấy Email"
                });
            }

            email.TrangThai = e.TrangThai;

            await _context.SaveChangesAsync();

            return Ok(new ApiResponseDto
            {
                Success = true,
                Message = "Cập nhật trạng thái Email thành công"
            });
        }

        [HttpGet("tep/{maTepTin}")]
        public async Task<IActionResult> GetTepTin(int maTepTin)
        {
            var tepTin = await _context.TepTins
                .FirstOrDefaultAsync(tt => tt.MaTep == maTepTin);

            if (tepTin == null)
            {
                return Ok(new TepTinResponse
                {
                    Success = false,
                    Message = "Không tìm thấy tệp tin",
                    TepTin = null
                });
            }

            return Ok(new TepTinResponse
            {
                Success = true,
                Message = "Lấy tệp tin thành công",
                TepTin = tepTin
            });
        }

        [HttpGet("tep-dinh-kem-email/{maCongViec}")]
        public async Task<GetTepDinhKemResponse> GetTepDinhKemEmailByMaCongViecAsync(string maCongViec)
        {
            var tepDinhKemList = await _context.TepDinhKemEmails
                .Include(tdk => tdk.Email)
                    .ThenInclude(e => e.ChiTietCongViec)
                .Include(tdk => tdk.TepTin)
                .Where(tdk => tdk.Email.ChiTietCongViec.MaCongViec == maCongViec)
                .ToListAsync();

            return new GetTepDinhKemResponse
            {
                Success = true,
                Message = tepDinhKemList.Any() ? "Lấy tệp đính kèm thành công" : "Không có tệp đính kèm cho công việc này",
                TepDinhKemEmail = tepDinhKemList
            };
        }

        [HttpGet("phan-hoi-cong-viec/{maCongViec}")]
        public async Task<IActionResult> GetPhanHoiByMaCongViec(string maCongViec)
        {
            var phanHoiList = await _context.PhanHoiCongViecs
                .Where(p => p.MaCongViec == maCongViec)
                .Include(p => p.NguoiDung)
                .ToListAsync();

            return Ok(new FeedBackResponse
            {
                Success = true,
                Message = phanHoiList.Any() ? "Lấy danh sách phản hồi thành công" : "Không có phản hồi nào cho công việc này",
                PhanHoiCongViec = phanHoiList
            });
        }

        [HttpPost("user-list-donvi-phongban")]
        public async Task<IActionResult> GetNguoiDungCungDonViPhongBanAsync([FromBody] DonViPhongBanRequest request)
        {
            var nguoiDungs = await _context.NguoiDungs
                .Where(nd => nd.MaDonVi == request.MaDonVi
                          && nd.MaPhongBan == request.MaPhongBan
                          && nd.Email != request.Email)
                .ToListAsync();

            var rel = nguoiDungs.Select(nd => new NguoiDungDTO
            {
                MaNguoiDung = nd.MaNguoiDung,
                HoTen = nd.HoTen,
                Email = nd.Email
            }).ToList();

            foreach (var i in rel)
            {
                Console.WriteLine("Email lay duoc " + i.Email);
            }

            return Ok(new GetNguoiDungCungDonViPhongBanResponse
            {
                Success = true,
                Message = "Lấy danh sách người dùng thành công",
                NguoiDungs = rel ?? new List<NguoiDungDTO>()
            });
        }

        [HttpPost("tao-cong-viec")]
        public async Task<IActionResult> TaoCongViec([FromBody] TaoCongViecRequest request)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nguoiGiaoInfo = request.NguoiDung;
                var cvRequest = request.CongViec;

                // --- Tạo mã công việc ---
                string prefix = (nguoiGiaoInfo.MaDonVi ?? "") + (nguoiGiaoInfo.MaPhongBan ?? "");

                // Lấy danh sách mã bắt đầu bằng prefix, đưa về bộ nhớ, rồi tách số
                var lastMaCV = await _context.CongViecs
                    .Where(cv => cv.MaCongViec.StartsWith(prefix))
                    .Select(cv => cv.MaCongViec)
                    .ToListAsync();

                int lastStt = lastMaCV
                    .Select(ma =>
                    {
                        if (!string.IsNullOrEmpty(ma) && ma.Contains("_"))
                        {
                            var part = ma.Split('_').Last();
                            return int.TryParse(part, out var num) ? num : 0;
                        }
                        return 0;
                    })
                    .OrderByDescending(num => num)
                    .FirstOrDefault();

                int newStt = lastStt + 1;
                string maCongViec = $"{prefix}_{newStt}";

                // --- Lưu Công Việc ---
                var cv = new CongViec
                {
                    MaCongViec = maCongViec,
                    NguoiGiao = nguoiGiaoInfo.MaNguoiDung,
                    NgayGiao = cvRequest.NgayGiao,
                    LapLai = cvRequest.LapLai,
                    TanSuat = cvRequest.TanSuat ?? "NONE",
                    NgayBatDau = cvRequest.NgayBatDau,
                    NgayKetThuc = cvRequest.NgayKetThuc,
                };
                _context.CongViecs.Add(cv);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new Object_Response<string> { Success = true, Message = "Tạo công việc thành công", Data = cv.MaCongViec });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("tao-chi-tiet-cong-viec")]
        public async Task<IActionResult> TaoChiTietCongViec([FromBody] ChiTietCongViec ct)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Kiểm tra MaCongViec có tồn tại không
                var congViec = await _context.CongViecs
                    .FirstOrDefaultAsync(c => c.MaCongViec == ct.MaCongViec);

                if (congViec == null)
                {
                    return BadRequest(new ApiResponseDto
                    {
                        Success = false,
                        Message = $"Mã công việc {ct.MaCongViec} không tồn tại."
                    });
                }

                // 2. Tạo chi tiết công việc
                var ctcv = new ChiTietCongViec
                {
                    MaCongViec = ct.MaCongViec,
                    NgayNhanCongViec = ct.NgayNhanCongViec,
                    NgayKetThucCongViec = ct.NgayKetThucCongViec,
                    NgayHoanThanh = ct.NgayHoanThanh,
                    SoNgayHoanThanh = ct.SoNgayHoanThanh,
                    TienDo = ct.TienDo,
                    MucDoUuTien = ct.MucDoUuTien,
                    TieuDe = ct.TieuDe,
                    NoiDung = ct.NoiDung,
                    TrangThai = ct.TrangThai
                };

                _context.ChiTietCongViecs.Add(ctcv);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new Object_Response<int>
                {
                    Success = true,
                    Message = "Tạo Chi Tiết công việc thành công",
                    Data = ctcv.MaChiTietCV
                });
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto
                {
                    Success = false,
                    Message = $"Lỗi khi lưu dữ liệu: {dbEx.InnerException?.Message ?? dbEx.Message}"
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}"
                });
            }
        }

        [HttpPost("tao-tep-tin")]
        public async Task<IActionResult> TaoTepTin([FromBody] TepTin tep)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var tt = new TepTin
                {
                    TenTep = tep.TenTep,
                    DuongDan = tep.DuongDan,
                    TenTepGoc = tep.TenTepGoc,

                    TepDinhKemEmails = tep.TepDinhKemEmails
                };

                _context.TepTins.Add(tt);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new Object_Response<int> { Success = true, Message = "Tạo Tep thành công", Data = tt.MaTep });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new Object_Response<int> { Success = false, Message = ex.Message, Data = 0 });
            }
        }

        [HttpPost("tao-nguoi-lien-quan-cong-viec")]
        public async Task<IActionResult> TaoNguoiLienQuanCongViec([FromBody] NguoiLienQuanCongViec nlq)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var n = new NguoiLienQuanCongViec
                {
                    MaCongViec = nlq.MaCongViec,
                    MaNguoiDung = nlq.MaNguoiDung,
                    VaiTro = nlq.VaiTro,
                };

                _context.NguoiLienQuanCongViecs.Add(n);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ApiResponseDto { Success = true, Message = "Tạo NLQCV thành công" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("tao-email")]
        public async Task<IActionResult> TaoEmail([FromBody] Email e)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var email = new Email
                {
                    MaEmail = e.MaEmail,
                    TieuDe = e.TieuDe,
                    NoiDung = e.NoiDung,
                    NgayGui = e.NgayGui,
                    NguoiGui = e.NguoiGui,
                    TrangThai = e.TrangThai,
                    MaChiTietCV = e.MaChiTietCV,

                    TepDinhKemEmails = [],
                    NguoiNhanEmails = [],
                };

                _context.Emails.Add(email);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ApiResponseDto { Success = true, Message = "Tạo Email thành công" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("tao-nguoi-nhan-email")]
        public async Task<IActionResult> TaoNguoiNhanEmail([FromBody] NguoiNhanEmail nne)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var n = new NguoiNhanEmail
                {
                    MaNguoiDung = nne.MaNguoiDung,
                    MaEmail = nne.MaEmail,
                    VaiTro = nne.VaiTro,
                };

                _context.NguoiNhanEmails.Add(n);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ApiResponseDto { Success = true, Message = "Tạo NNE thành công" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("tao-tep-dinh-kem")]
        public async Task<IActionResult> TaoTepDinhKem([FromBody] TepDinhKemEmail tdk)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var n = new TepDinhKemEmail
                {
                    MaEmail = tdk.MaEmail,
                    MaTep = tdk.MaTep,
                };

                _context.TepDinhKemEmails.Add(n);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ApiResponseDto { Success = true, Message = "Tạo TDK thành công" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("tao-thong-bao")]
        public async Task<IActionResult> TaoThongBao([FromBody] ThongBaoNguoiDung tb)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var n = new ThongBaoNguoiDung
                {
                    NoiDung = tb.NoiDung,
                    MaChiTietCV = tb.MaChiTietCV,
                    MaNguoiDung = tb.MaNguoiDung,
                    TrangThai = tb.TrangThai,
                    NgayThongBao = tb.NgayThongBao,
                };

                _context.ThongBaoNguoiDungs.Add(n);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ApiResponseDto { Success = true, Message = "Tạo ThongBao thành công" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("tao-phan-hoi-cong-viec")]
        public async Task<IActionResult> TaoPhanHoiCongViec([FromBody] PhanHoiCongViec phcv)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var n = new PhanHoiCongViec
                {
                    MaCongViec = phcv.MaCongViec,
                    MaNguoiDung = phcv.MaNguoiDung,
                    NoiDung = phcv.NoiDung,
                    ThoiGian = phcv.ThoiGian,
                    Loai = phcv.Loai,
                };

                _context.PhanHoiCongViecs.Add(n);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ApiResponseDto { Success = true, Message = "Tạo PHCV thành công" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("get-nguoi-dung-by-email")]
        public async Task<IActionResult> GetNguoiDungByEmail([FromBody] List<string>? emails)
        {
            // if (emails == null || emails.Count == 0)
            // {
            //     return BadRequest(new Object_Response<List<NguoiDung>>
            //     {
            //         Success = false,
            //         Message = "Danh sách email trống",
            //         Data = new List<NguoiDung>()
            //     });
            // }

            var nguoiDungs = await _context.NguoiDungs
                .Where(nd => emails.Contains(nd.Email))
                .Select(nd => new NguoiDung
                {
                    MaNguoiDung = nd.MaNguoiDung,
                    HoTen = nd.HoTen,
                    Email = nd.Email,
                    MaChucVu = nd.MaChucVu,
                    ChucVu = nd.ChucVu,
                    PhongBan = nd.PhongBan,
                    MaPhongBan = nd.MaPhongBan,
                    DonVi = nd.DonVi,
                    MaDonVi = nd.MaDonVi,
                })
                .ToListAsync();

            return Ok(new Object_Response<List<NguoiDung>>
            {
                Success = true,
                Message = nguoiDungs.Any()
                    ? "Lấy danh sách người dùng thành công"
                    : "Không tìm thấy người dùng nào",
                Data = nguoiDungs
            });
        }

        [HttpPost("gui-email")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest req)
        {
            if (req.Email == null || req.DanhSachNguoiNhanEmail == null || req.CurrentUser == null)
            {
                return BadRequest(new ApiResponseDto
                {
                    Success = false,
                    Message = "Thiếu thông tin gửi email"
                });
            }

            try
            {
                var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
                Env.Load(envPath);
                // Env.Load("../../.env");
                string CLIENT = Env.GetString("SMTP_CLIENT");
                int PORT = Convert.ToInt32(Env.GetString("SMTP_PORT"));

                var message = new MimeMessage();

                // From
                message.From.Add(new MailboxAddress("", req.CurrentUser.Email));
                message.Subject = req.Email.TieuDe ?? "";

                // To / Cc / Bcc
                foreach (var nguoiNhan in req.DanhSachNguoiNhanEmail)
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

                // Body
                var builder = new BodyBuilder
                {
                    HtmlBody = req.Email.NoiDung ?? "",
                    TextBody = Regex.Replace(req.Email.NoiDung ?? "", "<.*?>", string.Empty)
                };

                // File đính kèm
                if (req.DanhSachTepDinhKem != null && req.DanhSachTepDinhKem.Count > 0)
                {
                    foreach (var tep in req.DanhSachTepDinhKem)
                    {
                        string path = tep.TepTin?.DuongDan;
                        string tenTepGoc = tep.TepTin?.TenTepGoc;

                        if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                        {
                            builder.Attachments.Add(new MimePart()
                            {
                                Content = new MimeContent(System.IO.File.OpenRead(path)),
                                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                                ContentTransferEncoding = ContentEncoding.Base64,
                                FileName = tenTepGoc
                            });
                        }
                    }
                }

                message.Body = builder.ToMessageBody();

                Console.WriteLine(req.CurrentUser.Email + " " + req.CurrentUser.MatKhau);
                // Gửi qua SMTP
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(CLIENT, PORT, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(req.CurrentUser.Email, req.CurrentUser.MatKhau);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                // using (var client = new MailKit.Net.Smtp.SmtpClient())
                // {
                //     client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //     await client.ConnectAsync(CLIENT, PORT, MailKit.Security.SecureSocketOptions.StartTls);
                //     await client.AuthenticateAsync("test@intimexhcm.com", "TestInt2025@");
                //     await client.SendAsync(message);
                //     await client.DisconnectAsync(true);
                // }



                // Export .eml -> .zip
                string targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Attachments");
                if (!Directory.Exists(targetFolder))
                    Directory.CreateDirectory(targetFolder);

                string tempFolder = Path.Combine(Path.GetTempPath(), $"EmailTemp_{Guid.NewGuid()}");
                Directory.CreateDirectory(tempFolder);

                string safeSubject = SanitizeFileName(req.Email.TieuDe ?? "email");
                string emlFileName = $"{safeSubject}_{DateTime.Now:yyyyMMdd_HHmmss}.eml";
                string emlPath = Path.Combine(tempFolder, emlFileName);

                using (var stream = System.IO.File.Create(emlPath))
                {
                    await message.WriteToAsync(stream);
                }

                string zipPath = Path.Combine(targetFolder, Path.GetFileNameWithoutExtension(emlFileName) + ".zip");

                if (System.IO.File.Exists(zipPath))
                    System.IO.File.Delete(zipPath);

                ZipFile.CreateFromDirectory(tempFolder, zipPath, CompressionLevel.Optimal, includeBaseDirectory: false);

                Directory.Delete(tempFolder, true);

                return Ok(new ApiResponseDto
                {
                    Success = true,
                    Message = "Gửi Email thành công"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto
                {
                    Success = false,
                    Message = "Lỗi khi gửi email: " + ex.Message
                });
            }
        }

        private string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }

    }
}