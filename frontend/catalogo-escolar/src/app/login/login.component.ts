import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth-service/auth.service';
import { Title } from '@angular/platform-browser';
import { SnackbarService } from '../_services/snackbar-service/snackbar.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  form: FormGroup;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private titleService: Title, private snackbarService : SnackbarService) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      rememberMe: [false]
    });
    this.titleService.setTitle('Login');
  }

  submit() {
    if (this.form.valid) {
      this.authService.login(this.form.value.email, this.form.value.password).subscribe(
        response => { 
          console.log(response); 
          this.snackbarService.success('Login successful!');
        }, onerror => {
          console.error(onerror);
          this.snackbarService.error('Login failed! Please check your credentials.');
        });
    }
  }
}
