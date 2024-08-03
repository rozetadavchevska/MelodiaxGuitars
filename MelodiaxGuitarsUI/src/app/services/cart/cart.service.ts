import { Injectable } from '@angular/core';
import { BASE_API_URL } from '../../apiUrl';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShoppingCart } from '../../models/ShoppingCart';
import { CartItem } from '../../models/CartItem';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private baseApiUrl:string = BASE_API_URL;
  
  constructor(private http:HttpClient){}

  getShoppingCartByUserId(userId: string):Observable<ShoppingCart>{
    return this.http.get<ShoppingCart>(`${this.baseApiUrl}api/ShoppingCarts/user/${userId}`);
  }

  addCartItem(cartItem: CartItem): Observable<CartItem>{
    return this.http.post<CartItem>(`${this.baseApiUrl}api/CartItems`, cartItem);
  }
}
