using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBigScreen.Entities.Entities;
using TheBigScreen.Entities.Interfaces;
using TheBigScreen.Services.Interfaces;
using TheBigScreen.Services.ViewModels;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TraktApiSharp;
using TraktApiSharp.Requests.Parameters;

namespace TheBigScreen.Services.Impl
{
    public class MovieService: IMovieService
    {
        private readonly TMDbClient _tmdb;
        private readonly TraktClient _trakt;
        private readonly IUserRepository _userRepository;
        private readonly IWatchedMovieRepository _watchedMoviesRepositoy;
        private readonly IBookmarkedMovieRepository _bookmarkedMoviesRepository;
        private readonly IRatedMovieRepository _ratedMoviesRepository;
        private readonly ITraktMovieRepository _traktMovieRepository;

        public MovieService(
            IUnitOfWork unitOfWork,
            TraktClient trakt,
            TMDbClient tmdb,
            IUserRepository userRepository,
            ITraktMovieRepository traktMovieRepository,
            IWatchedMovieRepository watchedMoviesRepositoy,
            IBookmarkedMovieRepository bookmarkedMoviesRepository,
            IRatedMovieRepository ratedMoviesRepository
            )
        {
            _tmdb = tmdb;
            _trakt = trakt;
            _userRepository = userRepository;
            _watchedMoviesRepositoy = watchedMoviesRepositoy;
            _bookmarkedMoviesRepository = bookmarkedMoviesRepository;
            _ratedMoviesRepository = ratedMoviesRepository;
            _traktMovieRepository = traktMovieRepository;
        }

        public async Task<TraktMovieViewModel> GetTraktMovieDetailsAsync( int id, string userId)
        {
//            var posterPath = await GetTmdbMovieDetailsAsync(id, userId);
            var movie = await _trakt.Movies.GetMovieAsync(id.ToString(),
                extendedInfo: new TraktExtendedInfo {Full = true});
            var traktMovie = new TraktMovieViewModel
            {
                Title = movie.Value.Title,
                TraktId = (int) movie.Value.Ids.Trakt,
                TmdbId = (int?) movie.Value.Ids.Tmdb,
                ImdbId = movie.Value.Ids.Imdb,
                Slug = movie.Value.Ids.Slug,
                Overview = movie.Value.Overview,
                Tagline = movie.Value.Tagline,
                Rating = movie.Value.Rating,
                Certification = movie.Value.Certification,
                Homepage = movie.Value.Homepage,
                LanguageCode = movie.Value.LanguageCode,
                Released = movie.Value.Released,
                Runtime = movie.Value.Runtime,
                Trailer = movie.Value.Trailer,
                UpdatedAt = movie.Value.UpdatedAt,
                Votes = movie.Value.Votes,
                Year = movie.Value.Year,
//                PosterPath = posterPath
            };

//            await AddOrUpdateTraktMovie(traktMovie);

//            if (userId != null)
//            {
//                return await CheckUserInteraction(userId, traktMovie);
//            }


            return traktMovie;
        }

        public async Task<IEnumerable<TraktMovieViewModel>> GetTraktPopularMoviesAsync( int page, int pageSize, string userId = null)
        {
            var popularMovies = await _trakt.Movies.GetPopularMoviesAsync(page: page,
                limitPerPage: pageSize,
                extendedInfo: new TraktExtendedInfo {Full = true}
                );
            var traktMovies = popularMovies.Select(t => new TraktMovieViewModel
            {
                Title = t.Title,
                TraktId = (int) t.Ids.Trakt,
                TmdbId = (int?) t.Ids.Tmdb,
                ImdbId = t.Ids.Imdb,
                Slug = t.Ids.Slug,
                Overview = t.Overview,
                Tagline = t.Tagline,
                Rating = t.Rating,
                Certification = t.Certification,
                Homepage = t.Homepage,
                LanguageCode = t.LanguageCode,
                Released = t.Released,
                Runtime = t.Runtime,
                Trailer = t.Trailer,
                UpdatedAt = t.UpdatedAt,
                Votes = t.Votes,
                Year = t.Year
            });

            var movies = await GetTmdbMovieDetailsAsync(traktMovies);

            return movies;

            //            await AddOrUpdateTraktMovies(traktMovies);

            //            if (userId != null)
            //            {
            //                return CheckUserInteraction(userId, traktMovies);
            //            }

        }

