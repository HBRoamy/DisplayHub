import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private router: Router, private http: HttpClient) { 
  }


  baseUrl = "https://localhost:44376/api/Account/";

  // GetCurrentUserEmailWithRole() {
  //   return this.http.get(this.baseUrl + "currentRoleUser");
  // }
  token: any = ""
  Login(loginCreds: any) {
    let body = this.http.post<any>(this.baseUrl + "login", loginCreds)
    return body;

  }

  GetUserPublicInfo(name:any)
  {
    //name = name.replace(" ","+")
    return this.http.get(this.baseUrl+"user?username="+name);
  }

  SignUp(signupCreds: any) {

    return this.http.post<any>(this.baseUrl + "Register", signupCreds);

  }
  logout() {
    if(localStorage.getItem("JWT")){
      localStorage.removeItem("JWT")
    }
    if(localStorage.getItem("email")){
      localStorage.removeItem("email");
    }
    if(localStorage.getItem("displayName")){
      localStorage.removeItem("displayName");
    }
    // if(localStorage.getItem("isLoggedIn")){
    //   localStorage.removeItem("isLoggedIn");
    // }
    if(localStorage.getItem("loginStatus"))
    {
      localStorage.removeItem("loginStatus");
    }
    localStorage.clear();
    return this.http.post<any>(this.baseUrl + "Logout", "")
  }
}
