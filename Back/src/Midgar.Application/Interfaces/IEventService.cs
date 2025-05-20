using Midgar.Application.DTOs;

namespace Midgar.Application.Interfaces
{
    public interface IEventService
    {
        Task<EventDTO> AddEvents(EventDTO model);

        Task<EventDTO> UpdateEvents(int id, EventDTO model);

        Task<bool> DeleteEvents(int eventId);

        Task<EventDTO[]> GetAllEventsByThemeAsync(string theme, bool includedSpeakers = false);

        Task<EventDTO[]> GetAllEventsAsync(bool includedSpeakers = false);

        Task<EventDTO> GetEventByIdAsync(int eventId, bool includedSpeakers = false);
    }
}