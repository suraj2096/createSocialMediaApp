import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocialAuthService } from 'angularx-social-login';
import { GoogleLoginProvider } from 'angularx-social-login';
import { FacebookLoginProvider } from 'angularx-social-login';
import { Login } from '../login';
import { LoginsingupService } from '../loginsingup.service';
import { Signup } from '../signup';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private loginsignupservice:LoginsingupService,public router:Router,private socailAuthService:SocialAuthService){}
 loginsignupcheck:boolean = false;
 loginbtnstyle:{}={"border-bottom":"0.2rem solid"};
 signupbtnStyle:{}={};
 loginsignup:string="login";
 displaymessage:string="";
 dangersuccess:boolean=false;

// login and singup data;
logindata:Login = new Login();
singupdata:Signup = new Signup();
reentePassword:any;
  login(){
    this.loginsignup="login"
    this.loginsignupcheck = false;
    this.loginbtnstyle={};
    this.signupbtnStyle={};
this.loginbtnstyle={"border-bottom":"0.2rem solid"};
}
signup(){
  this.loginsignup="sign up"
this.loginsignupcheck = true;
this.loginbtnstyle={};
this.signupbtnStyle={"border-bottom":"0.2rem solid"};
}

// buttons click
loginClick(){
 var loginuserdata =  this.loginsignupservice.loginuser(this.logindata).subscribe(
    (response)=>{
      console.log(response);
          this.displaymessage=response.message;
          this.logindata.username="";
          this.logindata.password="";
          this.dangersuccess=response.success;
         setTimeout(() => {
          this.displaymessage="";
          if(this.dangersuccess == true){
          this.router.navigateByUrl("home");
          }
          this.logindata.username = "";
          this.logindata.password="";
         }, (4000));
    }
  ,(error)=>{
    console.log(error);
  });
}
signupClick(){
  if(this.singupdata.password != this.reentePassword){
    return;
  }
  var signupuserdata = this.loginsignupservice.sigupuser(this.singupdata).subscribe(
    (response)=>{
      //console.log(response);
          this.displaymessage=response.message;
          this.logindata.username="";
          this.logindata.password="";
          this.dangersuccess=response.success;
         setTimeout(() => {
          this.displaymessage="";
          // if(this.dangersuccess == true){
          // this.router.navigateByUrl("home");
          // }
          this.singupdata.userEmail="";
          this.singupdata.userName="";
          this.singupdata.password="";
          this.reentePassword="";
         }, (4000));
    }
  ,(error)=>{
    console.log(error);
  });
}

LoginWithGoogle(){
this.socailAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then((response)=>{
  //console.log(response);
if(this.loginsignupcheck == false){
  this.logindata.username = response.name;
  this.logindata.password = null;
  this.logindata.provider=response.provider;
  var loginuserdata =  this.loginsignupservice.loginuser(this.logindata).subscribe(
    (response)=>{
      console.log(response);
          this.displaymessage=response.message;
          this.logindata.username="";
          this.logindata.password="";
          this.logindata.provider="";
          this.dangersuccess=response.success;
         setTimeout(() => {
          this.displaymessage="";
          if(this.dangersuccess == true){
          this.router.navigateByUrl("home");
          }
          this.logindata.username = "";
          this.logindata.password="";
         }, (4000));
    }
  ,(error)=>{
    console.log(error);
    
  });
 }
else{
  this.singupdata.userName = response.name;
  this.singupdata.userEmail = response.email;
  this.singupdata.password = "null";
  this.singupdata.provider = response.provider;
  this.singupdata.providerKey = response.id;
    var signupuserdata = this.loginsignupservice.sigupuser(this.singupdata).subscribe(
      (response)=>{
        //console.log(response);
            this.displaymessage=response.message;
            this.logindata.username="";
            this.logindata.password="";
            this.dangersuccess=response.success;
           setTimeout(() => {
            this.displaymessage="";
            // if(this.dangersuccess == true){
            // this.router.navigateByUrl("home");
            // }
            this.singupdata.userEmail="";
            this.singupdata.userName="";
            this.singupdata.password="";
            this.reentePassword="";
           }, (4000));
      }
    ,(error)=>{
      console.log(error);
    });
}
}).catch((error)=>{console.log(error)});
}

loginwithFacebook(){
  this.socailAuthService.signIn(FacebookLoginProvider.PROVIDER_ID).then(
    (response)=>{
      if(this.loginsignupcheck == false){
        this.logindata.username = response.name;
        this.logindata.password = null;
        this.logindata.provider=response.provider;
        var loginuserdata =  this.loginsignupservice.loginuser(this.logindata).subscribe(
          (response)=>{
            console.log(response);
                this.displaymessage=response.message;
                this.logindata.username="";
                this.logindata.password="";
                this.logindata.provider="";
                this.dangersuccess=response.success;
               setTimeout(() => {
                this.displaymessage="";
                if(this.dangersuccess == true){
                this.router.navigateByUrl("home");
                }
                this.logindata.username = "";
                this.logindata.password="";
               }, (4000));
          }
        ,(error)=>{
          console.log(error);
          
        });
       }
      else{
        this.singupdata.userName = response.name;
        this.singupdata.userEmail = response.email;
        this.singupdata.password = "null";
        this.singupdata.provider = response.provider;
        this.singupdata.providerKey = response.id;
          var signupuserdata = this.loginsignupservice.sigupuser(this.singupdata).subscribe(
            (response)=>{
              //console.log(response);
                  this.displaymessage=response.message;
                  this.logindata.username="";
                  this.logindata.password="";
                  this.dangersuccess=response.success;
                 setTimeout(() => {
                  this.displaymessage="";
                  // if(this.dangersuccess == true){
                  // this.router.navigateByUrl("home");
                  // }
                  this.singupdata.userEmail="";
                  this.singupdata.userName="";
                  this.singupdata.password="";
                  this.reentePassword="";
                 }, (4000));
            }
          ,(error)=>{
            console.log(error);
          });
      }    
    }).catch((errror)=>{
         console.log(errror);
    })
}
}
