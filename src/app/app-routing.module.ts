import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


import { MoviesComponent } from './components/movies/movies.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { SearchComponent } from './components/search/search.component';
import { ShowsComponent } from './components/shows/shows.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MovieDetailsComponent } from './components/movie-details/movie-details.component';
import { ProfileComponent } from './components/profile/profile.component';
import { LandingComponent } from './components/landing/landing.component';


import { LandingGuard } from './services/auth/landing-guard.service';
import { AuthGuard } from "./services/auth/auth-guard.service";


const routes: Routes = [
  {path: '', component: LandingComponent, pathMatch: 'full', canActivate: [LandingGuard]},
  {path: 'home', component: LandingComponent, pathMatch: 'full', canActivate:[AuthGuard]},
  // {path: 'home', component: DashboardComponellont, pathMatch: 'full', canActivate:[AuthGuard]},
  {path: 'dashboard', component: DashboardComponent, pathMatch: 'full', canActivate:[AuthGuard]},
  {path: 'shows', component: ShowsComponent, pathMatch: 'full'},
  {path: 'shows/:id', component: ShowsComponent, pathMatch: 'full'},
  {path: 'movies', component: MoviesComponent, data: { category: 'popular'}, pathMatch: 'full'},
  {path: 'movies/popular', component: MoviesComponent, data: { category: 'popular'}, pathMatch: 'full'},
  {path: 'movies/top-rated', component: MoviesComponent, data: { category: 'top-rated'}, pathMatch: 'full'},
  {path: 'movies/box-office', component: MoviesComponent, data: { category: 'box-office'}, pathMatch: 'full'},
  {path: 'movies/upcoming', component: MoviesComponent, data: { category: 'upcoming'}, pathMatch: 'full'},
  {path: 'movies/:id', component: MovieDetailsComponent, pathMatch: 'full'},
  {path: 'calendar', component: CalendarComponent, pathMatch: 'full'},
  {path: 'search', component: SearchComponent, pathMatch: 'full'},
  {path: 'profile', component: ProfileComponent, pathMatch: 'full', canActivate:[AuthGuard]},
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
