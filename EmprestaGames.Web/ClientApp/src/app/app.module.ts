import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { JogoComponent } from './jogo/jogo.component';
import { EmprestimosComponent } from './emprestimos/emprestimos.component';
import { PessoaComponent } from './pessoa/pessoa.component';
import { PessoaXJogosComponent } from './pessoaxjogos/pessoaxjogos.component';
import { AuthGuard } from './_helpers/auth.guard';
import { LoginComponent } from './login/login.component';
import { AlertComponent } from './_component/alert.component';
import { AppConfig, APP_CONFIG } from './app.config';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { JwtInterceptor } from './_helpers/jwt.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    EmprestimosComponent,
    JogoComponent,
    PessoaComponent,
    PessoaXJogosComponent,
    LoginComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'emprestimos', component: EmprestimosComponent },
      { path: 'pessoa', component: PessoaComponent },
      { path: 'pessoaxjogos', component: PessoaXJogosComponent },
      { path: 'jogo', component: JogoComponent },

      { path: '**', redirectTo: '' }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    {
      provide: APP_CONFIG,
      useValue: AppConfig
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
