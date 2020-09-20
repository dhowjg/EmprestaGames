import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/internal/operators/first';
import { AlertService } from '../_services/alert.service';
import { AuthenticationService } from '../_services/authentication.service';
import { PessoaXJogosService } from '../_services/pessoaxjogos.service';



@Component({
  selector: 'app-pessoaxjogos',
  templateUrl: './pessoaxjogos.component.html',
})
export class PessoaXJogosComponent implements OnInit {

  public carregando: boolean;
  retorno: Result;

  public jogosxpessoas: PessoaXJogos[];
  public jogos;
  public pessoas;
  public select: FormGroup;
  public select2: FormGroup;

  Id = 0;
  PessoaId = 0;
  JogoId = 0;

  constructor(
    private formBuilder: FormBuilder,
    private pessoaXjogosService: PessoaXJogosService,
    private alertService: AlertService,
    private router: Router, private authenticationService: AuthenticationService) {
    if (this.authenticationService.currentUserValue === null) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    /** Inicia Primeiro Select  */
    this.select = this.formBuilder.group({
      select: ['']
    });

    /** Inicia Segundo Select  */
    this.select2 = this.formBuilder.group({
      select: ['']
    });


    this.getJogos();
    this.getPessoas();
    this.buscarJogosPessoas();
  }

  /** Get primeiro select  removendo removendo valores null */
  getPessoas() {
    this.pessoaXjogosService.getPessoa().subscribe(data => {
      this.pessoas = data.itens;
      console.log(this.pessoas);
    });
  }

  getJogos() {
    this.pessoaXjogosService.getJogos().subscribe(data => {
      this.jogos = data.itens;
      console.log(this.jogos);
    });
  }

  adcionarItem() {

    this.carregando = true;

    const body = { 'Id': this.Id.toString(), 'PessoaId': this.select.value.select.id, 'JogoId': this.select2.value.select.id };

    this.pessoaXjogosService.setAdicionarPessoaXJogos(body)
      .pipe(first())
      .subscribe(
        data => {
          if (data.sucess === true) {
            this.limparcampos();
            this.buscarJogosPessoas();
          }
        },
        error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  removerPessoXJogos(row) {
    this.carregando = true;
    const params = new HttpParams().set('Id', row.id.toString());

    this.pessoaXjogosService.setRemoverPessoaXJogos(params).pipe(first())
      .subscribe(
        data => {
          this.retorno = data;
          this.buscarJogosPessoas();
        },
        error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  buscarJogosPessoas() {

    this.pessoaXjogosService.getBuscarPessoasxJogos().pipe(first())
      .subscribe(
        data => {
          this.retorno = data;
          if (this.retorno.sucess === true) {
            this.jogosxpessoas = this.retorno.itens;
          } else {
            this.jogosxpessoas = [];
          }
        }, error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  limparcampos() {
    this.Id = 0;
    this.PessoaId = 0;
    this.JogoId = 0;
  }

}


