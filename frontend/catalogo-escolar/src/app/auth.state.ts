import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { JwtParser } from "./_helpers/jwt-decoder";
import { User } from "./_models/user";
import { StorageService } from "./_services/storage-service/storage.service";

@Injectable({
    providedIn: 'root'
})
export class AuthState {
    private userSubject: BehaviorSubject<User | undefined>;
    public user: Observable<User | undefined>;

    constructor(private router: Router, private storageService: StorageService) {
        const token = this.storageService.getToken();

        if (token) {
            try {
                const parsedUser = new JwtParser(token).getUser();
                this.userSubject = new BehaviorSubject<User | undefined>(parsedUser);
            } catch (error) {
                console.error('Error parsing user from token:', error);
                this.userSubject = new BehaviorSubject<User | undefined>(undefined);
            }
        } else {
            this.userSubject = new BehaviorSubject<User | undefined>(undefined);
        }

        this.user = this.userSubject.asObservable();
    }

    public get userValue() {
        return this.userSubject.value;
    }

    logout() {
        this.storageService.clean();
        this.userSubject.next(undefined);
        this.router.navigate(['/login']);
    }

    setUser(token: string, rememberMe : boolean): void {
        this.storageService.saveToken(token, rememberMe);
        const parsedUser = new JwtParser(token).getUser();
        this.userSubject.next(parsedUser);
    }
}
