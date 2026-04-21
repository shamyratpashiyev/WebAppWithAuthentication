using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppWithAuthenticationApi.Enums;
using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dummyUserList = new List<User>
        {
            new User("admin@email.com", "John", "Doe", "Software Engineer", new DateTime(2026, 04, 12), UserStatus.Active) { Id= 1, UserName = "admin", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("m.rossi@email.com", "Marco", "Rossi", "Project Manager", new DateTime(2026, 04, 15), UserStatus.Unverified) { Id= 2, UserName = "m.rossi", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("s.chen@email.com", "Sarah", "Chen", "UI/UX Designer", new DateTime(2026, 04, 18), UserStatus.Active) {Id= 3, UserName = "s.chen", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("a.smith@email.com", "Alice", "Smith", "DevOps Engineer", new DateTime(2026, 03, 20), UserStatus.Blocked) { Id= 4, UserName = "a.smith", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("h.tanaka@email.com", "Hiroshi", "Tanaka", "Backend Developer", new DateTime(2026, 04, 19), UserStatus.Unverified) { Id= 5, UserName = "h.tanaka", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("e.dubois@email.com", "Elena", "Dubois", "QA Analyst", new DateTime(2026, 04, 10), UserStatus.Active) { Id= 6, UserName = "e.dubois", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("j.kowalski@email.com", "Jan", "Kowalski", "Frontend Developer", new DateTime(2026, 04, 17), UserStatus.Blocked) { Id= 7, UserName = "j.kowalski", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("l.garcia@email.com", "Luis", "Garcia", "Data Scientist", new DateTime(2026, 04, 14), UserStatus.Active) { Id= 8, UserName = "l.garcia", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("m.ahmed@email.com", "Mariam", "Ahmed", "Systems Architect", new DateTime(2026, 04, 16), UserStatus.Unverified) { Id= 9, UserName = "m.ahmed", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("k.nielsen@email.com", "Kasper", "Nielsen", "Business Analyst", new DateTime(2026, 01, 05), UserStatus.Unverified) { Id= 10, UserName = "k.nielsen", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("f.silva@email.com", "Fernanda", "Silva", "Cloud Engineer", new DateTime(2026, 04, 11), UserStatus.Active) { Id= 11, UserName = "f.silva", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("o.peters@email.com", "Oliver", "Peters", "Security Specialist", new DateTime(2026, 04, 02), UserStatus.Unverified) { Id= 12, UserName = "o.peters", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("y.kim@email.com", "Yuna", "Kim", "Mobile Developer", new DateTime(2026, 04, 13), UserStatus.Active) { Id= 13, UserName = "y.kim", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("r.kumar@email.com", "Rohan", "Kumar", "Database Administrator", new DateTime(2026, 04, 15), UserStatus.Blocked) { Id= 14, UserName = "r.kumar", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("b.wright@email.com", "Beatrice", "Wright", "Product Owner", new DateTime(2026, 04, 18), UserStatus.Active) { Id= 15, UserName = "b.wright", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("t.muller@email.com", "Thomas", "Müller", "Scrum Master", new DateTime(2026, 04, 19), UserStatus.Active) { Id= 16, UserName = "t.muller", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("i.popov@email.com", "Igor", "Popov", "Network Engineer", new DateTime(2026, 04, 07), UserStatus.Blocked) { Id= 17, UserName = "i.popov", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("c.santos@email.com", "Clara", "Santos", "Software Engineer", new DateTime(2026, 04, 12), UserStatus.Active) { Id= 18, UserName = "c.santos", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("v.nguyen@email.com", "Vinh", "Nguyen", "Machine Learning Engineer", new DateTime(2026, 04, 14), UserStatus.Unverified) { Id= 19, UserName = "v.nguyen", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("g.morris@email.com", "Grace", "Morris", "Technical Writer", new DateTime(2026, 04, 10), UserStatus.Active) { Id= 20, UserName = "g.morris", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("d.patel@email.com", "Deepak", "Patel", "Fullstack Developer", new DateTime(2026, 04, 16), UserStatus.Blocked) { Id= 21, UserName = "d.patel", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("z.jones@email.com", "Zoe", "Jones", "Project Coordinator", new DateTime(2026, 02, 28), UserStatus.Unverified) { Id= 22, UserName = "z.jones", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("p.larsen@email.com", "Pia", "Larsen", "Human Resources", new DateTime(2026, 04, 19), UserStatus.Active) { Id= 23, UserName = "p.larsen", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("w.clark@email.com", "William", "Clark", "IT Manager", new DateTime(2026, 04, 01), UserStatus.Blocked) { Id= 24, UserName = "w.clark", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" },
            new User("k.adhikari@email.com", "Kiran", "Adhikari", "Embedded Systems Dev", new DateTime(2026, 04, 18), UserStatus.Blocked) { Id= 25, UserName = "k.adhikari", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941", PasswordHash = "AQAAAAIAAYagAAAAELBqe1NgUjUNqnZvM3XOzxCdspKAJQkM3OWjVDsF0wyKQVA+6r+huRE3hV0AOGKRpg==" }
        };
        foreach (var user in dummyUserList)
        {
            user.NormalizedEmail = user.Email?.ToUpperInvariant();
            user.NormalizedUserName = user.UserName?.ToUpperInvariant();
        }

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.Surname).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.Position).IsRequired(false);
        modelBuilder.Entity<User>().Property(x => x.LastLoginDate).IsRequired(false);
        
        modelBuilder.Entity<User>().HasData(dummyUserList);

    }

}