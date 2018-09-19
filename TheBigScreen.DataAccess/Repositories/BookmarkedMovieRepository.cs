using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBigScreen.Entities.Entities;
using TheBigScreen.Entities.Interfaces;

namespace TheBigScreen.DataAccess.Repositories
{
    public class BookmarkedMovieRepository: IBookmarkedMovieRepository
    {
        private readonly TheBigScreenDbContext _context;
        private DbSet<BookmarkedMovie> _entities;
        string errorMessage = string.Empty;

        public BookmarkedMovieRepository(TheBigScreenDbContext context)
        {
            this._context = context;
            _entities = context.Set<BookmarkedMovie>();
        }

        public async Task<IEnumerable<BookmarkedMovie>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync<BookmarkedMovie>();
        }

        public async Task<BookmarkedMovie> GetByIdAsync(string userId, long id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.MovieId == id && e.UserId == userId);
        }

        public async Task<bool> CreateAsync(int id, string userId)
        {
            var movieInDb = await GetByIdAsync(userId, id);
            if (movieInDb != null)
            {
                return false;
            }
            await _entities.AddAsync(new BookmarkedMovie
            {
                MovieId = id,
                UserId = userId,
                DateBookmarked = DateTime.Now,
                AddedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });
            return true;
        }

        public void Update(BookmarkedMovie entity)
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


        public async Task<IEnumerable<BookmarkedMovie>> GetByUserIdAsync(string userId, int page, int pageSize, bool sort)
        {
            var movies = await _entities
                .Include(m => m.Movie).ToListAsync();
            return movies.Where(bm => bm.UserId == userId).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
