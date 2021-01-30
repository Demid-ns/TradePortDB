import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {APIResponse, User} from '../interfaces/interfaces';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';
import {AUTH_API_URL} from '../app-injection-tokens';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Router} from '@angular/router';

export const LOCAL_TOKEN_KEY = 'tpdb_access_token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient,
              @Inject(AUTH_API_URL) private apiUrl: string,
              private jwtHelper: JwtHelperService,
              private router: Router) {
  }

  login(user: User): Observable<APIResponse> {
    user.returnJWTToken = true;
    return this.http.post(`${this.apiUrl}api/auth/login`, user)
      .pipe(
        tap(this.setToken)
      );
  }

  logout(): void {

  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem(LOCAL_TOKEN_KEY);
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  private setToken(response: APIResponse): void {
    console.log(response.access_token)
    localStorage.setItem(LOCAL_TOKEN_KEY, response.access_token);
  }

}
