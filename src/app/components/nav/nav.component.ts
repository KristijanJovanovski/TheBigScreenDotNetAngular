import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth/auth.service';

@Component({selector: 'tbs-nav', templateUrl: './nav.component.html', styleUrls: ['./nav.component.scss']})
export class NavComponent implements OnInit {

    userProfile: any;
    constructor(private auth : AuthService) {
    }
    
    ngOnInit() {
        this.auth.handleAuthentication();
        this.auth.scheduleRenewal();
        this.auth.getProfile((err, profile)=>{
            this.userProfile = profile;
            console.log(this.userProfile)
        });
        
    }

}
