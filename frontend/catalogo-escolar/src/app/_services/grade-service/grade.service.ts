import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class GradeService {
    private GRADE_API_URL = 'http://localhost:5027/grade';
    readonly httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
        })
    };

    constructor(private http: HttpClient) {
    }

    addGrade(studentId: number, assignmentId: number, value: number, givenAt: string, courseId : number): Observable<any> {
        const payload = {
            studentId,
            assignmentId,
            value,
            givenAt: new Date(givenAt).toISOString(),
            courseId
        };

        return this.http.post<any>(`${this.GRADE_API_URL}/post`, payload,
            { headers: this.httpOptions.headers, responseType: 'text' as 'json' });
    }

    getGrades(assignmentId: string): Observable<any[]> {
        const options = {
            ...this.httpOptions,
            params: { assignmentId: assignmentId },
        };
        return this.http.get<any[]>(`${this.GRADE_API_URL}/get`, options);
    }
}
