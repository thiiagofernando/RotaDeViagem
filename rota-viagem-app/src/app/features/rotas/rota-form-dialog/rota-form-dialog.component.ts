import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Rota } from '../../../core/models/rota.model';

@Component({
  selector: 'app-rota-form-dialog',
  templateUrl: './rota-form-dialog.component.html',
})
export class RotaFormDialogComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<RotaFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Rota
  ) {
    this.form = this.fb.group({
      id: [data?.id],
      origem: [data?.origem || '', [Validators.required, Validators.minLength(3), Validators.maxLength(3)]],
      destino: [data?.destino || '', [Validators.required, Validators.minLength(3), Validators.maxLength(3)]],
      valor: [data?.valor || '', [Validators.required, Validators.min(0.01)]]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
