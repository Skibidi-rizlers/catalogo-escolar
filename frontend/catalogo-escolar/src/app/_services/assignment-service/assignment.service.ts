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

    createAssignment(classId: number, title: string, description: string, dueDate: string): Observable<any> {
        const payload = {
            classId,
            title,
            description,
            dueDate: new Date(dueDate).toISOString()
        };

        return this.http.post<any>(`${this.ASSIGNMENT_API_URL}/post`, payload,
            { headers: this.httpOptions.headers, responseType: 'text' as 'json' });
    }

    getAssignments(courseId: number): Observable<any[]> {
        const options = {
            ...this.httpOptions,
            params: { courseId: courseId.toString() },
        };
        return this.http.get<any[]>(`${this.ASSIGNMENT_API_URL}/course`, options);
    }

    deleteAssignment(assignmentId: number) {
        const options = {
            ...this.httpOptions,
            params: { assignmentId: assignmentId.toString() },
        };
        return this.http.delete<any>(`${this.ASSIGNMENT_API_URL}/delete`, options);
    }
}
