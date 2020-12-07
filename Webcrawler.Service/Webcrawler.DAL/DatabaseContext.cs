using Microsoft.EntityFrameworkCore;

namespace Webcrawler.DAL
{
    public class DatabaseContext : DbContext
    {
        private readonly string connectionString = "Server=localhost\\SQLEXPRESS; Database=WebCrawler; User Id=sa; Password=123456;";

        public DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}