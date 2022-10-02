import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { MessageService } from '../services/message.service';
import { SERVICES } from './services-list';
import { SecurityService } from '../security/security.service';
import { AppUser } from '../security/app-user';
import { AppUserRole } from '../security/app-user-role';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient,
    private securityService: SecurityService, 
    private messageService: MessageService) { }

  getById(id: number): Observable<AppUser>{
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    return this.http.get<AppUser>(SERVICES.API_BaseUrl + SERVICES.USER_GetById + id.toString(), {headers:headers});
  }
  getAll(): Observable<AppUser[]>{
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    return this.http.get<AppUser[]>(SERVICES.API_BaseUrl + SERVICES.USER_GetAll, {headers:headers});
  }
}


