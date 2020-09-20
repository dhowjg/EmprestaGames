import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AlertService } from '../_services/alert.service';
import { AuthenticationService } from '../_services/authentication.service';
import { EmprestimoService } from '../_services/emprestimo.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  public carregando: boolean;
  retorno: Result;
  public jogosxpessoas: PessoaXJogos[];

  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private emprestimoService: EmprestimoService,
    private router: Router,
    private alertService: AlertService,
    private authenticationService: AuthenticationService) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }
  ngOnInit(): void {
    this.buscarEmprestimos();
  }

  buscarEmprestimos() {

    this.emprestimoService.getSomenteEmprestimos().pipe(first())
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
}
