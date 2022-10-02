import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { AppUser } from './app-user';
import { AppUserAuth } from './app-user-auth';
import { AppUserRole } from './app-user-role';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { SERVICES } from '../services/services-list';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  public userName: any = localStorage.getItem("user");
  public isAuth: boolean = false;
  public securityObject: AppUserAuth = new AppUserAuth();

  public constructor(private http: HttpClient, private router: Router) {
    
  }

  public checkAuth(): boolean {
    const user = localStorage.getItem("user");
    if (user == null || user == "")
      return false;
    else
      return true;
  }

  public validateRole(): Observable<AppUserRole>{
    let thisrole: AppUserRole = new AppUserRole();
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    return this.http.get<AppUserRole>(SERVICES.API_BaseUrl + SERVICES.USERROLE_Get + this.userName, {headers:headers});
  }

  public login(userData: AppUser): Observable<string>{
    this.resetSecurityObject();
    const body = JSON.stringify(userData);
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    return this.http.post<string>(SERVICES.API_BaseUrl + SERVICES.LOGIN_Authenticate, body, {headers: headers})
    ;
  }

  public changePass(pass: string): Observable<boolean>{
    let user: AppUser = new AppUser();
    user.Username = this.userName;
    user.UserPassword = pass;
    const body = JSON.stringify(user);
    let headers = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem("bearerToken")
    })
    return this.http.post<boolean>(SERVICES.API_BaseUrl + SERVICES.USER_ChangePassword, body, {headers: headers})
    ;
  }

  public logout(): void {
    this.resetSecurityObject();
  }

  private resetSecurityObject(): void {
    localStorage.setItem("role", "");
    localStorage.setItem("user","");
    localStorage.setItem("bearerToken","");
  }

}
