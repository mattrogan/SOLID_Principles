using Microsoft.EntityFrameworkCore;
using SOLID_Principles.Domain;

namespace SOLID_Principles.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext()
        : base()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> opts) 
        : base(opts)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // This is merely a sandbox project -- never going to be used in a production environment,
        // hence why the following are enabled & why the conn string is exposed..
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();

        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var user = modelBuilder.Entity<User>();

        var expense = modelBuilder.Entity<Expense>();
        expense.HasOne<User>()
            .WithOne()
            .HasPrincipalKey<User>(x => x.Id)
            .HasForeignKey<Expense>(x => x.UserId);
    }
}
