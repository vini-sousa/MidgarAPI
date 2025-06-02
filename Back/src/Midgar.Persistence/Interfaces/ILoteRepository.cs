using Midgar.Domain.Entities;

namespace Midgar.Persistence.Interfaces
{
    public interface ILoteRepository
    {
        Task<Lote[]> GetLotesByEventIdAsync(int eventId);

        Task<Lote> GetLoteByIdsAsync(int eventId, int loteId);
    }
}