        public async Task<IEnumerable<TraktMovieViewModel>> GetTraktMostAnticipatedMoviesAsync(int page, int pageSize, string userId)
        {
            var anticipatedMovies = await _trakt.Movies.GetMostAnticipatedMoviesAsync(page: page,
                limitPerPage: pageSize,
                extendedInfo: new TraktExtendedInfo {Full = true}
            );
            var traktMovies = anticipatedMovies.Select(t => new TraktMovieViewModel
            {
                Title = t.Title,
                TraktId = (int) t.Ids.Trakt,
                TmdbId = (int?) t.Ids.Tmdb,
                ImdbId = t.Ids.Imdb,
                Slug = t.Ids.Slug,
                Overview = t.Overview,
                Tagline = t.Tagline,
                Rating = t.Rating,
                Certification = t.Certification,
                Homepage = t.Homepage,
                LanguageCode = t.LanguageCode,
                Released = t.Released,
                Runtime = t.Runtime,
                Trailer = t.Trailer,
                UpdatedAt = t.UpdatedAt,
                Votes = t.Votes,
                Year = t.Year,
                ListCountAnticipated = t.ListCount
            });

            //            await AddOrUpdateTraktMovies(traktMovies);

            //            if (userId != null)
            //            {
            //                return CheckUserInteraction(userId, traktMovies);
            //            }
            var movies = await GetTmdbMovieDetailsAsync(traktMovies);

            return movies;

        }

        public async Task<IEnumerable<TraktMovieViewModel>> GetTraktBoxOfficeMoviesAsync(string userId)
        {
            var boxOfficeMovies = await _trakt.Movies.GetBoxOfficeMoviesAsync(extendedInfo: new TraktExtendedInfo {Full = true});
            var traktMovies = boxOfficeMovies.Select(t => new TraktMovieViewModel
            {
                Title = t.Title,
                TraktId = (int) t.Ids.Trakt,
                TmdbId = (int?) t.Ids.Tmdb,
                ImdbId = t.Ids.Imdb,
                Slug = t.Ids.Slug,
                Overview = t.Overview,
                Tagline = t.Tagline,
                Rating = t.Rating,
                Certification = t.Certification,
                Homepage = t.Homepage,
                LanguageCode = t.LanguageCode,
                Released = t.Released,
                Runtime = t.Runtime,
                Trailer = t.Trailer,
                UpdatedAt = t.UpdatedAt,
                Votes = t.Votes,
                Year = t.Year,
                RevenueBoxOffice = t.Revenue
            });

            //            await AddOrUpdateTraktMovies(traktMovies);

            //            if (userId != null)
            //            {
            //                return CheckUserInteraction(userId, traktMovies);
            //            }

            var movies = await GetTmdbMovieDetailsAsync(traktMovies);

            return movies;
        }

        public async Task<IEnumerable<TraktMovieViewModel>> GetTraktTrendingMoviesAsync(int page, int pageSize, string userId)
        {
            var trendingMovies = await _trakt.Movies.GetTrendingMoviesAsync(page: page,
                limitPerPage: pageSize,
                extendedInfo: new TraktExtendedInfo {Full = true}
            );
            var traktMovies = trendingMovies.Select(t => new TraktMovieViewModel
            {
                Title = t.Title,
                TraktId = (int) t.Ids.Trakt,
                TmdbId = (int?) t.Ids.Tmdb,
                ImdbId = t.Ids.Imdb,
                Slug = t.Ids.Slug,
                Overview = t.Overview,
                Tagline = t.Tagline,
                Rating = t.Rating,
                Certification = t.Certification,
                Homepage = t.Homepage,
                LanguageCode = t.LanguageCode,
                Released = t.Released,
                Runtime = t.Runtime,
                Trailer = t.Trailer,
                UpdatedAt = t.UpdatedAt,
                Votes = t.Votes,
                Year = t.Year,
                WatchersTrending = t.Watchers
            });

            //            await AddOrUpdateTraktMovies(traktMovies);

            //            if (userId != null)
            //            {
            //                return CheckUserInteraction(userId, traktMovies);
            //            }
            var movies = await GetTmdbMovieDetailsAsync(traktMovies);

            return movies;
        }

