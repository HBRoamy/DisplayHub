import { AuthGuard } from './auth.guard';
import { AccountService } from './account.service';
import { DataAccessService } from './data-access.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatCardModule } from '@angular/material/card';
// import {MatToolbarModule} from '@angular/material/toolbar';
import {MatGridListModule} from '@angular/material/grid-list';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from  '@angular/common/http';
import { ItemsListPageComponent } from './items-list-page/items-list-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorStateMatcher, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { LoginSignupComponent } from './login-signup/login-signup.component';
import { MasterComponentComponent } from './master-component/master-component.component';
import { UserPageComponent } from './user-page/user-page.component';
// import {MatInputModule} from '@angular/material/input';
@NgModule({
  declarations: [
    AppComponent,
    ItemsListPageComponent,
    LoginSignupComponent,
    MasterComponentComponent,
    UserPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatCardModule,
    ReactiveFormsModule,
    MatGridListModule,
    // MatInputModule,
    HttpClientModule
  ],
  providers: [DataAccessService,AccountService,AuthGuard,{provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher}],
  bootstrap: [AppComponent]
})
export class AppModule { }
