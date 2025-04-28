import { CommonModule, DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { SnackbarService } from '../_services/snackbar-service/snackbar.service';
import { GradeService } from '../_services/grade-service/grade.service';
import { FormsModule } from '@angular/forms';
import { TeacherService } from '../_services/teacher/teacher.service';
import { Student } from '../_models/student';

@Component({
  selector: 'app-assignment',
  imports: [FormsModule, CommonModule, RouterModule],
  providers: [DatePipe],
  templateUrl: './assignment.component.html',
  styleUrl: './assignment.component.scss'
})
export class AssignmentComponent {
  courseName: string | null = null;
  assignmentId: string | null = null;
  availableStudents: Student[] = [];

  studentId: number = 0;
  gradeValue: number = 0;
  givenAt: string = '';

  constructor(private route: ActivatedRoute, private title: Title, private router: Router, private snackbarService: SnackbarService,
    private gradeService: GradeService, private teacherService: TeacherService) {
    this.title.setTitle('Assignment');
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.courseName = params.get('courseName');
      this.assignmentId = params.get('assignmentId');
      if (this.assignmentId && this.courseName) {
        this.gradeService.getGrades(this.assignmentId).subscribe({
          next: (grades) => {
            console.log(grades);
          },
          error: (error) => {
            this.snackbarService.error('Failed to load assignment details.');
          }
        });

        this.teacherService.getCourseDetails(this.courseName).subscribe({
          next: (courseDetails) => {
            this.availableStudents = courseDetails.students;
            console.log(courseDetails.students);
          }
        });

      } else {
        this.snackbarService.error('Invalid assignment ID.');
      }
    });
  }

  addGrade() {
    if (!this.assignmentId) {
      this.snackbarService.error('Assignment ID missing.');
      return;
    }

    if (this.studentId <= 0 || this.gradeValue <= 0 || !this.givenAt) {
      this.snackbarService.error('Please fill all fields correctly.');
      return;
    }

    this.gradeService.addGrade(this.studentId, Number(this.assignmentId), this.gradeValue, this.givenAt).subscribe({
      next: (response) => {
        this.snackbarService.success('Grade added successfully!');
        this.studentId = 0;
        this.gradeValue = 0;
        this.givenAt = '';
      },
      error: (error) => {
        this.snackbarService.error('Failed to add grade.');
      }
    });
  }
}