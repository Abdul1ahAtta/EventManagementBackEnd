using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using BookingService.Services;

namespace BookingService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly BookingManager _bookingManager;

    public BookingsController(BookingManager bookingManager)
    {
        _bookingManager = bookingManager;
    }

    [HttpPost]
    public async Task<ActionResult<BookingResponse>> CreateBooking([FromBody] CreateBookingRequest request)
    {
        var userId = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var response = await _bookingManager.CreateBookingAsync(request, userId);
        return CreatedAtAction(nameof(GetBooking), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<BookingResponse>> UpdateBooking(Guid id, [FromBody] UpdateBookingRequest request)
    {
        var userId = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var response = await _bookingManager.UpdateBookingAsync(id, request, userId);
        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBooking(Guid id)
    {
        var userId = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _bookingManager.DeleteBookingAsync(id, userId);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookingResponse>> GetBooking(Guid id)
    {
        var userId = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var response = await _bookingManager.GetBookingByIdAsync(id);
        if (response == null || response.UserId != userId)
            return NotFound();

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingResponse>>> GetBookings([FromQuery] Guid? eventId = null)
    {
        var userId = User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var bookings = await _bookingManager.GetBookingsAsync(userId, eventId);
        return Ok(bookings);
    }

    [HttpGet("event/{eventId:guid}")]
    [Authorize(Policy = "RequireOrganizer")]
    public async Task<ActionResult<IEnumerable<BookingResponse>>> GetEventBookings(Guid eventId)
    {
        var bookings = await _bookingManager.GetBookingsAsync(eventId: eventId);
        return Ok(bookings);
    }
} 