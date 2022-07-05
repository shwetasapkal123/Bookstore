import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpServiceService } from '../http-service.service';

@Injectable({
  providedIn: 'root'
})
export class CartServiceService {

  token:any;

  constructor(private httpService:HttpServiceService) { 
    this.token = localStorage.getItem('token');
  }
  

  addToCart(id:any){
    console.log("token",this.token);
  
   let header ={
     headers: new HttpHeaders({
       'Content-type': 'application/json',
       'Authorization':`Bearer ${this.token}`
      })
   }
   return this.httpService.postService('/api/Cart/AddCart'+id,{},true,header)
  }
  
  getAllCartItems(){
    console.log("token",this.token);
  
   let header ={
     headers: new HttpHeaders({
       'Content-type': 'application/json',
       'Authorization':`Bearer ${this.token}`
     })
   }
   return this.httpService.getService('/api/Cart/GetCart', true,header)
  }

  UpdateCart(cartId:any,quantity:any){
    let header = {
      headers: new HttpHeaders({
        'Content-type': 'application/json',
        'Authorization': `Bearer ${this.token}`
      })
    }
    return this.httpService.patchService(`/api/Cart/UpdateCart`, {}, true, header);
  }
  
  
  removeCartItem(id:any){
    console.log("token",this.token);
  
   let header ={
     headers: new HttpHeaders({
       'Content-type': 'application/json',
       'Authorization': `Bearer ${this.token}`
     })
   }
    return this.httpService.deleteService(`/api/Cart/DeleteCart?cartId=${id}`, {}, true, header)
  }
  
}
