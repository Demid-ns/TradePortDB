import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Product} from '../models/product';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommodityService {
  constructor(private http: HttpClient) {
  }
}
