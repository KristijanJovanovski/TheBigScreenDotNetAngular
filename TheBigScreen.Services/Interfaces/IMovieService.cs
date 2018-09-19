using System.Collections.Generic;
using System.Threading.Tasks;
using TheBigScreen.Services.ViewModels;
using TraktApiSharp.Objects.Get.Movies;
using TraktApiSharp.Responses;

namespace TheBigScreen.Services.Interfaces
{
    public interface IMovieService
    {

        Task<TraktMovieViewModel> GetTraktMovieDetailsAsync(int id, string userId);

        Task<IEnumerable<TraktMovieViewModel>> GetTraktPopularMoviesAsync(int page, int pageSize, string userId);

        Task<IEnumerable<TraktMovieViewModel>> GetTraktMostAnticipatedMoviesAsync(int page, int pageSize, string userId);

        Task<IEnumerable<TraktMovieViewModel>> GetTraktBoxOfficeMoviesAsync(string userId);

        Task<IEnumerable<TraktMovieViewModel>> GetTraktTrendingMoviesAsync(int page, int pageSize, string userId);

        Task<IEnumerable<TraktMovieViewModel>> GetTraktMostWatchedMoviesAsync(int page, int pageSize, string userId);

//        Task<object> GetTraktCastAndCrewAsync(int id, string userId);

        Task<IEnumerable<TraktMovieViewModel>> GetTraktRelatedMoviesAsync(int page, int pageSize, int id, string userId);

        #region tmdb

        /*
        //___________________________TMDB____________________________
        Task<IEnumerable<TmdbPopularMovieDto>> GetTmdbPopularMoviesAsync(int page, int pageSize, string userId);

        Task<IEnumerable<TmdbTopRatedMovieDto>> GetTmdbTopRatedMoviesAsync(int page, int pageSize, string userId);

        Task<IEnumerable<TmdbBoxOfficeMovieDto>> GetTmdbBoxOfficeMoviesAsync(int page, int pageSize, string userId);

        Task<IEnumerable<TmdbUpcomingMovieDto>> GetTmdbUpcomingMoviesAsync(int page, int pageSize, string userId);

        Task<TmdbMovieDetailsDto> GetTmdbMovieDetailsAsync(int id, string userId);

        */

        #endregion


    }
}
 