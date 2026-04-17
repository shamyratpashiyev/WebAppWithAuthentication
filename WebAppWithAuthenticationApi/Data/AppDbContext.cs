using Microsoft.EntityFrameworkCore;
using WebAppWithAuthenticationApi.Enums;
using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.Surname).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.Position).IsRequired(false);
        modelBuilder.Entity<User>().Property(x => x.LastLoginDate).IsRequired(false);
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@email.com",
                Name = "John",
                Surname = "Doe",
                Position = "Software Engineer",
                Status = UserStatus.Active,
                ConcurrencyStamp = "b4260021-c964-4632-90f7-674683050016",
                SecurityStamp = "static-security-stamp-here",
                LastLoginDate = new DateTime(2026, 04, 12)
            });
    }

}