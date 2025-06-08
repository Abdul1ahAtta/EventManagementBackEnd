using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace UserService.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure ApplicationUser properties
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.FirstName)
                .HasMaxLength(100)
                .HasColumnType("TEXT");

            entity.Property(u => u.LastName)
                .HasMaxLength(100)
                .HasColumnType("TEXT");

            entity.Property(u => u.Address)
                .HasMaxLength(500)
                .HasColumnType("TEXT");
        });
    }
} 