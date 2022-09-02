using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammingLanguage>(p =>
            {
                p.ToTable("ProgrammingLanguages").HasKey(p => p.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.Name).HasColumnName("Name");
            }
        );

        ProgrammingLanguage[] programmingLanguagesEntitySeeds = { new(1, "C#"), new(2, "Java"), new(3, "Python") };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguagesEntitySeeds);
    }
}