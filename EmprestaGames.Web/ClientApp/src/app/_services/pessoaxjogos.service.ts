import { Inject, Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { APP_CONFIG, IAppConfig } from '../app.config';


@Injectable({
    providedIn: 'root'
})
export class PessoaXJogosService {

    private url;

    constructor(
        @Inject(APP_CONFIG) config: IAppConfig,
        private http: HttpClient
    ) {
        this.url = `${config.endpoint}/v1/`;
    }

    getPessoa(): Observable<any> {
        return this.http.get<any>(this.url + 'pessoa/get');
    }

    getJogos(): Observable<any> {
        return this.http.get<any>(this.url + 'jogo/get');
    }

    getBuscarPessoasxJogos(): Observable<any> {
        return this.http.get<any>(this.url + 'pessoaxjogo/get');
    }

    setAdicionarPessoaXJogos(body): Observable<any> {
        return this.http.post<any>(this.url + 'pessoaxjogo/inserir', body);
    }

    getPessoasDonasJogos(params): Observable<any> {
        const options = {
            params
        };

        return this.http.get<any>(`${this.url}pessoaxjogo/getpessoasdonos`, options);
    }

    getJogosNaoEmprestados(): Observable<any> {
        return this.http.get<any>(`${this.url}pessoaxjogo/jogonaoemprestado`);
    }

    setRemoverPessoaXJogos(params): Observable<any> {
        // tslint:disable-next-line:prefer-const
        let body: any;
        const options = {
            params
        };
        return this.http.post<any>(this.url + 'pessoaxjogo/remover', body, options);
    }

    getEmprestimos(): Observable<any> {
        return this.http.get<any>(this.url + 'pessoaxjogo/get');
    }
}
