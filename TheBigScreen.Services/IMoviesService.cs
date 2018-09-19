using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheBigScreen.DTOs;

namespace TheBigScreen.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetWathedMoviesAsync(long userId);
    }
}
