using Core.Security.Entities;
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
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

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

        modelBuilder.Entity<User>(u =>
        {
            u.ToTable("Users").HasKey(u => u.Id);
            u.Property(u => u.Id).HasColumnName("Id");
            u.Property(u => u.FirstName).HasColumnName("FirstName");
            u.Property(u => u.LastName).HasColumnName("LastName");
            u.Property(u => u.Email).HasColumnName("Email");
            u.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
            u.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
            u.Property(u => u.Status).HasColumnName("Status");
            u.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");
            u.HasMany(c => c.UserOperationClaims);
            u.HasMany(c => c.RefreshTokens);

        });

        modelBuilder.Entity<OperationClaim>(o =>
        {
            o.ToTable("OperationClaims").HasKey(o => o.Id);
            o.Property(o => o.Id).HasColumnName("Id");
            o.Property(o => o.Name).HasColumnName("Name");

        });
        modelBuilder.Entity<UserOperationClaim>(u =>
        {
            u.ToTable("UserOperationClaims").HasKey(u => u.Id);
            u.Property(u => u.Id).HasColumnName("Id");
            u.Property(u => u.UserId).HasColumnName("UserId");
            u.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
            u.HasOne(u => u.User);
            u.HasOne(u => u.OperationClaim);
        });


        ProgrammingLanguage[] programmingLanguagesEntitySeeds = { new(1, "C#"), new(2, "Java"), new(3, "Python") };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguagesEntitySeeds);

        Technology[] technologiesEntitySeeds = { new(1, "ASP.NET", 1), new(2, "Spring", 2), new(3, "React", 3) };
        modelBuilder.Entity<Technology>().HasData(technologiesEntitySeeds);

        OperationClaim[] operationClaimsEntitySeeds = { new(1, "Admin"), new(2, "User") };
        modelBuilder.Entity<OperationClaim>().HasData(operationClaimsEntitySeeds);
    }
}