import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { StorageService } from '../_services/storage-service/storage.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private storageService: StorageService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.storageService.getUser().pipe(
            map(user => {
                if (user) {
                    return true;
                }
                this.router.navigate(['/login']);
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