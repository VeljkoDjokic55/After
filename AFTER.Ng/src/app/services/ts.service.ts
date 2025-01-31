import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfig } from '../config/config';
import { TransmissionStation } from '../models/transmission.station.model';

@Injectable({
  providedIn: 'root'
})
export class TsService {

  private pathApi = this.config.setting['pathApi'] + "transmissionStation/";

  constructor(private http: HttpClient, private config: AppConfig) { }

  getAll(dataIn: any): Observable<any> {
    return this.http.post(`${this.pathApi}getAll`, dataIn);
  }

  save(ts: TransmissionStation): Observable<any> {
    return this.http.post(`${this.pathApi}save`, ts);
  }

  delete(ts: TransmissionStation): Observable<any> {
    return this.http.post(`${this.pathApi}delete`, ts);
  }
}
