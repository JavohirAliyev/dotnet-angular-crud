import { HttpClient } from '@angular/common/http';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, FormGroup, ReactiveFormsModule, FormBuilder } from '@angular/forms'; 
import { Agent } from '../../Entities/agent.entity';
import { Property } from '../../Entities/property.entity';
import { Observable } from 'rxjs/internal/Observable';
import { MatSelectModule } from '@angular/material/select';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
//00016096
@Component({
  selector: 'app-new-property',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
  templateUrl: './new-property.component.html',
  styleUrl: './new-property.component.css'
})

export class NewPropertyComponent implements OnInit {
  isEditMode = false;
  propertyForm: FormGroup;

  agents: Agent[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Property,
    private http: HttpClient,
    private dialogRef: MatDialogRef<NewPropertyComponent>
  ) {
    if (data) {
      this.isEditMode = true;
    }
    this.propertyForm = new FormGroup({
      address: new FormControl(data?.address || ''),
      price: new FormControl(data?.price || 0),
      agentId: new FormControl(data?.agentId || 0),
    });
  }

  
  ngOnInit(): void {
    this.loadAgents();
  }

  loadAgents() {
    this.getAgents().subscribe((agents: Agent[]) => {
      this.agents = agents;
    });
  }

  private getAgents(): Observable<Agent[]> {
    return this.http.get<Agent[]>('http://localhost:5137/api/Agents');
  }

  onPropertyFormSubmit() {
    const propertyRequest: Property = {
      address: this.propertyForm.value.address || '',
      price: this.propertyForm.value.price || 0,
      agentId: this.propertyForm.value.agentId || 1
    };

    if (this.isEditMode) {
      this.updateProperty(this.data.id!, propertyRequest);
    } else {
      this.addProperty(propertyRequest);
    }
  }

  private updateProperty(id: number, property: Property) {
    this.http.put<Property>(`http://localhost:5137/api/Properties/${id}`, property)
      .subscribe({
        next: () => {
          alert('Property was updated successfully');
          this.clearEditingState();
          this.dialogRef.close(property);
        },
        error: (error) => {
          alert('Property could not be updated');
        }
      });
  }

  private addProperty(property: Property) {
    this.http.post<Property>('http://localhost:5137/api/Properties', property)
      .subscribe({
        next: () => {
          alert('Property was added successfully');
          this.dialogRef.close(property);
        },
        error: (error) => {
          alert('Property could not be added');
        }
      });
  }

  private clearEditingState() {
    localStorage.removeItem('propertyToEdit');
  }

  onCancel() {
    this.clearEditingState();
    this.dialogRef.close();
  }
}