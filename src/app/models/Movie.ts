import { Asset } from './Asset';
export class Movie extends Asset{
    
    imdb_id : string;
    original_title : string;
    title: string;
    release_date : Date;

    budget : number;
    revenue : number;
    runtime : number;
    tagline : string;
}