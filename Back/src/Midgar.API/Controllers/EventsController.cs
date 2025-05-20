using Microsoft.AspNetCore.Mvc;
using Midgar.Application.Interfaces;
using Midgar.Application.DTOs;

namespace Midgar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var events = await _eventService.GetAllEventsAsync(true);

            if (events == null)
                return NoContent();
            
            return Ok(events);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve events. Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var eventById = await _eventService.GetEventByIdAsync(id, true);

            if (eventById == null)
                return NoContent();
            
            return Ok(eventById);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve events. Error: {ex.Message}");
        }
    }

    [HttpGet("{theme}/theme")]
    public async Task<IActionResult> GetByTheme(string theme)
    {
        try
        {
            var eventById = await _eventService.GetAllEventsByThemeAsync(theme, true);

            if (eventById == null)
                return NoContent();
            
            return Ok(eventById);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve events. Error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventDTO model)
    {
        try
        {
            var eventPost = await _eventService.AddEvents(model);

            if (eventPost == null)
                return BadRequest("Error when trying to add event.");
            
            return Ok(eventPost);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to add events. Error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventDTO model)
    {
        try
        {
            var eventPut = await _eventService.UpdateEvents(id, model);

            if (eventPut == null)
                return BadRequest("Error when trying to update event.");
            
            return Ok(eventPut);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to update events. Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var eventById = await _eventService.GetEventByIdAsync(id, true);

            if (eventById == null)
                return NoContent();
                
            return await _eventService.DeleteEvents(id) ? Ok("Deleted") : throw new Exception("An error occurred while trying to delete the event");
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to delete events. Error: {ex.Message}");
        }
    }
}