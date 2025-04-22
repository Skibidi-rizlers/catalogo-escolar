import { Routes } from '@angular/router';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { LoginComponent } from './login/login.component';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { RoleGuard } from './_helpers/role-guard';
import { AuthGuard } from './_helpers/auth-guard';
import { TeacherDashboardComponent } from './teacher-dashboard/teacher-dashboard.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

export const routes: Routes = [
  { path: 'change-password', component: ChangePasswordComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'reset-password/:id', component: ResetPasswordComponent },
  { path: 'student-dashboard', component: StudentDashboardComponent, canActivate: [RoleGuard], data: { role: 'student' } },
  { path: 'teacher-dashboard', component: TeacherDashboardComponent, canActivate: [RoleGuard], data: { role: 'teacher' } },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];