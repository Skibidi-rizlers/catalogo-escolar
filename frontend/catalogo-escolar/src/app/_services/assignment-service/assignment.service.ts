import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AssignmentService {
    private ASSIGNMENT_API_URL = 'http://localhost:5027/assignment';
    readonly httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
        })
    };

    constructor(private http: HttpClient) {
    }

    createAssignment(classId: Number, title: string, description: string, dueDate: string): Observable<any> {
        const options = {
            ...this.httpOptions,
            params: { classId, title, description, dueDate },
        };
        return this.http.post<any>(`${this.ASSIGNMENT_API_URL}/post`, options);
    }

    getAssignments(courseId: number): Observable<any[]> {
        const options = {
            ...this.httpOptions,
            params: { courseId: courseId.toString() },
        };
        return this.http.get<any[]>(`${this.ASSIGNMENT_API_URL}/course`, options);
    }
}
