import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CanActivate } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class LandingGuard implements CanActivate {

  constructor(private auth: AuthService, private router: Router) {}

  canActivate() {
    if(this.auth.isAuthenticated()) {
        this.router.navigate(['dashboard']);
        return false;
    } 
    return true;
  }
}