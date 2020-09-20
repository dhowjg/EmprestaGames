import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AlertService } from '../_services/alert.service';
import { AuthenticationService } from '../_services/authentication.service';
import { JogoService } from '../_services/jogo.service';


@Component({
  selector: 'app-jogo',
  templateUrl: './jogo.component.html',
})
export class JogoComponent implements OnInit {


  public carregando: boolean;

  public jogo: Jogo;
  public jogos: Jogo[];
  retorno: Result;

  Id = 0;
  Nome = '';
  Descricao = '';

  constructor(private jogoService: JogoService,
    private alertService: AlertService,
    private router: Router, private authenticationService: AuthenticationService) {
    if (this.authenticationService.currentUserValue === null) {
      this.router.navigate(['/']);
    }
  }
  ngOnInit(): void {
    this.buscarJogos();
  }

  adcionarItem() {

    this.carregando = true;

    const body = { 'Id': this.Id.toString(), 'Nome': this.Nome, 'Descricao': this.Descricao };

    this.jogoService.setAdicionarJogo(body)
      .pipe(first())
      .subscribe(
        data => {
          if (data.sucess === true) {
            this.limparcampos();
            this.buscarJogos();
          }
        },
        error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  buscarJogos() {
    this.jogoService.getJogos()
      .pipe(first())
      .subscribe(
        data => {
          this.retorno = data;
          if (this.retorno.sucess === true) {
            this.jogos = this.retorno.itens;
          } else {
            this.jogos = [];
          }
        }, error => {
          this.alertService.error(error);
          this.carregando = false;
        });
  }

  removerJogos(row) {
    this.carregando = true;

    const params = new HttpParams().set('Id', row.id.toString());
    this.jogoService.setRemoverJogos(params).pipe(first())
      .subscribe(
        data => {
          this.retorno = data;
          this.buscarJogos();
        },
        error => {
          this.alertService.error(error);
          this.carregando = false;
        });
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


