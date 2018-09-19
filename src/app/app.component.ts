import { Component } from '@angular/core';
import { OnInit, OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';
import { Movie } from './models/Movie';
import { StoreService } from './services/store/store.service';

import * as _ from 'lodash'
import { TmdbProvider } from './services/tmdb/tmdb';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from './services/auth/auth.service';

import { placeholder } from '../assets/base64/Trakt-placeholder'

@Component({
  selector: 'tbs-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [AuthService, StoreService, TmdbProvider]
})
export class AppComponent implements OnInit, OnDestroy{
  isCollapsed: boolean = true;
// deleter these functions and their references in the html
  collapsed(event: any): void {
    console.log(event);
  }
 
  expanded(event: any): void {
    console.log(event);
  }
  userProfile: any;
  readonly pageSize = 20;
  popularMovies: Movie[] = [];
  boxOfficeMovies: Movie[] = [];
  topRatedMovies: Movie[] = [];
  upcomingMovies: Movie[] = [];

  popularMoviesSubscription: Subscription;
  boxOfficeMoviesSubscription: Subscription;
  topRatedMoviesSubscription: Subscription;
  upcomingMoviesSubscription: Subscription;

  popularMoviesPage = 0;
  boxOfficeMoviesPage = 0;
  topRatedMoviesPage = 0;
  upcomingMoviesPage = 0;
  
  constructor(
    private store: StoreService,
    private tmdb: TmdbProvider,
    private auth: AuthService
  ){ }
  
  ngOnInit(): void {
    console.log('app init');
    let lsPlaceholder = localStorage.getItem('placeholder');
    if(lsPlaceholder == null){
      localStorage.setItem('placeholder', placeholder);
    }

    this.auth.handleAuthentication();
    if(this.auth.isAuthenticated()){
      this.getProfile();
    }
    this.subscribeToProviders();
  }
  ngOnDestroy(): void {
    console.log('app destroy');

    this.popularMoviesSubscription.unsubscribe();
    this.boxOfficeMoviesSubscription.unsubscribe();
    this.topRatedMoviesSubscription.unsubscribe();
    this.upcomingMoviesSubscription.unsubscribe();
  }
  
  subscribeToProviders(){
    this.popularMoviesSubscription = this.store.popularMoviesPage()
      .subscribe(page=>{
        if(this.popularMoviesPage < page){
          this.getPopularMovies(page);
          this.popularMoviesPage = page;
        }else{
          let localMovies = _.slice(this.popularMovies, 0, this.pageSize * page);
          this.store.popularMoviesNext(localMovies);
        }
      });

    this.boxOfficeMoviesSubscription = this.store.boxOfficeMoviesPage()
      .subscribe(page=>{
        if(this.boxOfficeMoviesPage < page){
          this.getBoxOfficeMovies(page);
          this.boxOfficeMoviesPage = page;
        }else{
          let localMovies = _.slice(this.boxOfficeMovies, 0, this.pageSize * page);
          this.store.boxOfficeMoviesNext(localMovies);
        }     
      });

    this.topRatedMoviesSubscription = this.store.topRatedMoviesPage()
      .subscribe(page=>{
        if(this.topRatedMoviesPage < page){
          this.getTopRatedMovies(page);
          this.topRatedMoviesPage = page;
        }else{
          let localMovies = _.slice(this.topRatedMovies, 0, this.pageSize * page);
          this.store.topRatedMoviesNext(localMovies);
        }     
      });

    this.upcomingMoviesSubscription = this.store.upcomingMoviesPage()
      .subscribe(page=>{
        if(this.upcomingMoviesPage < page){
          this.getUpcomingMovies(page);
          this.upcomingMoviesPage = page;
        }else{
          let localMovies = _.slice(this.upcomingMovies, 0,this.pageSize * page);
          this.store.upcomingMoviesNext(localMovies);
        }  
      });
  }

  
  getPopularMovies(page){
    this.tmdb.getPopularMovies(page)
    .subscribe(movies=>{
      this.popularMovies = _.concat(this.popularMovies, movies);
      this.store.popularMoviesNext(this.popularMovies);
    });
  }

  getBoxOfficeMovies(page){
    this.tmdb.getBoxOfficeMovies(page)
    .subscribe(movies=>{
      this.boxOfficeMovies = _.concat(this.boxOfficeMovies, movies);
      this.store.boxOfficeMoviesNext(this.boxOfficeMovies);
    });
  }

  getTopRatedMovies(page){
    this.tmdb.getTopRatedMovies(page)
    .subscribe(movies=>{
      this.topRatedMovies = _.concat(this.topRatedMovies, movies);
      this.store.topRatedMoviesNext(this.topRatedMovies);
    });
  }

  getUpcomingMovies(page){
    this.tmdb.getUpcomingMovies(page)
    .subscribe(movies=>{
      this.upcomingMovies = _.concat(this.upcomingMovies, movies);
      this.store.upcomingMoviesNext(this.upcomingMovies);
    });
  }

  getProfile(){
    this.auth.getProfile((err, profile)=>{
      this.userProfile = profile;
      // console.log(this.userProfile)
  });
  }

}
