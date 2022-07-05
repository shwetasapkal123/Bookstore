import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  forgotpasswordform!:FormGroup;
  submitted=false;
  emailId:any;

  constructor(private formBuilder: FormBuilder,private user:UserService,private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.forgotpasswordform=this.formBuilder.group({
      email:['',[Validators.required,Validators.email]]
    });
  }

  OnSubmit(){
    this.submitted=true;
    if(this.forgotpasswordform.value)
    {
      console.log(this.forgotpasswordform.value);
    
        this.emailId=this.forgotpasswordform.value.email,
    this.user.forgotPassword(this.emailId).subscribe((response:any)=>{
      console.log("Reset link sent successfully",response);
      this._snackBar.open('Reset link sent successfully', '', {
        duration: 3000,
        verticalPosition: 'bottom',
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
    this.forgotpasswordform.reset();
}

}
