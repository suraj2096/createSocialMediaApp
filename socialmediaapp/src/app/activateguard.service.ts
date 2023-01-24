import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { LoginsingupService } from './loginsingup.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ActivatedRouteSnapshot } from '@angular/router';
import { RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ActivateguardService implements CanActivate {

  constructor(private loginsignupservice:LoginsingupService,public router:Router) { }
 canActivate(route:ActivatedRouteSnapshot,state: RouterStateSnapshot):boolean
 {
    // var token = sessionStorage.getItem('currentUser')?JSON.parse(sessionStorage.getItem("currentUser") as string):null;
     if(this.loginsignupservice.isAuthenticated()){
     return true;
     }
     else{
         this.router.navigateByUrl("login");
         return false;
     }
 }
}
