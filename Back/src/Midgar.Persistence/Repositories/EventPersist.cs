using Microsoft.EntityFrameworkCore;
using Midgar.Domain.Entities;
using Midgar.Persistence.Context;
using Midgar.Persistence.Interfaces;

namespace Midgar.Persistence.Repositories
{
    public class EventRepository : IEventPersist
    {
        private readonly MidgarContext _context;
        public EventRepository(MidgarContext context)
        {
            _context = context;
        }

        async Task<Event[]> IEventPersist.GetAllEventsByThemeAsync(string theme, bool includedSpeakers = false)
        {
            IQueryable<Event> query = _context.Events.Include(e => e.Lotes).Include(e => e.SocialMedias);
            
            if (includedSpeakers) 
                query = query.Include(e => e.SpeakersEvents).ThenInclude(se => se.Speaker);

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Theme.ToLower().Contains(theme.ToLower()));

            return await query.ToArrayAsync();
        }

        async Task<Event[]> IEventPersist.GetAllEventsAsync(bool includedSpeakers = false)
        {
            IQueryable<Event> query = _context.Events.Include(e => e.Lotes).Include(e => e.SocialMedias);
            
            if (includedSpeakers) 
                query = query.Include(e => e.SpeakersEvents).ThenInclude(se => se.Speaker);

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
     
        async Task<Event> IEventPersist.GetEventByIdAsync(int eventId, bool includedSpeakers = false)
        {
            IQueryable<Event> query = _context.Events.Include(e => e.Lotes).Include(e => e.SocialMedias);
            
            if (includedSpeakers) 
                query = query.Include(e => e.SpeakersEvents).ThenInclude(se => se.Speaker);

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == eventId);

            return await query.FirstOrDefaultAsync();
        }
    }
}