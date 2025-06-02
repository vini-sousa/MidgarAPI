using Microsoft.EntityFrameworkCore;
using Midgar.Domain.Entities;
using Midgar.Persistence.Context;
using Midgar.Persistence.Interfaces;

namespace Midgar.Persistence.Repositories
{
    public class LoteRepository : ILoteRepository
    {
        private readonly MidgarContext _context;
        public LoteRepository(MidgarContext context)
        {
            _context = context;
        }

        public async Task<Lote[]> GetLotesByEventIdAsync(int eventId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking().Where(lote => lote.EventId == eventId);

            return await query.ToArrayAsync();
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventId, int loteId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking().Where(lote => lote.EventId == eventId && lote.Id == loteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}