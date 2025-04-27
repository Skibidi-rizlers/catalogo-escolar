import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Course } from '../../_models/course';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {
  private TEACHER_API_URL = 'http://localhost:5027/teacher';
  readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  constructor(private http: HttpClient) {
  }

  getCourseDetails(courseName: string) {
    const options = {
      ...this.httpOptions,
      params: { courseName: courseName },
    };

    return this.http.get<Course>(`${this.TEACHER_API_URL}/get-teacher-course`, options);
  }

  getCourses(): Observable<Course[]> {
    const options = {
      ...this.httpOptions,
      params: {},
    };

    return this.http.get<Course[]>(`${this.TEACHER_API_URL}/get-teacher-courses`, options);
  }

  modifyCourse(course: Course, newCourseName: string): void {
    const options = {
      ...this.httpOptions,
      params: { oldCourseName: course.name, courseName: newCourseName },
    };

    this.http.patch<Course>(`${this.TEACHER_API_URL}/modify-course`, course, options).subscribe(
      (response) => {
        console.log('Course modified successfully:', response);
      }
    );
  }

  deleteCourse(course: Course): void {
    const options = {
      ...this.httpOptions,
      params: { courseName: course.name },
    };

    this.http.delete<Course>(`${this.TEACHER_API_URL}/delete-course`, options).subscribe(
      (response) => {
        console.log('Course deleted successfully:', response);
      }
    );
  }

  addCourse(courseName: string): void {
    const options = {
      ...this.httpOptions,
      params: { courseName: courseName },
    };

    this.http.post<Course>(`${this.TEACHER_API_URL}/add-course`, {}, options).subscribe(
      (response) => {
        console.log('Course added successfully:', response);
      }
    );
  }

  deleteStudentFromCourse(studentName: string, courseName: string): void {
    const options = {
      ...this.httpOptions,
      params: { studentName: studentName, courseName: courseName },
    };

    this.http.delete<any>(`${this.TEACHER_API_URL}/delete-student-from-course`, options).subscribe(
      (response) => {
        console.log('Student deleted successfully:', response);
      }
    );
  }

  getStudents(): Observable<any[]> {
    const options = {
      ...this.httpOptions,
      params: {},
    };

    return this.http.get<any[]>(`${this.TEACHER_API_URL}/get-students`, options);
  }

  addStudentToCourse(studentName: string, courseName: string): void {
    const options = {
      ...this.httpOptions,
      params: { studentName: studentName, courseName: courseName },
    };

    this.http.post<any>(`${this.TEACHER_API_URL}/add-student-to-course`, {}, options).subscribe(
      (response) => {
        console.log('Student added successfully:', response);
      }
    );
  }
}
