using Midgar.Application.DTOs;

namespace Midgar.Application.Interfaces
{
    public interface ILoteService
    {
        Task<LoteDTO[]> SaveLotes(int id, LoteDTO[] model);

        Task<bool> DeleteLote(int eventId, int loteId);

        Task<LoteDTO[]> GetLotesByEventIdAsync(int eventId);

        Task<LoteDTO> GetLoteByIdsAsync(int eventId, int loteId);
    }
}