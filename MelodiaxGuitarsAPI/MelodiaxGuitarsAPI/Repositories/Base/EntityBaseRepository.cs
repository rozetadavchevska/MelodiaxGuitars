using MelodiaxGuitarsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MelodiaxGuitarsAPI.Repositories.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class
    {
        public readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity); 
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(string id) => await _context.Set<T>().FindAsync(id);

        public async Task UpdateAsync(string id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
