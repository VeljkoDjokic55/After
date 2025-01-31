import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtHelperService } from "@auth0/angular-jwt";
import { Router } from '@angular/router';
import { AppConfig } from '../config/config';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private pathApi = this.config.setting['pathApi'] + "auth/";
  private jwtService = new JwtHelperService();

  constructor(private http: HttpClient, 
    private config: AppConfig, 
    private router: Router) {  }

  login(email: string, password: string): Observable<any> {
    return this.http.post(this.pathApi + "login", { Email: email, Password: password });
  } 

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    window.sessionStorage.clear();
    this.router.navigate(['']);
  }

  updateUserInLocalStorageNoApi(user: any) {
    localStorage.setItem('currentUser', JSON.stringify(user));
  }

  isLoggedIn() {
    let token = localStorage.getItem('token');
    if (!token)
      return false;

    return !this.jwtService.isTokenExpired(token);
  }

  public getCurrentUser() {
    let token = localStorage.getItem('token');
    if (!token)
      return null;

    return this.jwtService.decodeToken(token);
  }
}
