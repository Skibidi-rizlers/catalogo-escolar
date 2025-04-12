import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-teacher-dashboard',
  imports: [],
  templateUrl: './teacher-dashboard.component.html',
  styleUrl: './teacher-dashboard.component.scss'
})
export class TeacherDashboardComponent {
  constructor(private titleService : Title){
    this.titleService.setTitle('Teacher Dashboard');
  }

}
