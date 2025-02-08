using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Entities;

namespace SizeFintech.Infrastructure.DataAccess;
internal class SizeFintechDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Anticipation> Anticipations { get; set; }
    public DbSet<AnticipationLimit> AnticipationLimits { get; set; }
    public DbSet<Industry> Industries { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anticipation>(entity =>
        {
            entity.Property(a => a.Limit)
                .HasPrecision(12, 2);

            entity.Property(a => a.NetTotal)
                .HasPrecision(12, 2);

            entity.Property(a => a.GrossTotal)
                .HasPrecision(12, 2);
        });

        modelBuilder.Entity<AnticipationLimit>(entity =>
        {
            entity.Property(a => a.AnticipationPercent)
                .HasPrecision(5, 4);

            entity.Property(a => a.RevenueMinimun)
                .HasPrecision(12, 2);

            entity.Property(a => a.RevenueMaximum)
                .HasPrecision(12, 2);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(a => a.NetAmount)
                .HasPrecision(12, 2);

            entity.Property(a => a.GrossAmount)
                .HasPrecision(12, 2);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(a => a.MonthlyRevenue)
                .HasPrecision(12, 2);
        });

        modelBuilder.Entity<Industry>().HasData(
            new Industry { Id = 1, Name = "Serviços" },
            new Industry { Id = 2, Name = "Produtos" }
        );

        modelBuilder.Entity<AnticipationLimit>().HasData(
            new AnticipationLimit { Id = 1, RevenueMinimun = 10000, RevenueMaximum = 50000, AnticipationPercent = 0.5m, IndustryId = 1 },
            new AnticipationLimit { Id = 2, RevenueMinimun = 10000, RevenueMaximum = 50000, AnticipationPercent = 0.5m, IndustryId = 2 },
            new AnticipationLimit { Id = 3, RevenueMinimun = 50001, RevenueMaximum = 100000, AnticipationPercent = 0.55m, IndustryId = 1 },
            new AnticipationLimit { Id = 4, RevenueMinimun = 50001, RevenueMaximum = 100000, AnticipationPercent = 0.6m, IndustryId = 2 },
            new AnticipationLimit { Id = 5, RevenueMinimun = 100001, RevenueMaximum = null, AnticipationPercent = 0.6m, IndustryId = 1 },
            new AnticipationLimit { Id = 6, RevenueMinimun = 100001, RevenueMaximum = null, AnticipationPercent = 0.65m, IndustryId = 2 }
        );

        base.OnModelCreating(modelBuilder);
    }
}
