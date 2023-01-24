import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { FormGroup, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { GoogleLoginProvider } from 'angularx-social-login';
import { SocialAuthService, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { FacebookLoginProvider } from 'angularx-social-login';
import { JWTInterceptorService } from './jwtinterceptor.service';
import { JwtModule} from '@auth0/angular-jwt';
import { DatePipe } from '@angular/common';


const googleLoginOptions = {
  scope: 'profile email',
  plugin_name:'sample_login' //can be any name
}; 

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config:{
        tokenGetter:()=>{
          return sessionStorage.getItem("currentUser")?JSON.parse(sessionStorage.getItem("currentUser") as string).user.token:null
        }
      }
    })
    
  ],
  providers: [
   {
    provide:'SocialAuthServiceConfig',
    useValue:{
      autoLogin:false,
      providers:[
        {
          id:GoogleLoginProvider.PROVIDER_ID,
          provider:new GoogleLoginProvider('797962498410-f16n3dc0s2772ddviov4qglvnsd8och9.apps.googleusercontent.com',googleLoginOptions)
        }
      ]
    } as SocialAuthServiceConfig
   },
   {
    provide:'SocialAuthServiceConfig',
    useValue:{
      autologin:false,
      providers:[
        {
          id:FacebookLoginProvider.PROVIDER_ID,
          provider:new FacebookLoginProvider('848401959569076')
        }
      ]
    } as SocialAuthServiceConfig
   },
   SocialAuthService,
   DatePipe,
   {
    provide:HTTP_INTERCEPTORS,
    useClass:JWTInterceptorService,
    multi:true
   }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
