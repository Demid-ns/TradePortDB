import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {Commodity} from '../models/commodity';

@Injectable({
  providedIn: 'root'
})
export class CommodityService {
  constructor(private http: HttpClient) {
  }

  public getCommodities(): Observable<Commodity[]> {
    return this.http.get<Commodity[]>(`${environment.resApi}api/commodities`);
  }

  public getCommodity(id: number): Observable<Commodity>{
    return this.http.get<Commodity>(`${environment.resApi}api/commodities/${id}`);
  }

  public removeCommodity(id: number): Observable<void>{
    return this.http.delete<void>(`${environment.resApi}api/commodities/${id}`);
  }

  public addCommodity(commodity: Commodity): Observable<Commodity> {
    return this.http.post<Commodity>(`${environment.resApi}api/commodities`, commodity);
  }
}
