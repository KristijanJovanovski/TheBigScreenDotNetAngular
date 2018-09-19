import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import {Category} from '../../models/Category';
import {Asset, AssetType} from '../../models/Asset';
import { Movie } from '../../models/Movie';
import { TvShow } from '../../models/TvShow';
import { placeholder } from '../../../assets/base64/Trakt-placeholder';

@Component({selector: 'tbs-tile', templateUrl: './tile.component.html', styleUrls: ['./tile.component.scss']})
export class TileComponent implements OnInit {
    default_img: string;
    img_width : number = 185;
    readonly movieType = AssetType.Movie;
    readonly tvShowType = AssetType.TvShow;
    
    movie : Movie;
    tvShow : TvShow;

    @Input()asset : Asset;
    @Input()category : Category;
    @Input()searchMode : boolean;

    @Output()watch : EventEmitter < Asset > = new EventEmitter < Asset > ();
    @Output()bookmark : EventEmitter < Asset > = new EventEmitter < Asset > ();
    @Output()rate : EventEmitter < Asset > = new EventEmitter < Asset > ();
    @Output()play : EventEmitter < Asset > = new EventEmitter < Asset > ();
    @Output()open : EventEmitter < Asset > = new EventEmitter < Asset > ();

    constructor(){}
    
    ngOnInit() {
        let encodedPlaceholder = localStorage.getItem('placeholder');        
        if(encodedPlaceholder != null) {
            this.default_img = `data:image/png;base64,${encodedPlaceholder}`;
        }
        else{
            this.default_img = `data:image/png;base64,${placeholder}`;
            localStorage.setItem('placeholder', placeholder);
        }
        
        if(this.category.assetType === AssetType.Movie){
            this.movie = this.asset as Movie;
        }
        else if(this.category.assetType === AssetType.TvShow){
            this.tvShow = this.asset as TvShow;
        }
    }

    assetOpen(asset : Asset, event) {
        this
            .open
            .emit(asset);
    }
    assetRate(asset : Asset, event) {
        this
            .rate
            .emit(asset);
    }
    assetWatch(asset : Asset, event) {
        this
            .watch
            .emit(asset);
    }
    assetBookmark(asset : Asset, event) {
        this
            .bookmark
            .emit(asset);
    }
    assetPlay(asset : Asset, event) {
        this
            .play
            .emit(asset);
    }

}
