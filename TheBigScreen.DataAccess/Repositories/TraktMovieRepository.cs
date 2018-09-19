using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBigScreen.Entities.Entities;
using TheBigScreen.Entities.Interfaces;

namespace TheBigScreen.DataAccess.Repositories
{
    public class TraktMovieRepository: ITraktMovieRepository
    {
        private readonly TheBigScreenDbContext _context;
        private DbSet<TraktMovie> _entities;
        string errorMessage = string.Empty;

        public TraktMovieRepository(TheBigScreenDbContext context)
        {
            this._context = context;
            _entities = context.Set<TraktMovie>();
        }

        public async Task<IEnumerable<TraktMovie>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync<TraktMovie>();
        }

        public async Task<TraktMovie> GetByIdAsync(long id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.TraktId == id);
        }

        public async Task CreateAsync(TraktMovie entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(TraktMovie entity)
        {
            _entities.Update(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            _entities.Remove(entity);
        }

        public async Task AddOrUpdate(long id, TraktMovie movie)
        {
            var m = await GetByIdAsync(id);
            if (m != null)
            {
                Update(movie);
            }
            else
            {
                await CreateAsync(movie);
            }
        }
    }
}