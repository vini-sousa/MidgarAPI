using Microsoft.EntityFrameworkCore;
using Midgar.Domain.Entities;
using Midgar.Persistence.Context;
using Midgar.Persistence.Interfaces;

namespace Midgar.Persistence.Repositories
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly MidgarContext _context;
        public SpeakerRepository(MidgarContext context)
        {
            _context = context;
        }

        async Task<Speaker[]> ISpeakerRepository.GetAllSpeakersByNameAsync(string name, bool includedEvents)
        {
            IQueryable<Speaker> query = _context.Speakers.Include(s => s.SocialMedias);
            
            if (includedEvents) 
                query = query.Include(s => s.SpeakerEvents).ThenInclude(se => se.Event);

            query = query.AsNoTracking().OrderBy(s => s.Id).Where(s => s.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        async Task<Speaker[]> ISpeakerRepository.GetAllSpeakersAsync(bool includedEvents)
        {
            IQueryable<Speaker> query = _context.Speakers.Include(s => s.SocialMedias);
            
            if (includedEvents) 
                query = query.Include(s => s.SpeakerEvents).ThenInclude(se => se.Event);

            query = query.AsNoTracking().OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }

        async Task<Speaker> ISpeakerRepository.GetSpeakerByIdAsync(int speakerId, bool includedEvents)
        {
            IQueryable<Speaker> query = _context.Speakers.Include(s => s.SocialMedias);
            
            if (includedEvents) 
                query = query.Include(s => s.SpeakerEvents).ThenInclude(se => se.Event);

            query = query.AsNoTracking().OrderBy(s => s.Id).Where(s => s.Id == speakerId);

            return await query.FirstOrDefaultAsync();
        }
    }
}