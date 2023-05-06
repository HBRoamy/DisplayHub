import { Component, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { DataAccessService } from '../data-access.service';
import { ItemsListPageComponent } from '../items-list-page/items-list-page.component';

@Component({
  selector: 'app-master-component',
  templateUrl: './master-component.component.html',
  styleUrls: ['./master-component.component.css']
})
export class MasterComponentComponent {

  constructor(private formBuilder: FormBuilder, private dataService: DataAccessService, private router: Router, private account:AccountService) {
    
  }
  ngOnInit(): void {
    console.warn(this.loginStatus)
  }

  title = 'practUi';

  //USE FORM BUILDER, if meme is private, a textbox will be added to enter allowed emails


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


  hideFooter = document.getElementsByTagName('footer');
  backgroundCol = "#212529"; 
  opacity = "0.9"
  toggleStatus=0;
  
  abc =  document.getElementsByTagName("ul")
  togglePositions()
  {
    // console.warn("called")
    if(this.toggleStatus===0)// if the nav is docked
    {
      this.hideFooter[0].style.display = "block";
      this.hideFooter[0].style.backgroundColor = "#6643b5";
      this.backgroundCol ="#6643b5";
      this.toggleStatus++;
    }else if(this.toggleStatus===1)
    {
      this.backgroundCol ="#c24d2c";
      this.hideFooter[0].style.backgroundColor = "#c24d2c";
      this.toggleStatus++;
    }
    else if(this.toggleStatus===2)
    {
      this.backgroundCol ="#be3144";
      this.hideFooter[0].style.backgroundColor = "#be3144";
      this.toggleStatus++;
    }
    else if(this.toggleStatus===3)
    {
      this.backgroundCol ="#005689";
      this.hideFooter[0].style.backgroundColor = "#005689";
      this.toggleStatus++;
    }
    else if(this.toggleStatus===4)
    {
      this.backgroundCol ="#2c5d63";
      this.hideFooter[0].style.backgroundColor = "#2c5d63";
      this.toggleStatus++;
    }
    else if(this.toggleStatus===5)
    {
      this.backgroundCol ="transparent";
      this.hideFooter[0].style.display = "none";
      this.toggleStatus++;
    }
    else{
      this.backgroundCol ="#212529";
      this.hideFooter[0].style.display = "block";
      this.hideFooter[0].style.bottom = "0";

     this.hideFooter[0].style.padding = "0.4em";

      this.hideFooter[0].style.backgroundColor = "#212529";
      this.toggleStatus=0;
    }
  }

  newItemForm = this.formBuilder.group({
    Title:['',Validators.required],
    Creater:'BossXoXo',
    CreationTime: (new Date()).toISOString(),
    Description: ['',Validators.required],
    IsPrivate:false,
    ItemImageLink:'',
    ItemCategory:['',Validators.required],
    ItemFlair:false,

  })

  @ViewChild(ItemsListPageComponent)
  child!: ItemsListPageComponent;

  UploadItem(newItem:any)// why any?
  {
    if(newItem.ItemFlair==true){
      newItem.ItemFlair="NSFW"
    }else{
      newItem.ItemFlair=""
    }
    //console.warn(newItem)
    this.dataService.CreateNewItem(newItem).subscribe(data=>{
      //this.router.navigate([""]);
      this.child.myInit();
    });
    
  }
  
  logout(){
    this.account.logout().subscribe(data=>{
      this.loginStatus = null,
      this.Username = null
      this.router.navigate([""]);
    })
  }

  loginStatus = localStorage.getItem("email")
  Username = localStorage.getItem("displayName")

}
