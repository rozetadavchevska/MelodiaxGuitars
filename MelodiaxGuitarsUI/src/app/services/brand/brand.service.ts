import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_API_URL } from '../../apiUrl';
import { Observable } from 'rxjs';
import { Brand } from '../../models/Brand';

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  private baseApiUrl:string = BASE_API_URL;

  constructor(private http:HttpClient) { }

  getBrands():Observable<Brand[]>{
    return this.http.get<Brand[]>(this.baseApiUrl + 'api/Brands');
  }

  updateBrand(id:string,brand:Brand):Observable<Brand>{
    return this.http.put<Brand>(this.baseApiUrl + 'api/Brands', {id ,brand});
  }

  deleteBrand(id:string):Observable<any>{
    return this.http.delete<any>(this.baseApiUrl + `api/Brands/${id}`);
  }
}
