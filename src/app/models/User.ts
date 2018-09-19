import { TvEpisode } from './TvEpisode';
import { TvSeason } from './TvSeason';
import { TvShow } from './TvShow';
import { Movie } from './Movie';
export class User {
    uid: string;
    firstName: string;
    lastName: string;
    username: string;
    avatar: any;
    lastLogin: string;
    totalMovieTime: number;
    totalTvTime: number;
    gender: string;
    email: string;

//  Lists
    bookmarkedMovies: [Movie];
    bookmarkedTvShows: [TvShow];

    ratedMovies: [Movie];
    ratedTvShows: [TvShow];
    ratedTvSeasons: [TvSeason];
    ratedTvEpisodes: [TvEpisode];

    watchedMovies: [Movie];
    watchedTvShows: [TvShow];
    watchedTvEpisodes: [TvEpisode];

    logins: [Date]
}