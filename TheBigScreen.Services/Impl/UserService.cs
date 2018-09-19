using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBigScreen.Entities.Entities;
using TheBigScreen.Entities.Interfaces;
using TheBigScreen.Services.Interfaces;
using TheBigScreen.Services.ViewModels;

namespace TheBigScreen.Services.Impl
{
//    TODO: implement pagination later
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWatchedMovieRepository _watchedMoviesRepository;
        private readonly IBookmarkedMovieRepository _bookmarkedMoviesRepository;
        private readonly IRatedMovieRepository _ratedMoviesRepository;
        private readonly ITraktMovieRepository _traktMovieRepository;

        public UserService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ITraktMovieRepository traktMovieRepository,
            IWatchedMovieRepository watchedMoviesRepository,
            IBookmarkedMovieRepository bookmarkedMoviesRepository,
            IRatedMovieRepository ratedMoviesRepository
        )
        {
            _userRepository = userRepository;
            _watchedMoviesRepository = watchedMoviesRepository;
            _bookmarkedMoviesRepository = bookmarkedMoviesRepository;
            _ratedMoviesRepository = ratedMoviesRepository;
            _traktMovieRepository = traktMovieRepository;
        }

        public async Task<IEnumerable<TraktMovieViewModel>> GetWatchedMovies(int page, int pageSize, string userId)
        {
            var watchedMovies = await _watchedMoviesRepository.GetByUserIdAsync(userId, page, pageSize, false);
            var movies = watchedMovies.Select(wm => new TraktMovieViewModel
            {
                Title = wm.Movie.Title,
                TraktId = wm.Movie.TraktId,
                TmdbId = wm.Movie.TmdbId,
                ImdbId = wm.Movie.ImdbId,
                Slug = wm.Movie.Slug,
                Overview = wm.Movie.Overview,
                Tagline = wm.Movie.Tagline,
                Rating = wm.Movie.Rating,
                Certification = wm.Movie.Certification,
                Homepage = wm.Movie.Homepage,
                LanguageCode = wm.Movie.LanguageCode,
                Released = wm.Movie.Released,
                Runtime = wm.Movie.Runtime,
                Trailer = wm.Movie.Trailer,
                UpdatedAt = wm.Movie.UpdatedAt,
                Votes = wm.Movie.Votes,
                Year = wm.Movie.Year,
                Watched = true,
                DateWatched = wm.DateWatched
            });
            return movies;
        }

        public async Task<IEnumerable<TraktMovieViewModel>> GetBookmarkedMovies(int page, int pageSize, string userId)
        {
            var bookmarkedMovies = await _bookmarkedMoviesRepository.GetByUserIdAsync(userId, page, pageSize, false);
            var movies = bookmarkedMovies.Select(bm => new TraktMovieViewModel
            {
                Title = bm.Movie.Title,
                TraktId = bm.Movie.TraktId,
                TmdbId = bm.Movie.TmdbId,
                ImdbId = bm.Movie.ImdbId,
                Slug = bm.Movie.Slug,
                Overview = bm.Movie.Overview,
                Tagline = bm.Movie.Tagline,
                Rating = bm.Movie.Rating,
                Certification = bm.Movie.Certification,
                Homepage = bm.Movie.Homepage,
                LanguageCode = bm.Movie.LanguageCode,
                Released = bm.Movie.Released,
                Runtime = bm.Movie.Runtime,
                Trailer = bm.Movie.Trailer,
                UpdatedAt = bm.Movie.UpdatedAt,
                Votes = bm.Movie.Votes,
                Year = bm.Movie.Year,
                Bookmarked = true,
                DateBookmarked = bm.DateBookmarked
            });
            return movies;
        }

        public async Task<IEnumerable<TraktMovieViewModel>> GetRatedMovies(int page, int pageSize, string userId)
        {
            var ratedMovies = await _ratedMoviesRepository.GetByUserIdAsync(userId, page, pageSize, false);
            var movies = ratedMovies.Select(rm => new TraktMovieViewModel
            {
                Title = rm.Movie.Title,
                TraktId = rm.Movie.TraktId,
                TmdbId = rm.Movie.TmdbId,
                ImdbId = rm.Movie.ImdbId,
                Slug = rm.Movie.Slug,
                Overview = rm.Movie.Overview,
                Tagline = rm.Movie.Tagline,
                Rating = rm.Movie.Rating,
                Certification = rm.Movie.Certification,
                Homepage = rm.Movie.Homepage,
                LanguageCode = rm.Movie.LanguageCode,
                Released = rm.Movie.Released,
                Runtime = rm.Movie.Runtime,
                Trailer = rm.Movie.Trailer,
                UpdatedAt = rm.Movie.UpdatedAt,
                Votes = rm.Movie.Votes,
                Year = rm.Movie.Year,
                Rated = true,
                DateRated = rm.DateRated,
                Rate = rm.Rate
            });
            return movies;
        }

        public async Task<bool> WatchMovie(int id, string userId)
        {
            return await _watchedMoviesRepository.CreateAsync(id, userId);
        }

        public async Task<bool> BookmarkMovie(int id, string userId)
        {
            return await _bookmarkedMoviesRepository.CreateAsync(id, userId);
        }

        public async Task<bool> RateMovie(int id, string userId, int rate)
        {
            return await _ratedMoviesRepository.CreateAsync(id, userId, rate);
        }

        public async Task<bool> DeleteWatchedMovie(int id, string userId)
        {
            return await _watchedMoviesRepository.DeleteAsync(userId, id);
        }

        public async Task<bool> DeleteBookmarkedMovie(int id, string userId)
        {
            return await _bookmarkedMoviesRepository.DeleteAsync(userId, id);
        }

        public async Task<bool> DeleteRatedMovie(int id, string userId)
        {
            return await _ratedMoviesRepository.DeleteAsync(userId, id);
        }


        /*
        private TraktMovie MapViewModeltoEntity(TraktMovieViewModel movie )
        {
            return new TraktMovie
            {
                Title = movie.Title,
                TraktId = movie.TraktId,
                TmdbId = movie.TmdbId,
                ImdbId = movie.ImdbId,
                Slug = movie.Slug,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Rating = movie.Rating,
                Certification = movie.Certification,
                Homepage = movie.Homepage,
                LanguageCode = movie.LanguageCode,
                Released = movie.Released,
                Runtime = movie.Runtime,
                Trailer = movie.Trailer,
                UpdatedAt = movie.UpdatedAt,
                Votes = movie.Votes,
                Year = movie.Year,
            };
        }
        */


#region UserDetailsCrud
        public async Task<UserViewModel> GetUserOwnerDetails(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                return MapUserToUserViewModel(user);
            }
            return null;
        }
        
        public async Task<UserViewModel> CreateOrUpdateUserDetails(string userId, UserViewModel userModel)
        {
            var user = MapUserViewModelToUser(userModel);
            var userInDb = await _userRepository.CreateOrUpdateAsync(userId, user);
            return MapUserToUserViewModel(userInDb);
        }

        public async Task<bool> DeleteUserDetails(string userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }

        // TODO: refactore for the next phase
        public async Task<UserViewModel> GetUserDetails(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                return MapUserToUserViewModel(user);
            }
            return null;
        }

        private UserViewModel MapUserToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Avatar = user.Avatar,
                Gender = user.Gender,
                Active = user.Active,
            };
        }

        private User MapUserViewModelToUser(UserViewModel user)
        {
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Avatar = user.Avatar,
                Gender = user.Gender,
                Active = user.Active,
            };
        }
        #endregion
    }
}
