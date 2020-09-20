import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
    providedIn: 'root'
})
export class PessoaService {

    private URL = 'https://localhost:3001/v1/';
    private autorizathion = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNjAwNTYyMDkwLCJleHAiOjE2MDA1NjkyOTAsImlhdCI6MTYwMDU2MjA5MH0.Wc15qjkTSTFei3mmP9Hlp3d_Qpx0nPsguKCU-aqtF_I';

    constructor(
        private http: HttpClient
    ) { }

    getPessoas(): Observable<any> {
        const options = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.autorizathion
            })
        };

        return this.http.get<any>(this.URL + 'pessoa/get', options);
    }

    setAdicionarPessoa(body): Observable<any> {

        const options = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.autorizathion
            })
        };

        return this.http.post<any>(this.URL + 'pessoa/inserir', body, options);
    }

    setRemoverPessoa(params): Observable<any> {
        // tslint:disable-next-line:prefer-const
        let body: any;
        const options = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.autorizathion
            }),
            params
        };

        return this.http.post<any>(this.URL + 'pessoa/remover', body, options);
    }
}
