import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Movie } from '../../models/Movie';

@Injectable()
export class StoreService {

  _popularMovies:  BehaviorSubject<Movie[]> = new BehaviorSubject<Movie[]>([]);
  _popularMoviesPage:  BehaviorSubject<number> = new BehaviorSubject<number>(0);

  _topRatedMovies:  BehaviorSubject<Movie[]> = new BehaviorSubject<Movie[]>([]);
  _topRatedMoviesPage:  BehaviorSubject<number> = new BehaviorSubject<number>(0);

  _boxOfficeMovies:  BehaviorSubject<Movie[]> = new BehaviorSubject<Movie[]>([]);
  _boxOfficeMoviesPage:  BehaviorSubject<number> = new BehaviorSubject<number>(0);

  _upcomingMovies:  BehaviorSubject<Movie[]> = new BehaviorSubject<Movie[]>([]);
  _upcomingMoviesPage:  BehaviorSubject<number> = new BehaviorSubject<number>(0);

  constructor() { }

  popularMovies(){
    return this._popularMovies.asObservable();
  }

  popularMoviesNext(movies: Movie[]){
    this._popularMovies.next(movies);
  }

  popularMoviesPage(){
    return this._popularMoviesPage.asObservable()
  }

  popularMoviesPageNext(page: number){
    this._popularMoviesPage.next(page);
  }

  topRatedMovies(){
    return this._topRatedMovies.asObservable();
  }

  topRatedMoviesNext(movies: Movie[]){
    this._topRatedMovies.next(movies);
  }

  topRatedMoviesPage(){
    return this._topRatedMoviesPage.asObservable()
  }

  topRatedMoviesPageNext(page: number){
    this._topRatedMoviesPage.next(page);
  }

  boxOfficeMovies(){
    return this._boxOfficeMovies.asObservable();
  }

  boxOfficeMoviesNext(movies: Movie[]){
    this._boxOfficeMovies.next(movies);
  }

  boxOfficeMoviesPage(){
    return this._boxOfficeMoviesPage.asObservable()
  }

  boxOfficeMoviesPageNext(page: number){
    this._boxOfficeMoviesPage.next(page);
  }

  upcomingMovies(){
    return this._upcomingMovies.asObservable();
  }

  upcomingMoviesNext(movies: Movie[]){
    this._upcomingMovies.next(movies);
  }

  upcomingMoviesPage(){
    return this._upcomingMoviesPage.asObservable()
  }

  upcomingMoviesPageNext(page: number){
    this._upcomingMoviesPage.next(page);
  }
}
