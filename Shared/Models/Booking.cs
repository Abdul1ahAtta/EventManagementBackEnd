using System;

namespace Shared.Models;

public class Booking
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int NumberOfTickets { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime? CancellationDate { get; set; }
}

public enum BookingStatus
{
    Pending,
    Confirmed,
    Cancelled,
    Completed
} 