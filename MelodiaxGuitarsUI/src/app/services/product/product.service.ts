import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../../models/Product';
import { BASE_API_URL } from '../../apiUrl';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseApiUrl: string = BASE_API_URL;

  constructor(private http:HttpClient) { }

  getProducts():Observable<Product[]>{
    return this.http.get<Product[]>(this.baseApiUrl + 'api/Products');
  }

  addProduct(product:Product):Observable<Product>{
    const body = {
      id: '',
      name: product.name,
      description: product.description,
      brandId: product.brandId, 
      model: product.model,
      type: product.type,
      hand: product.hand,
      bodyShape: product.bodyShape,
      color: product.color,
      top: product.top,
      sidesAndBack: product.sidesAndBack,
      neck: product.neck,
      nut: product.nut,
      fingerboard: product.fingerboard,
      strings: product.strings,
      tuners: product.tuners,
      bridge: product.bridge,
      controls: product.controls,
      pickups: product.pickups,
      pickupSwitch: product.pickupSwitch,
      cutaway: product.cutaway, 
      pickguard: product.pickguard,
      case: product.case,
      scaleLength: product.scaleLength,
      width: product.width,
      depth: product.depth,
      weight: product.weight,
      categoryId: product.categoryId,
      imageUrl: product.imageUrl,
      price: product.price
    }
    return this.http.post<Product>(this.baseApiUrl + 'api/Products', body);
  }

  updateProduct(id:string, product:Product):Observable<Product>{
    const body = {
      id: id,
      name: product.name,
      description: product.description,
      brandId: product.brandId, 
      model: product.model,
      type: product.type,
      hand: product.hand,
      bodyShape: product.bodyShape,
      color: product.color,
      top: product.top,
      sidesAndBack: product.sidesAndBack,
      neck: product.neck,
      nut: product.nut,
      fingerboard: product.fingerboard,
      strings: product.strings,
      tuners: product.tuners,
      bridge: product.bridge,
      controls: product.controls,
      pickups: product.pickups,
      pickupSwitch: product.pickupSwitch,
      cutaway: product.cutaway, 
      pickguard: product.pickguard,
      case: product.case,
      scaleLength: product.scaleLength,
      width: product.width,
      depth: product.depth,
      weight: product.weight,
      categoryId: product.categoryId,
      imageUrl: product.imageUrl,
      price: product.price
    };
    return this.http.put<Product>(this.baseApiUrl + `api/Products/${id}`, body);
  }

  deleteProduct(id:string):Observable<any>{
    return this.http.delete<any>(this.baseApiUrl + `api/Products/${id}`);
  }
}
