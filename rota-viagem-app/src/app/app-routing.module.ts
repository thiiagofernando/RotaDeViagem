import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RotasComponent } from './features/rotas/rotas.component';
import { ConsultaRotasComponent } from './features/consulta-rotas/consulta-rotas.component';

const routes: Routes = [
  { path: '', redirectTo: '/rotas', pathMatch: 'full' },
  { path: 'rotas', component: RotasComponent },
  { path: 'consulta', component: ConsultaRotasComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
