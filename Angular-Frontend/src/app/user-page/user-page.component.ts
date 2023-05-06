import { AccountService } from './../account.service';
import { DataAccessService } from './../data-access.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent implements OnInit {


  constructor(private actiRoute: ActivatedRoute, private dataService:DataAccessService, private accountService:AccountService) {

    
  }
  
  userPublicInfo:any ={
    displayName:'loading...',
    name:'loading...'
  };

  ngOnInit(): void {
    let ans = this.actiRoute.snapshot.paramMap.get("username");
    this.actiRoute.queryParams.subscribe((data:any)=>
    {
      this.accountService.GetUserPublicInfo(ans).subscribe((userReceived:any)=>
      {
        
       this.userPublicInfo.displayName = userReceived.displayName
      //  this.userPublicInfo.name = userReceived.name

      });
    })
  }

  fullScreenState = false;
  activeColor = "#fff"
  toggleFullScreen()
  {
    if(!this.fullScreenState)
    {
      document.documentElement.requestFullscreen();
      this.activeColor = "#42b883"

      this.fullScreenState=true;
    }else{
      document.exitFullscreen();
      this.activeColor = "#fff"

      this.fullScreenState=false;
    }
  }

  cardColor = "#212529"
  textColor = "#fff"
  colorCounter = 0;
  switchCardColors() {
    if (this.colorCounter == 0) {
      this.cardColor = "#bc2525"
      this.textColor = "#fff"
      this.colorCounter++;
    } else if (this.colorCounter == 1) {
      this.cardColor = "#be3144"
      this.textColor = "#fff"
      this.colorCounter++;

    } else if (this.colorCounter == 2) {
      this.cardColor = "#005689"
      this.colorCounter++;

    } else if (this.colorCounter == 3) {
      this.cardColor = "#2c5d63"
      this.colorCounter++;

    } else {
      this.textColor = "#fff"
      this.cardColor = "#212529"
      this.colorCounter = 0;
    }
  }

}
