import { Routes } from '@angular/router';
import { RotasComponent } from './features/rotas/rotas.component';
import { ConsultaRotasComponent } from './features/consulta-rotas/consulta-rotas.component';

export const routes: Routes = [
  { path: '', redirectTo: '/rotas', pathMatch: 'full' },
  { path: 'rotas', component: RotasComponent },
  { path: 'consulta', component: ConsultaRotasComponent }
];