using APIServerApp.Context;
using APIServerApp.Model;
using Microsoft.EntityFrameworkCore;

namespace APIServerApp.services
{
    public interface IEmailService
    {
        Task<List<NguoiNhanEmail>> GetListNguoiNhanEmail(string maEmail);
        Task<List<TepDinhKemEmail>> GetTepDinhKemEmailByMaCongViec(string maEmail);
    }

    public class EmailService : IEmailService
    {
        private readonly AppDbContext _context;

        public EmailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<NguoiNhanEmail>> GetListNguoiNhanEmail(string maEmail)
        {
            return await _context.NguoiNhanEmails
            .Where(x => x.MaEmail == maEmail)
            .ToListAsync();
        }

        public async Task<List<TepDinhKemEmail>> GetTepDinhKemEmailByMaCongViec(string maEmail)
        {
            return await _context.TepDinhKemEmails
            .Where(x => x.MaEmail == maEmail)
            .ToListAsync();
        }

    }

}