        public async Task<IEnumerable<TraktMovieViewModel>> GetTraktMostWatchedMoviesAsync(int page, int pageSize, string userId)
        {
            var mostWatchedMovies = await _trakt.Movies.GetMostWatchedMoviesAsync(page: page,
                limitPerPage: pageSize,
                extendedInfo: new TraktExtendedInfo {Full = true}
            );
            var traktMovies = mostWatchedMovies.Select(t => new TraktMovieViewModel
            {
                Title = t.Title,
                TraktId = (int) t.Ids.Trakt,
                TmdbId = (int?) t.Ids.Tmdb,
                ImdbId = t.Ids.Imdb,
                Slug = t.Ids.Slug,
                Overview = t.Overview,
                Tagline = t.Tagline,
                Rating = t.Rating,
                Certification = t.Certification,
                Homepage = t.Homepage,
                LanguageCode = t.LanguageCode,
                Released = t.Released,
                Runtime = t.Runtime,
                Trailer = t.Trailer,
                UpdatedAt = t.UpdatedAt,
                Votes = t.Votes,
                Year = t.Year,
                WatcherCountPWC = t.WatcherCount,
                PlayCountPWC = t.PlayCount
            });

            //            await AddOrUpdateTraktMovies(traktMovies);

            //            if (userId != null)
            //            {
            //                return CheckUserInteraction(userId, traktMovies);
            //            }
            var movies = await GetTmdbMovieDetailsAsync(traktMovies);

            return movies;
        }

        public async Task<IEnumerable<TraktMovieViewModel>> GetTraktRelatedMoviesAsync(int page, int pageSize, int id, string userId )
        {
            var relatedMovies = await _trakt.Movies.GetMovieRelatedMoviesAsync(id.ToString(), new TraktExtendedInfo {Full = true}, page, pageSize);
            var traktMovies = relatedMovies.Select(t => new TraktMovieViewModel
            {
                Title = t.Title,
                TraktId = (int) t.Ids.Trakt,
                TmdbId = (int?) t.Ids.Tmdb,
                ImdbId = t.Ids.Imdb,
                Slug = t.Ids.Slug,
                Overview = t.Overview,
                Tagline = t.Tagline,
                Rating = t.Rating,
                Certification = t.Certification,
                Homepage = t.Homepage,
                LanguageCode = t.LanguageCode,
                Released = t.Released,
                Runtime = t.Runtime,
                Trailer = t.Trailer,
                UpdatedAt = t.UpdatedAt,
                Votes = t.Votes,
                Year = t.Year
            });

            //            await AddOrUpdateTraktMovies(traktMovies);

            //            if (userId != null)
            //            {
            //                return CheckUserInteraction(userId, traktMovies);
            //            }
            
            var movies = await GetTmdbMovieDetailsAsync(traktMovies);

            return movies;
        }

        private IEnumerable<TraktMovieViewModel> CheckUserInteraction(string userId, IEnumerable<TraktMovieViewModel> movies)
        {
            movies.ToList().ForEach(async movie =>
            {
                var bookmarked = await _bookmarkedMoviesRepository.GetByIdAsync(userId, movie.TraktId);
                var rated = await _ratedMoviesRepository.GetByIdAsync(userId, movie.TraktId);
                var watched = await _watchedMoviesRepositoy.GetByIdAsync(userId, movie.TraktId);

                if (bookmarked != null)
                {
                    movie.Bookmarked = true;
                    movie.DateBookmarked = bookmarked.DateBookmarked;
                }
                if (rated != null)
                {
                    movie.Rated = true;
                    movie.DateRated = rated.DateRated;
                    movie.Rate = rated.Rate;
                }
                if (watched != null)
                {
                    movie.Watched = true;
                    movie.DateWatched = watched.DateWatched;
                }
            });
            return movies;
        }

