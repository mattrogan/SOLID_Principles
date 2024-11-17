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
