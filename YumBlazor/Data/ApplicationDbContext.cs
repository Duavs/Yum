using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YumBlazor.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Appetizers" },
            new Category { Id = 2, Name = "Main Courses" },
            new Category { Id = 3, Name = "Desserts" },
            new Category { Id = 4, Name = "Beverages" }
        );
    }
}  