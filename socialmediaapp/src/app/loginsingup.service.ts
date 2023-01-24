import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import{Observable} from 'rxjs';
import { Login } from './login';
import { Signup } from './signup';
import {map} from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class LoginsingupService {
  currentUserName:any="";
  constructor(private httpclient:HttpClient,public router:Router,private jwthelperservice:JwtHelperService) { }
  loginuser(logindata:Login):Observable<any>
  {
     return this.httpclient.post<Login>
     ("https://localhost:44304/api/login",logindata)
     .pipe(map(u=>{
      if(u){
        this.currentUserName=u.username;
        sessionStorage["currentUser"]=JSON.stringify(u);
      }
      return u;
     }));
  };
  sigupuser(singupdata:Signup):Observable<any>{
    return this.httpclient.post<Signup>("https://localhost:44304/api/signup",singupdata);
  }

  logoutUser(){
    this.currentUserName='';
    sessionStorage.removeItem("currentUser");
   this.router.navigateByUrl("login");
  }
  
  public isAuthenticated():boolean{
    if(this.jwthelperservice.isTokenExpired()){
      return false;
    }
    return true;
  }

}
