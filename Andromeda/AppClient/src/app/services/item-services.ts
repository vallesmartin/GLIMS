import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { MessageService } from '../services/message.service';
import { SERVICES } from './services-list';
import { SecurityService } from '../security/security.service';
import { Item } from '../models/item/item';

@Injectable({
    providedIn: 'root'
})

  export class ItemService {

    constructor(private http: HttpClient,
        private securityService: SecurityService, 
        private messageService: MessageService) { }
  
    getAll(): Observable<Item[]>{
      let headers = new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
      })
      return this.http.get<Item[]>(SERVICES.API_BaseUrl + SERVICES.ITEM_GetAll, {headers:headers});
    }
    getById(id: number): Observable<Item>{
      let headers = new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
      })
      return this.http.get<Item>(SERVICES.API_BaseUrl + SERVICES.ITEM_GetById + id.toString(), {headers:headers});
    }
    save(item: Item | undefined){
      let headers = new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
      })
      const body = JSON.stringify(item);
      return this.http.post<Item>(SERVICES.API_BaseUrl + SERVICES.ITEM_Save, body, {headers:headers});
    }
  }
  