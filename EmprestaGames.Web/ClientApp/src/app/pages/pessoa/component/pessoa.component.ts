import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { PessoaService } from '../services/pessoa.service';

@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.component.html',
})
export class PessoaComponent {

  public carregando: boolean;
  public pessoa: Pessoa;
  public mensagemalerta = '';
  public mensagemdanger = '';
  retorno: Result;
  public pessoas: Pessoa[];

  Id = 0;
  Nome = '';

  constructor(private servicePessoa: PessoaService) {
    this.buscarPessoas();
  }

  adcionarItem() {

    this.carregando = true;

    const body = { 'Id': this.Id.toString(), 'Nome': this.Nome };

    this.servicePessoa.setAdicionarPessoa(body).subscribe(
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

  buscarPessoas() {
    this.servicePessoa.getPessoas().subscribe(result => {
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
    const params = new HttpParams().set('Id', row.id.toString());

    this.servicePessoa.setRemoverPessoa(params).subscribe(
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


