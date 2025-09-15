using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using APIServerApp.Context;
using APIServerApp.Model;

namespace APIServerApp.Services
{
    public class EmailIdleService : BackgroundService
    {
        private readonly string _host = "imap.gmail.com";
        private readonly int _port = 993;
        private readonly string _username = "duong2101.test@gmail.com";
        private readonly string _password = "ufns jzmt difo offq";
        private readonly object _imapLock = new object();
        private readonly IServiceScopeFactory _scopeFactory;

        public EmailIdleService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var client = new ImapClient();

            await client.ConnectAsync(_host, _port, SecureSocketOptions.SslOnConnect, stoppingToken);
            await client.AuthenticateAsync(_username, _password, stoppingToken);

            var inbox = client.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadWrite, stoppingToken);
            Console.WriteLine($"📥 Inbox has {inbox.Count} messages");

            inbox.CountChanged += async (s, e) =>
            {
                try
                {
                    MimeMessage msg = null;

                    // Không lấy message trực tiếp từ client đang idle
                    lock (_imapLock)
                    {
                        if (inbox.Count == 0) return;
                    }

                    // Tạo client mới để fetch message
                    using var fetchClient = new ImapClient();
                    await fetchClient.ConnectAsync(_host, _port, SecureSocketOptions.SslOnConnect);
                    await fetchClient.AuthenticateAsync(_username, _password);
                    await fetchClient.Inbox.OpenAsync(FolderAccess.ReadWrite);

                    msg = await fetchClient.Inbox.GetMessageAsync(fetchClient.Inbox.Count - 1);

                    await ProcessEmail(msg);

                    await fetchClient.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Inbox fetch error: {ex.Message}");
                }
            };


            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Wrap idle trong lock nếu muốn đảm bảo không bị truy cập đồng thời
                    lock (_imapLock)
                    {
                        client.Idle(stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Idle error: {ex.Message}");
                    await Task.Delay(5000, stoppingToken);
                }
            }
        }

        private async Task ProcessEmail(MimeMessage msg)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var fromAddress = msg.From.Mailboxes.FirstOrDefault()?.Address;
            if (string.IsNullOrEmpty(fromAddress))
            {
                Console.WriteLine("❌ Email không có sender, bỏ qua.");
                return;
            }
            var lowerEmail = fromAddress.ToLower();

            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.Email.ToLower() == lowerEmail);


            if (nguoiDung == null)
            {
                Console.WriteLine($"⚠️ Không tìm thấy người dùng khớp với email {fromAddress}");
                return;
            }

            var chiTietCV = await _context.ChiTietCongViecs
                .Where(c => c.CongViec.NguoiGiao == nguoiDung.MaNguoiDung)
                .OrderByDescending(c => c.CongViec.NgayGiao)
                .FirstOrDefaultAsync();

            if (chiTietCV == null)
            {
                Console.WriteLine($"⚠️ Người dùng {nguoiDung.Email} chưa có task được giao, bỏ qua.");
                return;
            }

            // Tạo phản hồi công việc
            var phcv = new PhanHoiCongViec
            {
                MaCongViec = chiTietCV.MaCongViec,
                MaNguoiDung = nguoiDung.MaNguoiDung,
                NoiDung = msg.TextBody ?? msg.HtmlBody ?? "",
                ThoiGian = DateTime.Now,
                Loai = "Reply"
            };

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.PhanHoiCongViecs.Add(phcv);
                await _context.SaveChangesAsync();

                // Lưu attachments
                foreach (var attachment in msg.Attachments.OfType<MimePart>())
                {
                    var fileName = attachment.FileName ?? Guid.NewGuid().ToString();
                    var path = Path.Combine("EmailAttachments", fileName);
                    Directory.CreateDirectory("EmailAttachments");

                    using (var stream = File.Create(path))
                    {
                        await attachment.Content.DecodeToAsync(stream);
                    }

                    var tepTin = new TepTin
                    {
                        TenTepGoc = fileName,
                        DuongDan = path
                    };
                    _context.TepTins.Add(tepTin);
                    await _context.SaveChangesAsync();

                    var tepDinhKem = new TepDinhKemEmail
                    {
                        MaEmail = msg.MessageId,
                        MaTep = tepTin.MaTep,
                        Email = null,
                        TepTin = tepTin
                    };
                    _context.TepDinhKemEmails.Add(tepDinhKem);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                Console.WriteLine($"✅ Tạo phản hồi công việc + lưu attachments cho: {chiTietCV.MaCongViec}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"❌ Lỗi khi tạo phản hồi + attachments: {ex.Message}");
            }
        }



    }
}
