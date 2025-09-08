import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Tasks } from 'src/app/interfaces/tasks';
import { LoadingService } from 'src/app/services/loading.service';
import { TaskService } from 'src/app/services/task.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  tasks: Tasks[] = [];
  editingTask: Tasks | null = null;

  constructor(
    private taskService: TaskService,
    private route: ActivatedRoute,
    public loadingService: LoadingService
  ) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks().subscribe(tasks => {
      this.tasks = tasks;
    });
  }

  onTaskSubmit(taskData: Partial<Tasks>): void {
    if (this.editingTask) {
      this.onUpdateTask(taskData);
    } else {
      this.onCreateTask(taskData);
    }
  }

  onCreateTask(taskData: Partial<Tasks>): void {
    const newTask: Partial<Tasks> = {
      ...taskData,
      dataCriacao: new Date(),
      concluida: false
    };

    this.taskService.createTask(newTask as Tasks).subscribe(() => {
      this.loadTasks();
    });
  }

  onUpdateTask(taskData: Partial<Tasks>): void {
    if (this.editingTask) {
      const updatedTask: Tasks = {
        ...this.editingTask,
        ...taskData
      };

      this.taskService.updateTask(this.editingTask.id, updatedTask).subscribe(() => {
        this.loadTasks();
        this.editingTask = null;
      });
    }
  }

  onDeleteTask(id: number): void {
    this.taskService.deleteTask(id).subscribe(() => {
      this.loadTasks();
    });
  }

  onToggleTaskStatus(task: Tasks): void {
    const updatedTask = { ...task, concluida: !task.concluida };
    this.taskService.updateTask(task.id, updatedTask).subscribe(() => {
      this.loadTasks();
    });
  }

  onEditTask(task: Tasks): void {
    this.editingTask = task;
  }

  onCancelEdit(): void {
    this.editingTask = null;
  }
}
