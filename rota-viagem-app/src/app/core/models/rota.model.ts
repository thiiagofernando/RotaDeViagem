export interface Rota {
    id?: number;
    origem: string;
    destino: string;
    valor: number;
  }
  
  export interface ConsultaRota {
    origem: string;
    destino: string;
  }
  
  export interface ResultadoConsulta {
    trajeto: string[];
    custoTotal: number;
    rotaFormatada: string;
  }
  