import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-jogo',
  templateUrl: './jogo.component.html',
})
export class JogoComponent {

  private baseUrl;
  public carregando: boolean;
  private url = '';
  public jogo: Jogo;
  public jogos: Jogo[];
  public mensagemalerta = '';
  public mensagemdanger = '';
  retorno: Result;
  autorizathion = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNjAwNTYyMDkwLCJleHAiOjE2MDA1NjkyOTAsImlhdCI6MTYwMDU2MjA5MH0.Wc15qjkTSTFei3mmP9Hlp3d_Qpx0nPsguKCU-aqtF_I';

  Id = 0;
  Nome = '';
  Descricao = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = 'https://localhost:3001/';
    this.buscarJogos();
  }

  adcionarItem() {

    this.carregando = true;

    const body = { 'Id': this.Id.toString(), 'Nome': this.Nome, 'Descricao': this.Descricao };

    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.autorizathion
      })
    };

    this.http.post<Result>(this.baseUrl + 'v1/jogo/inserir', body, options).subscribe(
      result => {
        this.retorno = result;
      },
      err => this.mensagemdanger = err,
      () => {
        this.carregando = false;
        if (this.retorno.sucess === true) {
          this.mensagemalerta = this.retorno.message;
          this.limparcampos();
          this.buscarJogos();
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

  buscarJogos() {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.autorizathion
      })
    };

    this.http.get<Result>(this.baseUrl + 'v1/jogo/get', options).subscribe(result => {
      this.retorno = result;
      if (this.retorno.sucess === true) {
        this.jogos = this.retorno.itens;
      } else {
        this.jogos = [];
        this.mensagemalerta = this.retorno.message;
      }
    }, error => console.error(error));
  }

  removerJogos(row) {
    this.carregando = true;

    let body: any;
    const params = new HttpParams().set('Id', row.id.toString());

    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.autorizathion
      }),
      params
    };

    this.http.post<Result>(this.baseUrl + 'v1/jogo/remover', body, options).subscribe(
      result => {
        this.retorno = result;
      },
      err => this.mensagemdanger = err,
      () => {
        this.carregando = false;
        if (this.retorno.sucess === true) {
          this.mensagemalerta = this.retorno.message;
          this.limparcampos();
          this.buscarJogos();
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

  editarJogos(row) {
    this.Descricao = row.descricao;
    this.Id = row.id;
    this.Nome = row.nome;
  }

  limparcampos() {
    this.Id = 0;
    this.Descricao = '';
    this.Nome = '';
  }

}


