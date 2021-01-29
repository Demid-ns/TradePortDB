import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from '../interfaces/user';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  get token(): string {
    return '';
  }

  constructor(private http: HttpClient) {
  }

  login(user: User): Observable<any> {
    return this.http.post('', user);
  }

  logout(): void {

  }

  isAuthenticated(): boolean {
    return !!this.token;
  }

  private setToken() {

  }

}
