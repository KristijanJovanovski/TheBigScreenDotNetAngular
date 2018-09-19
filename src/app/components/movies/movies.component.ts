import { Component, OnInit, AfterViewInit, ElementRef, ViewChild, Renderer2, ChangeDetectorRef, HostListener } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
import { Category } from '../../models/Category';
import { Movie } from '../../models/Movie';
import { TmdbProvider } from '../../services/tmdb/tmdb';


import * as _ from 'lodash'
import { StoreService } from '../../services/store/store.service';
import { Asset, AssetType, AssetCategory } from '../../models/Asset';


@Component({
  selector: 'tbs-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss'],
  providers: [TmdbProvider]
})
export class MoviesComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild('tabsHorizontal',{read: ElementRef}) tabsH: ElementRef;
  @ViewChild('tabsVetrical',{read: ElementRef}) tabsV: ElementRef;
  @ViewChild('main',{read: ElementRef}) main: ElementRef;
  

  finished: boolean = false;

  categorySub: Subscription;
  categorySelected: Category;
  movies: Movie[];

  popularMoviesPage = 1;
  boxOfficeMoviesPage = 1;
  topRatedMoviesPage = 1;
  upcomingMoviesPage = 1;


  popularMoviesSubscription: Subscription;
  boxOfficeMoviesSubscription: Subscription;
  topRatedMoviesSubscription: Subscription;
  upcomingMoviesSubscription: Subscription;
  
  categoriesList: [Category] = [
    new Category(AssetCategory.Popular, 'Popular', AssetType.Movie),
    new Category(AssetCategory.TopRated, 'Top Rated', AssetType.Movie),
    new Category(AssetCategory.BoxOffice, 'Box Office', AssetType.Movie),
    new Category(AssetCategory.Upcoming, 'Upcoming', AssetType.Movie),
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private tmdb: TmdbProvider,
    private store: StoreService,
    private render: Renderer2,
    private cdr: ChangeDetectorRef,
    private el: ElementRef
  ) { }

  ngOnInit() {
    this.categorySub = this.route.data.subscribe(c => {
      this.categorySelected = this.categoriesList.find(v => v.id === (c.category ? c.category : ''));
      this.categorySelected.active = true;
      this.subscribeMovies();
      this.getMovies();
    });
    
    // console.log(this.tabsH.nativeElement);
  }
  
  ngAfterViewInit(){
    this.onResize();
  }

  ngOnDestroy(): void {
    console.log('Destroyed');    
    this.categorySub.unsubscribe();
    if(this.popularMoviesSubscription){
    this.popularMoviesSubscription.unsubscribe();
    }
    if(this.boxOfficeMoviesSubscription){
    this.boxOfficeMoviesSubscription.unsubscribe();
    }
    if(this.topRatedMoviesSubscription){
    this.topRatedMoviesSubscription.unsubscribe();
    }
    if(this.upcomingMoviesSubscription){
    this.upcomingMoviesSubscription.unsubscribe();
    }
  }

  @HostListener('window:resize', ['$event.target'])
  onResize(){
    console.log(this.tabsH);
    console.log(this.tabsV);
    

    if(this.tabsH){
      this.render.setStyle(
        this.main.nativeElement,
        'top',
        (this.tabsH.nativeElement.offsetHeight + 
        this.tabsH.nativeElement.offsetTop )+ 'px');
        
        this.cdr.detectChanges();

      this.render.setStyle(
        this.main.nativeElement,
        'margin-top',
        (this.tabsH.nativeElement.offsetHeight + 
          this.tabsH.nativeElement.offsetTop )+ 'px');

        this.cdr.detectChanges();
    }else{
      this.render.setStyle(
        this.main.nativeElement,
        'top', 57+'px');
        
        this.cdr.detectChanges();

      this.render.setStyle(
        this.main.nativeElement,
        'margin-top', 57+'px');

        this.cdr.detectChanges();
    }
  }

  subscribeMovies(){
    if(this.categorySelected.id === 'popular'){
      this.popularMoviesSubscription = this.store.popularMovies()
      .subscribe(movies=>{
        this.movies = movies;
        this.finished = true;
      });
    }else if(this.categorySelected.id === 'top-rated'){
      this.topRatedMoviesSubscription = this.store.topRatedMovies()
      .subscribe(movies=>{
        this.movies = movies;
        this.finished = true;
      });
    }else if(this.categorySelected.id === 'box-office'){
      this.boxOfficeMoviesSubscription = this.store.boxOfficeMovies()
      .subscribe(movies=>{
        this.movies = movies;
        this.finished = true;
      });
    }else if(this.categorySelected.id === 'upcoming'){
      this.upcomingMoviesSubscription = this.store.upcomingMovies()
      .subscribe(movies=>{
        this.movies = movies;
        this.finished = true;
      });
    }
  }

  getMovies(event?){
    
    if(this.categorySelected.id === 'popular'){
      this.getPopularMovies();
    }else if(this.categorySelected.id === 'top-rated'){
      this.getTopRatedMovies();
    }else if(this.categorySelected.id === 'box-office'){
      this.getBoxOfficeMovies();
    }else if(this.categorySelected.id === 'upcoming'){
      this.getUpcomingMovies();
    }
  }

  getPopularMovies(){
    this.store.popularMoviesPageNext(this.popularMoviesPage++);
    this.finished = false;
  }

  getTopRatedMovies(){
    this.store.topRatedMoviesPageNext(this.topRatedMoviesPage++);
    this.finished = false;
  }

  getBoxOfficeMovies(){
    this.store.boxOfficeMoviesPageNext(this.boxOfficeMoviesPage++);
    this.finished = false;
  }

  getUpcomingMovies(){
    this.store.upcomingMoviesPageNext(this.upcomingMoviesPage++);
    this.finished = false;
  }

  assetOpen(asset: Asset){
    let movie = asset as Movie;
    console.log('movie opened', movie);
  }
  assetRate(asset: Asset){
    let movie = asset as Movie;
    console.log('movie rated', movie);
  }
  assetWatch(asset: Asset){
    let movie = asset as Movie;
    console.log('movie watched', movie);
  }
  assetBookmark(asset: Asset){
    let movie = asset as Movie;
    console.log('movie bookmarked', movie);
  }
  assetPlay(asset: Asset){
    let movie = asset as Movie;
    console.log('movie played', movie);
    this.router.navigate(['movies', movie.id]);
  }

}
