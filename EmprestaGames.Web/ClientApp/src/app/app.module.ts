import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { EmprestimosComponent } from './pages/emprestimos/emprestimos.component';
import { JogoComponent } from './pages/jogo/jogo.component';
import { PessoaComponent } from './pages/pessoa/pessoa.component';
import { PessoaXJogosComponent } from './pages/pessoaxjogos/pessoaxjogos.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    EmprestimosComponent,
    JogoComponent,
    PessoaComponent,
    PessoaXJogosComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'emprestimos', component: EmprestimosComponent },
      { path: 'pessoa', component: PessoaComponent },
      { path: 'pessoaxjogos', component: PessoaXJogosComponent },
      { path: 'jogo', component: JogoComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
