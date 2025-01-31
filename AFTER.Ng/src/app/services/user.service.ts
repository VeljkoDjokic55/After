import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConfig } from '../config/config';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private pathApi = this.config.setting['pathApi'] + "user/";


  constructor(private http: HttpClient, private config: AppConfig) {

  }
  
  forgotPasswordRequest(email: any): Observable<any> {
    return this.http.post(`${this.pathApi}forgotPasswordRequest?email=${email}`, null)
  } 

  resetPassword(obj: any): Observable<any> {
    return this.http.post(`${this.pathApi}resetPassword?email=${obj.email}&password=${obj.password}&code=${obj.code}`, null);
  } 

  getAll(obj: any): Observable<any> {
    return this.http.post(`${this.pathApi}getAll`, obj);
  }

  setStatus(obj: any): Observable<any> {
    return this.http.post(`${this.pathApi}setStatus`, obj);
  }

  saveUser(obj: any): Observable<any> {
    return this.http.post(`${this.pathApi}save`, obj);
  }
}
