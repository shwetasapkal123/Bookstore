import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpServiceService } from '../http-service.service';

@Injectable({
  providedIn: 'root'
})
export class OrderServiceService {

  token:any;

  constructor(private httpService:HttpServiceService) { 
    this.token=localStorage.getItem('token');
  }

  addOrder(UserId:any,BookId:any,BookCount:any) {
    // console.log(reqData)
    let header = {
      headers: new HttpHeaders({
        'Content-type': 'application/json',
        'Authorization': `Bearer ${this.token}`
      })
    }
    return this.httpService.postService(`/AddOrder?UserId=${UserId}&BookId=${BookId}&BookCount=${BookCount}`, {}, true, header);
  }

  getAllOrders() {
    let header = {
      headers: new HttpHeaders({
        'Content-type': 'application/json',
        'Authorization': `Bearer ${this.token}`
      })
    }
    return this.httpService.getService('/GetAllOrders', true, header);
  }
}
