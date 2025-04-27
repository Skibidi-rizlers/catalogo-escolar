import { Component } from '@angular/core';
import { Course } from '../_models/course';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TeacherService } from '../_services/teacher/teacher.service';

@Component({
  selector: 'app-courses',
  imports: [FormsModule,CommonModule],
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.scss'
})
export class CourseListComponent {
  courses: Course[] = [];

  editingIndex: number | null = null;
  updatedName: string = '';

  constructor(private teacherService:TeacherService){
    teacherService.getCourses().subscribe((data: Course[]) => {
      this.courses = data;
    });
  }
  startEdit(index: number) {
    this.editingIndex = index;
    this.updatedName = this.courses[index].name;
  }

  saveEdit(index: number) {
    if (this.updatedName.trim()) {
      this.teacherService.modifyCourse(this.courses[index],this.updatedName);
      this.courses[index].name = this.updatedName;
      this.editingIndex = null;
      this.updatedName = '';
    }
  }

  cancelEdit() {
    this.editingIndex = null;
    this.updatedName = '';
  }

  deleteCourse(index: number) {
    this.teacherService.deleteCourse(this.courses[index]);
    this.courses.splice(index, 1);
    this.cancelEdit();
  }
}
