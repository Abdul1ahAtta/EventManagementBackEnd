using Shared.Models;

namespace Shared.DTOs;

public record CreateBookingRequest(
    Guid EventId,
    int NumberOfTickets
);

public record UpdateBookingRequest(
    int NumberOfTickets,
    BookingStatus Status
);

public record BookingResponse(
    Guid Id,
    Guid EventId,
    string UserId,
    int NumberOfTickets,
    decimal TotalPrice,
    BookingStatus Status,
    DateTime BookingDate,
    DateTime? CancellationDate
); 