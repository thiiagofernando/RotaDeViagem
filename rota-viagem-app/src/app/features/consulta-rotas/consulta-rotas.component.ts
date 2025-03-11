import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RotaService } from '../../core/services/rota.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ResultadoConsulta } from '../../core/models/rota.model';

@Component({
  selector: 'app-consulta-rotas',
  templateUrl: './consulta-rotas.component.html',
  styleUrls: ['./consulta-rotas.component.scss']
})
export class ConsultaRotasComponent {
  form: FormGroup;
  resultado: ResultadoConsulta | null = null;
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private rotaService: RotaService,
    private snackBar: MatSnackBar
  ) {
    this.form = this.fb.group({
      origem: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(3)]],
      destino: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(3)]]
    });
  }

  async onSubmit(): Promise<void> {
    if (this.form.valid) {
      this.isLoading = true;
      try {
        this.resultado = await this.rotaService.consultarMelhorRota(this.form.value);
      } catch (error) {
        this.snackBar.open('Erro ao consultar rota', 'OK', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
        this.resultado = null;
      } finally {
        this.isLoading = false;
      }
    }
  }
}
