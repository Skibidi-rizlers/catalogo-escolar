import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { CourseListComponent } from "../courses/courses.component";

@Component({
  selector: 'app-teacher-dashboard',
  imports: [CourseListComponent],
  templateUrl: './teacher-dashboard.component.html',
  styleUrl: './teacher-dashboard.component.scss'
})
export class TeacherDashboardComponent {
  constructor(private titleService : Title){
    this.titleService.setTitle('Teacher Dashboard');
  }

}
