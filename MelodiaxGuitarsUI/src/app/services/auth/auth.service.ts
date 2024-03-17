import { isPlatformBrowser } from '@angular/common';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { jwtDecode } from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'RyLCdWSwzLP0lNDFOKQYzQzhPkZWmtEM';

  constructor(@Inject(PLATFORM_ID) private platformId: Object) { }

  private getLocalStorage(): Storage | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage;
    }
    return null;
  }

  isLoggedIn(): boolean {
    const localStorage = this.getLocalStorage();
    return !!localStorage && !!localStorage.getItem(this.TOKEN_KEY);
  }

  getToken(): string | null {
    const localStorage = this.getLocalStorage();
    return !!localStorage ? localStorage.getItem(this.TOKEN_KEY) : null;
  }

  storeToken(token: string): void {
    const localStorage = this.getLocalStorage();
    if (!!localStorage) {
      localStorage.setItem(this.TOKEN_KEY, token);
    }
  }

  removeToken(): void {
    const localStorage = this.getLocalStorage();
    if (!!localStorage) {
      localStorage.removeItem(this.TOKEN_KEY);
    }
  }

  isAdmin():boolean {
    const token = this.getToken();
    if (token) {
      const decodedToken: any = jwtDecode(token);
      const userRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      return userRole === 'Admin';
    }
    return false; // If there's no token or role is not admin, return false
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token && !!this.isTokenExpired(token);
  }

  private isTokenExpired(token: string): boolean | null {
    const expirationDate = this.getTokenExpirationDate(token);
    return expirationDate && expirationDate < new Date();
  }

  private getTokenExpirationDate(token: string): Date | null {
    const decodedToken = this.decodeToken(token);
    if (decodedToken && decodedToken.exp) {
      return new Date(decodedToken.exp * 1000);
    }
    return null;
  }

  private decodeToken(token: string): any {
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch (error) {
      return null;
    }
  }
}
