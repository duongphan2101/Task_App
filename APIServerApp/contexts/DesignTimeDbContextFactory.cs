using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace APIServerApp.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=ADUONG;Initial Catalog=TASK_API_DB;User ID=sa;Password=123;TrustServerCertificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}