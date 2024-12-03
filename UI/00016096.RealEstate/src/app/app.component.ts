  import { HttpClient, HttpClientModule } from '@angular/common/http';
  import { Component, inject } from '@angular/core';
  import { Agent } from '../Entities/agent.entity';
  import { AsyncPipe } from '@angular/common';
  import { Property } from '../Entities/property.entity';
  import { HeaderComponent } from "./header/header.component";
  import { BehaviorSubject, combineLatest, map, Observable } from 'rxjs';
  import {MatDialog, MatDialogModule} from "@angular/material/dialog"
  import { NewAgentComponent } from './new-agent/new-agent.component';
import { NewPropertyComponent } from './new-property/new-property.component';

//00016096
  @Component({
    selector: 'app-root',
    imports: [HttpClientModule, AsyncPipe, HeaderComponent, MatDialogModule],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
  })

  export class AppComponent {
    http = inject(HttpClient);

    agents$ = this.getAgents();
    properties$ = this.getProperties();
    propertiesWithAgents$: Observable<any[]>;

  constructor(private dialog:MatDialog) {
    this.propertiesWithAgents$ = combineLatest([this.getProperties(), this.getAgents(), this.currentPage$]).pipe(
      map(([properties, agents]) =>
        properties.map(prop => ({
          ...prop,
          agentName: agents.find(agent => agent.id === prop.agentId)?.name || 'Unknown',
          agentNumber: agents.find(agent => agent.id === prop.agentId)?.phone || 'Unknown'
        })).slice((this.currentPage-1)*this.pageSize, this.currentPage*this.pageSize)
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

    newAgent(){
      var agentForm = this.dialog.open(NewAgentComponent,{
        width: '60%',
        height: '400px'
      })
    }

    newProperty(){
      var propertyForm = this.dialog.open(NewPropertyComponent,{
        width: '60%',
        height: '500px'
      })
    }

    onNewAgentFormSubmit(){
      const addAgentRequest = {
        name: this.newAgent.name
      }
    }

    private currentPageSubject = new BehaviorSubject<number>(1); // Current page state
    currentPage$ = this.currentPageSubject.asObservable();
    pageSize = 6;

    paginatedProperties$ = combineLatest([this.properties$, this.currentPage$]).pipe(
      map(([properties, currentPage]) => {
        const startIndex = (currentPage - 1) * this.pageSize;
        return properties.slice(startIndex, startIndex + this.pageSize);
      })
    );

    totalPages = 1;
    currentPage = 1;

    ngOnInit() {
      this.properties$.subscribe(properties => {
        this.totalPages = Math.ceil(properties.length / this.pageSize);
      });
    }
  
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
        this.currentPageSubject.next(this.currentPage);
      }
    }
  
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
        this.currentPageSubject.next(this.currentPage);
      }
    }
  }