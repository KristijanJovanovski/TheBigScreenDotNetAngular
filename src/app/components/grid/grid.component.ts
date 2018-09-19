import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Asset } from '../../models/Asset';
import { Category } from '../../models/Category';


@Component({
  selector: 'tbs-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit {

  @Input() category: Category;
  @Input() searchMode;
  @Input() assets: Asset[];
  @Input() finished: boolean;
  
  
  @Output() scrolled = new EventEmitter<boolean>();
  @Output() watch: EventEmitter<Asset> = new EventEmitter<Asset>();
  @Output() bookmark: EventEmitter<Asset> = new EventEmitter<Asset>();
  @Output() rate: EventEmitter<Asset> = new EventEmitter<Asset>();
  @Output() play: EventEmitter<Asset> = new EventEmitter<Asset>();
  @Output() open: EventEmitter<Asset> = new EventEmitter<Asset>();
  
  constructor() { }
  
  ngOnInit() {
  }
  
  assetOpen(asset: Asset){
    this.open.emit(asset);
  }
  assetRate(asset: Asset){
    this.rate.emit(asset);
  }
  assetWatch(asset: Asset){
    this.watch.emit(asset);
  }
  assetBookmark(asset: Asset){
    this.bookmark.emit(asset);
  }
  assetPlay(asset: Asset){
    this.play.emit(asset);
  }
  onScroll () {
    console.log('scrolledIn')
    this.scrolled.emit(true);
  }

}
