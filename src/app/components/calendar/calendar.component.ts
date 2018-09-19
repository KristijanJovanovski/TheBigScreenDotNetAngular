import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'tbs-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {
  tiles = [];
  movies = [];
  thisMonth;
  nextMonth;
  previousMonth;
  constructor() { }

  ngOnInit() {
    const possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    for(let i = 0; i < 500;i++){
      let text = ''
      for (let j = 0; j < 5; j++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));
      let movie = {
        title: text
      }
      this.movies.push(movie);
    }
    console.log(this.movies)

    const feb = new Date('2018-02-01');
    console.log(feb)
    let sliceStartIndex = 0, sliceEndIndex = 1;
    for(let i = 0; i < 31;i++){
      let tile = {
        date: new Date(`${feb.getFullYear()}-
        ${(feb.getMonth() < 10 )? feb.getMonth().toString().padStart(1) : feb.getMonth() }-
        ${((feb.getDate() + i)<10)?(feb.getDate() + i).toString().padStart(1):(feb.getDate() + i)}`),
        assets: this.movies.slice(sliceStartIndex, sliceEndIndex)
      }
      console.log(tile)
      sliceStartIndex = sliceEndIndex;
      sliceEndIndex = Math.round(Math.random()*5);
      if(sliceEndIndex > this.movies.length){
        sliceEndIndex = this.movies.length/2;
        sliceStartIndex = sliceEndIndex - 2;
      }
      this.tiles.push(tile);
    }

    const months = ['January','February','March','April','May','June','July','August','September','October','November','December'];
    const now = new Date();
    this.thisMonth = months[now.getMonth()];
    this.nextMonth = months[now.getMonth() + 1];
    this.previousMonth = months[now.getMonth() - 1];
  
  }

}
