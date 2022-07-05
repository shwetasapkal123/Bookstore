import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  submitted = false;
  category:any;


  constructor(private formBuilder: FormBuilder,private user:UserService) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      phone: ['', Validators.required],
      service:['advance']
    }); 
  }

  admin(){
    this.category = true;
  }

  userReg(){
    this.category = false;
  }

  onSubmit(){
    this.submitted=true;
    console.log("registration Suceessful", this.registerForm.value);
    if(this.registerForm.valid){
      console.log("valid",this.registerForm.value);
      let data= {
          fullName:this.registerForm.value.fullName,
          email:this.registerForm.value.email,
          password:this.registerForm.value.password,
          phone:this.registerForm.value.phone,
      }
      if(this.category == true){
        this.user.adminRegister(data).subscribe((response:any)=>{
          console.log(response); 
        },(error:any)=>{
          console.log(error);    
        })
      }
      else if(this.category == false){
        this.user.register(data).subscribe((response:any)=>{
          console.log("done",response);
        }, (error:any)=>{
          console.log(error);    
        })
       }
      }
     
    else{
      console.log("Enter valid data");
    }

  }

}
