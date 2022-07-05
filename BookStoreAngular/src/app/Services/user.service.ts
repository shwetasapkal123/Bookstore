import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpServiceService } from './http-service.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService:HttpServiceService) { }

  register(reqdata:any){
    console.log(reqdata);
    let header ={
      headers: new HttpHeaders({
        'Content-type': 'application/json'      
      })
    }
    return this.httpService.postService('/api/User/Register',reqdata,false,header)
  }

  login(reqdata:any){
    console.log(reqdata);
    let header ={
      headers: new HttpHeaders({
        'Content-type': 'application/json'      
      })
    }
    return this.httpService.postService('/api/User/login',reqdata,false,header)
  }
  forgotPassword(data:any){
  
    let header ={
      headers: new HttpHeaders({
        'Content-type': 'application/json'      
      })
    }
    return this.httpService.postService(`/api/User/ForgotPassword?email=`+data,{},false,header)
  }
  resetpassword(newPass:any,confirm:any,token:any)
  {
    let header ={
      headers: new HttpHeaders({
        'Content-type': 'application/json',
        'Authorization':`Bearer ${token}`      
      })
    }
    //return this.httpService.patchService('/api/User/ResetPassword?newPassword='+data,{},true,header)
    return this.httpService.putService(`/api/User/ResetPassword?newPassword=${newPass}&confirmPassword=${confirm}`, {}, true, header);
  }
  // encode(data: any) {
  //   const formBody = [];
  //   for (const property in data) {
  //     const encodedKey = encodeURIComponent(property);
  //     const encodedValue = encodeURIComponent(data[property]);
  //     formBody.push(encodedKey + '=' + encodedValue);
  //   }
  //   return formBody.join('&');
  // }

  adminRegister(data:any){
    let header ={
      headers: new HttpHeaders({
        'Content-type': 'application/json'      
      })
    }
    return this.httpService.postService('/api/Admin/AdminLogin', data, false, header)
  }

  adminLogin(data:any){
    let header ={
      headers: new HttpHeaders({
        'Content-type': 'application/json'      
      })
    }
    return this.httpService.postService('/api/Admin/AdminLogin', data, false, header)
  }
}
