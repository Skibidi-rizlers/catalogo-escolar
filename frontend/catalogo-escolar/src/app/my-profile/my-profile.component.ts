import { Component } from '@angular/core';
import { AuthState } from '../auth.state';
import { User } from '../_models/user';
import { NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-my-profile',
  imports: [NgIf, RouterLink],
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.scss'
})
export class MyProfileComponent {
  user?: User | undefined;

  constructor(private authState: AuthState) {
    this.authState.user.subscribe(user => {
      this.user = user;
    });
  }
}
