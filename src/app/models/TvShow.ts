import { Asset } from './Asset';
import { TvSeason } from "./TvSeason";
export class TvShow extends Asset{
    
    original_name : string;
    name: string;

    first_air_date : Date;
    last_air_date : Date;

    episode_run_time : number;
    in_production : Boolean;

    number_of_episodes : number;
    number_of_seasons : number;


    seasons : TvSeason[];

}