import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { StorageService } from '../_services/storage-service/storage.service';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
    constructor(
        private router: Router,
        private storageService: StorageService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        const expectedRole = route.data['role'];
        return this.storageService.getUser().pipe(
            map(user => {
                if (user) {
                    if (expectedRole === 'student' && user.role === 'student') {
                        return true;
                    }
                    if (expectedRole === 'teacher' && user.role === 'teacher') {
                        return true;
                    }
                    if (user.role === 'teacher') {
                        this.router.navigate(['/teacher-dashboard']);
                    } else {
                        this.router.navigate(['/student-dashboard']);
                    }
                } else {
                    this.router.navigate(['/login']);
                }
                return false;
            }),
            catchError(error => {
                this.storageService.clean();
                this.router.navigate(['/login']);
                return of(false);
            })
        );
    }
}