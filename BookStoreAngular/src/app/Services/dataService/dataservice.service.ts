import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataserviceService {

  private info = new Subject<any>();
  public store = this.info.asObservable();


  constructor() { }

  updateData(text:any){
    this.info.next(text);
  }

  sendData(text:any){
    this.info.next(text);
  }
}
