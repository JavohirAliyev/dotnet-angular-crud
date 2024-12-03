import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms'; 
import { Agent } from '../../Entities/agent.entity';

@Component({
  selector: 'app-new-agent',
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './new-agent.component.html',
  styleUrl: './new-agent.component.css',
})
export class NewAgentComponent {
  
  constructor(private http: HttpClient) {}

  agentForm = new FormGroup({
    name: new FormControl<string>(''),
    phone: new FormControl<string>(''),
  });

  onAgentFormSubmit() {
    const addAgentRequest: Agent = {
      name: this.agentForm.value.name || '',
      phone: this.agentForm.value.phone || '',
      properties: []
    };

    this.addAgent(addAgentRequest).subscribe({
      next: () => {
        alert('Agent was added successfully');
      },
      error: (error) => {
        alert('Agent could not be added');
      }
    })
  }

  addAgent(agent: Agent) {
    return this.http.post<Agent>('http://localhost:5137/api/Agents', agent);
  }
}
