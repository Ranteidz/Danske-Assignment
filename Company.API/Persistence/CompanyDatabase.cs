using Company.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.API.Persistence;

public class CompanyDatabase : DbContext
{
    public CompanyDatabase(DbContextOptions<CompanyDatabase> options) : base(options)
    {
    }

    public CompanyDatabase()
    {
    }

    public DbSet<CompanyDto> Companies { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source = database.db");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<CompanyDto>()
            .HasIndex(u => u.Number)
            .IsUnique();
    }
}