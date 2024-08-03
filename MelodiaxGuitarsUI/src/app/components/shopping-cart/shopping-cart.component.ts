import { Component, OnInit } from '@angular/core';
import { ShoppingCart } from '../../models/ShoppingCart';
import { CartService } from '../../services/cart/cart.service';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.scss'
})
export class ShoppingCartComponent implements OnInit{
  shoppingCart:ShoppingCart | null = null;

  constructor(
    private cartService:CartService, 
    private authService:AuthService
  ){}

  ngOnInit(): void {
      const userId = this.authService.getUserId();
      if(userId){
        this.cartService.getShoppingCartByUserId(userId).subscribe(
          (shoppingCart) => {
            this.shoppingCart = shoppingCart;
          },
          (error) => {
            console.error("Error fetching shopping cart ", error);
          }
        )
      }
  }
}
