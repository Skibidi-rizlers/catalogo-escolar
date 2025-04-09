import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth-service/auth.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  form: FormGroup;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private titleService: Title) {
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
        response => { console.log(response); });
      console.log('Form Submitted', this.form.value);
    }
  }
}
