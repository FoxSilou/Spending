namespace Spending.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Spending> Spendings { get; set; }
        public DbSet<Spender> Spenders { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Spending>(entity =>
            {
                entity.HasOne(x => x.Spender).WithMany().OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Currency).WithMany().OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Spender>(entity =>
            {
                entity.HasOne(x => x.Currency).WithMany().OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Currency>();
        }
    }
}
