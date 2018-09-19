using System.Collections.Generic;
using System.Threading.Tasks;
using TheBigScreen.Entities.Entities;

namespace TheBigScreen.Entities.Interfaces
{
    public interface IBookmarkedMovieRepository
    {
        Task<IEnumerable<BookmarkedMovie>> GetAllAsync();

        Task<BookmarkedMovie> GetByIdAsync(string userId, long id);

        Task<bool> CreateAsync(int entity, string userId);

        void Update(BookmarkedMovie entity);

        Task<bool> DeleteAsync(string userId, long id);

        Task<IEnumerable<BookmarkedMovie>> GetByUserIdAsync(string userId, int page, int pageSize, bool sort);

    }
}
