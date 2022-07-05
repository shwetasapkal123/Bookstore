import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookComponent } from './Component/book/book.component';
import { CartComponent } from './Component/cart/cart.component';
import { DashboardComponent } from './Component/dashboard/dashboard.component';
import { ForgotPasswordComponent } from './Component/forgot-password/forgot-password.component';
import { LoginComponent } from './Component/login/login.component';
import { ResetpasswordComponent } from './Component/resetpassword/resetpassword.component';
import { RegisterComponent } from './Component/signUp/register.component';

const routes: Routes = [
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'forgotpassword',component:ForgotPasswordComponent},
  {path:'resetpassword/:token',component:ResetpasswordComponent},
  {path:'dashboard',component:DashboardComponent,
  children:[
    { path: '', redirectTo: '/dashboard/getallbooks', pathMatch: 'full' },
    {path:'getallbooks',component:BookComponent},
    {path:'cart',component:CartComponent},
]},
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
