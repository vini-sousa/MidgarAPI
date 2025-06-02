using Midgar.Persistence.Context;
using Midgar.Persistence.Interfaces;

namespace Midgar.Persistence.Repositories
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly MidgarContext _context;
        public GeneralRepository(MidgarContext context)
        {
            _context = context;
        }

        void IGeneralRepository.Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        void IGeneralRepository.Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        void IGeneralRepository.Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);       
        }

        void IGeneralRepository.DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        async Task<bool> IGeneralRepository.SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}