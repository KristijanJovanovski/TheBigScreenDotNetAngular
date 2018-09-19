using System.Collections.Generic;
using System.Threading.Tasks;
using TheBigScreen.Services.ViewModels;

namespace TheBigScreen.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<TraktMovieViewModel>> GetWatchedMovies(int page, int pageSize, string userId);

        Task<IEnumerable<TraktMovieViewModel>> GetBookmarkedMovies(int page, int pageSize, string userId);

        Task<IEnumerable<TraktMovieViewModel>> GetRatedMovies(int page, int pageSize, string userId);
        
        Task<bool> WatchMovie(int id, string userId);

        Task<bool> BookmarkMovie(int id, string userId);

        Task<bool> RateMovie(int id, string userId, int rate);

        Task<bool> DeleteWatchedMovie(int id, string userId);

        Task<bool> DeleteBookmarkedMovie(int id, string userId);

        Task<bool> DeleteRatedMovie(int id, string userId);


#region userDetails

        Task<UserViewModel> GetUserOwnerDetails(string userId);

        Task<UserViewModel> CreateOrUpdateUserDetails(string userId, UserViewModel userModel);
        
        Task<bool> DeleteUserDetails(string userId);

        Task<UserViewModel> GetUserDetails(string id);
        #endregion


    }
}
