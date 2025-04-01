import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  imports: [FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss'
})
export class ChangePasswordComponent {
  credentials: FormGroup;

  constructor(private fb: FormBuilder) {
    this.credentials = this.fb.group({
      old_password: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      password_confirmation: ['', Validators.required]
    }, { validators: this.passwordsMatch });
  }

  get oldPassword() { return this.credentials.get('old_password'); }
  get newPassword() { return this.credentials.get('password'); }
  get confirmPassword() { return this.credentials.get('password_confirmation'); }

  passwordsMatch(group: FormGroup) {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('password_confirmation')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {
  }

  onReset() {
    this.credentials.reset();
  }
}
