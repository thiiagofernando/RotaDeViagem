import { Injectable } from '@angular/core';
import axios, { AxiosInstance } from 'axios';
import { Rota, ConsultaRota, ResultadoConsulta } from '../models/rota.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RotaService {
  private api: AxiosInstance;

  constructor() {
    this.api = axios.create({
      baseURL: environment.apiUrl
    });
  }

  async getRotas(): Promise<Rota[]> {
    const response = await this.api.get<Rota[]>('/api/rota');
    return response.data;
  }

  async getRota(id: number): Promise<Rota> {
    const response = await this.api.get<Rota>(`/api/rota/${id}`);
    return response.data;
  }

  async createRota(rota: Rota): Promise<Rota> {
    const response = await this.api.post<Rota>('/api/rota', rota);
    return response.data;
  }

  async updateRota(id: number, rota: Rota): Promise<Rota> {
    const response = await this.api.put<Rota>(`/api/rota/${id}`, rota);
    return response.data;
  }

  async deleteRota(id: number): Promise<void> {
    await this.api.delete(`/api/rota/${id}`);
  }

  async consultarMelhorRota(consulta: ConsultaRota): Promise<ResultadoConsulta> {
    const response = await this.api.get<ResultadoConsulta>('/api/rota/consulta', {
      params: consulta
    });
    return response.data;
  }
}
