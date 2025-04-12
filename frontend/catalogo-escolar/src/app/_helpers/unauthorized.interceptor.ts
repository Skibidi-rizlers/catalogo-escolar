import { HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SnackbarService } from '../_services/snackbar-service/snackbar.service';
import { StorageService } from '../_services/storage-service/storage.service';

export const unauthorizedInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn): Observable<any> => {
  const storageService = inject(StorageService);
  const snackService = inject(SnackbarService);

  return next(req).pipe(
    catchError(err => {

      if (req.url.endsWith("login"))
        return throwError(() => err);

      //JWT token expired
      if ([401, 403].includes(err.status)) {
        snackService.error("Your session expired. Please login again.");
        storageService.clean();
      }

      //Server is down
      if (err.status === 0) {
        snackService.error("Unable to connect to the server. Please try again later.");
      }

      return throwError(() => err);
    })
  );
};