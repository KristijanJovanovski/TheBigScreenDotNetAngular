using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBigScreen.Entities.Entities;
using TheBigScreen.Entities.Interfaces;

namespace TheBigScreen.DataAccess.Repositories
{
    public class RatedMovieRepository: IRatedMovieRepository
    {
        private readonly TheBigScreenDbContext _context;
        private DbSet<RatedMovie> _entities;
        string errorMessage = string.Empty;

        public RatedMovieRepository(TheBigScreenDbContext context)
        {
            this._context = context;
            _entities = context.Set<RatedMovie>();
        }

        public async Task<IEnumerable<RatedMovie>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync<RatedMovie>();
        }

        public async Task<RatedMovie> GetByIdAsync(string userId, long id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.MovieId == id && e.UserId == userId);
        }

        public async Task<bool> CreateAsync(int id, string userId, int rate)
        {
            var movieInDb = await GetByIdAsync(userId, id);
            if (movieInDb != null)
            {
                return false;
            }
            await _entities.AddAsync(new RatedMovie
            {
                MovieId = id,
                UserId = userId,
                DateRated = DateTime.Now,
                AddedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Rate = rate
            });
            return true;
        }

        public void Update(RatedMovie entity)
        {
            _entities.Update(entity);
        }

        public async Task<bool> DeleteAsync(string userId, long id)
        {
            var entity = await GetByIdAsync(userId, id);
            if (entity != null)
            {
                _entities.Remove(entity);
                return true;
            }
            return false;
        }


        public async Task<IEnumerable<RatedMovie>> GetByUserIdAsync(string userId, int page, int pageSize, bool sort)
        {
            var movies = await _entities
                .Include(m => m.Movie).ToListAsync();
            return movies.Where(rm => rm.UserId == userId).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}