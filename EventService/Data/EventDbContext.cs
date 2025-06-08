using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EventService.Data;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnType("TEXT");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("TEXT");

            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasColumnType("TEXT");

            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnType("TEXT");

            entity.Property(e => e.OrganizerId)
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property(e => e.Price)
                .HasColumnType("REAL");

            entity.Property(e => e.Status)
                .HasColumnType("INTEGER");

            entity.Property(e => e.StartDate)
                .HasColumnType("TEXT");

            entity.Property(e => e.EndDate)
                .HasColumnType("TEXT");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("TEXT");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("TEXT");
        });
    }
} 