        private async Task<TraktMovieViewModel> CheckUserInteraction(string userId,TraktMovieViewModel movie)
        {
            var bookmarked = await _bookmarkedMoviesRepository.GetByIdAsync(userId, movie.TraktId);
            var rated = await _ratedMoviesRepository.GetByIdAsync(userId, movie.TraktId);
            var watched = await _watchedMoviesRepositoy.GetByIdAsync(userId, movie.TraktId);

            if (bookmarked != null)
            {
                movie.Bookmarked = true;
                movie.DateBookmarked = bookmarked.DateBookmarked;
            }
            if (rated != null)
            {
                movie.Rated = true;
                movie.DateRated = rated.DateRated;
                movie.Rate = rated.Rate;
            }
            if (watched != null)
            {
                movie.Watched = true;
                movie.DateWatched = watched.DateWatched;
            }
            
            return movie;
        }

        private async Task AddOrUpdateTraktMovies(IEnumerable<TraktMovieViewModel> traktMovies)
        {
            traktMovies.ToList().ForEach(async movie =>
            {
                await AddOrUpdateTraktMovie(movie);
            });
        }

        private async Task AddOrUpdateTraktMovie(TraktMovieViewModel movie)
        {
            
                await _traktMovieRepository.AddOrUpdate(movie.TraktId, new TraktMovie {
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
                    Year = movie.Year
                });
        }

        private async Task<IEnumerable<TraktMovieViewModel>> GetTmdbMovieDetailsAsync(IEnumerable<TraktMovieViewModel> traktMovies)
        {
            List<TraktMovieViewModel> movies = new List<TraktMovieViewModel>();
            foreach (var m in traktMovies)
            {
                if (m.TmdbId != null)
                {
                    var movie = await _tmdb.GetMovieAsync((int)m.TmdbId, MovieMethods.Credits
                                                              | MovieMethods.Images
                                                              | MovieMethods.Recommendations
                                                              | MovieMethods.Credits
                                                              | MovieMethods.Videos);
                    movies.Add(new TraktMovieViewModel
                    {
                        Title = m.Title,
                        TraktId = m.TraktId,
                        TmdbId = m.TmdbId,
                        ImdbId = m.ImdbId,
                        Slug = m.Slug,
                        Overview = m.Overview,
                        Tagline = m.Tagline,
                        Rating = m.Rating,
                        Certification = m.Certification,
                        Homepage = m.Homepage,
                        LanguageCode = m.LanguageCode,
                        Released = m.Released,
                        Runtime = m.Runtime,
                        Trailer = m.Trailer,
                        UpdatedAt = m.UpdatedAt,
                        Votes = m.Votes,
                        Year = m.Year,
                        PosterPath = movie.PosterPath
                    });
                }
            }
            return movies;
        }

