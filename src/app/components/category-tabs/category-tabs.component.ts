import { Component, OnInit, AfterViewInit, Input,
   ViewChild, ElementRef, Renderer2,
    ChangeDetectorRef, ChangeDetectionStrategy } from '@angular/core';
import { TabsetComponent } from 'ngx-bootstrap';
import { Category } from '../../models/category';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';


@Component({
  selector: 'tbs-category-tabs',
  templateUrl: './category-tabs.component.html',
  styleUrls: ['./category-tabs.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CategoryTabsComponent implements OnInit, AfterViewInit {
  categorySelected: Category;
  @Input() categories: [Category];
  @Input() isVertical: boolean;

  // @ViewChild("myTabs") tabset;

  categorySub: Subscription;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private render: Renderer2,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.categorySub = this.route.data.subscribe(c => {
      this.categorySelected = this.categories.find(v => v.id === (c.category ? c.category : ''));
      this.categorySelected.active = true;
      // console.log(this.tabset);
    });
    
  }
  
  ngAfterViewInit(){
    if(this.isVertical){
    //   this.render.setProperty(this.tabset,'classMap', 'flex-column nav-stacked nav-justified')
    //   this.cdr.detectChanges();
    // }else{
    //   this.render.setProperty(this.tabset,'classMap', 'centered')
    //   this.cdr.detectChanges();
    }
    // console.log(this.tabset)
    
  }

  ngOnDestroy(): void {
    this.categorySub.unsubscribe();
  }

  tabSelect(tabz) {
    tabz.active = true;
    this.router.navigate(['movies', tabz.id]);
  }

}
