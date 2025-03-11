import { Component, OnInit } from '@angular/core';
import { RotaService } from '../../core/services/rota.service';
import { Rota } from '../../core/models/rota.model';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RotaFormDialogComponent } from './rota-form-dialog/rota-form-dialog.component';

@Component({
  selector: 'app-rotas',
  templateUrl: './rotas.component.html',
})
export class RotasComponent implements OnInit {
  rotas: Rota[] = [];
  displayedColumns: string[] = ['origem', 'destino', 'valor', 'acoes'];
  constructor(
    private rotaService: RotaService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  async ngOnInit(): Promise<void> {
    await this.loadRotas();
  }

  async loadRotas(): Promise<void> {
    try {
      this.rotas = await this.rotaService.getRotas();
    } catch (error) {
      this.showError('Erro ao carregar rotas');
    }
  }

  openRotaDialog(rota?: Rota): void {
    const dialogRef = this.dialog.open(RotaFormDialogComponent, {
      width: '400px',
      data: rota
    });

    dialogRef.afterClosed().subscribe(async (result) => {
      if (result) {
        try {
          if (result.id) {
            await this.rotaService.updateRota(result.id, result);
            this.showSuccess('Rota atualizada com sucesso');
          } else {
            await this.rotaService.createRota(result);
            this.showSuccess('Rota criada com sucesso');
          }
          await this.loadRotas();
        } catch (error) {
          this.showError('Erro ao salvar rota');
        }
      }
    });
  }

  async deleteRota(id: number): Promise<void> {
    try {
      await this.rotaService.deleteRota(id);
      this.showSuccess('Rota excluída com sucesso');
      await this.loadRotas();
    } catch (error) {
      this.showError('Erro ao excluir rota');
    }
  }

  private showSuccess(message: string): void {
    this.snackBar.open(message, 'OK', { duration: 3000 });
  }

  private showError(message: string): void {
    this.snackBar.open(message, 'OK', { duration: 3000, panelClass: ['error-snackbar'] });
  }
}
