using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace BookingService.Data;

public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Booking>(entity =>
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.Id)
                .HasColumnType("TEXT");

            entity.Property(b => b.EventId)
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property(b => b.UserId)
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property(b => b.NumberOfTickets)
                .IsRequired()
                .HasColumnType("INTEGER");

            entity.Property(b => b.TotalPrice)
                .HasColumnType("REAL");

            entity.Property(b => b.Status)
                .IsRequired()
                .HasColumnType("INTEGER");

            entity.Property(b => b.BookingDate)
                .IsRequired()
                .HasColumnType("TEXT");

            entity.Property(b => b.CancellationDate)
                .HasColumnType("TEXT");
        });
    }
} 