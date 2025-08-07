using Microsoft.EntityFrameworkCore;
using System.Linq;
using APIServerApp.Model;

namespace APIServerApp.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envPath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).FullName, ".env");
            Env.Load(envPath);

            var host = Env.GetString("DB_HOST");
            var db = Env.GetString("DB_NAME");
            var user = Env.GetString("DB_USER");
            var pass = Env.GetString("DB_PASS");

            var connectionString = $"Data Source={host};Initial Catalog={db};User ID={user};Password={pass};TrustServerCertificate=True";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Cấu hình composite keys
            modelBuilder.Entity<NguoiLienQuanCongViec>()
                .HasKey(x => new { x.MaCongViec, x.MaNguoiDung });

            modelBuilder.Entity<NguoiNhanEmail>()
                .HasKey(x => new { x.MaEmail, x.MaNguoiDung });

            modelBuilder.Entity<TepDinhKemEmail>()
                .HasKey(x => new { x.MaEmail, x.MaTep });

            modelBuilder.Entity<MaCongViecSequence>()
                .HasKey(x => new { x.MaDonVi, x.MaPhongBan });

            // 2. Cấu hình tất cả quan hệ với DeleteBehavior.NoAction
            // (Restrict có thể gây lỗi trong một số trường hợp)

            // Quan hệ NguoiLienQuanCongViec
            modelBuilder.Entity<NguoiLienQuanCongViec>()
                .HasOne(n => n.CongViec)
                .WithMany(c => c.NguoiLienQuanCongViecs)
                .HasForeignKey(n => n.MaCongViec)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NguoiLienQuanCongViec>()
                .HasOne(n => n.NguoiDung)
                .WithMany(u => u.NguoiLienQuanCongViecs)
                .HasForeignKey(n => n.MaNguoiDung)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ NguoiDung
            modelBuilder.Entity<NguoiDung>()
                .HasOne(u => u.PhongBan)
                .WithMany(p => p.NguoiDungs)
                .HasForeignKey(u => u.MaPhongBan)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NguoiDung>()
                .HasOne(u => u.DonVi)
                .WithMany(d => d.NguoiDungs)
                .HasForeignKey(u => u.MaDonVi)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NguoiDung>()
                .HasOne(u => u.ChucVu)
                .WithMany(c => c.NguoiDungs)
                .HasForeignKey(u => u.MaChucVu)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ MaCongViecSequence
            modelBuilder.Entity<MaCongViecSequence>()
                .HasOne(m => m.DonVi)
                .WithMany(d => d.MaCongViecSequences)
                .HasForeignKey(m => m.MaDonVi)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MaCongViecSequence>()
                .HasOne(m => m.PhongBan)
                .WithMany(p => p.MaCongViecSequences)
                .HasForeignKey(m => m.MaPhongBan)
                .OnDelete(DeleteBehavior.NoAction);

            // Các quan hệ khác
            modelBuilder.Entity<ChiTietCongViec>()
                .HasOne(c => c.CongViec)
                .WithMany(cv => cv.ChiTietCongViecs)
                .HasForeignKey(c => c.MaCongViec)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Email>()
                .HasOne(e => e.ChiTietCongViec)
                .WithMany()
                .HasForeignKey(e => e.MaChiTietCV)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Email>()
                .HasOne(e => e.NguoiGuiObj)
                .WithMany()
                .HasForeignKey(e => e.NguoiGui)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NguoiNhanEmail>()
                .HasOne(n => n.Email)
                .WithMany(e => e.NguoiNhanEmails)
                .HasForeignKey(n => n.MaEmail)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NguoiNhanEmail>()
                .HasOne(n => n.NguoiDung)
                .WithMany(u => u.NguoiNhanEmails)
                .HasForeignKey(n => n.MaNguoiDung)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TepDinhKemEmail>()
                .HasOne(t => t.Email)
                .WithMany(e => e.TepDinhKemEmails)
                .HasForeignKey(t => t.MaEmail)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TepDinhKemEmail>()
                .HasOne(t => t.TepTin)
                .WithMany(t => t.TepDinhKemEmails)
                .HasForeignKey(t => t.MaTep)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ThongBaoNguoiDung>()
                .HasOne(t => t.ChiTietCongViec)
                .WithMany()
                .HasForeignKey(t => t.MaChiTietCV)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ThongBaoNguoiDung>()
                .HasOne(t => t.NguoiDung)
                .WithMany(u => u.ThongBaoNguoiDungs)
                .HasForeignKey(t => t.MaNguoiDung)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PhanHoiCongViec>()
                .HasOne(p => p.CongViec)
                .WithMany(c => c.PhanHoiCongViecs)
                .HasForeignKey(p => p.MaCongViec)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PhanHoiCongViec>()
                .HasOne(p => p.NguoiDung)
                .WithMany(u => u.PhanHoiCongViecs)
                .HasForeignKey(p => p.MaNguoiDung)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

        // Danh sách DbSet
        public DbSet<ChiTietCongViec> ChiTietCongViecs { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<CongViec> CongViecs { get; set; }
        public DbSet<DonVi> DonVis { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<MaCongViecSequence> MaCongViecSequences { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<NguoiLienQuanCongViec> NguoiLienQuanCongViecs { get; set; }
        public DbSet<NguoiNhanEmail> NguoiNhanEmails { get; set; }
        public DbSet<PhanHoiCongViec> PhanHoiCongViecs { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<TepDinhKemEmail> TepDinhKemEmails { get; set; }
        public DbSet<TepTin> TepTins { get; set; }
        public DbSet<ThongBaoNguoiDung> ThongBaoNguoiDungs { get; set; }
    }
}