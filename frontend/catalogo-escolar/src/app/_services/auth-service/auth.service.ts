import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService{
  private AUTH_API_URL = 'http://localhost:5027/auth';

  readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  constructor(private http: HttpClient) { }

  login(email: string, password: string) {
    return this.http.post<string>(
      `${this.AUTH_API_URL}/login`,
      { email, password },
      { headers: this.httpOptions.headers, responseType: 'text' as 'json' }
    );
  }
}
