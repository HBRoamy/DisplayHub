import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class DataAccessService {

  constructor(private router: Router, private http: HttpClient, private user: AccountService) { }

  baseUrl = "https://localhost:44376/api/DisplayableItems/";

  GetRequests(type: number): Observable<any> {

    let token = localStorage.getItem("JWT");
    // if (!localStorage.getItem("JWT")) {
    //   // then run the get part so as to handle the 401
    // }
    //console.warn(token)
    let headerObject = new HttpHeaders().set("Authorization", "Bearer " + token)
    //load the below details only if user is an approver, so use account service
    return this.http.get(this.baseUrl + "reimbursements/lookfor/" + type, { headers: headerObject });

  }

  GetAllItems() {
    return this.http.get(this.baseUrl);
  }
//   GetRequestsForUser(email: string) {
// //console.warn(localStorage.getItem("JWT"))
//     return this.http.get(this.baseUrl + "reimbursements/Search?requestedForEmail=" + email,
//     {
//       headers: new HttpHeaders({
//         "Authorization": "Bearer " + localStorage.getItem("JWT")
//       })
//     });

    
//   }
 
  LikeItem(itemId:any, likePatchBody:any)
  {
    
    return  this.http.patch(this.baseUrl + "like/" + itemId, likePatchBody);
  }
  CreateNewItem(requestBody: any) {
    return this.http.post<any>(this.baseUrl + "Create", requestBody);
  }

  EditRequest(id: number, patchBody: any) {
    //can be used for decline, approve etc.
    return this.http.patch(this.baseUrl + "reimbursements/" + id, patchBody, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + localStorage.getItem("JWT")
      })
    });
  }
  PutEditRequest(id: number, putBody: any) {
    return this.http.put<any>(this.baseUrl + "reimbursements/" + id, putBody, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + localStorage.getItem("JWT")
      })
    });
  }
  DeleteRequest(id: number) {
    return this.http.delete(this.baseUrl + "reimbursements/" + id,{
      headers: new HttpHeaders({
        "Authorization": "Bearer " + localStorage.getItem("JWT")
      })
    });
  }

}
