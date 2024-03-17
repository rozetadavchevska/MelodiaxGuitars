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
}
