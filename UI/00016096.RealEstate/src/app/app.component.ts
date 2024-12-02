  import { HttpClient, HttpClientModule } from '@angular/common/http';
  import { Component, inject } from '@angular/core';
  import { RouterOutlet } from '@angular/router';
  import { Agent } from '../Entities/agent.entity';
  import { AsyncPipe } from '@angular/common';
  import { Property } from '../Entities/property.entity';
  import { HeaderComponent } from "./header/header.component";
  import { combineLatest, map, Observable } from 'rxjs';


  @Component({
    selector: 'app-root',
    imports: [RouterOutlet, HttpClientModule, AsyncPipe, HeaderComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
  })

  export class AppComponent {
    http = inject(HttpClient);

    agents$ = this.getAgents();
    properties$ = this.getProperties();
    propertiesWithAgents$: Observable<any[]>;

  constructor() {
    this.propertiesWithAgents$ = combineLatest([this.getProperties(), this.getAgents()]).pipe(
      map(([properties, agents]) =>
        properties.map(prop => ({
          ...prop,
          agentName: agents.find(agent => agent.id === prop.agentId)?.name || 'Unknown'
        }))
      )
    );
  }

    
    private getAgents(): Observable<Agent[]> {
      return this.http.get<Agent[]>('http://localhost:5137/api/Agents');
    }

    getProperties(): Observable<any[]> {
      return this.http.get<Property[]>('http://localhost:5137/api/Properties')
    }

    getAgentById(id : number): Observable<Agent> {
      return this.http.get<Agent>('http://localhost:5137/api/Agents/' + id);
    }

    getAgentName(agentId: number): Observable<string> {
      return this.http.get<Agent>(`http://localhost:5137/api/Agents/${agentId}`).pipe(
        map(agent => agent.name)
      );
    }
  }