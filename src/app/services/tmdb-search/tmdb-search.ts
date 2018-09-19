import { AssetType } from './../../models/Asset';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Movie } from './../../models/Movie';
import { TvShow } from './../../models/TvShow';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators/tap';
import { map } from "rxjs/operators";
import { Observable } from "rxjs/Rx";
import { Asset } from "../../models/Asset";
import { HttpClient } from '@angular/common/http';

// Consts
const apiKey = "api_key=";
const apiUrl = "https://api.themoviedb.org/3/"

@Injectable()
export class TmdbSearchProvider {
  
  querySubject: BehaviorSubject<string> = new BehaviorSubject(null);
  
  constructor(public http: HttpClient) {
    console.log('Hello TmdbSearchProvider Provider');
  }

  setQuery(query: string){
    this.querySubject.next(query);
  }
  getQuery():Observable<string>{
    return this.querySubject.asObservable();
  }



  searchMovies(query: string, page: number = 1):Observable<Movie[]>{
    if(query){
      return this.http.get(`${apiUrl}search/movie?query=${query}&page=${page}&include_adult=false&${apiKey}&language=en-US`)
      .do(response => {
        let results = response? response['results'] : [];
        results.forEach(element => {
          element.media_type = AssetType.Movie;
        });
      })
      .map(response => response?response['results'] as Movie[]:[])
      .catch(error => {
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      });
    }else{
      return Observable.of([]);
    }
  }

  searchTv(query: string, page: number = 1):Observable<TvShow[]>{
    if(query){
      return this.http.get(`${apiUrl}search/tv?query=${query}&page=${page}&include_adult=false&${apiKey}&language=en-US`)
      .do(response => {
        let results = response? response['results'] : [];
        results.forEach(element => {
          element.title = element.name;
          element.media_type = AssetType.TvShow;
        });
      })
      .map(response => response?response['results'] as TvShow[]:[])
      .catch(error => {
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      });
    }else{
      return Observable.of([]);
    }
  }

  searchTopResults(query: string, page: number = 1):Observable<Asset[]>{
    if(query){
      return this.http.get(`${apiUrl}search/multi?query=${query}&page=${page}&include_adult=false&${apiKey}&language=en-US`)
      .map(res=> {
        let assets: Asset[] = [];
        let results = res? res['results'] : [];
        results.forEach(element => {
          if(element.media_type === 'movie'){
            assets.push(element as Movie);
          }else if(element.media_type === 'tv'){
            element.title = element.name;
            assets.push(element as TvShow);
          }
        });
        return assets;
      })
      // .map(response => response?response.results as Asset[]:[])
      .catch(error => {
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
      });
    }else{
      return Observable.of([]);
    }
  }

}
