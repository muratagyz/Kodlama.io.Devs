using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public DbSet<Technology> Technologies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammingLanguage>(p =>
            {
                p.ToTable("ProgrammingLanguages").HasKey(p => p.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.Name).HasColumnName("Name");
                p.HasMany(p => p.Technologies);
            }
        );

        modelBuilder.Entity<Technology>(t =>
            {
                t.ToTable("Technologies").HasKey(t => t.Id);
                t.Property(t => t.Id).HasColumnName("Id");
                t.Property(t => t.Name).HasColumnName("Name");
                t.Property(t => t.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                t.HasOne(t => t.ProgrammingLanguage);
            }
        );

        ProgrammingLanguage[] programmingLanguagesEntitySeeds = { new(1, "C#"), new(2, "Java"), new(3, "Python") };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguagesEntitySeeds);

        Technology[] technologiesEntitySeeds = { new(1, "ASP.NET", 1), new(2, "Spring", 2), new(3, "React", 3) };
        modelBuilder.Entity<Technology>().HasData(technologiesEntitySeeds);
    }
}