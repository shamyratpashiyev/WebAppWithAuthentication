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
        new List<User>
        {
            new User(1, "John", "Doe", "Software Engineer", new DateTime(2026, 04, 12), UserStatus.Active) { UserName = "admin", Email = "admin@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(2, "Marco", "Rossi", "Project Manager", new DateTime(2026, 04, 15), UserStatus.Unverified) { UserName = "m.rossi", Email = "m.rossi@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(3, "Sarah", "Chen", "UI/UX Designer", new DateTime(2026, 04, 18), UserStatus.Active) { UserName = "s.chen", Email = "s.chen@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(4, "Alice", "Smith", "DevOps Engineer", new DateTime(2026, 03, 20), UserStatus.Blocked) { UserName = "a.smith", Email = "a.smith@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(5, "Hiroshi", "Tanaka", "Backend Developer", new DateTime(2026, 04, 19), UserStatus.Unverified) { UserName = "h.tanaka", Email = "h.tanaka@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(6, "Elena", "Dubois", "QA Analyst", new DateTime(2026, 04, 10), UserStatus.Active) { UserName = "e.dubois", Email = "e.dubois@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(7, "Jan", "Kowalski", "Frontend Developer", new DateTime(2026, 04, 17), UserStatus.Blocked) { UserName = "j.kowalski", Email = "j.kowalski@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(8, "Luis", "Garcia", "Data Scientist", new DateTime(2026, 04, 14), UserStatus.Active) { UserName = "l.garcia", Email = "l.garcia@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(9, "Mariam", "Ahmed", "Systems Architect", new DateTime(2026, 04, 16), UserStatus.Unverified) { UserName = "m.ahmed", Email = "m.ahmed@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(10, "Kasper", "Nielsen", "Business Analyst", new DateTime(2026, 01, 05), UserStatus.Unverified) { UserName = "k.nielsen", Email = "k.nielsen@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(11, "Fernanda", "Silva", "Cloud Engineer", new DateTime(2026, 04, 11), UserStatus.Active) { UserName = "f.silva", Email = "f.silva@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(12, "Oliver", "Peters", "Security Specialist", new DateTime(2026, 04, 02), UserStatus.Unverified) { UserName = "o.peters", Email = "o.peters@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(13, "Yuna", "Kim", "Mobile Developer", new DateTime(2026, 04, 13), UserStatus.Active) { UserName = "y.kim", Email = "y.kim@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(14, "Rohan", "Kumar", "Database Administrator", new DateTime(2026, 04, 15), UserStatus.Blocked) { UserName = "r.kumar", Email = "r.kumar@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(15, "Beatrice", "Wright", "Product Owner", new DateTime(2026, 04, 18), UserStatus.Active) { UserName = "b.wright", Email = "b.wright@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(16, "Thomas", "Müller", "Scrum Master", new DateTime(2026, 04, 19), UserStatus.Active) { UserName = "t.muller", Email = "t.muller@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(17, "Igor", "Popov", "Network Engineer", new DateTime(2026, 04, 07), UserStatus.Blocked) { UserName = "i.popov", Email = "i.popov@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(18, "Clara", "Santos", "Software Engineer", new DateTime(2026, 04, 12), UserStatus.Active) { UserName = "c.santos", Email = "c.santos@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(19, "Vinh", "Nguyen", "Machine Learning Engineer", new DateTime(2026, 04, 14), UserStatus.Unverified) { UserName = "v.nguyen", Email = "v.nguyen@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(20, "Grace", "Morris", "Technical Writer", new DateTime(2026, 04, 10), UserStatus.Active) { UserName = "g.morris", Email = "g.morris@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(21, "Deepak", "Patel", "Fullstack Developer", new DateTime(2026, 04, 16), UserStatus.Blocked) { UserName = "d.patel", Email = "d.patel@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(22, "Zoe", "Jones", "Project Coordinator", new DateTime(2026, 02, 28), UserStatus.Unverified) { UserName = "z.jones", Email = "z.jones@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(23, "Pia", "Larsen", "Human Resources", new DateTime(2026, 04, 19), UserStatus.Active) { UserName = "p.larsen", Email = "p.larsen@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(24, "William", "Clark", "IT Manager", new DateTime(2026, 04, 01), UserStatus.Blocked) { UserName = "w.clark", Email = "w.clark@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" },
            new User(25, "Kiran", "Adhikari", "Embedded Systems Dev", new DateTime(2026, 04, 18), UserStatus.Blocked) { UserName = "k.adhikari", Email = "k.adhikari@email.com", ConcurrencyStamp = "72a568297b6e492cb7159c92257d091e", SecurityStamp = "f4d927c3e10a4865a9e3427f1020b941" }
        });
    }

}