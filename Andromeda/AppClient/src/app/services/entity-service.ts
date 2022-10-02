import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { MessageService } from '../services/message.service';
import { SERVICES } from './services-list';
import { SecurityService } from '../security/security.service';
import { Entity } from '../models/entity/entity';

@Injectable({
    providedIn: 'root'
})
export class EntityService {

    constructor(private http: HttpClient,
        private securityService: SecurityService, 
        private messageService: MessageService) { }
  
    getById(id: number): Observable<Entity>{
      let headers = new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
      })
      return this.http.get<Entity>(SERVICES.API_BaseUrl + SERVICES.ENTITY_GetById + id.toString(), {headers:headers});
    }
    getByType(type: number): Observable<Entity[]>{
      let headers = new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
      })
      return this.http.get<Entity[]>(SERVICES.API_BaseUrl + SERVICES.ENTITY_GetByType + type.toString(), {headers:headers});
    }
    saveEntity(entity: Entity){
      const body = JSON.stringify(entity);
      let headers = new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
      })
      return this.http.post<string>(SERVICES.API_BaseUrl + SERVICES.ENTITY_Save, body, {headers:headers});
    }
  }
  
  
  