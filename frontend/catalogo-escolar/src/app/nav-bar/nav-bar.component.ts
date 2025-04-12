import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { User } from '../_models/user';
import { StorageService } from '../_services/storage-service/storage.service';
import { AuthState } from '../auth.state';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink, NgIf, CommonModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent {
  user?: User | undefined;
  constructor(private authState: AuthState) {
    this.authState.user.subscribe(user => {
      this.user = user;
    });
  }

  logout() {
    this.authState.logout();
  }
}
