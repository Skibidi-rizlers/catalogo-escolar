import { Injectable } from '@angular/core';
import { JwtParser } from '../../_helpers/jwt-decoder';
import { User } from '../../_models/user';
import { Observable, of } from 'rxjs';

const AUTH_TOKEN = 'auth-token';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor() {}

  clean(): void {
    window.sessionStorage.clear();
  }

  public saveToken(token: string): void {
    window.sessionStorage.removeItem(AUTH_TOKEN);
    window.sessionStorage.setItem(AUTH_TOKEN, token);
  }

  public getUser(): Observable<User | undefined> {
    const token = window.sessionStorage.getItem(AUTH_TOKEN);
    if (token) {
      let decoder = new JwtParser(token);
      return of(decoder.getUser());
    }
  
    return of(undefined);
  }

  public getToken() : string | null{
    const token = window.sessionStorage.getItem(AUTH_TOKEN);
    return token;
  }

  public isLoggedIn(): boolean {
    const token = window.sessionStorage.getItem(AUTH_TOKEN);
    if (token) {
      return true;
    }

    return false;
  }
}