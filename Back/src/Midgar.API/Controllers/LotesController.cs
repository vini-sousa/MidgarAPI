using Microsoft.AspNetCore.Mvc;
using Midgar.Application.Interfaces;
using Midgar.Application.DTOs;

namespace Midgar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LotesController : ControllerBase
{
    private readonly ILoteService _loteService;
    public LotesController(ILoteService eventService)
    {
        _loteService = eventService;
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetAll(int eventId)
    {
        try
        {
            var lotes = await _loteService.GetLotesByEventIdAsync(eventId);

            if (lotes == null)
                return NoContent();
            
            return Ok(lotes);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve lotes. Error: {ex.Message}");
        }
    }

    [HttpPut("{eventId}")]
    public async Task<IActionResult> SaveLotes(int eventId, LoteDTO[] models)
    {
        try
        {
            var lotes = await _loteService.SaveLotes(eventId, models);

            if (lotes == null)
                return BadRequest("Error when trying to save lotes.");
            
            return Ok(lotes);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to save lotes. Error: {ex.Message}");
        }
    }

    [HttpDelete("{eventId}/{loteId}")]
    public async Task<IActionResult> Delete(int eventId, int loteId)
    {
        try
        {
            var lote = await _loteService.GetLoteByIdsAsync(eventId, loteId);

            if (lote == null)
                return NoContent();
                
            return await _loteService.DeleteLote(lote.EventId, loteId) ? Ok(new { message = "Deleted" }) : throw new Exception("An error occurred while trying to delete the lote");
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to delete lotes. Error: {ex.Message}");
        }
    }
}