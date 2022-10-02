import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";
import {FormControl, Validators} from '@angular/forms';
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';



import { AppUser } from './app-user';
import { AppUserAuth } from './app-user-auth';
import { SecurityService } from './security.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  title = "Sesion";
  cssProgress = "progress-hidden";
  hide = true;
  username = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required]);
  user: AppUser = new AppUser();
  securityObject: AppUserAuth = new AppUserAuth();
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  
  constructor(private securityService: SecurityService, 
    private router: Router, 
    private _snackBar: MatSnackBar,
    private titleService: Title) {
      this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
  }

  login() {
    this.cssProgress = "progress";
    this.user.Username = this.username.value;
    this.user.Password = this.password.value;
    this.securityService.login(this.user).subscribe( data => {
      console.log(data);
      if(data.length > 0){
        localStorage.setItem("user",this.username.value);
        this.securityService.userName = this.username.value;
        localStorage.setItem("bearerToken",data);
        this.cssProgress = "progress-hidden";
        this.router.navigate(['/dashboard']);
      }
      },(err) => {
        this.openSnackBar();
        this.cssProgress = "progress-hidden"; }
      );
  } 

  logout() {
    this.securityService.logout();
    this.router.navigate(['/login']);
  }

  isUserValid(): boolean{
    return this.securityService.checkAuth();
  }

  getErrorMessage() {
    if (this.username.hasError('required')) {
      return 'Debe ingresar un usuario';
    }
    return this.username.hasError('username') ? 'No es un usuario valido' : '';
  }

  getErrorPassMessage() {
    if (this.password.hasError('required')) {
      return 'Debe ingresar una contraseña';
    }
    return this.password.hasError('password') ? 'No es un password valido' : '';
  }

  openSnackBar() {
    this._snackBar.open('Inicio de sesion incorrecto', 'Ok', {
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
      duration: 3000
    });
  }
}