using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class BaseRepository<T> where T : class
    {
        protected readonly IoTHubContext _context;

        public BaseRepository(IoTHubContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<List<T>?> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
