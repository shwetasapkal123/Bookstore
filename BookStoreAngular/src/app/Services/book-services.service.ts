import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpServiceService } from './http-service.service';

@Injectable({
  providedIn: 'root'
})
export class BookServicesService {

  token: any;

  constructor(private httpService:HttpServiceService) { 
    this.token = localStorage.getItem('token');
  }

  getallBook()
  {
    let header ={
      headers: new HttpHeaders({
        'Content-type': 'application/json',
        'Authorization':`Bearer ${this.token}`      
      })
    }
    return this.httpService.getService(`/api/Book/GetAllBook`,true, header);
  }
}
