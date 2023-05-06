import { MasterComponentComponent } from './master-component/master-component.component';
import { UserPageComponent } from './user-page/user-page.component';
import { AppComponent } from './app.component';
import { LoginSignupComponent } from './login-signup/login-signup.component';
import { ItemsListPageComponent } from './items-list-page/items-list-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
  {path:"account", component:LoginSignupComponent},
  {path:"", component:MasterComponentComponent },
  // , 
  // {path:"allItems", component:MasterComponentComponent},
  {path:"user/:username", component:UserPageComponent },
  {path:"user", component:UserPageComponent }
  // {path:"**", component:ItemsListPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
