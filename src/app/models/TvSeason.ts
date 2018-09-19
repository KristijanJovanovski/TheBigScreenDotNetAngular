import { Asset } from "./Asset"
export class TvSeason extends Asset{
    id : string;

    air_date : Date;

    poster_path : string;
    
    episode_count : number;
    season_number : number;
}