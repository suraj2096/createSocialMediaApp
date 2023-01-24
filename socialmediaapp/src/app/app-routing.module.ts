import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ActivateguardService } from './activateguard.service';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {path:"",redirectTo:"home",pathMatch:'full'},
  {path:"home",component:HomeComponent,canActivate:[ActivateguardService]},
  {path:"login",component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }