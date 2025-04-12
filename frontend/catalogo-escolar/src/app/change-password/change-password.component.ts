import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { AuthService } from '../_services/auth-service/auth.service';
import { SnackbarService } from '../_services/snackbar-service/snackbar.service';
import { StorageService } from '../_services/storage-service/storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  imports: [FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss'
})
export class ChangePasswordComponent {
  form: FormGroup;

  constructor(private fb: FormBuilder, private titleService: Title, private authService: AuthService, private snackbarService: SnackbarService,
    private storageService : StorageService, private router : Router
  ) {
    this.form = this.fb.group({
      old_password: ['', Validators.required],
      password: ['', [Validators.required]],
      password_confirmation: ['', Validators.required]
    }, { validators: this.passwordsMatch });
    this.titleService.setTitle('Change Password');
  }

  get oldPassword() { return this.form.get('old_password'); }
  get newPassword() { return this.form.get('password'); }
  get confirmPassword() { return this.form.get('password_confirmation'); }

  passwordsMatch(group: FormGroup) {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('password_confirmation')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.form.valid) {
      this.authService.changePassword(this.form.value.old_password, this.form.value.password).subscribe({
        next: (result) => {
          console.log(result);
          if (result === true) {
            this.snackbarService.success('Operation successful! Please login again');
            this.storageService.clean();
            this.router.navigate(["/login"]);
          } else {
            this.snackbarService.error("Operation failure");
          }
        },
        error: (error) => {
          this.snackbarService.error(error.message);
        }
      });
    }
  }

  onReset() {
    this.form.reset();
  }
}
