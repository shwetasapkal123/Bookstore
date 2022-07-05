import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpServiceService {
  BaseUrl=environment.baseUrl;

  constructor(private http: HttpClient) { }
  postService(url:string, reqPayload:any={},token:boolean=false,option:any){
    return this.http.post(this.BaseUrl+url,reqPayload, token && option)

  }

  getService(url:string, token:boolean=false,option:any){
    return this.http.get(this.BaseUrl+url, token && option)

  }
  patchService(url : string='',reqData: any,  tokenRequired: boolean = false, httpOptions: any = {} ){ 
    return this.http.patch(this.BaseUrl + url, reqData, tokenRequired && httpOptions);
   }

  putService(url:string, reqData:any={}, token:boolean=false, httpoptions:any={}){
    return this.http.put(this.BaseUrl+url, reqData, token && httpoptions)
  }

  deleteService(url:string, reqData:any={}, token:boolean=false, httpoptions:any={}){
    return this.http.delete(this.BaseUrl+url, token && httpoptions)
  }

}
