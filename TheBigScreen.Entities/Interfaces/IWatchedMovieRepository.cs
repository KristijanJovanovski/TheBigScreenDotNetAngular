using System.Collections.Generic;
using System.Threading.Tasks;
using TheBigScreen.Entities.Entities;

namespace TheBigScreen.Entities.Interfaces
{
    public interface IWatchedMovieRepository
    {
        Task<IEnumerable<WatchedMovie>> GetAllAsync();

        Task<WatchedMovie> GetByIdAsync(string userId, long id);

        Task<bool> CreateAsync(int id, string userId);

        void Update(WatchedMovie entity);

        Task<bool> DeleteAsync(string userId, long id);

        Task<IEnumerable<WatchedMovie>> GetByUserIdAsync(string userId, int page, int pageSize, bool sort);
    }
}