export interface MovieRate{
    userId : string;
    movieId : number;
    date : Date;
    rate : number;
    
    backdrop_path:  string;
    poster_path :  string;
    title : string;
    vote_average : number;
  }
  export interface MovieBookmark{
    userId : string;
    movieId : number;
    date : Date;
    
    backdrop_path:  string;
    poster_path :  string;
    title : string;
    vote_average : number;
  }
  export interface MovieWatch{
    userId : string;
    movieId : number;
    date : Date;

    backdrop_path:  string;
    poster_path :  string;
    title : string;
    vote_average : number;
  }
  export interface TvshowRate{
    userId : string;
    tvshowId : number;
    date : Date;
    rate : number;

    backdrop_path:  string;
    poster_path :  string;
    title : string;
    vote_average : number;
  }
  export interface TvshowBookmark{
    userId : string;
    tvshowId : number;
    date : Date;
    
    backdrop_path:  string;
    poster_path :  string;
    title : string;
    vote_average : number;
  }
  export interface TvshowWatch{
    userId : string;
    tvshowId : number;
    date : Date;
    
    backdrop_path:  string;
    poster_path :  string;
    title : string;
    vote_average : number;
  }