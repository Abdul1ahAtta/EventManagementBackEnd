using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using EventService.Services;

namespace EventService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly EventManager _eventManager;

    public EventsController(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    [HttpPost]
    public async Task<ActionResult<EventResponse>> CreateEvent([FromBody] CreateEventRequest request)
    {
        // Use a default user ID for unauthenticated requests
        var userId = User.FindFirst("sub")?.Value ?? "anonymous-user";
        var response = await _eventManager.CreateEventAsync(request, userId);
        return CreatedAtAction(nameof(GetEvent), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EventResponse>> UpdateEvent(Guid id, [FromBody] UpdateEventRequest request)
    {
        var userId = User.FindFirst("sub")?.Value ?? "anonymous-user";
        var response = await _eventManager.UpdateEventAsync(id, request, userId);
        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var userId = User.FindFirst("sub")?.Value ?? "anonymous-user";
        var result = await _eventManager.DeleteEventAsync(id, userId);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EventResponse>> GetEvent(Guid id)
    {
        var response = await _eventManager.GetEventByIdAsync(id);
        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventResponse>>> GetEvents([FromQuery] string? organizerId = null)
    {
        var events = await _eventManager.GetEventsAsync(organizerId);
        return Ok(events);
    }
} 