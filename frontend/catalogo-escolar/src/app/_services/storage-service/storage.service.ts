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
    const user = window.sessionStorage.getItem(AUTH_TOKEN);
    if (user) {
      let decoder = new JwtParser(user);
      return of(decoder.getUser());
    }
  
    return of(undefined);
  }

  public isLoggedIn(): boolean {
    const user = window.sessionStorage.getItem(AUTH_TOKEN);
    if (user) {
      return true;
    }

    return false;
  }
}