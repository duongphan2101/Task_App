using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using MimeKit;

namespace APIServerApp.services
{
    public class EmailIdleService : BackgroundService
    {
        private readonly string _host = "imap.gmail.com";
        private readonly int _port = 993;
        private readonly string _username = "duong2101.test@gmail.com";
        private readonly string _password = "ufns jzmt difo offq";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var client = new ImapClient();

            await client.ConnectAsync(_host, _port, SecureSocketOptions.SslOnConnect, stoppingToken);
            await client.AuthenticateAsync(_username, _password, stoppingToken);

            // --- Inbox ---
            var inbox = client.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadOnly, stoppingToken);
            Console.WriteLine($"📥 Inbox has {inbox.Count} messages");

            // log tất cả mail trong Inbox
            for (int i = 0; i < inbox.Count; i++)
            {
                var msg = await inbox.GetMessageAsync(i, stoppingToken);
                LogEmailInfo(msg, "📥 Inbox");
            }

            inbox.CountChanged += async (s, e) =>
            {
                try
                {
                    var msg = await inbox.GetMessageAsync(inbox.Count - 1, stoppingToken);
                    LogEmailInfo(msg, "📥 Inbox");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Inbox error: {ex.Message}");
                }
            };

            // --- Sent ---
            var sent = client.GetFolder(SpecialFolder.Sent);
            await sent.OpenAsync(FolderAccess.ReadOnly, stoppingToken);
            Console.WriteLine($"📤 Sent has {sent.Count} messages");

            // log tất cả mail trong Sent
            for (int i = 0; i < sent.Count; i++)
            {
                var msg = await sent.GetMessageAsync(i, stoppingToken);
                LogEmailInfo(msg, "📤 Sent");
            }

            sent.CountChanged += async (s, e) =>
            {
                try
                {
                    var msg = await sent.GetMessageAsync(sent.Count - 1, stoppingToken);
                    LogEmailInfo(msg, "📤 Sent");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Sent error: {ex.Message}");
                }
            };

            // --- Idle loop ---
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await client.IdleAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Idle error: {ex.Message}");
                }
            }
        }

        private void LogEmailInfo(MimeMessage msg, string folder)
        {
            var messageId = msg.MessageId;
            if (!string.IsNullOrEmpty(messageId) && messageId.Contains("@"))
            {
                messageId = messageId.Split('@')[0];
            }

            Console.WriteLine("=================================================");
            Console.WriteLine($"{folder}");
            Console.WriteLine($"📧 From: {msg.From}");
            Console.WriteLine($"📤 To: {msg.To}");
            if (msg.Cc.Count > 0)
                Console.WriteLine($"📋 Cc: {msg.Cc}");
            Console.WriteLine($"📝 Subject: {msg.Subject}");
            Console.WriteLine($"📅 Date: {msg.Date}");
            Console.WriteLine($"📄 Message-ID: {messageId}");

            // log full nội dung email
            Console.WriteLine("🌐 HtmlBody:");
            Console.WriteLine(string.IsNullOrEmpty(msg.HtmlBody) ? "(no html body)" : msg.HtmlBody);

            Console.WriteLine("📄 TextBody:");
            Console.WriteLine(string.IsNullOrEmpty(msg.TextBody) ? "(no text body)" : msg.TextBody);

            Console.WriteLine("=================================================");
        }


    }
}
