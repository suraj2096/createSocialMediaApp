import { Component, OnChanges, SimpleChanges } from '@angular/core';
import { LoginsingupService } from './loginsingup.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  
  public constructor(public loginservice:LoginsingupService){}
  title = 'socialmediaapp';

  logout(){
    this.loginservice.logoutUser();
  }
}
