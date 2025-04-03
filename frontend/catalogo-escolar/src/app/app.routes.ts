import { Routes } from '@angular/router';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
  { path: 'change-password', component: ChangePasswordComponent },
  { path: 'login', component: LoginComponent }
];
