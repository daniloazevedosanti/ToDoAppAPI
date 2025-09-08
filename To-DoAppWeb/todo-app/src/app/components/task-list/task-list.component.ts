import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Tasks } from 'src/app/interfaces/tasks';


@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit {
  @Input() tasks: Tasks[] = [];
  @Output() editTask = new EventEmitter<Tasks>();
  @Output() deleteTask = new EventEmitter<number>();
  @Output() toggleStatus = new EventEmitter<Tasks>();

  filteredTasks: Tasks[] = [];
  filter: string = 'all';
  searchTerm: string = '';

  ngOnInit(): void {
    this.applyFilter();
  }

  ngOnChanges(): void {
    this.applyFilter();
  }

  applyFilter(): void {
    let filtered = this.tasks;

    // Aplicar filtro de status
    if (this.filter === 'completed') {
      filtered = filtered.filter(task => task.concluida);
    } else if (this.filter === 'pending') {
      filtered = filtered.filter(task => !task.concluida);
    }

    // Aplicar busca
    if (this.searchTerm) {
      const term = this.searchTerm.toLowerCase();
      filtered = filtered.filter(task =>
        task.titulo.toLowerCase().includes(term) ||
        (task.descricao && task.descricao.toLowerCase().includes(term))

      );
    }

    this.filteredTasks = filtered;
  }

  onFilterChange(filter: string): void {
    this.filter = filter;
    this.applyFilter();
  }

  onSearchChange(term: string): void {
    this.searchTerm = term;
    this.applyFilter();
  }

  onToggleStatus(task: Tasks): void {
    this.toggleStatus.emit(task);
  }

  onEditTask(task: Tasks): void {
    this.editTask.emit(task);
  }

  onDeleteTask(id: number): void {
    this.deleteTask.emit(id);
  }

  trackByTaskId(index: number, task: Tasks): number {
    return task.id;
  }
}
