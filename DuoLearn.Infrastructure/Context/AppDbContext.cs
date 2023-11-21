using DuoLearn.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DuoLearn.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Language> Languages { get; set; }


    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<User>()
    //         .HasMany(e => e.Languages)
    //         .WithMany(e => e.Users)
    //         .UsingEntity(
    //         "UserLanguages",
    //         l => l.HasOne(typeof(Language)).WithMany().HasForeignKey("UserId").HasPrincipalKey(nameof(User.Id)),
    //         r => r.HasOne(typeof(User)).WithMany().HasForeignKey("LanguageId").HasPrincipalKey(nameof(Language.Id)),
    //         j => j.HasKey("LanguageId", "UserId"));
    // }
}