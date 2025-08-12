using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace APIServerApp.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // var envPath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).FullName, ".env");
            // Env.Load(envPath);

            // var host = Env.GetString("DB_HOST");
            // var db = Env.GetString("DB_NAME");
            // var user = Env.GetString("DB_USER");
            // var pass = Env.GetString("DB_PASS");

            var connectionString = $"Data Source=ADUONG;Initial Catalog=TASK_API_DB;User ID=sa;Password=123;TrustServerCertificate=True";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}