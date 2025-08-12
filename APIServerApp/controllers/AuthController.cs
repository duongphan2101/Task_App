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
            MaNguoiDung = user.MaNguoiDung,
            HoTen = user.HoTen,
            Email = user.Email,
            MaPhongBan = user.PhongBan.TenPhongBan,
            MaDonVi = user.DonVi.TenDonVi,
            MaChucVu = user.ChucVu.TenChucVu,
            LaLanhDao = user.LaLanhDao
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
