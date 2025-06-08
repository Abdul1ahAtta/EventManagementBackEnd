using Microsoft.EntityFrameworkCore;
using EventService.Data;
using Shared.DTOs;
using Shared.Models;

namespace EventService.Services;

public class EventManager
{
    private readonly EventDbContext _context;

    public EventManager(EventDbContext context)
    {
        _context = context;
    }

    public async Task<EventResponse> CreateEventAsync(CreateEventRequest request, string organizerId)
    {
        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Location = request.Location,
            MaxAttendees = request.MaxAttendees,
            Price = request.Price,
            OrganizerId = organizerId,
            Status = EventStatus.Draft,
            CreatedAt = DateTime.UtcNow
        };

        _context.Events.Add(@event);
        await _context.SaveChangesAsync();

        return MapToResponse(@event);
    }

    public async Task<EventResponse?> UpdateEventAsync(Guid id, UpdateEventRequest request, string organizerId)
    {
        var @event = await _context.Events.FindAsync(id);
        if (@event == null || @event.OrganizerId != organizerId)
            return null;

        @event.Title = request.Title;
        @event.Description = request.Description;
        @event.StartDate = request.StartDate;
        @event.EndDate = request.EndDate;
        @event.Location = request.Location;
        @event.MaxAttendees = request.MaxAttendees;
        @event.Price = request.Price;
        @event.Status = request.Status;
        @event.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToResponse(@event);
    }

    public async Task<bool> DeleteEventAsync(Guid id, string organizerId)
    {
        var @event = await _context.Events.FindAsync(id);
        if (@event == null || @event.OrganizerId != organizerId)
            return false;

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<EventResponse?> GetEventByIdAsync(Guid id)
    {
        var @event = await _context.Events.FindAsync(id);
        return @event != null ? MapToResponse(@event) : null;
    }

    public async Task<IEnumerable<EventResponse>> GetEventsAsync(string? organizerId = null)
    {
        var query = _context.Events.AsQueryable();
        
        if (!string.IsNullOrEmpty(organizerId))
            query = query.Where(e => e.OrganizerId == organizerId);

        var events = await query.OrderByDescending(e => e.CreatedAt).ToListAsync();
        return events.Select(MapToResponse);
    }

    private static EventResponse MapToResponse(Event @event) =>
        new(
            @event.Id,
            @event.Title,
            @event.Description,
            @event.StartDate,
            @event.EndDate,
            @event.Location,
            @event.MaxAttendees,
            @event.Price,
            @event.OrganizerId,
            @event.Status,
            @event.CreatedAt,
            @event.UpdatedAt
        );
} 