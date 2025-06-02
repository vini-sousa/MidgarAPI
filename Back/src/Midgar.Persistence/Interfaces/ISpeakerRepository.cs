using Midgar.Domain.Entities;

namespace Midgar.Persistence.Interfaces
{
    public interface ISpeakerRepository
    {
        Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includedEvents = false);

        Task<Speaker[]> GetAllSpeakersAsync(bool includedEvents = false);

        Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includedEvents = false);
    }
}