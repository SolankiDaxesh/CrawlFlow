using Microsoft.EntityFrameworkCore;

namespace CrawlFlow.Infrastructure
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options)
            : base(options)
        {
        }

        // Example DbSet
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}