        #region tmdb
            /*
            //        _______________________________________TMDB_________________________________


            public async Task<IEnumerable<TmdbPopularMovieDto>> GetTmdbPopularMoviesAsync(int page, int pageSize, string userId)
            {
                var tmdbMovies = await _tmdb.GetMoviePopularListAsync("en-US", page);
                var tmdbPopularMovies = tmdbMovies.Results.Select(movie => new TmdbPopularMovieDto
                {
                    Adult = movie.Adult,
                    BackdropPath = movie.BackdropPath,
                    GenreIds = movie.GenreIds,
                    Id = movie.Id,
                    MediaType = movie.MediaType,
                    OriginalLanguage = movie.OriginalLanguage,
                    OriginalTitle = movie.OriginalTitle,
                    Overview = movie.Overview,
                    Popularity = movie.Popularity,
                    PosterPath = movie.PosterPath,
                    ReleaseDate = movie.ReleaseDate,
                    Title = movie.Title,
                    Video = movie.Video,
                    VoteAverage = movie.VoteAverage,
                    VoteCount = movie.VoteCount
                });

                return tmdbPopularMovies;
            }

            public async Task<IEnumerable<TmdbTopRatedMovieDto>> GetTmdbTopRatedMoviesAsync(int page, int pageSize, string userId)
            {
                var tmdbMovies = await _tmdb.GetMovieTopRatedListAsync("en-US", page);
                var tmdbTopRatedMovies = tmdbMovies.Results.Select(movie => new TmdbTopRatedMovieDto
                {
                    Adult = movie.Adult,
                    BackdropPath = movie.BackdropPath,
                    GenreIds = movie.GenreIds,
                    Id = movie.Id,
                    MediaType = movie.MediaType,
                    OriginalLanguage = movie.OriginalLanguage,
                    OriginalTitle = movie.OriginalTitle,
                    Overview = movie.Overview,
                    Popularity = movie.Popularity,
                    PosterPath = movie.PosterPath,
                    ReleaseDate = movie.ReleaseDate,
                    Title = movie.Title,
                    Video = movie.Video,
                    VoteAverage = movie.VoteAverage,
                    VoteCount = movie.VoteCount
                });

                return tmdbTopRatedMovies;
            }

            public async Task<IEnumerable<TmdbBoxOfficeMovieDto>> GetTmdbBoxOfficeMoviesAsync(int page, int pageSize, string userId)
            {
                var tmdbMovies = await _tmdb.GetMovieNowPlayingListAsync("en-US", page);
                var tmdbBoxOfficeMovies = tmdbMovies.Results.Select(movie => new TmdbBoxOfficeMovieDto
                {
                    Adult = movie.Adult,
                    BackdropPath = movie.BackdropPath,
                    GenreIds = movie.GenreIds,
                    Id = movie.Id,
                    MediaType = movie.MediaType,
                    OriginalLanguage = movie.OriginalLanguage,
                    OriginalTitle = movie.OriginalTitle,
                    Overview = movie.Overview,
                    Popularity = movie.Popularity,
                    PosterPath = movie.PosterPath,
                    ReleaseDate = movie.ReleaseDate,
                    Title = movie.Title,
                    Video = movie.Video,
                    VoteAverage = movie.VoteAverage,
                    VoteCount = movie.VoteCount
                });

                return tmdbBoxOfficeMovies;
            }

            public async Task<IEnumerable<TmdbUpcomingMovieDto>> GetTmdbUpcomingMoviesAsync(int page, int pageSize, string userId)
            {
                var tmdbMovies = await _tmdb.GetMovieUpcomingListAsync("en-US", page);
                var tmdbUpcomingMovies = tmdbMovies.Results.Select(movie => new TmdbUpcomingMovieDto
                {
                    Adult = movie.Adult,
                    BackdropPath = movie.BackdropPath,
                    GenreIds = movie.GenreIds,
                    Id = movie.Id,
                    MediaType = movie.MediaType,
                    OriginalLanguage = movie.OriginalLanguage,
                    OriginalTitle = movie.OriginalTitle,
                    Overview = movie.Overview,
                    Popularity = movie.Popularity,
                    PosterPath = movie.PosterPath,
                    ReleaseDate = movie.ReleaseDate,
                    Title = movie.Title,
                    Video = movie.Video,
                    VoteAverage = movie.VoteAverage,
                    VoteCount = movie.VoteCount
                });

                return tmdbUpcomingMovies;
            }

            public async Task<TmdbMovieDetailsDto> GetTmdbMovieDetailsAsync(int id, string userId)
            {

                var movie = await _tmdb.GetMovieAsync(id , MovieMethods.Credits
                                       | MovieMethods.Images
                                       | MovieMethods.Recommendations
                                       | MovieMethods.Credits
                                       | MovieMethods.Videos);
                var movieDetails = new TmdbMovieDetailsDto
                {
                    Adult = movie.Adult,
                    BackdropPath = movie.BackdropPath,
                    Id = movie.Id,
                    OriginalLanguage = movie.OriginalLanguage,
                    OriginalTitle = movie.OriginalTitle,
                    Overview = movie.Overview,
                    Popularity = movie.Popularity,
                    PosterPath = movie.PosterPath,
                    ReleaseDate = movie.ReleaseDate,
                    Title = movie.Title,
                    Translations = movie.Translations,
                    Video = movie.Video,
                    VoteAverage = movie.VoteAverage,
                    VoteCount = movie.VoteCount,
                    Budget = movie.Budget,
                    Credits = movie.Credits,
                    ExternalIds = movie.ExternalIds,
                    Recommendations = movie.Recommendations,
                    Similar = movie.Similar,
                    ImdbId = movie.ImdbId,
                    Images = movie.Images,
                    Videos = movie.Videos,
                    Revenue = movie.Revenue,
                    ReleaseDates = movie.ReleaseDates
                };

                return movieDetails;

            }
            */
            #endregion
        }
}
