import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';
import { DatePipe } from "@angular/common";

import { AppRoutingModule } from './app-routing.module';

import { ModalModule, PopoverModule, TabsModule, BsDropdownModule, CollapseModule } from 'ngx-bootstrap';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { LazyLoadImageModule } from "ng-lazyload-image";
import { ResponsiveModule, ResponsiveConfig } from 'ngx-responsive';
import { ResponsiveDefinitions } from './app-responsive-config';

import { AppComponent } from './app.component';
import { NavComponent } from './components/nav/nav.component';
import { FooterComponent } from './components/footer/footer.component';
import { SearchComponent } from './components/search/search.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { ShowsComponent } from './components/shows/shows.component';
import { MoviesComponent } from './components/movies/movies.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TileComponent } from './components/tile/tile.component';
import { GridComponent } from './components/grid/grid.component';
import { CategoryTabsComponent } from './components/category-tabs/category-tabs.component';


import { TmdbProvider } from './services/tmdb/tmdb';
import { TmdbSearchProvider } from './services/tmdb-search/tmdb-search';
import { AuthService } from './services/auth/auth.service';
import { AuthGuard } from './services/auth/auth-guard.service';
import { LandingComponent } from './components/landing/landing.component';
import { MovieDetailsComponent } from './components/movie-details/movie-details.component';
import { SettingsComponent } from './components/settings/settings.component';
import { ProfileComponent } from './components/profile/profile.component';
import { LandingGuard } from './services/auth/landing-guard.service';

@NgModule({
  declarations: [
    AppComponent,
    // NavComponent,
    FooterComponent,
    SearchComponent,
    CalendarComponent,
    ShowsComponent,
    MoviesComponent,
    DashboardComponent,
    TileComponent,
    GridComponent,
    CategoryTabsComponent,
    LandingComponent,
    MovieDetailsComponent,
    SettingsComponent,
    ProfileComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    PopoverModule,
    ModalModule,
    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    TabsModule.forRoot(),
    InfiniteScrollModule,
    LazyLoadImageModule,
    ResponsiveModule
  ],
  providers: [
    TmdbProvider,
    TmdbSearchProvider,
    AuthService,
    AuthGuard,
    LandingGuard,
    {
      provide: ResponsiveConfig,
      useFactory: ResponsiveDefinitions  
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
