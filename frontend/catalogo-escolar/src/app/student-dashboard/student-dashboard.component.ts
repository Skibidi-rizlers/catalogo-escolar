import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-student-dashboard',
  imports: [],
  templateUrl: './student-dashboard.component.html',
  styleUrl: './student-dashboard.component.scss'
})
export class StudentDashboardComponent {
  constructor(private titleService : Title) { 
    this.titleService.setTitle('Student Dashboard');
  }
}
