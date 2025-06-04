using Microsoft.AspNetCore.Mvc;
using Midgar.Application.Interfaces;
using Midgar.Application.DTOs;

namespace Midgar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IWebHostEnvironment _hostEnvironment;

    public EventsController(IEventService eventService, IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
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

    [HttpGet("theme/{theme}")]
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
                
            if (await _eventService.DeleteEvents(id)) 
            {
                DeleteImage(eventById.ImageURL);
                return Ok(new { message = "Deleted" });
            }
            else 
                throw new Exception("An error occurred while trying to delete the event");
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to delete events. Error: {ex.Message}");
        }
    }

    [HttpPost("upload-image/{eventId}")]
    public async Task<IActionResult> UploadImage(int eventId)
    {
        try
        {
            var eventById = await _eventService.GetEventByIdAsync(eventId, true);

            if (eventById == null)
                return BadRequest("Error when trying to add event.");

            var file = Request.Form.Files[0];

            if (file.Length > 0)
            {
                DeleteImage(eventById.ImageURL);
                eventById.ImageURL = await SaveImage(file);
            }

            var returnEvent = await _eventService.UpdateEvents(eventId, eventById);

            return Ok(returnEvent);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to add events. Error: {ex.Message}");
        }
    }

    [NonAction]
    public void DeleteImage(string imageName) 
    {
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);

        if (System.IO.File.Exists(imagePath))
            System.IO.File.Delete(imagePath);
    }

    [NonAction]
    public async Task<String> SaveImage(IFormFile imageFile)
    {
        string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

        imageName = $"{imageName}{DateTime.UtcNow:yyyyMMddfff}{Path.GetExtension(imageFile.FileName)}";

        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);

        using (var fileStream = new FileStream(imagePath, FileMode.Create))
            await imageFile.CopyToAsync(fileStream);

        return imageName;
    }
}