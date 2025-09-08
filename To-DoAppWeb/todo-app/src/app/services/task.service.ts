// src/app/services/task.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Tasks } from '../interfaces/tasks';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = `${environment.apiUrl}/Tarefas`;
  private tasksSubject = new BehaviorSubject<Tasks[]>([]);
  public tasks$ = this.tasksSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadTasks();
  }

  private loadTasks(): void {
    this.http.get<Tasks[]>(this.apiUrl).subscribe(tasks => {
      this.tasksSubject.next(tasks);
    });
  }

  getTasks(filter?: string): Observable<Tasks[]> {
    let params = new HttpParams();
    if (filter) {
      params = params.set('filter', filter);
    }

    return this.http.get<any>(this.apiUrl, { params }).pipe(
    map(response => response.data), // Extrai a propriedade data
    tap(tasks => this.tasksSubject.next(tasks))
  );
  }

  getTask(id: number): Observable<Tasks> {
    return this.http.get<Tasks>(`${this.apiUrl}/${id}`);
  }

  createTask(task: Tasks): Observable<Tasks> {
    return this.http.post<Tasks>(this.apiUrl, task).pipe(
      tap(newTask => {
        const currentTasks = this.tasksSubject.value;
        this.tasksSubject.next([...currentTasks, newTask]);
      })
    );
  }

  updateTask(id: number, task: Tasks): Observable<Tasks> {
    return this.http.put<Tasks>(`${this.apiUrl}/${id}`, task).pipe(
      tap(updatedTask => {
        const currentTasks = this.tasksSubject.value;
        const index = currentTasks.findIndex(t => t.id === id);
        if (index !== -1) {
          currentTasks[index] = updatedTask;
          this.tasksSubject.next([...currentTasks]);
        }
      })
    );
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentTasks = this.tasksSubject.value;
        this.tasksSubject.next(currentTasks.filter(task => task.id !== id));
      })
    );
  }

  searchTasks(term: string): Observable<Tasks[]> {
    const params = new HttpParams().set('search', term);
    return this.http.get<Tasks[]>(this.apiUrl, { params });
  }
}
