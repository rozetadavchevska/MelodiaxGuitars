import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'RyLCdWSwzLP0lNDFOKQYzQzhPkZWmtEM';

  constructor() { }

  isLoggedIn():boolean{
    return !!localStorage.getItem(this.TOKEN_KEY);
  }

  getToken():string|null{
    return localStorage.getItem(this.TOKEN_KEY);
  }

  storeToken(token:string):void{
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  removeToken():void{
    localStorage.removeItem(this.TOKEN_KEY);
  }

  isAuthenticated():boolean{
    const token = this.getToken();
    return !!token && !!this.isTokenExpired(token);
  }

  private isTokenExpired(token:string):boolean | null{
    const expirationDate = this.getTokenExpirationDate(token);
    return expirationDate && expirationDate < new Date();
  }

  private getTokenExpirationDate(token:string):Date|null{
    const decodedToken = this.decodeToken(token);
    if(decodedToken && decodedToken.exp){
      return new Date(decodedToken.exp * 1000);
    }
    return null;
  }

  private decodeToken(token:string):any{
    try{
      return JSON.parse(atob(token.split('.')[1]));
    }catch(error){
      return null;
    }
  }
}
