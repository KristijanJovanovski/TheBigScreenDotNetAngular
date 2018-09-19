using System.Collections.Generic;
using System.Threading.Tasks;
using TheBigScreen.Entities.Entities;

namespace TheBigScreen.Entities.Interfaces
{
    public interface ITraktMovieRepository
    {
        Task<IEnumerable<TraktMovie>> GetAllAsync();

        Task<TraktMovie> GetByIdAsync(long id);

        Task CreateAsync(TraktMovie entity);

        void Update(TraktMovie entity);
        
        Task DeleteAsync(long id);

        Task AddOrUpdate(long id, TraktMovie movie);
    }
}