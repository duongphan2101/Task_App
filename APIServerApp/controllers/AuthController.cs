using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIServerApp.Context;
using APIServerApp.Model;
using APIServerApp.DTO;
using APIServerApp.Helper;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Console.WriteLine($"Đang login: {request.Email}");
        // Console.WriteLine($"Đang login: {request.MatKhau}");

        // Console.WriteLine("MK đã mã hóa: " + PasswordHasher.Hash(request.MatKhau));

        var user = await _context.NguoiDungs
            .Include(u => u.PhongBan)
            .Include(u => u.DonVi)
            .Include(u => u.ChucVu)
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        // Console.WriteLine($"User có tồn tại: {user != null}");

        if (user == null || !PasswordHasher.Verify(user.MatKhau, request.MatKhau))
        {
            Console.WriteLine("Mật khẩu sai");
            return Unauthorized("Email hoặc mật khẩu không đúng.");
        }

        var token = GenerateJwtToken(user);
        Console.WriteLine("Đăng nhập thành công!");

        return Ok(new LoginReponse
        {
            Token = token,
            UserId = user.MaNguoiDung
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] NguoiDung request)
    {

        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.MatKhau))
            return BadRequest("Email và mật khẩu không được để trống.");

        var existingUser = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
            return BadRequest("Email đã được sử dụng.");

        // Mã hóa mật khẩu
        request.MatKhau = PasswordHasher.Hash(request.MatKhau);

        _context.NguoiDungs.Add(request);
        await _context.SaveChangesAsync();

        return Ok(new ApiResponseDto { Message = "Đăng ký thành công!", Success = true });
    }

    [HttpGet("get-account-inactive")]
    public async Task<IActionResult> GetAccountInactive()
    {
        var accs = await _context.NguoiDungs
            .Where(u => u.TrangThai == 0)
            .Select(u => new
            {
                u.MaNguoiDung,
                u.HoTen,
                u.Email,
                DonVi = new
                {
                    MaDonVi = u.MaDonVi,
                    TenDonVi = u.DonVi.TenDonVi
                },
                PhongBan = new
                {
                    MaPhongBan = u.MaPhongBan,
                    TenPhongBan = u.PhongBan.TenPhongBan
                },
                ChucVu = new
                {
                    MaChucVu = u.MaChucVu,
                    TenChucVu = u.ChucVu.TenChucVu
                },

                MaChucVu = u.MaChucVu,
                MaDonVi = u.MaDonVi,
                MaPhongBan = u.MaPhongBan

            })
            .ToListAsync();

        return Ok(new
        {
            Success = true,
            Message = accs.Count > 0 ? "Lấy danh sách người dùng chưa xác nhận" : "Không có người dùng chưa xác nhận",
            Data = accs
        });
    }



    private string GenerateJwtToken(NguoiDung user)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.MaNguoiDung.ToString()),
            new Claim(ClaimTypes.Name, user.HoTen ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("MaPhongBan", user.MaPhongBan ?? ""),
            new Claim("MaDonVi", user.MaDonVi ?? ""),
            new Claim("MaChucVu", user.MaChucVu ?? "")
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
