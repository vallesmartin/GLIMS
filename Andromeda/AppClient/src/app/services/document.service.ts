import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Document } from '../models/document/document';
import { MessageService } from '../services/message.service';
import { SERVICES } from './services-list';
import { SecurityService } from '../security/security.service';
import { DocumentLine } from '../models/document/doument-line';
import { ApiResponse } from '../models/api-response';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  constructor(private http: HttpClient,private securityService: SecurityService, private messageService: MessageService) { }

  getById(id: number): Observable<Document>{
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    return this.http.get<Document>(SERVICES.API_BaseUrl + SERVICES.DOCUMENT_GetById + id.toString(), {headers:headers});
  }
  getByStatus(status: number): Observable<Document[]>{
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    return this.http.get<Document[]>(SERVICES.API_BaseUrl + SERVICES.DOCUMENT_GetByStatus + status.toString(), {headers:headers});
  }
  getLastActivity(): Observable<Document[]>{
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    return this.http.get<Document[]>(SERVICES.API_BaseUrl + SERVICES.DOCUMENT_GetByActivity + status.toString(), {headers:headers});
  }
  setPrepared(document: Document | undefined): Observable<ApiResponse>{
    console.log("setPrepared:"+document);
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    const body = JSON.stringify(document);
    return this.http.post<ApiResponse>(SERVICES.API_BaseUrl + SERVICES.DOCUMENT_SetPrepared, body, {headers:headers});
  }
  setBilled(document: Document | undefined): Observable<ApiResponse>{
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    const body = JSON.stringify(document);
    return this.http.post<ApiResponse>(SERVICES.API_BaseUrl + SERVICES.DOCUMENT_SetBilled, body, {headers:headers});
  }
  updateLine(line: DocumentLine){
    const body = JSON.stringify(line);
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    return this.http.post<string>(SERVICES.API_BaseUrl + SERVICES.DOCUMENT_UpdateLine, body, {headers:headers});
  }
}


