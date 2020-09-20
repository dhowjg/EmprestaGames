import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/internal/operators/first';
import { AlertService } from '../_services/alert.service';
import { AuthenticationService } from '../_services/authentication.service';
import { EmprestimoService } from '../_services/emprestimo.service';
import { JogoService } from '../_services/jogo.service';
import { PessoaService } from '../_services/pessoa.service';
import { PessoaXJogosService } from '../_services/pessoaxjogos.service';

@Component({
  selector: 'app-emprestimos',
  templateUrl: './emprestimos.component.html'
})
export class EmprestimosComponent implements OnInit {

  public carregando: boolean;
  retorno: Result;

  public jogosxpessoas: PessoaXJogos[];
  public jogos;
  public pessoasAEmprestar;
  public pessoasDono;
  public select: FormGroup;
  public select2: FormGroup;
  public select3: FormGroup;

  Id = 0;
  PessoaId = 0;
  JogoId = 0;



  constructor(
    private formBuilder: FormBuilder, private router: Router,
    private emprestimoService: EmprestimoService,
    private jogoService: JogoService,
    private pessoaService: PessoaService,
    private pessoasxjogos: PessoaXJogosService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService) {
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

    /** Inicia Terceiro Select  */
    this.select3 = this.formBuilder.group({
      select: ['']
    });


    this.getJogos();
    this.getPessoasEmprestar();
    this.buscarEmprestimos();
  }

  /** Get primeiro select  removendo removendo valores null */
  getPessoasEmprestar() {
    this.pessoaService.getPessoas().subscribe(data => {
      this.pessoasAEmprestar = data.itens;
      console.log(this.pessoasAEmprestar);
    });
  }

  /** Get primeiro select  removendo removendo valores null */
  getPessoasDonos(jogoId) {
    const params = new HttpParams().set('JogoId', jogoId.toString());

    this.pessoasxjogos.getPessoasDonasJogos(params).subscribe(data => {
      this.pessoasDono = data.itens;
      console.log(this.pessoasDono);
    });
  }

  getJogos() {
    this.pessoasxjogos.getJogosNaoEmprestados().subscribe(data => {
      this.jogos = data.itens;
      console.log(this.jogos);
    });
  }

  buscarEmprestimos() {

    this.emprestimoService.getEmprestimos().pipe(first())
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

  adcionarItem() {

    this.carregando = true;

    const body = {
      'Id': this.Id.toString(),
      'PessoaEmprestadaId': this.select3.value.select.id,
      'JogoId': this.select2.value.select.id,
      'PessoaDonoJogoId': this.select.value.select.id
    };

    this.emprestimoService.setAdicionarEmprestimo(body)
      .pipe(first())
      .subscribe(
        data => {
          if (data.sucess === true) {
            this.recarregar();
          }
        },
        error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  carregarDonoJogo() {
    console.log('Dono do jogo');
    this.getPessoasDonos(this.select2.value.select.id);
  }

  removerEmprestimo(row) {
    this.carregando = true;
    const params = new HttpParams().set('Id', row.id.toString());

    this.emprestimoService.setRemoverEmprestimo(params).pipe(first())
      .subscribe(
        data => {
          this.retorno = data;
          this.recarregar();
        },
        error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  devolverEmprestimo(row) {
    this.carregando = true;
    const params = new HttpParams().set('Id', row.id.toString());

    this.emprestimoService.setDevolverEmprestimo(params).pipe(first())
      .subscribe(
        data => {
          this.retorno = data;
          this.recarregar();
        },
        error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  recarregar() {
    this.getJogos();
    this.buscarEmprestimos();
  }
}


