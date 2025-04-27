import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../_services/auth-service/auth.service';
import { SnackbarService } from '../_services/snackbar-service/snackbar.service';

@Component({
  selector: 'app-reset-password',
  imports: [FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss'
})
export class ResetPasswordComponent implements OnInit {
  hasToken: boolean = false;
  token: string | null = null;
  requestForm!: FormGroup;
  resetForm!: FormGroup;

  constructor(private route: ActivatedRoute, private title: Title,
    private fb: FormBuilder, private authService: AuthService, private router: Router, private snackbarService: SnackbarService) {
    this.title.setTitle('Reset Password');
  }

  get email() { return this.requestForm.get('email'); }
  get newPassword() { return this.resetForm.get('newPassword'); }
  get confirmPassword() { return this.resetForm.get('confirmPassword'); }

  passwordsMatch(group: FormGroup) {
    const password = group.get('newPassword')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.token = params.get('id');
      this.hasToken = !!this.token;
      this.hasToken ? this.title.setTitle('Reset Password') : this.title.setTitle('Request Reset Password');

      this.requestForm = this.fb.group({
        email: ['', [Validators.required, Validators.email]]
      });

      this.resetForm = this.fb.group({
        newPassword: ['', [Validators.required, Validators.minLength(5)]],
        confirmPassword: ['', Validators.required],
      }, { validators: this.passwordsMatch });
    });
  }

  onRequestSubmit(): void {
    if (this.requestForm.invalid) return;

    const email = this.requestForm.value.email;

    this.authService.requestResetPassword(email).subscribe({
      next: success => {
        if (success === true) {
          this.snackbarService.success('Reset password email was sent successfully!');
          this.router.navigate(['/login']);
        } else {
          this.snackbarService.error('An error has occurred. Please try again.');
        }
      },
      error: err => {
        console.error(err);
        this.snackbarService.error('An error has occurred. Please try again.');
      }
    });
  }

  onResetSubmit(): void {
    if (this.resetForm.invalid) return;

    const { newPassword, confirmPassword } = this.resetForm.value;

    this.authService.resetPassword(this.token!, newPassword).subscribe({
      next: success => {
        if (success === true) {
          this.snackbarService.success('Password was reset!');
          this.router.navigate(['/login']);
        } else {
          this.snackbarService.error('Failure at resetting password.');
        }
      },
      error: err => {
        console.error(err);
        this.snackbarService.error('An error has occurred. Please try again.');
      }
    });
  }
}