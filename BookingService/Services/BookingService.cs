using Microsoft.EntityFrameworkCore;
using BookingService.Data;
using Shared.DTOs;
using Shared.Models;

namespace BookingService.Services;

public class BookingManager
{
    private readonly BookingDbContext _context;

    public BookingManager(BookingDbContext context)
    {
        _context = context;
    }

    public async Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request, string userId)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            EventId = request.EventId,
            UserId = userId,
            NumberOfTickets = request.NumberOfTickets,
            Status = BookingStatus.Pending,
            BookingDate = DateTime.UtcNow
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return MapToResponse(booking);
    }

    public async Task<BookingResponse?> UpdateBookingAsync(Guid id, UpdateBookingRequest request, string userId)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null || booking.UserId != userId)
            return null;

        booking.NumberOfTickets = request.NumberOfTickets;
        booking.Status = request.Status;
        
        if (request.Status == BookingStatus.Cancelled)
            booking.CancellationDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToResponse(booking);
    }

    public async Task<bool> DeleteBookingAsync(Guid id, string userId)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null || booking.UserId != userId)
            return false;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BookingResponse?> GetBookingByIdAsync(Guid id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        return booking != null ? MapToResponse(booking) : null;
    }

    public async Task<IEnumerable<BookingResponse>> GetBookingsAsync(string? userId = null, Guid? eventId = null)
    {
        var query = _context.Bookings.AsQueryable();
        
        if (!string.IsNullOrEmpty(userId))
            query = query.Where(b => b.UserId == userId);
            
        if (eventId.HasValue)
            query = query.Where(b => b.EventId == eventId.Value);

        var bookings = await query.OrderByDescending(b => b.BookingDate).ToListAsync();
        return bookings.Select(MapToResponse);
    }

    private static BookingResponse MapToResponse(Booking booking) =>
        new(
            booking.Id,
            booking.EventId,
            booking.UserId,
            booking.NumberOfTickets,
            booking.TotalPrice,
            booking.Status,
            booking.BookingDate,
            booking.CancellationDate
        );
} 