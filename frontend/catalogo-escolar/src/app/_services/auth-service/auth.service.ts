import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private AUTH_API_URL = 'http://localhost:5027/auth';

  readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<string> {
    return this.http.post<string>(
      `${this.AUTH_API_URL}/login`,
      { email, password },
      { headers: this.httpOptions.headers, responseType: 'text' as 'json' }
    );
  }

  changePassword(oldPassword: string, newPassword: string): Observable<boolean> {
    return this.http.post<string>(
      `${this.AUTH_API_URL}/change-password`,
      { oldPassword, newPassword },
      { headers: this.httpOptions.headers, responseType: 'text' as 'json' }
    ).pipe(
      map((response: string) => response === 'true')
    );
  }

  requestResetPassword(email: string): Observable<boolean> {
    return this.http.post<string>(
      `${this.AUTH_API_URL}/request-reset-password`,
      { email },
      { headers: this.httpOptions.headers, responseType: 'text' as 'json' }
    ).pipe(
      map((response: string) => response === 'true')
    );
  }

  resetPassword(encodedId: string, password: string): Observable<boolean> {
    return this.http.post<string>(
      `${this.AUTH_API_URL}/reset-password`,
      { encodedId, password },
      { headers: this.httpOptions.headers, responseType: 'text' as 'json' }
    ).pipe(
      map((response: string) => response === 'true')
    );
  }
}
