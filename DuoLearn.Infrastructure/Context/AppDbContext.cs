using DuoLearn.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DuoLearn.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
}