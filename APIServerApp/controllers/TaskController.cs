using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIServerApp.Context;
using APIServerApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIServerApp.Model;

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

        [HttpPost("tao-cong-viec")]
        public async Task<IActionResult> TaoCongViec(
            [FromBody] CongViecRequestDto request,
            [FromQuery] NguoiGiaoRequestDto nguoiGiaoInfo)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // --- Tạo mã công việc ---
                string prefix = (nguoiGiaoInfo.MaDonVi ?? "") + (nguoiGiaoInfo.MaPhongBan ?? "");
                var lastMaCV = await _context.CongViecs
                    .Where(cv => cv.MaCongViec.StartsWith(prefix))
                    .OrderByDescending(cv => cv.MaCongViec)
                    .Select(cv => cv.MaCongViec)
                    .FirstOrDefaultAsync();

                int newStt = 1;
                if (!string.IsNullOrEmpty(lastMaCV))
                {
                    var parts = lastMaCV.Split('_');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int lastStt))
                    {
                        newStt = lastStt + 1;
                    }
                }
                string maCongViec = $"{prefix}_{newStt}";

                // --- Lưu Công Việc ---
                var cv = new CongViec
                {
                    MaCongViec = maCongViec,
                    NguoiGiao = request.NguoiGiao,
                    NgayGiao = request.NgayGiao,
                    LapLai = request.LapLai,
                    TanSuat = request.TanSuat ?? "NONE",
                    NgayBatDau = request.NgayBatDau,
                    NgayKetThuc = request.NgayKetThuc
                };
                _context.CongViecs.Add(cv);

                // --- Lưu Chi Tiết Công Việc + liên quan ---
                if (request.ChiTietCongViecs != null)
                {
                    foreach (var ctDto in request.ChiTietCongViecs)
                    {
                        var ct = new ChiTietCongViec
                        {
                            MaCongViec = maCongViec,
                            TieuDe = ctDto.TieuDe,
                            NoiDung = ctDto.NoiDung,
                            NgayNhanCongViec = ctDto.NgayNhanCongViec,
                            NgayKetThucCongViec = ctDto.NgayKetThucCongViec,
                            NgayHoanThanh = ctDto.NgayHoanThanh,
                            SoNgayHoanThanh = ctDto.SoNgayHoanThanh,
                            TrangThai = ctDto.TrangThai,
                            TienDo = ctDto.TienDo,
                            MucDoUuTien = ctDto.MucDoUuTien
                        };
                        _context.ChiTietCongViecs.Add(ct);

                        // NguoiLienQuan
                        if (ctDto.NguoiLienQuans != null)
                        {
                            foreach (var nlq in ctDto.NguoiLienQuans)
                            {
                                _context.NguoiLienQuanCongViecs.Add(new NguoiLienQuanCongViec
                                {
                                    MaCongViec = maCongViec,
                                    MaNguoiDung = nlq.MaNguoiDung,
                                    VaiTro = nlq.VaiTro
                                });
                            }
                        }

                        // Email + NguoiNhan + TepDinhKem
                        if (ctDto.Emails != null)
                        {
                            foreach (var emailDto in ctDto.Emails)
                            {
                                var email = new Email
                                {
                                    MaEmail = Guid.NewGuid().ToString(),
                                    NguoiGui = emailDto.NguoiGui,
                                    MaChiTietCV = ct.MaChiTietCV,
                                    TieuDe = emailDto.TieuDe,
                                    NoiDung = emailDto.NoiDung,
                                    NgayGui = emailDto.NgayGui,
                                    TrangThai = emailDto.TrangThai
                                };
                                _context.Emails.Add(email);

                                if (emailDto.NguoiNhans != null)
                                {
                                    foreach (var nn in emailDto.NguoiNhans)
                                    {
                                        _context.NguoiNhanEmails.Add(new NguoiNhanEmail
                                        {
                                            MaEmail = email.MaEmail,
                                            MaNguoiDung = nn.MaNguoiDung,
                                            VaiTro = nn.VaiTro
                                        });
                                    }
                                }

                                if (emailDto.TepDinhKems != null)
                                {
                                    foreach (var tep in emailDto.TepDinhKems)
                                    {
                                        var tepTin = new TepTin
                                        {
                                            TenTep = tep.TenTep,
                                            TenTepGoc = tep.TenTepGoc,
                                            DuongDan = tep.DuongDan
                                        };
                                        _context.TepTins.Add(tepTin);

                                        _context.TepDinhKemEmails.Add(new TepDinhKemEmail
                                        {
                                            MaEmail = email.MaEmail,
                                            MaTep = tepTin.MaTep
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                // Lưu tất cả cùng lúc
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ApiResponseDto { Success = true, Message = "Tạo công việc thành công" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new ApiResponseDto { Success = false, Message = ex.Message });
            }
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

            return Ok(new GetNguoiDungCungDonViPhongBanResponse
            {
                Success = true,
                Message = "Lấy danh sách người dùng thành công",
                NguoiDungs = rel
            });
        }


    }
}