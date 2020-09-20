import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { JogoService } from 'src/app/_services/jogo.service';


@Component({
  selector: 'app-jogo',
  templateUrl: './jogo.component.html',
})
export class JogoComponent implements OnInit {


  public carregando: boolean;

  public jogo: Jogo;
  public jogos: Jogo[];
  public mensagemalerta = '';
  public mensagemdanger = '';
  retorno: Result;

  Id = 0;
  Nome = '';
  Descricao = '';

  constructor(private jogoService: JogoService) {

  }
  ngOnInit(): void {
    this.buscarJogos();
  }

  adcionarItem() {

    this.carregando = true;

    const body = { 'Id': this.Id.toString(), 'Nome': this.Nome, 'Descricao': this.Descricao };

    this.jogoService.setAdicionarJogo(body).subscribe(
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
    this.jogoService.getJogos().subscribe(result => {
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

    const params = new HttpParams().set('Id', row.id.toString());
    this.jogoService.setRemoverJogos(params).subscribe(
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


