import { Tasks } from '../interfaces/tasks';

export class TaskModel implements Tasks {
  constructor(
    public id: number = 0,
    public titulo: string = '',
    public descricao: string = '',
    public dataCriacao: Date = new Date(),
    public concluida: boolean = false
  ) { }
}
