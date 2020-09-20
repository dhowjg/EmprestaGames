import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { pipe } from 'rxjs';
import { first } from 'rxjs/operators';
import { AlertService } from '../_services/alert.service';
import { AuthenticationService } from '../_services/authentication.service';
import { PessoaService } from '../_services/pessoa.service';


@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.component.html',
})
export class PessoaComponent {

  public carregando: boolean;
  public pessoa: Pessoa;
  retorno: Result;
  public pessoas: Pessoa[];

  Id = 0;
  Nome = '';

  constructor(private servicePessoa: PessoaService,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue === null) {
      this.router.navigate(['/']);
    }
    this.buscarPessoas();
  }

  adcionarItem() {

    this.carregando = true;

    const body = { 'Id': this.Id.toString(), 'Nome': this.Nome };

    this.servicePessoa.setAdicionarPessoa(body)
      .pipe(first())
      .subscribe(
        data => {
          if (data.sucess === true) {
            this.limparcampos();
            this.buscarPessoas();
          }
        },
        error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  buscarPessoas() {
    this.servicePessoa.getPessoas()
      .pipe(first())
      .subscribe(
        data => {
          this.retorno = data;
          if (this.retorno.sucess === true) {
            this.pessoas = this.retorno.itens;
          } else {
            this.pessoas = [];
          }
        }, error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  removerPessoas(row) {

    this.carregando = true;
    const params = new HttpParams().set('Id', row.id.toString());

    this.servicePessoa.setRemoverPessoa(params)
      .pipe(first())
      .subscribe(
        data => {
          this.retorno = data;
          this.buscarPessoas();
        },
        error => {
          this.alertService.error(error);
          this.carregando = false;
        });
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


