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

  addBrand(brandDto:Brand):Observable<Brand>{
    const body = {
      id: '',
      name: brandDto.name,
      description: brandDto.description
    };
    return this.http.post<Brand>(this.baseApiUrl + 'api/Brands', body);
  }

  updateBrand(id: string, brandDto: Brand): Observable<Brand> {
    const body = {
      id: id,
      name: brandDto.name,
      description: brandDto.description
    };
    return this.http.put<Brand>(`${this.baseApiUrl}api/Brands/${id}`, body);
  }
  deleteBrand(id:string):Observable<any>{
    return this.http.delete<any>(this.baseApiUrl + `api/Brands/${id}`);
  }
}
