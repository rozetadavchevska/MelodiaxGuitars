import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_API_URL } from '../../apiUrl';
import { Category } from '../../models/Category';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseApiUrl = BASE_API_URL;

  constructor(private http: HttpClient) { }

  getCategories():Observable<Category[]>{
    return this.http.get<Category[]>(this.baseApiUrl + 'api/Categories');
  }

  addCategory(category:Category): Observable<Category>{
    const body = {
      id: '',
      name: category.name,
      description: category.description
    };
    return this.http.post<Category>(this.baseApiUrl + 'api/Categories', body);
  }

  updateCategory(id:string, category:Category): Observable<Category>{
    const body = {
      id: id,
      name: category.name,
      description: category.description,
    }
    return this.http.put<Category>(this.baseApiUrl + `api/Categories/${id}`, body);
  }

  deleteCategory(id:string):Observable<any>{
    return this.http.delete<any>(this.baseApiUrl + `api/Categories/${id}`)
  }
}
