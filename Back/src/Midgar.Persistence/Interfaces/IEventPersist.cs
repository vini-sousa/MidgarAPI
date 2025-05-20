using Midgar.Domain.Entities;

namespace Midgar.Persistence.Interfaces
{
    public interface IEventPersist
    {
        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includedSpeakers = false);

        Task<Event[]> GetAllEventsAsync(bool includedSpeakers = false);

        Task<Event> GetEventByIdAsync(int eventId, bool includedSpeakers = false);
    }
}