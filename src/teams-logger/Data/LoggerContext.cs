using Microsoft.EntityFrameworkCore;
using teams_logger.Models;

namespace teams_logger.Data
{
    public class LoggerContext : DbContext
    {
        public DbSet<LogItem> AuditLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AuditLog.db;");
            
        }
    }
}