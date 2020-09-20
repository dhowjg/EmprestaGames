import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Usuario } from '../_models/usuario';
import { APP_CONFIG, IAppConfig } from '../app.config';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private url;
    private currentUserSubject: BehaviorSubject<Usuario>;
    public currentUser: Observable<Usuario>;

    constructor(@Inject(APP_CONFIG) config: IAppConfig, private http: HttpClient) {
        this.url = `${config.endpoint}/v1/`;
        this.currentUserSubject = new BehaviorSubject<Usuario>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): Usuario {
        return this.currentUserSubject.value;
    }

    login(username, password) {
        const options = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };

        const body = {
            'Nome': username,
            'Senha': password
        };

        return this.http.post<any>(`${this.url}account/login`, body, options)
            .pipe(map(user => {
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }

    logout() {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}
