using Shared.Models;

namespace Shared.DTOs;

public record CreateEventRequest(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    string Location,
    int MaxAttendees,
    decimal Price
);

public record UpdateEventRequest(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    string Location,
    int MaxAttendees,
    decimal Price,
    EventStatus Status
);

public record EventResponse(
    Guid Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    string Location,
    int MaxAttendees,
    decimal Price,
    string OrganizerId,
    EventStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
); 