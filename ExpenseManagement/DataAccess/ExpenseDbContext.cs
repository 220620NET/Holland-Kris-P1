using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext() : base() { }
        public ExpenseDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> users { get; set; }
        public DbSet<Tickets> tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tickets>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(p => p.author);
            modelBuilder.Entity<Tickets>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(p => p.resolver);
        }
    }
}
