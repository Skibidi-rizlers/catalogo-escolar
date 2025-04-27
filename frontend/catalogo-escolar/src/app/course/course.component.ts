import { Component } from '@angular/core';
import { FormBuilder, FormsModule } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../_services/auth-service/auth.service';
import { SnackbarService } from '../_services/snackbar-service/snackbar.service';
import { TeacherService } from '../_services/teacher/teacher.service';
import { CommonModule, NgFor } from '@angular/common';
import { Student } from '../_models/student';

@Component({
  selector: 'app-course',
  imports: [FormsModule, CommonModule],
  templateUrl: './course.component.html',
  styleUrl: './course.component.scss'
})
export class CourseComponent {
  view = 'students';
  course: any | undefined;
  availableStudents: Student[] = [];
  selectedStudent: Student | null = null;

  constructor(private route: ActivatedRoute, private title: Title, private router: Router, private snackbarService: SnackbarService,
    private teacherService: TeacherService) {
    this.title.setTitle('Course');

    teacherService.getStudents().subscribe((data: Student[]) => {
      this.availableStudents = data;
    });

  }
  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const courseId = params.get('courseName');
      if (courseId) {
        this.teacherService.getCourseDetails(courseId).subscribe({
          next: (courseDetails) => {
            console.log(courseDetails);
            this.course = courseDetails;
            this.title.setTitle(courseDetails.name);
          },
          error: (error) => {
            this.snackbarService.error('Failed to load course details.');
          }
        });
      } else {
        this.snackbarService.error('Invalid course ID.');
      }
    });
  }

  addStudentToCourse() {
    if (this.selectedStudent) {
      if (this.course.students.some((student: { name: string | undefined; }) => student.name === this.selectedStudent?.name)) {
        alert("Student already exists in the course.");
        return;
      }

      this.teacherService.addStudentToCourse(this.selectedStudent.name, this.course.name);
      this.course.students.push(this.selectedStudent);
    } else {
      alert("Please select both a student and a course.");
    }
  }

  removeStudent(index: number) {
    console.log(this.course);
    console.log('Removing student:', this.course.students[index].name, 'from course:', this.course.name);
    this.teacherService.deleteStudentFromCourse(this.course.students[index].name, this.course.name);
    this.course.students.splice(index, 1);
  }
}
