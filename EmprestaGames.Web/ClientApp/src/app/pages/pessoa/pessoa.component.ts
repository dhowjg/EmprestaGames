import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.component.html',
})
export class PessoaComponent {

  private baseUrl;
  public carregando: boolean;
  private url = '';
  public pessoa: Pessoas;
  public mensagemalerta = '';
  public mensagemdanger = '';
  retorno: Result;
  public pessoas: Pessoas[];
  autorizathion = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNjAwNTYyMDkwLCJleHAiOjE2MDA1NjkyOTAsImlhdCI6MTYwMDU2MjA5MH0.Wc15qjkTSTFei3mmP9Hlp3d_Qpx0nPsguKCU-aqtF_I';

  Id = 0;
  Nome = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = 'https://localhost:3001/';
    this.buscarPessoas();
  }

  adcionarItem() {

    this.carregando = true;

    const body = { 'Id': this.Id.toString(), 'Nome': this.Nome };

    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.autorizathion
      })
    };

    this.http.post<Result>(this.baseUrl + 'v1/pessoa/inserir', body, options).subscribe(
      result => {
        this.retorno = result;
        if (this.retorno.sucess === true && this.retorno.itens !== '') {
          this.url = result.itens;
        }
      },
      err => this.mensagemdanger = err,
      () => {
        this.carregando = false;
        if (this.retorno.sucess === true) {
          this.mensagemalerta = this.retorno.message;
          this.limparcampos();
          this.buscarPessoas();
        } else {
          if (this.retorno.message.length > 0) {
            for (let index = 0; index < this.retorno.message.length; index++) {
              this.mensagemdanger += this.retorno.message[index] + ';  ';
            }
          } else {
            this.mensagemdanger = this.retorno.message;
          }
        }
      }
    );
  }

  buscarPessoas() {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.autorizathion
      })
    };

    this.http.get<Result>(this.baseUrl + 'v1/pessoa/get', options).subscribe(result => {
      this.retorno = result;
      if (this.retorno.sucess === true) {
        this.pessoas = this.retorno.itens;
      } else {
        this.pessoas = [];
        this.mensagemalerta = this.retorno.message;
      }
    }, error => console.error(error));
  }

  removerPessoas(row) {
    this.carregando = true;

    const body: any;
    const params = new HttpParams().set('Id', row.id.toString());

    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.autorizathion
      }),
      params
    };

    this.http.post<Result>(this.baseUrl + 'v1/pessoa/remover', body, options).subscribe(
      result => {
        this.retorno = result;
      },
      err => this.mensagemdanger = err,
      () => {
        this.carregando = false;
        if (this.retorno.sucess === true) {
          this.mensagemalerta = this.retorno.message;
          this.limparcampos();
          this.buscarPessoas();
        } else {
          if (this.retorno.message.length > 0) {
            for (let index = 0; index < this.retorno.message.length; index++) {
              this.mensagemdanger += this.retorno.message[index] + ';  ';
            }
          } else {
            this.mensagemdanger = this.retorno.message;
          }
        }
      }
    );
  }

  editarPessoas(row) {
    this.Id = row.id;
    this.Nome = row.nome;
  }

  limparcampos() {
    this.Id = 0;
    this.Nome = '';
  }

}


