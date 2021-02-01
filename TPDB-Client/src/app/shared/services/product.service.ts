import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Product} from '../models/product';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) {
  }

  public getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${environment.resApi}api/products`);
  }

  public getProduct(id: number): Observable<Product>{
    return this.http.get<Product>(`${environment.resApi}api/products/${id}`);
  }
}
