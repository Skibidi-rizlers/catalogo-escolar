import { HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { StorageService } from '../_services/storage-service/storage.service';

export const jwtInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {
   const storageService = inject(StorageService);
   const token = storageService.getToken();

   if (token) {
     req = req.clone({
       setHeaders: {
         Authorization: `Bearer ${token}`,
       },
     });
   }

  return next(req);
};