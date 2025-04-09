import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  constructor(private _snackBar: MatSnackBar) { }

  error(message: string, duration: number = 3000) {
    return this._snackBar.open(message, undefined, {
      duration,
      panelClass: ['snackbar-error']
    });
  }

  success(message: string, duration: number = 3000) {
    return this._snackBar.open(message, undefined, {
      duration,
      panelClass: ['snackbar-success']
    });
  }

  info(message: string, duration: number = 3000) {
    return this._snackBar.open(message, undefined, {
      duration,
      panelClass: ['snackbar-info']
    });
  }
}