import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PessoaXJogosService } from 'src/app/_services/pessoaxjogos.service';



@Component({
  selector: 'app-pessoaxjogos',
  templateUrl: './pessoaxjogos.component.html',
})
export class PessoaXJogosComponent implements OnInit {

  private baseUrl;
  public carregando: boolean;
  private url = '';

  public mensagemalerta = '';
  public mensagemdanger = '';
  retorno: Result;

  public jogosxpessoas: PessoaXJogos[];
  public jogos;
  public pessoas;
  public select: FormGroup;
  public select2: FormGroup;

  Id = 0;
  PessoaId = 0;
  JogoId = 0;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string,
    private formBuilder: FormBuilder,
    private pessoaXjogosService: PessoaXJogosService) {

    this.baseUrl = 'https://localhost:5001/';
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

    this.pessoaXjogosService.setAdicionarPessoaXJogos(body).subscribe(
      result => {
        this.retorno = result;
      },
      err => this.mensagemdanger = err,
      () => {
        this.carregando = false;
        if (this.retorno.sucess === true) {
          this.mensagemalerta = this.retorno.message;
          this.limparcampos();
          this.buscarJogosPessoas();
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

  removerPessoXJogos(row) {
    this.carregando = true;
    const params = new HttpParams().set('Id', row.id.toString());

    this.pessoaXjogosService.setRemoverPessoaXJogos(params).subscribe(
      result => {
        this.retorno = result;
      },
      err => this.mensagemdanger = err,
      () => {
        this.carregando = false;
        if (this.retorno.sucess === true) {
          this.mensagemalerta = this.retorno.message;
          this.limparcampos();
          this.buscarJogosPessoas();
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

  buscarJogosPessoas() {

    this.pessoaXjogosService.getBuscarPessoasxJogos().subscribe(result => {
      this.retorno = result;
      if (this.retorno.sucess === true) {
        this.jogosxpessoas = this.retorno.itens;
        console.log(this.jogosxpessoas);
      } else {
        this.jogosxpessoas = [];
        this.mensagemalerta = this.retorno.message;
      }
    }, error => console.error(error));
  }

  limparcampos() {
    this.Id = 0;
    this.PessoaId = 0;
    this.JogoId = 0;
  }

}


