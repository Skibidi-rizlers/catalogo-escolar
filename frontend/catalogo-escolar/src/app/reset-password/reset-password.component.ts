import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reset-password',
  imports: [NgIf],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss'
})
export class ResetPasswordComponent implements OnInit {
  hasToken: boolean = false;
  token: string | null = null;

  constructor(private route: ActivatedRoute, private title: Title) {
    this.title.setTitle('Reset Password');
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.token = params.get('id');
      this.hasToken = !!this.token;
      console.log(this.token);
    });
  }
}