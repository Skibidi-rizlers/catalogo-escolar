import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth-service/auth.service';
import { Title } from '@angular/platform-browser';
import { SnackbarService } from '../_services/snackbar-service/snackbar.service';
import { Router } from '@angular/router';
import { StorageService } from '../_services/storage-service/storage.service';
import { map } from 'rxjs';
import { AuthState } from '../auth.state';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private storageService: StorageService,
    private titleService: Title, private snackbarService: SnackbarService, private authState: AuthState) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      rememberMe: [false]
    });
    this.titleService.setTitle('Login');
  }
  ngOnInit(): void {
    this.authState.user.pipe(
      map(user => {
        if (user?.role === 'student') {
          this.router.navigate(['/student-dashboard']);
        } else if (user?.role === 'teacher') {
          this.router.navigate(['/teacher-dashboard']);
        }
      })
    ).subscribe();
  }

  submit() {
    if (this.form.valid) {
      const { email, password, rememberMe } = this.form.value;
      this.authService.login(email, password).subscribe({
        next: (JWT) => {
          this.authState.setUser(JWT, rememberMe);
          this.snackbarService.success('Login successful!');
          this.router.navigate(['/student-dashboard']);
        },
        error: (error) => {
          this.snackbarService.error('Login failed! Please check your credentials.');
        }
      });
    }
  }
}
