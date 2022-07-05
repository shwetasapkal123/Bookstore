import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CartServiceService } from 'src/app/Services/CartServices/cart-service.service';
import { OrderServiceService } from 'src/app/Services/OrderServices/order-service.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  cartItemsCount:any;
  cartList:any;

  constructor(public cartservice:CartServiceService,private formbuilder: FormBuilder, private route:Router,public orderService:OrderServiceService) { }

  ngOnInit(): void {
    this.getAllCart()
  }

  addCart(userId:any){
    this.cartservice.addToCart(userId).subscribe((res:any)=>
    {
      console.log(res)
    }
    )
  }
  getAllCart()
  {
    this.cartservice.getAllCartItems().subscribe((res:any)=>
    {
      console.log("All Carts are :",res.response)
      this.cartItemsCount=res.response.length
      this.cartList=res.response
    })
  }

  // minusCartQty(cartId:any,quantity:any){
  //   if(this.book_quantity>1){
  //    this.cartservice.UpdateCart(cartId,(quantity-1)).subscribe((res:any)=>
  //    {
  //     console.log("cart quantity decreases successfully",res);
  //     this.getAllCart()
  //    })
  //   } 
  //   }
  
   plusCartQty(cartId:any,quantity:any){
   this.cartservice.UpdateCart(cartId,(quantity+1)).subscribe((res:any)=>
   {
    console.log("Cart quantity added successfully",res);
    this.getAllCart()
   })
   }
  
  removeFromCart(cartId:any){
     this.cartservice.removeCartItem(cartId).subscribe((res:any)=>
     {
       console.log("Cart removed successfully",res);
     })
  }
  
}
