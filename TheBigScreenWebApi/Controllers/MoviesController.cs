using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TheBigScreen.Entities.Interfaces;
using TheBigScreen.Services.Interfaces;
using TheBigScreen.WebApi.Models;

namespace TheBigScreen.WebApi.Controllers
{

    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private string userId = "109186138895177821151";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public MoviesController(IUnitOfWork unitOfWork, IMovieService movieService, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _movieService = movieService;
            _mapper = mapper;
            _userService = userService;
        }

        #region tmdb(shuld be reimplemented)

        /*
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularMovies([FromQuery] PagingParameterModel pagingparametermodel)
        {
            // if user is authed go fetch from db info about the user interactions with those movies else return data merged from trakt and tmdb
            //TODO 
//            var popularMovies = await this._movieService.GetTraktPopularMoviesAsync(pagingparametermodel.pageNumber, pagingparametermodel.pageSize, null]
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            var popularMovies = await this._movieService.GetTmdbPopularMoviesAsync(page, pageSize, null);
            return Ok(popularMovies);
        }


        [HttpGet("box-office")]
        public async Task<IActionResult> GetBoxOfficeMovies([FromQuery] PagingParameterModel pagingparametermodel)
        {
            //TODO 
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            var boxOfficeMovies = await this._movieService.GetTmdbBoxOfficeMoviesAsync(page, pageSize, null);
            return Ok(boxOfficeMovies);
        }


        [HttpGet("top-rated")]
        public async Task<IActionResult> GetTopRatedMovies([FromQuery] PagingParameterModel pagingparametermodel)
        {
            //TODO 
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            var topRatedMovies = await this._movieService.GetTmdbTopRatedMoviesAsync(page, pageSize, null);
            return Ok(topRatedMovies);
        }


        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingMovies([FromQuery] PagingParameterModel pagingparametermodel)
        {
            //TODO 
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            var upcomingMovies = await this._movieService.GetTmdbUpcomingMoviesAsync(page, pageSize, null);
            return Ok(upcomingMovies);
        }

//        _________________________________________TMDB__________________

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            try
            {
                var movie = await _movieService.GetTmdbMovieDetailsAsync(id, null);
                if (movie != null)
                {
                    return Ok(movie);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        */
        #endregion

