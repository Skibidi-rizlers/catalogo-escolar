import { Component } from '@angular/core';
import { FormBuilder, FormsModule } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService } from '../_services/auth-service/auth.service';
import { SnackbarService } from '../_services/snackbar-service/snackbar.service';
import { TeacherService } from '../_services/teacher/teacher.service';
import { CommonModule, DatePipe, NgFor } from '@angular/common';
import { Student } from '../_models/student';
import { AssignmentService } from '../_services/assignment-service/assignment.service';
import { Assignment } from '../_models/assignment';

@Component({
  selector: 'app-course',
  imports: [FormsModule, CommonModule, RouterModule],
  providers: [DatePipe],
  templateUrl: './course.component.html',
  styleUrl: './course.component.scss'
})
export class CourseComponent {
  view = 'assignments';
  course: any | undefined;
  availableStudents: Student[] = [];
  assignments: Assignment[] = [];
  selectedStudent: Student | null = null;
  newAssignment: Assignment = {
    title: '',
    description: '',
    dueDate: '',
    id: 0
  };

  constructor(private route: ActivatedRoute, private title: Title, private router: Router, private snackbarService: SnackbarService,
    private teacherService: TeacherService, private assignmentService: AssignmentService, public datePipe: DatePipe) {
    this.title.setTitle('Course');

    teacherService.getStudents().subscribe((data: Student[]) => {
      this.availableStudents = data;
    });
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const courseName = params.get('courseName');
      if (courseName) {
        this.teacherService.getCourseDetails(courseName).subscribe({
          next: (courseDetails) => {
            this.course = courseDetails;
            this.title.setTitle(courseDetails.name);
            this.refreshAssignments();
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

  refreshAssignments() {
    this.assignmentService.getAssignments(this.course.id).subscribe((data: any[]) => {
      this.assignments = data;
    });
  }

  removeStudent(index: number) {
    console.log(this.course);
    this.teacherService.deleteStudentFromCourse(this.course.students[index].name, this.course.name);
    this.snackbarService.success('Student removed successfully!');
    this.course.students.splice(index, 1);
  }

  createAssignment() {
    if (this.newAssignment.title && this.newAssignment.description && this.newAssignment.dueDate) {
      this.assignmentService.createAssignment(this.course.id, this.newAssignment.title, this.newAssignment.description, this.newAssignment.dueDate).subscribe(
        response => {
          this.snackbarService.success('Assignment created successfully!');
          this.refreshAssignments();
          this.view = 'assignments';
        },
        error => {
          this.snackbarService.error('Failed to create assignment.');
        }
      );
    } else {
      this.snackbarService.error('All fields are required.');
    }
  }

  deleteAssignment(index: number) {
    this.assignmentService.deleteAssignment(this.assignments[index].id).subscribe(
      response => {
        this.snackbarService.success('Assignment deleted successfully!');
        this.refreshAssignments();
      },
      error => {
        this.snackbarService.error('Failed to delete assignment.');
      }
    );
  }

  goToAssignment(assignment: Assignment) {
    this.router.navigate(['/assignment', this.course.name, assignment.id]);
  }
}
