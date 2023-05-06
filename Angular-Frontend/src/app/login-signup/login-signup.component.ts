import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from './../account.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-login-signup',
  templateUrl: './login-signup.component.html',
  styleUrls: ['./login-signup.component.css']
})
export class LoginSignupComponent {

  constructor(private accountService: AccountService, private router: Router, private fb:FormBuilder) {

  }

  signUpForm = this.fb.group({
    Email:['',[Validators.required, Validators.email]],
    DisplayName:['',Validators.required],
    Password:['',Validators.required]
  })

  registerUser(newUserDetails: any) {
    this.accountService.SignUp(newUserDetails).subscribe((data: any) => {
      localStorage.setItem("loginStatus", data)
    })
  }

  currentUser:any={
    email:'',
    displayName:''
  }

  loginForm = this.fb.group({
    Email:['',[Validators.required, Validators.email]],
    Password:['',[Validators.required, Validators.email]]
  })
  login(userDetails: any) {
    this.accountService.Login(userDetails).subscribe((data: any) => {

      localStorage.setItem("token", data.token)
      localStorage.setItem("email", data.email)
      localStorage.setItem("displayName", data.displayName)

      this.currentUser.email = data.email
      this.currentUser.displayName = data.displayName
      this.router.navigate(["/allItems"]);
    })

  }

  logout(){
    this.accountService.logout().subscribe(data=>{
     
        localStorage.setItem("loginStatus", "false");
      
    });
  }
}
