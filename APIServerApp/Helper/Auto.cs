using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using APIServerApp.Model;
using DotNetEnv;
using MimeKit;

namespace APIServerApp.Helper
{
    public class Auto
    {
        public bool SendEmail(Email e, List<NguoiNhanEmail> lstNguoiNhan, List<TepDinhKemEmail> lstTep, NguoiDung nd)
        {
            try
            {
                var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
                Env.Load(envPath);

                string CLIENT = Env.GetString("SMTP_CLIENT");
                int PORT = Convert.ToInt32(Env.GetString("SMTP_PORT"));

                var message = new MimeMessage();

                // From
                message.From.Add(new MailboxAddress("", nd.Email));
                message.Subject = e.TieuDe ?? "";

                // To / Cc / Bcc
                foreach (var nguoiNhan in lstNguoiNhan)
                {
                    var email = nguoiNhan.NguoiDung.Email;
                    switch (nguoiNhan.VaiTro)
                    {
                        case "to": message.To.Add(MailboxAddress.Parse(email)); break;
                        case "cc": message.Cc.Add(MailboxAddress.Parse(email)); break;
                        case "bcc": message.Bcc.Add(MailboxAddress.Parse(email)); break;
                    }
                }

                // Body
                var builder = new BodyBuilder
                {
                    HtmlBody = e.NoiDung ?? "",
                    TextBody = Regex.Replace(e.NoiDung ?? "", "<.*?>", string.Empty)
                };

                // File đính kèm
                if (lstTep?.Count > 0)
                {
                    foreach (var tep in lstTep)
                    {
                        string path = tep.TepTin?.DuongDan;
                        string tenTepGoc = tep.TepTin?.TenTepGoc;

                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        {
                            builder.Attachments.Add(path, new ContentType("application", "octet-stream"));
                        }
                    }
                }

                message.Body = builder.ToMessageBody();

                // Gửi qua SMTP
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(CLIENT, PORT, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(nd.Email, nd.MatKhau);
                    client.Send(message);
                    client.Disconnect(true);
                }

                // Export .eml -> .zip
                string targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Attachments");
                if (!Directory.Exists(targetFolder))
                    Directory.CreateDirectory(targetFolder);

                string tempFolder = Path.Combine(Path.GetTempPath(), $"EmailTemp_{Guid.NewGuid()}");
                Directory.CreateDirectory(tempFolder);

                string safeSubject = SanitizeFileName(e.TieuDe ?? "email");
                string emlFileName = $"{safeSubject}_{DateTime.Now:yyyyMMdd_HHmmss}.eml";
                string emlPath = Path.Combine(tempFolder, emlFileName);

                using (var stream = File.Create(emlPath))
                {
                    message.WriteTo(stream);
                }

                string zipPath = Path.Combine(targetFolder, Path.GetFileNameWithoutExtension(emlFileName) + ".zip");

                if (File.Exists(zipPath)) File.Delete(zipPath);

                ZipFile.CreateFromDirectory(tempFolder, zipPath, CompressionLevel.Optimal, includeBaseDirectory: false);
                Directory.Delete(tempFolder, true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi email: " + ex.Message);
                return false;
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