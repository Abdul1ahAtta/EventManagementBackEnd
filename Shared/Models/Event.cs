using System;

namespace Shared.Models;

public class Event
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public int MaxAttendees { get; set; }
    public decimal Price { get; set; }
    public string OrganizerId { get; set; } = string.Empty;
    public EventStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public enum EventStatus
{
    Draft,
    Published,
    Cancelled,
    Completed
} 