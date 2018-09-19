using System.Collections.Generic;
using System.Threading.Tasks;
using TheBigScreen.Entities.Entities;

namespace TheBigScreen.Entities.Interfaces
{
    public interface IRatedMovieRepository
    {
        Task<IEnumerable<RatedMovie>> GetAllAsync();

        Task<RatedMovie> GetByIdAsync(string userId, long id);
        
        Task<bool> CreateAsync(int entity, string userId, int rate);

        void Update(RatedMovie entity);

        Task<bool> DeleteAsync(string userId, long id);

        Task<IEnumerable<RatedMovie>> GetByUserIdAsync(string userId, int page, int pageSize, bool sort);

        
    }
}