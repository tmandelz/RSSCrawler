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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
    .HasMany(c => c.Contents)
    .WithOne(e => e.Entry);
            modelBuilder.Entity<Entry>()
    .HasMany(a => a.Authors)
    .WithOne(e => e.Entry);
            modelBuilder.Entity<Entry>()
.HasMany(ca => ca.Categories)
.WithOne(e => e.Entry);

            modelBuilder.Entity<Entry>()
                .HasOne(p => p.Provider).WithOne(e => e.Entry)
            .HasForeignKey<Provider>(b => b.EntryId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Content> Contents { get; set; }
    }
}