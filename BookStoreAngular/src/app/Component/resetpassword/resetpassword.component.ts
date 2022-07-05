import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.scss']
})
export class ResetpasswordComponent implements OnInit {

  resetPassword!:FormGroup;
  submitted=false;
  token:any;
  confirmPassword:any;
  newPassword:any;
  
  constructor(private formBuilder: FormBuilder,private user:UserService, private activeRoute:ActivatedRoute,private router:Router,private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.resetPassword=this.formBuilder.group({
      password:['',[Validators.required,Validators.minLength(6)]],
      confirmpassword:['',Validators.required]
    });
    this.token = this.activeRoute.snapshot.paramMap.get('token');
  }

  OnSubmit(){
    this.submitted=true;
    if(this.resetPassword.valid)
    {
      console.log(this.resetPassword.value);
         this.newPassword=this.resetPassword.value.password,
         this.confirmPassword=this.resetPassword.value.confirmpassword

      this.user.resetpassword(this.newPassword,this.confirmPassword,this.token).subscribe((res:any)=>
      {
        console.log("Password changed successfully", res);
        this.router.navigateByUrl('/login')

        this._snackBar.open('Congratulations! Password changed successfully', '', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          panelClass: ['snackbar-green']
        })
      })
    }
    else
    {
      console.log("enter data");
    }
  }

  onReset() {
    this.submitted = false;
    this.resetPassword.reset();
}
}
