import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Tasks } from 'src/app/interfaces/tasks';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss']
})
export class TaskFormComponent implements OnInit, OnChanges {
  @Input() task: Tasks | null = null;
  @Output() submitTask = new EventEmitter<Partial<Tasks>>();
  @Output() cancelEdit = new EventEmitter<void>();

  taskForm: FormGroup;
  isEditing = false;

  constructor(private fb: FormBuilder) {
    this.taskForm = this.createForm();
  }

  ngOnInit(): void {
    this.initForm();
  }

  ngOnChanges(): void {
    this.initForm();
  }

  createForm(): FormGroup {
    return this.fb.group({
      titulo: ['', [Validators.required, Validators.minLength(3)]],
      descricao: ['']
    });
  }

  initForm(): void {
    if (this.task) {
      this.isEditing = true;
      this.taskForm.patchValue({
        titulo: this.task.titulo,
        descricao: this.task.descricao || ''
      });
    } else {
      this.isEditing = false;
      this.taskForm.reset();
    }
  }

  onSubmit(): void {
    if (this.taskForm.valid) {
      const formValue = this.taskForm.value;
      const taskData: Partial<Tasks> = {
        titulo: formValue.titulo,
        descricao: formValue.descricao
      };

      this.submitTask.emit(taskData);
      this.taskForm.reset();
    }
  }

  onCancel(): void {
    this.cancelEdit.emit();
    this.taskForm.reset();
    this.isEditing = false;
  }

  get titulo() {
    return this.taskForm.get('titulo');
  }
}
