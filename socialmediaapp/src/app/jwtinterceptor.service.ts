import { HttpInterceptor } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {HttpRequest,HttpHandler,HttpEvent} from '@angular/common/http';
import {Observable} from 'rxjs';
import { LoginsingupService } from './loginsingup.service';

@Injectable({
  providedIn: 'root'
})
export class JWTInterceptorService implements HttpInterceptor {

  constructor(public loginsignupservice:LoginsingupService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  var currentUser;
    var currentuserSession = sessionStorage.getItem("currentUser");
    if(currentuserSession!=null){
      currentUser = JSON.parse(currentuserSession);
      this.loginsignupservice.currentUserName=currentUser.user.username;
    req = req.clone({
      setHeaders:{
        Authorization:"Bearer "+currentUser.user.token
      }
    })
  }
    return next.handle(req);
  }
}
