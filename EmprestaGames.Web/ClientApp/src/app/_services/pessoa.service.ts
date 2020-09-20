import { Inject, Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { APP_CONFIG, IAppConfig } from '../app.config';


@Injectable({
    providedIn: 'root'
})
export class PessoaService {

    private url;
    constructor(
        @Inject(APP_CONFIG) config: IAppConfig,
        private http: HttpClient
    ) {
        this.url = `${config.endpoint}/v1/`;
    }

    getPessoas(): Observable<any> {
        return this.http.get<any>(`${this.url}pessoa/get`);
    }

    setAdicionarPessoa(body): Observable<any> {
        return this.http.post<any>(`${this.url}pessoa/inserir`, body);
    }

    setRemoverPessoa(params): Observable<any> {
        // tslint:disable-next-line:prefer-const
        let body: any;
        const options = {
            params
        };

        return this.http.post<any>(`${this.url}pessoa/remover`, body, options);
    }
}
