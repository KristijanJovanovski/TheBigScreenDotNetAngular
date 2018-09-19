import {TvShow} from './../../models/TvShow';
import {Movie} from './../../models/Movie';
import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";

import { tap } from 'rxjs/operators/tap';
import { map } from "rxjs/operators";
import { Observable } from "rxjs/Rx";
// Consts
const apiKey = "api_key=";
const apiUrl = "https://api.themoviedb.org/3/"

@Injectable()
export class TmdbProvider {
  constructor(public http : HttpClient) {
    console.log('Hello TmdbProvider Provider');
  }

  getPopularMovies(page : number) : Observable < Movie[] > {
    return this
      .http
      .get(apiUrl + "movie/popular?" + apiKey + "&language=en-US&page=" + page)
      .map(response => response
        ? response['results'] as Movie[]
        : [])
      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }
  getTopRatedMovies(page : number) : Observable < Movie[] > {
    return this
      .http
      .get(apiUrl + "movie/top_rated?" + apiKey + "&language=en-US&page=" + page)

      .map(response => response
        ? response['results'] as Movie[]
        : [])
      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }
  getUpcomingMovies(page : number) : Observable < Movie[] > {
    return this
      .http
      .get(apiUrl + "movie/upcoming?" + apiKey + "&language=en-US&page=" + page)

      .map(response => response
        ? response['results'] as Movie[]
        : [])
      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }

  getBoxOfficeMovies(page : number) : Observable < Movie[] > {
    return this
      .http
      .get(apiUrl + "movie/now_playing?" + apiKey + "&language=en-US&page=" + page)

      .map(response => response
        ? response['results'] as Movie[]
        : [])
      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }

  getMovieById(id : number) : Observable < Movie > {
    return this
      .http
      .get(apiUrl + "movie/" + id + "?" + apiKey + "&language=en-US&append_to_response=videos%2Crecommendations")

      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }

  getMovieCredits(id : number) : Observable < any > {
    return this
      .http
      .get(apiUrl + "movie/" + id + "/credits?" + apiKey + "&language=en-US&append_to_response=videos%2Crecommendations")

      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }
  /*--------------------------------------------TV--------------------------------------------------*/

  getPopularTvShows(page : number) : Observable < TvShow[] > {
    return this
      .http
      .get(apiUrl + "tv/popular?" + apiKey + "&language=en-US&page=" + page)

      .map(response => response
        ? response['results'] as TvShow[]
        : [])
      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }
  getTopRatedTvShows(page : number) : Observable < TvShow[] > {
    return this
      .http
      .get(apiUrl + "tv/top_rated?" + apiKey + "&language=en-US&page=" + page)

      .map(response => response
        ? response['results'] as TvShow[]
        : [])
      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }
  getAiringTodayTvShows(page : number) : Observable < TvShow[] > {
    return this
      .http
      .get(apiUrl + "tv/airing_today?" + apiKey + "&language=en-US&page=" + page)

      .map(response => response
        ? response['results'] as TvShow[]
        : [])
      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }

  getOnTheAirTvShows(page : number) : Observable < TvShow[] > {
    return this
      .http
      .get(apiUrl + "tv/on_the_air?" + apiKey + "&language=en-US&page=" + page)

      .map(response => response
        ? response['results'] as TvShow[]
        : [])
      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }

  getTvShowsById(id : number) : Observable < TvShow > {
    return this
      .http
      .get(apiUrl + "tv/" + id + "?" + apiKey + "&language=en-US&append_to_response=videos%2Crecommendations")

      .catch(error => {
        let errMsg = (error.message)
          ? error.message
          : error.status
            ? `${error.status} - ${error.statusText}`
            : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      })
  }

}