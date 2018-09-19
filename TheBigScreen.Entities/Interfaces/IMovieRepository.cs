using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheBigScreen.Entities;
using TheBigScreen.Entities.Entities;

namespace TheBigScreen.Entities.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();

        Task<Movie> GetByIdAsync(long id);

        Task CreateAsync(Movie entity);

        void Update(Movie entity);

        Task DeleteAsync(long id);
    }
}
