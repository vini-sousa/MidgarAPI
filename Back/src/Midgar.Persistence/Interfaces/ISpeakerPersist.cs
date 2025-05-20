using Midgar.Domain.Entities;

namespace Midgar.Persistence.Interfaces
{
    public interface ISpeakerPersist
    {
        Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includedEvents = false);

        Task<Speaker[]> GetAllSpeakersAsync(bool includedEvents = false);

        Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includedEvents = false);
    }
}