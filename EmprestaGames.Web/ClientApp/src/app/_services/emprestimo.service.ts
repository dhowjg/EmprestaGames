import { Inject, Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { APP_CONFIG, IAppConfig } from '../app.config';


@Injectable({
    providedIn: 'root'
})
export class EmprestimoService {

    private url;
    constructor(
        @Inject(APP_CONFIG) config: IAppConfig,
        private http: HttpClient
    ) {
        this.url = `${config.endpoint}/v1/`;
    }

    getEmprestimos(): Observable<any> {
        return this.http.get<any>(`${this.url}emprestimo/get`);
    }

    getSomenteEmprestimos(): Observable<any> {
        return this.http.get<any>(`${this.url}emprestimo/soemprestados`);
    }

    setAdicionarEmprestimo(body): Observable<any> {
        return this.http.post<any>(`${this.url}emprestimo/inserir`, body);
    }

    setRemoverEmprestimo(params): Observable<any> {
        // tslint:disable-next-line:prefer-const
        let body: any;
        const options = {
            params
        };

        return this.http.post<any>(`${this.url}emprestimo/remover`, body, options);
    }

    setDevolverEmprestimo(params): Observable<any> {
        // tslint:disable-next-line:prefer-const
        let body: any;
        const options = {
            params
        };

        return this.http.post<any>(`${this.url}emprestimo/devolver`, body, options);
    }
}
