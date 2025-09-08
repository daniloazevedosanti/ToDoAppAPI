export interface Tasks {
  id: number;
  titulo: string;
  descricao?: string;
  dataCriacao: Date;
  concluida: boolean;
}
