import { Injectable } from '@angular/core';
import { JwtParser } from '../../_helpers/jwt-decoder';
import { User } from '../../_models/user';
import { Observable, of } from 'rxjs';

const AUTH_TOKEN = 'auth-token';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor() { }

  public clean(): void {
    window.sessionStorage.removeItem(AUTH_TOKEN);
    window.localStorage.removeItem(AUTH_TOKEN);
  }


  public saveToken(token: string, rememberMe: boolean = false): void {
    window.sessionStorage.removeItem(AUTH_TOKEN);
    window.localStorage.removeItem(AUTH_TOKEN);

    if (rememberMe === true) {
      window.localStorage.setItem(AUTH_TOKEN, token);
    } else if (rememberMe === false) {
      window.sessionStorage.setItem(AUTH_TOKEN, token);
    }
  }


  public getUser(): Observable<User | undefined> {
    const token = this.getToken();
    if (token) {
      let decoder = new JwtParser(token);
      return of(decoder.getUser());
    }

    return of(undefined);
  }

  public getToken(): string | null {
    return window.sessionStorage.getItem(AUTH_TOKEN) || window.localStorage.getItem(AUTH_TOKEN);
  }


  public isLoggedIn(): boolean {
    return !!this.getToken();
  }

}