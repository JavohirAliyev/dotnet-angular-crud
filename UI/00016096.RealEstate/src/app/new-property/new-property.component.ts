import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Output, OnInit } from '@angular/core';
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
//00016096
@Component({
  selector: 'app-new-property',
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
export class NewPropertyComponent implements OnInit{
  constructor(private http: HttpClient,
              private fb: FormBuilder,
              private dialogRef: MatDialogRef<NewPropertyComponent>) {}

  agents: Agent[] = [];

  loadAgents() {
    this.getAgents().subscribe((agents: Agent[]) => {
      this.agents = agents;
    });
  }

  ngOnInit(): void {
    this.loadAgents();
  }

  private getAgents(): Observable<Agent[]> {
    return this.http.get<Agent[]>('http://localhost:5137/api/Agents');
  }
  
  propertyForm = new FormGroup({
    address: new FormControl<string>(''),
    price: new FormControl<number>(0),
    agentId: new FormControl<number>(1)
  });

  onPropertyFormSubmit() {
    const addPropertyRequest: Property = {
      address: this.propertyForm.value.address || '',
      price: this.propertyForm.value.price || 0,
      agentId: this.propertyForm.value.agentId || 1
    };
  
    this.addProperty(addPropertyRequest).subscribe({
      next: () => {
        alert('Property was added successfully');
      },
      error: (error) => {
        alert('Property could not be added');
      }
    })
  }

  onCancel(){
    this.dialogRef.close();
  }

  addProperty(property: Property) {
    return this.http.post<Property>('http://localhost:5137/api/Properties', property);
  }
}