        [HttpGet("popular")]
        public async Task<IActionResult> GetTraktPopularMovies(
            [FromQuery] PagingParameterModel pagingparametermodel)
        {
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            
            try
            {
                var movies = await _movieService.GetTraktPopularMoviesAsync(page, pageSize, userId);
                await _unitOfWork.CompleteAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
        }


        [HttpGet("most-anticipated")]
        public async Task<IActionResult> GetTraktMostAnticipatedMovies(
            [FromQuery] PagingParameterModel pagingparametermodel)
        {
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            
            try
            {
                var movies = await _movieService.GetTraktMostAnticipatedMoviesAsync(page, pageSize, userId);
                await _unitOfWork.CompleteAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }


        [HttpGet("box-office")]
        public async Task<IActionResult> GetTraktBoxOfficeMovies(
            [FromQuery] PagingParameterModel pagingparametermodel)
        {
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            
            try
            {
                var movies = await _movieService.GetTraktBoxOfficeMoviesAsync(userId);
                await _unitOfWork.CompleteAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }


        [HttpGet("trending")]
        public async Task<IActionResult> GetTraktTrendingMovies(
            [FromQuery] PagingParameterModel pagingparametermodel)
        {
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            
            try
            {
                var movies = await _movieService.GetTraktTrendingMoviesAsync(page, pageSize, userId);
                await _unitOfWork.CompleteAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        // should make it accept period (defaults to a week)
        [HttpGet("most-watched")]
        public async Task<IActionResult> GetTraktMostWatchedMovies(
            [FromQuery] PagingParameterModel pagingparametermodel)
        {
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            
            try
            {
                var movies = await _movieService.GetTraktMostWatchedMoviesAsync(page, pageSize, userId);
                await _unitOfWork.CompleteAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTraktMovieDetails(int id)
        {
            
            try
            {
                var movie = await _movieService.GetTraktMovieDetailsAsync(id, userId);
                await _unitOfWork.CompleteAsync();
                return Ok(movie);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}/related")]
        public async Task<IActionResult> GetTraktRelatedMovies(int id, 
            [FromQuery] PagingParameterModel pagingparametermodel)
        {
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;
            
            try
            {
                var movie = await _movieService.GetTraktRelatedMoviesAsync(page, pageSize, id, userId);
                await _unitOfWork.CompleteAsync();
                return Ok(movie);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }


        //        [HttpGet("{id}/people")]
        //        public async Task<IActionResult> GetTraktPeopleMovieDetails(int id)
        //        {
        //            
        //            try
        //            {
        //                var movie = await _movieService.GetTraktCastAndCrewAsync(id, userId);
        //                await _unitOfWork.CompleteAsync();
        //                return Ok(movie);
        //            }
        //            catch (Exception e)
        //            {
        //                return BadRequest();
        //            }
        //
        //        }



        #region authedRoutesForMovies


        [Authorize("read:userdetails")]
        [HttpGet("watched")]
        public async Task<IActionResult> GetWatchedMovies([FromQuery] PagingParameterModel pagingparametermodel)
        {
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;

            try
            {
                var movies = await _userService.GetWatchedMovies(page, pageSize, userId);
                //                await _unitOfWork.CompleteAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [Authorize("read:userdetails")]
        [HttpGet("bookmarked")]
        public async Task<IActionResult> GetBookmarkedMovies([FromQuery] PagingParameterModel pagingparametermodel)
        {
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;

            try
            {
                var movies = await _userService.GetBookmarkedMovies(page, pageSize, userId);
                //                await _unitOfWork.CompleteAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [Authorize("read:userdetails")]
        [HttpGet("rated")]
        public async Task<IActionResult> GetRatedMovies([FromQuery] PagingParameterModel pagingparametermodel)
        {
            var page = pagingparametermodel.pageNumber ?? 1;
            var pageSize = pagingparametermodel.pageSize ?? 20;

            try
            {
                var movies = await _userService.GetRatedMovies(page, pageSize, userId);
                await _unitOfWork.CompleteAsync();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [Authorize("read:userdetails")]
        [HttpPost("watched/{id}")]
        public async Task<IActionResult> PostWatchedMovie(int id)
        {
            try
            {
                var watched = await _userService.WatchMovie(id, userId);
                if (watched)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok($"watched: {watched}");
                }
                else
                {
                    return NotFound($"watched: {watched}");
                }

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [Authorize("read:userdetails")]
        [HttpPost("bookmarked/{id}")]
        public async Task<IActionResult> PostBookmarkedMovie(int id)
        {
            try
            {
                var bookmarked = await _userService.BookmarkMovie(id, userId);
                if (bookmarked)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok($"bookmarked: {bookmarked}");
                }
                else
                {
                    return NotFound($"bookmarked: {bookmarked}");
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [Authorize("read:userdetails")]
        [HttpPost("rated/{id}")]
        public async Task<IActionResult> PostRatedMovie(int id, [FromBody] int rate)
        {
            try
            {
                var rated = await _userService.RateMovie(id, userId, rate);
                if (rated)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok($"rated: {rated}");
                }
                else
                {
                    return NotFound($"rated: {rated}");
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }



        [Authorize("read:userdetails")]
        [HttpDelete("watched/{id}")]
        public async Task<IActionResult> DeleteWatchedMovie(int id)
        {

            try
            {
                var watched = await _userService.DeleteWatchedMovie(id, userId);
                if (watched)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok($"unwatched: {watched}");
                }
                else
                {
                    return NotFound($"unwatched: {watched}");
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [Authorize("read:userdetails")]
        [HttpDelete("bookmarked/{id}")]
        public async Task<IActionResult> DeleteBookmarkedMovie(int id)
        {

            try
            {
                var bookmarked = await _userService.DeleteBookmarkedMovie(id, userId);
                if (bookmarked)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok($"unbookmarked: {bookmarked}");
                }
                else
                {
                    return NotFound($"unbookmarked: {bookmarked}");
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [Authorize("read:userdetails")]
        [HttpDelete("rated/{id}")]
        public async Task<IActionResult> DeleteRatedMovie(int id)
        {

            try
            {
                var rated = await _userService.DeleteRatedMovie(id, userId);
                if (rated)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok($"unrated: {rated}");
                }
                else
                {
                    return NotFound($"unrated: {rated}");
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        #endregion


    }
}
