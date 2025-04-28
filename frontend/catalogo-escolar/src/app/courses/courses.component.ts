import { Component } from '@angular/core';
import { Course } from '../_models/course';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TeacherService } from '../_services/teacher/teacher.service';
import { Student } from '../_models/student';
import { Router } from '@angular/router';

@Component({
  selector: 'app-courses',
  imports: [FormsModule, CommonModule],
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.scss'
})
export class CourseListComponent {
  courses: Course[] = [];

  editingIndex: number | null = null;
  updatedName: string = '';
  newCourseName: string = '';
  availableStudents: Student[] = [];
  selectedStudent: Student | null = null;
  selectedCourse: Course | null = null;

  constructor(private teacherService: TeacherService, private router: Router) {
    teacherService.getCourses().subscribe((data: Course[]) => {
      this.courses = data;
    });
    teacherService.getStudents().subscribe((data: Student[]) => {
      this.availableStudents = data;
    }
    );
  }

  startEdit(index: number) {
    this.editingIndex = index;
    this.updatedName = this.courses[index].name;
  }

  saveEdit(index: number) {
    if (this.updatedName.trim()) {
      this.teacherService.modifyCourse(this.courses[index], this.updatedName);
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

  addCourse() {
    if (this.newCourseName.trim()) {
      const newCourse: Course = {
        id: 0,
        name: this.newCourseName,
        students: [],
      };
      this.teacherService.addCourse(this.newCourseName);
      this.courses.push(newCourse);
      this.newCourseName = '';
    }
  }
  
  removeStudent(studentName: string, courseName: string) {
    this.teacherService.deleteStudentFromCourse(studentName, courseName);
    const course = this.courses.find(course => course.name === courseName);
    if (course) {
      course.students = course.students.filter(student => student.name !== studentName);
    }
  }

  addStudentToCourse() {
    if (this.selectedStudent && this.selectedCourse) {
      if (this.selectedCourse.students.some(student => student.name === this.selectedStudent?.name)) {
        alert("Student already exists in the course.");
        return;
      }

      this.teacherService.addStudentToCourse(this.selectedStudent.name, this.selectedCourse.name);
      this.selectedCourse.students.push(this.selectedStudent);
    } else {
      alert("Please select both a student and a course.");
    }
  }

  goToCourse(course: any) {
    this.router.navigate(['/course', course.name]);
  }
}
