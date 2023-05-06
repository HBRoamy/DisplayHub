import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { DataAccessService } from '../data-access.service';

@Component({
  selector: 'app-items-list-page',
  templateUrl: './items-list-page.component.html',
  styleUrls: ['./items-list-page.component.css']
})
export class ItemsListPageComponent implements OnInit {

  /**
   *
   */
  constructor(private dataService: DataAccessService) {

  }
  AllItems:any
  clickedActiveItem:any ={
    title:'loading...',
    description:'loading...',
    itemImageLink: 'loading...',
    creater: 'loading...'
  };
  ngOnInit(): void {
  this.myInit();    


  }
myInit()
{
  this.dataService.GetAllItems().subscribe((data: any) => {
    this.AllItems = data;
  })
}
  cardColor = "#212529"
  textColor = "#fff"
  colorCounter = 0;
  @Output() childEmitter = new EventEmitter();
  switchCardColors() {
    this.childEmitter.emit();
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

itemIsLiked=false;
currentLikes:number=0
itemIsDisliked=false;
likeItem(itemItself:any){
  //also send who liked it
  // console.warn(itemItself.likesCounter + " " + itemItself.itemId)
  let likePatchBody = [
    {
      "op": "replace",
      "path": "LikesCounter",
      "value": itemItself.likesCounter+1
    }]
  this.dataService.LikeItem(itemItself.itemId, likePatchBody).subscribe((data:any)=>{
    console.warn(data)
    itemItself.likesCounter = data
    this.itemIsLiked=true;
    //this.myInit();
  });
}



setModalItem(item:any){
this.clickedActiveItem.title = item.title;
this.clickedActiveItem.description = item.description;
this.clickedActiveItem.itemImageLink = item.itemImageLink;
this.clickedActiveItem.creater = item.creater;
// console.warn(this.clickedActiveItem)
}

}
