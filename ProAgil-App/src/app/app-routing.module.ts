import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AgendamentosComponent } from './agendamentos/agendamentos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { HomeComponent } from './home/home.component';
import { ContatosComponent } from './contatos/contatos.component';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';


const routes: Routes = [
  {path: 'user', component: UserComponent,
   children: [
     {path: 'login', component: LoginComponent},
     {path: 'registration', component: RegistrationComponent}
   ]
  },
// {path: 'evento/:id/edit', component: EventoEditComponent, canActivate: [AuthGuard]},
  {path: 'palestrantes', component: PalestrantesComponent, canActivate: [AuthGuard]},
  {path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  {path: 'contatos', component: ContatosComponent, canActivate: [AuthGuard]},
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: '**', redirectTo: 'home', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }