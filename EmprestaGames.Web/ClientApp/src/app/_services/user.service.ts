import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { APP_CONFIG, IAppConfig } from '../app.config';
import { Usuario } from '../_models/usuario';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
    private url;

    // tslint:disable-next-line:no-shadowed-variable
    constructor(@Inject(APP_CONFIG) config: IAppConfig,
        private http: HttpClient) {
        this.url = `${config.endpoint}/v1/`;
    }

    getAll() {
        return this.http.get<Usuario[]>(this.url + '/users');
    }

    register(user: Usuario) {
        return this.http.post(this.url + '/users', user);
    }
}
